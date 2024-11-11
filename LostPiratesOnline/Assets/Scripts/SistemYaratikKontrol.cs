using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Text;


public class SistemYaratikKontrol : NetworkBehaviour
{
    [SyncVar] public string geminame;
    public int HaritaKac = 1;
    [SyncVar] public int Can;
    [SyncVar] bool olmeDurumu = false;
    private bool gemiOldumu = false;
    public GameObject sonsaldiranoyuncu;
    public int OdulTp, OdulAltin, odulElitPuan;
    public int MaksCan;
    private SpriteRenderer spriteRenderer;
    public float sonHasarAlinanZaman = 0;

    private float currentAlpha = 0f;
    private float fadeSpeed;
    public float fadeDuration = 2f; // Yavaþça geçiþ süresi (saniye)
    public GameObject IlkSaldiranOyuncu;

    // Start is called before the first frame update
    void Start()
    {
        if (isClient)
        {
            transform.Find("OyuncuAdi").GetComponent<TextMeshPro>().text = geminame;
            GetComponent<NavMeshAgent>().updateRotation = false;
            GetComponent<NavMeshAgent>().updateUpAxis = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
            spriteRenderer = transform.Find("Gemi").GetComponent<SpriteRenderer>();
            fadeSpeed = 255f / fadeDuration; // Transparanlýk deðiþim hýzý
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            if (Time.time - sonHasarAlinanZaman > 20)
            {
                IlkSaldiranOyuncu = null;
            }
            if (Can <= 0 && sonsaldiranoyuncu != null && gemiOldumu == false)
            {
                gemiOldumu = true;
                if ((HaritaKac == 1 || HaritaKac == 2) && IlkSaldiranOyuncu != null)
                {
                    OdulVer(IlkSaldiranOyuncu);
                }
                else
                {
                    OdulVer(sonsaldiranoyuncu);
                }

            }
        }
#endif
        if (isClient && gameObject != null)
        {
            if (olmeDurumu == true)
            {
                gameObject.SetActive(false);
            }
            if (Vector2.Distance(GameManager.gm.BenimGemim.transform.position, transform.position) < 11f)
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
            else if (Vector2.Distance(GameManager.gm.BenimGemim.transform.position, transform.position) >= 11f && currentAlpha > 0f)
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
            gameObject.transform.Find("OyuncuCanvas/Can").GetComponent<Slider>().value = Can;
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
                    command.CommandText = "Update Kullanici SET onBeslikKGTopDepo = onBeslikKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOnBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOnBesKilolukTopDepo + 1);
                    }
                }
            }
            else if (itemDusmeihtimali <= 40)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET yirmiBeslikKGTopDepo = yirmiBeslikKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiBesKilolukTopDepo + 1);
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
                    command.CommandText = "Update Kullanici SET onBeslikKGTopDepo = onBeslikKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOnBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOnBesKilolukTopDepo + 1);
                    }
                }
            }
            else if (itemDusmeihtimali <= 40)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET yirmiBeslikKGTopDepo = yirmiBeslikKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiBesKilolukTopDepo + 1);
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
                    command.CommandText = "Update Kullanici SET onBeslikKGTopDepo = onBeslikKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOnBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOnBesKilolukTopDepo + 1);
                        Debug.Log("15KG Top düþürdün");
                    }
                }
            }
            else if (itemDusmeihtimali <= 60)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET yirmiBeslikKGTopDepo = yirmiBeslikKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiBesKilolukTopDepo + 1);
                        Debug.Log("25KG Top düþürdün");
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
                    command.CommandText = "Update Kullanici SET yirmiBeslikKGTopDepo = yirmiBeslikKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiBesKilolukTopDepo + 1);
                    }
                }
            }
            else if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET yirmiYediBucukKGTopDepo = yirmiYediBucukKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiYediBucukKilolukTopDepo + 1);
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
                    command.CommandText = "Update Kullanici SET yirmiBeslikKGTopDepo = yirmiBeslikKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiBesKilolukTopDepo + 1);
                    }
                }
            }
            else if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET yirmiYediBucukKGTopDepo = yirmiYediBucukKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiYediBucukKilolukTopDepo + 1);
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
                    command.CommandText = "Update Kullanici SET yirmiBeslikKGTopDepo = yirmiBeslikKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiBesKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiBesKilolukTopDepo + 1);
                    }
                }
            }
            else if (itemDusmeihtimali <= 50)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET yirmiYediBucukKGTopDepo = yirmiYediBucukKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiYediBucukKilolukTopDepo + 1);
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
                    command.CommandText = "Update Kullanici SET yirmiYediBucukKGTopDepo = yirmiYediBucukKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiYediBucukKilolukTopDepo + 1);
                    }
                }
            }
            else if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzKilolukTopDepo = otuzKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzKilolukTopDepo + 1);
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
                    command.CommandText = "Update Kullanici SET yirmiYediBucukKGTopDepo = yirmiYediBucukKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiYediBucukKilolukTopDepo + 1);
                    }
                }
            }
            else if (itemDusmeihtimali <= 30)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzKilolukTopDepo = otuzKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzKilolukTopDepo + 1);
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
                    command.CommandText = "Update Kullanici SET yirmiYediBucukKGTopDepo = yirmiYediBucukKGTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuYirmiYediBucukKilolukTopDepo + 1);
                    }
                }
            }
            else if (itemDusmeihtimali <= 50)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET otuzKilolukTopDepo = otuzKilolukTopDepo + 1 WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@ID", oyuncu.GetComponent<Player>().oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        oyuncu.GetComponent<Player>().SetOyuncuOtuzKilolukTopDepo(oyuncu.GetComponent<Player>().oyuncuOtuzKilolukTopDepo + 1);
                    }
                }
            }
        }
    }

