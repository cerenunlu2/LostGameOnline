using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Text;

public class EtkinlikBossKontrol : NetworkBehaviour
{
    public Sprite[] GemiAnimasyon; // yukarı:0, sag ust:1, sag:2, sag alt:3, alt:4, sol alt:5, sol:6, sol ust:7 
    public float hiz;
    [SyncVar] public string geminame;
    public int HaritaKac = 1;
    private NavMeshAgent myAgent;
    [SyncVar] public int Can = 1;
    [SyncVar] public int MaxCan = 1;
    private bool gemiOldumu = false;
    public int odulElitPuanOrani = 0;

    public GameObject[] odulAlacakKisiler;
    public int[] hasarVerenOyuncularinHasarlari;

    public GameObject sonsaldiranoyuncu;
    private float sonsaldirilanzaman = 0;
    public float saldirihizi = 6f, menzil = 3.8f, hasar;
    public int OdulTp, OdulAltin, OdulAltinOran = 4, odulElitPuan;
    private SpriteRenderer spriteRenderer;
    [SyncVar(hook = nameof(SyncGemiyiHareketEttir))]
    public Vector3 target = new Vector3(0, 0, 0);

    public float sonHasarAlinanZaman = 0, sonKonumUretilenZaman = 0;
    bool gemiDuruyor = false;
    public GameObject[] gulleler;

    public int GemiAnimasyonuSonGirilenId = -1;
    private float currentAlpha = 0f;
    private float fadeSpeed;
    public float fadeDuration = 2f; // Yavaşça geçiş süresi (saniye)
    // Start is called before the first frame update

    void Start()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            odulAlacakKisiler = new GameObject[100];
            hasarVerenOyuncularinHasarlari = new int[100];
            HedefiYolla();
        }
#endif
        if (isClient)
        {
            spriteRenderer = transform.Find("Gemi").GetComponent<SpriteRenderer>();
            fadeSpeed = 255f / fadeDuration; // Transparanlık değişim hızı
            spriteRenderer.sprite = GemiAnimasyon[2];
            GetComponent<NavMeshAgent>().updateRotation = false;
            GetComponent<NavMeshAgent>().updateUpAxis = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
            myAgent = transform.GetComponent<NavMeshAgent>();
            transform.Find("OyuncuAdi").GetComponent<TextMeshPro>().text = "<color=#ff0000>" + geminame + "</color>";
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            // gemi hareket kodu
            if (sonsaldiranoyuncu != null && gemiDuruyor == false)
            {
                target = transform.position;
                SyncGemiyiHareketEttir(target, target);
                gemiDuruyor = true;
            }
            else if (Time.time - sonHasarAlinanZaman > 20 && gemiDuruyor == true && gameObject.GetComponent<NavMeshAgent>().isOnNavMesh == true)
            {
                gemiDuruyor = false;
                sonsaldiranoyuncu = null;
                HedefiYolla();
            }
            else if (Time.time - sonKonumUretilenZaman > 30 && gameObject.GetComponent<NavMeshAgent>().isOnNavMesh == true && GetComponent<NavMeshAgent>().remainingDistance < 1 && gemiDuruyor == false)
            {
                HedefiYolla();
            }
            // npc gemilerine saldirirken gulle yaratir ve oyuncuya saldirirken gulle yaratir
            if (sonsaldiranoyuncu != null && Time.time - sonsaldirilanzaman >= saldirihizi && Can > 0)
            {
                gulleYarat(sonsaldiranoyuncu);
            }
            if (Can <= 0 && sonsaldiranoyuncu != null && gemiOldumu == false)
            {
                GameManager.gm.BenSaldiriyorum.SetActive(false);
                GameManager.gm.SaldiriIptalButton.SetActive(false);
                GameManager.gm.SaldirButton.SetActive(true);
                gemiOldumu = true;
                OdulVer();
            }
        }
