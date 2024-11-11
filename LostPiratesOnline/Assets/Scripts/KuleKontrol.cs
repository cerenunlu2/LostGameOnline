using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Text;

public class KuleKontrol : NetworkBehaviour
{
    [SyncVar] public string geminame;
    [SyncVar] public string filoKisaltmasi;
    [SyncVar] public int HaritaKac = 1;
    [SyncVar] public int Can = 1;
    [SyncVar] public int MaxCan = 1;
    private bool kuleOldumu = false;
    [SyncVar] public int hangiKule = 0;
    public bool kuleTamirDurumu = false;
    public GameObject sonsaldiranoyuncu;
    private float sonsaldirilanzaman = 0;
    public float saldirihizi = 6f, hasar;

    public float sonHasarAlinanZaman = 0;
    public GameObject[] gulleler;
    private SpriteRenderer spriteRenderer;


    private float currentAlpha = 0f;
    private float fadeSpeed;
    public float fadeDuration = 2f; // Yavaþça geçiþ süresi (saniye)
    // Start is called before the first frame update
    public GameObject kuleMenzil;
    public float sonTamirEdilenZaman = 0;
    void Start()
    {
        if (isClient)
        {
            fadeSpeed = 255f / fadeDuration; // Transparanlýk deðiþim hýzý
            transform.eulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.SetParent(GameManager.gm.HaritadakiFiloAdalari[HaritaKac - 5].transform.Find("Kule" + hangiKule).transform);
            transform.localPosition = Vector3.zero;
            transform.Find("OyuncuAdi").GetComponent<TextMeshPro>().text = "<color=#FFE100>[" + filoKisaltmasi + "]" + geminame + "</color>";

        }
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            kuleMenzil.SetActive(true);
        }
#endif

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            if (Can <= 0 && sonsaldiranoyuncu != null && kuleOldumu == false)
            {
                GameManager.gm.BenSaldiriyorum.SetActive(false);
                GameManager.gm.SaldiriIptalButton.SetActive(false);
                GameManager.gm.SaldirButton.SetActive(true);
                kuleOldumu = true;
                OdulVer();
            }
            // kule tamir edilirken hasar aldiysa tamir edilmesi dursun
            if (isServer && kuleTamirDurumu && Time.time - sonHasarAlinanZaman <= 3)
            {
                kuleTamirDurumu = false;
            }
            // tamir
            else if (isServer && Can < MaxCan && kuleTamirDurumu && Time.time - sonTamirEdilenZaman >= 4f)
            {
                sonTamirEdilenZaman = Time.time;
                if (Can + 1000 > MaxCan)
                {
                    Can = MaxCan;
                    kuleTamirDurumu = false;
                }
                else
                {
                    Can = Can + 1000;
                }
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
                    spriteRenderer = transform.Find("Gemi").GetComponent<SpriteRenderer>();
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
                spriteRenderer = transform.Find("Gemi").GetComponent<SpriteRenderer>();
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
            transform.Find("OyuncuCanvas/Can").GetComponent<Slider>().maxValue = MaxCan;
            transform.Find("OyuncuCanvas/Can").GetComponent<Slider>().value = Can;
        }
    }