#endif

    public void OdulVer(GameObject hedef)
    {
#if UNITY_SERVER || UNITY_EDITOR
        hedef.GetComponent<Player>().SureliPaketKontrol();
        ItemDrop(hedef);
        OdulTp = (int)(OdulTp + (hedef.GetComponent<Player>().OyuncuPremiumDurumu * 1) + (hedef.GetComponent<Player>().OyuncuPremiumDurumu * 0.05f * OdulTp) + (hedef.GetComponent<Player>().OyuncuTpKatlamaDurumu * 0.3f * OdulTp));
        OdulAltin = (int)(OdulAltin + (hedef.GetComponent<Player>().OyuncuPremiumDurumu * 1) + (hedef.GetComponent<Player>().OyuncuPremiumDurumu * 0.05f * OdulAltin) + (hedef.GetComponent<Player>().OyuncuAltinKatlamaDurumu * 0.3f * OdulAltin));
        // odul elit puansa
        if (odulElitPuan > 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET ElitPuan = ElitPuan + '" + odulElitPuan + "' WHERE Kullanici_Adi=@kullanici_adi;";
                command.Parameters.AddWithValue("@kullanici_adi", hedef.name);
                command.ExecuteNonQuery();
            }
        }
        else
        {
            if (hedef.GetComponent<Player>().oyuncuFiloId >= 0)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET TecrubePuani = TecrubePuani + '" + OdulTp + "',Altin=Altin + '" + OdulAltin + "' WHERE Kullanici_Adi=@kullanici_adi;Update Filolar SET FiloTp = FiloTp + " + (int)(OdulTp * (0.1f)) + " where FiloId=" + hedef.GetComponent<Player>().oyuncuFiloId;
                    command.Parameters.AddWithValue("@kullanici_adi", hedef.name);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET TecrubePuani = TecrubePuani + '" + OdulTp + "',Altin=Altin + '" + OdulAltin + "' WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", hedef.name);
                    command.ExecuteNonQuery();
                }
            }

        }

        if (HaritaKac == 2 && hedef.GetComponent<Player>().oyuncuTecrubePuan > 600)
        {
            GameManager.gm.BenimGemim.GetComponent<Player>().SetOyuncuHaritaIkiBesNpcCanavarKesGorevi(GameManager.gm.BenimGemim.GetComponent<Player>().haritaIkýBesNpcCanavarKesGoreviSayac + 1);
            GameManager.gm.BenimGemim.GetComponent<Player>().GorevKontrolu();
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET haritaIkýBesNpcCanavarKesGoreviSayac = HaritaBirOnNpcKesmeGoreviSayac + 1 WHERE ID=@ID;";
                command.Parameters.AddWithValue("@ID", hedef.GetComponent<Player>().oyuncuId);
                command.ExecuteNonQuery();
            }
        }
        else if (HaritaKac == 3 && hedef.GetComponent<Player>().oyuncuTecrubePuan > 900)
        {
            GameManager.gm.BenimGemim.GetComponent<Player>().SetOyuncuHaritaUcSekizNpcCanavarKesGorevi(GameManager.gm.BenimGemim.GetComponent<Player>().haritaUcSekizNpcCanavarKesGoreviSayac + 1);
            GameManager.gm.BenimGemim.GetComponent<Player>().GorevKontrolu();
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET haritaUcSekizNpcCanavarKesGoreviSayac = HaritaBirOnNpcKesmeGoreviSayac + 1 WHERE ID=@ID;";
                command.Parameters.AddWithValue("@ID", hedef.GetComponent<Player>().oyuncuId);
                command.ExecuteNonQuery();
            }
        }
        else if (HaritaKac == 4 && hedef.GetComponent<Player>().oyuncuTecrubePuan > 1200)
        {
            GameManager.gm.BenimGemim.GetComponent<Player>().SetOyuncuHaritaDortOnikiNpcCanavarKesGorevi(GameManager.gm.BenimGemim.GetComponent<Player>().haritaDortOnIkiNpcCanavarKesGoreviSayac + 1);
            GameManager.gm.BenimGemim.GetComponent<Player>().GorevKontrolu();
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET haritaDortOnIkiNpcCanavarKesGoreviSayac = HaritaBirOnNpcKesmeGoreviSayac + 1 WHERE ID=@ID;";
                command.Parameters.AddWithValue("@ID", hedef.GetComponent<Player>().oyuncuId);
                command.ExecuteNonQuery();
            }
        }
        else if (HaritaKac == 5 && hedef.GetComponent<Player>().oyuncuTecrubePuan > 4000)
        {
            GameManager.gm.BenimGemim.GetComponent<Player>().SetOyuncuHaritaBesOnSekizNpcCanavarKesGorevi(GameManager.gm.BenimGemim.GetComponent<Player>().haritaBesOnSekizNpcCanavarKesGoreviSayac + 1);
            GameManager.gm.BenimGemim.GetComponent<Player>().GorevKontrolu();
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET haritaBesOnSekizNpcCanavarKesGoreviSayac = haritaBesOnSekizNpcCanavarKesGoreviSayac + 1 WHERE ID=@ID;";
                command.Parameters.AddWithValue("@ID", hedef.GetComponent<Player>().oyuncuId);
                command.ExecuteNonQuery();
            }
        }
        NetworkIdentity opponentIdentity = hedef.GetComponent<NetworkIdentity>();
        TargetOdulDonusu(opponentIdentity.connectionToClient, OdulTp, OdulAltin,odulElitPuan);
        hedef.GetComponent<Player>().botKontrolKalanIslemSayisiGuncelle();

        StartCoroutine(GemiOldur());
        hedef.GetComponent<Player>().SetOyuncuTerubePuani(hedef.GetComponent<Player>().oyuncuTecrubePuan + OdulTp);
        hedef.GetComponent<Player>().SetOyuncuAltin(hedef.GetComponent<Player>().oyuncuAltin + OdulAltin);
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Kullanici SET BatirilanBot = BatirilanBot + 1 WHERE kullanici_adi=@kullaniciadi;";
            command.Parameters.AddWithValue("@kullaniciadi", hedef.GetComponent<Player>().oyuncuadi);
            command.ExecuteNonQuery();

        }
#endif
    }

    [TargetRpc]
    public void TargetOdulDonusu(NetworkConnection target, int OdulTp, int OdulAltin, int odulElitPuan)
    {
        GameManager.gm.SaldiriIptalButton.SetActive(false);
        GameManager.gm.SaldirButton.SetActive(true);
        if (odulElitPuan > 0)
        {
            GameManager.gm.OdulText.GetComponent<Text>().text = geminame + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247] + odulElitPuan + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 251];
            GameManager.gm.OdulYazisiSayacBaslat();
            GameManager.gm.AddItemsSeyirDefteri(geminame + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247] + odulElitPuan + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 251]);

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


    IEnumerator GemiOldur()
    {
        yield return new WaitForSeconds(3f);
        olmeDurumu = true;
        NpcSpawn();
        NetworkServer.Destroy(gameObject);
    }

    public void NpcSpawn()
    {
#if UNITY_SERVER || UNITY_EDITOR

       // GameManager.gm.EtkinlikDenizYaratigiDogurSunucu(geminame, HaritaKac);
        
         GameManager.gm.YaratikUret(geminame, HaritaKac);
#endif
    }
}