#endif
        if (isClient)
        {
            if (gameObject != null && Vector2.Distance(GameManager.gm.BenimGemim.transform.position, transform.position) < 11f)
            {
                transform.Find("MiniMapIcon").gameObject.SetActive(true);
                if (currentAlpha < 255f)
                {
                    currentAlpha += fadeSpeed * Time.deltaTime;
                    currentAlpha = Mathf.Clamp(currentAlpha, 0f, 255f);
                    Color spriteColor = spriteRenderer.color;
                    spriteColor.a = currentAlpha / 255f;
                    spriteRenderer.color = spriteColor;
                    if (currentAlpha > 150f)
                    {
                        transform.Find("OyuncuCanvas").gameObject.SetActive(true);
                        transform.Find("OyuncuAdi").gameObject.SetActive(true);
                    }
                }
            }
            else if (gameObject != null && Vector2.Distance(GameManager.gm.BenimGemim.transform.position, transform.position) >= 11f && currentAlpha > 0f)
            {
                transform.Find("MiniMapIcon").gameObject.SetActive(false);
                currentAlpha -= fadeSpeed * Time.deltaTime;
                currentAlpha = Mathf.Clamp(currentAlpha, 0f, 255f);
                Color spriteColor = spriteRenderer.color;
                spriteColor.a = currentAlpha / 255f;
                spriteRenderer.color = spriteColor;
                if (currentAlpha < 70f)
                {
                    transform.Find("OyuncuCanvas").gameObject.SetActive(false);
                    transform.Find("OyuncuAdi").gameObject.SetActive(false);
                }
            }
            GemiAnimasyonu();
            transform.Find("OyuncuCanvas/Can").GetComponent<Slider>().maxValue = MaxCan;
            transform.Find("OyuncuCanvas/Can").GetComponent<Slider>().value = Can;
        }
    }

#if UNITY_SERVER || UNITY_EDITOR
    private void gulleYarat(GameObject Hedef)
    {
        if (isServer)
        {
            //menzil kontrolu yapiyor
            if (Vector2.Distance(transform.position, Hedef.transform.position) <= menzil)
            {
                float vurulanHasar = Random.Range(hasar * 0.8f, hasar);
                vurulanHasar -= (vurulanHasar * Hedef.GetComponent<Player>().OyuncuElitPuanZirhBonusu) + (vurulanHasar * Hedef.GetComponent<Player>().oyuncuYetenekZirh * 0.005f);
                if (Hedef.GetComponent<Player>().oyuncuKalkanDurum && Hedef.GetComponent<Player>().oyuncuKalkan > 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET Kalkan = Kalkan - 1 WHERE Kullanici_Adi=@kullanici_adi and Kalkan > 0;";
                        command.Parameters.AddWithValue("@kullanici_adi", Hedef.GetComponent<Player>().oyuncuadi);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            Hedef.GetComponent<Player>().SetOyuncuKalkan(Hedef.GetComponent<Player>().oyuncuKalkan - 1);
                            vurulanHasar *= (0.9f - (Hedef.GetComponent<Player>().oyuncuYetenekKalkani * 0.01f));
                        }
                    }
                }
                else if (Hedef.GetComponent<Player>().oyuncuKalkanDurum && Hedef.GetComponent<Player>().oyuncuKalkan == 0)
                {
                    Hedef.GetComponent<Player>().SetOyuncuKalkanDurum(false);
                }
                sonsaldirilanzaman = Time.time;
                RpcGulleYarat(Hedef, (int)vurulanHasar, Hedef.GetComponent<Player>().oyuncuKalkanDurum);
                GulleHasarVer(Hedef, (int)vurulanHasar);
            }
        }
    }

    public void GulleHasarVer(GameObject Hedef, int hasar)
    {
        Hedef.GetComponent<Player>().Can -= hasar;
        Hedef.GetComponent<Player>().CanKontrol(Hedef.GetComponent<Player>().Can, Hedef.GetComponent<Player>().Can);
        Hedef.GetComponent<Player>().SetOyuncuTamirDurum(false);
        Hedef.GetComponent<Player>().sonsaldiranoyuncu = gameObject;
        Hedef.GetComponent<Player>().sonHasarAlinanZaman = Time.time;
    }