#if UNITY_SERVER || UNITY_EDITOR
    public void gulleYarat()
    {
        if (isServer && sonsaldiranoyuncu != null && Time.time - sonsaldirilanzaman >= saldirihizi && Can > 0)
        {
            float vurulanHasar = Random.Range(hasar * 0.8f, hasar);
            vurulanHasar -= (vurulanHasar * sonsaldiranoyuncu.GetComponent<Player>().OyuncuElitPuanZirhBonusu) + (vurulanHasar * sonsaldiranoyuncu.GetComponent<Player>().oyuncuYetenekZirh * 0.005f);
            if (sonsaldiranoyuncu.GetComponent<Player>().oyuncuKalkanDurum && sonsaldiranoyuncu.GetComponent<Player>().oyuncuKalkan > 0)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Kalkan = Kalkan - 1 WHERE Kullanici_Adi=@kullanici_adi and Kalkan > 0;";
                    command.Parameters.AddWithValue("@kullanici_adi", sonsaldiranoyuncu.GetComponent<Player>().oyuncuadi);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        sonsaldiranoyuncu.GetComponent<Player>().SetOyuncuKalkan(sonsaldiranoyuncu.GetComponent<Player>().oyuncuKalkan - 1);
                        vurulanHasar *= (0.9f - (sonsaldiranoyuncu.GetComponent<Player>().oyuncuYetenekKalkani * 0.01f));
                    }
                }
            }
            else if (sonsaldiranoyuncu.GetComponent<Player>().oyuncuKalkanDurum && sonsaldiranoyuncu.GetComponent<Player>().oyuncuKalkan == 0)
            {
                sonsaldiranoyuncu.GetComponent<Player>().SetOyuncuKalkanDurum(false);
            }
            sonsaldirilanzaman = Time.time;
            RpcGulleYarat(sonsaldiranoyuncu, (int)vurulanHasar, sonsaldiranoyuncu.GetComponent<Player>().oyuncuKalkanDurum);
            GulleHasarVer(sonsaldiranoyuncu, (int)vurulanHasar);
        }
    }

    public void GulleHasarVer(GameObject Hedef, int hasar)
    {
        Hedef.GetComponent<Player>().Can -= hasar;
        Hedef.GetComponent<Player>().SetOyuncuTamirDurum(false);
        Hedef.GetComponent<Player>().sonsaldiranoyuncu = gameObject;
        Hedef.GetComponent<Player>().sonHasarAlinanZaman = Time.time;
    }
#endif
    [ClientRpc]
    void RpcGulleYarat(GameObject Hedef, int hasar, bool KalkanAktiflikDurumu)
    {
        GulleSureliYarat(Hedef, 0, hasar, KalkanAktiflikDurumu);
    }

    public void GulleSureliYarat(GameObject Hedef, int gulleId, int hasar, bool KalkanAktiflikDurumu)
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
        gullegameobject.GetComponent<GulleHareket>().hedefledenOrtalamaYukseklik = 0.3f;
        gullegameobject.GetComponent<GulleHareket>().kalkanAktifDurum = KalkanAktiflikDurumu;
        gullegameobject.transform.position = transform.position;
    }

#if UNITY_SERVER || UNITY_EDITOR
    public void OdulVer()
    {
        StartCoroutine(KuleOldur());
    }
#endif

#if UNITY_SERVER || UNITY_EDITOR
    IEnumerator KuleOldur()
    {
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Adalar SET Kule" + hangiKule + " = 0 WHERE AdaId = @harita;";
            command.Parameters.AddWithValue("@harita", HaritaKac);
            command.ExecuteNonQuery();
        }
        GameManager.gm.HaritadakiFiloAdalari[HaritaKac - 5].transform.Find("AdaOfisiCarpismaNoktasi").GetComponent<FiloAdasiOfisi>().AdaElDegistirmeKontrol();
        yield return new WaitForSeconds(3f);
        NetworkServer.Destroy(gameObject);
    }

    public void SetSonSaldiranOyuncu(GameObject sonSaldiranOyuncuVerisi, int hasar)
    {
        sonsaldiranoyuncu = sonSaldiranOyuncuVerisi;
        if (hasar > 0 && sonSaldiranOyuncuVerisi.GetComponent<Player>().oyuncuFiloId > 0)
        {
            GameManager.gm.HaritadakiFiloAdalari[HaritaKac - 5].transform.Find("AdaOfisiCarpismaNoktasi").GetComponent<FiloAdasiOfisi>().AdayaVerilenHasariHesapla(sonSaldiranOyuncuVerisi.GetComponent<Player>().oyuncuFiloId, hasar);
        }
        if (Can < (MaxCan / 2))
        {
            GameManager.gm.HaritadakiFiloAdalari[HaritaKac - 5].transform.Find("AdaOfisiCarpismaNoktasi").GetComponent<FiloAdasiOfisi>().AdaSaldiriAltindaBildirimiKontrol();
        }
    }
#endif
}