#endif
    [ClientRpc]
    void RpcGulleYarat(GameObject Hedef, int hasar, bool KalkanAktiflikDurumu)
    {
        StartCoroutine(GulleSureliYarat(Hedef, 0, hasar, KalkanAktiflikDurumu));
    }

    IEnumerator GulleSureliYarat(GameObject Hedef, int gulleId, int hasar, bool KalkanAktiflikDurumu)
    {
        if (GameManager.gm.BenimGemim == Hedef)
        {
            Hedef.GetComponent<Player>().sonsaldiranoyuncu = gameObject;
            GameManager.gm.sonBizeSaldirilanZaman = Time.time;
        }
        GameObject gullegameobject = Instantiate(gulleler[gulleId]);
        gullegameobject.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
        gullegameobject.GetComponent<GulleHareket>().hedef = Hedef;
        gullegameobject.GetComponent<GulleHareket>().hasar = hasar;
        gullegameobject.GetComponent<GulleHareket>().kalkanAktifDurum = KalkanAktiflikDurumu;
        gullegameobject.transform.position = transform.position;

        yield return new WaitForSeconds(0.03f);

        GameObject gullegameobject2 = Instantiate(gulleler[gulleId]);
        gullegameobject2.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
        gullegameobject2.GetComponent<GulleHareket>().hedef = Hedef;
        gullegameobject2.transform.position = transform.position;

        yield return new WaitForSeconds(0.03f);


        GameObject gullegameobject3 = Instantiate(gulleler[gulleId]);
        gullegameobject3.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
        gullegameobject3.GetComponent<GulleHareket>().hedef = Hedef;
        gullegameobject3.transform.position = transform.position;

        yield return new WaitForSeconds(0.03f);

        GameObject gullegameobject4 = Instantiate(gulleler[gulleId]);
        gullegameobject4.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
        gullegameobject4.GetComponent<GulleHareket>().hedef = Hedef;
        gullegameobject4.transform.position = transform.position;

        yield return new WaitForSeconds(0.03f);

        GameObject gullegameobject5 = Instantiate(gulleler[gulleId]);
        gullegameobject5.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
        gullegameobject5.GetComponent<GulleHareket>().hedef = Hedef;
        gullegameobject5.transform.position = transform.position;
    }

#if UNITY_SERVER || UNITY_EDITOR
    public void OdulVer()
    {
        for (int i = 0; i < 100; i++)
        {
            if (i == 0)
            {
                StartCoroutine(GemiOldur());
            }
            if (odulAlacakKisiler[i] != null)
            {
                odulAlacakKisiler[i].GetComponent<Player>().SureliPaketKontrol();

                OdulTp = (int)(hasarVerenOyuncularinHasarlari[i] / 200);
                OdulAltin = (int)((hasarVerenOyuncularinHasarlari[i] / 200) * OdulAltinOran);
                OdulTp = (int)(OdulTp + (odulAlacakKisiler[i].GetComponent<Player>().OyuncuPremiumDurumu * 0.05f * OdulTp) + (odulAlacakKisiler[i].GetComponent<Player>().OyuncuTpKatlamaDurumu * 0.3f * OdulTp) + (odulAlacakKisiler[i].GetComponent<Player>().adaBonuslari * OdulTp));
                OdulAltin = (int)(OdulAltin + (odulAlacakKisiler[i].GetComponent<Player>().OyuncuPremiumDurumu * 0.05f * OdulAltin) + (odulAlacakKisiler[i].GetComponent<Player>().OyuncuAltinKatlamaDurumu * 0.3f * OdulAltin) + (odulAlacakKisiler[i].GetComponent<Player>().adaBonuslari * OdulAltin));
                odulElitPuan = (int)((hasarVerenOyuncularinHasarlari[i] / 200) * odulElitPuanOrani);

                if (odulAlacakKisiler[i].GetComponent<Player>().oyuncuFiloId >= 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET TecrubePuani = TecrubePuani + '" + OdulTp + "',Altin=Altin + '" + OdulAltin + "',ElitPuan = ElitPuan + '" + odulElitPuan + "' WHERE ID=@ID;Update Filolar SET FiloTp = FiloTp + " + (int)(OdulTp * (0.1f)) + " where FiloId=" + odulAlacakKisiler[i].GetComponent<Player>().oyuncuFiloId;
                        command.Parameters.AddWithValue("@ID", odulAlacakKisiler[i].GetComponent<Player>().oyuncuId);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET TecrubePuani = TecrubePuani + '" + OdulTp + "',Altin=Altin + '" + OdulAltin + "',ElitPuan = ElitPuan + '" + odulElitPuan + "' WHERE ID=@ID;";
                        command.Parameters.AddWithValue("@ID", odulAlacakKisiler[i].GetComponent<Player>().oyuncuId);
                        command.ExecuteNonQuery();
                    }
                }
                NetworkIdentity opponentIdentity = odulAlacakKisiler[i].GetComponent<NetworkIdentity>();
                TargetOdulDonusu(opponentIdentity.connectionToClient, OdulTp, OdulAltin,odulElitPuan);

                odulAlacakKisiler[i].GetComponent<Player>().SetOyuncuTerubePuani(odulAlacakKisiler[i].GetComponent<Player>().oyuncuTecrubePuan + OdulTp);
                odulAlacakKisiler[i].GetComponent<Player>().SetOyuncuAltin(odulAlacakKisiler[i].GetComponent<Player>().oyuncuAltin + OdulAltin);
                odulAlacakKisiler[i].GetComponent<Player>().SetElitPuani(odulAlacakKisiler[i].GetComponent<Player>().oyuncuElitPuan + odulElitPuan);

            }
        }
    }
#endif

    [TargetRpc]
    public void TargetOdulDonusu(NetworkConnection target, int OdulTp, int OdulAltin,int donenElitPuan)
    {
        if (donenElitPuan > 0)
        {
            if (GameManager.gm.OdulText.text == "")
            {
                GameManager.gm.OdulText.GetComponent<Text>().text = geminame + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247] + OdulTp + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 253] + OdulAltin + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 250] + donenElitPuan + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 568];
                GameManager.gm.OdulYazisiSayacBaslat();
            }
            else if (GameManager.gm.OdulText.GetComponent<Text>().text != null)
            {
                GameManager.gm.OdulText2.GetComponent<Text>().text = geminame + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247] + OdulTp + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 253] + OdulAltin + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 250] + donenElitPuan + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 568];
                GameManager.gm.OdulYazisiSayacBaslat();
                return;
            }
            GameManager.gm.AddItemsSeyirDefteri(geminame + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247] + OdulTp + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 253] + OdulAltin + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 250] + donenElitPuan + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 568]);

        }
        else
        {
            if (GameManager.gm.OdulText.text == "")
            {
                GameManager.gm.OdulText.GetComponent<Text>().text = geminame + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247] + OdulTp + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 253] + OdulAltin + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 250];
                GameManager.gm.OdulYazisiSayacBaslat();
            }
            else if (GameManager.gm.OdulText.GetComponent<Text>().text != null)
            {
                GameManager.gm.OdulText2.GetComponent<Text>().text = geminame + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247] + OdulTp + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 253] + OdulAltin + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 250];
                GameManager.gm.OdulYazisiSayacBaslat();
                return;
            }
            GameManager.gm.AddItemsSeyirDefteri(geminame + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247] + OdulTp + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 253] + OdulAltin + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 250]);
        }
      
    }

#if UNITY_SERVER || UNITY_EDITOR
    IEnumerator GemiOldur()
    {
        yield return new WaitForSeconds(3f);
        NetworkServer.Destroy(gameObject);
    }

    private void HedefiYolla()
    {
        sonKonumUretilenZaman = Time.time;
        target = GameManager.gm.NavMeshPos(HaritaKac);
        SyncGemiyiHareketEttir(target, target);
    }

    public void SetSonSaldiranOyuncu(GameObject sonSaldiranOyuncuVerisi, int hasarlar)
    {
        ItemDrop(sonsaldiranoyuncu);
        sonsaldiranoyuncu = sonSaldiranOyuncuVerisi;
        for (int i = 0; i < 100; i++)
        {
            if (odulAlacakKisiler[i] == null)
            {
                odulAlacakKisiler[i] = sonsaldiranoyuncu;
                hasarVerenOyuncularinHasarlari[i] += hasarlar;
                break;
            }
            else if (odulAlacakKisiler[i] == sonsaldiranoyuncu)
            {
                hasarVerenOyuncularinHasarlari[i] += hasarlar;
                break;
            }
        }
    }
#endif

    public void SyncGemiyiHareketEttir(Vector3 oldvalue, Vector3 newvalue)
    {
        if (!gameObject.GetComponent<NavMeshAgent>().enabled)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            gameObject.GetComponent<NavMeshAgent>().radius = 0.1f;
        }
        gameObject.GetComponent<NavMeshAgent>().destination = newvalue;
        gameObject.GetComponent<NavMeshAgent>().speed = hiz;
    }

    public void GemiAnimasyonu()
    {
        if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite == null)
        {
            transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[Random.Range(0, 31)];
        }
        if (GemiAnimasyonuSonGirilenId != -1 && GameManager.gm.BenimGemim != null && GameManager.gm.BenimGemim.GetComponent<Player>().OyunaGirisYapildimi)
        {
            // sağ
            if (GemiAnimasyonuSonGirilenId == 8 && myAgent.desiredVelocity.x > hiz * 0.9f && myAgent.desiredVelocity.y <= hiz * 0.15f && myAgent.desiredVelocity.y >= -hiz * 0.15f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[8])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[8];
                }
                GemiAnimasyonuSonGirilenId = 8;
            }
            // alt
            else if (GemiAnimasyonuSonGirilenId == 16 && myAgent.desiredVelocity.y < -hiz * 0.9f && myAgent.desiredVelocity.x <= hiz * 0.15f && myAgent.desiredVelocity.x >= -hiz * 0.15f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[16])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[16];
                }
                GemiAnimasyonuSonGirilenId = 16;
            }
            //sol
            else if (GemiAnimasyonuSonGirilenId == 24 && myAgent.desiredVelocity.x < -hiz * 0.9f && myAgent.desiredVelocity.y <= hiz * 0.15f && myAgent.desiredVelocity.y >= -hiz * 0.15f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[24])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[24];
                }
                GemiAnimasyonuSonGirilenId = 24;
            }
            // üst
            if (GemiAnimasyonuSonGirilenId == 0 && myAgent.desiredVelocity.y > hiz * 0.9f && myAgent.desiredVelocity.x <= hiz * 0.15f && myAgent.desiredVelocity.x >= -hiz * 0.15f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[0])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[0];
                }
                GemiAnimasyonuSonGirilenId = 0;
            }
            // sağ üst 85
            else if (GemiAnimasyonuSonGirilenId == 1 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.85f && myAgent.desiredVelocity.y < hiz * 1f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[1])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[1];
                }
                GemiAnimasyonuSonGirilenId = 1;
            }
            // sağ üst 70
            else if (GemiAnimasyonuSonGirilenId == 2 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.70f && myAgent.desiredVelocity.y < hiz * 0.90f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[2])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[2];
                }
                GemiAnimasyonuSonGirilenId = 2;
            }
            // sağ üst 55
            else if (GemiAnimasyonuSonGirilenId == 3 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.55f && myAgent.desiredVelocity.y < hiz * 0.75f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[3])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[3];
                }
                GemiAnimasyonuSonGirilenId = 3;
            }
            // sağ üst 47
            else if (GemiAnimasyonuSonGirilenId == 4 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.47f && myAgent.desiredVelocity.y < hiz * 0.60f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[4])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[4];
                }
                GemiAnimasyonuSonGirilenId = 4;
            }
            // sağ üst 40
            else if (GemiAnimasyonuSonGirilenId == 5 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.40f && myAgent.desiredVelocity.y < hiz * 0.52f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[5])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[5];
                }
                GemiAnimasyonuSonGirilenId = 5;
            }
            // sağ üst 25
            else if (GemiAnimasyonuSonGirilenId == 6 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.25f && myAgent.desiredVelocity.y < hiz * 0.45f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[6])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[6];
                }
                GemiAnimasyonuSonGirilenId = 6;
            }
            // sağ üst 10
            else if (GemiAnimasyonuSonGirilenId == 7 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.10f && myAgent.desiredVelocity.y < hiz * 0.30f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[7])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[7];
                }
                GemiAnimasyonuSonGirilenId = 7;
            }
            // sağ alt 85
            else if (GemiAnimasyonuSonGirilenId == 15 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 1f && myAgent.desiredVelocity.y < -hiz * 0.85f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[15])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[15];
                }
                GemiAnimasyonuSonGirilenId = 15;
            }
            // sağ alt 70
            else if (GemiAnimasyonuSonGirilenId == 14 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.90f && myAgent.desiredVelocity.y < -hiz * 0.70f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[14])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[14];
                }
                GemiAnimasyonuSonGirilenId = 14;
            }
            // sağ alt 55
            else if (GemiAnimasyonuSonGirilenId == 12 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.75f && myAgent.desiredVelocity.y < -hiz * 0.55f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[12])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[12];
                }
                GemiAnimasyonuSonGirilenId = 12;
            }
            // sağ alt 47
            else if (GemiAnimasyonuSonGirilenId == 13 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.60 && myAgent.desiredVelocity.y < -hiz * 0.47f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[13])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[13];
                }
                GemiAnimasyonuSonGirilenId = 13;
            }
            // sağ alt 40
            else if (GemiAnimasyonuSonGirilenId == 11 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.52f && myAgent.desiredVelocity.y < -hiz * 0.40f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[11])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[11];
                }
                GemiAnimasyonuSonGirilenId = 11;
            }
            // sağ alt 25
            else if (GemiAnimasyonuSonGirilenId == 10 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.45f && myAgent.desiredVelocity.y < -hiz * 0.25f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[10])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[10];
                }
                GemiAnimasyonuSonGirilenId = 10;
            }
            // sağ alt 10
            else if (GemiAnimasyonuSonGirilenId == 9 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.30f && myAgent.desiredVelocity.y < -hiz * 0.10f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[9])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[9];
                }
                GemiAnimasyonuSonGirilenId = 9;
            }
            // sol alt 85
            else if (GemiAnimasyonuSonGirilenId == 17 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 1f && myAgent.desiredVelocity.y < -hiz * 0.85f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[17])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[17];
                }
                GemiAnimasyonuSonGirilenId = 17;
            }
            // sol alt 70
            else if (GemiAnimasyonuSonGirilenId == 18 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.90f && myAgent.desiredVelocity.y < -hiz * 0.70f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[18])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[18];
                }
                GemiAnimasyonuSonGirilenId = 18;
            }
            // sol alt 55
            else if (GemiAnimasyonuSonGirilenId == 19 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.75f && myAgent.desiredVelocity.y < -hiz * 0.55f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[19])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[19];
                }
                GemiAnimasyonuSonGirilenId = 19;
            }
            // sol alt 47
            else if (GemiAnimasyonuSonGirilenId == 20 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.60f && myAgent.desiredVelocity.y < -hiz * 0.47f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[20])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[20];
                }
                GemiAnimasyonuSonGirilenId = 20;
            }
            // sol alt 40
            else if (GemiAnimasyonuSonGirilenId == 21 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.52f && myAgent.desiredVelocity.y < -hiz * 0.40f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[21])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[21];
                }
                GemiAnimasyonuSonGirilenId = 21;
            }
            // sol alt 25
            else if (GemiAnimasyonuSonGirilenId == 22 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.45f && myAgent.desiredVelocity.y < -hiz * 0.25f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[22])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[22];
                }
                GemiAnimasyonuSonGirilenId = 22;
            }
            // sol alt 10
            else if (GemiAnimasyonuSonGirilenId == 23 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.30f && myAgent.desiredVelocity.y < -hiz * 0.10f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[23])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[23];
                }
                GemiAnimasyonuSonGirilenId = 23;
            }
            // sol üst 10
            else if (GemiAnimasyonuSonGirilenId == 25 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.10f && myAgent.desiredVelocity.y < hiz * 0.30f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[25])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[25];
                }

                GemiAnimasyonuSonGirilenId = 25;
            }
            // sol üst 25
            else if (GemiAnimasyonuSonGirilenId == 26 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.25f && myAgent.desiredVelocity.y < hiz * 0.45f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[26])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[26];
                }
                GemiAnimasyonuSonGirilenId = 26;
            }
            // sol üst 40
            else if (GemiAnimasyonuSonGirilenId == 27 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.40f && myAgent.desiredVelocity.y < hiz * 0.52f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[27])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[27];
                }
                GemiAnimasyonuSonGirilenId = 27;
            }
            // sol üst 47
            else if (GemiAnimasyonuSonGirilenId == 28 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.47f && myAgent.desiredVelocity.y < hiz * 0.60f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[28])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[28];
                }
                GemiAnimasyonuSonGirilenId = 28;
            }
            // sol üst 55
            else if (GemiAnimasyonuSonGirilenId == 29 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.55f && myAgent.desiredVelocity.y < hiz * 0.75f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[29])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[29];
                }
                GemiAnimasyonuSonGirilenId = 29;
            }
            // sol üst 70
            else if (GemiAnimasyonuSonGirilenId == 30 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.70f && myAgent.desiredVelocity.y < hiz * 0.90f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[30])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[30];
                }
                GemiAnimasyonuSonGirilenId = 30;
            }
            // sol üst 85
            else if (GemiAnimasyonuSonGirilenId == 31 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.85f && myAgent.desiredVelocity.y < hiz * 1f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[31])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[31];
                }
                GemiAnimasyonuSonGirilenId = 31;
            }
            else
            {
                GemiAnimasyonuSonGirilenId = -1;
            }
        }
        if (GemiAnimasyonuSonGirilenId == -1)
        {
            // navmeshi kullanarak animatordeki spriteları değiştirir ve gemi hareket animasyonu oluşturur
            if (GameManager.gm.BenimGemim != null && GameManager.gm.BenimGemim.GetComponent<Player>().OyunaGirisYapildimi)
            {
                // sağ
                if (myAgent.desiredVelocity.x > hiz * 0.9f && myAgent.desiredVelocity.y <= hiz * 0.15f && myAgent.desiredVelocity.y >= -hiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[8])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[8];
                    }
                    GemiAnimasyonuSonGirilenId = 8;
                }
                // alt
                else if (myAgent.desiredVelocity.y < -hiz * 0.9f && myAgent.desiredVelocity.x <= hiz * 0.15f && myAgent.desiredVelocity.x >= -hiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[16])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[16];
                    }
                    GemiAnimasyonuSonGirilenId = 16;
                }
                //sol
                else if (myAgent.desiredVelocity.x < -hiz * 0.9f && myAgent.desiredVelocity.y <= hiz * 0.15f && myAgent.desiredVelocity.y >= -hiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[24])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[24];
                    }
                    GemiAnimasyonuSonGirilenId = 24;
                }
                // üst
                if (myAgent.desiredVelocity.y > hiz * 0.9f && myAgent.desiredVelocity.x <= hiz * 0.15f && myAgent.desiredVelocity.x >= -hiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[0])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[0];
                    }
                    GemiAnimasyonuSonGirilenId = 0;
                }
                // sağ üst 85
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.85f && myAgent.desiredVelocity.y < hiz * 1f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[1])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[1];
                    }
                    GemiAnimasyonuSonGirilenId = 1;
                }
                // sağ üst 70
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.70f && myAgent.desiredVelocity.y < hiz * 0.90f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[2])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[2];
                    }
                    GemiAnimasyonuSonGirilenId = 2;
                }
                // sağ üst 55
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.55f && myAgent.desiredVelocity.y < hiz * 0.75f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[3])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[3];
                    }
                    GemiAnimasyonuSonGirilenId = 3;
                }
                // sağ üst 47
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.47f && myAgent.desiredVelocity.y < hiz * 0.60f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[4])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[4];
                    }
                    GemiAnimasyonuSonGirilenId = 4;
                }
                // sağ üst 40
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.40f && myAgent.desiredVelocity.y < hiz * 0.52f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[5])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[5];
                    }
                    GemiAnimasyonuSonGirilenId = 5;
                }
                // sağ üst 25
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.25f && myAgent.desiredVelocity.y < hiz * 0.45f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[6])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[6];
                    }
                    GemiAnimasyonuSonGirilenId = 6;
                }
                // sağ üst 10
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.10f && myAgent.desiredVelocity.y < hiz * 0.30f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[7])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[7];
                    }
                    GemiAnimasyonuSonGirilenId = 7;
                }
                // sağ alt 85
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 1f && myAgent.desiredVelocity.y < -hiz * 0.85f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[15])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[15];
                    }
                    GemiAnimasyonuSonGirilenId = 15;
                }
                // sağ alt 70
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.90f && myAgent.desiredVelocity.y < -hiz * 0.70f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[14])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[14];
                    }
                    GemiAnimasyonuSonGirilenId = 14;
                }
                // sağ alt 55
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.75f && myAgent.desiredVelocity.y < -hiz * 0.55f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[12])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[12];
                    }
                    GemiAnimasyonuSonGirilenId = 12;
                }
                // sağ alt 47
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.60 && myAgent.desiredVelocity.y < -hiz * 0.47f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[13])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[13];
                    }
                    GemiAnimasyonuSonGirilenId = 13;
                }
                // sağ alt 40
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.52f && myAgent.desiredVelocity.y < -hiz * 0.40f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[11])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[11];
                    }
                    GemiAnimasyonuSonGirilenId = 11;
                }
                // sağ alt 25
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.45f && myAgent.desiredVelocity.y < -hiz * 0.25f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[10])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[10];
                    }
                    GemiAnimasyonuSonGirilenId = 10;
                }
                // sağ alt 10
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.30f && myAgent.desiredVelocity.y < -hiz * 0.10f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[9])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[9];
                    }
                    GemiAnimasyonuSonGirilenId = 9;
                }
                // sol alt 85
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 1f && myAgent.desiredVelocity.y < -hiz * 0.85f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[17])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[17];
                    }
                    GemiAnimasyonuSonGirilenId = 17;
                }
                // sol alt 70
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.90f && myAgent.desiredVelocity.y < -hiz * 0.70f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[18])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[18];
                    }
                    GemiAnimasyonuSonGirilenId = 18;
                }
                // sol alt 55
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.75f && myAgent.desiredVelocity.y < -hiz * 0.55f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[19])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[19];
                    }
                    GemiAnimasyonuSonGirilenId = 19;
                }
                // sol alt 47
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.60f && myAgent.desiredVelocity.y < -hiz * 0.47f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[20])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[20];
                    }
                    GemiAnimasyonuSonGirilenId = 20;
                }
                // sol alt 40
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.52f && myAgent.desiredVelocity.y < -hiz * 0.40f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[21])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[21];
                    }
                    GemiAnimasyonuSonGirilenId = 21;
                }
                // sol alt 25
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.45f && myAgent.desiredVelocity.y < -hiz * 0.25f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[22])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[22];
                    }
                    GemiAnimasyonuSonGirilenId = 22;
                }
                // sol alt 10
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.30f && myAgent.desiredVelocity.y < -hiz * 0.10f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[23])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[23];
                    }
                    GemiAnimasyonuSonGirilenId = 23;
                }
                // sol üst 10
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.10f && myAgent.desiredVelocity.y < hiz * 0.30f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[25])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[25];
                    }

                    GemiAnimasyonuSonGirilenId = 25;
                }
                // sol üst 25
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.25f && myAgent.desiredVelocity.y < hiz * 0.45f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[26])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[26];
                    }
                    GemiAnimasyonuSonGirilenId = 26;
                }
                // sol üst 40
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.40f && myAgent.desiredVelocity.y < hiz * 0.52f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[27])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[27];
                    }
                    GemiAnimasyonuSonGirilenId = 27;
                }
                // sol üst 47
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.47f && myAgent.desiredVelocity.y < hiz * 0.60f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[28])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[28];
                    }
                    GemiAnimasyonuSonGirilenId = 28;
                }
                // sol üst 55
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.55f && myAgent.desiredVelocity.y < hiz * 0.75f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[29])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[29];
                    }
                    GemiAnimasyonuSonGirilenId = 29;
                }
                // sol üst 70
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.70f && myAgent.desiredVelocity.y < hiz * 0.90f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[30])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[30];
                    }
                    GemiAnimasyonuSonGirilenId = 30;
                }
                // sol üst 85
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.85f && myAgent.desiredVelocity.y < hiz * 1f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[31])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[31];
                    }
                    GemiAnimasyonuSonGirilenId = 31;
                }
            }
        }
    }


#if UNITY_SERVER || UNITY_EDITOR
    public void ItemDrop(GameObject oyuncu)
    {
        int itemDusmeihtimali = Random.Range(0, 1001);
        if (HaritaKac == 1)
        {
            if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
            else if (itemDusmeihtimali <= 40)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
        }
        else if (HaritaKac == 2)
        {
            if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
            else if (itemDusmeihtimali <= 40)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
        }
        else if (HaritaKac == 3)
        {
            if (itemDusmeihtimali <= 40)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
            else if (itemDusmeihtimali <= 60)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
        }
        else if (HaritaKac == 4)
        {
            if (itemDusmeihtimali <= 20)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
            else if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
        }
        else if (HaritaKac == 5)
        {
            if (itemDusmeihtimali <= 20)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
            else if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
        }
        else if (HaritaKac == 6)
        {
            if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
            else if (itemDusmeihtimali <= 50)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
        }
        else if (HaritaKac == 7)
        {
            if (itemDusmeihtimali <= 20)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
            else if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
        }
        else if (HaritaKac == 8)
        {
            if (itemDusmeihtimali <= 20)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
            else if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
        }
        else if (HaritaKac == 9)
        {
            if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
            else if (itemDusmeihtimali <= 50)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzBesKilolukTopDepo + 1);
                        NetworkIdentity opponentIdentity = oyuncu.GetComponent<NetworkIdentity>();
                        TargetItemDropDonusu(opponentIdentity.connectionToClient);
                    }
                }
            }
        }
    }
#endif

    [TargetRpc]
    public void TargetItemDropDonusu(NetworkConnection target)
    {
            if (GameManager.gm.OdulText.text == "")
            {
                GameManager.gm.OdulText.GetComponent<Text>().text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 562];
                GameManager.gm.OdulYazisiSayacBaslat();
            }
            else if (GameManager.gm.OdulText.GetComponent<Text>().text != null)
            {
                GameManager.gm.OdulText2.GetComponent<Text>().text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 562];
                GameManager.gm.OdulYazisiSayacBaslat();
                return;
            }
            GameManager.gm.AddItemsSeyirDefteri(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 562]);
    }
}
