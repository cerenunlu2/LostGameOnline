using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.AI;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using Unity.Burst.CompilerServices;
using Cinemachine;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Experimental.GlobalIllumination;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using MySql.Data.MySqlClient;

public class Player : NetworkBehaviour
{
    public NavMeshAgent myAgent;
    public GameObject obje;
    public bool saldiridurumu = false;
    public int seciligulleid = 0, seciliZipkinId = 0;
    public GameObject[] gulleler, zipkinlar;
    public GameObject roket;

    public int oyuncuOnBesKilolukTopDepo = 0, oyuncuYirmiBesKilolukTopDepo = 0, oyuncuYirmiYediBucukKilolukTopDepo = 0, oyuncuOtuzKilolukTopDepo = 0, oyuncuOtuzBesKilolukTopDepo = 0,oyuncuCifteVurusTopDepo = 0, oyuncuOnBesKilolukTopGemi = 0, oyuncuYirmiBesKilolukTopGemi = 0, oyuncuYirmiYediBucukKilolukTopGemi = 0, oyuncuOtuzKilolukTopGemi = 0, oyuncuOtuzBesKilolukTopGemi = 0,oyuncuCifteVurusTopGemi = 0;
    public string verificationcode = "", MailVerificationCode = "", NewMailVerificationCode = "";
    public int oyuncuKayik = 0;
    public int oyuncuTecrubePuan = 0, oyuncuElitPuan = 0, oyuncuSifaGulle = 0, oyuncuAlevGulle = 0, oyuncuDemirGulle = 0, oyuncuHavaiFisekGulle = 0, oyuncuKalkan = 0, oyuncuBarut = 0, oyuncuHizTasi = 0, oyuncuRoket = 0, oyuncuAltin = 0,
         oyuncuSavasPuani = 0, oyuncuLostCoin = 0, oyuncuPaslanmisZipkin = 0, oyuncuGumusZipkin = 0, oyuncuAltinZipkin = 0, oyuncuTamir = 0,OyuncuEtkinlikPuani = 0,OyuncuHallowenGulle = 0,OyuncuNoelGullesi = 0, OyuncuHallowenZipkin = 0, OyuncuNoelZipkin = 0,KalpliGulle = 0,KalpliZipkin = 0;
    public int harita = 0;
    public GameObject sonsaldiranoyuncu;
    public int seviye = 1;
    float olmeTimer = 10.0f, gemiOlmeLobiyeIsinlamaTimer = 2f;
    private int oyuncuRutbePuan = 0;
    public int GemiAnimasyonuSonGirilenId = -1, hizTasiKullanildiAnimasyonFrameGecikmesi = 0;
    public int oyuncuOzelGemiBirSatinalmaDurumu = 0, oyuncuOzelGemiIkiSatinalmaDurumu = 0, oyuncuOzelGemiUcSatinalmaDurumu = 0, oyuncuOzelGemiDortSatinalmaDurumu = 0, oyuncuOzelGemiBesSatinalmaDurumu = 0, oyuncuOzelGemiAltiSatinalmaDurumu = 0, oyuncuOzelGemiYediSatinalmaDurumu = 0, oyuncuOzelGemiSekizSatinalmaDurumu = 0, oyuncuOzelGemiNoelSatinalmaDurumu = 0, oyuncuOzelGemiDokuzSatinalmaDurumu = 0;


    string oyuncuKayitTarihi = "";
    public Sprite[] GemiAnimasyon;
    private float currentAlpha = 0f;
    private float fadeSpeed;
    public float fadeDuration = 2f; // Yavaþça geçiþ süresi (saniye)

    // 1 = kaptan, 2 = yardýmcý kaptan, 3 = kýdemli korsan , 4 = korsan
    public int OyuncuYetkiID = -1;
    public int OyuncuPremiumDurumu = 0;
    public int OyuncuSandikKatlamaDurumu = 0;
    public int OyuncuTpKatlamaDurumu = 0;
    public int OyuncuAltinKatlamaDurumu = 0;
    public int BaslangicPaket1Durumu = 0;
    public int BaslangicPaket2Durumu = 0;
    public float sonsaldirilanzaman = 0, sonRoketAtilanZaman = 0, sonHasarAlinanZaman = 0, sonTamirEdilenZaman = 0, sonaktiflikzamani = 0, hizTasiKullanmaIcýnGerekliSure = 10f, sonHizTasiKullanilanZaman = 0;
    public float OyuncuElitPuanHasarBonusu = 0, OyuncuElitPuanZirhBonusu = 0;
    public int OyuncuBotDurumu = 0;
    public int filoUyeSayisi = 0;
    public int maxFiloUyeSayisi = 0;
    public float adaBonuslari = 0;
    public int oyuncuYetenekBarut, oyuncuYetnekGemiHizi, oyuncuYetenekHizTasi, oyuncuYetenekPVEHasar, oyuncuYetenekZipkinMenzili, oyuncuYetenekZipkinSaldiriHizi, oyuncuYetenekMaxCan, oyuncuYetenekTamir,oyuncuYetenekKalkani,oyuncuYetenekMenzil, oyuncuYetenekZirh, oyuncuYetenekSaldiriHizi, oyuncuYetenekKritikHasar, oyuncuYetenekKiritikVurusIhtimali, oyuncuYetenekRoketHasari, oyuncuYetenekZipkinHasari, oyuncuYetenekZirhDelme, oyuncuYetenekIsabetOrani, oyuncuHarcananYetenekPuaniSayisi, oyuncuKalanYetenekPuaniSayisi;

    [SyncVar] public int oyuncuMaksTopYuvasi = 0, oyuncuDonanilmisTopSayisi = 0;
    [SyncVar] public float saldirihizi = 6f, zipkinSaldiriHizi = 5f, roketSaldiriHizi = 10f, menzil = 3.8f, zipkinMenzil = 4f, roketMenzil = 8f, tamirHizi = 5f, tamirMiktari = 300, hizTasiEtkiSuresi = 5f;
    [SyncVar] public bool OyunaGirisYapildimi = false;
    [SyncVar] public string RutbeId = "";
    [SyncVar] public float maksHiz = 0f;
    [SyncVar] public bool oyuncuBarutDurum = false;
    [SyncVar] public bool oyuncuKalkanDurum = false;
    [SyncVar] public bool oyuncuRoketDurum = false;
    [SyncVar] public bool oyuncuTamirDurumu = false;
    [SyncVar] public bool gemiGorunmez = false;
    [SyncVar] public int oyuncuId = -1;

    bool PanelAcikmi = false;
    List<string> dostFilolarKisaAdList = new List<string>();
    List<string> dusmanFilolarKisaAdList = new List<string>();
    public int birSonrakiBotKotntrolIcinKalanIslemSayisi = 40;
    public bool OyuncuTopSecmeDUrumu = false;
    public int OyuncuTopSatisFiyati = 0;
    public int Oyuncutoplamsatisfiyatý = 0;
    public int OyuncuSatilacakTopID = -1;
    public bool oyuncuBaskinHaritasýnaGirebilir = true;


    //GOREVLER
    public int haritaBirOnNpcKesGoreviSayac, haritaBirBesSandikToplaGoreviSayac;
    public int haritaIkiOnNpcKesGoreviSayac, haritaIkýBesNpcCanavarKesGoreviSayac,HaritaIkiOnSandikToplaSayac;
    public int haritaUcOnBesNpcKesGoreviSayac, haritaUcSekizNpcCanavarKesGoreviSayac,HaritaUcYirmiSandikToplaSayac;
    public int haritaDortYirmiNpcKesGoreviSayac, haritaDortOnIkiNpcCanavarKesGoreviSayac,HaritaDortKirkSandikToplaSayac;
    public int haritaBesOtuzNpcKesGoreviSayac, haritaBesOnSekizNpcCanavarKesGoreviSayac,HaritaBesAtmisSandikToplaSayac;

#if UNITY_SERVER || UNITY_EDITOR
    int MailDegistirmeRastgeleSayi = 0;
    int MaildegistirmeIslemiHataSayisi = 0;
    int botkontrolrastgelesayi = 0;
#endif

    [SyncVar(hook = nameof(SyncOyuncuAdiYazdir))]
    public string oyuncuadi = "";

    [SyncVar(hook = nameof(MaksCanKontrol))]
    public int MaksCan = 0;

    [SyncVar(hook = nameof(CanKontrol))]
    public int Can = 0;

    [SyncVar(hook = nameof(GemiKullan))]
    public int oyuncuKullanilanGemiId = -1;

    [SyncVar(hook = nameof(GemiHareketEttir))]
    public Vector3 target = new(0, 0, 0);

    [SyncVar(hook = nameof(HizKontrol))]
    public float hiz = 0;

    [SyncVar(hook = nameof(HizTasiAnimasyonAktiflikDurumu))]
    public bool hizTasiAnimasyonDurumu = false;

    //---------------------------------- Filo degiskenleri ---------------------------------
    public string OyuncuFiloAd = "";
    public string OyuncuFiloAciklama = "";
    public int oyuncuFiloId = -1, filoTecrubePuan = 0;
    public int FiloAltin = 0;
    [SyncVar(hook = nameof(SyncOyuncuAdiYazdir))]
    public string OyuncuFiloKisaltma = "";

#if UNITY_SERVER || UNITY_EDITOR
    private string server = "45.141.151.74";
    private string database = "sql_lostpirateso";
    private string uid = "sql_lostpirateso";
    private string password = "3YhDfCMyp5S4z2fm";
    private string connectionString;
    public MySqlConnection connection;
    public string botkontrolTXT = "";
#endif

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            GameManager.gm.BenimGemim = this.gameObject;
#if UNITY_ANDROID
            SunucuBaglantiYukleme(GameManager.gm.Version);
#endif
            X2LostCoinAktiflikDurumuYukle();
        }
        if (isClient)
        {
            fadeSpeed = 255f / fadeDuration; // Transparanlýk deðiþim hýzý
            //spriteRenderer.sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 16) + 2];
        }
#if UNITY_SERVER //|| !UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            sonaktiflikzamani = Time.time;
        }
#endif

        GetComponent<NavMeshAgent>().updateRotation = false;
        GetComponent<NavMeshAgent>().updateUpAxis = false;
        transform.eulerAngles = new Vector3(0, 0, 0);
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_SERVER || UNITY_EDITOR
        // Oyuncu olduyse geminin batmasi
        if (isServer && OyunaGirisYapildimi && Can <= 0)
        {
            gemiOlmeLobiyeIsinlamaTimer -= Time.deltaTime;
            if (gemiOlmeLobiyeIsinlamaTimer <= 0f)
            {
                if (sonsaldiranoyuncu != null && sonsaldiranoyuncu.CompareTag("Oyuncu"))
                {
                    Can = (int)(MaksCan * 0.1f);
                    CanKontrol(Can, Can);
                    Vector2 baslangickonumu = new Vector2(0, 0);
                    GetComponent<NavMeshAgent>().Warp(baslangickonumu);
                    OyuncuIsinla(baslangickonumu, sonsaldiranoyuncu.GetComponent<Player>().oyuncuadi);
                }
                else if (sonsaldiranoyuncu != null && sonsaldiranoyuncu.CompareTag("SistemGemisi"))
                {
                    Can = (int)(MaksCan * 0.1f);
                    CanKontrol(Can, Can);
                    Vector2 baslangickonumu = new Vector2(0, 0);
                    GetComponent<NavMeshAgent>().Warp(baslangickonumu);
                    OyuncuIsinla(baslangickonumu, sonsaldiranoyuncu.GetComponent<SistemGemisiKontrol>().geminame);
                }
                else if (sonsaldiranoyuncu != null && sonsaldiranoyuncu.CompareTag("SistemOyuncuGemisi"))
                {
                    Can = (int)(MaksCan * 0.1f);
                    CanKontrol(Can, Can);
                    Vector2 baslangickonumu = new Vector2(0, 0);
                    GetComponent<NavMeshAgent>().Warp(baslangickonumu);
                    OyuncuIsinla(baslangickonumu, sonsaldiranoyuncu.GetComponent<SistemOyuncuKontrol>().geminame);
                }
                else if (sonsaldiranoyuncu != null && sonsaldiranoyuncu.CompareTag("EtkinlikGemisi"))
                {
                    Can = (int)(MaksCan * 0.1f);
                    CanKontrol(Can, Can);
                    Vector2 baslangickonumu = new Vector2(0, 0);
                    GetComponent<NavMeshAgent>().Warp(baslangickonumu);
                    OyuncuIsinla(baslangickonumu, sonsaldiranoyuncu.GetComponent<EtkinlikSistemGemileriKontrol>().geminame);
                }
                else if (sonsaldiranoyuncu != null && sonsaldiranoyuncu.CompareTag("EtkinlikBossu"))
                {
                    Can = (int)(MaksCan * 0.1f);
                    CanKontrol(Can, Can);
                    Vector2 baslangickonumu = new Vector2(0, 0);
                    GetComponent<NavMeshAgent>().Warp(baslangickonumu);
                    OyuncuIsinla(baslangickonumu, sonsaldiranoyuncu.GetComponent<EtkinlikBossKontrol>().geminame);
                }
                else if (sonsaldiranoyuncu != null && sonsaldiranoyuncu.CompareTag("Tower"))
                {
                    Can = (int)(MaksCan * 0.1f);
                    CanKontrol(Can, Can);
                    Vector2 baslangickonumu = new Vector2(0, 0);
                    GetComponent<NavMeshAgent>().Warp(baslangickonumu);
                    OyuncuIsinla(baslangickonumu, sonsaldiranoyuncu.GetComponent<KuleKontrol>().geminame);
                }
                else
                {
                    Can = (int)(MaksCan * 0.1f);
                    CanKontrol(Can, Can);
                    Vector2 baslangickonumu = new Vector2(0, 0);
                    OyuncuIsinla(baslangickonumu, "");
                }
                gemiOlmeLobiyeIsinlamaTimer = 2.0f;
                sonaktiflikzamani = Time.time;
            }
        }
        // tamir
        else if (isServer && OyunaGirisYapildimi && Can < MaksCan && oyuncuTamirDurumu && Time.time - sonHasarAlinanZaman >= 3 && Time.time - sonTamirEdilenZaman >= tamirHizi)
        {
            sonTamirEdilenZaman = Time.time;
            if (Can + (int)tamirMiktari > MaksCan)
            {
                Can = MaksCan;
                CanKontrol(Can, Can);
                SetOyuncuTamirDurum(false);
            }
            else
            {
                Can = Can + (int)tamirMiktari;
                CanKontrol(Can, Can);
            }
        }


        if (isServer)
        {
            // oyuncu afk veya oyuncu oyuna giris yapmadiysa oyundan atma kodlari
            if (Time.time - sonaktiflikzamani >= 600f && OyunaGirisYapildimi)
            {
                oyuncuCikisYaptýBilgiGuncelle();
                NetworkServer.DestroyPlayerForConnection(GetComponent<NetworkIdentity>().connectionToClient);
                connectionToClient.Disconnect();
            }
            else if (Time.time - sonaktiflikzamani >= 600f && !OyunaGirisYapildimi)
            {
                NetworkServer.DestroyPlayerForConnection(GetComponent<NetworkIdentity>().connectionToClient);
                connectionToClient.Disconnect();
            }
        }

#endif
        if (!isServerOnly && OyunaGirisYapildimi)
        {
            if (isLocalPlayer && (Can <= 0 || olmeTimer < 10))
            {
                GameManager.gm.oyuncuOlduPanel.SetActive(true);
                olmeTimer -= Time.deltaTime;
                GameManager.gm.dogmaSaniye.text = ((int)(olmeTimer)).ToString();
                if (olmeTimer <= 0.0f)
                {
                    GameManager.gm.YenidenÝsinlanTXT.SetActive(true);
                    GameManager.gm.YenidenÝsinlanBTN.SetActive(true);
                    olmeTimer = 10.0f;
                }
            }

            if (isLocalPlayer)
            {
                //gemi haketi
                OyuncuGitmekIstenilenYereTikla();
                GameManager.gm.BotkontrolActiveTimer += Time.deltaTime;
                GameManager.gm.odulYazisiTimer -= Time.deltaTime;
            }
            if (isClient)
            {
                //gemi hareket animasyonu
                GemiAnimasyonu();
                if (gameObject != null && Vector2.Distance(GameManager.gm.BenimGemim.transform.position, transform.position) < 11f && gemiGorunmez == false)
                {
                    if (oyuncuTamirDurumu && transform.Find("TamirAnimasyon").gameObject.activeSelf == false)
                    {
                        transform.Find("TamirAnimasyon").gameObject.SetActive(true);
                    }
                    else if (oyuncuTamirDurumu == false && transform.Find("TamirAnimasyon").gameObject.activeSelf)
                    {
                        transform.Find("TamirAnimasyon").gameObject.SetActive(false);
                    }
                    transform.Find("MiniMapIcon").gameObject.SetActive(true);
                    if (currentAlpha < 255f)
                    {
                        currentAlpha += fadeSpeed * Time.deltaTime;
                        currentAlpha = Mathf.Clamp(currentAlpha, 0f, 255f);
                        Color spriteColor = transform.Find("Gemi").GetComponent<SpriteRenderer>().color;
                        spriteColor.a = currentAlpha / 255f;
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().color = spriteColor;
                        if (currentAlpha > 150f)
                        {
                            transform.Find("OyuncuCanvas").gameObject.SetActive(true);
                            transform.Find("OyuncuAdi").gameObject.SetActive(true);
                            transform.Find("OyuncuID").gameObject.SetActive(true);
                        }
                    }
                }
                else if (gameObject != null && gemiGorunmez)
                {
                    transform.Find("MiniMapIcon").gameObject.SetActive(false);
                    /*currentAlpha -= fadeSpeed * Time.deltaTime;
                    currentAlpha = Mathf.Clamp(currentAlpha, 0f, 255f);
                    Color spriteColor = transform.Find("Gemi").GetComponent<SpriteRenderer>().color;
                    spriteColor.a = currentAlpha / 255f;*/
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = null;
                    transform.Find("OyuncuCanvas").gameObject.SetActive(false);
                    transform.Find("OyuncuAdi").gameObject.SetActive(false);
                    transform.Find("OyuncuID").gameObject.SetActive(false);
                    /*transform.Find("Gemi").GetComponent<SpriteRenderer>().color = spriteColor;
                    if (currentAlpha < 70f)
                    {
                        transform.Find("OyuncuCanvas").gameObject.SetActive(false);
                        transform.Find("OyuncuAdi").gameObject.SetActive(false);
                    }*/
                }
                else if (gameObject != null && Vector2.Distance(GameManager.gm.BenimGemim.transform.position, transform.position) >= 11f && currentAlpha > 0f)
                {
                    transform.Find("MiniMapIcon").gameObject.SetActive(false);
                    currentAlpha -= fadeSpeed * Time.deltaTime;
                    currentAlpha = Mathf.Clamp(currentAlpha, 0f, 255f);
                    Color spriteColor = transform.Find("Gemi").GetComponent<SpriteRenderer>().color;
                    spriteColor.a = currentAlpha / 255f;
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().color = spriteColor;
                    if (currentAlpha < 70f)
                    {
                        transform.Find("OyuncuCanvas").gameObject.SetActive(false);
                        transform.Find("OyuncuAdi").gameObject.SetActive(false);
                        transform.Find("OyuncuID").gameObject.SetActive(false);
                    }
                }
            }
            if (isLocalPlayer && GameManager.gm.ÝsCooldownHiz)
            {
                GameManager.gm.ApplyCooldownHiz();
            }

            if (isLocalPlayer && GameManager.gm.ÝsCooldownSaldiri)
            {
                GameManager.gm.ApplyCooldownSaldiri();
            }
            if (isLocalPlayer && GameManager.gm.hedefgemi == null)
            {
                saldiridurumu = false;
            }
            if (isLocalPlayer && GameManager.gm.hedefgemi != null && ((GameManager.gm.hedefgemi.CompareTag("Oyuncu") && harita < 3 )  || (GameManager.gm.hedefgemi.CompareTag("Tower") && GameManager.gm.hedefgemi.GetComponent<KuleKontrol>().filoKisaltmasi == OyuncuFiloKisaltma)))
            {
                // SavasYasakliBolgeUyarisi();
            }
            if (isLocalPlayer && GameManager.gm.hedefgemi != null && ((GameManager.gm.hedefgemi.CompareTag("Oyuncu") && harita == 98) || (GameManager.gm.hedefgemi.CompareTag("Tower") && GameManager.gm.hedefgemi.GetComponent<KuleKontrol>().filoKisaltmasi == OyuncuFiloKisaltma)))
            {
                // SavasYasakliBolgeUyarisi();
            }
            else if (isLocalPlayer && GameManager.gm.hedefgemi != null && Can > 0 && ((GameManager.gm.hedefgemi.CompareTag("Oyuncu") && GameManager.gm.hedefgemi.GetComponent<Player>().Can > 0) || (GameManager.gm.hedefgemi.CompareTag("SistemGemisi") && GameManager.gm.hedefgemi.GetComponent<SistemGemisiKontrol>().Can > 0) || (GameManager.gm.hedefgemi.CompareTag("SistemOyuncuGemisi") && GameManager.gm.hedefgemi.GetComponent<SistemOyuncuKontrol>().Can > 0) || (GameManager.gm.hedefgemi.CompareTag("EtkinlikGemisi") && GameManager.gm.hedefgemi.GetComponent<EtkinlikSistemGemileriKontrol>().Can > 0) || (GameManager.gm.hedefgemi.CompareTag("EtkinlikBossu") && GameManager.gm.hedefgemi.GetComponent<EtkinlikBossKontrol>().Can > 0) || (GameManager.gm.hedefgemi.CompareTag("Tower") && GameManager.gm.hedefgemi.GetComponent<KuleKontrol>().Can > 0)))
            {
                if (Vector2.Distance(transform.position, GameManager.gm.hedefgemi.transform.position) <= menzil)
                {
                    // npc gemilerine saldirirken gulle yaratir ve oyuncuya saldirirken gulle yaratir
                    if (saldiridurumu && (GameManager.gm.hedefgemi.CompareTag("SistemGemisi") || GameManager.gm.hedefgemi.CompareTag("Tower") || GameManager.gm.hedefgemi.CompareTag("SistemOyuncuGemisi") || GameManager.gm.hedefgemi.CompareTag("EtkinlikGemisi") || GameManager.gm.hedefgemi.CompareTag("EtkinlikBossu") || GameManager.gm.hedefgemi.CompareTag("Oyuncu")) && Time.time - sonsaldirilanzaman >= saldirihizi)
                    {
                        if (GameManager.gm.hedefgemi.CompareTag("Oyuncu") && OyuncuFiloKisaltma.Length > 0 && GameManager.gm.hedefgemi.GetComponent<Player>().OyuncuFiloKisaltma == OyuncuFiloKisaltma && seciligulleid != 3)
                        {
                            saldiridurumu = false;
                        }
                        else
                        {
                            int topSayisi = oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi;
                            switch (seciligulleid)
                            {
                                case 0:

                                    GameManager.gm.SaldirButton.SetActive(false);
                                    GameManager.gm.SaldiriIptalButton.SetActive(true);
                                    GameManager.gm.WindowsSaldirButton.SetActive(false);
                                    GameManager.gm.WindowsSaldirIptalButton.SetActive(true);
                                    if (GameManager.gm.hedefgemi != null)
                                    {
                                        GulleYarat(GameManager.gm.hedefgemi, seciligulleid);
                                    }
                                    GameManager.gm.textColldownSaldiri.gameObject.SetActive(true);
                                    GameManager.gm.imageCooldownSaldiri.gameObject.SetActive(true);
                                    GameManager.gm.WindowstextColldownSaldiri.gameObject.SetActive(true);
                                    GameManager.gm.WindowsimageCooldownSaldiri.gameObject.SetActive(true);
                                    GameManager.gm.ÝsCooldownSaldiri = true;

                                    break;
                                case 1:
                                    if (oyuncuDemirGulle >= topSayisi)
                                    {
                                        GameManager.gm.SaldirButton.SetActive(false);
                                        GameManager.gm.SaldiriIptalButton.SetActive(true);
                                        GameManager.gm.WindowsSaldirButton.SetActive(false);
                                        GameManager.gm.WindowsSaldirIptalButton.SetActive(true);
                                        if (GameManager.gm.hedefgemi != null)
                                        {
                                            GulleYarat(GameManager.gm.hedefgemi, seciligulleid);
                                        }
                                        GameManager.gm.textColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.imageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowstextColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowsimageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.ÝsCooldownSaldiri = true;
                                    }
                                    else
                                    {
                                        Saldir(false);
                                        oyuncucephanesibitti();
                                    }
                                    break;
                                case 2:
                                    if (oyuncuAlevGulle >= topSayisi)
                                    {
                                        GameManager.gm.SaldirButton.SetActive(false);
                                        GameManager.gm.SaldiriIptalButton.SetActive(true);
                                        GameManager.gm.WindowsSaldirButton.SetActive(false);
                                        GameManager.gm.WindowsSaldirIptalButton.SetActive(true);
                                        if (GameManager.gm.hedefgemi != null)
                                        {
                                            GulleYarat(GameManager.gm.hedefgemi, seciligulleid);
                                        }
                                        GameManager.gm.textColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.imageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowstextColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowsimageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.ÝsCooldownSaldiri = true;
                                    }
                                    else
                                    {
                                        Saldir(false);
                                        oyuncucephanesibitti();

                                    }
                                    break;
                                case 3:
                                    if (oyuncuSifaGulle >= topSayisi)
                                    {
                                        GameManager.gm.SaldirButton.SetActive(false);
                                        GameManager.gm.SaldiriIptalButton.SetActive(true);
                                        GameManager.gm.WindowsSaldirButton.SetActive(false);
                                        GameManager.gm.WindowsSaldirIptalButton.SetActive(true);
                                        if (GameManager.gm.hedefgemi != null)
                                        {
                                            GulleYarat(GameManager.gm.hedefgemi, seciligulleid);
                                        }
                                        GameManager.gm.textColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.imageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowstextColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowsimageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.ÝsCooldownSaldiri = true;
                                    }
                                    else
                                    {
                                        Saldir(false);
                                        oyuncucephanesibitti();
                                    }
                                    break;
                                case 4:
                                    if (oyuncuHavaiFisekGulle >= topSayisi)
                                    {
                                        GameManager.gm.SaldirButton.SetActive(false);
                                        GameManager.gm.SaldiriIptalButton.SetActive(true);
                                        GameManager.gm.WindowsSaldirButton.SetActive(false);
                                        GameManager.gm.WindowsSaldirIptalButton.SetActive(true);
                                        if (GameManager.gm.hedefgemi != null)
                                        {
                                            GulleYarat(GameManager.gm.hedefgemi, seciligulleid);
                                        }
                                        GameManager.gm.textColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.imageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowstextColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowsimageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.ÝsCooldownSaldiri = true;
                                    }
                                    else
                                    {
                                        Saldir(false);
                                        oyuncucephanesibitti();
                                    }
                                    break;
                                case 10:
                                    if (OyuncuHallowenGulle >= topSayisi)
                                    {
                                        GameManager.gm.SaldirButton.SetActive(false);
                                        GameManager.gm.SaldiriIptalButton.SetActive(true);
                                        GameManager.gm.WindowsSaldirButton.SetActive(false);
                                        GameManager.gm.WindowsSaldirIptalButton.SetActive(true);
                                        if (GameManager.gm.hedefgemi != null)
                                        {
                                            GulleYarat(GameManager.gm.hedefgemi, seciligulleid);
                                        }
                                        GameManager.gm.textColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.imageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowstextColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowsimageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.ÝsCooldownSaldiri = true;
                                    }
                                    else
                                    {
                                        Saldir(false);
                                        oyuncucephanesibitti();
                                    }
                                    break;
                                case 11:
                                    if (OyuncuNoelGullesi >= topSayisi)
                                    {
                                        GameManager.gm.SaldirButton.SetActive(false);
                                        GameManager.gm.SaldiriIptalButton.SetActive(true);
                                        GameManager.gm.WindowsSaldirButton.SetActive(false);
                                        GameManager.gm.WindowsSaldirIptalButton.SetActive(true);
                                        if (GameManager.gm.hedefgemi != null)
                                        {
                                            GulleYarat(GameManager.gm.hedefgemi, seciligulleid);
                                        }
                                        GameManager.gm.textColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.imageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowstextColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowsimageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.ÝsCooldownSaldiri = true;
                                    }
                                    else
                                    {
                                        Saldir(false);
                                        oyuncucephanesibitti();
                                    }
                                    break;
                                case 12:
                                    if (KalpliGulle >= topSayisi)
                                    {
                                        GameManager.gm.SaldirButton.SetActive(false);
                                        GameManager.gm.SaldiriIptalButton.SetActive(true);
                                        GameManager.gm.WindowsSaldirButton.SetActive(false);
                                        GameManager.gm.WindowsSaldirIptalButton.SetActive(true);
                                        if (GameManager.gm.hedefgemi != null)
                                        {
                                            GulleYarat(GameManager.gm.hedefgemi, seciligulleid);
                                        }
                                        GameManager.gm.textColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.imageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowstextColldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.WindowsimageCooldownSaldiri.gameObject.SetActive(true);
                                        GameManager.gm.ÝsCooldownSaldiri = true;
                                    }
                                    else
                                    {
                                        Saldir(false);
                                        oyuncucephanesibitti();
                                    }
                                    break;
                            }

                        }
                    }
                }
            }
            if (isLocalPlayer && GameManager.gm.hedefgemi != null && oyuncuRoketDurum && ((GameManager.gm.hedefgemi.CompareTag("Oyuncu") || harita < 3  ) || (GameManager.gm.hedefgemi.CompareTag("Tower") && GameManager.gm.hedefgemi.GetComponent<KuleKontrol>().filoKisaltmasi == OyuncuFiloKisaltma)))
            {
                // SavasYasakliBolgeUyarisi();
            }
            if (isLocalPlayer && GameManager.gm.hedefgemi != null && oyuncuRoketDurum && ((GameManager.gm.hedefgemi.CompareTag("Oyuncu") || harita == 98) &&  (GameManager.gm.hedefgemi.CompareTag("Tower") && GameManager.gm.hedefgemi.GetComponent<KuleKontrol>().filoKisaltmasi == OyuncuFiloKisaltma)))
            {
                // SavasYasakliBolgeUyarisi();
            }
            else if (isLocalPlayer && GameManager.gm.hedefgemi != null && oyuncuRoketDurum && Can > 0 && ((GameManager.gm.hedefgemi.CompareTag("Oyuncu") && GameManager.gm.hedefgemi.GetComponent<Player>().Can > 0 && (OyuncuFiloKisaltma.Length > 0 && GameManager.gm.hedefgemi.GetComponent<Player>().OyuncuFiloKisaltma == OyuncuFiloKisaltma) == false) || (GameManager.gm.hedefgemi.CompareTag("SistemGemisi") && GameManager.gm.hedefgemi.GetComponent<SistemGemisiKontrol>().Can > 0) || (GameManager.gm.hedefgemi.CompareTag("Tower") && GameManager.gm.hedefgemi.GetComponent<KuleKontrol>().Can > 0) || (GameManager.gm.hedefgemi.CompareTag("SistemOyuncuGemisi") && GameManager.gm.hedefgemi.GetComponent<SistemOyuncuKontrol>().Can > 0) || (GameManager.gm.hedefgemi.CompareTag("EtkinlikGemisi") && GameManager.gm.hedefgemi.GetComponent<EtkinlikSistemGemileriKontrol>().Can > 0) || (GameManager.gm.hedefgemi.CompareTag("EtkinlikBossu") && GameManager.gm.hedefgemi.GetComponent<EtkinlikBossKontrol>().Can > 0)) && Time.time - sonRoketAtilanZaman >= roketSaldiriHizi)
            {
                RoketYarat(GameManager.gm.hedefgemi);
            }
            // deniz yaratiklarina saldirirken zipkin yaratir
            if (isLocalPlayer && GameManager.gm.hedefgemi != null && Can > 0 && saldiridurumu && GameManager.gm.hedefgemi.CompareTag("DenizYaratigi") && GameManager.gm.hedefgemi.GetComponent<SistemYaratikKontrol>().Can > 0 && Time.time - sonsaldirilanzaman >= (zipkinSaldiriHizi - (oyuncuYetenekZipkinSaldiriHizi * 0.2f)))
            {
                if (Vector2.Distance(transform.position, GameManager.gm.hedefgemi.transform.position) <= (zipkinSaldiriHizi - (oyuncuYetenekZipkinSaldiriHizi * 0.2f)))
                {
                    GameManager.gm.SaldirButton.SetActive(false);
                    GameManager.gm.SaldiriIptalButton.SetActive(true);
                    GameManager.gm.WindowsSaldirButton.SetActive(false);
                    GameManager.gm.WindowsSaldirIptalButton.SetActive(true);
                    ZipkinYarat(GameManager.gm.hedefgemi, seciliZipkinId);
                    GameManager.gm.textColldownSaldiri.gameObject.SetActive(true);
                    GameManager.gm.imageCooldownSaldiri.gameObject.SetActive(true);
                    GameManager.gm.WindowstextColldownSaldiri.gameObject.SetActive(true);
                    GameManager.gm.WindowsimageCooldownSaldiri.gameObject.SetActive(true);
                    GameManager.gm.ÝsCooldownSaldiri = true;
                }
            }
            if (isLocalPlayer)
            {
                HaritaAtlamaPozisyonKontrol();
            }
        }
    }


    public void oyuncucephanesibitti()
    {
        GameManager.gm.OdulText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 347];
        GameManager.gm.OdulYazisiSayacBaslat();
    }



    [TargetRpc]
    public void OldurenKisiyeOdulVer(string donenOyuncuAdi, int kazanilanSavasPuani)
    {
        // kimi oldurdugun hakkýnda yazi yazdiriyor
        StartCoroutine(GameManager.gm.Parlat());
        if (kazanilanSavasPuani > 0)
        {
            GameManager.gm.OdulText.GetComponent<Text>().text = donenOyuncuAdi + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 248] + kazanilanSavasPuani + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 249];
            GameManager.gm.AddItemsSeyirDefteri(donenOyuncuAdi + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 248] + kazanilanSavasPuani + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 249]);
            if (GameManager.gm.OdulText.GetComponent<Text>().text != null)
            {
                GameManager.gm.OdulYazisiSayacBaslat();
                return;
            }
        }
        else
        {
            GameManager.gm.OdulText.GetComponent<Text>().text = donenOyuncuAdi + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247];
            GameManager.gm.AddItemsSeyirDefteri(donenOyuncuAdi + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 247]);
            if (GameManager.gm.OdulText.GetComponent<Text>().text != null)
            {
                GameManager.gm.OdulYazisiSayacBaslat();
                return;
            }
        }
    }

    [TargetRpc]
    public void OyuncuIsinla(Vector2 konum, string oldurenOyuncuAdi)
    {
        // oyuncuyu isinliyor
        GetComponent<NavMeshAgent>().Warp(konum);

        // kimin oldurdugu hakkýnda yazi yazdiriyor
        GameManager.gm.OdulText.GetComponent<Text>().text = oldurenOyuncuAdi + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 254];
        GameManager.gm.AddItemsSeyirDefteri(oldurenOyuncuAdi + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 254]);
        if (GameManager.gm.OdulText.GetComponent<Text>().text != null)
        {
            GameManager.gm.OdulYazisiSayacBaslat();
            return;
        }
    }

    public void HaritaAtlamaPozisyonKontrol()
    {
        //--------------------------Harita1----------------------------
        // 1.harita --> 2.harita
        if (harita == 1 && transform.position.x > -1 && transform.position.x < 2)
        {
            if (seviye >= 2)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }

            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }
        }
        // 1.harita --> 4.harita
        else if (harita == 1 && transform.position.x > -112 && transform.position.x < -110)
        {
            if (seviye >= 4)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }
        // 1.harita --> 5.harita
        else if (harita == 1 && transform.position.y > 170.5f && transform.position.y < 173)
        {
            if (seviye >= 5)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }
        }
        //--------------------------Harita2----------------------------
        // 2.harita --> 1.harita
        else if (transform.position.x > 48 && transform.position.x < 50 && harita == 2)
        {

            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }



        }
        // 2.harita --> 3.harita
        else if (harita == 2 && transform.position.x > 159 && transform.position.x < 161)
        {
            if (seviye >= 3)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }
        // 2.harita --> 6.harita
        else if (harita == 2 && transform.position.y > 170.5f && transform.position.y < 173)
        {
            if (seviye >= 6)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }
        //--------------------------Harita3----------------------------
        // 3.harita --> 2.harita
        else if (transform.position.x > 208 && transform.position.x < 210 && harita == 3)
        {

            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }


        }
        // 3.harita --> 4.harita
        else if (harita == 3 && transform.position.x > 319 && transform.position.x < 321)
        {
            if (seviye >= 4)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }
        // 3.harita --> 7.harita
        else if (harita == 3 && transform.position.y > 170.5f && transform.position.y < 173)
        {
            if (seviye >= 7)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }
        }
        //--------------------------Harita4----------------------------
        // 4.harita --> 3.harita
        else if (harita == 4 && transform.position.x > 368 && transform.position.x < 370)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        // 4.harita --> 1.harita
        else if (harita == 4 && transform.position.x > 479 && transform.position.x < 481)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        // 4.harita --> 8.harita
        else if (harita == 4 && transform.position.y > 170.5f && transform.position.y < 173)
        {
            if (seviye >= 8)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }

        //--------------------------Harita5----------------------------
        // 5.harita --> 6.harita
        else if (harita == 5 && transform.position.x > -1 && transform.position.x < 2)
        {
            if (seviye >= 6)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }
        // 5.harita --> 8.harita
        else if (harita == 5 && transform.position.x > -112 && transform.position.x < -110)
        {
            if (seviye >= 8)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }
        // 5.harita --> 1.harita
        else if (harita == 5 && transform.position.y > 218 && transform.position.y < 222)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        // 5.harita --> 9.harita
        else if (harita == 5 && transform.position.y > 330.5f && transform.position.y < 333)
        {
            if (seviye >= 9)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }
        //--------------------------Harita6----------------------------
        // 6.harita --> 5.harita
        else if (harita == 6 && transform.position.x > 48 && transform.position.x < 50)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        // 6.harita --> 7.harita
        else if (harita == 6 && transform.position.x > 159 && transform.position.x < 161)
        {
            if (seviye >= 7)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }
        // 6.harita --> 2.harita
        else if (harita == 6 && transform.position.y > 218 && transform.position.y < 222)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        //--------------------------Harita7----------------------------
        // 7.harita --> 6.harita
        else if (harita == 7 && transform.position.x > 208 && transform.position.x < 210)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        // 7.harita --> 8.harita
        else if (harita == 7 && transform.position.x > 319 && transform.position.x < 321)
        {
            if (seviye >= 8)
            {
                if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
                {
#if UNITY_ANDROID
                    GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                    GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
                }
            }
            else
            {
                GameManager.gm.OdulText.text = "Harita Atlamak Ýçin Seviyeniz Yetersiz";
                GameManager.gm.OdulYazisiSayacBaslat();

            }

        }
        // 7.harita --> 3.harita
        else if (harita == 7 && transform.position.y > 218 && transform.position.y < 222)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        //--------------------------Harita8----------------------------
        // 8.harita --> 7.harita
        else if (harita == 8 && transform.position.x > 368 && transform.position.x < 370)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        // 8.harita --> 5.harita
        else if (harita == 8 && transform.position.x > 479 && transform.position.x < 481)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        // 8.harita --> 4.harita
        else if (harita == 8 && transform.position.y > 218 && transform.position.y < 222)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        //--------------------------Harita9----------------------------
        // 9.harita --> 5.harita
        else if (harita == 9 && transform.position.y > 379 && transform.position.y < 382)
        {
            if (!GameManager.gm.haritaAtlaButtonAndroid.activeSelf || !GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(true);
#endif
            }
        }
        else
        {
            if (GameManager.gm.haritaAtlaButtonAndroid.activeSelf || GameManager.gm.haritaAtlaButtonWindows.activeSelf)
            {
#if UNITY_ANDROID
                GameManager.gm.haritaAtlaButtonAndroid.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.haritaAtlaButtonWindows.SetActive(false);
#endif
            }
        }
    }

    // harita atlama kodlari
    [Command]
    public void HaritaAtlamaIstegiSunucu(int tur)
    {
#if UNITY_SERVER || UNITY_EDITOR

        if (tur == 0)
        {
            //--------------------------Harita1----------------------------
            // 1.harita --> 2.harita
            if (harita == 1 && seviye >= 2 && transform.position.x > -1 && transform.position.x < 2)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x + 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 2;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 2 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 1.harita --> 4.harita
            else if (harita == 1 && seviye >= 4 && transform.position.x > -112 && transform.position.x < -110)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x + 587, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 4;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 4 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 1.harita --> 5.harita
            else if (harita == 1 && seviye >= 5 && transform.position.y > 170.5f && transform.position.y < 173)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y + 52.5f));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 5;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 5 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            //--------------------------Harita2----------------------------
            // 2.harita --> 1.harita
            else if (harita == 2 && transform.position.x > 48 && transform.position.x < 50)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x - 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 1;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 1 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 2.harita --> 3.harita
            else if (harita == 2 && seviye >= 3 && transform.position.x > 159 && transform.position.x < 161)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x + 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 3;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 3 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 2.harita --> 6.harita
            else if (harita == 2 && seviye >= 6 && transform.position.y > 170.5f && transform.position.y < 173)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y + 52.5f));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 6;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 6 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }

            //--------------------------Harita3----------------------------
            // 3.harita --> 2.harita
            else if (harita == 3 && transform.position.x > 208 && transform.position.x < 210)
            {
                if (Time.time - sonHasarAlinanZaman > 10)
                {
                    GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x - 54, transform.position.y));
                    target = transform.position;
                    GemiHareketEttir(target, target);
                    harita = 2;
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET Harita = 2 WHERE Kullanici_Adi=@kullanici_adi;";
                        command.Parameters.AddWithValue("@kullanici_adi", name);
                        command.ExecuteNonQuery();
                    }
                    TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
                }
                else
                {
                    HaritaAtlamaUyarisiGonder();
                }
            }
            // 3.harita --> 4.harita
            else if (harita == 3 && seviye >= 4 && transform.position.x > 319 && transform.position.x < 321)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x + 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 4;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 4 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 3.harita --> 7.harita
            else if (harita == 3 && seviye >= 7 && transform.position.y > 170.5f && transform.position.y < 173)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y + 52.5f));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 7;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 7 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            //--------------------------Harita4----------------------------
            // 4.harita --> 3.harita
            else if (harita == 4 && transform.position.x > 368 && transform.position.x < 370)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x - 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 3;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 3 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 4.harita --> 1.harita
            else if (harita == 4 && transform.position.x > 479 && transform.position.x < 481)
            {
                if (Time.time - sonHasarAlinanZaman > 10)
                {
                    GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x - 587, transform.position.y));
                    target = transform.position;
                    GemiHareketEttir(target, target);
                    harita = 1;
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET Harita = 1 WHERE Kullanici_Adi=@kullanici_adi;";
                        command.Parameters.AddWithValue("@kullanici_adi", name);
                        command.ExecuteNonQuery();
                    }
                    TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
                }
                else
                {
                    HaritaAtlamaUyarisiGonder();
                }
            }
            // 4.harita --> 8.harita
            else if (harita == 4 && seviye >= 8 && transform.position.y > 170.5f && transform.position.y < 173)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y + 52.5f));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 8;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 8 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            //--------------------------Harita5----------------------------
            // 5.harita --> 6.harita
            else if (harita == 5 && seviye >= 6 && transform.position.x > -1 && transform.position.x < 2)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x + 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 6;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 6 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 5.harita --> 8.harita
            else if (harita == 5 && seviye >= 8 && transform.position.x > -112 && transform.position.x < -110)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x + 587, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 8;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 8 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 5.harita --> 1.harita
            else if (harita == 5 && transform.position.y > 218 && transform.position.y < 222)
            {
                if (Time.time - sonHasarAlinanZaman > 10)
                {
                    GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y - 52.5f));
                    target = transform.position;
                    GemiHareketEttir(target, target);
                    harita = 1;
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET Harita = 1 WHERE Kullanici_Adi=@kullanici_adi;";
                        command.Parameters.AddWithValue("@kullanici_adi", name);
                        command.ExecuteNonQuery();
                    }
                    TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
                }
                else
                {
                    HaritaAtlamaUyarisiGonder();
                }
            }
            // 5.harita --> 9.harita
            else if (harita == 5 && seviye >= 9 && transform.position.y > 330.5f && transform.position.y < 333)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y + 52.5f));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 9;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 9 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            //--------------------------Harita6----------------------------
            // 6.harita --> 5.harita
            else if (harita == 6 && transform.position.x > 48 && transform.position.x < 50)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x - 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 5;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 5 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 6.harita --> 7.harita
            else if (harita == 6 && seviye >= 7 && transform.position.x > 159 && transform.position.x < 161)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x + 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 7;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 7 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 6.harita --> 2.harita
            else if (harita == 6 && transform.position.y > 218 && transform.position.y < 222)
            {
                if (Time.time - sonHasarAlinanZaman > 10)
                {
                    GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y - 52.5f));
                    target = transform.position;
                    GemiHareketEttir(target, target);
                    harita = 2;
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET Harita = 2 WHERE Kullanici_Adi=@kullanici_adi;";
                        command.Parameters.AddWithValue("@kullanici_adi", name);
                        command.ExecuteNonQuery();
                    }
                    TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
                }
                else
                {
                    HaritaAtlamaUyarisiGonder();
                }
            }
            //--------------------------Harita7----------------------------
            // 7.harita --> 6.harita
            else if (harita == 7 && transform.position.x > 208 && transform.position.x < 210)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x - 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 6;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 6 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 7.harita --> 8.harita
            else if (harita == 7 && seviye >= 8 && transform.position.x > 319 && transform.position.x < 321)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x + 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 8;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 8 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 7.harita --> 3.harita
            else if (harita == 7 && transform.position.y > 218 && transform.position.y < 222)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y - 52.5f));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 3;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 3 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            //--------------------------Harita8----------------------------
            // 8.harita --> 7.harita
            else if (harita == 8 && transform.position.x > 368 && transform.position.x < 370)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x - 54, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 7;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 7 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 8.harita --> 5.harita
            else if (harita == 8 && transform.position.x > 479 && transform.position.x < 481)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x - 587, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 5;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 5 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            // 8.harita --> 4.harita
            else if (harita == 8 && transform.position.y > 218 && transform.position.y < 222)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y - 52.5f));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 4;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 4 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            //--------------------------Harita9----------------------------
            // 9.harita --> 5.harita
            else if (harita == 9 && transform.position.y > 379 && transform.position.y < 382)
            {
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y - 52.5f));
                target = transform.position;
                GemiHareketEttir(target, target);
                harita = 5;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Harita = 5 WHERE Kullanici_Adi=@kullanici_adi;";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.ExecuteNonQuery();
                }
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
        }
        //baskinHaritasiPVE
        else if (tur == 1)
        {
            if (harita != 97)
            {
                if (oyuncuBaskinHaritasýnaGirebilir == true)
                {
                    if (Time.time - sonHasarAlinanZaman > 10)
                    {
                        GetComponent<NavMeshAgent>().Warp(GameManager.gm.NavMeshPos(98));
                        target = transform.position;
                        GemiHareketEttir(target, target);
                        harita = 98;
                        TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
                    }
                    else
                    {
                        HaritaAtlamaUyarisiGonder();
                    }
                }
                else
                {
                    BaskinHaritaAtlamaUyarisiGonder();
                }
            }
          

        }
        //baskinHaritasiPVP
        else if (tur == 2)
        {
            if (harita != 98)
            {
                if (oyuncuBaskinHaritasýnaGirebilir == true)
                {
                    if (Time.time - sonHasarAlinanZaman > 10)
                    {
                        GetComponent<NavMeshAgent>().Warp(GameManager.gm.NavMeshPos(97));
                        target = transform.position;
                        GemiHareketEttir(target, target);
                        harita = 97;
                        TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
                    }
                    else
                    {
                        HaritaAtlamaUyarisiGonder();
                    }
                }
                else
                {
                    BaskinHaritaAtlamaUyarisiGonder();
                }
            }
          
        }
       
        else if (tur == 3)
        {
            if (Time.time - sonHasarAlinanZaman > 10)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT Harita FROM Kullanici WHERE ID=@ýd;";
                    command.Parameters.AddWithValue("@ýd", oyuncuId);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            harita = int.Parse(reader["Harita"].ToString());
                        }
                        reader.Close();
                    }
                }
                GetComponent<NavMeshAgent>().Warp(new Vector2(transform.position.x, transform.position.y));
                target = transform.position;
                GemiHareketEttir(target, target);
                GetComponent<NavMeshAgent>().Warp(GameManager.gm.NavMeshPos(harita));
                TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            }
            else
            {
                HaritaAtlamaUyarisiGonder();
            }
        }
#endif
    }

    [TargetRpc]
    public void BaskinHaritaAtlamaUyarisiGonder()
    {
        GameManager.gm.OdulText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 570];
        GameManager.gm.OdulYazisiSayacBaslat();
    }

    [TargetRpc]
    public void HaritaAtlamaUyarisiGonder()
    {
        GameManager.gm.OdulText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 346];
        GameManager.gm.OdulYazisiSayacBaslat();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        // return results.Count > 0;
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "UI")
            {
                return true;
            }
        }
        return false;
    }

    [Command]
    public void KullaniciGiris(string kullanici_adi, string sifre, int versionkac)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (GameManager.gm.Version == versionkac || kullanici_adi == "Gurcag")
        {
            bool oyundagemizatenacik = false;
            Vector2 baslangickonumu = GameManager.gm.NavMeshPosDogurma(1);
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Kullanici WHERE Kullanici_Adi=@kullaniciadi and Sifre=@sifre;";
                command.Parameters.AddWithValue("@kullaniciadi", kullanici_adi);
                command.Parameters.AddWithValue("@sifre", sifre);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        OyunaGirisYapildimi = true;
                        SetEtkinlikAktiflikDurumu(GameManager.gm.EtkinlikAktiflikDurumu);
                        if (GameObject.Find(kullanici_adi))
                        {
                            Player player = GameObject.Find(kullanici_adi).GetComponent<Player>();
                            baslangickonumu = GameObject.Find(kullanici_adi).GetComponent<NavMeshAgent>().transform.position;
                            NetworkServer.DestroyPlayerForConnection(GameObject.Find(kullanici_adi).GetComponent<NetworkIdentity>().connectionToClient);
                            oyundagemizatenacik = true;
                            Can = player.Can;
                            CanKontrol(Can, Can);
                            sonaktiflikzamani = Time.time;
                        }
                        else
                        {
                            Can = int.Parse(reader["Can"].ToString());
                            CanKontrol(Can, Can);
                        }
                        SetOyuncuTerubePuani(int.Parse(reader["TecrubePuani"].ToString()));
                        SetElitPuani(int.Parse(reader["ElitPuan"].ToString()));
                        SetOyuncuSifaGullesi(int.Parse(reader["SifaGulle"].ToString()));
                        SetOyuncuDemirGullesi(int.Parse(reader["DemirGulle"].ToString()));
                        SetOyuncuAlevGullesi(int.Parse(reader["AlevGulle"].ToString()));
                        SetOyuncuHavaiFisek(int.Parse(reader["HavaiFisekGulle"].ToString()));
                        SetOyuncuHallowenGullesi(int.Parse(reader["HallowenGulle"].ToString()));
                        SetOyuncuNoelGullesi(int.Parse(reader["NoelGulle"].ToString()));
                        SetOyuncuKalpliGulle(int.Parse(reader["KalpliGulle"].ToString()));
                        SetOyuncuHiztasi(int.Parse(reader["HizTasi"].ToString()));
                        SetOyuncuKalkan(int.Parse(reader["Kalkan"].ToString()));
                        SetOyuncuBarut(int.Parse(reader["Barut"].ToString()));
                        SetOyuncuRoket(int.Parse(reader["Roket"].ToString()));
                        SetOyuncuTamir(int.Parse(reader["Tamir"].ToString()));
                        SetOyuncuAltin(int.Parse(reader["Altin"].ToString()));
                        SetOyuncuSavasPuani(int.Parse(reader["SavasPuani"].ToString()));
                        SetOyuncuLostCoin(int.Parse(reader["LostCoin"].ToString()));
                        SetOyuncuId(int.Parse(reader["ID"].ToString()));
                        SetOyuncuPremiumDurumu(int.Parse(reader["PermiumDurumu"].ToString()));
                        SetOyuncuSandikKatlamaDurumu(int.Parse(reader["SandikKatlamaDurumu"].ToString()));
                        SetOyuncuTpKatlamaDurumu(int.Parse(reader["TpKatlamaDurumu"].ToString()));
                        SetOyuncuAltinKatlamaDurumu(int.Parse(reader["AltinKatlamaDurum"].ToString()));
                        SetOyuncuEtkinlikPuani(int.Parse(reader["EtkinlikPuani"].ToString()));
                        //SetOyuncuKayitTarihi(System.DateTime.Parse(reader["KayitTarihi"].ToString()).ToString("dd/MM/yyyy"));
                        //Toplari Yukleme Saslangic
                        SetCifteVurusTopDepo(int.Parse(reader["CifteVurusTopDepo"].ToString()));

                        SetOyuncuOnBesKilolukTopDepo(int.Parse(reader["onBeslikKGTopDepo"].ToString()));
                        SetOyuncuYirmiBesKilolukTopDepo(int.Parse(reader["yirmiBeslikKGTopDepo"].ToString()));
                        SetOyuncuYirmiYediBucukKilolukTopDepo(int.Parse(reader["yirmiYediBucukKGTopDepo"].ToString()));
                        SetOyuncuOtuzKilolukTopDepo(int.Parse(reader["otuzKilolukTopDepo"].ToString()));
                        SetOyuncuOtuzBesKilolukTopDepo(int.Parse(reader["otuzBesKilolukTopDepo"].ToString()));
                        SetOyuncuOnBesKilolukTopGemi(int.Parse(reader["onBeslikKGTopGemi"].ToString()));
                        SetOyuncuCifteVurusTopGemi(int.Parse(reader["CifteVurusTopGemi"].ToString()));
                        SetOyuncuYirmiBesKilolukTopGemi(int.Parse(reader["yirmiBeslikKGTopGemi"].ToString()));
                        SetOyuncuYirmiYediBucukKilolukTopGemi(int.Parse(reader["yirmiYediBucukKGTopGemi"].ToString()));
                        SetOyuncuOtuzKilolukTopGemi(int.Parse(reader["otuzKilolukTopGemi"].ToString()));
                        SetOyuncuOtuzbesKilolukTopGemi(int.Parse(reader["otuzBesKilolukTopGemi"].ToString()));
                        SetBaslangicPaket2Durumu(int.Parse(reader["BaslangicPaketi2"].ToString()));
                        SetoyuncuYetkiId(int.Parse(reader["YetkiId"].ToString()));

                        //YETENEKLER
                        SetOyuncuYetenekBarut(int.Parse(reader["YetenekBarut"].ToString()));
                        SetOyuncuYetenekGemiHizi(int.Parse(reader["YetenekGemiHizi"].ToString()));
                        SetOyuncuYetenekHizTasi(int.Parse(reader["YetenekHizTasi"].ToString()));
                        SetOyuncuYetenekPVEHasar(int.Parse(reader["YetenekPVEHasar"].ToString()));
                        SetOyuncuYetenekZipkinMenzili(int.Parse(reader["YetenekZipkinMenzili"].ToString()));
                        SetOyuncuYetenekZipkinSaldiriHizi(int.Parse(reader["YetenekZipkinSaldiriHizi"].ToString()));
                        SetOyuncuYetenekMaxCan(int.Parse(reader["YetenekMaxCan"].ToString()));
                        SetOyuncuYetnekTamir(int.Parse(reader["YetenkTamir"].ToString()));
                        SetOyuncuYetnekKalkan(int.Parse(reader["YetenekKalkan"].ToString()));
                        SetOyuncuYetenekMenzil(int.Parse(reader["YetenekMenzil"].ToString()));
                        SetOyuncuYetenekZirh(int.Parse(reader["YetenekZirh"].ToString()));
                        SetOyuncuYetenekSaldiriHizi(int.Parse(reader["YetenekSaldiriHizi"].ToString()));
                        SetOyuncuYetnekKiritikHasar(int.Parse(reader["YetenekKritikHasar"].ToString()));
                        SetOyuncuYetenekKritikVurusIhtimali(int.Parse(reader["YetenekKritikvuruþihtimali"].ToString()));
                        SetOyuncuYetenekRoketHasari(int.Parse(reader["YetenekRoketHasari"].ToString()));
                        SetOyuncuYetenekZipkinHasari(int.Parse(reader["YetenekZipkinHasari"].ToString()));
                        SetOyuncuYetenekZirhDelme(int.Parse(reader["YetenekZirhDelme"].ToString()));
                        SetOyuncuYetenekIsabetOraný(int.Parse(reader["YetenekIsabetorani"].ToString()));
                        SetOyuncuHarcananYetenekPuaniSayisi(int.Parse(reader["HarcananYetenekPuaniSayisi"].ToString()));
                        SetOyuncuKalanYetenekPuaniSayisi(int.Parse(reader["KalanYetenekPuaniSayisi"].ToString()));
                        SetOyuncuOzelGemiBir(int.Parse(reader["OzelGemiBirSatinAlinmaDurumu"].ToString()));
                        SetOyuncuOzelGemiIki(int.Parse(reader["OzelGemiIkiSatinAlinmaDurumu"].ToString()));
                        SetOyuncuOzelGemiUc(int.Parse(reader["OzelGemiUcSatinAlinmaDurumu"].ToString()));
                        SetOyuncuOzelGemiDort(int.Parse(reader["OzelGemiDortSatinAlinmaDurumu"].ToString()));
                        SetOyuncuOzelGemiBes(int.Parse(reader["OzelGemiBesSatinAlinmaDurumu"].ToString()));
                        SetOyuncuOzelGemiAlti(int.Parse(reader["OzelGemiAltiSatinAlinmaDurumu"].ToString()));
                        SetOyuncuOzelGemiSekiz(int.Parse(reader["OzelGemiSekizSatinAlinmaDurumu"].ToString()));
                        SetOyuncuOzelGemiDokuz(int.Parse(reader["OzelGemDokuzSatinAlinmaDurumu"].ToString()));
                        SetOyuncuOzelGemiEtkinlikNoel(int.Parse(reader["NoelGemisiEtkinlikSatinAlinmaDurumu"].ToString()));

                        //Toplari Yukleme Bitis
                        SetOyuncuPaslanmisZipkin(int.Parse(reader["PaslanmisZipkin"].ToString()));
                        SetOyuncuGumusZipkin(int.Parse(reader["GumusZipkin"].ToString()));
                        SetOyuncuAltinZipkin(int.Parse(reader["AltinZipkin"].ToString()));
                        SetOyuncuHallowenZipkin(int.Parse(reader["HallowenZipkin"].ToString()));
                        SetOyuncuNoelZipkin(int.Parse(reader["NoelZipkin"].ToString()));
                        SetOyuncuKalpliZipkin(int.Parse(reader["KalpliZipkin"].ToString()));

                        SetOyuncuKullanilanGemiId(int.Parse(reader["KullanilanGemiId"].ToString()));
                        SetOyuncuRutbePuan(int.Parse(reader["RutbePuan"].ToString()));
                        SetOyuncuFiloId(int.Parse(reader["FiloId"].ToString()));
                        SetoyuncuBotDurumu(int.Parse(reader["OyuncuBotDurumu"].ToString()));
                        //Gorevler
                        SetOyuncuHaritaBirOnNpcKesmeGorevi(int.Parse(reader["HaritaBirOnNpcKesmeGoreviSayac"].ToString()));
                        SetOyuncuHaritaBirBesSandikToplamaGorevi(int.Parse(reader["HaritaBirBesSandikToplamaGoreviSayac"].ToString()));
                        SetOyuncuHaritaIkiOnNpcKesmeGorevi(int.Parse(reader["HaritaBirBesSandikToplamaGoreviSayac"].ToString()));
                        SetOyuncuHaritaIkiBesNpcCanavarKesGorevi(int.Parse(reader["HaritaBirBesSandikToplamaGoreviSayac"].ToString()));
                        SetOyuncuHaritaIkiOnSandikToplamaGorevi(int.Parse(reader["HaritaIkiOnSandikToplaSayac"].ToString()));
                        SetOyuncuHaritaUcOnBesNpcKesmeGorevi(int.Parse(reader["haritaUcOnBesNpcKesGoreviSayac"].ToString()));
                        SetOyuncuHaritaUcSekizNpcCanavarKesGorevi(int.Parse(reader["haritaUcSekizNpcCanavarKesGoreviSayac"].ToString()));
                        SetOyuncuHaritaUcYirmiSandikToplamaGorevi(int.Parse(reader["HaritaUcYirmiSandikToplaSayac"].ToString()));
                        SetOyuncuHaritaDortYirmiNpcKesmeGorevi(int.Parse(reader["haritaDortYirmiNpcKesGoreviSayac"].ToString()));
                        SetOyuncuHaritaDortOnikiNpcCanavarKesGorevi(int.Parse(reader["haritaDortOnIkiNpcCanavarKesGoreviSayac"].ToString()));
                        SetOyuncuHaritaDortKirkSandikToplamaGorevi(int.Parse(reader["HaritaDortKirkSandikToplaSayac"].ToString()));
                        SetOyuncuHaritaBesOtuzNpcKesmeGorevi(int.Parse(reader["haritaBesOtuzNpcKesGoreviSayac"].ToString()));
                        SetOyuncuHaritaBesOnSekizNpcCanavarKesGorevi(int.Parse(reader["haritaBesOnSekizNpcCanavarKesGoreviSayac"].ToString()));
                        SetOyuncuHaritaBesAltmisSandikToplamaGorevi(int.Parse(reader["HaritaBesAtmisSandikToplaSayac"].ToString()));

                        harita = int.Parse(reader["Harita"].ToString());
                        if (oyundagemizatenacik == false)
                        {
                            baslangickonumu = GameManager.gm.NavMeshPosDogurma(harita);
                        }
                        GetComponent<NavMeshAgent>().enabled = true;
                        GetComponent<NavMeshAgent>().Warp(baslangickonumu);
                        sonaktiflikzamani = Time.time;
                        if (OyuncuPremiumDurumu == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "UPDATE Kullanici set PermiumDurumu = 0, PremiumTarihi = null where kullanici_adi=@kullaniciadi and sifre=@sifre and PermiumDurumu = 1 and date(PremiumTarihi) < DATE('now','start of day','-30 days');";
                                command2.Parameters.AddWithValue("@kullaniciadi", kullanici_adi);
                                command2.Parameters.AddWithValue("@sifre", sifre);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    SetOyuncuPremiumDurumu(OyuncuPremiumDurumu = 0);
                                }
                            }
                        }
                        SureliSandikPaketiKontrol();
                        SureliPaketKontrol();
                        birSonrakiBotKotntrolIcinKalanIslemSayisi = Random.Range(30, 40);
                    }
                    else
                    {
                    }
                    reader.Close();
                    if (OyunaGirisYapildimi)
                    {

#if UNITY_SERVER || !UNITY_EDITOR
                        if (OyuncuBotDurumu == 1)
                        {
                            OyuncuBotKontrol();
                        }
#endif
                        GunlukGirisOdulVer(); 
                        // rutbe kodlari
                        command.CommandText = "SELECT (select count(ID) from Kullanici WHERE RutbePuan >= (select RutbePuan from kullanici where Kullanici_Adi=@kullaniciadi))," +
                            "(select max(RutbePuan) from kullanici)," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 2))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 5))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 6))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 10))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 11))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 20))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 21))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 40))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 41))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 70))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 71))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 100))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 101))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 150))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 151))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 200))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 201))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 500))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 501))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 1000))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 1001))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 1500))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 1501))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 2000))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 2001))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 2500))," +
                            "(select min(RutbePuan) from (SELECT RutbePuan FROM Kullanici ORDER BY RutbePuan desc LIMIT 2501)) from Kullanici;";
                        command.Parameters.AddWithValue("@kullaniciadi", kullanici_adi);
                        using (IDataReader reader2 = command.ExecuteReader())
                        {
                            int[] gonderilecekVeriler = new int[29];
                            if (reader2.Read())
                            {
                                gonderilecekVeriler[0] = int.Parse(reader2[0].ToString());
                                gonderilecekVeriler[1] = int.Parse(reader2[1].ToString());
                                gonderilecekVeriler[2] = int.Parse(reader2[2].ToString());
                                gonderilecekVeriler[3] = int.Parse(reader2[3].ToString());
                                gonderilecekVeriler[4] = int.Parse(reader2[4].ToString());
                                gonderilecekVeriler[5] = int.Parse(reader2[5].ToString());
                                gonderilecekVeriler[6] = int.Parse(reader2[6].ToString());
                                gonderilecekVeriler[7] = int.Parse(reader2[7].ToString());
                                gonderilecekVeriler[8] = int.Parse(reader2[8].ToString());
                                gonderilecekVeriler[9] = int.Parse(reader2[9].ToString());
                                gonderilecekVeriler[10] = int.Parse(reader2[10].ToString());
                                gonderilecekVeriler[11] = int.Parse(reader2[11].ToString());
                                gonderilecekVeriler[12] = int.Parse(reader2[12].ToString());
                                gonderilecekVeriler[13] = int.Parse(reader2[13].ToString());
                                gonderilecekVeriler[14] = int.Parse(reader2[14].ToString());
                                gonderilecekVeriler[15] = int.Parse(reader2[15].ToString());
                                gonderilecekVeriler[16] = int.Parse(reader2[16].ToString());
                                gonderilecekVeriler[17] = int.Parse(reader2[17].ToString());
                                gonderilecekVeriler[18] = int.Parse(reader2[18].ToString());
                                gonderilecekVeriler[19] = int.Parse(reader2[19].ToString());
                                gonderilecekVeriler[20] = int.Parse(reader2[20].ToString());
                                gonderilecekVeriler[21] = int.Parse(reader2[21].ToString());
                                gonderilecekVeriler[22] = int.Parse(reader2[22].ToString());
                                gonderilecekVeriler[23] = int.Parse(reader2[23].ToString());
                                gonderilecekVeriler[24] = int.Parse(reader2[24].ToString());
                                gonderilecekVeriler[25] = int.Parse(reader2[25].ToString());
                                gonderilecekVeriler[26] = int.Parse(reader2[26].ToString());
                                gonderilecekVeriler[27] = int.Parse(reader2[27].ToString());
                                gonderilecekVeriler[28] = int.Parse(reader2[28].ToString());
                                SetOyuncuRutbe(int.Parse(reader2[0].ToString()));
                                TargetRutbeSinirlariniOyuncuyaGonder(gonderilecekVeriler);
                            }
                            reader2.Close();
                        }
                        if (oyuncuFiloId != 0)
                        {
                            command.CommandText = "SELECT * FROM Filolar WHERE FiloId=" + oyuncuFiloId + ";";
                            using (IDataReader reader3 = command.ExecuteReader())
                            {
                                if (reader3.Read())
                                {
                                    SetOyuncuFiloAd(reader3["FiloAd"].ToString());
                                    OyuncuFiloKisaltma = reader3["FiloKisaltma"].ToString();
                                    SetOyuncuFiloAciklamasi(reader3["FiloAciklama"].ToString());
                                    SetFiloAltin(int.Parse(reader3["FiloAltin"].ToString()));
                                }
                                reader3.Close();
                            }
                            command.CommandText = "SELECT * from Adalar WHERE AdaSahibiFiloId=" + oyuncuFiloId + ";";
                            using (IDataReader reader3 = command.ExecuteReader())
                            {
                                float hesaplananBonus = 0;
                                while (reader3.Read())
                                {
                                    hesaplananBonus += 0.02f;
                                }
                                SetOyuncuAdaBonuslari(hesaplananBonus);
                                reader3.Close();
                            }
                        }
                        oyuncuadi = kullanici_adi;
                        SyncOyuncuAdiYazdir(oyuncuadi, oyuncuadi);

                        using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                        {
                            command2.CommandText = "Update Kullanici SET AktiflikDurumu = 1 WHERE ID=@ID;";
                            command2.Parameters.AddWithValue("@ID", oyuncuId);
                            command2.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        else
        {
            VersionHatamesajýDon();
        }

#endif
    }

    [TargetRpc]
    public void VersionHatamesajýDon()
    {
        GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 345]);
    }

    public void OyuncuSaldiriHiziVeMenzilGuncelle()
    {
        saldirihizi = ((oyuncuOnBesKilolukTopGemi * 6) + (oyuncuYirmiBesKilolukTopGemi * 5) + (oyuncuYirmiYediBucukKilolukTopGemi * 5f) + (oyuncuOtuzKilolukTopGemi * 4.9f) + (oyuncuOtuzBesKilolukTopGemi * 4.8f) + (oyuncuCifteVurusTopGemi * 4f)) / ((oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi+oyuncuCifteVurusTopGemi));
        menzil = ((oyuncuOnBesKilolukTopGemi * 4f) + (oyuncuYirmiBesKilolukTopGemi * 5f) + (oyuncuYirmiYediBucukKilolukTopGemi * 5.5f) + (oyuncuOtuzKilolukTopGemi * 6f) + (oyuncuOtuzBesKilolukTopGemi * 6.5f) + (oyuncuCifteVurusTopGemi * 6.5f)) / ((oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi+oyuncuCifteVurusTopGemi));
        menzil = menzil + (oyuncuYetenekMenzil * 0.1f);
        saldirihizi = saldirihizi - (oyuncuYetenekSaldiriHizi * 0.1f);
    }

    public void SetOyuncuKullanilanGemiId(int deger)
    {
        oyuncuKullanilanGemiId = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer && OyunaGirisYapildimi)
        {
            if (oyuncuElitPuan >= 30000000)
            {
                MaksCan = 12000 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(34);
                maksHiz = 2.50f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.50f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.14f;
                OyuncuElitPuanZirhBonusu = 0.075f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 22000000)
            {
                MaksCan = 11500 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(33);
                maksHiz = 2.42f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.42f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.13f;
                OyuncuElitPuanZirhBonusu = 0.070f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 18000000)
            {
                MaksCan = 11000 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(32);
                maksHiz = 2.38f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.38f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.12f;
                OyuncuElitPuanZirhBonusu = 0.065f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 14000000)
            {
                MaksCan = 10500 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(31);
                maksHiz = 2.34f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.34f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.11f;
                OyuncuElitPuanZirhBonusu = 0.060f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 9000000)
            {
                MaksCan = 10000 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(30);
                maksHiz = 2.3f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.3f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.1f;
                OyuncuElitPuanZirhBonusu = 0.055f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 5000000)
            {
                MaksCan = 9500 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(29);
                maksHiz = 2.28f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.28f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.09f;
                OyuncuElitPuanZirhBonusu = 0.050f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 2800000)
            {
                MaksCan = 9000 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(28);
                maksHiz = 2.24f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.24f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.08f;
                OyuncuElitPuanZirhBonusu = 0.045f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 1400000)
            {
                MaksCan = 8500 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(27);
                maksHiz = 2.2f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.2f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.07f;
                OyuncuElitPuanZirhBonusu = 0.040f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 700000)
            {
                MaksCan = 8000 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(26);
                maksHiz = 2.15f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.15f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.06f;
                OyuncuElitPuanZirhBonusu = 0.035f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 300000)
            {
                MaksCan = 7500 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(25);
                maksHiz = 2.1f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.1f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.05f;
                OyuncuElitPuanZirhBonusu = 0.030f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 100000)
            {
                MaksCan = 7000 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(24);
                maksHiz = 2.05f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.05f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.04f;
                OyuncuElitPuanZirhBonusu = 0.025f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 20000)
            {
                MaksCan = 6500 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(23);
                maksHiz = 2f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.03f;
                OyuncuElitPuanZirhBonusu = 0.020f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 7000)
            {
                MaksCan = 6000 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(22);
                maksHiz = 1.95f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 1.95f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.02f;
                OyuncuElitPuanZirhBonusu = 0.015f;
                HizKontrol(hiz, hiz);
            }
            else if (oyuncuElitPuan >= 1500)
            {
                MaksCan = 5500 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(21);
                maksHiz = 1.9f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 1.9f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0.01f;
                OyuncuElitPuanZirhBonusu = 0.010f;
                HizKontrol(hiz, hiz);

            }
            else if (oyuncuElitPuan >= 0)
            {
                MaksCan = 5000 + (oyuncuYetenekMaxCan * 500);
                if (Can == 0)
                {
                    Can = MaksCan;
                    CanKontrol(Can, Can);
                }
                SetMaksTopYuvasi(20);
                maksHiz = 1.8f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 1.8f + (oyuncuYetnekGemiHizi * 0.025f);
                OyuncuElitPuanHasarBonusu = 0;
                OyuncuElitPuanZirhBonusu = 0;
                HizKontrol(hiz, hiz);
            }
        }
#endif
    }

    public void SetOyuncuKayitTarihi(string deger)
    {
        oyuncuKayitTarihi = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuKayitTarihi(oyuncuKayitTarihi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.KayitTarihi.text = oyuncuKayitTarihi;
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuKayitTarihi(string donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuKayitTarihi(donenVeri);
        }
    }




    //----------------------------------------- Toplar ---------------------------------------//
    public void ToplariDiz(int onBesKilolukTopDepo, int yirmiBesKilolukTopDepo, int yirmiYediBucukKilolukTopDepo, int otuzKilolukTopDepo, int otuzBesKilolukTopDepo, int onBesKilolukTopGemi, int yirmiBesKilolukTopGemi, int yirmiYediBucukKilolukTopGemi, int otuzKilolukTopGemi, int otuzBesKilolukTopGemi,int cifteVurusTopGemi,int cifteVurusTopDepo)
    {
        // depodaki slotlari bosaltan kod
        for (int x = 0; x < 20; x++)
        {
            GameManager.gm.depodakiToplarSlot[x].sprite = GameManager.gm.SlotBoslukKontrol;
            GameManager.gm.DepoSlotText[x].text = "";
            GameManager.gm.depodakiToplarSlot[x].GetComponent<TopYuvasi>().topId = -1;
            OyuncuTopSecmeDUrumu = false;
            GameManager.gm.depodakiToplarSlot[x].GetComponent<TopYuvasi>().transform.Find("SelectedSlot").gameObject.SetActive(false);

        }
        for (int x = 0; x < 6; x++)
        {
            GameManager.gm.gemidekiToplarSlot[x].sprite = GameManager.gm.SlotBoslukKontrol2;
            GameManager.gm.GemiSlotText[x].text = "";
        }

        int i = 0;
        if (cifteVurusTopDepo > 0)
        {
            GameManager.gm.depodakiToplarSlot[i].sprite = GameManager.gm.TopImage[5];
            GameManager.gm.depodakiToplarSlot[i].GetComponent<TopYuvasi>().topId = 5;
            GameManager.gm.DepoSlotText[i].text = cifteVurusTopDepo.ToString();
            i++;
        }
        if (otuzBesKilolukTopDepo > 0)
        {
            GameManager.gm.depodakiToplarSlot[i].sprite = GameManager.gm.TopImage[4];
            GameManager.gm.depodakiToplarSlot[i].GetComponent<TopYuvasi>().topId = 4;
            GameManager.gm.DepoSlotText[i].text = otuzBesKilolukTopDepo.ToString();
            i++;
        }
        if (otuzKilolukTopDepo > 0)
        {
            GameManager.gm.depodakiToplarSlot[i].sprite = GameManager.gm.TopImage[3];
            GameManager.gm.depodakiToplarSlot[i].GetComponent<TopYuvasi>().topId = 3;
            GameManager.gm.DepoSlotText[i].text = otuzKilolukTopDepo.ToString();
            i++;
        }
        if (yirmiYediBucukKilolukTopDepo > 0)
        {
            GameManager.gm.depodakiToplarSlot[i].sprite = GameManager.gm.TopImage[2];
            GameManager.gm.depodakiToplarSlot[i].GetComponent<TopYuvasi>().topId = 2;
            GameManager.gm.DepoSlotText[i].text = yirmiYediBucukKilolukTopDepo.ToString();
            i++;
        }
        if (yirmiBesKilolukTopDepo > 0)
        {
            GameManager.gm.depodakiToplarSlot[i].sprite = GameManager.gm.TopImage[1];
            GameManager.gm.depodakiToplarSlot[i].GetComponent<TopYuvasi>().topId = 1;
            GameManager.gm.DepoSlotText[i].text = yirmiBesKilolukTopDepo.ToString();
            i++;
        }
        if (onBesKilolukTopDepo > 0)
        {
            GameManager.gm.depodakiToplarSlot[i].sprite = GameManager.gm.TopImage[0];
            GameManager.gm.depodakiToplarSlot[i].GetComponent<TopYuvasi>().topId = 0;
            GameManager.gm.DepoSlotText[i].text = onBesKilolukTopDepo.ToString();
        }

        i = 0;
        if (cifteVurusTopGemi > 0)
        {
            GameManager.gm.gemidekiToplarSlot[i].sprite = GameManager.gm.TopImage[5];
            GameManager.gm.gemidekiToplarSlot[i].GetComponent<TopYuvasi>().topId = 5;
            GameManager.gm.GemiSlotText[i].text = cifteVurusTopGemi.ToString();

            i++;
        }
        if (otuzBesKilolukTopGemi > 0)
        {
            GameManager.gm.gemidekiToplarSlot[i].sprite = GameManager.gm.TopImage[4];
            GameManager.gm.gemidekiToplarSlot[i].GetComponent<TopYuvasi>().topId = 4;
            GameManager.gm.GemiSlotText[i].text = otuzBesKilolukTopGemi.ToString();

            i++;
        }
        if (otuzKilolukTopGemi > 0)
        {
            GameManager.gm.gemidekiToplarSlot[i].sprite = GameManager.gm.TopImage[3];
            GameManager.gm.gemidekiToplarSlot[i].GetComponent<TopYuvasi>().topId = 3;
            GameManager.gm.GemiSlotText[i].text = otuzKilolukTopGemi.ToString();
            i++;
        }
        if (yirmiYediBucukKilolukTopGemi > 0)
        {
            GameManager.gm.gemidekiToplarSlot[i].sprite = GameManager.gm.TopImage[2];
            GameManager.gm.gemidekiToplarSlot[i].GetComponent<TopYuvasi>().topId = 2;
            GameManager.gm.GemiSlotText[i].text = yirmiYediBucukKilolukTopGemi.ToString();
            i++;
        }
        if (yirmiBesKilolukTopGemi > 0)
        {
            GameManager.gm.gemidekiToplarSlot[i].sprite = GameManager.gm.TopImage[1];
            GameManager.gm.gemidekiToplarSlot[i].GetComponent<TopYuvasi>().topId = 1;
            GameManager.gm.GemiSlotText[i].text = yirmiBesKilolukTopGemi.ToString();
            i++;
        }
        if (onBesKilolukTopGemi > 0)
        {
            GameManager.gm.gemidekiToplarSlot[i].sprite = GameManager.gm.TopImage[0];
            GameManager.gm.gemidekiToplarSlot[i].GetComponent<TopYuvasi>().topId = 0;
            GameManager.gm.GemiSlotText[i].text = onBesKilolukTopGemi.ToString();
        }
    }

    public void SetOyuncuOnBesKilolukTopDepo(int deger)
    {
        oyuncuOnBesKilolukTopDepo = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOnBesKilolukTopDepo(oyuncuOnBesKilolukTopDepo);
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi,oyuncuCifteVurusTopGemi,oyuncuCifteVurusTopDepo);
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOnBesKilolukTopDepo(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOnBesKilolukTopDepo(donenVeri);
        }
    }

    public void SetOyuncuYirmiBesKilolukTopDepo(int deger)
    {
        oyuncuYirmiBesKilolukTopDepo = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYirmiBesKilolukTopDepo(oyuncuYirmiBesKilolukTopDepo);
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYirmiBesKilolukTopDepo(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYirmiBesKilolukTopDepo(donenVeri);
        }
    }

    public void SetOyuncuYirmiYediBucukKilolukTopDepo(int deger)
    {
        oyuncuYirmiYediBucukKilolukTopDepo = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYirmiYediBucukKilolukTopDepo(oyuncuYirmiYediBucukKilolukTopDepo);
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYirmiYediBucukKilolukTopDepo(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYirmiYediBucukKilolukTopDepo(donenVeri);
        }
    }


    public void SetOyuncuOtuzKilolukTopDepo(int deger)
    {
        oyuncuOtuzKilolukTopDepo = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOtuzKilolukTopDepo(oyuncuOtuzKilolukTopDepo);
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOtuzKilolukTopDepo(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOtuzKilolukTopDepo(donenVeri);
        }
    }

    public void SetOyuncuOtuzBesKilolukTopDepo(int deger)
    {
        oyuncuOtuzBesKilolukTopDepo = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOtuzBesKilolukTopDepo(oyuncuOtuzBesKilolukTopDepo);
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOtuzBesKilolukTopDepo(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOtuzBesKilolukTopDepo(donenVeri);
        }
    }

    public void SetCifteVurusTopDepo(int deger)
    {
        oyuncuCifteVurusTopDepo = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetCifteVurusTopDepo(oyuncuCifteVurusTopDepo);
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);
        }
    }

    [TargetRpc]
    public void TargetSetCifteVurusTopDepo(int donenVeri)
    {
        if (!isServer)
        {
            SetCifteVurusTopDepo(donenVeri);
        }
    }

    public void SetOyuncuOnBesKilolukTopGemi(int deger)
    {
        oyuncuOnBesKilolukTopGemi = deger;
        oyuncuDonanilmisTopSayisi = oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi + oyuncuCifteVurusTopGemi;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOnBesKilolukTopGemi(oyuncuOnBesKilolukTopGemi);
            OyuncuSaldiriHiziVeMenzilGuncelle();
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);


        }

    }



    [TargetRpc]
    public void TargetSetOyuncuOnBesKilolukTopGemi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOnBesKilolukTopGemi(donenVeri);

        }
    }

    public void SetOyuncuYirmiBesKilolukTopGemi(int deger)
    {
        oyuncuYirmiBesKilolukTopGemi = deger;
        oyuncuDonanilmisTopSayisi = oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi + oyuncuCifteVurusTopGemi;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYirmiBesKilolukTopGemi(oyuncuYirmiBesKilolukTopGemi);
            OyuncuSaldiriHiziVeMenzilGuncelle();
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);

        }

    }

    [TargetRpc]
    public void TargetSetOyuncuYirmiBesKilolukTopGemi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYirmiBesKilolukTopGemi(donenVeri);
        }
    }

    public void SetOyuncuYirmiYediBucukKilolukTopGemi(int deger)
    {
        oyuncuYirmiYediBucukKilolukTopGemi = deger;
        oyuncuDonanilmisTopSayisi = oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi + oyuncuCifteVurusTopGemi;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYirmiYediBucukKilolukTopGemi(oyuncuYirmiYediBucukKilolukTopGemi);
            OyuncuSaldiriHiziVeMenzilGuncelle();
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);

        }

    }

    [TargetRpc]
    public void TargetSetOyuncuYirmiYediBucukKilolukTopGemi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYirmiYediBucukKilolukTopGemi(donenVeri);
        }
    }
    public void SetOyuncuOtuzKilolukTopGemi(int deger)
    {
        oyuncuOtuzKilolukTopGemi = deger;
        oyuncuDonanilmisTopSayisi = oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi + oyuncuCifteVurusTopGemi;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOtuzKilolukTopGemi(oyuncuOtuzKilolukTopGemi);
            OyuncuSaldiriHiziVeMenzilGuncelle();
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);
        }

    }

    [TargetRpc]
    public void TargetSetOtuzKilolukTopGemi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOtuzKilolukTopGemi(donenVeri);
        }
    }
    public void SetOyuncuOtuzbesKilolukTopGemi(int deger)
    {
        oyuncuOtuzBesKilolukTopGemi = deger;
        oyuncuDonanilmisTopSayisi = oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi + oyuncuCifteVurusTopGemi; 
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOtuzbesKilolukTopGemi(oyuncuOtuzBesKilolukTopGemi);
            OyuncuSaldiriHiziVeMenzilGuncelle();
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);
        }

    }

    [TargetRpc]
    public void TargetSetOtuzbesKilolukTopGemi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOtuzbesKilolukTopGemi(donenVeri);
        }
    }



    public void SetOyuncuCifteVurusTopGemi(int deger)
    {
        oyuncuCifteVurusTopGemi = deger;
        oyuncuDonanilmisTopSayisi = oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi+oyuncuCifteVurusTopGemi;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuCifteVurusTopGemi(oyuncuCifteVurusTopGemi);
            OyuncuSaldiriHiziVeMenzilGuncelle();
        }
#endif
        if (isLocalPlayer)
        {
            ToplariDiz(oyuncuOnBesKilolukTopDepo, oyuncuYirmiBesKilolukTopDepo, oyuncuYirmiYediBucukKilolukTopDepo, oyuncuOtuzKilolukTopDepo, oyuncuOtuzBesKilolukTopDepo, oyuncuOnBesKilolukTopGemi, oyuncuYirmiBesKilolukTopGemi, oyuncuYirmiYediBucukKilolukTopGemi, oyuncuOtuzKilolukTopGemi, oyuncuOtuzBesKilolukTopGemi, oyuncuCifteVurusTopGemi, oyuncuCifteVurusTopDepo);
        }

    }

    [TargetRpc]
    public void TargetSetOyuncuCifteVurusTopGemi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuCifteVurusTopGemi(donenVeri);
        }
    }
    public void SetOyuncuRutbe(int oyuncununRutbeSirasi)
    {
        if (oyuncununRutbeSirasi <= 1)
        {
            //DenizlerinHukumdari
            RutbeId = "Rutbe14";
        }
        else if (oyuncununRutbeSirasi <= 5)
        {
            //KorsanKrali
            RutbeId = "Rutbe13";
        }
        else if (oyuncununRutbeSirasi <= 10)
        {
            //KorsanBaronu
            RutbeId = "Rutbe12";
        }
        else if (oyuncununRutbeSirasi <= 20)
        {
            //Baron
            RutbeId = "Rutbe11";
        }
        else if (oyuncununRutbeSirasi <= 40)
        {
            //YuzBasi
            RutbeId = "Rutbe10";
        }
        else if (oyuncununRutbeSirasi <= 70)
        {
            //Yagmaci
            RutbeId = "Rutbe9";
        }
        else if (oyuncununRutbeSirasi <= 100)
        {
            //DenizHaydutu
            RutbeId = "Rutbe8";
        }
        else if (oyuncununRutbeSirasi <= 150)
        {
            //GuverteSubayi
            RutbeId = "Rutbe7";
        }
        else if (oyuncununRutbeSirasi <= 200)
        {
            //Cavus
            RutbeId = "Rutbe6";
        }
        else if (oyuncununRutbeSirasi <= 500)
        {
            //DenizEri
            RutbeId = "Rutbe5";
        }
        else if (oyuncununRutbeSirasi <= 1000)
        {
            //Gulleci
            RutbeId = "Rutbe4";
        }
        else if (oyuncununRutbeSirasi <= 1500)
        {
            //Tayfa
            RutbeId = "Rutbe3";
        }
        else if (oyuncununRutbeSirasi <= 2000)
        {
            //GemiCaylagi
            RutbeId = "Rutbe2";
        }
        else if (oyuncununRutbeSirasi <= 2500)
        {
            //AcemiDenizci
            RutbeId = "Rutbe1";
        }
        else
        {
            //Egitmen
            RutbeId = "Rutbe0";
        }
    }

    [TargetRpc]
    public void TargetRutbeSinirlariniOyuncuyaGonder(int[] donenRutbeSinirlari)
    {
        int oyuncununRutbeSirasi = int.Parse(donenRutbeSinirlari[0].ToString());
        if (oyuncununRutbeSirasi <= 1)
        {
            //DenizlerinHukumdari
            GameManager.gm.ustRutbePuanText.text = "";
            GameManager.gm.ustRutbePuanImage.enabled = false;
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[2].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[13];
            GameManager.gm.mevcutRutbePuanText.text = "Mevcut Rutbe Puaný: " + donenRutbeSinirlari[1].ToString();
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[14];
            RutbeId = "Rutbe14";
        }
        else if (oyuncununRutbeSirasi <= 5)
        {
            //KorsanKrali
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[1].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[14];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[4].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[12];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[13];
            RutbeId = "Rutbe13";
        }
        else if (oyuncununRutbeSirasi <= 10)
        {
            //KorsanBaronu
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[3].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[13];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[6].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[11];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[12];
            RutbeId = "Rutbe12";
        }
        else if (oyuncununRutbeSirasi <= 20)
        {
            //Baron
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[5].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[12];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[8].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[10];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[11];
            RutbeId = "Rutbe11";
        }
        else if (oyuncununRutbeSirasi <= 40)
        {
            //YuzBasi
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[7].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[11];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[10].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[9];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[10];
            RutbeId = "Rutbe10";
        }
        else if (oyuncununRutbeSirasi <= 70)
        {
            //Yagmaci
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[9].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[10];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[12].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[8];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[9];
            RutbeId = "Rutbe9";
        }
        else if (oyuncununRutbeSirasi <= 100)
        {
            //DenizHaydutu
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[11].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[9];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[14].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[7];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[8];
            RutbeId = "Rutbe8";
        }
        else if (oyuncununRutbeSirasi <= 150)
        {
            //GuverteSubayi
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[13].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[8];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[16].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[6];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[7];
            RutbeId = "Rutbe7";
        }
        else if (oyuncununRutbeSirasi <= 200)
        {
            //Cavus
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[15].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[7];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[18].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[5];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[6];
            RutbeId = "Rutbe6";
        }
        else if (oyuncununRutbeSirasi <= 500)
        {
            //DenizEri
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[17].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[6];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[20].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[4];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[5];
            RutbeId = "Rutbe5";
        }
        else if (oyuncununRutbeSirasi <= 1000)
        {
            //Gulleci
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[19].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[5];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[22].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[3];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[4];
            RutbeId = "Rutbe4";
        }
        else if (oyuncununRutbeSirasi <= 1500)
        {
            //Tayfa
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[21].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[4];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[24].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[2];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[3];
            RutbeId = "Rutbe3";
        }
        else if (oyuncununRutbeSirasi <= 2000)
        {
            //GemiCaylagi
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[23].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[3];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[26].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[1];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[2];
            RutbeId = "Rutbe2";
        }
        else if (oyuncununRutbeSirasi <= 2500)
        {
            //AcemiDenizci
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[25].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[2];
            GameManager.gm.altRutbePuanText.text = "Alt Rutbe Puaný: " + donenRutbeSinirlari[28].ToString();
            GameManager.gm.altRutbePuanImage.sprite = GameManager.gm.rutbeResimler[0];
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[1];
            RutbeId = "Rutbe1";
        }
        else
        {
            //Egitmen
            GameManager.gm.ustRutbePuanText.text = "Ust Rutbe Puaný: " + donenRutbeSinirlari[27].ToString();
            GameManager.gm.ustRutbePuanImage.sprite = GameManager.gm.rutbeResimler[1];
            GameManager.gm.altRutbePuanText.enabled = false;
            GameManager.gm.altRutbePuanImage.enabled = false;
            GameManager.gm.mevcutRutbePuanImage.sprite = GameManager.gm.rutbeResimler[0];
            RutbeId = "Rutbe0";
        }
    }

    public void SetMaksTopYuvasi(int maksTopSayisi)
    {
        oyuncuMaksTopYuvasi = maksTopSayisi;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetMaksTopYuvasi(oyuncuMaksTopYuvasi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.GemiTopSayisiDurumuText.text = oyuncuDonanilmisTopSayisi.ToString() + "/" + oyuncuMaksTopYuvasi.ToString();
        }
    }

    [TargetRpc]
    public void TargetSetMaksTopYuvasi(int donenVeri)
    {
        if (!isServer)
        {
            SetMaksTopYuvasi(donenVeri);
        }
    }


    public void SetDonanilmisTopSayisi(int donanilmistop)
    {
        oyuncuDonanilmisTopSayisi = donanilmistop;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetDonanilmisTopsayisi(oyuncuDonanilmisTopSayisi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.GemiTopSayisiDurumuText.text = oyuncuDonanilmisTopSayisi.ToString() + "/" + oyuncuMaksTopYuvasi.ToString();
        }
    }

    [TargetRpc]
    public void TargetSetDonanilmisTopsayisi(int donenVeri)
    {
        if (!isServer)
        {
            SetDonanilmisTopSayisi(donenVeri);
        }
    }

    [TargetRpc]
    public void GirisYapildiVeriDonusu(bool girisYapilmaDurumu, Vector2 baslangickonumu, int donenHarita, int dayCycle, float dunyaSaati, string[] AdaSahibiFiloKisaAdlari)
    {
        GameManager.gm.FiloAdalariSahibiKisaAdlari = AdaSahibiFiloKisaAdlari;
        for (int i = 0; i < AdaSahibiFiloKisaAdlari.Length; i++)
        {
            GameManager.gm.miniHaritaFiloKisaAdlari[i].text = AdaSahibiFiloKisaAdlari[i];
        }
        if (girisYapilmaDurumu)
        {
            GameManager.gm.OyunaGirisYapildimi = true;
            OyunaGirisYapildimi = true;
            if (GameManager.gm.BeniHatirla.isOn)
            {
                PlayerPrefs.SetInt("Isaret", 1);
                PlayerPrefs.SetString("benihatirlakadi", GameManager.gm.kullaniciAdi);
                PlayerPrefs.SetString("benihatirlasifre", GameManager.gm.sifre);
            }
            harita = donenHarita;
            MiniMapKameraAyarla();
            // Giriþ yapýldýysa çalýþacak kodlar
            GetComponent<NavMeshAgent>().Warp(baslangickonumu);
            GameManager.gm.OyuniciAryuz.SetActive(true);
            GameManager.gm.GirisYapMenu.SetActive(false);
            GameManager.gm.BenimGemim.transform.Find("OyuncuCanvas/Cember").gameObject.GetComponent<Image>().color = Color.white;
            GameManager.gm.BenimGemim.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(true);
#if UNITY_ANDROID
            GameManager.gm.HaritaTXT.text = harita.ToString();
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinHangiMapTXT.text = harita.ToString();
#endif
            GameManager.gm.ZipkinDegistir(0);
            GameManager.gm.GulleDegistir(0);
            //transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 2];
            if (oyuncuFiloId > 0)
            {
                FiloCevrimiciUyeleriCek();
            }
            GameManager.gm.SesSlider.value = PlayerPrefs.GetFloat("audioVolumeGulle");
            GameManager.gm.GemiGulleAtmaSesi.volume = PlayerPrefs.GetFloat("audioVolumeGulle");
            GameManager.gm.GemiZipkinAtmaSesi.volume = PlayerPrefs.GetFloat("audioVolumeGulle");
        }
        else
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 266]);
        }
    }

    [Command]
    public void SendMessageGlobal(string message)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            int banlimi = 0;
            bool isTextFound = CheckText(message, "/banla ");
            bool banAc = CheckText(message, "/banac ");
            bool denkGel = CheckText(message, "/denkgel ");
            bool kov = CheckText(message, "/kov ");
            bool isinlan = CheckText(message, "/isinlan ");
            bool Gorunmez = CheckText(message, "/gorunmez");
            bool doublepay = CheckText(message, "/x2baslat");

            if (isTextFound && oyuncuId == 16)
            {
                string banlanacakKullaniciAdi = ProcessText(message, "/banla ");
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET BanDurumu = 1 WHERE Kullanici_Adi=@kullaniciAdi;";
                    command.Parameters.AddWithValue("@kullaniciAdi", banlanacakKullaniciAdi);
                    command.ExecuteNonQuery();
                }
            }
            else if (banAc && oyuncuId == 16)
            {
                string banAcilacakKullaniciAdi = ProcessText(message, "/banac ");
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET BanDurumu = 0 WHERE Kullanici_Adi=@kullaniciAdi;";
                    command.Parameters.AddWithValue("@kullaniciAdi", banAcilacakKullaniciAdi);
                    command.ExecuteNonQuery();
                }
            }
            else if (denkGel && oyuncuId == 16)
            {
                string denkGelinecekOyuncuAdi = ProcessText(message, "/denkgel ");
                GameManager.gm.OyuncuDenkgelIsinla(denkGelinecekOyuncuAdi);
            }
            else if (kov && oyuncuId == 16)
            {
                string kovulanOyuncununAdi = ProcessText(message, "/kov ");
                GameManager.gm.OyuncuKov(kovulanOyuncununAdi);

            }
            else if (isinlan && oyuncuId == 16)
            {
                string isinlanilacakOyuncununAdi = ProcessText(message, "/isinlan ");
                if (GameObject.Find(isinlanilacakOyuncununAdi))
                {
                    Player player = GameObject.Find(isinlanilacakOyuncununAdi).GetComponent<Player>();
                    GetComponent<NavMeshAgent>().Warp(player.transform.position);
                    Sunucugemigezdir(player.transform.position);
                    TargetAtlamaIstegiSunucu(player.GetComponent<Player>().harita, transform.position);
                }
            }
            else if (Gorunmez && oyuncuId == 16)
            {
                gemiGorunmez = !gemiGorunmez;
            }
            else if (doublepay && oyuncuId == 16)
            {
               GameManager.gm.IkiKatiLostCoinBaslatSunucu();
            }
            else if (oyuncuTecrubePuan > 60)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Kullanici WHERE Kullanici_Adi=@kullaniciadi;";
                    command.Parameters.AddWithValue("@kullaniciadi", name);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            banlimi = int.Parse(reader["BanDurumu"].ToString());
                        }
                        reader.Close();
                    }
                }
                if (banlimi == 0)
                {
                    GameManager.gm.oyuncularadonensohbetveri(message, "<link=" + oyuncuId + ">" + name + "</link>", OyuncuFiloKisaltma, oyuncuId);
                }
            }
        }
#endif
    }

    [Command]
    public void SendMessageFilo(string message)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            int banlimi = 0;
            if (oyuncuTecrubePuan > 60 && oyuncuFiloId > 0)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Kullanici WHERE ID=@kullaniciadi;";
                    command.Parameters.AddWithValue("@kullaniciadi", oyuncuId);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            banlimi = int.Parse(reader["BanDurumu"].ToString());
                        }
                        reader.Close();
                    }
                }
                if (banlimi == 0)
                {
                    GameManager.gm.Filodandonensohbetveri(message, "<link=" + oyuncuId + ">" + name + "</link>", OyuncuFiloKisaltma, oyuncuId, oyuncuFiloId);
                }
            }
        }
#endif
    }

#if UNITY_SERVER || UNITY_EDITOR
    private string ProcessText(string text, string girilenKod)
    {
        string processedText = text.Replace(girilenKod, string.Empty).Trim(); // /bancarij kelimesini çýkar ve boþluklarý temizle

        return processedText;
    }
    private bool CheckText(string text, string word)
    {
        return text.Contains(word); // Metinde belirli bir kelimeyi kontrol etmek için Contains() fonksiyonunu kullanýn
    }
#endif

    

    private void OyuncuGitmekIstenilenYereTikla()
    {
        if (Input.GetMouseButtonDown(0) && OyunaGirisYapildimi && !IsPointerOverUIObject())
        {
            // gemi hareket kodlarý
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //eðer bir gemiye týklandýysa
            if (hit.collider != null && (hit.collider.gameObject.transform.CompareTag("SistemGemisi") || hit.collider.gameObject.transform.CompareTag("Tower") || hit.collider.gameObject.transform.CompareTag("SistemOyuncuGemisi") || hit.collider.gameObject.transform.CompareTag("EtkinlikGemisi") || hit.collider.gameObject.transform.CompareTag("EtkinlikBossu") || hit.collider.gameObject.transform.CompareTag("Merkez") || hit.collider.gameObject.transform.CompareTag("DenizYaratigi") || hit.collider.gameObject.transform.CompareTag("Oyuncu") || hit.collider.gameObject.transform.CompareTag("Sandik")) && hit.collider.gameObject.name != oyuncuadi)
            {
                //hedef secme
                if (GameManager.gm.hedefgemi != null)
                {
                    GameManager.gm.SaldiriDurdur();
                    GameManager.gm.hedefgemi.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(false);
                }
                if (hit.collider.gameObject.transform.CompareTag("SistemGemisi"))
                {
                    GameManager.gm.GulleDegistir(seciligulleid);
                    GameManager.gm.SaldiriDurdur();
                    GameManager.gm.hedefgemi = hit.collider.gameObject;
                    GameManager.gm.hedefgemi.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(true);
                }
                else if (hit.collider.gameObject.transform.CompareTag("SistemOyuncuGemisi"))
                {
                    GameManager.gm.GulleDegistir(seciligulleid);
                    GameManager.gm.SaldiriDurdur();
                    GameManager.gm.hedefgemi = hit.collider.gameObject;
                    GameManager.gm.hedefgemi.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(true);
                }
                else if (hit.collider.gameObject.transform.CompareTag("Tower"))
                {
                    GameManager.gm.GulleDegistir(seciligulleid);
                    GameManager.gm.SaldiriDurdur();
                    GameManager.gm.hedefgemi = hit.collider.gameObject;
                    GameManager.gm.hedefgemi.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(true);
                }
                else if (hit.collider.gameObject.transform.CompareTag("EtkinlikGemisi"))
                {
                    GameManager.gm.GulleDegistir(seciligulleid);
                    GameManager.gm.SaldiriDurdur();
                    GameManager.gm.hedefgemi = hit.collider.gameObject;
                    GameManager.gm.hedefgemi.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(true);
                }
                else if (hit.collider.gameObject.transform.CompareTag("EtkinlikBossu"))
                {
                    GameManager.gm.GulleDegistir(seciligulleid);
                    GameManager.gm.SaldiriDurdur();
                    GameManager.gm.hedefgemi = hit.collider.gameObject;
                    GameManager.gm.hedefgemi.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(true);
                }
                else if (hit.collider.gameObject.transform.CompareTag("DenizYaratigi"))
                {
                    GameManager.gm.ZipkinDegistir(seciliZipkinId);
                    GameManager.gm.SaldiriDurdur();
                    GameManager.gm.hedefgemi = hit.collider.gameObject;
                    GameManager.gm.hedefgemi.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(true);
                }
                else if (hit.collider.gameObject.transform.CompareTag("Oyuncu"))
                {
                    GameManager.gm.GulleDegistir(seciligulleid);
                    GameManager.gm.SaldiriDurdur();
                    GameManager.gm.hedefgemi = hit.collider.gameObject;
                    GameManager.gm.BenSaldýrýyorumCanText.text = GameManager.gm.hedefgemi.GetComponent<Player>().Can + "/" + GameManager.gm.hedefgemi.GetComponent<Player>().Can;
                    // oyuncu cemberini kirmizi yapar
                    GameManager.gm.hedefgemi.transform.Find("OyuncuCanvas/Cember").gameObject.GetComponent<Image>().color = Color.yellow;
                    GameManager.gm.hedefgemi.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(true);
                }
                else if (hit.collider.gameObject.transform.CompareTag("Sandik"))
                {
                    target = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, -10.08f);
                    GameManager.gm.Cursor.transform.position = new Vector3(target.x, target.y, 0);
                }
            }
            else if (hit.collider != null && (hit.collider.gameObject.transform.CompareTag("AdaOfisi") && hit.collider.gameObject.name != oyuncuadi))
            {
                GameManager.gm.FiloOfisi.SetActive(true);
                AdaninKulelerininBilgileriniCekSunucu();
            }
            else
            {
                // gitmek istenilen konum target e atanýr
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // cursor gitmek istenilen konuma kaydýrýlýr
                GameManager.gm.Cursor.transform.position = new Vector3(target.x, target.y, 0);
                GameManager.gm.Cursor.SetActive(true);
                StartCoroutine(CursorKapa());

            }
            Sunucugemigezdir(target);
            GemiHareketEttir(target, target);
        }
    }
    [Command]
    public void Sunucugemigezdir(Vector3 hit)
    {
#if UNITY_SERVER || UNITY_EDITOR
        target = hit;
        GemiHareketEttir(hit, hit);
        sonaktiflikzamani = Time.time;
#endif
    }
    IEnumerator CursorKapa()
    {
        if (GameManager.gm.Cursor.activeSelf)
        {
            yield return new WaitForSeconds(0.1f);
            GameManager.gm.Cursor.SetActive(false);
        }
    }

    public void GemiHareketEttir(Vector3 oldvalue, Vector3 newvalue)
    {
        // gemi gitmek istenilen konuma hareket etmeye baþlar
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<NavMeshAgent>().SetDestination(new Vector3(newvalue.x, newvalue.y, 0));
    }

    public void SyncOyuncuAdiYazdir(string oldvalue, string newvalue)
    {
        gameObject.name = oyuncuadi;
        if (!isServerOnly)
        {
            /*if (!isLocalPlayer)
            {
                transform.Find("Gemi").GetComponent<SpriteRenderer>().color = Color.red;
            }*/
            if (OyuncuFiloKisaltma.Length > 0)
            {
                if (GameManager.gm.BenimGemim.GetComponent<Player>().dostFilolarKisaAdList.Contains(OyuncuFiloKisaltma))
                {
                    gameObject.transform.Find("OyuncuAdi").GetComponent<TextMeshPro>().text = "<sprite name=" + RutbeId + ">" + "<color=#00e3db>[" + OyuncuFiloKisaltma + "]</color>" + oyuncuadi;
                }
                else if (GameManager.gm.BenimGemim.GetComponent<Player>().dusmanFilolarKisaAdList.Contains(OyuncuFiloKisaltma))
                {
                    gameObject.transform.Find("OyuncuAdi").GetComponent<TextMeshPro>().text = "<sprite name=" + RutbeId + ">" + "<color=#ff0000>[" + OyuncuFiloKisaltma + "]</color>" + oyuncuadi;
                }
                else
                {
                    gameObject.transform.Find("OyuncuAdi").GetComponent<TextMeshPro>().text = "<sprite name=" + RutbeId + ">" + "<color=#ffff00>[" + OyuncuFiloKisaltma + "]</color>" + oyuncuadi;
                }
            }
            else
            {
                gameObject.transform.Find("OyuncuAdi").GetComponent<TextMeshPro>().text = "<sprite name=" + RutbeId + ">" + oyuncuadi;
            }


            gameObject.transform.Find("OyuncuID").GetComponent<TextMeshPro>().text = ((int)(oyuncuId)).ToString();

        }
    }

    public void SetOyuncuRutbePuan(int deger)
    {
        oyuncuRutbePuan = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuRutbePuan(oyuncuRutbePuan);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.mevcutRutbePuanText.text = "Mevcut Rutbe Puaný: " + oyuncuRutbePuan;
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuRutbePuan(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuRutbePuan(donenVeri);
        }
    }

    public void SetOyuncuTerubePuani(int deger)
    {
        oyuncuTecrubePuan = deger;
        seviye = GameManager.gm.OyuncuSeviyesiHesapla(oyuncuTecrubePuan);
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuTerubePuani(oyuncuTecrubePuan);
        }
#endif
    }
    [TargetRpc]
    public void TargetSetOyuncuTerubePuani(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuTerubePuani(donenVeri);
            if (donenVeri > 8000)
            {
                GameManager.gm.GirButtonPVP.SetActive(true);
                GameManager.gm.GirButtonPVE.SetActive(true);
            }
        }
    }

    public void SetElitPuani(int deger)
    {
        oyuncuElitPuan = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetElitPuani(oyuncuElitPuan);
        }
#endif
        // Oyuncu Elit puanýný yazdýrýr
        if (isLocalPlayer)
        {
            GameManager.gm.ProfilElitPuani.text = oyuncuElitPuan.ToString();
            GameManager.gm.marketElitPuani.text = oyuncuElitPuan.ToString();
        }
    }
    [TargetRpc]
    public void TargetSetElitPuani(int donenElitPuan)
    {
        if (!isServer)
        {
            SetElitPuani(donenElitPuan);
        }
    }

    public void SetEtkinlikAktiflikDurumu(bool deger)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetEtkinlikAktiflikDurumu(GameManager.gm.EtkinlikAktiflikDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.EtkinlikAktiflikDurumu = deger;
            if (deger == false)
            {
#if UNITY_ANDROID
                GameManager.gm.EtkinlikSayaciAndroid.gameObject.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
                GameManager.gm.EtkinlikSayaciWin.gameObject.SetActive(false);
#endif
            }
        }
    }
    [TargetRpc]
    public void TargetSetEtkinlikAktiflikDurumu(bool donenDeger)
    {
        if (!isServer)
        {
            SetEtkinlikAktiflikDurumu(donenDeger);
        }
    }

    public void SetOyuncuSifaGullesi(int deger)
    {
        oyuncuSifaGulle = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuSifaGullesi(oyuncuSifaGulle);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot4TXT.text = GameManager.gm.SayiKisaltici(oyuncuSifaGulle);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsGulleSlot3TXT.text = GameManager.gm.SayiKisaltici(oyuncuSifaGulle);
#endif
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuSifaGullesi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuSifaGullesi(donenVeri);
        }
    }



    public void SetOyuncuHallowenGullesi(int deger)
    {
        OyuncuHallowenGulle = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHallowenGullesi(OyuncuHallowenGulle);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
           GameManager.gm.GulleSlot9TXT.text = GameManager.gm.SayiKisaltici(OyuncuHallowenGulle);
#endif
#if UNITY_STANDALONE_WIN
           GameManager.gm.WindowsGulleSlot6TXT.text = GameManager.gm.SayiKisaltici(OyuncuHallowenGulle);
#endif
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuHallowenGullesi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHallowenGullesi(donenVeri);
        }
    }

    public void SetOyuncuNoelGullesi(int deger)
    {
        OyuncuNoelGullesi = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuNoelnGullesi(OyuncuNoelGullesi);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
           GameManager.gm.GulleSlot11TXT.text = GameManager.gm.SayiKisaltici(OyuncuNoelGullesi);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsGulleSlot7TXT.text = GameManager.gm.SayiKisaltici(OyuncuNoelGullesi);
#endif
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuNoelnGullesi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuNoelGullesi(donenVeri);
        }
    }



    public void SetOyuncuKalpliGulle(int deger)
    {
        KalpliGulle = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuKalpliGulle(KalpliGulle);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
           GameManager.gm.GulleSlot14TXT.text = GameManager.gm.SayiKisaltici(KalpliGulle);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsGulleSlot8TXT.text = GameManager.gm.SayiKisaltici(KalpliGulle);
#endif
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuKalpliGulle(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuKalpliGulle(donenVeri);
        }
    }




    public void SetOyuncuDemirGullesi(int deger)
    {
        oyuncuDemirGulle = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuDemirGullesi(oyuncuDemirGulle);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot2TXT.text = GameManager.gm.SayiKisaltici(oyuncuDemirGulle);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsGulleSlot2TXT.text = GameManager.gm.SayiKisaltici(oyuncuDemirGulle);
#endif
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuDemirGullesi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuDemirGullesi(donenVeri);
        }
    }

    public void SetOyuncuAlevGullesi(int deger)
    {
        oyuncuAlevGulle = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuAlevGullesi(oyuncuAlevGulle);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot3TXT.text = GameManager.gm.SayiKisaltici(oyuncuAlevGulle);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsGulleSlot4TXT.text = GameManager.gm.SayiKisaltici(oyuncuAlevGulle);
#endif
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuAlevGullesi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuAlevGullesi(donenVeri);
        }
    }

    public void SetOyuncuHavaiFisek(int deger)
    {
        oyuncuHavaiFisekGulle = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHavaiFisek(oyuncuHavaiFisekGulle);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot5TXT.text = GameManager.gm.SayiKisaltici(oyuncuHavaiFisekGulle);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsGulleSlot5TXT.text = GameManager.gm.SayiKisaltici(oyuncuHavaiFisekGulle);
#endif
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuHavaiFisek(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHavaiFisek(donenVeri);
        }
    }

    public void SetOyuncuRoket(int deger)
    {
        oyuncuRoket = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuRoket(oyuncuRoket);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.RoketText.text = GameManager.gm.SayiKisaltici(oyuncuRoket);
            GameManager.gm.WindowsRoketText.text = GameManager.gm.SayiKisaltici(oyuncuRoket);
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuRoket(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuRoket(donenVeri);
        }
    }


    public void SetOyuncuTamir(int deger)
    {
        oyuncuTamir = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuTamir(oyuncuTamir);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.TamirText.text = GameManager.gm.SayiKisaltici(oyuncuTamir);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.winTamirText.text = GameManager.gm.SayiKisaltici(oyuncuTamir);
#endif
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuTamir(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuTamir(donenVeri);
        }
    }

    public void SetOyuncuBarut(int deger)
    {
        oyuncuBarut = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuBarut(oyuncuBarut);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.BarutText.text = GameManager.gm.SayiKisaltici(oyuncuBarut);
            GameManager.gm.WindowsBarutText.text = GameManager.gm.SayiKisaltici(oyuncuBarut);

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuBarut(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuBarut(donenVeri);
        }
    }

    public void SetOyuncuKalkan(int deger)
    {
        oyuncuKalkan = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuKalkan(oyuncuKalkan);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.KalkanText.text = GameManager.gm.SayiKisaltici(oyuncuKalkan);
            GameManager.gm.WindowsKalkanText.text = GameManager.gm.SayiKisaltici(oyuncuKalkan);

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuKalkan(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuKalkan(donenVeri);
        }
    }

    public void SetOyuncuAltin(int deger)
    {
        oyuncuAltin = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuAltin(oyuncuAltin);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.AltinText.text = GameManager.gm.SayiKisaltici(oyuncuAltin);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinAltinTXT.text = GameManager.gm.SayiKisaltici(oyuncuAltin);
#endif
            GameManager.gm.marketAltýn.text = oyuncuAltin.ToString();

        }
    }
    [TargetRpc]
    public void TargetSetOyuncuAltin(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuAltin(donenVeri);
        }
    }

 


    public void SetOyuncuEtkinlikPuani(int deger)
    {
        OyuncuEtkinlikPuani = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuEtkinlikPuani(OyuncuEtkinlikPuani);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.OyuncuEtkinlikPuaniSlider.value = OyuncuEtkinlikPuani;
            if (GameManager.gm.OyuncuEtkinlikPuaniSlider.value == 17)
            {
                GameManager.gm.EtkinlikYildiz1.GetComponent<Image>().color = new Color(255,255,255);
            }
            if (GameManager.gm.OyuncuEtkinlikPuaniSlider.value == 71)
            {
                GameManager.gm.EtkinlikYildiz2.GetComponent<Image>().color = new Color(255, 255, 255);
            }
            if (GameManager.gm.OyuncuEtkinlikPuaniSlider.value == 117)
            {
                GameManager.gm.EtkinlikYildiz3.GetComponent<Image>().color = new Color(255, 255, 255);
            }
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuEtkinlikPuani(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuEtkinlikPuani(donenVeri);
        }
    }





























    public void SetOyuncuSavasPuani(int deger)
    {
        oyuncuSavasPuani = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuSavasPuani(oyuncuSavasPuani);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.SavasPuani.text = oyuncuSavasPuani.ToString();
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuSavasPuani(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuSavasPuani(donenVeri);
        }
    }

    public void SetOyuncuLostCoin(int deger)
    {
        oyuncuLostCoin = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuLostCoin(oyuncuLostCoin);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.Lostcoin.text = oyuncuLostCoin.ToString();
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuLostCoin(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuLostCoin(donenVeri);
        }
    }
    public void SetOyuncuPremiumDurumu(int deger)
    {
        OyuncuPremiumDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuPremiumDurumu(OyuncuPremiumDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (OyuncuPremiumDurumu == 1)
            {
                GameManager.gm.AndroidUstPanelVipIcon.SetActive(true);
                GameManager.gm.WindowsUstPanelVipIcon.SetActive(true);
                GameManager.gm.PremiumPaketEnebled.SetActive(true);

            }
            else
            {
                GameManager.gm.AndroidUstPanelVipIcon.SetActive(false);
                GameManager.gm.WindowsUstPanelVipIcon.SetActive(false);
            }
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuPremiumDurumu(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuPremiumDurumu(donenVeri);
        }
    }

    public void SetOyuncuSandikKatlamaDurumu(int deger)
    {
        OyuncuSandikKatlamaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuSandikKatlamaDurumu(OyuncuSandikKatlamaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (OyuncuSandikKatlamaDurumu == 1)
            {
                GameManager.gm.AndroidUstPanelSandikKatlamaIcon.SetActive(true);
                GameManager.gm.WindowsUstPanelSandikKatlamaIcon.SetActive(true);
                GameManager.gm.SandikKatlamaEnebled.SetActive(true);

            }
            else
            {
                GameManager.gm.AndroidUstPanelSandikKatlamaIcon.SetActive(false);
                GameManager.gm.WindowsUstPanelSandikKatlamaIcon.SetActive(false);
            }
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuSandikKatlamaDurumu(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuSandikKatlamaDurumu(donenVeri);
        }
    }



    public void SetOyuncuTpKatlamaDurumu(int deger)
    {
        OyuncuTpKatlamaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuTpKatlamaDurumu(OyuncuTpKatlamaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (OyuncuTpKatlamaDurumu == 1)
            {
                GameManager.gm.AndroidUstPanelTpBossIcon.SetActive(true);
                GameManager.gm.WindowsUstPanelTpBossIcon.SetActive(true);
                GameManager.gm.XpBossEnebled.SetActive(true);

            }
            else
            {
                GameManager.gm.AndroidUstPanelTpBossIcon.SetActive(false);
                GameManager.gm.WindowsUstPanelTpBossIcon.SetActive(false);
            }
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuTpKatlamaDurumu(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuTpKatlamaDurumu(donenVeri);
        }
    }

    public void SetOyuncuAltinKatlamaDurumu(int deger)
    {
        OyuncuAltinKatlamaDurumu = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuAltinKatlamaDurumu(OyuncuAltinKatlamaDurumu);
        }


#endif
        if (isLocalPlayer)
        {
            if (OyuncuAltinKatlamaDurumu == 1)
            {
                GameManager.gm.AndroidUstPanelAltinBossIcon.SetActive(true);
                GameManager.gm.WindowsUstPanelAltinBossIcon.SetActive(true);
                GameManager.gm.AltýnBossPaketEnebled.SetActive(true);
            }
            else
            {
                GameManager.gm.AndroidUstPanelAltinBossIcon.SetActive(false);
                GameManager.gm.WindowsUstPanelAltinBossIcon.SetActive(false);
            }
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuAltinKatlamaDurumu(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuAltinKatlamaDurumu(donenVeri);
        }
    }

    public void SetOyuncuAdaBonuslari(float deger)
    {
        adaBonuslari = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuAdaBonuslari(adaBonuslari);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.AdaEkstraBonusu.text = "Ada Ekstra Tp : % " + (int)Mathf.Round(adaBonuslari * 100) + "\n" + "Ada Ekstra Altin : % " + (int)Mathf.Round(adaBonuslari * 100);
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuAdaBonuslari(float donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuAdaBonuslari(donenVeri);
        }
    }

    public void SetBaslangicPaket2Durumu(int deger)
    {
        BaslangicPaket2Durumu = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetBaslangicPaket2Durumu(BaslangicPaket2Durumu);
        }


#endif
        if (isLocalPlayer)
        {
            if (BaslangicPaket2Durumu == 1)
            {
                GameManager.gm.Paket2Enebled.SetActive(true);
            }
        }
    }
    [TargetRpc]
    public void TargetBaslangicPaket2Durumu(int donenVeri)
    {
        if (!isServer)
        {
            SetBaslangicPaket2Durumu(donenVeri);
        }
    }

    public void SetOyuncuHiztasi(int deger)
    {
        oyuncuHizTasi = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHiztasi(oyuncuHizTasi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HizTasiText.text = GameManager.gm.SayiKisaltici(oyuncuHizTasi);
            GameManager.gm.WindowsHizTasiText.text = GameManager.gm.SayiKisaltici(oyuncuHizTasi);
        }
    }


    [TargetRpc]
    public void TargetSetOyuncuHiztasi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHiztasi(donenVeri);
        }
    }

    public void SetOyuncuPaslanmisZipkin(int deger)
    {
        oyuncuPaslanmisZipkin = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuPaslanmisZipkin(oyuncuPaslanmisZipkin);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot6TXT.text = GameManager.gm.SayiKisaltici(oyuncuPaslanmisZipkin);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsZipkinSlot1TXT.text = GameManager.gm.SayiKisaltici(oyuncuPaslanmisZipkin);
#endif
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuPaslanmisZipkin(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuPaslanmisZipkin(donenVeri);
        }
    }

    public void SetOyuncuGumusZipkin(int deger)
    {
        oyuncuGumusZipkin = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuGumusZipkin(oyuncuGumusZipkin);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot7TXT.text = GameManager.gm.SayiKisaltici(oyuncuGumusZipkin);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsZipkinSlot2TXT.text = GameManager.gm.SayiKisaltici(oyuncuGumusZipkin);
#endif
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuGumusZipkin(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuGumusZipkin(donenVeri);
        }
    }

    public void SetOyuncuAltinZipkin(int deger)
    {
        oyuncuAltinZipkin = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuAltinZipkin(oyuncuAltinZipkin);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot8TXT.text = GameManager.gm.SayiKisaltici(oyuncuAltinZipkin);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsZipkinSlot3TXT.text = GameManager.gm.SayiKisaltici(oyuncuAltinZipkin);
#endif
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuAltinZipkin(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuAltinZipkin(donenVeri);
        }
    }





    public void SetOyuncuHallowenZipkin(int deger)
    {
        OyuncuHallowenZipkin = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHallowenZipkin(OyuncuHallowenZipkin);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot10TXT.text = GameManager.gm.SayiKisaltici(OyuncuHallowenZipkin);
#endif
#if UNITY_STANDALONE_WIN
           GameManager.gm.WindowsZipkinSlot4TXT.text = GameManager.gm.SayiKisaltici(OyuncuHallowenZipkin);
#endif
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHallowenZipkin(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHallowenZipkin(donenVeri);
        }
    }



    public void SetOyuncuNoelZipkin(int deger)
    {
        OyuncuNoelZipkin = deger;
        
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuNoelZipkin(OyuncuNoelZipkin);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot12TXT.text = GameManager.gm.SayiKisaltici(OyuncuNoelZipkin);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsZipkinSlot5TXT.text = GameManager.gm.SayiKisaltici(OyuncuNoelZipkin);
#endif
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuNoelZipkin(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuNoelZipkin(donenVeri);
        }
    }

    public void SetOyuncuKalpliZipkin(int deger)
    {
        KalpliZipkin = deger;

#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetSetOyuncuKalpliZipkin(KalpliZipkin);
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.GulleSlot13TXT.text = GameManager.gm.SayiKisaltici(KalpliZipkin);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WindowsZipkinSlot6TXT.text = GameManager.gm.SayiKisaltici(KalpliZipkin);
#endif
        }
    }
    [TargetRpc]
    public void TargetSetSetOyuncuKalpliZipkin(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuKalpliZipkin(donenVeri);
        }
    }



    public void SetOyuncuHaritaBirOnNpcKesmeGorevi(int deger)
    {
        haritaBirOnNpcKesGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaBirOnNpcKesmeGorevi(haritaBirOnNpcKesGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaBirOnNpcKesmeGoreviText.text = deger + " / 10";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaBirOnNpcKesmeGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaBirOnNpcKesmeGorevi(donenVeri);
        }
    }


    public void SetOyuncuHaritaBirBesSandikToplamaGorevi(int deger)
    {
        haritaBirBesSandikToplaGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaBirBesSandikToplamaGorevi(haritaBirBesSandikToplaGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaBirBesSandikToplamaGoreviText.text = deger + " / 5";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaBirBesSandikToplamaGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaBirBesSandikToplamaGorevi(donenVeri);
        }
    }




    public void SetOyuncuHaritaIkiOnNpcKesmeGorevi(int deger)
    {
        haritaIkiOnNpcKesGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaIkiOnNpcKesmeGorevi(haritaIkiOnNpcKesGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaIkiOnNpcKesmeGoreviText.text = deger + " / 10";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaIkiOnNpcKesmeGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaIkiOnNpcKesmeGorevi(donenVeri);
        }
    }


    public void SetOyuncuHaritaIkiOnSandikToplamaGorevi(int deger)
    {
        HaritaIkiOnSandikToplaSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaIkiOnSandikToplamaGorevi(HaritaIkiOnSandikToplaSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaIkiOnSandikToplamaGoreviText.text = deger + " / 10";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaIkiOnSandikToplamaGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaIkiOnSandikToplamaGorevi(donenVeri);
        }
    }

    public void SetOyuncuHaritaIkiBesNpcCanavarKesGorevi(int deger)
    {
        haritaIkýBesNpcCanavarKesGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaIkiBesNpcCanavarKesGorevi(haritaBesOnSekizNpcCanavarKesGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaIkiBesNpcCanavarKesGoreviText.text = deger + " / 5";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaIkiBesNpcCanavarKesGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaIkiBesNpcCanavarKesGorevi(donenVeri);
        }
    }













    public void SetOyuncuHaritaUcOnBesNpcKesmeGorevi(int deger)
    {
        haritaUcOnBesNpcKesGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaUcOnBesNpcKesmeGorevi(haritaUcOnBesNpcKesGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaUcOnBesNpcKesmeGoreviText.text = deger + " / 15";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaUcOnBesNpcKesmeGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaUcOnBesNpcKesmeGorevi(donenVeri);
        }
    }


    public void SetOyuncuHaritaUcYirmiSandikToplamaGorevi(int deger)
    {
        HaritaUcYirmiSandikToplaSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaUcYirmiSandikToplamaGorevi(HaritaUcYirmiSandikToplaSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaUcYirmiSandikToplamaGoreviText.text = deger + " / 20";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaUcYirmiSandikToplamaGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaUcYirmiSandikToplamaGorevi(donenVeri);
        }
    }

    public void SetOyuncuHaritaUcSekizNpcCanavarKesGorevi(int deger)
    {
        haritaUcSekizNpcCanavarKesGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaUcSekizNpcCanavarKesGorevi(haritaUcSekizNpcCanavarKesGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaUcSekizNpcCanavarKesGoreviText.text = deger + " / 8";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaUcSekizNpcCanavarKesGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaUcSekizNpcCanavarKesGorevi(donenVeri);
        }
    }











    public void SetOyuncuHaritaDortYirmiNpcKesmeGorevi(int deger)
    {
        haritaDortYirmiNpcKesGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaDortYirmiNpcKesmeGorevi(haritaDortYirmiNpcKesGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaDortYirmiNpcKesmeGoreviText.text = deger + " / 20";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaDortYirmiNpcKesmeGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaDortYirmiNpcKesmeGorevi(donenVeri);
        }
    }


    public void SetOyuncuHaritaDortKirkSandikToplamaGorevi(int deger)
    {
        HaritaDortKirkSandikToplaSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaDortKirkSandikToplamaGorevi(HaritaDortKirkSandikToplaSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaDortKirkSandikToplamaGoreviText.text = deger + " / 40";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaDortKirkSandikToplamaGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaDortKirkSandikToplamaGorevi(donenVeri);
        }
    }

    public void SetOyuncuHaritaDortOnikiNpcCanavarKesGorevi(int deger)
    {
        haritaDortOnIkiNpcCanavarKesGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaDortOnikiNpcCanavarKesGorevi(haritaDortOnIkiNpcCanavarKesGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaDortOnIkiNpcCanavarKesGoreviText.text = deger + " / 12";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaDortOnikiNpcCanavarKesGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaDortOnikiNpcCanavarKesGorevi(donenVeri);
        }
    }









    public void SetOyuncuHaritaBesOtuzNpcKesmeGorevi(int deger)
    {
        haritaBesOtuzNpcKesGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaBesOtuzNpcKesmeGorevi(haritaBesOtuzNpcKesGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaBesOtuzNpcKesmeGoreviText.text = deger + " / 30";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaBesOtuzNpcKesmeGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaBesOtuzNpcKesmeGorevi(donenVeri);
        }
    }


    public void SetOyuncuHaritaBesAltmisSandikToplamaGorevi(int deger)
    {
        HaritaBesAtmisSandikToplaSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaBesAltmisSandikToplamaGorevi(HaritaBesAtmisSandikToplaSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaBesAtmisSandikToplamaGoreviText.text = deger + " / 60";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaBesAltmisSandikToplamaGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaBesAltmisSandikToplamaGorevi(donenVeri);
        }
    }

    public void SetOyuncuHaritaBesOnSekizNpcCanavarKesGorevi(int deger)
    {
        haritaDortOnIkiNpcCanavarKesGoreviSayac = deger;


#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHaritaBesOnSekizNpcCanavarKesGorevi(haritaDortOnIkiNpcCanavarKesGoreviSayac);
        }


#endif
        if (isLocalPlayer)
        {
            GameManager.gm.HaritaBesOnSekizNpcCanavarKesGoreviText.text = deger + " / 18";
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuHaritaBesOnSekizNpcCanavarKesGorevi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHaritaBesOnSekizNpcCanavarKesGorevi(donenVeri);
        }
    }














    public void GemiAnimasyonu()
    {
        if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite == null)
        {
            transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + Random.Range(0, 31)];
        }
        if (hizTasiKullanildiAnimasyonFrameGecikmesi == 1)
        {
            if (GemiAnimasyonuSonGirilenId != -1 && GameManager.gm.BenimGemim != null && GameManager.gm.BenimGemim.GetComponent<Player>().OyunaGirisYapildimi)
            {
                // sað
                if (GemiAnimasyonuSonGirilenId == 8 && myAgent.desiredVelocity.x > maksHiz * 0.9f && myAgent.desiredVelocity.y <= maksHiz * 0.15f && myAgent.desiredVelocity.y >= -maksHiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 8])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 8];
                    }
                    GemiAnimasyonuSonGirilenId = 8;
                }
                // alt
                else if (GemiAnimasyonuSonGirilenId == 16 && myAgent.desiredVelocity.y < -maksHiz * 0.9f && myAgent.desiredVelocity.x <= maksHiz * 0.15f && myAgent.desiredVelocity.x >= -maksHiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 16])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 16];
                    }
                    GemiAnimasyonuSonGirilenId = 16;
                }
                //sol
                else if (GemiAnimasyonuSonGirilenId == 24 && myAgent.desiredVelocity.x < -maksHiz * 0.9f && myAgent.desiredVelocity.y <= maksHiz * 0.15f && myAgent.desiredVelocity.y >= -maksHiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 24])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 24];
                    }
                    GemiAnimasyonuSonGirilenId = 24;
                }
                // üst
                if (GemiAnimasyonuSonGirilenId == 0 && myAgent.desiredVelocity.y > maksHiz * 0.9f && myAgent.desiredVelocity.x <= maksHiz * 0.15f && myAgent.desiredVelocity.x >= -maksHiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 0])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 0];
                    }
                    GemiAnimasyonuSonGirilenId = 0;
                }
                // sað üst 85
                else if (GemiAnimasyonuSonGirilenId == 1 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > maksHiz * 0.85f && myAgent.desiredVelocity.y < maksHiz * 1f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 1])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 1];
                    }
                    GemiAnimasyonuSonGirilenId = 1;
                }
                // sað üst 70
                else if (GemiAnimasyonuSonGirilenId == 2 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > maksHiz * 0.70f && myAgent.desiredVelocity.y < maksHiz * 0.90f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 2])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 2];
                    }
                    GemiAnimasyonuSonGirilenId = 2;
                }
                // sað üst 55
                else if (GemiAnimasyonuSonGirilenId == 3 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > maksHiz * 0.55f && myAgent.desiredVelocity.y < maksHiz * 0.75f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 3])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 3];
                    }
                    GemiAnimasyonuSonGirilenId = 3;
                }
                // sað üst 47
                else if (GemiAnimasyonuSonGirilenId == 4 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > maksHiz * 0.47f && myAgent.desiredVelocity.y < maksHiz * 0.60f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 4])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 4];
                    }
                    GemiAnimasyonuSonGirilenId = 4;
                }
                // sað üst 40
                else if (GemiAnimasyonuSonGirilenId == 5 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > maksHiz * 0.40f && myAgent.desiredVelocity.y < maksHiz * 0.52f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 5])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 5];
                    }
                    GemiAnimasyonuSonGirilenId = 5;
                }
                // sað üst 25
                else if (GemiAnimasyonuSonGirilenId == 6 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > maksHiz * 0.25f && myAgent.desiredVelocity.y < maksHiz * 0.45f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 6])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 6];
                    }
                    GemiAnimasyonuSonGirilenId = 6;
                }
                // sað üst 10
                else if (GemiAnimasyonuSonGirilenId == 7 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > maksHiz * 0.10f && myAgent.desiredVelocity.y < maksHiz * 0.30f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 7])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 7];
                    }
                    GemiAnimasyonuSonGirilenId = 7;
                }
                // sað alt 85
                else if (GemiAnimasyonuSonGirilenId == 15 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > -maksHiz * 1f && myAgent.desiredVelocity.y < -maksHiz * 0.85f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 15])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 15];
                    }
                    GemiAnimasyonuSonGirilenId = 15;
                }
                // sað alt 70
                else if (GemiAnimasyonuSonGirilenId == 14 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > -maksHiz * 0.90f && myAgent.desiredVelocity.y < -maksHiz * 0.70f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 14])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 14];
                    }
                    GemiAnimasyonuSonGirilenId = 14;
                }
                // sað alt 55
                else if (GemiAnimasyonuSonGirilenId == 12 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > -maksHiz * 0.75f && myAgent.desiredVelocity.y < -maksHiz * 0.55f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 12])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 12];
                    }
                    GemiAnimasyonuSonGirilenId = 12;
                }
                // sað alt 47
                else if (GemiAnimasyonuSonGirilenId == 13 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > -maksHiz * 0.60 && myAgent.desiredVelocity.y < -maksHiz * 0.47f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 13])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 13];
                    }
                    GemiAnimasyonuSonGirilenId = 13;
                }
                // sað alt 40
                else if (GemiAnimasyonuSonGirilenId == 11 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > -maksHiz * 0.52f && myAgent.desiredVelocity.y < -maksHiz * 0.40f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 11])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 11];
                    }
                    GemiAnimasyonuSonGirilenId = 11;
                }
                // sað alt 25
                else if (GemiAnimasyonuSonGirilenId == 10 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > -maksHiz * 0.45f && myAgent.desiredVelocity.y < -maksHiz * 0.25f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 10])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 10];
                    }
                    GemiAnimasyonuSonGirilenId = 10;
                }
                // sað alt 10
                else if (GemiAnimasyonuSonGirilenId == 9 && myAgent.desiredVelocity.x > maksHiz * 0.15f && myAgent.desiredVelocity.x < maksHiz * 1f && myAgent.desiredVelocity.y > -maksHiz * 0.30f && myAgent.desiredVelocity.y < -maksHiz * 0.10f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 9])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 9];
                    }
                    GemiAnimasyonuSonGirilenId = 9;
                }
                // sol alt 85
                else if (GemiAnimasyonuSonGirilenId == 17 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > -maksHiz * 1f && myAgent.desiredVelocity.y < -maksHiz * 0.85f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 17])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 17];
                    }
                    GemiAnimasyonuSonGirilenId = 17;
                }
                // sol alt 70
                else if (GemiAnimasyonuSonGirilenId == 18 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > -maksHiz * 0.90f && myAgent.desiredVelocity.y < -maksHiz * 0.70f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 18])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 18];
                    }
                    GemiAnimasyonuSonGirilenId = 18;
                }
                // sol alt 55
                else if (GemiAnimasyonuSonGirilenId == 19 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > -maksHiz * 0.75f && myAgent.desiredVelocity.y < -maksHiz * 0.55f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 19])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 19];
                    }
                    GemiAnimasyonuSonGirilenId = 19;
                }
                // sol alt 47
                else if (GemiAnimasyonuSonGirilenId == 20 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > -maksHiz * 0.60f && myAgent.desiredVelocity.y < -maksHiz * 0.47f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 20])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 20];
                    }
                    GemiAnimasyonuSonGirilenId = 20;
                }
                // sol alt 40
                else if (GemiAnimasyonuSonGirilenId == 21 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > -maksHiz * 0.52f && myAgent.desiredVelocity.y < -maksHiz * 0.40f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 21])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 21];
                    }
                    GemiAnimasyonuSonGirilenId = 21;
                }
                // sol alt 25
                else if (GemiAnimasyonuSonGirilenId == 22 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > -maksHiz * 0.45f && myAgent.desiredVelocity.y < -maksHiz * 0.25f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 22])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 22];
                    }
                    GemiAnimasyonuSonGirilenId = 22;
                }
                // sol alt 10
                else if (GemiAnimasyonuSonGirilenId == 23 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > -maksHiz * 0.30f && myAgent.desiredVelocity.y < -maksHiz * 0.10f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 23])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 23];
                    }
                    GemiAnimasyonuSonGirilenId = 23;
                }
                // sol üst 10
                else if (GemiAnimasyonuSonGirilenId == 25 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.10f && myAgent.desiredVelocity.y < maksHiz * 0.30f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 25])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 25];
                    }

                    GemiAnimasyonuSonGirilenId = 25;
                }
                // sol üst 25
                else if (GemiAnimasyonuSonGirilenId == 26 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.25f && myAgent.desiredVelocity.y < maksHiz * 0.45f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 26])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 26];
                    }
                    GemiAnimasyonuSonGirilenId = 26;
                }
                // sol üst 40
                else if (GemiAnimasyonuSonGirilenId == 27 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.40f && myAgent.desiredVelocity.y < maksHiz * 0.52f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 27])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 27];
                    }
                    GemiAnimasyonuSonGirilenId = 27;
                }
                // sol üst 47
                else if (GemiAnimasyonuSonGirilenId == 28 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.47f && myAgent.desiredVelocity.y < maksHiz * 0.60f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 28])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 28];
                    }
                    GemiAnimasyonuSonGirilenId = 28;
                }
                // sol üst 55
                else if (GemiAnimasyonuSonGirilenId == 29 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.55f && myAgent.desiredVelocity.y < maksHiz * 0.75f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 29])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 29];
                    }
                    GemiAnimasyonuSonGirilenId = 29;
                }
                // sol üst 70
                else if (GemiAnimasyonuSonGirilenId == 30 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.70f && myAgent.desiredVelocity.y < maksHiz * 0.90f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 30])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 30];
                    }
                    GemiAnimasyonuSonGirilenId = 30;
                }
                // sol üst 85
                else if (GemiAnimasyonuSonGirilenId == 31 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.85f && myAgent.desiredVelocity.y < maksHiz * 1f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 31])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 31];
                    }
                    GemiAnimasyonuSonGirilenId = 31;
                } // sol üst 10
                else if (GemiAnimasyonuSonGirilenId == 25 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.2f && myAgent.desiredVelocity.y > maksHiz * 0.10f && myAgent.desiredVelocity.y < maksHiz * 0.30f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 25])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 25];
                    }

                    GemiAnimasyonuSonGirilenId = 25;
                }
                // sol üst 25
                else if (GemiAnimasyonuSonGirilenId == 26 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.25f && myAgent.desiredVelocity.y < maksHiz * 0.45f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 26])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 26];
                    }
                    GemiAnimasyonuSonGirilenId = 26;
                }
                // sol üst 40
                else if (GemiAnimasyonuSonGirilenId == 27 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.40f && myAgent.desiredVelocity.y < maksHiz * 0.52f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 27])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 27];
                    }
                    GemiAnimasyonuSonGirilenId = 27;
                }
                // sol üst 47
                else if (GemiAnimasyonuSonGirilenId == 28 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.47f && myAgent.desiredVelocity.y < maksHiz * 0.60f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 28])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 28];
                    }
                    GemiAnimasyonuSonGirilenId = 28;
                }
                // sol üst 55
                else if (GemiAnimasyonuSonGirilenId == 29 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.55f && myAgent.desiredVelocity.y < maksHiz * 0.75f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 29])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 29];
                    }
                    GemiAnimasyonuSonGirilenId = 29;
                }
                // sol üst 70
                else if (GemiAnimasyonuSonGirilenId == 30 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.70f && myAgent.desiredVelocity.y < maksHiz * 0.90f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 30])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 30];
                    }
                    GemiAnimasyonuSonGirilenId = 30;
                }
                // sol üst 85
                else if (GemiAnimasyonuSonGirilenId == 31 && myAgent.desiredVelocity.x > -maksHiz * 1f && myAgent.desiredVelocity.x < -maksHiz * 0.15f && myAgent.desiredVelocity.y > maksHiz * 0.85f && myAgent.desiredVelocity.y < maksHiz * 1f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 31])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 31];
                    }
                    GemiAnimasyonuSonGirilenId = 31;
                }
                else
                {
                    GemiAnimasyonuSonGirilenId = -1;
                }
            }
            hizTasiKullanildiAnimasyonFrameGecikmesi = 0;
        }
        else if (GemiAnimasyonuSonGirilenId != -1 && GameManager.gm.BenimGemim != null && GameManager.gm.BenimGemim.GetComponent<Player>().OyunaGirisYapildimi)
        {
            // sað
            if (GemiAnimasyonuSonGirilenId == 8 && myAgent.desiredVelocity.x > hiz * 0.9f && myAgent.desiredVelocity.y <= hiz * 0.15f && myAgent.desiredVelocity.y >= -hiz * 0.15f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 8])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 8];
                }
                GemiAnimasyonuSonGirilenId = 8;
            }
            // alt
            else if (GemiAnimasyonuSonGirilenId == 16 && myAgent.desiredVelocity.y < -hiz * 0.9f && myAgent.desiredVelocity.x <= hiz * 0.15f && myAgent.desiredVelocity.x >= -hiz * 0.15f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 16])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 16];
                }
                GemiAnimasyonuSonGirilenId = 16;
            }
            //sol
            else if (GemiAnimasyonuSonGirilenId == 24 && myAgent.desiredVelocity.x < -hiz * 0.9f && myAgent.desiredVelocity.y <= hiz * 0.15f && myAgent.desiredVelocity.y >= -hiz * 0.15f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 24])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 24];
                }
                GemiAnimasyonuSonGirilenId = 24;
            }
            // üst
            if (GemiAnimasyonuSonGirilenId == 0 && myAgent.desiredVelocity.y > hiz * 0.9f && myAgent.desiredVelocity.x <= hiz * 0.15f && myAgent.desiredVelocity.x >= -hiz * 0.15f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 0])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 0];
                }
                GemiAnimasyonuSonGirilenId = 0;
            }
            // sað üst 85
            else if (GemiAnimasyonuSonGirilenId == 1 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.85f && myAgent.desiredVelocity.y < hiz * 1f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 1])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 1];
                }
                GemiAnimasyonuSonGirilenId = 1;
            }
            // sað üst 70
            else if (GemiAnimasyonuSonGirilenId == 2 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.70f && myAgent.desiredVelocity.y < hiz * 0.90f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 2])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 2];
                }
                GemiAnimasyonuSonGirilenId = 2;
            }
            // sað üst 55
            else if (GemiAnimasyonuSonGirilenId == 3 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.55f && myAgent.desiredVelocity.y < hiz * 0.75f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 3])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 3];
                }
                GemiAnimasyonuSonGirilenId = 3;
            }
            // sað üst 47
            else if (GemiAnimasyonuSonGirilenId == 4 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.47f && myAgent.desiredVelocity.y < hiz * 0.60f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 4])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 4];
                }
                GemiAnimasyonuSonGirilenId = 4;
            }
            // sað üst 40
            else if (GemiAnimasyonuSonGirilenId == 5 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.40f && myAgent.desiredVelocity.y < hiz * 0.52f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 5])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 5];
                }
                GemiAnimasyonuSonGirilenId = 5;
            }
            // sað üst 25
            else if (GemiAnimasyonuSonGirilenId == 6 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.25f && myAgent.desiredVelocity.y < hiz * 0.45f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 6])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 6];
                }
                GemiAnimasyonuSonGirilenId = 6;
            }
            // sað üst 10
            else if (GemiAnimasyonuSonGirilenId == 7 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.10f && myAgent.desiredVelocity.y < hiz * 0.30f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 7])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 7];
                }
                GemiAnimasyonuSonGirilenId = 7;
            }
            // sað alt 85
            else if (GemiAnimasyonuSonGirilenId == 15 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 1f && myAgent.desiredVelocity.y < -hiz * 0.85f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 15])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 15];
                }
                GemiAnimasyonuSonGirilenId = 15;
            }
            // sað alt 70
            else if (GemiAnimasyonuSonGirilenId == 14 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.90f && myAgent.desiredVelocity.y < -hiz * 0.70f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 14])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 14];
                }
                GemiAnimasyonuSonGirilenId = 14;
            }
            // sað alt 55
            else if (GemiAnimasyonuSonGirilenId == 12 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.75f && myAgent.desiredVelocity.y < -hiz * 0.55f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 12])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 12];
                }
                GemiAnimasyonuSonGirilenId = 12;
            }
            // sað alt 47
            else if (GemiAnimasyonuSonGirilenId == 13 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.60 && myAgent.desiredVelocity.y < -hiz * 0.47f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 13])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 13];
                }
                GemiAnimasyonuSonGirilenId = 13;
            }
            // sað alt 40
            else if (GemiAnimasyonuSonGirilenId == 11 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.52f && myAgent.desiredVelocity.y < -hiz * 0.40f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 11])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 11];
                }
                GemiAnimasyonuSonGirilenId = 11;
            }
            // sað alt 25
            else if (GemiAnimasyonuSonGirilenId == 10 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.45f && myAgent.desiredVelocity.y < -hiz * 0.25f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 10])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 10];
                }
                GemiAnimasyonuSonGirilenId = 10;
            }
            // sað alt 10
            else if (GemiAnimasyonuSonGirilenId == 9 && myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.30f && myAgent.desiredVelocity.y < -hiz * 0.10f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 9])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 9];
                }
                GemiAnimasyonuSonGirilenId = 9;
            }
            // sol alt 85
            else if (GemiAnimasyonuSonGirilenId == 17 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 1f && myAgent.desiredVelocity.y < -hiz * 0.85f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 17])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 17];
                }
                GemiAnimasyonuSonGirilenId = 17;
            }
            // sol alt 70
            else if (GemiAnimasyonuSonGirilenId == 18 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.90f && myAgent.desiredVelocity.y < -hiz * 0.70f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 18])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 18];
                }
                GemiAnimasyonuSonGirilenId = 18;
            }
            // sol alt 55
            else if (GemiAnimasyonuSonGirilenId == 19 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.75f && myAgent.desiredVelocity.y < -hiz * 0.55f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 19])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 19];
                }
                GemiAnimasyonuSonGirilenId = 19;
            }
            // sol alt 47
            else if (GemiAnimasyonuSonGirilenId == 20 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.60f && myAgent.desiredVelocity.y < -hiz * 0.47f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 20])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 20];
                }
                GemiAnimasyonuSonGirilenId = 20;
            }
            // sol alt 40
            else if (GemiAnimasyonuSonGirilenId == 21 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.52f && myAgent.desiredVelocity.y < -hiz * 0.40f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 21])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 21];
                }
                GemiAnimasyonuSonGirilenId = 21;
            }
            // sol alt 25
            else if (GemiAnimasyonuSonGirilenId == 22 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.45f && myAgent.desiredVelocity.y < -hiz * 0.25f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 22])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 22];
                }
                GemiAnimasyonuSonGirilenId = 22;
            }
            // sol alt 10
            else if (GemiAnimasyonuSonGirilenId == 23 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.30f && myAgent.desiredVelocity.y < -hiz * 0.10f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 23])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 23];
                }
                GemiAnimasyonuSonGirilenId = 23;
            }
            // sol üst 10
            else if (GemiAnimasyonuSonGirilenId == 25 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.10f && myAgent.desiredVelocity.y < hiz * 0.30f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 25])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 25];
                }

                GemiAnimasyonuSonGirilenId = 25;
            }
            // sol üst 25
            else if (GemiAnimasyonuSonGirilenId == 26 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.25f && myAgent.desiredVelocity.y < hiz * 0.45f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 26])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 26];
                }
                GemiAnimasyonuSonGirilenId = 26;
            }
            // sol üst 40
            else if (GemiAnimasyonuSonGirilenId == 27 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.40f && myAgent.desiredVelocity.y < hiz * 0.52f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 27])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 27];
                }
                GemiAnimasyonuSonGirilenId = 27;
            }
            // sol üst 47
            else if (GemiAnimasyonuSonGirilenId == 28 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.47f && myAgent.desiredVelocity.y < hiz * 0.60f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 28])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 28];
                }
                GemiAnimasyonuSonGirilenId = 28;
            }
            // sol üst 55
            else if (GemiAnimasyonuSonGirilenId == 29 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.55f && myAgent.desiredVelocity.y < hiz * 0.75f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 29])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 29];
                }
                GemiAnimasyonuSonGirilenId = 29;
            }
            // sol üst 70
            else if (GemiAnimasyonuSonGirilenId == 30 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.70f && myAgent.desiredVelocity.y < hiz * 0.90f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 30])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 30];
                }
                GemiAnimasyonuSonGirilenId = 30;
            }
            // sol üst 85
            else if (GemiAnimasyonuSonGirilenId == 31 && myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.85f && myAgent.desiredVelocity.y < hiz * 1f)
            {
                if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 31])
                {
                    transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 31];
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
            // navmeshi kullanarak animatordeki spritelarý deðiþtirir ve gemi hareket animasyonu oluþturur
            if (GameManager.gm.BenimGemim != null && GameManager.gm.BenimGemim.GetComponent<Player>().OyunaGirisYapildimi)
            {
                // sað
                if (myAgent.desiredVelocity.x > hiz * 0.9f && myAgent.desiredVelocity.y <= hiz * 0.15f && myAgent.desiredVelocity.y >= -hiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 8])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 8];
                    }
                    GemiAnimasyonuSonGirilenId = 8;
                }
                // alt
                else if (myAgent.desiredVelocity.y < -hiz * 0.9f && myAgent.desiredVelocity.x <= hiz * 0.15f && myAgent.desiredVelocity.x >= -hiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 16])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 16];
                    }
                    GemiAnimasyonuSonGirilenId = 16;
                }
                //sol
                else if (myAgent.desiredVelocity.x < -hiz * 0.9f && myAgent.desiredVelocity.y <= hiz * 0.15f && myAgent.desiredVelocity.y >= -hiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 24])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 24];
                    }
                    GemiAnimasyonuSonGirilenId = 24;
                }
                // üst
                if (myAgent.desiredVelocity.y > hiz * 0.9f && myAgent.desiredVelocity.x <= hiz * 0.15f && myAgent.desiredVelocity.x >= -hiz * 0.15f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 0])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 0];
                    }
                    GemiAnimasyonuSonGirilenId = 0;
                }
                // sað üst 85
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.85f && myAgent.desiredVelocity.y < hiz * 1f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 1])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 1];
                    }
                    GemiAnimasyonuSonGirilenId = 1;
                }
                // sað üst 70
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.70f && myAgent.desiredVelocity.y < hiz * 0.90f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 2])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 2];
                    }
                    GemiAnimasyonuSonGirilenId = 2;
                }
                // sað üst 55
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.55f && myAgent.desiredVelocity.y < hiz * 0.75f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 3])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 3];
                    }
                    GemiAnimasyonuSonGirilenId = 3;
                }
                // sað üst 47
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.47f && myAgent.desiredVelocity.y < hiz * 0.60f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 4])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 4];
                    }
                    GemiAnimasyonuSonGirilenId = 4;
                }
                // sað üst 40
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.40f && myAgent.desiredVelocity.y < hiz * 0.52f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 5])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 5];
                    }
                    GemiAnimasyonuSonGirilenId = 5;
                }
                // sað üst 25
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.25f && myAgent.desiredVelocity.y < hiz * 0.45f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 6])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 6];
                    }
                    GemiAnimasyonuSonGirilenId = 6;
                }
                // sað üst 10
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > hiz * 0.10f && myAgent.desiredVelocity.y < hiz * 0.30f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 7])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 7];
                    }
                    GemiAnimasyonuSonGirilenId = 7;
                }
                // sað alt 85
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 1f && myAgent.desiredVelocity.y < -hiz * 0.85f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 15])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 15];
                    }
                    GemiAnimasyonuSonGirilenId = 15;
                }
                // sað alt 70
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.90f && myAgent.desiredVelocity.y < -hiz * 0.70f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 14])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 14];
                    }
                    GemiAnimasyonuSonGirilenId = 14;
                }
                // sað alt 55
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.75f && myAgent.desiredVelocity.y < -hiz * 0.55f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 12])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 12];
                    }
                    GemiAnimasyonuSonGirilenId = 12;
                }
                // sað alt 47
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.60 && myAgent.desiredVelocity.y < -hiz * 0.47f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 13])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 13];
                    }
                    GemiAnimasyonuSonGirilenId = 13;
                }
                // sað alt 40
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.52f && myAgent.desiredVelocity.y < -hiz * 0.40f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 11])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 11];
                    }
                    GemiAnimasyonuSonGirilenId = 11;
                }
                // sað alt 25
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.45f && myAgent.desiredVelocity.y < -hiz * 0.25f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 10])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 10];
                    }
                    GemiAnimasyonuSonGirilenId = 10;
                }
                // sað alt 10
                else if (myAgent.desiredVelocity.x > hiz * 0.15f && myAgent.desiredVelocity.x < hiz * 1f && myAgent.desiredVelocity.y > -hiz * 0.30f && myAgent.desiredVelocity.y < -hiz * 0.10f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 9])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 9];
                    }
                    GemiAnimasyonuSonGirilenId = 9;
                }
                // sol alt 85
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 1f && myAgent.desiredVelocity.y < -hiz * 0.85f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 17])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 17];
                    }
                    GemiAnimasyonuSonGirilenId = 17;
                }
                // sol alt 70
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.90f && myAgent.desiredVelocity.y < -hiz * 0.70f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 18])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 18];
                    }
                    GemiAnimasyonuSonGirilenId = 18;
                }
                // sol alt 55
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.75f && myAgent.desiredVelocity.y < -hiz * 0.55f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 19])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 19];
                    }
                    GemiAnimasyonuSonGirilenId = 19;
                }
                // sol alt 47
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.60f && myAgent.desiredVelocity.y < -hiz * 0.47f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 20])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 20];
                    }
                    GemiAnimasyonuSonGirilenId = 20;
                }
                // sol alt 40
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.52f && myAgent.desiredVelocity.y < -hiz * 0.40f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 21])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 21];
                    }
                    GemiAnimasyonuSonGirilenId = 21;
                }
                // sol alt 25
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.45f && myAgent.desiredVelocity.y < -hiz * 0.25f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 22])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 22];
                    }
                    GemiAnimasyonuSonGirilenId = 22;
                }
                // sol alt 10
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > -hiz * 0.30f && myAgent.desiredVelocity.y < -hiz * 0.10f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 23])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 23];
                    }
                    GemiAnimasyonuSonGirilenId = 23;
                }
                // sol üst 10
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.10f && myAgent.desiredVelocity.y < hiz * 0.30f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 25])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 25];
                    }

                    GemiAnimasyonuSonGirilenId = 25;
                }
                // sol üst 25
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.25f && myAgent.desiredVelocity.y < hiz * 0.45f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 26])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 26];
                    }
                    GemiAnimasyonuSonGirilenId = 26;
                }
                // sol üst 40
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.40f && myAgent.desiredVelocity.y < hiz * 0.52f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 27])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 27];
                    }
                    GemiAnimasyonuSonGirilenId = 27;
                }
                // sol üst 47
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.47f && myAgent.desiredVelocity.y < hiz * 0.60f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 28])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 28];
                    }
                    GemiAnimasyonuSonGirilenId = 28;
                }
                // sol üst 55
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.55f && myAgent.desiredVelocity.y < hiz * 0.75f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 29])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 29];
                    }
                    GemiAnimasyonuSonGirilenId = 29;
                }
                // sol üst 70
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.70f && myAgent.desiredVelocity.y < hiz * 0.90f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 30])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 30];
                    }
                    GemiAnimasyonuSonGirilenId = 30;
                }
                // sol üst 85
                else if (myAgent.desiredVelocity.x > -hiz * 1f && myAgent.desiredVelocity.x < -hiz * 0.15f && myAgent.desiredVelocity.y > hiz * 0.85f && myAgent.desiredVelocity.y < hiz * 1f)
                {
                    if (transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite != GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 31])
                    {
                        transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 31];
                    }
                    GemiAnimasyonuSonGirilenId = 31;
                }
            }
        }
    }
    public void CanKontrol(int oldvalue, int newvalue)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            if (Can < (MaksCan * 0.3f))
            {
                hiz = maksHiz * 0.5f;
                HizKontrol(hiz, hiz);
            }
            else
            {
                if (hizTasiAnimasyonDurumu == false)
                {
                    hiz = maksHiz;
                }
                HizKontrol(hiz, hiz);
            }
        }
#endif
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.sliderCan.value = Can;
            GameManager.gm.ValueTextCan.text = Can + "/" + MaksCan;
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinCanSLÝDER.value = Can;
            GameManager.gm.WinCanTXT.text = Can + "/" + MaksCan;
#endif
        }
        if (!isServerOnly)
        {
            if (oldvalue > newvalue)
            {
                GameManager.gm.oyundancikiscancel = true;
            }
            transform.Find("OyuncuCanvas/Can").GetComponent<Slider>().value = Can;
        }
    }

    public void MaksCanKontrol(int oldvalue, int newvalue)
    {
        if (isLocalPlayer)
        {
#if UNITY_ANDROID
            GameManager.gm.sliderCan.maxValue = MaksCan;
            GameManager.gm.ValueTextCan.text = Can + "/" + MaksCan;
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinCanSLÝDER.maxValue = MaksCan;
            GameManager.gm.WinCanTXT.text = Can + "/" + MaksCan;
#endif
        }
        if (!isServerOnly)
        {
            transform.Find("OyuncuCanvas/Can").GetComponent<Slider>().maxValue = MaksCan;
        }
    }

    //---------------------------Saldiri---------------------------------------------------------

    [Command]
    private void GulleYarat(GameObject Hedef, int gulleId)
    {

#if UNITY_SERVER || UNITY_EDITOR
        if (Hedef != null)
        {
            if (isServer &&  harita == 98 && Hedef.CompareTag("Oyuncu"))
            {
                SavasYasakliBolgeUyarisi();
            }
            else if (isServer &&  harita == 98 && Hedef.CompareTag("Oyuncu"))
            {
                SavasYasakliBolgeUyarisi();
            }
            else if (Can > 0 && (Hedef.CompareTag("Oyuncu") && Hedef.GetComponent<Player>().Can > 0) || (Hedef.CompareTag("SistemGemisi") && Hedef.GetComponent<SistemGemisiKontrol>().Can > 0) || (Hedef.CompareTag("Tower") && Hedef.GetComponent<KuleKontrol>().Can > 0) || (Hedef.CompareTag("SistemOyuncuGemisi") && Hedef.GetComponent<SistemOyuncuKontrol>().Can > 0) || (Hedef.CompareTag("EtkinlikGemisi") && Hedef.GetComponent<EtkinlikSistemGemileriKontrol>().Can > 0) || (Hedef.CompareTag("EtkinlikBossu") && Hedef.GetComponent<EtkinlikBossKontrol>().Can > 0))
            {
                // ayný filodaki oyuncuya saldýrmaya çalýþýyorsa
                if (Hedef.CompareTag("Oyuncu") && OyuncuFiloKisaltma.Length > 0 && Hedef.GetComponent<Player>().OyuncuFiloKisaltma == OyuncuFiloKisaltma && gulleId != 3)
                {
                    saldiridurumu = false;
                }
                else
                {
                    if (isServer && Time.time - sonsaldirilanzaman >= saldirihizi)
                    {
                        int topSayisi = oyuncuOnBesKilolukTopGemi + oyuncuYirmiBesKilolukTopGemi + oyuncuYirmiYediBucukKilolukTopGemi + oyuncuOtuzKilolukTopGemi + oyuncuOtuzBesKilolukTopGemi+ oyuncuCifteVurusTopGemi;
                        // atikacak gulleyi seciyor
                        int atilacakGulleAdet = 0;
                        switch (gulleId)
                        {
                            case 0:
                                atilacakGulleAdet = topSayisi + 1;
                                break;
                            case 1:
                                atilacakGulleAdet = oyuncuDemirGulle;
                                break;
                            case 2:
                                atilacakGulleAdet = oyuncuAlevGulle;
                                break;
                            case 3:
                                atilacakGulleAdet = oyuncuSifaGulle;
                                break;
                            case 4:
                                atilacakGulleAdet = oyuncuHavaiFisekGulle;
                                break;
                            case 10:
                                atilacakGulleAdet = OyuncuHallowenGulle;
                                break;
                            case 11:
                                atilacakGulleAdet = OyuncuNoelGullesi;
                                break;
                            case 12:
                                atilacakGulleAdet = KalpliGulle;
                                break;
                        }
                        // yeteri kadar cephanemiz varmý ve dusman menzilimizdemi kontrolunun yapildigi yer
                        if (Hedef != null && Vector2.Distance(transform.position, Hedef.transform.position) <= menzil && atilacakGulleAdet >= topSayisi)
                        {
                            int elitPuanCarpan = 0;
                            float hasar = (oyuncuOnBesKilolukTopGemi * 20) + (oyuncuYirmiBesKilolukTopGemi * 25) + (oyuncuYirmiYediBucukKilolukTopGemi * 30) + (oyuncuOtuzKilolukTopGemi * 35) + (oyuncuOtuzBesKilolukTopGemi * 40) + (oyuncuCifteVurusTopGemi * 50);


                            float isabetIhtimali = ((oyuncuOnBesKilolukTopGemi * 0.45f) + (oyuncuYirmiBesKilolukTopGemi * 0.5f) + (oyuncuYirmiYediBucukKilolukTopGemi * 0.55f) + (oyuncuOtuzKilolukTopGemi * 0.6f) + (oyuncuOtuzBesKilolukTopGemi * 0.65f) + (oyuncuCifteVurusTopGemi * 0.75f)) / topSayisi;
                            isabetIhtimali = isabetIhtimali + (oyuncuYetenekIsabetOrani * 0.01f);

                            switch (gulleId)
                            {
                                // Taþ Gülle
                                case 0:
                                    hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f)) * 0.8f;
                                    break;
                                // Demir Gülle
                                case 1:
                                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                                    {
                                        command.CommandText = "Update Kullanici SET DemirGulle = DemirGulle - " + topSayisi + " WHERE Kullanici_Adi=@kullanici_adi;";
                                        command.Parameters.AddWithValue("@kullanici_adi", name);
                                        command.ExecuteNonQuery();
                                    }
                                    SetOyuncuDemirGullesi(oyuncuDemirGulle - topSayisi);
                                    hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f));
                                    break;
                                // Alev Gülle
                                case 2:
                                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                                    {
                                        command.CommandText = "Update Kullanici SET AlevGulle = AlevGulle - " + topSayisi + " WHERE Kullanici_Adi=@kullanici_adi;";
                                        command.Parameters.AddWithValue("@kullanici_adi", name);
                                        command.ExecuteNonQuery();
                                    }
                                    SetOyuncuAlevGullesi(oyuncuAlevGulle - topSayisi);
                                    // Alev Gülle ile sistem gemisine saldýrý
                                    if (Hedef.CompareTag("SistemGemisi") || Hedef.CompareTag("EtkinlikGemisi") || Hedef.CompareTag("EtkinlikBossu"))
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f)) * 1.5f;
                                        elitPuanCarpan = 1;
                                    }
                                    // Alev Gülle ile oyuncuya saldýrý
                                    else
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f));
                                        elitPuanCarpan = 1;
                                    }
                                    break;
                                // Þifa Gülle
                                case 3:
                                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                                    {
                                        command.CommandText = "Update Kullanici SET SifaGulle = SifaGulle - " + topSayisi + " WHERE Kullanici_Adi=@kullanici_adi;";
                                        command.Parameters.AddWithValue("@kullanici_adi", name);
                                        command.ExecuteNonQuery();
                                    }
                                    SetOyuncuSifaGullesi(oyuncuSifaGulle - topSayisi);
                                    hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f)) * -0.5f;
                                    elitPuanCarpan = 1;
                                    break;
                                // Havai Gülle
                                case 4:
                                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                                    {
                                        command.CommandText = "Update Kullanici SET HavaiFisekGulle = HavaiFisekGulle - " + topSayisi + " WHERE Kullanici_Adi=@kullanici_adi;";
                                        command.Parameters.AddWithValue("@kullanici_adi", name);
                                        command.ExecuteNonQuery();
                                    }
                                    SetOyuncuHavaiFisek(oyuncuHavaiFisekGulle - topSayisi);
                                    // Havai ile oyuncuya saldirilirsa
                                    if (Hedef.CompareTag("Oyuncu") || Hedef.CompareTag("SistemOyuncuGemisi") || Hedef.CompareTag("Tower"))
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f)) * 1.25f;
                                        elitPuanCarpan = 2;
                                    }
                                    // Havai ile npc ye saldirilirsa
                                    else
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f));
                                        elitPuanCarpan = 2;
                                    }
                                    break;
                                case 10:
                                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                                    {
                                        command.CommandText = "Update Kullanici SET HallowenGulle = HallowenGulle - " + topSayisi + " WHERE Kullanici_Adi=@kullanici_adi;";
                                        command.Parameters.AddWithValue("@kullanici_adi", name);
                                        command.ExecuteNonQuery();
                                    }
                                    SetOyuncuHallowenGullesi(OyuncuHallowenGulle - topSayisi);
                                    if (Hedef.CompareTag("SistemGemisi") || Hedef.CompareTag("EtkinlikGemisi") || Hedef.CompareTag("EtkinlikBossu"))
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f)) * 1.5f;
                                        elitPuanCarpan = 2;
                                    }
                                    // Alev Gülle ile oyuncuya saldýrý
                                    else
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f));
                                        elitPuanCarpan = 2;
                                    }
                                    break;
                                case 11:
                                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                                    {
                                        command.CommandText = "Update Kullanici SET NoelGulle = NoelGulle - " + topSayisi + " WHERE Kullanici_Adi=@kullanici_adi;";
                                        command.Parameters.AddWithValue("@kullanici_adi", name);
                                        command.ExecuteNonQuery();
                                    }
                                    SetOyuncuNoelGullesi(OyuncuNoelGullesi - topSayisi);
                                    if (Hedef.CompareTag("SistemGemisi") || Hedef.CompareTag("EtkinlikGemisi") || Hedef.CompareTag("EtkinlikBossu"))
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f)) * 1.5f;
                                        elitPuanCarpan = 2;
                                    }
                                    // Alev Gülle ile oyuncuya saldýrý
                                    else
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f));
                                        elitPuanCarpan = 2;
                                    }
                                    break;
                                case 12:
                                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                                    {
                                        command.CommandText = "Update Kullanici SET KalpliGulle = KalpliGulle - " + topSayisi + " WHERE Kullanici_Adi=@kullanici_adi;";
                                        command.Parameters.AddWithValue("@kullanici_adi", name);
                                        command.ExecuteNonQuery();
                                    }
                                    SetOyuncuKalpliGulle(KalpliGulle - topSayisi);
                                    if (Hedef.CompareTag("SistemGemisi") || Hedef.CompareTag("EtkinlikGemisi") || Hedef.CompareTag("EtkinlikBossu"))
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f)) * 1.5f;
                                        elitPuanCarpan = 2;
                                    }
                                    // Alev Gülle ile oyuncuya saldýrý
                                    else
                                    {
                                        hasar = Random.Range(hasar * isabetIhtimali, hasar * (isabetIhtimali + 0.1f));
                                        elitPuanCarpan = 2;
                                    }
                                    break;
                            }

                            if (oyuncuBarutDurum && oyuncuBarut > 0)
                            {
                                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                                {
                                    command.CommandText = "Update Kullanici SET Barut = Barut - 1 WHERE Kullanici_Adi=@kullanici_adi and Barut > 0;";
                                    command.Parameters.AddWithValue("@kullanici_adi", name);
                                    if (command.ExecuteNonQuery() == 1)
                                    {
                                        SetOyuncuBarut(oyuncuBarut - 1);
                                        hasar = hasar * (1.1f + (0.01f * oyuncuYetenekBarut));

                                    }
                                }
                            }
                            else if (oyuncuBarutDurum && oyuncuBarut == 0)
                            {
                                SetOyuncuBarutDurum(false);
                            }
                            if (Random.Range(0, 100) < (oyuncuYetenekKiritikVurusIhtimali))
                            {
                                hasar = hasar * (1.2f + (oyuncuYetenekKritikHasar * 0.02f));
                            }
                            if (Hedef.CompareTag("Oyuncu"))
                            {
                                hasar += (hasar * OyuncuElitPuanHasarBonusu) - ((hasar * Hedef.GetComponent<Player>().OyuncuElitPuanZirhBonusu) + (hasar * (Hedef.GetComponent<Player>().oyuncuYetenekZirh * 0.005f)));
                                if (Hedef.GetComponent<Player>().oyuncuKalkanDurum && Hedef.GetComponent<Player>().oyuncuKalkan > 0)
                                {
                                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                                    {
                                        command.CommandText = "Update Kullanici SET Kalkan = Kalkan - 1 WHERE ID=@ID and Kalkan > 0;";
                                        command.Parameters.AddWithValue("@ID", Hedef.GetComponent<Player>().oyuncuId);
                                        if (command.ExecuteNonQuery() == 1)
                                        {
                                            Hedef.GetComponent<Player>().SetOyuncuKalkan(Hedef.GetComponent<Player>().oyuncuKalkan - 1);
                                            hasar *= (0.9f - (Hedef.GetComponent<Player>().oyuncuYetenekKalkani * 0.01f));
                                        }
                                    }
                                }
                                else if (Hedef.GetComponent<Player>().oyuncuKalkanDurum && Hedef.GetComponent<Player>().oyuncuKalkan == 0)
                                {
                                    Hedef.GetComponent<Player>().SetOyuncuKalkanDurum(false);
                                }
                                hasar = hasar + (oyuncuYetenekZirhDelme * 10);
                                RpcGulleYarat(Hedef, gulleId, (int)hasar, false, oyuncuBarutDurum, Hedef.GetComponent<Player>().oyuncuKalkanDurum);
                            }
                            else if (Hedef.CompareTag("SistemGemisi") || Hedef.CompareTag("EtkinlikGemisi") || Hedef.CompareTag("EtkinlikBossu") || Hedef.CompareTag("SistemOyuncuGemisi") || Hedef.CompareTag("Tower"))
                            {

                                hasar += (hasar * OyuncuElitPuanHasarBonusu) + (hasar * (oyuncuYetenekPVEHasar * 0.01f));
                                hasar = hasar + (oyuncuYetenekZirhDelme * 10);
                                RpcGulleYarat(Hedef, gulleId, (int)hasar, false, oyuncuBarutDurum, false);

                            }
                            ElitPuanEkle((int)((topSayisi * elitPuanCarpan) + ((topSayisi * elitPuanCarpan) * (OyuncuTpKatlamaDurumu * 0.1f))));
                            sonsaldirilanzaman = Time.time;
                            GulleHasarVer(Hedef, (int)hasar);
                            TargetGulleHasarver();
                        }
                        else
                        {
                            saldiridurumu = false;
                        }
                    }
                    sonaktiflikzamani = Time.time;
                }
            }
        }
#endif
    }

    [TargetRpc]
    public void SavasYasakliBolgeUyarisi()
    {
        GameManager.gm.OdulText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 344];
        GameManager.gm.OdulYazisiSayacBaslat();
    }

    [TargetRpc]
    public void TargetGulleHasarver()
    {
#if UNITY_STANDALONE_WIN || UNITY_ANDROID
        GameManager.gm.GemiGulleAtmaSesi.Play();
#endif

        GameManager.gm.SalýrýyorumBilgiMenutimer = 10.0f;
        GameManager.gm.cooldowntimerSaldiri = saldirihizi;
        GameManager.gm.cooldowntimeSaldiri = saldirihizi;
    }

    [Command]
    private void ZipkinYarat(GameObject Hedef, int zipkinId)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer && Hedef != null && Can > 0 && Hedef.CompareTag("DenizYaratigi") && Hedef.GetComponent<SistemYaratikKontrol>().Can > 0 && Time.time - sonsaldirilanzaman >= (zipkinSaldiriHizi - (oyuncuYetenekZipkinSaldiriHizi * 0.2f)))
        {
            int hasar = 0;
            int kullanilacakZipkin = 0;
            switch (zipkinId)
            {
                case 0:
                    kullanilacakZipkin = oyuncuPaslanmisZipkin;
                    hasar = 50;
                    break;
                case 1:
                    kullanilacakZipkin = oyuncuGumusZipkin;
                    hasar = 100;
                    break;
                case 2:
                    kullanilacakZipkin = oyuncuAltinZipkin;
                    hasar = 200;
                    break;
                case 3:
                    kullanilacakZipkin = OyuncuHallowenZipkin;
                    hasar = 300;
                    break;
                case 4:
                    kullanilacakZipkin = OyuncuNoelZipkin;
                    hasar = 300;
                    break;
                case 8:
                    kullanilacakZipkin = KalpliZipkin;
                    hasar = 300;
                    break;

            }
            if (kullanilacakZipkin > 0)
            {
                if (Vector2.Distance(transform.position, Hedef.transform.position) <= (zipkinMenzil + (oyuncuYetenekZipkinMenzili * 0.4f)))
                {
                    switch (zipkinId)
                    {
                        case 0:
                            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command.CommandText = "Update Kullanici SET PaslanmisZipkin = PaslanmisZipkin - 1 WHERE Kullanici_Adi=@kullanici_adi;";
                                command.Parameters.AddWithValue("@kullanici_adi", name);
                                command.ExecuteNonQuery();
                            }
                            SetOyuncuPaslanmisZipkin(oyuncuPaslanmisZipkin - 1);
                            break;
                        case 1:
                            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command.CommandText = "Update Kullanici SET GumusZipkin = GumusZipkin - 1 WHERE Kullanici_Adi=@kullanici_adi;";
                                command.Parameters.AddWithValue("@kullanici_adi", name);
                                command.ExecuteNonQuery();
                            }
                            SetOyuncuGumusZipkin(oyuncuGumusZipkin - 1);
                            break;
                        case 2:
                            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command.CommandText = "Update Kullanici SET AltinZipkin = AltinZipkin - 1 WHERE Kullanici_Adi=@kullanici_adi;";
                                command.Parameters.AddWithValue("@kullanici_adi", name);
                                command.ExecuteNonQuery();
                            }
                            SetOyuncuAltinZipkin(oyuncuAltinZipkin - 1);
                            break;
                        case 3:
                            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command.CommandText = "Update Kullanici SET HallowenZipkin = HallowenZipkin - 1 WHERE Kullanici_Adi=@kullanici_adi;";
                                command.Parameters.AddWithValue("@kullanici_adi", name);
                                command.ExecuteNonQuery();
                            }
                            SetOyuncuHallowenZipkin(OyuncuHallowenZipkin - 1);
                            break;
                        case 4:
                            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command.CommandText = "Update Kullanici SET NoelZipkin = NoelZipkin - 1 WHERE Kullanici_Adi=@kullanici_adi;";
                                command.Parameters.AddWithValue("@kullanici_adi", name);
                                command.ExecuteNonQuery();
                            }
                            SetOyuncuNoelZipkin(OyuncuNoelZipkin - 1);
                            break;
                        case 8:
                            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command.CommandText = "Update Kullanici SET KalpliZipkin = KalpliZipkin - 1 WHERE Kullanici_Adi=@kullanici_adi;";
                                command.Parameters.AddWithValue("@kullanici_adi", name);
                                command.ExecuteNonQuery();
                            }
                            SetOyuncuKalpliZipkin(KalpliZipkin - 1);
                            break;

                    }
                    sonsaldirilanzaman = Time.time;
                    hasar = hasar + (int)(hasar * (oyuncuYetenekZipkinHasari * 0.1f));
                    RpcZipkinYarat(Hedef, zipkinId, hasar);
                    ZipkinHasarVer(Hedef, hasar);
                    TargetZipkinYarat();
                }
            }
            else
            {
                oyuncucephanesibittiZipkin();
            }
        }
#endif
    }
    [TargetRpc]
    public void TargetZipkinYarat()
    {
        sonsaldirilanzaman = Time.time;
#if UNITY_STANDALONE_WIN || UNITY_ANDROID
        GameManager.gm.GemiZipkinAtmaSesi.Play();
#endif

        GameManager.gm.SalýrýyorumBilgiMenutimer = 10.0f;
        GameManager.gm.cooldowntimerSaldiri = zipkinSaldiriHizi - (oyuncuYetenekZipkinSaldiriHizi * 0.2f);
        GameManager.gm.cooldowntimeSaldiri = zipkinSaldiriHizi - (oyuncuYetenekZipkinSaldiriHizi * 0.2f);
    }
    [TargetRpc]
    public void oyuncucephanesibittiZipkin()
    {
        GameManager.gm.OdulText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 347];
        GameManager.gm.OdulYazisiSayacBaslat();
    }
    [Command]
    private void RoketYarat(GameObject Hedef)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (Hedef != null)
        {
            if ( isServer && harita < 3  && Hedef.CompareTag("Oyuncu"))
            {
                SavasYasakliBolgeUyarisi();
            }
            if (isServer && harita == 98 && Hedef.CompareTag("Oyuncu"))
            {
                SavasYasakliBolgeUyarisi();
            }
            else if (isServer && Can > 0 && ((Hedef.CompareTag("Oyuncu") && Hedef.GetComponent<Player>().Can > 0) || (Hedef.CompareTag("SistemGemisi") && Hedef.GetComponent<SistemGemisiKontrol>().Can > 0) || (Hedef.CompareTag("SistemOyuncuGemisi") && Hedef.GetComponent<SistemOyuncuKontrol>().Can > 0) || (Hedef.CompareTag("Tower") && Hedef.GetComponent<KuleKontrol>().Can > 0) || (Hedef.CompareTag("EtkinlikBossu") && Hedef.GetComponent<EtkinlikBossKontrol>().Can > 0) || (Hedef.CompareTag("EtkinlikGemisi") && Hedef.GetComponent<EtkinlikSistemGemileriKontrol>().Can > 0)) && Time.time - sonRoketAtilanZaman >= roketSaldiriHizi)
            {
                int hasar = 400 + (oyuncuYetenekRoketHasari * 50);
                if (Vector2.Distance(transform.position, Hedef.transform.position) <= roketMenzil && oyuncuRoket > 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET Roket = Roket - 1 WHERE Kullanici_Adi=@kullanici_adi;";
                        command.Parameters.AddWithValue("@kullanici_adi", name);
                        command.ExecuteNonQuery();
                    }
                    SetOyuncuRoket(oyuncuRoket - 1);
                    sonRoketAtilanZaman = Time.time;
                    RpcRoketYarat(Hedef, hasar);
                    RoketHasarVer(Hedef, hasar);
                }
                else if (oyuncuRoketDurum && oyuncuRoket == 0)
                {
                    SetOyuncuRoketDurum(false);
                }
            }
            sonaktiflikzamani = Time.time;
        }
#endif
    }

    public void Saldir(bool saldiridurumukontrol)
    {
        saldiridurumu = saldiridurumukontrol;
    }

    //---------------------------------------- Elit Puan Ekleme Bölümü --------------------------------------------------------

#if UNITY_SERVER || UNITY_EDITOR
    public void ElitPuanEkle(int eklenecekElitPuan)
    {
        if (eklenecekElitPuan > 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET ElitPuan = ElitPuan + " + eklenecekElitPuan + " WHERE Kullanici_Adi=@kullanici_adi;";
                command.Parameters.AddWithValue("@kullanici_adi", name);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetElitPuani(oyuncuElitPuan + eklenecekElitPuan);
                }
            }
        }
    }

    public void GulleHasarVer(GameObject Hedef, int hasar)
    {
        if (Hedef != null && hasar >= 0 && Hedef.CompareTag("SistemGemisi"))
        {
            Hedef.GetComponent<SistemGemisiKontrol>().Can -= hasar;
            Hedef.GetComponent<SistemGemisiKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<SistemGemisiKontrol>().sonHasarAlinanZaman = Time.time;
        }
        if (Hedef != null && hasar >= 0 && Hedef.CompareTag("SistemOyuncuGemisi"))
        {
            Hedef.GetComponent<SistemOyuncuKontrol>().Can -= hasar;
            Hedef.GetComponent<SistemOyuncuKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<SistemOyuncuKontrol>().sonHasarAlinanZaman = Time.time;
        }
        if (Hedef != null && hasar >= 0 && Hedef.CompareTag("Tower"))
        {
            Hedef.GetComponent<KuleKontrol>().Can -= hasar;
            Hedef.GetComponent<KuleKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<KuleKontrol>().sonHasarAlinanZaman = Time.time;
        }
        if (Hedef != null && hasar >= 0 && Hedef.CompareTag("EtkinlikGemisi"))
        {
            Hedef.GetComponent<EtkinlikSistemGemileriKontrol>().Can -= hasar;
            Hedef.GetComponent<EtkinlikSistemGemileriKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<EtkinlikSistemGemileriKontrol>().sonHasarAlinanZaman = Time.time;
        }
        if (Hedef != null && hasar >= 0 && Hedef.CompareTag("EtkinlikBossu"))
        {
            Hedef.GetComponent<EtkinlikBossKontrol>().Can -= hasar;
            Hedef.GetComponent<EtkinlikBossKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<EtkinlikBossKontrol>().sonHasarAlinanZaman = Time.time;
        }
        else if (Hedef != null && hasar >= 0 && Hedef.CompareTag("Oyuncu") && Hedef.GetComponent<Player>().Can > 0 && Hedef.GetComponent<Player>().oyuncuId != 16)
        {
            Hedef.GetComponent<Player>().Can -= hasar;
            Hedef.GetComponent<Player>().CanKontrol(Hedef.GetComponent<Player>().Can, Hedef.GetComponent<Player>().Can);
            if (Hedef.GetComponent<Player>().Can <= 0)
            {
                GameManager.gm.SunucuTahtasiGuncelle(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " -  " + oyuncuadi + "<color=#ff0000> Kill </color>" + Hedef.GetComponent<Player>().oyuncuadi);
                SavasPuaniEkle(Hedef.GetComponent<Player>().oyuncuadi, Hedef.GetComponent<Player>().seviye);
                FiloSavasdaysaSkorEklemeKontrolu(Hedef);
            }
            Hedef.GetComponent<Player>().sonaktiflikzamani = Time.time;
            Hedef.GetComponent<Player>().sonHasarAlinanZaman = Time.time;
            Hedef.GetComponent<Player>().SetOyuncuTamirDurum(false);
            Hedef.GetComponent<Player>().sonsaldiranoyuncu = gameObject;
        }
        else if (Hedef != null && hasar < 0 && Hedef.CompareTag("Oyuncu") && Hedef.GetComponent<Player>().Can < Hedef.GetComponent<Player>().MaksCan)
        {
            Hedef.GetComponent<Player>().Can -= hasar;
            Hedef.GetComponent<Player>().CanKontrol(Hedef.GetComponent<Player>().Can, Hedef.GetComponent<Player>().Can);
            Hedef.GetComponent<Player>().sonaktiflikzamani = Time.time;
            Hedef.GetComponent<Player>().sonsaldiranoyuncu = gameObject;

            if (Hedef.GetComponent<Player>().Can > MaksCan)
            {
                Hedef.GetComponent<Player>().Can = MaksCan;
                Hedef.GetComponent<Player>().CanKontrol(Hedef.GetComponent<Player>().Can, Hedef.GetComponent<Player>().Can);
            }
        }
    }

    public void ZipkinHasarVer(GameObject Hedef, int hasar)
    {
        if (Hedef != null && hasar >= 0)
        {
            Hedef.GetComponent<SistemYaratikKontrol>().Can -= hasar;
            Hedef.GetComponent<SistemYaratikKontrol>().sonsaldiranoyuncu = gameObject;
            Hedef.GetComponent<SistemYaratikKontrol>().sonHasarAlinanZaman = Time.time;
            if (Hedef.GetComponent<SistemYaratikKontrol>().IlkSaldiranOyuncu == null && (harita == 1 || harita == 2))
            {
                Hedef.GetComponent<SistemYaratikKontrol>().IlkSaldiranOyuncu = gameObject;
            }
        }
    }
    public void RoketHasarVer(GameObject Hedef, int hasar)
    {
        if (Hedef != null && hasar >= 0 && Hedef.CompareTag("EtkinlikGemisi"))
        {
            Hedef.GetComponent<EtkinlikSistemGemileriKontrol>().Can -= hasar;
            Hedef.GetComponent<EtkinlikSistemGemileriKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<EtkinlikSistemGemileriKontrol>().sonHasarAlinanZaman = Time.time;
        }
        else if (Hedef != null && hasar >= 0 && Hedef.CompareTag("EtkinlikBossu"))
        {
            Hedef.GetComponent<EtkinlikBossKontrol>().Can -= hasar;
            Hedef.GetComponent<EtkinlikBossKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<EtkinlikBossKontrol>().sonHasarAlinanZaman = Time.time;
        }
        else if (Hedef != null && hasar >= 0 && Hedef.CompareTag("SistemGemisi"))
        {
            Hedef.GetComponent<SistemGemisiKontrol>().Can -= hasar;
            Hedef.GetComponent<SistemGemisiKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<SistemGemisiKontrol>().sonHasarAlinanZaman = Time.time;
        }
        else if (Hedef != null && hasar >= 0 && Hedef.CompareTag("SistemOyuncuGemisi"))
        {
            Hedef.GetComponent<SistemOyuncuKontrol>().Can -= hasar;
            Hedef.GetComponent<SistemOyuncuKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<SistemOyuncuKontrol>().sonHasarAlinanZaman = Time.time;
        }
        else if (Hedef != null && hasar >= 0 && Hedef.CompareTag("Tower"))
        {
            Hedef.GetComponent<KuleKontrol>().Can -= hasar;
            Hedef.GetComponent<KuleKontrol>().SetSonSaldiranOyuncu(gameObject, hasar);
            Hedef.GetComponent<KuleKontrol>().sonHasarAlinanZaman = Time.time;
        }
        else if (Hedef != null && hasar >= 0 && Hedef.CompareTag("Oyuncu") && Hedef.GetComponent<Player>().oyuncuId != 16)
        {
            Hedef.GetComponent<Player>().Can -= hasar;
            Hedef.GetComponent<Player>().CanKontrol(Hedef.GetComponent<Player>().Can, Hedef.GetComponent<Player>().Can);
            if (Hedef.GetComponent<Player>().Can <= 0)
            {
                GameManager.gm.SunucuTahtasiGuncelle(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " -  " + oyuncuadi + "<color=#ff0000> Kill </color>" + Hedef.GetComponent<Player>().oyuncuadi);
                SavasPuaniEkle(Hedef.GetComponent<Player>().oyuncuadi, Hedef.GetComponent<Player>().seviye);
                FiloSavasdaysaSkorEklemeKontrolu(Hedef);
            }
            Hedef.GetComponent<Player>().sonaktiflikzamani = Time.time;
            Hedef.GetComponent<Player>().SetOyuncuTamirDurum(false);
            Hedef.GetComponent<Player>().sonsaldiranoyuncu = gameObject;
        }
    }
#endif


    [ClientRpc]
    void RpcGulleYarat(GameObject Hedef, int gulleid, int hasar, bool kritikgelmedurumu, bool barutAktiflikDurumu, bool kalkanAktiflikDurumu)
    {
        StartCoroutine(GulleSureliYarat(Hedef, gulleid, hasar, barutAktiflikDurumu, kalkanAktiflikDurumu));
    }

    [ClientRpc]
    void RpcZipkinYarat(GameObject Hedef, int zipkinId, int hasar)
    {
        ZipkinYaratOyuncu(Hedef, zipkinId, hasar);
    }

    [ClientRpc]
    void RpcRoketYarat(GameObject Hedef, int hasar)
    {
        RoketYaratOyuncu(Hedef, hasar);
    }

    IEnumerator GulleSureliYarat(GameObject Hedef, int gulleId, int hasar, bool barutAktiflikDurumu, bool kalkanAktiflikDurumu)
    {
        if (Hedef == GameManager.gm.BenimGemim)
        {
            Hedef.GetComponent<Player>().sonsaldiranoyuncu = gameObject;
            gameObject.transform.Find("OyuncuCanvas/Cember").gameObject.GetComponent<Image>().color = Color.red;
            gameObject.transform.Find("OyuncuCanvas/Cember").gameObject.SetActive(true);
            GameManager.gm.sonBizeSaldirilanZaman = Time.time;
        }
        if (gulleId != 4)
        {
            int yaratilacakGulleId = gulleId;
            if (gulleId == 10)
            {
                yaratilacakGulleId = 8;
            }
            if (gulleId == 11)
            {
                yaratilacakGulleId = 9;
            }
            if (gulleId == 12)
            {
                yaratilacakGulleId = 10;
            }
            GameObject gullegameobject = Instantiate(gulleler[yaratilacakGulleId]);
            gullegameobject.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject.GetComponent<GulleHareket>().barutAktifDurum = barutAktiflikDurumu;
            gullegameobject.GetComponent<GulleHareket>().kalkanAktifDurum = kalkanAktiflikDurumu;

            gullegameobject.GetComponent<GulleHareket>().hasar = hasar;
            gullegameobject.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);

            GameObject gullegameobject2 = Instantiate(gulleler[yaratilacakGulleId]);
            gullegameobject2.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject2.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject2.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);


            GameObject gullegameobject3 = Instantiate(gulleler[yaratilacakGulleId]);
            gullegameobject3.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject3.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject3.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);

            GameObject gullegameobject4 = Instantiate(gulleler[yaratilacakGulleId]);
            gullegameobject4.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject4.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject4.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);

            GameObject gullegameobject5 = Instantiate(gulleler[yaratilacakGulleId]);
            gullegameobject5.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject5.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject5.transform.position = transform.position;
        }
        else if (gulleId == 4)
        {
            GameObject gullegameobject = Instantiate(gulleler[Random.Range(gulleId, gulleId + 3)]);
            float xy = Random.Range(0.03f, 0.04f);
            gullegameobject.transform.localScale = new Vector3(xy, xy, 1);
            gullegameobject.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject.GetComponent<GulleHareket>().hasar = hasar;
            gullegameobject.GetComponent<GulleHareket>().barutAktifDurum = barutAktiflikDurumu;
            gullegameobject.GetComponent<GulleHareket>().kalkanAktifDurum = kalkanAktiflikDurumu;
            gullegameobject.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);
            GameObject gullegameobject2 = Instantiate(gulleler[Random.Range(gulleId, gulleId + 3)]);
            float xy2 = Random.Range(0.03f, 0.04f);
            gullegameobject2.transform.localScale = new Vector3(xy2, xy2, 1);
            gullegameobject2.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject2.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject2.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);
            GameObject gullegameobject3 = Instantiate(gulleler[Random.Range(gulleId, gulleId + 3)]);
            float xy3 = Random.Range(0.03f, 0.04f);
            gullegameobject3.transform.localScale = new Vector3(xy3, xy3, 1);
            gullegameobject3.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject3.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject3.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);
            GameObject gullegameobject4 = Instantiate(gulleler[Random.Range(gulleId, gulleId + 3)]);
            float xy4 = Random.Range(0.03f, 0.04f);
            gullegameobject4.transform.localScale = new Vector3(xy4, xy4, 1);
            gullegameobject4.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject4.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject4.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);
            GameObject gullegameobject5 = Instantiate(gulleler[Random.Range(gulleId, gulleId + 3)]);
            float xy5 = Random.Range(0.03f, 0.04f);
            gullegameobject5.transform.localScale = new Vector3(xy5, xy5, 1);
            gullegameobject5.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject5.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject5.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);
            GameObject gullegameobject6 = Instantiate(gulleler[Random.Range(gulleId, gulleId + 3)]);
            float xy6 = Random.Range(0.03f, 0.04f);
            gullegameobject6.transform.localScale = new Vector3(xy6, xy6, 1);
            gullegameobject6.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject6.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject6.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);
            GameObject gullegameobject7 = Instantiate(gulleler[Random.Range(gulleId, gulleId + 3)]);
            float xy7 = Random.Range(0.03f, 0.04f);
            gullegameobject7.transform.localScale = new Vector3(xy7, xy7, 1);
            gullegameobject7.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject7.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject7.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);
            GameObject gullegameobject8 = Instantiate(gulleler[Random.Range(gulleId, gulleId + 3)]);
            float xy8 = Random.Range(0.03f, 0.04f);
            gullegameobject8.transform.localScale = new Vector3(xy8, xy8, 1);
            gullegameobject8.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject8.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject8.transform.position = transform.position;

            yield return new WaitForSeconds(0.03f);
            GameObject gullegameobject9 = Instantiate(gulleler[Random.Range(gulleId, gulleId + 3)]);
            float xy9 = Random.Range(0.03f, 0.04f);
            gullegameobject9.transform.localScale = new Vector3(xy9, xy9, 1);
            gullegameobject9.GetComponent<GulleHareket>().journeyTime = Random.Range(0.6f, 1f);
            gullegameobject9.GetComponent<GulleHareket>().hedef = Hedef;
            gullegameobject9.transform.position = transform.position;
        }

    }

    public void ZipkinYaratOyuncu(GameObject Hedef, int ZipkinId, int hasar)
    {
        if (ZipkinId == 8)
        {
            ZipkinId = 5;
        }
        GameObject zipkinGameobject = Instantiate(zipkinlar[ZipkinId]);
        zipkinGameobject.GetComponent<ZipkinHareket>().journeyTime = Random.Range(0.6f, 1f);
        zipkinGameobject.GetComponent<ZipkinHareket>().hedef = Hedef;
        zipkinGameobject.GetComponent<ZipkinHareket>().hasar = hasar;
        zipkinGameobject.transform.position = transform.position;
    }

    public void RoketYaratOyuncu(GameObject Hedef, int hasar)
    {
        GameObject roketGameobject = Instantiate(roket);
        roketGameobject.transform.position = transform.position;
        roketGameobject.GetComponent<RoketKontrol>().hedef = Hedef;
        roketGameobject.GetComponent<RoketKontrol>().sahibi = gameObject;
        roketGameobject.GetComponent<RoketKontrol>().hasar = hasar;
    }


    [Command]
    public void TopAktar(int topId, int aktarilacakYer)
    {
#if UNITY_SERVER || UNITY_EDITOR
        string sqlcommand = "";
        if (topId == 5 && aktarilacakYer == 1 && oyuncuDonanilmisTopSayisi < oyuncuMaksTopYuvasi)
        {
            sqlcommand = "Update Kullanici SET CifteVurusTopDepo = CifteVurusTopDepo - 1, CifteVurusTopGemi=CifteVurusTopGemi + 1 WHERE Kullanici_Adi=@kullanici_adi and CifteVurusTopDepo > 0 ;";
        }
        else if (topId == 5 && aktarilacakYer == 0)
        {
            sqlcommand = "Update Kullanici SET CifteVurusTopGemi = CifteVurusTopGemi - 1, CifteVurusTopDepo=CifteVurusTopDepo + 1 WHERE Kullanici_Adi=@kullanici_adi and CifteVurusTopGemi > 0;";
        }
        if (topId == 4 && aktarilacakYer == 1 && oyuncuDonanilmisTopSayisi < oyuncuMaksTopYuvasi)
        {
            sqlcommand = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo - 1, otuzBesKilolukTopGemi=otuzBesKilolukTopGemi + 1 WHERE Kullanici_Adi=@kullanici_adi and otuzBesKilolukTopDepo > 0 ;";
        }
        else if (topId == 4 && aktarilacakYer == 0)
        {
            sqlcommand = "Update Kullanici SET otuzBesKilolukTopGemi = otuzBesKilolukTopGemi - 1, otuzBesKilolukTopDepo=otuzBesKilolukTopDepo + 1 WHERE Kullanici_Adi=@kullanici_adi and otuzBesKilolukTopGemi > 0;";
        }
        else if (topId == 3 && aktarilacakYer == 1 && oyuncuDonanilmisTopSayisi < oyuncuMaksTopYuvasi)
        {
            sqlcommand = "Update Kullanici SET otuzKilolukTopDepo = otuzKilolukTopDepo - 1, otuzKilolukTopGemi=otuzKilolukTopGemi + 1 WHERE Kullanici_Adi=@kullanici_adi and otuzKilolukTopDepo > 0 ;";
        }
        else if (topId == 3 && aktarilacakYer == 0)
        {
            sqlcommand = "Update Kullanici SET otuzKilolukTopGemi = otuzKilolukTopGemi - 1, otuzKilolukTopDepo=otuzKilolukTopDepo + 1 WHERE Kullanici_Adi=@kullanici_adi and otuzKilolukTopGemi > 0;";
        }
        else if (topId == 2 && aktarilacakYer == 1 && oyuncuDonanilmisTopSayisi < oyuncuMaksTopYuvasi)
        {
            sqlcommand = "Update Kullanici SET yirmiYediBucukKGTopDepo = yirmiYediBucukKGTopDepo - 1, yirmiYediBucukKGTopGemi=yirmiYediBucukKGTopGemi + 1 WHERE Kullanici_Adi=@kullanici_adi and yirmiYediBucukKGTopDepo > 0 ;";
        }
        else if (topId == 2 && aktarilacakYer == 0)
        {
            sqlcommand = "Update Kullanici SET yirmiYediBucukKGTopGemi = yirmiYediBucukKGTopGemi - 1, yirmiYediBucukKGTopDepo=yirmiYediBucukKGTopDepo + 1 WHERE Kullanici_Adi=@kullanici_adi and yirmiYediBucukKGTopGemi > 0;";
        }
        else if (topId == 1 && aktarilacakYer == 1 && oyuncuDonanilmisTopSayisi < oyuncuMaksTopYuvasi)
        {
            sqlcommand = "Update Kullanici SET yirmiBeslikKGTopDepo = yirmiBeslikKGTopDepo - 1, yirmiBeslikKGTopGemi=yirmiBeslikKGTopGemi + 1 WHERE Kullanici_Adi=@kullanici_adi and yirmiBeslikKGTopDepo > 0;";
        }
        else if (topId == 1 && aktarilacakYer == 0)
        {
            sqlcommand = "Update Kullanici SET yirmiBeslikKGTopGemi = yirmiBeslikKGTopGemi - 1, yirmiBeslikKGTopDepo=yirmiBeslikKGTopDepo + 1 WHERE Kullanici_Adi=@kullanici_adi and yirmiBeslikKGTopGemi > 0;";
        }
        else if (topId == 0 && aktarilacakYer == 1 && oyuncuDonanilmisTopSayisi < oyuncuMaksTopYuvasi)
        {
            sqlcommand = "Update Kullanici SET onBeslikKGTopDepo = onBeslikKGTopDepo - 1, onBeslikKGTopGemi=onBeslikKGTopGemi + 1 WHERE Kullanici_Adi=@kullanici_adi and onBeslikKGTopDepo > 0;";
        }
        else if (topId == 0 && aktarilacakYer == 0)
        {
            sqlcommand = "Update Kullanici SET onBeslikKGTopGemi = onBeslikKGTopGemi - 1, onBeslikKGTopDepo=onBeslikKGTopDepo + 1 WHERE Kullanici_Adi=@kullanici_adi and onBeslikKGTopGemi > 0;";
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = sqlcommand;
            command.Parameters.AddWithValue("@kullanici_adi", name);
            if (command.ExecuteNonQuery() == 1)
            {

                if (topId == 5 && aktarilacakYer == 1)
                {
                    SetCifteVurusTopDepo(oyuncuCifteVurusTopDepo - 1);
                    SetOyuncuCifteVurusTopGemi(oyuncuCifteVurusTopGemi + 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
                else if (topId == 5 && aktarilacakYer == 0)
                {
                    SetCifteVurusTopDepo(oyuncuCifteVurusTopDepo + 1);
                    SetOyuncuCifteVurusTopGemi(oyuncuCifteVurusTopGemi - 1); ;
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }

                if (topId == 4 && aktarilacakYer == 1)
                {
                    SetOyuncuOtuzBesKilolukTopDepo(oyuncuOtuzBesKilolukTopDepo - 1);
                    SetOyuncuOtuzbesKilolukTopGemi(oyuncuOtuzBesKilolukTopGemi + 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
                else if (topId == 4 && aktarilacakYer == 0)
                {
                    SetOyuncuOtuzBesKilolukTopDepo(oyuncuOtuzBesKilolukTopDepo + 1);
                    SetOyuncuOtuzbesKilolukTopGemi(oyuncuOtuzBesKilolukTopGemi - 1); ;
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
                else if (topId == 3 && aktarilacakYer == 1)
                {
                    SetOyuncuOtuzKilolukTopDepo(oyuncuOtuzKilolukTopDepo - 1);
                    SetOyuncuOtuzKilolukTopGemi(oyuncuOtuzKilolukTopGemi + 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
                else if (topId == 3 && aktarilacakYer == 0)
                {
                    SetOyuncuOtuzKilolukTopDepo(oyuncuOtuzKilolukTopDepo + 1);
                    SetOyuncuOtuzKilolukTopGemi(oyuncuOtuzKilolukTopGemi - 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
                else if (topId == 2 && aktarilacakYer == 1)
                {
                    SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncuYirmiYediBucukKilolukTopDepo - 1);
                    SetOyuncuYirmiYediBucukKilolukTopGemi(oyuncuYirmiYediBucukKilolukTopGemi + 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
                else if (topId == 2 && aktarilacakYer == 0)
                {
                    SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncuYirmiYediBucukKilolukTopDepo + 1);
                    SetOyuncuYirmiYediBucukKilolukTopGemi(oyuncuYirmiYediBucukKilolukTopGemi - 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
                else if (topId == 1 && aktarilacakYer == 1)
                {
                    SetOyuncuYirmiBesKilolukTopDepo(oyuncuYirmiBesKilolukTopDepo - 1);
                    SetOyuncuYirmiBesKilolukTopGemi(oyuncuYirmiBesKilolukTopGemi + 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);

                }
                else if (topId == 1 && aktarilacakYer == 0)
                {
                    SetOyuncuYirmiBesKilolukTopDepo(oyuncuYirmiBesKilolukTopDepo + 1);
                    SetOyuncuYirmiBesKilolukTopGemi(oyuncuYirmiBesKilolukTopGemi - 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
                else if (topId == 0 && aktarilacakYer == 1)
                {
                    SetOyuncuOnBesKilolukTopDepo(oyuncuOnBesKilolukTopDepo - 1);
                    SetOyuncuOnBesKilolukTopGemi(oyuncuOnBesKilolukTopGemi + 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
                else if (topId == 0 && aktarilacakYer == 0)
                {
                    SetOyuncuOnBesKilolukTopDepo(oyuncuOnBesKilolukTopDepo + 1);
                    SetOyuncuOnBesKilolukTopGemi(oyuncuOnBesKilolukTopGemi - 1);
                    SetDonanilmisTopSayisi(oyuncuDonanilmisTopSayisi);
                }
            }
        }
#endif
    }

    //---------------------------------------- Market ve Menü açýlýþýnda bilgileri senkronize eden kýsým --------------------------------------------------------

#if UNITY_SERVER || UNITY_EDITOR
    private void SatinAl(int satinAlinacakOgeID, int adet)
    {
        if (adet > 0)
        {
            bool satinAlmaIslemiDuzgun = false;
            string sqlKodu = "";
            int toplamBedel = 0;
            switch (satinAlinacakOgeID)
            {
                case 12:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 50;
                    sqlKodu = " Update Kullanici SET Altin = Altin - " + toplamBedel + ", onBeslikKGTopDepo = onBeslikKGTopDepo + " + adet + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 13:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 250;
                    sqlKodu = " Update Kullanici SET Altin = Altin - " + toplamBedel + ", yirmiBeslikKGTopDepo = yirmiBeslikKGTopDepo + " + adet + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 14:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 800;
                    sqlKodu = " Update Kullanici SET Altin = Altin - " + toplamBedel + ", yirmiYediBucukKGTopDepo = yirmiYediBucukKGTopDepo + " + adet + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 15:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 8000;
                    sqlKodu = " Update Kullanici SET Altin = Altin - " + toplamBedel + ", otuzKilolukTopDepo = otuzKilolukTopDepo + " + adet + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 16:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 30000;
                    sqlKodu = " Update Kullanici SET Altin = Altin - " + toplamBedel + ", otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + " + adet + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 0:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 25;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", DemirGulle = DemirGulle + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 1:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 80;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", SifaGulle = SifaGulle + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 2:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 100;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", AlevGulle = AlevGulle + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 3:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 150;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", HavaiFisekGulle = HavaiFisekGulle + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 4:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 10;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", barut = barut + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 5:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 10;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", kalkan = kalkan + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 6:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 350;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", roket = roket + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 7:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 20;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", HizTasi = HizTasi + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 8:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 20;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", PaslanmisZipkin = PaslanmisZipkin + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 9:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 80;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", GumusZipkin = GumusZipkin + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 10:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 300;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", AltinZipkin = AltinZipkin + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 11:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 5;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", Tamir = Tamir + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 17:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 110;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", HallowenGulle = HallowenGulle + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 18:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 300;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", HallowenZipkin = HallowenZipkin + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 19:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 130;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", NoelGulle = NoelGulle + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 20:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 600;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", NoelZipkin = NoelZipkin + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 21:
                    if (oyuncuOzelGemiBirSatinalmaDurumu == 0)
                    {
                        satinAlmaIslemiDuzgun = true;
                        toplamBedel = adet * 300;
                        sqlKodu = " Update Kullanici SET  LostCoin = LostCoin - " + toplamBedel + ", OzelGemiBirSatinAlinmaDurumu = 1 WHERE Kullanici_Adi=@kullanici_adi and OzelGemiBirSatinAlinmaDurumu = 0 and LostCoin >= " + toplamBedel + ";";
                    }
                    break;
                case 22:
                    if (oyuncuOzelGemiIkiSatinalmaDurumu == 0)
                    {
                        satinAlmaIslemiDuzgun = true;
                        toplamBedel = adet * 300;
                        sqlKodu = " Update Kullanici SET  LostCoin = LostCoin - " + toplamBedel + ", OzelGemiIkiSatinAlinmaDurumu = 1 WHERE Kullanici_Adi=@kullanici_adi and OzelGemiIkiSatinAlinmaDurumu = 0 and LostCoin >= " + toplamBedel + ";";
                    }
                    break;
                case 23:
                    if (oyuncuOzelGemiUcSatinalmaDurumu == 0)
                    {
                        satinAlmaIslemiDuzgun = true;
                        toplamBedel = adet * 300;
                        sqlKodu = " Update Kullanici SET  LostCoin = LostCoin - " + toplamBedel + ", OzelGemiUcSatinAlinmaDurumu = 1 WHERE Kullanici_Adi=@kullanici_adi and OzelGemiUcSatinAlinmaDurumu = 0 and LostCoin >= " + toplamBedel + ";";
                    }
                    break;
                case 24:
                    if (oyuncuOzelGemiDortSatinalmaDurumu == 0)
                    {
                        satinAlmaIslemiDuzgun = true;
                        toplamBedel = adet * 300;
                        sqlKodu = " Update Kullanici SET  LostCoin = LostCoin - " + toplamBedel + ", OzelGemiDortSatinAlinmaDurumu = 1 WHERE Kullanici_Adi=@kullanici_adi and OzelGemiDortSatinAlinmaDurumu = 0 and LostCoin >= " + toplamBedel + ";";
                    }
                    break;
                case 25:
                    if (oyuncuOzelGemiBesSatinalmaDurumu == 0)
                    {
                        satinAlmaIslemiDuzgun = true;
                        toplamBedel = adet * 300;
                        sqlKodu = " Update Kullanici SET  LostCoin = LostCoin - " + toplamBedel + ", OzelGemiBesSatinAlinmaDurumu = 1 WHERE Kullanici_Adi=@kullanici_adi and OzelGemiBesSatinAlinmaDurumu = 0 and LostCoin >= " + toplamBedel + ";";
                    }
                    break;
                case 26:
                    if (oyuncuOzelGemiAltiSatinalmaDurumu == 0)
                    {
                        satinAlmaIslemiDuzgun = true;
                        toplamBedel = adet * 300;
                        sqlKodu = " Update Kullanici SET  LostCoin = LostCoin - " + toplamBedel + ", OzelGemiAltiSatinAlinmaDurumu = 1 WHERE Kullanici_Adi=@kullanici_adi and OzelGemiAltiSatinAlinmaDurumu = 0 and LostCoin >= " + toplamBedel + ";";
                    }
                    break;
                case 27:
                    if (oyuncuOzelGemiYediSatinalmaDurumu == 0)
                    {
                        satinAlmaIslemiDuzgun = true;
                        toplamBedel = adet * 300;
                        sqlKodu = " Update Kullanici SET  LostCoin = LostCoin - " + toplamBedel + ", OzelGemiYediSatinAlinmaDurumu = 1 WHERE Kullanici_Adi=@kullanici_adi and OzelGemiYediSatinAlinmaDurumu = 0 and LostCoin >= " + toplamBedel + ";";
                    }
                    break;
                case 28:
                    if (oyuncuOzelGemiSekizSatinalmaDurumu == 0)
                    {
                        satinAlmaIslemiDuzgun = true;
                        toplamBedel = adet * 300;
                        sqlKodu = " Update Kullanici SET  LostCoin = LostCoin - " + toplamBedel + ", OzelGemiSekizSatinAlinmaDurumu = 1 WHERE Kullanici_Adi=@kullanici_adi and OzelGemiSekizSatinAlinmaDurumu = 0 and LostCoin >= " + toplamBedel + ";";
                    }
                    break;
                case 29:
                    if (oyuncuOzelGemiDokuzSatinalmaDurumu == 0)
                    {
                        satinAlmaIslemiDuzgun = true;
                        toplamBedel = adet * 300;
                        sqlKodu = " Update Kullanici SET  LostCoin = LostCoin - " + toplamBedel + ", OzelGemDokuzSatinAlinmaDurumu = 1 WHERE Kullanici_Adi=@kullanici_adi and OzelGemDokuzSatinAlinmaDurumu = 0 and LostCoin >= " + toplamBedel + ";";
                    }
                    break;
                case 30:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 70000;
                    sqlKodu = " Update Kullanici SET Altin = Altin - " + toplamBedel + ", CifteVurusTopDepo = CifteVurusTopDepo + " + adet + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 31:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 130;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", KalpliGulle = KalpliGulle + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
                case 32:
                    satinAlmaIslemiDuzgun = true;
                    toplamBedel = adet * 600;
                    sqlKodu = " Update Kullanici SET  Altin = Altin - " + toplamBedel + ", KalpliZipkin = KalpliZipkin + " + adet * 100 + "  WHERE Kullanici_Adi=@kullanici_adi and Altin >= " + toplamBedel + ";";
                    break;
            }
            if (satinAlmaIslemiDuzgun)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlKodu;
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        // oyuncu satýn alýmý baþarýyla gerçekleþtirdiðinde çalýþacak kodlar
                        SetOyuncuAltin(oyuncuAltin - toplamBedel);
                        switch (satinAlinacakOgeID)
                        {

                            case 0:
                                SetOyuncuDemirGullesi(oyuncuDemirGulle + (adet * 100));
                                break;
                            case 1:
                                SetOyuncuSifaGullesi(oyuncuSifaGulle + (adet * 100));
                                break;
                            case 2:
                                SetOyuncuAlevGullesi(oyuncuAlevGulle + (adet * 100));
                                break;
                            case 3:
                                SetOyuncuHavaiFisek(oyuncuHavaiFisekGulle + (adet * 100));
                                break;
                            case 6:
                                SetOyuncuRoket(oyuncuRoket + (adet * 100));
                                break;
                            case 7:
                                SetOyuncuHiztasi(oyuncuHizTasi + (adet * 100));
                                break;
                            case 4:
                                SetOyuncuBarut(oyuncuBarut + (adet * 100));
                                break;
                            case 5:
                                SetOyuncuKalkan(oyuncuKalkan + (adet * 100));
                                break;
                            case 12:
                                SetOyuncuOnBesKilolukTopDepo(oyuncuOnBesKilolukTopDepo + 1);
                                break;
                            case 13:
                                SetOyuncuYirmiBesKilolukTopDepo(oyuncuYirmiBesKilolukTopDepo + 1);
                                break;
                            case 14:
                                SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncuYirmiYediBucukKilolukTopDepo + 1);
                                break;
                            case 15:
                                SetOyuncuOtuzKilolukTopDepo(oyuncuOtuzKilolukTopDepo + 1);
                                break;
                            case 16:
                                SetOyuncuOtuzBesKilolukTopDepo(oyuncuOtuzBesKilolukTopDepo + 1);
                                break;
                            case 8:
                                SetOyuncuPaslanmisZipkin(oyuncuPaslanmisZipkin + (adet * 100));
                                break;
                            case 9:
                                SetOyuncuGumusZipkin(oyuncuGumusZipkin + (adet * 100));
                                break;
                            case 10:
                                SetOyuncuAltinZipkin(oyuncuAltinZipkin + (adet * 100));
                                break;
                            case 11:
                                SetOyuncuTamir(oyuncuTamir + (adet * 100));
                                break;
                            case 17:
                                SetOyuncuHallowenGullesi(OyuncuHallowenGulle + (adet * 100));
                                break;
                            case 18:
                                SetOyuncuHallowenZipkin(OyuncuHallowenZipkin + (adet * 100));
                                break;
                            case 19:
                                SetOyuncuNoelGullesi(OyuncuNoelGullesi + (adet * 100));
                                break;
                            case 20:
                                SetOyuncuNoelZipkin(OyuncuNoelZipkin + (adet * 100));
                                break;
                            case 21:
                                SetOyuncuLostCoin(oyuncuLostCoin - 300);
                                SetOyuncuOzelGemiBir(oyuncuOzelGemiBirSatinalmaDurumu = 1);
                                break;
                            case 22:
                                SetOyuncuLostCoin(oyuncuLostCoin - 300);
                                SetOyuncuOzelGemiIki(oyuncuOzelGemiIkiSatinalmaDurumu = 1);
                                break;
                            case 23:
                                SetOyuncuLostCoin(oyuncuLostCoin - 300);
                                SetOyuncuOzelGemiUc(oyuncuOzelGemiUcSatinalmaDurumu = 1);
                                break;
                            case 24:
                                SetOyuncuLostCoin(oyuncuLostCoin - 300);
                                SetOyuncuOzelGemiDort(oyuncuOzelGemiDortSatinalmaDurumu = 1);
                                break;
                            case 25:
                                SetOyuncuLostCoin(oyuncuLostCoin - 300);
                                SetOyuncuOzelGemiBes(oyuncuOzelGemiBesSatinalmaDurumu = 1);
                                break;
                            case 26:
                                SetOyuncuLostCoin(oyuncuLostCoin - 300);
                                SetOyuncuOzelGemiAlti(oyuncuOzelGemiAltiSatinalmaDurumu = 1);
                                break;
                            case 27:
                                SetOyuncuLostCoin(oyuncuLostCoin - 300);
                                break;
                            case 28:
                                SetOyuncuLostCoin(oyuncuLostCoin - 300);
                                SetOyuncuOzelGemiSekiz(oyuncuOzelGemiSekizSatinalmaDurumu = 1);
                                break;
                            case 29:
                                SetOyuncuLostCoin(oyuncuLostCoin - 300);
                                SetOyuncuOzelGemiDokuz(oyuncuOzelGemiDokuzSatinalmaDurumu = 1);
                                break;
                            case 30:
                                SetCifteVurusTopDepo(oyuncuCifteVurusTopDepo + 1);
                                break;
                            case 31:
                                SetOyuncuKalpliGulle(KalpliGulle + (adet * 100));
                                break;
                            case 32:
                                SetOyuncuKalpliZipkin(KalpliZipkin + (adet * 100));
                                break;
                        }
                        SatinAlimSeyirDefterineYazdir(satinAlinacakOgeID, adet);
                    }
                    else
                    {
                        HataMesajýDon();
                    }
                }
            }
        }
    }
#endif
    [TargetRpc]
    public void SatinAlimSeyirDefterineYazdir(int satinAlinanOgeID, int adet)
    {
        if (satinAlinanOgeID < 17)
        {
            GameManager.gm.AddItemsSeyirDefteri(adet + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 255] + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 300 + satinAlinanOgeID] + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 256]);
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], adet + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 255] + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 300 + satinAlinanOgeID] + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 256]);
            GameManager.gm.BildirimPaneli.SetActive(true);
        }
        else if (satinAlinanOgeID >= 21 && satinAlinanOgeID <= 29)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 584]);
            GameManager.gm.BildirimPaneli.SetActive(true);
        }
        else if (satinAlinanOgeID == 30)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 585]);
            GameManager.gm.BildirimPaneli.SetActive(true);
        }
        else if (satinAlinanOgeID >30)
        {
            GameManager.gm.BildirimOlustur("BAÞARILI","SatýnAlýndý");
        }
        else
        {
            GameManager.gm.AddItemsSeyirDefteri(adet + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 255] + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 538] + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 256]);
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], adet + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 255] + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 538] + " " + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 256]);
            GameManager.gm.BildirimPaneli.SetActive(true);
        }
       
    }
    [TargetRpc]
    public void HataMesajýDon()
    {
        GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 259]);
    }

    [Command]
    public void SunucuyaSatinAlmaIstegiYolla(int satinAlinacakOgeID, int adet)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (adet > 0)
        {
            SatinAl(satinAlinacakOgeID, adet);
        }
#endif
    }

    [Command]
    public void SunucudaGemiDegistir(int gemiID)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (Can == MaksCan)
        {

            if (gemiID == 0)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 0 WHERE ID=@ID;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(0);
                    }
                }
            }
            else if (gemiID == 1 && oyuncuElitPuan >= 1500)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 1 WHERE ID=@ID and ElitPuan >= 1500;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(1);
                    }
                }
            }
            else if (gemiID == 2 && oyuncuElitPuan >= 7000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 2 WHERE ID=@ID and ElitPuan >= 7000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(2);
                    }
                }
            }
            else if (gemiID == 3 && oyuncuElitPuan >= 20000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 3 WHERE ID=@ID and ElitPuan >= 20000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(3);
                    }
                }
            }
            else if (gemiID == 4 && oyuncuElitPuan >= 100000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 4 WHERE ID=@ID and ElitPuan >= 100000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(4);

                    }
                }
            }
            else if (gemiID == 5 && oyuncuElitPuan >= 300000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 5 WHERE ID=@ID and ElitPuan >= 300000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(5);
                    }
                }
            }
            else if (gemiID == 6 && oyuncuElitPuan >= 700000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 6 WHERE ID=@ID and ElitPuan >= 700000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(6);

                    }
                }
            }
            else if (gemiID == 7 && oyuncuElitPuan >= 1400000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 7 WHERE ID=@ID and ElitPuan >= 1400000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(7);

                    }
                }
            }
            else if (gemiID == 8 && oyuncuElitPuan >= 2800000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 8 WHERE ID=@ID and ElitPuan >= 2800000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(8);

                    }
                }
            }
            else if (gemiID == 9 && oyuncuElitPuan >= 5000000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 9 WHERE ID=@ID and ElitPuan >= 5000000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(9);

                    }
                }
            }
            else if (gemiID == 10 && oyuncuElitPuan >= 9000000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 10 WHERE ID=@ID and ElitPuan >= 9000000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(10);
                    }
                }
            }
            else if (gemiID == 11 && oyuncuElitPuan >= 14000000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 11 WHERE ID=@ID and ElitPuan >= 14000000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(11);
                    }
                }
            }
            else if (gemiID == 12 && oyuncuElitPuan >= 18000000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 12 WHERE ID=@ID and ElitPuan >= 18000000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(12);
                    }
                }
            }
            else if (gemiID == 13 && oyuncuOzelGemiBirSatinalmaDurumu == 1)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 13 WHERE ID=@ID and OzelGemiBirSatinAlinmaDurumu = 1;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(13);
                    }
                }
            }
            else if (gemiID == 14 && oyuncuElitPuan >= 22000000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 14 WHERE ID=@ID and ElitPuan >= 22000000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(14);
                    }
                }
            }
            else if (gemiID == 15 && oyuncuOzelGemiIkiSatinalmaDurumu == 1)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 15 WHERE ID=@ID and OzelGemiIkiSatinAlinmaDurumu = 1;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(15);
                    }
                }
            }
            else if (gemiID == 16 && oyuncuOzelGemiNoelSatinalmaDurumu == 1)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 16 WHERE ID=@ID and NoelGemisiEtkinlikSatinAlinmaDurumu = 1;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(16);
                    }
                }
            }
            else if (gemiID == 17 && oyuncuOzelGemiUcSatinalmaDurumu == 1)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 17 WHERE ID=@ID and OzelGemiUcSatinAlinmaDurumu = 1;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(17);
                    }
                }
            }
            else if (gemiID == 18 && oyuncuOzelGemiDortSatinalmaDurumu == 1)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 18 WHERE ID=@ID and OzelGemiDortSatinAlinmaDurumu = 1;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(18);
                    }
                }
            }
            else if (gemiID == 19 && oyuncuOzelGemiBesSatinalmaDurumu == 1)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 19 WHERE ID=@ID and OzelGemiBesSatinAlinmaDurumu = 1;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(19);
                    }
                }
            }

            else if (gemiID == 20 && oyuncuOzelGemiAltiSatinalmaDurumu == 1)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 20 WHERE ID=@ID and OzelGemiAltiSatinAlinmaDurumu = 1;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(20);
                    }
                }
            }


            else if (gemiID == 21 && oyuncuElitPuan >= 30000000)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 21 WHERE ID=@ID and ElitPuan >= 30000000;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(21);
                    }
                }
            }

            else if (gemiID == 22 && oyuncuOzelGemiSekizSatinalmaDurumu == 1)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 22 WHERE ID=@ID and OzelGemiSekizSatinAlinmaDurumu = 1;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(22);
                    }
                }
            }

            else if (gemiID == 23 && oyuncuOzelGemiDokuzSatinalmaDurumu == 1)
            {
                string sqlcommand = "Update Kullanici SET KullanilanGemiId = 23 WHERE ID=@ID and OzelGemDokuzSatinAlinmaDurumu = 1;";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuKullanilanGemiId(23);
                    }
                }
            }

        }
#endif
    }


    /* [Command]
     public void MarketAc()
     {
         // bu satir bu kodun build alinirken silinmesine yarar böylelikle oyuncular sunucu tabanlý kodlarý göremez
#if UNITY_SERVER || UNITY_EDITOR
         // sunucuda veritabanýna baðlanabildiyse anlamýný taþýr
         using (var command = GameManager.gm.sqliteConnection.CreateCommand())
         {
             // veritabanýnda çalýþmasýný istediðimiz kodu buraya yazariz, parametre kullanmamizin amaci sunucudaki veritabanin güvenliðini arttirmak için
             command.CommandText = "SELECT * FROM Kullanici WHERE Kullanici_Adi=@kullaniciadi;";
             command.Parameters.AddWithValue("@kullaniciadi", name);
             // IdataReader kullanarak veritabanindan veriler okunur
             using (IDataReader reader = command.ExecuteReader())
             {
                 // eger veri okunduysa if blogunun icine girer
                 if (reader.Read())
                 {
                     // okunan veri istenilen degiskene atanir
                     kayik = int.Parse(reader["Kayik"].ToString());
                     elitGemi = int.Parse(reader["ElitPuan"].ToString());
                     TargetRpcMarketAcVeriDondur(kayik,elitGemi);
                 }
                 // ve okuyucu kapatilir boylelikle tekrar kullanmak icin hazir olur
                 reader.Close();
             }
         }
#endif
     }
     [TargetRpc]
     public void TargetRpcMarketAcVeriDondur(int donenKayikVerisi, int donenElitGemiVerisi)
     {
         kayik = donenKayikVerisi;
         elitGemi = donenElitGemiVerisi;
     }
    */
    // gemi kullanma kodlari

    public void GemiKullan(int oldvalue, int newvalue)
    {
        if (!isServerOnly && OyunaGirisYapildimi)
        {
            transform.Find("Gemi").GetComponent<SpriteRenderer>().sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 2];
            GameManager.gm.TershaneKullanilanGemiIMG.sprite = GemiAnimasyon[(oyuncuKullanilanGemiId * 32) + 11];
        }
    }

    [TargetRpc]
    public void TargetAtlamaIstegiSunucu(int donenHarita, Vector2 donenKonum)
    {
        GameManager.gm.CikButtonPVP.SetActive(false);
        GameManager.gm.GirButtonPVP.SetActive(true);
        GameManager.gm.EtkinlikSayaciAndroid.gameObject.SetActive(false);
        GameManager.gm.CikButtonPVE.SetActive(false);
        GameManager.gm.GirButtonPVE.SetActive(true);
        harita = donenHarita;
        GetComponent<NavMeshAgent>().Warp(donenKonum);
        MiniMapKameraAyarla();
#if UNITY_ANDROID
        GameManager.gm.HaritaTXT.text = harita.ToString();
#endif
#if UNITY_STANDALONE_WIN
        GameManager.gm.WinHangiMapTXT.text = harita.ToString();
#endif
        GameManager.gm.KameraOyuncuyaKilitlensin();
        if (donenHarita == 98)
        {
            GameManager.gm.CikButtonPVE.SetActive(true);
            GameManager.gm.GirButtonPVE.SetActive(false);
            GameManager.gm.CikButtonPVP.SetActive(false);
            GameManager.gm.GirButtonPVP.SetActive(true);
        }
        if (donenHarita == 97)
        {
            GameManager.gm.CikButtonPVP.SetActive(true);
            GameManager.gm.GirButtonPVP.SetActive(false);
            GameManager.gm.CikButtonPVE.SetActive(false);
            GameManager.gm.GirButtonPVE.SetActive(true);
        }
       
        
    }

    public void MiniMapKameraAyarla()
    {
        if (harita < 97)
        {
#if UNITY_ANDROID
        GameManager.gm.EtkinlikSayaciAndroid.text = GameManager.gm.EtkinlikHaritaNpcSayac[harita - 1] + "/250";
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.EtkinlikSayaciWin.text = GameManager.gm.EtkinlikHaritaNpcSayac[harita - 1] + "/250";
#endif
        }
        if (harita == 1)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[0];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[0];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(-55.5f, 116.1f, -10);
            GameManager.gm.BuyukMapSt9GemiIcon.SetActive(false);

            GameManager.gm.BuyukMapSt1GemiIcon.SetActive(true);
            GameManager.gm.BuyukMapSt8GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt7GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt6GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt5GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt4GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt3GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt2GemiIcon.SetActive(false);
        }
        else if (harita == 2)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[1];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[1];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(104.4f, 116.1f, -10);
            GameManager.gm.BuyukMapSt9GemiIcon.SetActive(false);

            GameManager.gm.BuyukMapSt2GemiIcon.SetActive(true);
            GameManager.gm.BuyukMapSt8GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt7GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt6GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt5GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt4GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt3GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt1GemiIcon.SetActive(false);

        }
        else if (harita == 3)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[2];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[2];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(264.6f, 116.1f, -10);
            GameManager.gm.BuyukMapSt9GemiIcon.SetActive(false);

            GameManager.gm.BuyukMapSt3GemiIcon.SetActive(true);
            GameManager.gm.BuyukMapSt8GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt7GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt6GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt5GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt4GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt2GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt1GemiIcon.SetActive(false);

        }
        else if (harita == 4)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[3];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[3];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(424.4f, 116.1f, -10);
            GameManager.gm.BuyukMapSt9GemiIcon.SetActive(false);

            GameManager.gm.BuyukMapSt4GemiIcon.SetActive(true);
            GameManager.gm.BuyukMapSt8GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt7GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt6GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt5GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt3GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt2GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt1GemiIcon.SetActive(false);

        }
        else if (harita == 5)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[4];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[4];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(-55.5f, 276.4f, -10);
            GameManager.gm.BuyukMapSt9GemiIcon.SetActive(false);

            GameManager.gm.BuyukMapSt5GemiIcon.SetActive(true);
            GameManager.gm.BuyukMapSt8GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt7GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt6GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt4GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt3GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt2GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt1GemiIcon.SetActive(false);

        }
        else if (harita == 6)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[5];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[5];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(104.4f, 276.4f, -10);
            GameManager.gm.BuyukMapSt9GemiIcon.SetActive(false);

            GameManager.gm.BuyukMapSt6GemiIcon.SetActive(true);
            GameManager.gm.BuyukMapSt8GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt7GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt5GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt4GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt3GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt2GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt1GemiIcon.SetActive(false);
        }
        else if (harita == 7)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[6];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[6];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(264.6f, 276.4f, -10);

            GameManager.gm.BuyukMapSt9GemiIcon.SetActive(false);

            GameManager.gm.BuyukMapSt7GemiIcon.SetActive(true);
            GameManager.gm.BuyukMapSt8GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt6GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt5GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt4GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt3GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt2GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt1GemiIcon.SetActive(false);

        }
        else if (harita == 8)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[7];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[7];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(424.4f, 276.4f, -10);
            GameManager.gm.BuyukMapSt9GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt8GemiIcon.SetActive(true);
            GameManager.gm.BuyukMapSt7GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt6GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt5GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt4GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt3GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt2GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt1GemiIcon.SetActive(false);
        }
        else if (harita == 9)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[8];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[8];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(-55.5f, 436f, -10);
            GameManager.gm.BuyukMapSt9GemiIcon.SetActive(true);
            GameManager.gm.BuyukMapSt8GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt7GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt6GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt5GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt4GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt3GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt2GemiIcon.SetActive(false);
            GameManager.gm.BuyukMapSt1GemiIcon.SetActive(false);
        }
        else if (harita == 97)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[10];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[10];
#endif

#if UNITY_ANDROID
        GameManager.gm.EtkinlikSayaciAndroid.text = GameManager.gm.BaskinHaritasiAdmiralNpcSayacPVP + "/300";
            GameManager.gm.EtkinlikSayaciAndroid.gameObject.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.EtkinlikSayaciWin.text = GameManager.gm.BaskinHaritasiAdmiralNpcSayacPVP + "/300";
            GameManager.gm.EtkinlikSayaciWin.gameObject.SetActive(true);
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(584.4f, 276.4f, -10);
        }
        else if (harita == 98)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[9];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[9];
#endif

#if UNITY_ANDROID
        GameManager.gm.EtkinlikSayaciAndroid.text = GameManager.gm.BaskinHaritasiAdmiralNpcSayacPVE + "/300";
            GameManager.gm.EtkinlikSayaciAndroid.gameObject.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.EtkinlikSayaciWin.text = GameManager.gm.BaskinHaritasiAdmiralNpcSayacPVE + "/300";
            GameManager.gm.EtkinlikSayaciWin.gameObject.SetActive(true);
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(584.4f, 116.1f, -10);
        }
        else if (harita == 99)
        {
#if UNITY_ANDROID
            GameManager.gm.miniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[0];
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinMiniMapArkaPlan.GetComponent<Image>().sprite = GameManager.gm.miniMapArkaPlanlar[0];
#endif
            GameManager.gm.MiniMapCamera.transform.position = new Vector3(-143, 0, -10);
        }
    }

#if UNITY_SERVER || UNITY_EDITOR
    private void SavasPuaniEkle(string oldurulenOyuncuAdi, int eklenecekSavasPuani)
    {
        if (isServer)
        {
            int SavasPuaniSonucu = 0;
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(OldurenAd) FROM GunlukSavasPuaniTablosu WHERE OldurenAd=@kullanici_adi and OldurulenAd=@OldurulenAd;";
                command.Parameters.AddWithValue("@kullanici_adi", oyuncuadi);
                command.Parameters.AddWithValue("@OldurulenAd", oldurulenOyuncuAdi);

                using (IDataReader reader = command.ExecuteReader())
                {
                    // oyuncuyu daha onceden oldurmussek
                    if (reader.Read())
                    {
                        SavasPuaniSonucu = int.Parse(reader[0].ToString());
                    }
                    if (SavasPuaniSonucu < 2)
                    {
                        OldurenKisiyeOdulVer(oldurulenOyuncuAdi, seviye);
                    }
                    else
                    {
                        OldurenKisiyeOdulVer(oldurulenOyuncuAdi, 0);
                    }
                    reader.Close();
                }
            }
            if (SavasPuaniSonucu < 2)
            {
                string sqlcommand = "Update Kullanici SET SavasPuani = SavasPuani + " + eklenecekSavasPuani + " WHERE Kullanici_Adi=@kullanici_adi; INSERT INTO GunlukSavasPuaniTablosu (OldurenAd,OldurulenAd) VALUES (@kullanici_adi,@OldurulenAd);";
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = sqlcommand;
                    command.Parameters.AddWithValue("@kullanici_adi", oyuncuadi);
                    command.Parameters.AddWithValue("@OldurulenAd", oldurulenOyuncuAdi);
                    command.ExecuteNonQuery();
                }
            }
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET BatirilanOyuncu = BatirilanOyuncu + 1 WHERE ID=@ID;";
                command.Parameters.AddWithValue("@ID", oyuncuId);
                command.ExecuteNonQuery();
            }
        }
    }
#endif

    //---------------------------------------- Siralama Ac --------------------------------------------------------

    [Command]
    public void SiralamaYukle(int siralamaId)
    {
#if UNITY_SERVER || UNITY_EDITOR
        // tecrubePuani = 0, savasPuani = 1
        if (siralamaId == 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Kullanici where OyunYetkisiDurumu = 0 ORDER BY TecrubePuani desc";
                using (IDataReader reader = command.ExecuteReader())
                {
                    List<string> kullaniciAdlari = new List<string>();
                    List<string> tecrubePuanlari = new List<string>();
                    int i = 0;
                    while (reader.Read())
                    {
                        kullaniciAdlari.Add(reader["Kullanici_Adi"].ToString());
                        tecrubePuanlari.Add(reader["TecrubePuani"].ToString());
                        i++;
                    }
                    reader.Close();
                    TargetSiralamaYukle(kullaniciAdlari, tecrubePuanlari, i);
                }
            }
        }
        else if (siralamaId == 1)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Kullanici where OyunYetkisiDurumu = 0 ORDER BY SavasPuani desc";
                using (IDataReader reader = command.ExecuteReader())
                {
                    List<string> kullaniciAdlari = new List<string>();
                    List<string> savasPuanlari = new List<string>();
                    int i = 0;
                    while (reader.Read())
                    {
                        kullaniciAdlari.Add(reader["Kullanici_Adi"].ToString());
                        savasPuanlari.Add(reader["SavasPuani"].ToString());

                        i++;
                    }
                    reader.Close();
                    TargetSiralamaYukle(kullaniciAdlari, savasPuanlari, i);
                }
            }
        }
        else if (siralamaId == 2)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Kullanici where OyunYetkisiDurumu = 0 ORDER BY ElitPuan desc ";
                using (IDataReader reader = command.ExecuteReader())
                {
                    List<string> kullaniciAdlari = new List<string>();
                    List<string> elitPuanlari = new List<string>();
                    int i = 0;
                    while (reader.Read())
                    {
                        kullaniciAdlari.Add(reader["Kullanici_Adi"].ToString());
                        elitPuanlari.Add(reader["ElitPuan"].ToString());
                        i++;
                    }
                    reader.Close();
                    TargetSiralamaYukle(kullaniciAdlari, elitPuanlari, i);
                }
            }
        }
        else if (siralamaId == 3)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Filolar ORDER BY FiloTp desc";
                using (IDataReader reader = command.ExecuteReader())
                {
                    List<string> kullaniciAdlari = new List<string>();
                    List<string> tecrubePuanlari = new List<string>();
                    int i = 0;
                    while (reader.Read())
                    {
                        kullaniciAdlari.Add(reader["FiloAd"].ToString());
                        tecrubePuanlari.Add(reader["FiloTp"].ToString());
                        i++;
                    }
                    reader.Close();
                    TargetFiloSiralamaYukle(kullaniciAdlari, tecrubePuanlari, i);
                }
            }
        }
        else if (siralamaId == 4)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Kullanici where OyunYetkisiDurumu = 0 ORDER BY EtkinlikPuani desc";
                using (IDataReader reader = command.ExecuteReader())
                {
                    List<string> kullaniciAdlari = new List<string>();
                    List<int> etkinlikpuani = new List<int>();
                    int i = 0;
                    while (reader.Read())
                    {
                        kullaniciAdlari.Add(reader["Kullanici_Adi"].ToString());
                        etkinlikpuani.Add(int.Parse(reader["EtkinlikPuani"].ToString()));
                        i++;
                    }
                    reader.Close();
                    TargetEtkinlikSiralamaYukle(kullaniciAdlari, etkinlikpuani, i);
                }
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetFiloSiralamaYukle(List<string> donenFiloAdlari, List<string> donenPuanlar, int donenUyeSayisi)
    {
        foreach (Transform child in GameManager.gm.FiloSiralamasicontent.transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.gm.FiloSiralamasiitemList.Clear();
        GameManager.gm.FiloSiralamasicontentHeight = 0;
        for (int i = 0; i < donenUyeSayisi; i++)
        {
            GameManager.gm.AddItemsFiloSiralama(donenFiloAdlari[i], donenPuanlar[i], donenUyeSayisi);
        }
        GameManager.gm.Sira = 0;
    }


    [TargetRpc]
    public void TargetSiralamaYukle(List<string> donenKullaniciAdlari, List<string> donenPuanlar, int donenUyeSayisi)
    {
        foreach (Transform child in GameManager.gm.MenuSiralamacontent.transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.gm.MenuSiralamaitemList.Clear();
        GameManager.gm.MenuSiralamacontentHeight = 0;
        for (int i = 0; i < donenUyeSayisi; i++)
        {
            GameManager.gm.AddItemsMenuSiralama(donenKullaniciAdlari[i], donenPuanlar[i], donenUyeSayisi);
        }
        GameManager.gm.Sira = 0;
    }


    [TargetRpc]
    public void TargetEtkinlikSiralamaYukle(List<string> donenKullaniciAdlari, List<int> donenPuanlar, int donenUyeSayisi)
    {
        GameManager.gm.EtkinlikSira = 0;
        for (int i = 0; i < donenUyeSayisi; i++)
        {
            if (donenKullaniciAdlari[i] == oyuncuadi)
            {
                GameManager.gm.EtkinlikKendiSýramAd.text = donenKullaniciAdlari[i].ToString();
                GameManager.gm.EtkinlikKendiSýramPuan.text = donenPuanlar[i].ToString();
                GameManager.gm.EtkinlikKendiSýram.text = (GameManager.gm.EtkinlikSira + 1).ToString();
                SetOyuncuEtkinlikPuani(donenPuanlar[i]);
            }
            if (GameManager.gm.EtkinlikSira < 4)
            {
                if (GameManager.gm.EtkinlikSira == 0)
                {
                    GameManager.gm.EtkinlikBirincisiAd.text = donenKullaniciAdlari[i].ToString();
                    GameManager.gm.EtkinlikBirincisiPuan.text = donenPuanlar[i].ToString();
                }
                else if (GameManager.gm.EtkinlikSira == 1)
                {
                    GameManager.gm.EtkinlikÝkincisiAd.text = donenKullaniciAdlari[i].ToString();
                    GameManager.gm.EtkinlikÝkincisiPuan.text = donenPuanlar[i].ToString();
                }
                else if (GameManager.gm.EtkinlikSira == 2)
                {
                    GameManager.gm.EtkinlikUcuncuAd.text = donenKullaniciAdlari[i].ToString();
                    GameManager.gm.EtkinlikUcuncuPuan.text = donenPuanlar[i].ToString();
                }
            }
            GameManager.gm.EtkinlikSira++;
        }
    }

    /*[TargetRpc]
    public void TargetOyuncuIsinla(NetworkConnection target, int donenHarita, Vector2 donenKonum)
    {
        harita = donenHarita;
        GetComponent<NavMeshAgent>().Warp(donenKonum);
        MiniMapKameraAyarla();
#if UNITY_ANDROID
        GameManager.gm.HaritaTXT.text = harita.ToString();
#endif
#if UNITY_STANDALONE_WIN
        GameManager.gm.WinHangiMapTXT.text = harita.ToString();
#endif
        GameManager.gm.KameraOyuncuyaKilitlensin();
    }*/

    public void HizKontrol(float oldvalue, float newvalue)
    {
        hizTasiKullanildiAnimasyonFrameGecikmesi = 1;
        myAgent.speed = hiz;
    }

    public void HizTasiAnimasyonAktiflikDurumu(bool oldvalue, bool newvalue)
    {
        gameObject.transform.Find("Gemi/HizTasiAnim").gameObject.SetActive(hizTasiAnimasyonDurumu);
    }

    [Command]
    public void ProfilYukle(string profiliGosterilecekKullaniciAdi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        // filosu yok ise 1 koyulup tekrar kontrol ediliyor
        bool FilosuYokDurumu = false;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM kullanici JOIN Filolar ON kullanici.FiloId = Filolar.FiloId WHERE kullanici.Kullanici_Adi=@kullaniciadi LIMIT 1;";
            command.Parameters.AddWithValue("@kullaniciadi", profiliGosterilecekKullaniciAdi);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    TargetProfilYukle(profiliGosterilecekKullaniciAdi, int.Parse(reader["TecrubePuani"].ToString()), int.Parse(reader["BatirilanBot"].ToString()), int.Parse(reader["AdamaVerilenHasar"].ToString()), int.Parse(reader["BatirilanOyuncu"].ToString()), int.Parse(reader["ToplananSandik"].ToString()), int.Parse(reader["YapilanGorevler"].ToString()), int.Parse(reader["SavasPuani"].ToString()), reader["FiloAd"].ToString(), int.Parse(reader["FiloId"].ToString()), System.
                    DateTime.Parse(reader["KayitTarihi"].ToString()).ToString("dd/MM/yyyy"));
                }
                else
                {
                    FilosuYokDurumu = true;
                }
                reader.Close();
            }
            if (FilosuYokDurumu)
            {
                command.CommandText = "SELECT * FROM kullanici Where Kullanici_Adi=@kullaniciadi LIMIT 1;";
                command.Parameters.AddWithValue("@kullaniciadi", profiliGosterilecekKullaniciAdi);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        TargetProfilYukle(profiliGosterilecekKullaniciAdi, int.Parse(reader["TecrubePuani"].ToString()), int.Parse(reader["BatirilanBot"].ToString()), int.Parse(reader["AdamaVerilenHasar"].ToString()), int.Parse(reader["BatirilanOyuncu"].ToString()), int.Parse(reader["ToplananSandik"].ToString()), int.Parse(reader["YapilanGorevler"].ToString()), int.Parse(reader["SavasPuani"].ToString()), "YOK", -1, System.DateTime.Parse(reader["KayitTarihi"].ToString()).ToString("dd/MM/yyyy"));
                    }

                    reader.Close();
                }
            }

        }
#endif
    }

    [TargetRpc]
    public void TargetProfilYukle(string donenKullaniciAdi, int donenTecrubePuan, int donenBatirilanBot, int donenOyuncularaVerilenHasar, int donenBatirilanOyuncu, int donenToplananSandikSayisi, int donenYapilanGorevSayisi, int donenSavasPuani, string donenfiloAd, int donenfiloýd, string kayitTarihi)
    {
        GameManager.gm.KayitTarihi.text = kayitTarihi.ToString();
        GameManager.gm.ProfilOyuncuAdi.text = donenKullaniciAdi.ToString();
        GameManager.gm.ProfilTecrubePuani.text = donenTecrubePuan.ToString();
        GameManager.gm.BatirilanBot.text = donenBatirilanBot.ToString();
        GameManager.gm.AdamaVerilenHasar.text = donenOyuncularaVerilenHasar.ToString();
        GameManager.gm.BatirilanOyuncu.text = donenBatirilanOyuncu.ToString();
        GameManager.gm.ToplananSandik.text = donenToplananSandikSayisi.ToString();
        GameManager.gm.YapilanGorevler.text = donenYapilanGorevSayisi.ToString();
        GameManager.gm.SavasPuani.text = donenSavasPuani.ToString();
        if (donenfiloýd == -1)
        {
            GameManager.gm.ProfilOyuncuFiloAdi.text = "YOK";
        }
        GameManager.gm.ProfilOyuncuFiloAdi.text = donenfiloAd;

    }

    [Command]
    public void OyuncuBarutAcKapat()
    {
        SetOyuncuBarutDurum(!oyuncuBarutDurum);
    }

    public void SetOyuncuBarutDurum(bool deger)
    {
        oyuncuBarutDurum = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuBarutDurum(oyuncuBarutDurum);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuBarutDurum == false)
            {
                GameManager.gm.barutAcKapatButton.GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
                GameManager.gm.barutAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
                GameManager.gm.WindowsbarutAcKapatButton.GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
                GameManager.gm.WindowsbarutAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
            }
            else
            {
                GameManager.gm.barutAcKapatButton.GetComponent<Image>().color = new(1, 1, 1);
                GameManager.gm.barutAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(1, 1, 1);
                GameManager.gm.WindowsbarutAcKapatButton.GetComponent<Image>().color = new(1, 1, 1);
                GameManager.gm.WindowsbarutAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(1, 1, 1);
            }

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuBarutDurum(bool donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuBarutDurum(donenVeri);
        }
    }

    [Command]
    public void OyuncuKalkanAcKapat()
    {
        SetOyuncuKalkanDurum(!oyuncuKalkanDurum);
    }

    public void SetOyuncuKalkanDurum(bool deger)
    {
        oyuncuKalkanDurum = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuKalkanDurum(oyuncuKalkanDurum);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuKalkanDurum == false)
            {
                GameManager.gm.kalkanAcKapatButton.GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
                GameManager.gm.kalkanAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
                GameManager.gm.WindowskalkanAcKapatButton.GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
                GameManager.gm.WindowskalkanAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);

            }
            else
            {
                GameManager.gm.kalkanAcKapatButton.GetComponent<Image>().color = new(1, 1, 1);
                GameManager.gm.kalkanAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(1, 1, 1);
                GameManager.gm.WindowskalkanAcKapatButton.GetComponent<Image>().color = new(1, 1, 1);
                GameManager.gm.WindowskalkanAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(1, 1, 1);
            }

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuKalkanDurum(bool donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuKalkanDurum(donenVeri);
        }
    }

    [Command]
    public void OyuncuRoketAcKapat()
    {
        SetOyuncuRoketDurum(!oyuncuRoketDurum);
    }

    public void SetOyuncuRoketDurum(bool deger)
    {
        oyuncuRoketDurum = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuRoketDurum(oyuncuRoketDurum);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuRoketDurum == false)
            {
                GameManager.gm.roketAcKapatButton.GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
                GameManager.gm.roketAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
                GameManager.gm.WindowsroketAcKapatButton.GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
                GameManager.gm.WindowsroketAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(0.388f, 0.388f, 0.388f);
            }
            else
            {
                GameManager.gm.roketAcKapatButton.GetComponent<Image>().color = new(1, 1, 1);
                GameManager.gm.roketAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(1, 1, 1);
                GameManager.gm.WindowsroketAcKapatButton.GetComponent<Image>().color = new(1, 1, 1);
                GameManager.gm.WindowsroketAcKapatButton.transform.GetChild(0).GetComponent<Image>().color = new(1, 1, 1);
            }

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuRoketDurum(bool donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuRoketDurum(donenVeri);
        }
    }


    public void SetOyuncuTamirDurum(bool deger)
    {
        oyuncuTamirDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuTamirDurum(oyuncuTamirDurumu);
        }
#endif
        if (isLocalPlayer)
        {

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuTamirDurum(bool donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuTamirDurum(donenVeri);
        }
    }


    [Command]
    public void SunucuHizTasiKullan()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (Time.time - sonHizTasiKullanilanZaman >= hizTasiKullanmaIcýnGerekliSure && oyuncuHizTasi > 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET HizTasi = HizTasi - 1 WHERE Kullanici_Adi=@kullanici_adi;";
                command.Parameters.AddWithValue("@kullanici_adi", name);
                command.ExecuteNonQuery();
            }
            sonHizTasiKullanilanZaman = Time.time;
            TargetSunucuHizTasiKullan();
            SetOyuncuHiztasi(oyuncuHizTasi - 1);
            StartCoroutine(SunucuHizTasiEtkisiKontrol());
        }
#endif
    }

    [TargetRpc]
    public void TargetSunucuHizTasiKullan()
    {
        GameManager.gm.textColldownHiz.gameObject.SetActive(true);
        GameManager.gm.imageCooldownHiz.gameObject.SetActive(true);
        GameManager.gm.WindowsimageCooldownHiz.gameObject.SetActive(true);
        GameManager.gm.WindowstextColldownHiz.gameObject.SetActive(true);
        GameManager.gm.cooldowntimerHiz = hizTasiKullanmaIcýnGerekliSure;
        GameManager.gm.cooldowntimeHiz = hizTasiKullanmaIcýnGerekliSure;
        GameManager.gm.ÝsCooldownHiz = true;
    }












    public void SetOyuncuYetenekBarut(int deger)
    {
        oyuncuYetenekBarut = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekBarut(oyuncuYetenekBarut);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[12].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekBarut(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekBarut(donenVeri);
        }
    }


    public void SetOyuncuYetenekGemiHizi(int deger)
    {
        oyuncuYetnekGemiHizi = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekGemiHizi(oyuncuYetnekGemiHizi);
            if (oyuncuElitPuan >= 18000000)
            {
                maksHiz = 2.38f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.38f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 14000000)
            {
                maksHiz = 2.34f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.34f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 9000000)
            {
                maksHiz = 2.3f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.3f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 5000000)
            {
                maksHiz = 2.28f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.28f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 2800000)
            {
                maksHiz = 2.24f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.24f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 1400000)
            {
                maksHiz = 2.2f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.2f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 700000)
            {
                maksHiz = 2.15f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.15f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 300000)
            {
                maksHiz = 2.1f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.1f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 100000)
            {
                maksHiz = 2.05f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2.05f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 20000)
            {
                maksHiz = 2f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 2f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 7000)
            {
                maksHiz = 1.95f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 1.95f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 1500)
            {
                maksHiz = 1.9f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 1.9f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            else if (oyuncuElitPuan >= 0)
            {
                maksHiz = 1.8f + (oyuncuYetnekGemiHizi * 0.025f);
                hiz = 1.8f + (oyuncuYetnekGemiHizi * 0.025f);
            }
            HizKontrol(hiz, hiz);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[13].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekGemiHizi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekGemiHizi(donenVeri);
        }
    }

    public void SetOyuncuYetenekHizTasi(int deger)
    {
        oyuncuYetenekHizTasi = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekHizTasi(oyuncuYetenekHizTasi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[14].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekHizTasi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekHizTasi(donenVeri);
        }
    }


    public void SetOyuncuYetenekPVEHasar(int deger)
    {
        oyuncuYetenekPVEHasar = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekPVEHasar(oyuncuYetenekPVEHasar);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[15].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekPVEHasar(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekPVEHasar(donenVeri);
        }
    }


    public void SetOyuncuYetenekZipkinMenzili(int deger)
    {
        oyuncuYetenekZipkinMenzili = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekZipkinMenzili(oyuncuYetenekZipkinMenzili);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[16].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekZipkinMenzili(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekZipkinMenzili(donenVeri);
        }
    }


    public void SetOyuncuYetenekZipkinSaldiriHizi(int deger)
    {
        oyuncuYetenekZipkinSaldiriHizi = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekZipkinSaldiriHizi(oyuncuYetenekZipkinSaldiriHizi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[17].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekZipkinSaldiriHizi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekZipkinSaldiriHizi(donenVeri);
        }
    }


    public void SetOyuncuYetenekMaxCan(int deger)
    {
        oyuncuYetenekMaxCan = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekMaxCan(oyuncuYetenekMaxCan);
            if (oyuncuElitPuan >= 18000000)
            {
                MaksCan = 11000 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 14000000)
            {
                MaksCan = 10500 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 9000000)
            {
                MaksCan = 10000 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 5000000)
            {
                MaksCan = 9500 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 2800000)
            {
                MaksCan = 9000 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 1400000)
            {
                MaksCan = 8500 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 700000)
            {
                MaksCan = 8000 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 300000)
            {
                MaksCan = 7500 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 100000)
            {
                MaksCan = 7000 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 20000)
            {
                MaksCan = 6500 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 7000)
            {
                MaksCan = 6000 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 1500)
            {
                MaksCan = 5500 + (oyuncuYetenekMaxCan * 500);
            }
            else if (oyuncuElitPuan >= 0)
            {
                MaksCan = 5000 + (oyuncuYetenekMaxCan * 500);
            }
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[7].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekMaxCan(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekMaxCan(donenVeri);
        }
    }


    public void SetOyuncuYetnekTamir(int deger)
    {
        oyuncuYetenekTamir = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            tamirMiktari = 300 + (oyuncuYetenekTamir * 100);
            TargetSetOyuncuYetnekTamir(oyuncuYetenekTamir);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[8].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetnekTamir(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetnekTamir(donenVeri);
        }
    }


    public void SetOyuncuYetnekKalkan(int deger)
    {
        oyuncuYetenekKalkani = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetnekKalkan(oyuncuYetenekKalkani);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[9].text = deger + "/10";

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetnekKalkan(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetnekKalkan(donenVeri);
        }
    }


    public void SetOyuncuYetenekMenzil(int deger)
    {
        oyuncuYetenekMenzil = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekMenzil(oyuncuYetenekMenzil);
            OyuncuSaldiriHiziVeMenzilGuncelle();
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[10].text = deger + "/10";

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekMenzil(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekMenzil(donenVeri);
        }
    }


    public void SetOyuncuYetenekZirh(int deger)
    {
        oyuncuYetenekZirh = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekZirh(oyuncuYetenekZirh);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[11].text = deger + "/10";

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekZirh(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekZirh(donenVeri);
        }
    }


    public void SetOyuncuYetenekSaldiriHizi(int deger)
    {
        oyuncuYetenekSaldiriHizi = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            OyuncuSaldiriHiziVeMenzilGuncelle();
            TargetSetOyuncuYetenekSaldiriHizi(oyuncuYetenekSaldiriHizi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[0].text = deger + "/10";

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekSaldiriHizi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekSaldiriHizi(donenVeri);
        }
    }



    public void SetOyuncuYetnekKiritikHasar(int deger)
    {
        oyuncuYetenekKritikHasar = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetnekKiritikHasar(oyuncuYetenekKritikHasar);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[1].text = deger + "/10";

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetnekKiritikHasar(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetnekKiritikHasar(donenVeri);
        }
    }



    public void SetOyuncuYetenekKritikVurusIhtimali(int deger)
    {
        oyuncuYetenekKiritikVurusIhtimali = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekKritikVurusIhtimali(oyuncuYetenekKiritikVurusIhtimali);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[2].text = deger + "/10";

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekKritikVurusIhtimali(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekKritikVurusIhtimali(donenVeri);
        }
    }



    public void SetOyuncuYetenekRoketHasari(int deger)
    {
        oyuncuYetenekRoketHasari = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekRoketHasari(oyuncuYetenekRoketHasari);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[3].text = deger + "/10";

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekRoketHasari(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekRoketHasari(donenVeri);
        }
    }




    public void SetOyuncuYetenekZipkinHasari(int deger)
    {
        oyuncuYetenekZipkinHasari = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekZipkinHasari(oyuncuYetenekZipkinHasari);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[4].text = deger + "/10";

        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekZipkinHasari(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekZipkinHasari(donenVeri);
        }
    }





    public void SetOyuncuYetenekZirhDelme(int deger)
    {
        oyuncuYetenekZirhDelme = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekZirhDelme(oyuncuYetenekZirhDelme);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[5].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekZirhDelme(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekZirhDelme(donenVeri);
        }
    }






    public void SetOyuncuYetenekIsabetOraný(int deger)
    {
        oyuncuYetenekIsabetOrani = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuYetenekIsabetOraný(oyuncuYetenekIsabetOrani);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[6].text = deger + "/10";
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuYetenekIsabetOraný(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuYetenekIsabetOraný(donenVeri);
        }
    }





    public void SetOyuncuKalanYetenekPuaniSayisi(int deger)
    {
        oyuncuKalanYetenekPuaniSayisi = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[18].text = deger.ToString();
            GameManager.gm.YetenekPuanlariSlot[19].text = (oyuncuHarcananYetenekPuaniSayisi + oyuncuKalanYetenekPuaniSayisi) + "/90";
            GameManager.gm.oyuncuSatinalYetenekBedel.text = ((oyuncuHarcananYetenekPuaniSayisi + oyuncuKalanYetenekPuaniSayisi) * 4000).ToString();
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuKalanYetenekPuaniSayisi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuKalanYetenekPuaniSayisi(donenVeri);
        }
    }




    public void SetOyuncuOzelGemiBir(int deger)
    {
        oyuncuOzelGemiBirSatinalmaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOzelGemiBir(oyuncuOzelGemiBirSatinalmaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuOzelGemiBirSatinalmaDurumu == 1)
            {
                GameManager.gm.OzelGemiFiyat[0].SetActive(false);
                GameManager.gm.OzelGemiSatinAlBTN[0].SetActive(false);
                GameManager.gm.OzelGemiKullanBTN[0].SetActive(true);

            } 
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOzelGemiBir(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOzelGemiBir(donenVeri);
        }
    }


    public void SetOyuncuOzelGemiIki(int deger)
    {
        oyuncuOzelGemiIkiSatinalmaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOzelGemiIki(oyuncuOzelGemiIkiSatinalmaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuOzelGemiIkiSatinalmaDurumu == 1)
            {
                GameManager.gm.OzelGemiFiyat[1].SetActive(false);
                GameManager.gm.OzelGemiSatinAlBTN[1].SetActive(false);
                GameManager.gm.OzelGemiKullanBTN[1].SetActive(true);

            }
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOzelGemiIki(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOzelGemiIki(donenVeri);
        }
    }



    public void SetOyuncuOzelGemiUc(int deger)
    {
        oyuncuOzelGemiUcSatinalmaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOzelGemiUc(oyuncuOzelGemiUcSatinalmaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuOzelGemiUcSatinalmaDurumu == 1)
            {
                GameManager.gm.OzelGemiFiyat[2].SetActive(false);
                GameManager.gm.OzelGemiSatinAlBTN[2].SetActive(false);
                GameManager.gm.OzelGemiKullanBTN[2].SetActive(true);

            }
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOzelGemiUc(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOzelGemiUc(donenVeri);
        }
    }


    public void SetOyuncuOzelGemiDort(int deger)
    {
        oyuncuOzelGemiDortSatinalmaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOzelGemiDort(oyuncuOzelGemiDortSatinalmaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuOzelGemiDortSatinalmaDurumu == 1)
            {
                GameManager.gm.OzelGemiFiyat[3].SetActive(false);
                GameManager.gm.OzelGemiSatinAlBTN[3].SetActive(false);
                GameManager.gm.OzelGemiKullanBTN[3].SetActive(true);

            }
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOzelGemiDort(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOzelGemiDort(donenVeri);
        }
    }


    public void SetOyuncuOzelGemiBes(int deger)
    {
        oyuncuOzelGemiBesSatinalmaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOzelGemiBes(oyuncuOzelGemiBesSatinalmaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuOzelGemiBesSatinalmaDurumu == 1)
            {
                GameManager.gm.OzelGemiFiyat[4].SetActive(false);
                GameManager.gm.OzelGemiSatinAlBTN[4].SetActive(false);
                GameManager.gm.OzelGemiKullanBTN[4].SetActive(true);

            }
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOzelGemiBes(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOzelGemiBes(donenVeri);
        }
    }


    public void SetOyuncuOzelGemiAlti(int deger)
    {
        oyuncuOzelGemiAltiSatinalmaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOzelGemiAlti(oyuncuOzelGemiAltiSatinalmaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuOzelGemiAltiSatinalmaDurumu == 1)
            {
                GameManager.gm.OzelGemiFiyat[5].SetActive(false);
                GameManager.gm.OzelGemiSatinAlBTN[5].SetActive(false);
                GameManager.gm.OzelGemiKullanBTN[5].SetActive(true);


            }
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOzelGemiAlti(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOzelGemiAlti(donenVeri);
        }
    }

   

    public void SetOyuncuOzelGemiSekiz(int deger)
    {
        oyuncuOzelGemiSekizSatinalmaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOzelGemiSekiz(oyuncuOzelGemiSekizSatinalmaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuOzelGemiSekizSatinalmaDurumu == 1)
            {
                GameManager.gm.OzelGemiFiyat[7].SetActive(false);
                GameManager.gm.OzelGemiSatinAlBTN[7].SetActive(false);
                GameManager.gm.OzelGemiKullanBTN[7].SetActive(true);

            }
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOzelGemiSekiz(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOzelGemiSekiz(donenVeri);
        }
    }




    public void SetOyuncuOzelGemiDokuz(int deger)
    {
        oyuncuOzelGemiDokuzSatinalmaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOzelGemiDokuz(oyuncuOzelGemiDokuzSatinalmaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuOzelGemiDokuzSatinalmaDurumu == 1)
            {
                GameManager.gm.OzelGemiFiyat[8].SetActive(false);
                GameManager.gm.OzelGemiSatinAlBTN[8].SetActive(false);
                GameManager.gm.OzelGemiKullanBTN[9].SetActive(true);

            }
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOzelGemiDokuz(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOzelGemiDokuz(donenVeri);
        }
    }



    public void SetOyuncuOzelGemiEtkinlikNoel(int deger)
    {
        oyuncuOzelGemiNoelSatinalmaDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuOzelGemiEtkinlikNoel(oyuncuOzelGemiNoelSatinalmaDurumu);
        }
#endif
        if (isLocalPlayer)
        {
            if (oyuncuOzelGemiNoelSatinalmaDurumu == 1)
            {
                GameManager.gm.OzelGemiKullanBTN[8].SetActive(true);

            }
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuOzelGemiEtkinlikNoel(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuOzelGemiEtkinlikNoel(donenVeri);
        }
    }



    public void SetOyuncuHarcananYetenekPuaniSayisi(int deger)
    {
        oyuncuHarcananYetenekPuaniSayisi = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.YetenekPuanlariSlot[19].text = (oyuncuHarcananYetenekPuaniSayisi + oyuncuKalanYetenekPuaniSayisi) + "/90";
            GameManager.gm.YetenekBedeliTXT.text = (oyuncuHarcananYetenekPuaniSayisi * 500).ToString();
            GameManager.gm.oyuncuSatinalYetenekBedel.text = ((oyuncuHarcananYetenekPuaniSayisi + oyuncuKalanYetenekPuaniSayisi) * 4000).ToString();
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuHarcananYetenekPuaniSayisi(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuHarcananYetenekPuaniSayisi(donenVeri);
        }
    }

    public IEnumerator SunucuHizTasiEtkisiKontrol()
    {
        hizTasiAnimasyonDurumu = true;
        if (!isServerOnly)
        {
            HizTasiAnimasyonAktiflikDurumu(hizTasiAnimasyonDurumu, hizTasiAnimasyonDurumu);
        }
        float kullanimOncesiHiz = hiz;
        hiz = hiz * (1.5f + (oyuncuYetenekHizTasi * 0.05f));
        HizKontrol(hiz, hiz);
        yield return new WaitForSeconds(hizTasiEtkiSuresi);
        hiz = kullanimOncesiHiz;
        HizKontrol(hiz, hiz);
        hizTasiAnimasyonDurumu = false;
    }

    [Command]
    public void PromosyonKoduSunucuyaYolla(string yollanacakPromosyonKod)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (seviye >= 3)
        {
            string odulTipi = "";
            int odulAdet = 0;
            int kodId = -1;
            // 0 ise kullanýlmamýþ, 1 ise kullanýlmýþtýr
            int kodKullanilmaDurumu = 0;
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Kodlar WHERE PromosyonKod=@kod;";
                command.Parameters.AddWithValue("@kod", yollanacakPromosyonKod);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        odulTipi = reader["OdulTipi"].ToString();
                        kodId = int.Parse(reader["id"].ToString());
                        odulAdet = int.Parse(reader["OdulAdet"].ToString());
                    }
                    reader.Close();
                }
                // kodu daha önceden kullanmýþmý kontrolü
                command.CommandText = "SELECT COUNT(*) FROM KullanilanKodlar WHERE KodId=@kodId and OyuncuId=@oyuncuId;";
                command.Parameters.AddWithValue("@kodId", kodId);
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        kodKullanilmaDurumu = int.Parse(reader[0].ToString());
                    }
                    reader.Close();
                }
            }
            if (odulAdet > 0 && kodKullanilmaDurumu == 0)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO KullanilanKodlar (KodId,OyuncuId) VALUES (@kodId,@oyuncuId);";
                    command.Parameters.AddWithValue("@kullanici_adi", name);
                    command.Parameters.AddWithValue("@kodId", kodId);
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                        {
                            command2.CommandText = " Update Kullanici set " + odulTipi + " = " + odulTipi + " + " + odulAdet + " where Kullanici_Adi=@kullanici_adi;";
                            command2.Parameters.AddWithValue("@kullanici_adi", name);
                            command2.ExecuteNonQuery();
                        }
                        if (odulTipi == "Altin")
                        {
                            SetOyuncuAltin(oyuncuAltin + odulAdet);
                            TargetPromosyonKoduSunucuyaYolla(odulAdet, 1);
                        }
                        else if (odulTipi == "AlevGulle")
                        {
                            SetOyuncuAlevGullesi(oyuncuAlevGulle + odulAdet);
                            TargetPromosyonKoduSunucuyaYolla(odulAdet, 2);
                        }
                        else if (odulTipi == "HavaiFisekGulle")
                        {
                            SetOyuncuHavaiFisek(oyuncuHavaiFisekGulle + odulAdet);
                            TargetPromosyonKoduSunucuyaYolla(odulAdet, 3);
                        }
                        else if (odulTipi == "LostCoin")
                        {
                            SetOyuncuLostCoin(oyuncuLostCoin + odulAdet);
                            TargetPromosyonKoduSunucuyaYolla(odulAdet, 4);
                        }
                        else if (odulTipi == "AltinZipkin")
                        {
                            SetOyuncuAltinZipkin(oyuncuAltinZipkin + odulAdet);
                            TargetPromosyonKoduSunucuyaYolla(odulAdet, 5);
                        }
                    }
                }
            }
            else
            {
                TargetPromosyonKoduSunucuyaYolla(0, 0);
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetPromosyonKoduSunucuyaYolla(int odulAdet, int odulId)
    {
        if (odulId == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], odulAdet + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 481]);
        }
        else if (odulId == 2)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], odulAdet + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 482]);
        }
        else if (odulId == 3)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], odulAdet + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 483]);
        }
        else if (odulId == 4)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], odulAdet + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 480]);
        }
        else if (odulId == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258],  GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 479]);
        }
    }

    //------------------------------------ FILO ------------------------------------------//
    /// <YETKÝ>
    /// 1 = filo lideri
    /// 2 = filo lideri yardýmcýsý
    /// 3 = subay 
    /// 4 = üye


    [Command]
    public void FiloKurmaIstegiYollaSunucuya(string filoAdi, string filoKisaltmasi, string filoAciklamasi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (filoAdi.Length > 2 && filoAdi.Length < 20 && filoKisaltmasi.Length == 3 && oyuncuAltin >= 1000)
        {
            bool FiloBilgileriKontrol = false;
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloAd,FiloKisaltma FROM Filolar WHERE FiloAd=@filoAd or FiloKisaltma=@filoKisaltma;";
                command.Parameters.AddWithValue("@filoAd", filoAdi);
                command.Parameters.AddWithValue("@filoKisaltma", filoKisaltmasi);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read() == false)
                    {
                        FiloBilgileriKontrol = true;
                    }
                    reader.Close();
                }
            }
            if (oyuncuFiloId == -1 && FiloBilgileriKontrol)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Filolar (FiloAd,FiloKisaltma,FiloAciklama) VALUES (@filoAd,@filoKisaltma,@filoAciklama);";
                    command.Parameters.AddWithValue("@filoAd", filoAdi);
                    command.Parameters.AddWithValue("@filoKisaltma", filoKisaltmasi);
                    command.Parameters.AddWithValue("@filoAciklama", filoAciklamasi);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                        {
                            command2.CommandText = "Update Kullanici SET FiloId=(Select FiloId From Filolar where FiloAd = @filoAd),YetkiId=1, Altin=Altin - 1000 where ID = @ID;";
                            command2.Parameters.AddWithValue("@filoAd", filoAdi);
                            command2.Parameters.AddWithValue("@ID", oyuncuId);

                            if (command2.ExecuteNonQuery() == 1)
                            {
                                SetOyuncuFiloAd(filoAdi);
                                OyuncuFiloKisaltma = filoKisaltmasi;
                                SetOyuncuFiloAciklamasi(filoAciklamasi);
                                using (var command3 = GameManager.gm.sqliteConnection.CreateCommand())
                                {
                                    command3.CommandText = "SELECT FiloId FROM Kullanici WHERE ID=@ID;";
                                    command3.Parameters.AddWithValue("@ID", oyuncuId);
                                    using (IDataReader reader = command3.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            SetOyuncuFiloId(int.Parse(reader["FiloId"].ToString()));
                                        }
                                        reader.Close();
                                    }
                                }
                                using (var command4 = GameManager.gm.sqliteConnection.CreateCommand())
                                {
                                    command4.CommandText = "Delete from FiloBasvurulari where OyuncuAd = @Kullaniciadi;";
                                    command4.Parameters.AddWithValue("@Kullaniciadi", oyuncuadi);
                                    command4.ExecuteNonQuery();
                                }
                                TargetFiloKurmaDenemesiDonus(1);
                                SetOyuncuAltin(oyuncuAltin - 1000);
                            }
                        }
                    }
                }
            }
            else if (FiloBilgileriKontrol == false)
            {
                TargetFiloKurmaDenemesiDonus(0);
            }
        }
#endif
    }
    public void SetOyuncuFiloAd(string deger)
    {
        OyuncuFiloAd = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuFiloAd(OyuncuFiloAd);
        }
#endif
        if (isLocalPlayer)
        {
            filoMuttefikCekSunucu();
            filoDusmanCekSunucu();
            GameManager.gm.FiloIcýFiloAdTXT.text = OyuncuFiloAd;
            // oyuncuda calisacak kodlar
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuFiloAd(string donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuFiloAd(donenVeri);
        }
    }


    public void SetOyuncuFiloAciklamasi(string deger)
    {
        OyuncuFiloAciklama = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuFiloAciklama(OyuncuFiloAciklama);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.FiloiciGenelBakisAciklama.text = OyuncuFiloAciklama;
            // oyuncuda calisacak kodlar
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuFiloAciklama(string donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuFiloAciklamasi(donenVeri);
        }
    }


    [TargetRpc]
    public void TargetFiloKurmaDenemesiDonus(int donenVeri)
    {
        // 0  filo kurma denemsi basarisiz anlamina gelir
        // 1 filo kurma denemesi basarili anlamina gelir
        if (donenVeri == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 267]);
        }
        else if (donenVeri == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 269] + OyuncuFiloAd + "\n" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 270] + OyuncuFiloKisaltma);
            FiloCevrimiciUyeleriCek();
            GameManager.gm.FiloIcý.SetActive(true);
            GameManager.gm.FiloKurulacakKisim.SetActive(false);
            GameManager.gm.ProfilOyuncuFiloAdi.text = OyuncuFiloAd.ToString();
        }
    }

    [Command]
    public void FiloBilgileriniCek()
    {

#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuFiloAd.Length > 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloTp,UyeSayisi FROM Filolar WHERE FiloAd=@filoAd;";
                command.Parameters.AddWithValue("@filoAd", OyuncuFiloAd);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SetFiloTp(int.Parse(reader["FiloTp"].ToString()), int.Parse(reader["UyeSayisi"].ToString()));
                    }
                    reader.Close();
                }
            }
        }
#endif
    }

    public void SetFiloTp(int deger, int uyeSayisi)
    {
        filoTecrubePuan = deger;
        filoUyeSayisi = uyeSayisi;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetFiloTp(filoTecrubePuan, uyeSayisi);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.FiloSeviyesiHesapla(filoTecrubePuan);
        }
    }

    [TargetRpc]
    public void TargetSetFiloTp(int donenVeri, int uyeSayisi)
    {
        if (!isServer)
        {
            SetFiloTp(donenVeri, uyeSayisi);
        }
    }
    public void SetFiloAltin(int deger)
    {
        FiloAltin = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetFiloAltin(FiloAltin);
        }
#endif
        if (isLocalPlayer)
        {
            GameManager.gm.FiloKasasiAltinTxt.text = FiloAltin.ToString();
        }
    }

    [TargetRpc]
    public void TargetSetFiloAltin(int donenVeri)
    {
        if (!isServer)
        {
            SetFiloAltin(donenVeri);
        }
    }

    [Command]
    public void FiloAramaIstegiYolla(string aranacakDeger, int aranacakDegerTipi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (aranacakDegerTipi == 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                int[] filoId = new int[50];
                string[] filoAd = new string[50];
                string[] filoKisaltma = new string[50];
                string[] filoUyeSayisi = new string[50];
                command.CommandText = "SELECT * FROM Filolar WHERE FiloKisaltma like @filoKisaltma;";
                command.Parameters.AddWithValue("@filoKisaltma", "%" + aranacakDeger + "%");
                using (IDataReader reader = command.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        filoId[i] = int.Parse(reader["FiloId"].ToString());
                        filoAd[i] = reader["FiloAd"].ToString();
                        filoKisaltma[i] = reader["FiloKisaltma"].ToString();
                        filoUyeSayisi[i] = reader["UyeSayisi"].ToString();
                        i++;
                    }
                    TargetArananFilolar(filoId, filoAd, filoKisaltma, filoUyeSayisi, i);
                    reader.Close();
                }
            }
        }
        else if (aranacakDegerTipi == 1)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                int[] filoId = new int[50];
                string[] filoAd = new string[50];
                string[] filoKisaltma = new string[50];
                string[] filoUyeSayisi = new string[50];
                command.CommandText = "SELECT * FROM Filolar WHERE FiloAd like @fiload;";
                command.Parameters.AddWithValue("@fiload", "%" + aranacakDeger + "%");
                using (IDataReader reader = command.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        filoId[i] = int.Parse(reader["FiloId"].ToString());
                        filoAd[i] = reader["FiloAd"].ToString();
                        filoKisaltma[i] = reader["FiloKisaltma"].ToString();
                        filoUyeSayisi[i] = reader["UyeSayisi"].ToString();
                        i++;
                    }

                    TargetArananFilolar(filoId, filoAd, filoKisaltma, filoUyeSayisi, i);
                    reader.Close();
                }
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetArananFilolar(int[] donenFiloId, string[] donenFiloAd, string[] donenFiloKisaltma, string[] filoUyeSayisi, int gelenVeriSayisi)
    {
        // filolar buraya geliyor burada oyuncudaki listeye yazýlacak
        foreach (Transform child in GameManager.gm.FiloAramacontent.transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.gm.FiloAramaitemList.Clear();
        GameManager.gm.FiloAramacontentHeight = 0;
        for (int i = 0; i < gelenVeriSayisi; i++)
        {
            if (donenFiloId[i] > 0)
            {
                GameManager.gm.AddItemsFiloArama(donenFiloId[i], donenFiloAd[i], donenFiloKisaltma[i], filoUyeSayisi[i], gelenVeriSayisi);
                //  GameManager.gm.FiloAramaSlot[i].transform.Find("KlanUzunÝsimi").GetComponent<Text>().text = donenFiloAd[i];
                // GameManager.gm.FiloAramaSlot[i].transform.Find("FiloKisaAd").GetComponent<Text>().text = donenFiloKisaltma[i];
                // GameManager.gm.FiloAramaSlot[i].transform.Find("KisiSayisi").GetComponent<Text>().text = filoUyeSayisi[i];
            }
        }
    }

    [Command]
    public void FiloBasvuruAtmaIstegiYolla(int filoId)
    {
#if UNITY_SERVER || UNITY_EDITOR
        string OyuncuYeniFiloIstekAtabilecekSaat = "";
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT OyuncuYeniFiloIstekAtabilecekSaat FROM Kullanici WHERE ID=@Id LIMIT 1;";
            command.Parameters.AddWithValue("@Id", oyuncuId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    OyuncuYeniFiloIstekAtabilecekSaat = reader["OyuncuYeniFiloIstekAtabilecekSaat"].ToString();
                }
                reader.Close();
            }
        }


        if (System.DateTime.Parse(OyuncuYeniFiloIstekAtabilecekSaat) <= System.DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm")))
        {
            bool basvuruZatenAtilmis = false;
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM FiloBasvurulari WHERE OyuncuAd=@kullaniciadi;";
                command.Parameters.AddWithValue("@kullaniciadi", oyuncuadi);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read() && reader["OyuncuAd"].ToString() == oyuncuadi && int.Parse(reader["FiloId"].ToString()) == filoId)
                    {
                        basvuruZatenAtilmis = true;
                    }
                    reader.Close();
                }
            }
            if (basvuruZatenAtilmis == false)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO FiloBasvurulari (OyuncuAd,FiloId) VALUES (@oyuncuAd,@filoId);";
                    command.Parameters.AddWithValue("@oyuncuAd", oyuncuadi);
                    command.Parameters.AddWithValue("@filoId", filoId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        TargetFiloBasvuruAtmaIslemiTamamlandi(1, OyuncuYeniFiloIstekAtabilecekSaat);
                    }
                }
            }
            else
            {
                TargetFiloBasvuruAtmaIslemiTamamlandi(0, OyuncuYeniFiloIstekAtabilecekSaat);
            }
        }
        else
        {
            TargetFiloBasvuruAtmaIslemiTamamlandi(2, OyuncuYeniFiloIstekAtabilecekSaat);
        }
#endif
    }
    [TargetRpc]
    public void TargetFiloBasvuruAtmaIslemiTamamlandi(int donenBasvuruSonucu, string donenOyuncuYeniFiloIstekAtabilecekSaat)
    {
        if (donenBasvuruSonucu == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 271]);
        }
        else
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 272], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 273]);
        }
        if (donenBasvuruSonucu == 2)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], System.DateTime.Parse(donenOyuncuYeniFiloIstekAtabilecekSaat) - System.DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm")) + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 554]);
        }
    }

    [Command]
    public void FiloBasvurulariGoster()
    {
#if UNITY_SERVER || UNITY_EDITOR
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            string[] basvuranOyuncuAdlari = new string[5];
            command.CommandText = "SELECT * FROM FiloBasvurulari WHERE FiloId=@filoId limit 10;";
            command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                int i = 0;
                while (reader.Read())
                {
                    basvuranOyuncuAdlari[i] = reader["OyuncuAd"].ToString();
                    i++;
                }
                TargetFiloBasvurulariGoster(basvuranOyuncuAdlari);
                reader.Close();
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetFiloBasvurulariGoster(string[] donenBasvuranOyuncuAdlari)
    {
        for (int i = 0; i < donenBasvuranOyuncuAdlari.Length; i++)
        {
            if (donenBasvuranOyuncuAdlari[i] != null)
            {
                GameManager.gm.FiloBasvurular[i].transform.Find("OyuncuAdi").GetComponent<Text>().text = donenBasvuranOyuncuAdlari[i];
            }
        }
    }

    [Command]
    public void FiloyaBagisYap(int bagisMiktari)
    {
        // Bagis Turu 0 ise altýn
#if UNITY_SERVER || UNITY_EDITOR
        if (oyuncuAltin >= bagisMiktari && bagisMiktari > 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Filolar SET FiloAltin = FiloAltin + @bagisMiktari WHERE FiloId=@filoId and @bagisMiktari <= (Select Altin From Kullanici where ID = @Id);";
                command.Parameters.AddWithValue("@bagisMiktari", bagisMiktari);
                command.Parameters.AddWithValue("@Id", oyuncuId);
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                if (command.ExecuteNonQuery() == 1)
                {
                    using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command2.CommandText = "INSERT INTO FiloBagislari (FiloId,BagisYapanOyuncuAdi,BagisMiktari,BagisYapilanTarih) VALUES (@filoId,@bagisYapanOyuncuAdi,@bagisMiktari,date('now'));";
                        command2.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                        command2.Parameters.AddWithValue("@bagisYapanOyuncuAdi", oyuncuadi);
                        command2.Parameters.AddWithValue("@bagisMiktari", bagisMiktari);
                        command2.ExecuteNonQuery();
                        TargetFiloyaBagisYap(1);
                    }
                    command.CommandText = "Update Kullanici SET Altin = Altin - @bagisMiktari WHERE ID=@ID;";
                    command.Parameters.AddWithValue("@bagisMiktari", bagisMiktari);
                    command.Parameters.AddWithValue("@ID", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuAltin(oyuncuAltin - bagisMiktari);
                    }
                    FiloAltinCek();
                }
                else
                {
                    TargetFiloyaBagisYap(0);
                }
            }
        }
#endif
    }

#if UNITY_SERVER || UNITY_EDITOR
    public void FiloAltinCek()
    {
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT FiloAltin FROM Filolar WHERE FiloId=@FiloId;";
            command.Parameters.AddWithValue("@FiloId", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    SetFiloAltin(int.Parse(reader["FiloAltin"].ToString()));
                }
                reader.Close();
            }
        }
    }
#endif


    [TargetRpc]
    public void TargetFiloyaBagisYap(int donensonuc)
    {
        if (donensonuc == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 274]);
            FiloBagisCek();
        }
        else
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 275]);
        }
    }

    [Command]
    public void FiloBagisCek()
    {
#if UNITY_SERVER || UNITY_EDITOR
        string[] Bagisyapanoyuncuadi = new string[10];
        int[] BagisMiktari = new int[10];
        int i = 0;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT BagisYapanOyuncuAdi,BagisMiktari FROM FiloBagislari WHERE FiloId=@filoId order by Id desc limit 10;";
            command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Bagisyapanoyuncuadi[i] = reader["BagisYapanOyuncuAdi"].ToString();
                    BagisMiktari[i] = int.Parse(reader["BagisMiktari"].ToString());
                    i++;
                }
                TargetFiloBagisCek(BagisMiktari, Bagisyapanoyuncuadi);
                reader.Close();
            }
        }
        FiloAltinCek();
#endif
    }

    [TargetRpc]
    public void TargetFiloBagisCek(int[] DoneBagisMiktari, string[] DonenOyuncuAdi)
    {
        GameManager.gm.AddItemsFiloBagisSeyirDefteri(DoneBagisMiktari, DonenOyuncuAdi);
    }



    [Command]
    public void FilodanOyuncuAt(string atilacakOyuncuAd)
    {
#if UNITY_SERVER || UNITY_EDITOR
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Kullanici SET FiloId = -1,YetkiId= -1 WHERE Kullanici_Adi=@filodanAtilacakOyuncuAd and (((select YetkiId from Kullanici where ID = @ID) = 1 and (select YetkiId from Kullanici where Kullanici_Adi=@filodanAtilacakOyuncuAd) > 1) OR ((select YetkiId from Kullanici where ID = @ID) = 2) and (select YetkiId from Kullanici where Kullanici_Adi=@filodanAtilacakOyuncuAd) > 2);";
            command.Parameters.AddWithValue("@filodanAtilacakOyuncuAd", atilacakOyuncuAd);
            command.Parameters.AddWithValue("@ID", oyuncuId);
            if (command.ExecuteNonQuery() == 1)
            {
                command.CommandText = "Update Filolar Set UyeSayisi = UyeSayisi - 1 where FiloId=@filoId;";
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                command.ExecuteNonQuery();
                TargetFilodanOyuncuAt(1);
            }
            else
            {
                TargetFilodanOyuncuAt(0);
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetFilodanOyuncuAt(int sonuc)
    {
        if (sonuc == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 276], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 277]);
            FiloUyeleriniGoster();
        }
        else
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 276], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 278]);
        }
    }


    [Command]
    public void FilodanCikma()
    {
#if UNITY_SERVER || UNITY_EDITOR
        System.DateTime YeniFiloIstekAtilabilecekTarih = System.DateTime.Now.AddHours(+24);
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Kullanici SET FiloId=-1,OyuncuYeniFiloIstekAtabilecekSaat = @OyuncuYeniFiloIstekAtabilecekSaat, YetkiId=-1 where ID = @oyuncuId and YetkiId > 1;";
            command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
            command.Parameters.AddWithValue("@OyuncuYeniFiloIstekAtabilecekSaat", YeniFiloIstekAtilabilecekTarih.ToString("yyyy-MM-dd HH:mm"));
            if (command.ExecuteNonQuery() == 1)
            {
                command.CommandText = "Update Filolar Set UyeSayisi = UyeSayisi - 1 where FiloId=@filoId;";
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                command.ExecuteNonQuery();
                TargetFilodanCikma();
                SetOyuncuFiloAciklamasi("");
                SetOyuncuFiloAd("");
                OyuncuFiloKisaltma = "";
                SetOyuncuFiloId(-1);
                
            }
            else if (OyuncuYetkiID == 1)
            {
                command.CommandText = "Update Kullanici SET FiloId=-1, YetkiId=-1 where FiloId = @filoId and (select YetkiId from Kullanici where ID = @oyuncuId) = 1;";
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM BarisIstekleri WHERE IstekAtanFiloId=@filoId or IstekAtilanFiloId=@filoId;";
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM DiplomasiMuttefik WHERE MuttefikBirId=@filoId or MuttefikIkiId=@filoId;";
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM DiplomasiSavas WHERE SaldiranFiloId=@filoId or SavunanFiloId=@filoId;";
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM FiloBagislari WHERE FiloId=@filoId;";
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM FiloBasvurulari WHERE FiloId=@filoId;";
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM MuttefikIstekleri WHERE IstekAtanFiloId=@filoId or IstegiKabulEdecekFiloID=@filoId;";
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM Filolar WHERE FiloId=@filoId;";
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                if (command.ExecuteNonQuery() == 1)
                {
                    TargetFilodanCikma();
                    SetOyuncuFiloAciklamasi("");
                    SetOyuncuFiloAd("");
                    OyuncuFiloKisaltma = "";
                    SetOyuncuFiloId(-1);
                }

            }
        }
#endif
    }

    [TargetRpc]
    public void TargetFilodanCikma()
    {
        GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 276], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 281]);
        GameManager.gm.FiloAna.SetActive(false);
        FiloUyeleriniGoster();
    }
    [Command]
    public void FiloyaBasvuranOyuncuyuFiloyaEkle(string filoyaBasvuranOyuncuAdi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2 && filoUyeSayisi < (GameManager.gm.FiloSeviyesiHesapla(filoTecrubePuan) + 49))
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Delete from FiloBasvurulari where OyuncuAd = @oyuncuAd;";
                command.Parameters.AddWithValue("@oyuncuAd", filoyaBasvuranOyuncuAdi);
                command.ExecuteNonQuery();
            }
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET FiloId=@filoId, YetkiId=4 where Kullanici_Adi = @filoyaBasvuranOyuncuAdi and FiloId = -1;";
                command.Parameters.AddWithValue("@filoyaBasvuranOyuncuAdi", filoyaBasvuranOyuncuAdi);
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                if (command.ExecuteNonQuery() == 1)
                {
                    command.CommandText = "Update Filolar Set UyeSayisi = UyeSayisi + 1 where FiloId=@filoId;";
                    command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        TargetFiloyaBasvuranOyuncuyuFiloyaEkle(1);
                    }
                }
            }
        }
        else
        {
            TargetFiloyaBasvuranOyuncuyuFiloyaEkle(0);
        }
#endif
    }

    [TargetRpc]
    public void TargetFiloyaBasvuranOyuncuyuFiloyaEkle(int donen)
    {
        if (donen == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 276], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 283]);
            if (OyuncuYetkiID <= 2)
            {
                FiloBasvurulariniGoster();
            }
            FiloBilgileriniCek();
            FiloUyeleriniGoster();
        }
        else if (donen == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 298]);
        }

    }

    [Command]
    public void FiloyaBasvuranOyuncuyuReddet(string filoyaBasvuranOyuncuAdi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Delete from FiloBasvurulari where OyuncuAd = @Kullaniciadi and FiloId=@filoId;";
                command.Parameters.AddWithValue("@Kullaniciadi", filoyaBasvuranOyuncuAdi);
                command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
                if (command.ExecuteNonQuery() == 1)
                {
                    TargetFiloyaBasvuranOyuncuyuReddet();
                }
            }
        }
       
#endif
    }

    [TargetRpc]
    public void TargetFiloyaBasvuranOyuncuyuReddet()
    {
        GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 276], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 282]);
        if (OyuncuYetkiID <= 2)
        {
            FiloBasvurulariniGoster();
        }
    }

    [Command]
    public void FiloUyeleriniGoster()
    {
#if UNITY_SERVER || UNITY_EDITOR
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            List<string> filoUyeleri = new List<string>();
            List<int> filoUyeleriSeviye = new List<int>();
            List<int> filoUyeleriUnvan = new List<int>();

            command.CommandText = "SELECT * FROM Kullanici WHERE FiloId=@filoId;";
            command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    filoUyeleri.Add(reader["Kullanici_Adi"].ToString());
                    filoUyeleriUnvan.Add(int.Parse(reader["YetkiId"].ToString()));
                    filoUyeleriSeviye.Add(GameManager.gm.OyuncuSeviyesiHesapla(int.Parse(reader["TecrubePuani"].ToString())));
                }
                TargetFiloUyeleriniGoster(filoUyeleri, filoUyeleriUnvan, filoUyeleriSeviye);
                reader.Close();
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetFiloUyeleriniGoster(List<string> donenFiloUyeleri, List<int> donenyetkiID, List<int> donenFiloSeviye)
    {
        filoUyeSayisi = donenFiloUyeleri.Count;
        GameManager.gm.MaksimumUyesayisi.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + filoUyeSayisi + "/" + maxFiloUyeSayisi;
        GameManager.gm.FiloiciUyeleritemCount = donenFiloUyeleri.Count;
        GameManager.gm.FiloiciUyeleritemList = new List<GameObject>();
        GameManager.gm.FiloUyelerininAdlari = new string[donenFiloUyeleri.Count];
        GameManager.gm.AddItems(donenFiloUyeleri, donenFiloSeviye, donenyetkiID);
    }

    public void SetOyuncuFiloId(int deger)
    {
        oyuncuFiloId = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncufiloID(oyuncuFiloId);
        }
#endif
        if (isLocalPlayer)
        {
            // oyuncuda calisacak kodlar
        }
    }

    [TargetRpc]
    public void TargetSetOyuncufiloID(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuFiloId(donenVeri);
        }
    }









    public void SetoyuncuBotDurumu(int deger)
    {
        OyuncuBotDurumu = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetoyuncuBotDurumu(OyuncuBotDurumu);
        }
#endif
        if (isLocalPlayer)
        {
         
        }
    }

    [TargetRpc]
    public void TargetSetoyuncuBotDurumu(int donenVeri)
    {
        if (!isServer)
        {
            SetoyuncuBotDurumu(donenVeri);
        }
    }







    public void SetoyuncuYetkiId(int deger)
    {
        OyuncuYetkiID = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetYetkiId(OyuncuYetkiID);
        }
#endif
        if (isLocalPlayer)
        {
            // oyuncuda calisacak kodlar
        }
    }

    [TargetRpc]
    public void TargetSetYetkiId(int donenVeri)
    {
        if (!isServer)
        {
            SetoyuncuYetkiId(donenVeri);
        }
    }

    [Command]
    public void FiloBasvurulariniGoster()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                List<string> filoBasvurulariOyuncuAdi = new List<string>();
                List<int> filoBasvurulariSeviye = new List<int>();
                command.CommandText = "SELECT * FROM FiloBasvurulari ft JOIN Kullanici kl ON ft.OyuncuAd = kl.Kullanici_Adi where ft.FiloId = @FiloId;";
                command.Parameters.AddWithValue("@FiloId", oyuncuFiloId);
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        filoBasvurulariOyuncuAdi.Add(reader["OyuncuAd"].ToString());
                        filoBasvurulariSeviye.Add(GameManager.gm.OyuncuSeviyesiHesapla(int.Parse(reader["TecrubePuani"].ToString())));
                    }
                    TargetFiloBasvurulariniGoster(filoBasvurulariOyuncuAdi, filoBasvurulariSeviye);
                    reader.Close();
                }
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetFiloBasvurulariniGoster(List<string> donenFiloBascuralarýOyuncuAdi, List<int> donenFiloBasvuruSeviye)
    {
        GameManager.gm.FiloiciUyeleritemCount = donenFiloBascuralarýOyuncuAdi.Count;
        GameManager.gm.FiloiciUyeleritemList = new List<GameObject>();
        GameManager.gm.FiloUyelerininAdlari = new string[donenFiloBascuralarýOyuncuAdi.Count];
        GameManager.gm.AddItemsFiloBasvurular(donenFiloBascuralarýOyuncuAdi, donenFiloBasvuruSeviye);
    }

    [Command]
    public void FiloAciklamasiniDegistir(string yeniAciklama)
    {
#if UNITY_SERVER || UNITY_EDITOR
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Filolar SET FiloAciklama=@filoAciklama where FiloId = @filoId;";
            command.Parameters.AddWithValue("@filoId", oyuncuFiloId);
            command.Parameters.AddWithValue("@filoAciklama", yeniAciklama);
            command.ExecuteNonQuery();
        }
#endif
    }
    //---------------------------- Oyundan Cikis ---------------------------------------//
    [Command]
    public void OyuncuOyunuKapatmaDonusSunucuya()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (Time.time - sonHasarAlinanZaman >= 9f)
        {
            TargetOyuncuOyunuKapatmaDonusSunucuya(1);
            oyuncuCikisYaptýBilgiGuncelle();
            NetworkServer.DestroyPlayerForConnection(GetComponent<NetworkIdentity>().connectionToClient);
            connectionToClient.Disconnect();
        }
        else
        {
            TargetOyuncuOyunuKapatmaDonusSunucuya(2);
        }
#endif
    }

    [TargetRpc]
    public void TargetOyuncuOyunuKapatmaDonusSunucuya(int donus)
    {
        if (donus == 1)
        {
        }
        else
        {
            GameManager.gm.OyundanCýkmaSiyahEkraný.SetActive(false);
        }
    }

    public IEnumerator OduluEkrandaYazdir()
    {
        yield return new WaitForSeconds(5f);
        GameManager.gm.OdulText.GetComponent<Text>().text = "";
        GameManager.gm.OdulText.GetComponent<Text>().text = GameManager.gm.OdulText2.GetComponent<Text>().text;
        GameManager.gm.OdulText2.GetComponent<Text>().text = "";
    }

    public void SetOyuncuId(int deger)
    {
        oyuncuId = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuId(oyuncuId);
        }
#endif
        if (isLocalPlayer)
        {
            // oyuncuda calisacak kodlar
        }
    }
    [TargetRpc]
    public void TargetSetOyuncuId(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuId(donenVeri);
        }
    }
    [Command]
    public void FiloSiralamasi()
    {
#if UNITY_SERVER || UNITY_EDITOR
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT FiloAd,FiloKisaltma,UyeSayisi,FiloTp FROM Filolar ORDER BY FiloTp desc LIMIT 10";
            using (IDataReader reader = command.ExecuteReader())
            {
                string[] kullaniciAdlari = new string[10], tecrubePuanlari = new string[10], filokisaltmasi = new string[10], filoUyeSayisi = new string[10];
                int i = 0;
                while (reader.Read())
                {
                    kullaniciAdlari[i] = reader["FiloAd"].ToString();
                    tecrubePuanlari[i] = reader["FiloTp"].ToString();
                    filokisaltmasi[i] = reader["FiloKisaltma"].ToString();
                    filoUyeSayisi[i] = reader["UyeSayisi"].ToString();
                    i++;
                }
                reader.Close();
                TargetFiloSiralamaYukle(kullaniciAdlari, tecrubePuanlari, filokisaltmasi, filoUyeSayisi);
            }
        }
#endif
    }
    [TargetRpc]
    public void TargetFiloSiralamaYukle(string[] donenKullaniciAdlari, string[] donentecrubePuanlari, string[] donenfilokisaltmasi, string[] donenfiloUyeSayisi)
    {
        for (int i = 0; i < 10; i++)
        {
            // GameManager.gm.AddItemsFiloGenelSiralama(donenKullaniciAdlari, donentecrubePuanlari, donenfilokisaltmasi, donenfiloUyeSayisi);
        }
    }

    [Command]
    public void FiloCevrimiciUyeleriCek()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (oyuncuFiloId > 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT Kullanici_Adi, TecrubePuani FROM Kullanici Where FiloId=@FiloId and AktiflikDurumu = 1 limit 50";
                command.Parameters.AddWithValue("@FiloId", oyuncuFiloId);
                using (IDataReader reader = command.ExecuteReader())
                {
                    string[] kullaniciAdlari = new string[50];
                    int[] tecrubePuanlari = new int[50];
                    int i = 0;
                    while (reader.Read())
                    {
                        kullaniciAdlari[i] = reader["Kullanici_Adi"].ToString();
                        tecrubePuanlari[i] = GameManager.gm.OyuncuSeviyesiHesapla(int.Parse(reader["TecrubePuani"].ToString()));
                        i++;
                    }
                    reader.Close();
                    TargetFiloCevrimiciUyeleriCek(kullaniciAdlari, tecrubePuanlari, i);
                }
            }
        }
#endif
    }
    [TargetRpc]
    public void TargetFiloCevrimiciUyeleriCek(string[] donenKullaniciAdlari, int[] donentecrubePuanlari, int donenuyesayisi)
    {
        for (int i = 0; i < donenuyesayisi; i++)
        {
            GameManager.gm.AddItemsFiloCevrimicicUye(donenKullaniciAdlari, donentecrubePuanlari, donenuyesayisi);
        }
    }

    [Command]
    public void YetkiVer(int gelenYetki, string gelenName)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (gelenYetki + 1 > 1)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET YetkiId = @Yeniyetki WHERE Kullanici_Adi=@kullanici_adi and @Yeniyetki > 1 and (Select YetkiId From Kullanici where ID = @ýd) = 1 and @kullanici_adi != @benimkullaniciadim;";
                command.Parameters.AddWithValue("@kullanici_adi", gelenName);
                command.Parameters.AddWithValue("@Yeniyetki", gelenYetki + 1);
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                command.Parameters.AddWithValue("@benimkullaniciadim", oyuncuadi);
                command.ExecuteNonQuery();
            }
        }
        else if (gelenYetki + 1 == 1)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET YetkiId = @Yeniyetki WHERE Kullanici_Adi=@kullanici_adi and @Yeniyetki = 1 and (Select YetkiId From Kullanici where ID = @ýd) = 1 and @kullanici_adi != @benimkullaniciadim;";
                command.Parameters.AddWithValue("@kullanici_adi", gelenName);
                command.Parameters.AddWithValue("@Yeniyetki", gelenYetki + 1);
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                command.Parameters.AddWithValue("@benimkullaniciadim", oyuncuadi);
                if(command.ExecuteNonQuery() == 1)
                {
                    using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command2.CommandText = "Update Kullanici SET YetkiId = 2 WHERE ID = @ýd;";
                        command2.Parameters.AddWithValue("@ýd", oyuncuId);
                        command2.ExecuteNonQuery();
                        SetoyuncuYetkiId(2);
                    }
                }
            }
        }
       
#endif
    }

    public void oyuncuCikisYaptýBilgiGuncelle()
    {
#if UNITY_SERVER || UNITY_EDITOR
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Kullanici SET AktiflikDurumu = 0, Can = @can WHERE ID=@Id;";
            command.Parameters.AddWithValue("@Id", oyuncuId);
            if(Can > 0)
            {
                command.Parameters.AddWithValue("@can", Can);
            }
            else
            {
                command.Parameters.AddWithValue("@can", (int)(MaksCan * 0.1f));
            }
            command.ExecuteNonQuery();
        }
#endif
    }

    [Command]
    public void SunucuOyuncuyuYenidenDogur()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (harita >= 97)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT Harita FROM Kullanici WHERE ID=@ýd;";
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        harita = int.Parse(reader["Harita"].ToString());
                    }
                    reader.Close();
                }
            }
            Vector2 baslangickonumu = GameManager.gm.NavMeshPosDogurma(harita);
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().Warp(baslangickonumu);
            sonaktiflikzamani = Time.time;
            TargetSunucuOyuncuyuYenidenDogur(baslangickonumu);
            GetComponent<NavMeshAgent>().Warp(GameManager.gm.NavMeshPos(harita));
            target = transform.position;
            GemiHareketEttir(target, target);
            TargetAtlamaIstegiSunucu(harita, new Vector2(transform.position.x, transform.position.y));
            oyuncuBaskinHaritasýnaGirebilir = false;
            StartCoroutine(oyuncuBaskinHaritasiGecisSuresi());
        }
        else
        {
            Vector2 baslangickonumu = GameManager.gm.NavMeshPosDogurma(harita);
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().Warp(baslangickonumu);
            sonaktiflikzamani = Time.time;
            TargetSunucuOyuncuyuYenidenDogur(baslangickonumu);
            oyuncuBaskinHaritasýnaGirebilir = false;
            StartCoroutine(oyuncuBaskinHaritasiGecisSuresi());
        }

#endif
    }

    [TargetRpc]
    public void TargetSunucuOyuncuyuYenidenDogur(Vector2 konum)
    {
        olmeTimer = 10f;
        // oyuncuyu isinliyor
        GameManager.gm.YenidenÝsinlanBTN.SetActive(false);
        GameManager.gm.YenidenÝsinlanTXT.SetActive(false);
        GameManager.gm.oyuncuOlduPanel.SetActive(false);
        transform.Find("OlmeAnimasyon").gameObject.SetActive(false);
        transform.Find("OyuncuAdi").gameObject.SetActive(true);
        transform.Find("OyuncuCanvas").gameObject.SetActive(true);
        transform.Find("MiniMapIcon").gameObject.SetActive(true);
        Color spriteColor = transform.Find("Gemi").GetComponent<SpriteRenderer>().color;
        spriteColor.a = 1f;
        transform.Find("Gemi").GetComponent<SpriteRenderer>().color = spriteColor;
        GetComponent<NavMeshAgent>().Warp(konum);
        GameManager.gm.KameraOyuncuyaKilitlensin();
      

    }

    IEnumerator oyuncuBaskinHaritasiGecisSuresi()
    {
        yield return new WaitForSeconds(900f);
        oyuncuBaskinHaritasýnaGirebilir = true;
    }

    [Command]
    public void SunucudestekTalebiGonder(int konuId, string kullaniciMesaji)
    {
#if UNITY_SERVER || UNITY_EDITOR
        int oyuncuDestekSayac = 0;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT OyuncuDestekTalebiSayac FROM Kullanici WHERE ID=@kullaniciID LIMIT 1;";
            command.Parameters.AddWithValue("@kullaniciID", oyuncuId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    oyuncuDestekSayac = int.Parse(reader["OyuncuDestekTalebiSayac"].ToString());
                }
                reader.Close();
            }
        }
        if (oyuncuDestekSayac < 2)
        {
            if (kullaniciMesaji.Length > 0 && konuId > 0)
            {
                int destekNo = -1;
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO DestekAna(KullaniciId,KonuId) VALUES (@KullaniciId,@KonuId);";
                    command.Parameters.AddWithValue("@KullaniciId", oyuncuId);
                    command.Parameters.AddWithValue("@KonuId", konuId);
                    command.ExecuteNonQuery();
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT max(DestekNo) as DestekNo FROM DestekAna WHERE KullaniciId=@kullaniciID LIMIT 1;";
                    command.Parameters.AddWithValue("@kullaniciID", oyuncuId);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            destekNo = int.Parse(reader["DestekNo"].ToString());
                        }
                        reader.Close();
                    }
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Destek(DestekNo,KullaniciId,Mesaj) VALUES (@DestekNo,@KullaniciId,@Mesaj);";
                    command.Parameters.AddWithValue("@DestekNo", destekNo);
                    command.Parameters.AddWithValue("@KullaniciId", oyuncuId);
                    command.Parameters.AddWithValue("@Mesaj", kullaniciMesaji);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        TargetdestekTalebiGonder(0);
                        using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                        {
                            command2.CommandText = "Update Kullanici SET OyuncuDestekTalebiSayac = OyuncuDestekTalebiSayac + 1 WHERE ID=@ýd;";
                            command2.Parameters.AddWithValue("@ýd", oyuncuId);
                            command2.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        else
        {
            TargetdestekTalebiGonder(1);
        }

        
#endif
    }
    [TargetRpc]
    public void TargetdestekTalebiGonder(int donenDurum)
    {
        if (donenDurum == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 284]);
            GameManager.gm.destekTalebiOlusturINPT.GetComponent<InputField>().text = "";
            GameManager.gm.destekTalebiOlusturDropdown.value = 0;
            GameManager.gm.destekTalebiOlusturBTN.SetActive(false);
            GameManager.gm.destekTalebiOlusturINPT.SetActive(false);
        }
        else
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 555]);
        }

    }

    [Command]
    public void SunucuDestekTalepleriniCek()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (oyuncuId == 16)
        {
            int[] KonuId = new int[50];
            int[] TamamlanmaDurumu = new int[50];
            int[] DestekNo = new int[50];
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT KonuId,TamamlanmaDurumu,DestekNo FROM DestekAna WHERE TamamlanmaDurumu = 0 Limit 50;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        KonuId[i] = int.Parse(reader["KonuId"].ToString());
                        TamamlanmaDurumu[i] = int.Parse(reader["TamamlanmaDurumu"].ToString());
                        DestekNo[i] = int.Parse(reader["DestekNo"].ToString());
                        i++;
                    }
                    reader.Close();
                    TargetDestekTalepleriniCek(KonuId, TamamlanmaDurumu, i, DestekNo);
                }
            }
        }
        else
        {
            int[] KonuId = new int[50];
            int[] TamamlanmaDurumu = new int[50];
            int[] DestekNo = new int[50];
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT KonuId,TamamlanmaDurumu,DestekNo FROM DestekAna WHERE KullaniciId=@kullaniciID Limit 50;";
                command.Parameters.AddWithValue("@kullaniciID", oyuncuId);
                using (IDataReader reader = command.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        KonuId[i] = int.Parse(reader["KonuId"].ToString());
                        TamamlanmaDurumu[i] = int.Parse(reader["TamamlanmaDurumu"].ToString());
                        DestekNo[i] = int.Parse(reader["DestekNo"].ToString());
                        i++;
                    }
                    reader.Close();
                    TargetDestekTalepleriniCek(KonuId, TamamlanmaDurumu, i, DestekNo);
                }
            }
        }

#endif
    }
    [TargetRpc]
    public void TargetDestekTalepleriniCek(int[] DonenkonuId, int[] DonenTamamlanmaDurumu, int DonenTalepSayisi, int[] DonenDestekNo)
    {
        GameManager.gm.AddItemsDestekTalebiDurumu(DonenkonuId, DonenTamamlanmaDurumu, DonenTalepSayisi, DonenDestekNo);
    }

    [Command]
    public void DestekTalebininDetaylariniYukle(int DestekNo, int tamamlanmaDurumu)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (DestekNo > 0)
        {
            string[] mesaj = new string[50];
            int[] kullaniciId = new int[50];
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT Mesaj,KullaniciId FROM Destek WHERE DestekNo=@destekNo LIMIT 50;";
                command.Parameters.AddWithValue("@destekNo", DestekNo);
                using (IDataReader reader = command.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        mesaj[i] = reader["Mesaj"].ToString();
                        kullaniciId[i] = int.Parse(reader["KullaniciId"].ToString());
                        i++;
                    }
                    // ve okuyucu kapatilir boylelikle tekrar kullanmak icin hazir olur
                    reader.Close();
                }
            }
            TargetDestekTalebininDetaylariniYukle(mesaj, kullaniciId, tamamlanmaDurumu);
        }

#endif
    }

    [TargetRpc]
    public void TargetDestekTalebininDetaylariniYukle(string[] donenMesaj, int[] donenKullaniciId, int tamamlanmaDurumu)
    {
        GameManager.gm.AyarlarDestekMesajlasmaAc();
        foreach (Transform child in GameManager.gm.DestekTalebiKullaniciMesajcontent.transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.gm.DestekTalebiKullaniciMesajitemList.Clear();
        GameManager.gm.DestekTalebiKullaniciMesajcontentHeight = 0;
        for (int i = 0; i < donenMesaj.Length - 1; i++)
        {
            if (donenKullaniciId[i] > 0)
            {
                GameManager.gm.AddItemsDestekTalebiKullaniciMesaj(donenMesaj[i], donenKullaniciId[i]);
            }
        }
        if (oyuncuId == 16)
        {
            GameManager.gm.destekTalebiKilitleBTN.SetActive(true);
        }
        else
        {
            GameManager.gm.destekTalebiKilitleBTN.SetActive(false);
        }
        if (tamamlanmaDurumu == 1)
        {
            GameManager.gm.destekTalebiMesajINPT.SetActive(false);
            GameManager.gm.destekTalebiMesajGonderBTN.SetActive(false);
        }
        else if (tamamlanmaDurumu == 0)
        {
            GameManager.gm.destekTalebiMesajINPT.SetActive(true);
            GameManager.gm.destekTalebiMesajGonderBTN.SetActive(true);
        }
    }

    [Command]
    public void SunucudestekMesajGonder(int destekNo, string kullaniciMesaji)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (kullaniciMesaji.Length > 0 && destekNo > 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Destek(DestekNo,KullaniciId,Mesaj) VALUES (@DestekNo,@KullaniciId,@Mesaj);";
                command.Parameters.AddWithValue("@DestekNo", destekNo);
                command.Parameters.AddWithValue("@KullaniciId", oyuncuId);
                command.Parameters.AddWithValue("@Mesaj", kullaniciMesaji);
                if (command.ExecuteNonQuery() == 1)
                {
                    TargetdestekMesajGonder(kullaniciMesaji);
                }
            }
        }
#endif
    }
    [TargetRpc]
    public void TargetdestekMesajGonder(string donenMesaj)
    {
        GameManager.gm.AddItemsDestekTalebiKullaniciMesaj(donenMesaj, oyuncuId);
        GameManager.gm.destekTalebiMesajINPT.GetComponent<InputField>().text = "";
    }

    [Command]
    public void destekKilitle(int destekNo)
    {
#if UNITY_SERVER || UNITY_EDITOR
        int kullaniciId = -1;

        if (oyuncuId == 16)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT kullaniciId FROM DestekAna WHERE DestekNo=@destekNo LIMIT 1;";
                command.Parameters.AddWithValue("@destekNo", destekNo);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        kullaniciId = int.Parse(reader["kullaniciId"].ToString());
                    }
                    reader.Close();
                }
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update DestekAna SET TamamlanmaDurumu = 1 WHERE DestekNo=@DestekNo;";
            command.Parameters.AddWithValue("@DestekNo", destekNo);
            if (command.ExecuteNonQuery() == 1)
            {
                using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command2.CommandText = "Update Kullanici SET OyuncuDestekTalebiSayac = OyuncuDestekTalebiSayac - 1 WHERE ID=@ýd;";
                    command2.Parameters.AddWithValue("@ýd", kullaniciId);
                    command2.ExecuteNonQuery();
                }
            }
        }
        TargetdestekKilitle();
#endif
    }


[TargetRpc]
    public void TargetdestekKilitle()
    {
        GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 285]);
    }

    [Command]
    public void PaketYukle(string transactionID, string odemetarihi, string paketid)
    {
#if UNITY_SERVER || UNITY_EDITOR
        Debug.Log("Paket yüklemeyi deniyor " + oyuncuId);
        if (paketid == "100_lostcoin")
        {
            int odemeBasarili = 0;
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO MobilOdemelerAndroid (transactionID, odemetarihi, OyuncuId) VALUES (@faturaId, @tarih, @oyuncuId);";
                command.Parameters.AddWithValue("@faturaId", transactionID);
                command.Parameters.AddWithValue("@tarih", odemetarihi);
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    odemeBasarili = 1;
                }
            }
            if (odemeBasarili == 1)
            {
                int alinanlostcoin = 100;
                if (GameManager.gm.ikiyeKatlamaAktiflikDurumu)
                {
                    alinanlostcoin = 200;
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET LostCoin = LostCoin + " + alinanlostcoin + " WHERE ID=@oyuncuId;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuLostCoin(oyuncuLostCoin + alinanlostcoin);
                    }
                }
            }
        }
        else if (paketid == "200_lostcoin")
        {
            int odemeBasarili = 0;
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO MobilOdemelerAndroid (transactionID, odemetarihi, OyuncuId) VALUES (@faturaId, @tarih, @oyuncuId);";
                command.Parameters.AddWithValue("@faturaId", transactionID);
                command.Parameters.AddWithValue("@tarih", odemetarihi);
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    odemeBasarili = 1;
                }
            }
            if (odemeBasarili == 1)
            {
                int alinanlostcoin = 200;
                if (GameManager.gm.ikiyeKatlamaAktiflikDurumu)
                {
                    alinanlostcoin = 400;
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET LostCoin = LostCoin + " + alinanlostcoin + " WHERE ID=@oyuncuId;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuLostCoin(oyuncuLostCoin + alinanlostcoin);
                    }
                }
            }
        }
        else if (paketid == "500_lostcoin")
        {
            int odemeBasarili = 0;
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO MobilOdemelerAndroid (transactionID, odemetarihi, OyuncuId) VALUES (@faturaId, @tarih, @oyuncuId);";
                command.Parameters.AddWithValue("@faturaId", transactionID);
                command.Parameters.AddWithValue("@tarih", odemetarihi);
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    odemeBasarili = 1;
                }
            }
            if (odemeBasarili == 1)
            {
                int alinanlostcoin = 500;
                if (GameManager.gm.ikiyeKatlamaAktiflikDurumu)
                {
                    alinanlostcoin = 1000;
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET LostCoin = LostCoin + " + alinanlostcoin + " WHERE ID=@oyuncuId;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuLostCoin(oyuncuLostCoin + alinanlostcoin);
                    }
                }
            }
        }
#endif
    }

    [Command]
    public void OdemeSorunuMobil(string odemeSorunuYazisi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        Debug.Log("------------------------------------------------");
        Debug.Log("Oyuncu Id = " + oyuncuId);
        Debug.Log("Hata Mesajý");
        Debug.Log(odemeSorunuYazisi);
        Debug.Log("------------------------------------------------");
#endif
    }

    [Command]
    public void KayitOl(string kullaniciAdi, string sifre, string eposta, int versionkac)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (GameManager.gm.Version == versionkac)
        {
            if (kullaniciAdi.Length > 4 && kullaniciAdi.Length < 16)
            {
                if (sifre.Length > 4 && sifre.Length < 16)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "SELECT Kullanici_Adi FROM Kullanici WHERE Kullanici_Adi=@kullanici_adi or Mail=@eposta;";
                        command.Parameters.AddWithValue("@kullanici_adi", kullaniciAdi);
                        command.Parameters.AddWithValue("@eposta", eposta);
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TargetKayitOl(0, " ");
                            }
                            else
                            {
                                using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                                {
                                    command2.CommandText = "INSERT INTO Kullanici (Kullanici_Adi,Sifre,KayitTarihi,Mail) VALUES (@kullanici_adi, @sifre, date('now'),@eposta);";
                                    command2.Parameters.AddWithValue("@kullanici_adi", kullaniciAdi);
                                    command2.Parameters.AddWithValue("@sifre", sifre);
                                    command2.Parameters.AddWithValue("@eposta", eposta);
                                    if (command2.ExecuteNonQuery() == 1)
                                    {
                                        // Veritabaný baðlantýsý oluþtur
                                        connectionString = "Server=" + server + ";Database=" + database + ";Uid=" + uid + ";Pwd=" + password + ";";
                                        connection = new MySqlConnection(connectionString);

                                        try
                                        {
                                            // Veritabanýna baðlan
                                            connection.Open();
                                            string insertQuery = "INSERT INTO users (user_username, user_pass, user_mail,user_realname) VALUES (@kullanici_adi, @sifre, @eposta,'Ali Cabbar')";
                                            MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                                            cmd.Parameters.AddWithValue("@kullanici_adi", kullaniciAdi);
                                            cmd.Parameters.AddWithValue("@sifre", sifre);
                                            cmd.Parameters.AddWithValue("@eposta", eposta);
                                            if (cmd.ExecuteNonQuery() == 1)
                                            {
                                                TargetKayitOl(1, kullaniciAdi);
                                            }
                                        }
                                        catch
                                        {
                                            TargetKayitOl(2, " ");
                                        }
                                        finally
                                        {
                                            // Baðlantýyý kapat
                                            connection.Close();
                                        }

                                    }
                                    else
                                    {
                                        TargetKayitOl(2, " ");
                                    }
                                }
                            }
                            reader.Close();
                        }
                    }
                }
                else
                {
                    TargetKayitOl(4, " ");
                }
            }
            else
            {
                TargetKayitOl(3, " ");
            }
        }
        else
        {
            VersionHatamesajýDon();
        }

#endif
    }

    [TargetRpc]
    public void TargetKayitOl(int donusKayitOlma, string donenkullaniciadi)
    {
        if (donusKayitOlma == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 286]);
            GameManager.gm.GirisYapPaneli.SetActive(true);
            GameManager.gm.KayitIOlPaneli.SetActive(false);
            GameManager.gm.kullaniciAdiGirisYapText.text = donenkullaniciadi.ToString();
        }
        else if (donusKayitOlma == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 287]);
        }
        else if (donusKayitOlma == 2)
        {
            // eposta kullanýlýyor

            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 288]);
        }
        else if (donusKayitOlma == 3)
        {
            // kullanýcý adý karakter 
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 343]);
        }
        else if (donusKayitOlma == 4)
        {
            // þifre  karakter 

            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 372]);
        }
    }

    [Command]
    public void MailYollaSunucu(string epostagonderilen)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (epostagonderilen.Length > 0)
        {
            string kullaniciAdi = "";
            string sifre = "";
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT Kullanici_Adi,Sifre FROM Kullanici WHERE Mail=@eposta LIMIT 1;";
                command.Parameters.AddWithValue("@eposta", epostagonderilen);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        kullaniciAdi = reader["Kullanici_Adi"].ToString();
                        sifre = reader["Sifre"].ToString();
                    }
                    reader.Close();
                }
            }

            if (kullaniciAdi.Length > 0)
            {
                TargetSifremiUnuttumSunucu();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");
                SmtpServer.Timeout = 10000;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587;

                mail.From = new MailAddress("Morpes_Games@hotmail.com");
                mail.To.Add(new MailAddress(epostagonderilen));

                mail.Subject = "TO INFORM";
                mail.Body = "Your UserName = " + kullaniciAdi + " || Your Password = " + sifre;

                SmtpServer.Credentials = new System.Net.NetworkCredential("Morpes_Games@hotmail.com", "bofhpkfvuiroqxfu") as ICredentialsByHost; SmtpServer.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                SmtpServer.Send(mail);
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetSifremiUnuttumSunucu()
    {
        GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 299]);
    }

    [Command]
    public void tamirOlBaslat()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (oyuncuTamirDurumu == false && oyuncuTamir > 0 && Can < MaksCan && Time.time - sonTamirEdilenZaman >= tamirHizi && Time.time - sonHasarAlinanZaman > 6)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET Tamir = Tamir - 1 WHERE Kullanici_Adi=@kullanici_adi and Tamir > 0;";
                command.Parameters.AddWithValue("@kullanici_adi", name);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuTamirDurum(true);
                    sonTamirEdilenZaman = Time.time;
                    SetOyuncuTamir(oyuncuTamir - 1);
                }
            }
        }
        sonaktiflikzamani = Time.time;
#endif
    }

    [Command]
    public void PremiumPaketSatinAl(int paketID)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (paketID == 2 && oyuncuLostCoin >= 600 && BaslangicPaket2Durumu == 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET BaslangicPaketi2 = 1, otuzKilolukTopDepo = otuzKilolukTopDepo + 10,otuzBesKilolukTopDepo = otuzBesKilolukTopDepo + 10 ,PaslanmisZipkin = PaslanmisZipkin + 10000,AlevGulle = AlevGulle + 20000,HavaiFisekGulle = HavaiFisekGulle + 15000, Roket = Roket + 2000,Barut = Barut + 3000,Kalkan = Kalkan + 3000,HizTasi = HizTasi + 3000,Tamir = Tamir + 3000,LostCoin = LostCoin - 600,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi + 5,SifaGulle = SifaGulle + 10000 WHERE ID=@ID and LostCoin >= 600 and BaslangicPaketi2 = 0;";
                command.Parameters.AddWithValue("@ID", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuOtuzBesKilolukTopDepo(oyuncuOtuzBesKilolukTopDepo + 10);
                    SetOyuncuOtuzKilolukTopDepo(oyuncuOtuzKilolukTopDepo + 10);
                    SetOyuncuPaslanmisZipkin(oyuncuPaslanmisZipkin + 10000);
                    SetOyuncuAlevGullesi(oyuncuAlevGulle + 20000);
                    SetOyuncuHavaiFisek(oyuncuHavaiFisekGulle + 12000);
                    SetOyuncuRoket(oyuncuRoket + 2000);
                    SetOyuncuHiztasi(oyuncuHizTasi + 3000);
                    SetOyuncuBarut(oyuncuBarut + 3000);
                    SetOyuncuKalkan(oyuncuKalkan + 3000);
                    SetOyuncuTamir(oyuncuTamir + 3000);
                    SetOyuncuSifaGullesi(oyuncuSifaGulle + 10000);
                    SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi + 5);
                    SetOyuncuLostCoin(oyuncuLostCoin - 600);
                    SetBaslangicPaket2Durumu(1);

                    TargetPremiumPaketSatinAl(1);
                }
            }
        }
        else if (paketID == 3 && oyuncuLostCoin >= 100 && OyuncuPremiumDurumu == 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET PermiumDurumu = 1,LostCoin = LostCoin - 100, PremiumTarihi = date('now') WHERE ID=@ID and LostCoin >= 100 and PermiumDurumu = 0;";
                command.Parameters.AddWithValue("@ID", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuPremiumDurumu(1);
                    SetOyuncuLostCoin(oyuncuLostCoin - 100);
                    TargetPremiumPaketSatinAl(1);
                }
            }
        }
        else if (paketID == 4 && oyuncuLostCoin >= 40 && OyuncuSandikKatlamaDurumu == 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET SandikKatlamaDurumu = 1,LostCoin = LostCoin - 40, SandikKatlamaAlimTarihi = @now WHERE ID=@ID and LostCoin >= 40 and SandikKatlamaDurumu = 0;";
                command.Parameters.AddWithValue("@now", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@ID", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuSandikKatlamaDurumu(1);
                    SetOyuncuLostCoin(oyuncuLostCoin - 40);
                    TargetPremiumPaketSatinAl(1);
                }
            }
        }
        else if (paketID == 5 && oyuncuLostCoin >= 20 && OyuncuTpKatlamaDurumu == 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET TpKatlamaDurumu = 1,LostCoin = LostCoin - 20, TpKatlamaTarihi = @now WHERE ID=@ID and LostCoin >= 20 and TpKatlamaDurumu = 0;";
                command.Parameters.AddWithValue("@now", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@ID", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuTpKatlamaDurumu(1);
                    SetOyuncuLostCoin(oyuncuLostCoin - 20);
                    TargetPremiumPaketSatinAl(1);
                }
            }
        }
        else if (paketID == 6 && oyuncuLostCoin >= 20 && OyuncuAltinKatlamaDurumu == 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET AltinKatlamaDurum = 1,LostCoin = LostCoin - 20, AltinKatlamaTarihi = @now WHERE ID=@ID and LostCoin >= 20 and AltinKatlamaDurum = 0;";
                command.Parameters.AddWithValue("@now", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@ID", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltinKatlamaDurumu(1);
                    SetOyuncuLostCoin(oyuncuLostCoin - 20);
                    TargetPremiumPaketSatinAl(1);
                }
            }
        }
        else if (paketID == 7 && oyuncuLostCoin >= 20 )
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET LostCoin = LostCoin - 20, KalpliGulle = KalpliGulle + 25000 WHERE ID=@ID and LostCoin >= 20;";
                command.Parameters.AddWithValue("@ID", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuKalpliGulle(KalpliGulle + 25000);
                    SetOyuncuLostCoin(oyuncuLostCoin - 20);
                    TargetPremiumPaketSatinAl(1);
                }
            }
        }
        else if (paketID == 8 && oyuncuLostCoin >= 100 )
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET LostCoin = LostCoin - 100, Altin = Altin + 100000 WHERE ID=@ID and LostCoin >= 100;";
                command.Parameters.AddWithValue("@ID", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin + 100000);
                    SetOyuncuLostCoin(oyuncuLostCoin - 100);
                    TargetPremiumPaketSatinAl(1);
                }
            }
        }
        else if (paketID == 9 && oyuncuLostCoin >= 200)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET LostCoin = LostCoin - 200, Altin = Altin + 210000 WHERE ID=@ID and LostCoin >= 200;";
                command.Parameters.AddWithValue("@ID", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin + 210000);
                    SetOyuncuLostCoin(oyuncuLostCoin - 200);
                    TargetPremiumPaketSatinAl(1);
                }
            }
        }
        else if (paketID == 10 && oyuncuLostCoin >= 500)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET LostCoin = LostCoin - 500, Altin = Altin + 550000 WHERE ID=@ID and LostCoin >= 500;";
                command.Parameters.AddWithValue("@ID", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin + 550000);
                    SetOyuncuLostCoin(oyuncuLostCoin - 500);
                    TargetPremiumPaketSatinAl(1);
                }
            }
        }
        else
        {
            TargetPremiumPaketSatinAl(0);
        }
#endif
    }

    [TargetRpc]
    public void TargetPremiumPaketSatinAl(int donendurum)
    {
        if (donendurum == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 259]);
        }
        else if (donendurum == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 334]);
        }

    }

#if UNITY_SERVER || UNITY_EDITOR
    public void SureliPaketKontrol()
    {
        if (OyuncuTpKatlamaDurumu == 1 || OyuncuAltinKatlamaDurumu == 1)
        {
            System.DateTime twentyFourHoursAgo = System.DateTime.Now.AddHours(-24);
            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command2.CommandText = "UPDATE Kullanici set TpKatlamaDurumu = 0, TpKatlamaTarihi = null where ID=@ID and TpKatlamaDurumu = 1 and TpKatlamaTarihi < @twentyFourHoursAgo;";
                command2.Parameters.AddWithValue("@ID", oyuncuId);
                command2.Parameters.AddWithValue("@twentyFourHoursAgo", twentyFourHoursAgo.ToString("yyyy-MM-dd HH:mm:ss"));
                if (command2.ExecuteNonQuery() == 1)
                {
                    SetOyuncuTpKatlamaDurumu(0);
                }
            }
            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command2.CommandText = "UPDATE Kullanici set AltinKatlamaDurum = 0, AltinKatlamaTarihi = null where ID=@ID and AltinKatlamaDurum = 1 and AltinKatlamaTarihi < @twentyFourHoursAgo";
                command2.Parameters.AddWithValue("@ID", oyuncuId);
                command2.Parameters.AddWithValue("@twentyFourHoursAgo", twentyFourHoursAgo.ToString("yyyy-MM-dd HH:mm:ss"));
                if (command2.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltinKatlamaDurumu(0);
                }
            }
        }
    }
    public void SureliSandikPaketiKontrol()
    {
        if (OyuncuSandikKatlamaDurumu == 1)
        {
            System.DateTime twentyFourHoursAgo = System.DateTime.Now.AddHours(-168);
            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command2.CommandText = "UPDATE Kullanici set SandikKatlamaDurumu = 0, SandikKatlamaAlimTarihi = null where ID=@ID and SandikKatlamaDurumu = 1 and SandikKatlamaAlimTarihi < @twentyFourHoursAgo;";
                command2.Parameters.AddWithValue("@ID", oyuncuId);
                command2.Parameters.AddWithValue("@twentyFourHoursAgo", twentyFourHoursAgo.ToString("yyyy-MM-dd HH:mm:ss"));
                if (command2.ExecuteNonQuery() == 1)
                {
                    SetOyuncuSandikKatlamaDurumu(0);
                }
            }
        }
    }
#endif

    [Command]
    public void AyarlarNickDegistirmeOnayla(string oyuncuYeniNick, string email)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (oyuncuAltin >= 5000)
        {
            /* using (var command = GameManager.gm.sqliteConnection.CreateCommand())
             {
                 command.CommandText = "SELECT * FROM Kullanici WHERE Kullanici_Adi=@kullaniciadi;";
                 command.Parameters.AddWithValue("@kullaniciadi", name);
                 using (IDataReader reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                         string okunanVeri = reader["kolonAdi"].ToString();
                     }
                     reader.Close();
                 }
             }*/
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET Kullanici_Adi = @YeniOyuncuAdi, Altin = Altin - 5000 WHERE ID=@ID and Mail = @mail and (select count(ID) from Kullanici where Kullanici_Adi = @YeniOyuncuAdi) = 0 and Altin >= 5000;";
                command.Parameters.AddWithValue("@YeniOyuncuAdi", oyuncuYeniNick);
                command.Parameters.AddWithValue("@ID", oyuncuId);
                command.Parameters.AddWithValue("@mail", email);
                if (command.ExecuteNonQuery() == 1)
                {
                    // Veritabaný baðlantýsý oluþtur
                    connectionString = "Server=" + server + ";Database=" + database + ";Uid=" + uid + ";Pwd=" + password + ";";
                    connection = new MySqlConnection(connectionString);

                    try
                    {
                        // Veritabanýna baðlan
                        connection.Open();
                        string insertQuery = "Update users set user_username = @kullanici_adi where user_username=@eskioyuncuadi";
                        MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                        cmd.Parameters.AddWithValue("@kullanici_adi", oyuncuYeniNick);
                        cmd.Parameters.AddWithValue("@eskioyuncuadi", oyuncuadi);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            oyuncuadi = oyuncuYeniNick;
                            SyncOyuncuAdiYazdir(oyuncuadi, oyuncuadi);
                            SetOyuncuAltin(oyuncuAltin - 5000);
                            //degisti durumu
                            TargetAyarlarNickDegistirmeOnayla(1);
                        }
                    }
                    catch
                    {
                        //TargetAyarlarNickDegistirmeOnayla(1);
                    }
                    finally
                    {
                        // Baðlantýyý kapat
                        connection.Close();
                    }
                }
                else
                {
                    // benzerlikdurumu
                    TargetAyarlarNickDegistirmeOnayla(0);
                }
            }
        }
        else
        {
            //altýnyetmiyor
            TargetAyarlarNickDegistirmeOnayla(2);
        }
#endif
    }

    [TargetRpc]
    public void TargetAyarlarNickDegistirmeOnayla(int donen)
    {
        if (donen == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 350]);
        }
        else if (donen == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 351]);
        }
        else if (donen == 2)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 352]);
        }
    }
    [Command]
    public void AyarlarMailDegistirme(string oyuncudandonenmaill)
    {
#if UNITY_SERVER || UNITY_EDITOR
        //kullanýcýnýn mailini çeker
        string okunanVeri = "";
        int mailVarmi = 0;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "select count(Id) from Kullanici where Mail = @mail;";
            command.Parameters.AddWithValue("@mail", oyuncudandonenmaill);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    mailVarmi = int.Parse(reader[0].ToString());
                }
                reader.Close();
            }
        }
        if (mailVarmi == 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT Mail FROM Kullanici WHERE Id=@ýd;";
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        okunanVeri = reader["Mail"].ToString();
                    }
                    reader.Close();
                }
            }
            if (okunanVeri.Length > 0)
            {
                Debug.Log(okunanVeri);
                //Maile Göndermek için random bir sayý üretir
                MailDegistirmeRastgeleSayi = Random.Range(10000, 100000);
                //gönderilecek mail portlarýný ayarlar
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");
                SmtpServer.Timeout = 10000;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587;

                mail.From = new MailAddress("Morpes_Games@hotmail.com");
                mail.To.Add(new MailAddress(okunanVeri));

                //kullanýcýnýn girdiði maile kod yollar
                mail.Subject = "Code To Change Email";
                mail.Body = "Confrim Code = " + MailDegistirmeRastgeleSayi;
                SmtpServer.Credentials = new System.Net.NetworkCredential("Morpes_Games@hotmail.com", "bofhpkfvuiroqxfu") as ICredentialsByHost; SmtpServer.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                SmtpServer.Send(mail);
            }
        }
#endif
    }

    [Command]
    public void MailDegistirmeOnayKoduSunucuyaYolla(int onayKodu, string oyuncudandonenmaill)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (MaildegistirmeIslemiHataSayisi > 3)
        {
            TargetAyarlarMailDegistirme(2);
        }
        else if (onayKodu == MailDegistirmeRastgeleSayi)
        {
            //kullanýcýnýn girdiði maille güncelleme yapar
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET Mail = @mail WHERE Id=@ýd and (select count(Id) from Kullanici where Mail = @mail) = 0;";
                command.Parameters.AddWithValue("@mail", oyuncudandonenmaill);
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {

                    // Veritabaný baðlantýsý oluþtur
                    connectionString = "Server=" + server + ";Database=" + database + ";Uid=" + uid + ";Pwd=" + password + ";";
                    connection = new MySqlConnection(connectionString);

                    try
                    {
                        // Veritabanýna baðlan
                        connection.Open();
                        string insertQuery = "Update users set user_mail = @user_mail where user_username=@oyuncuadi;";
                        MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                        cmd.Parameters.AddWithValue("@user_mail", oyuncudandonenmaill);
                        cmd.Parameters.AddWithValue("@oyuncuadi", oyuncuadi);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            TargetAyarlarMailDegistirme(1);
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        // Baðlantýyý kapat
                        connection.Close();
                    }
                }
                else
                {
                    TargetAyarlarMailDegistirme(0);
                }
            }
        }
        else
        {
            MaildegistirmeIslemiHataSayisi++;
        }
#endif
    }

    [TargetRpc]
    public void TargetAyarlarMailDegistirme(int donen)
    {
        if (donen == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 365]);
            GameManager.gm.AyarlarmailDegistirBTN.SetActive(false);
            GameManager.gm.AyarlarMailDegistirmeMailINPT.SetActive(false);
            GameManager.gm.kullaniciMailDegistrimeOnayKoduPaneli.SetActive(true);
        }
        else if (donen == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 366]);
        }
        else if (donen == 2)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 367]);
        }
    }


    [Command]
    public void SifreDegistirmeIstegiSunucuyaYolla(string donenSifre,string donenmail)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (donenSifre.Length > 4 && donenSifre.Length < 16)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET Sifre = @YeniOyuncuSifresi WHERE Id=@ýd and Mail = @mail;";
                command.Parameters.AddWithValue("@YeniOyuncuSifresi", donenSifre);
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                command.Parameters.AddWithValue("@mail", donenmail);
                if (command.ExecuteNonQuery() == 1)
                {
                    // Veritabaný baðlantýsý oluþtur
                    connectionString = "Server=" + server + ";Database=" + database + ";Uid=" + uid + ";Pwd=" + password + ";";
                    connection = new MySqlConnection(connectionString);
                    try
                    {
                        // Veritabanýna baðlan
                        connection.Open();
                        string insertQuery = "update users set user_pass = @sifre where user_username=@oyuncuadi;";
                        MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                        cmd.Parameters.AddWithValue("@sifre", donenSifre);
                        cmd.Parameters.AddWithValue("@oyuncuadi", oyuncuadi);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            TargetAyarlarSifreDegistirme(1);
                        }
                    }
                    catch
                    {
                        TargetKayitOl(2, " ");
                    }
                    finally
                    {
                        // Baðlantýyý kapat
                        connection.Close();
                    }
                }
                else
                {
                    TargetAyarlarSifreDegistirme(0);
                }
            }
        }
        else
        {
            TargetAyarlarSifreDegistirme(2);
        }

#endif

    }

    [TargetRpc]
    public void TargetAyarlarSifreDegistirme(int donen)
    {


        if (donen == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 368]);
            GameManager.gm.SifreDegistirSifre.text = "";
            GameManager.gm.AyarlarSifreDegistirMail.text = "";
            GameManager.gm.SifreDegistirTekrarSifre.text = "";
        }
        else if (donen == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 369]);
        }
        else if (donen == 2)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 370]);
        }

    }

    void OnDestroy()
    {
        if (isLocalPlayer)
        {
            Application.Quit();
        }
    }

#if UNITY_SERVER || UNITY_EDITOR
    public void GunlukGirisOdulVer()
    {
        var yestedaysDay = System.DateTime.Today.AddDays(-1);
        string oyuncuSongirilenTarih = "";
        int OyunaGirisSayac = 0;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT SongirilenTarih,GunlukGirisSayac,strftime('%Y-%m-%d %H:%M', 'now', 'localtime') FROM Kullanici WHERE ID=@Id LIMIT 1;";
            command.Parameters.AddWithValue("@Id", oyuncuId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    oyuncuSongirilenTarih = reader["SongirilenTarih"].ToString();
                    OyunaGirisSayac = int.Parse(reader["GunlukGirisSayac"].ToString());
                }
                reader.Close();
            }
        }
        // eger oyuncu gunluk giris odulunu almadýysa
        if (oyuncuSongirilenTarih != System.DateTime.Now.ToString("yyyy-MM-dd"))
        {
            // en son dün girdiyse yani kombo yapýyorsa
            if (yestedaysDay.ToString("yyyy-MM-dd") == oyuncuSongirilenTarih)
            {
                if (OyunaGirisSayac < 7)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET SongirilenTarih = @oyuncuSonGirilenTarih, GunlukGirisSayac = GunlukGirisSayac + 1 WHERE Id=@ýd;";
                        command.Parameters.AddWithValue("@oyuncuSonGirilenTarih", System.DateTime.Now.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@ýd", oyuncuId);
                        command.ExecuteNonQuery();
                        OyunaGirisSayac++;
                    }
                }
                else
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Kullanici SET SongirilenTarih = @oyuncuSonGirilenTarih WHERE Id=@ýd;";
                        command.Parameters.AddWithValue("@oyuncuSonGirilenTarih", System.DateTime.Now.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@ýd", oyuncuId);
                        command.ExecuteNonQuery();
                    }
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Altin =  Altin + (GunlukGirisSayac * 120) WHERE Id=@ýd;";
                    command.Parameters.AddWithValue("@ýd", oyuncuId);
                    command.ExecuteNonQuery();
                    SetOyuncuAltin(oyuncuAltin + (OyunaGirisSayac * 120));
                    TargetGunlukGirisOdulVer(OyunaGirisSayac);
                }
            }
            else
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET SongirilenTarih = @oyuncuSonGirilenTarih, GunlukGirisSayac = 1 WHERE Id=@ýd;";
                    command.Parameters.AddWithValue("@oyuncuSonGirilenTarih", System.DateTime.Now.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@ýd", oyuncuId);
                    command.ExecuteNonQuery();
                    OyunaGirisSayac = 1;
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET Altin = Altin + 120 WHERE Id=@ýd;";
                    command.Parameters.AddWithValue("@ýd", oyuncuId);
                    command.ExecuteNonQuery();
                    SetOyuncuAltin(oyuncuAltin + 120);
                    TargetGunlukGirisOdulVer(OyunaGirisSayac);
                }
            }
        }
    }
#endif

    [TargetRpc]
    public void TargetGunlukGirisOdulVer(int donensayac)
    {
        GameManager.gm.GunlukGirisPaneli.SetActive(true);
        if (donensayac == 1)
        {
            GameManager.gm.GununOduluSlot[0].gameObject.SetActive(true);
            GameManager.gm.GununOduluSlot[1].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[2].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[3].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[4].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[5].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[6].gameObject.SetActive(false);
        }
        else if (donensayac == 2)
        {
            GameManager.gm.GununOduluSlot[0].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[1].gameObject.SetActive(true);
            GameManager.gm.GununOduluSlot[2].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[3].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[4].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[5].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[6].gameObject.SetActive(false);

            GameManager.gm.GecmisOdulSlot[0].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[1].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[2].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[3].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[4].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[5].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[6].gameObject.SetActive(false);

        }
        else if (donensayac == 3)
        {
            GameManager.gm.GununOduluSlot[0].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[1].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[2].gameObject.SetActive(true);
            GameManager.gm.GununOduluSlot[3].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[4].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[5].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[6].gameObject.SetActive(false);

            GameManager.gm.GecmisOdulSlot[0].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[1].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[2].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[3].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[4].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[5].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[6].gameObject.SetActive(false);
        }
        else if (donensayac == 4)
        {
            GameManager.gm.GununOduluSlot[0].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[1].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[2].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[3].gameObject.SetActive(true);
            GameManager.gm.GununOduluSlot[4].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[5].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[6].gameObject.SetActive(false);

            GameManager.gm.GecmisOdulSlot[0].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[1].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[2].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[3].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[4].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[5].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[6].gameObject.SetActive(false);
        }
        else if (donensayac == 5)
        {
            GameManager.gm.GununOduluSlot[0].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[1].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[2].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[3].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[4].gameObject.SetActive(true);
            GameManager.gm.GununOduluSlot[5].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[6].gameObject.SetActive(false);

            GameManager.gm.GecmisOdulSlot[0].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[1].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[2].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[3].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[4].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[5].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[6].gameObject.SetActive(false);
        }
        else if(donensayac == 6)
        {
            GameManager.gm.GununOduluSlot[0].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[1].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[2].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[3].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[4].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[5].gameObject.SetActive(true);
            GameManager.gm.GununOduluSlot[6].gameObject.SetActive(false);

            GameManager.gm.GecmisOdulSlot[0].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[1].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[2].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[3].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[4].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[5].gameObject.SetActive(false);
            GameManager.gm.GecmisOdulSlot[6].gameObject.SetActive(false);

        }
        else if (donensayac == 7)
        {
            GameManager.gm.GununOduluSlot[0].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[1].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[2].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[3].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[4].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[5].gameObject.SetActive(false);
            GameManager.gm.GununOduluSlot[6].gameObject.SetActive(true);

            GameManager.gm.GecmisOdulSlot[0].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[1].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[2].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[3].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[4].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[5].gameObject.SetActive(true);
            GameManager.gm.GecmisOdulSlot[6].gameObject.SetActive(false);
        }
    }

#if UNITY_SERVER || UNITY_EDITOR
    public IEnumerator oyuncuBotKontrolGirdimi()
    {
        yield return new WaitForSeconds(120f);
        oyuncuBotDurumunuGuncelle();
        if (OyuncuBotDurumu == 1)
        {
            yield return new WaitForSeconds(5f);
            connectionToClient.Disconnect();
        } 
    }
#endif

    [Command]
    public void oyuncuBotDurumunuGuncelle()
    {
#if UNITY_SERVER || UNITY_EDITOR
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT OyuncuBotDurumu FROM Kullanici WHERE ID=@Id LIMIT 1;";
            command.Parameters.AddWithValue("@Id", oyuncuId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    SetoyuncuBotDurumu(int.Parse(reader["OyuncuBotDurumu"].ToString()));
                }
                reader.Close();
            }
        }
        if (OyuncuBotDurumu == 1)
        {
            TargetoyuncuBotKontrolGirdimi();
        }
#endif
    }


    [TargetRpc]
    public void TargetoyuncuBotKontrolGirdimi()
    {
        Application.Quit();
    }


    public void OyuncuBotKontrol()
    {
#if UNITY_SERVER || UNITY_EDITOR

        if (OyunaGirisYapildimi && PanelAcikmi == false)
        {
            StartCoroutine(oyuncuBotKontrolGirdimi());
            string[] kuzu = new string[] { "2b827.png", "2bg48.png", "2cegf.png", "2en7g.png", "2enf4.png", "2fxgd.png", "2g7nm.png", "2g783.png", "2gyb6.png", "2mg87.png", "2n73f.png", "2nbc5.png", "2nbcx.png", "2nf26.png", "2npg6.png", "2nx38.png", "2p2y8.png", "2pfpn.png", "2w4y7.png", "2wc38.png", "2wx73.png", "2x7bm.png", "2xc2n.png", "2ycn8.png", "2yggg.png", "3b4we.png", "3bd8f.png", "3bfnd.png", "3bnyf.png", "3bx86.png", "3cpwb.png", "3d7bd.png", "3den6.png", "3dgmf.png", "3ebnn.png", "3ebpw.png", "3eny7.png", "3fbxd.png", "3g2w6.png", "3mxdn.png", "3n2b4.png", "3n3cf.png", "3n7mx.png", "3ndxd.png", "3nfdn.png", "3nnpw.png", "3nw7w.png", "3ny45.png", "3p4nn.png", "3p67n.png", "3pe4g.png", "3w2bw.png", "3wnd3.png", "3x5fm.png", "3x325.png", "3xcgg.png", "3xng6.png", "3ye2e.png", "3ygde.png", "3ym7f.png", "4b2pw.png", "4c8n8.png", "4cfw8.png", "4cn7b.png", "4d22m.png", "4dgf7.png", "4dw3w.png", "4egem.png", "4exnn.png", "4f8yp.png", "4fc36.png", "4fp5g.png", "4gb3f.png", "4gycb.png", "4m2w5.png", "4n2yg.png", "4n3mn.png", "4nc37.png", "4nnf3.png", "4w6mw.png", "4w76g.png", "4yc85.png", "4yc85.png", "4ynf3.png", "5bb66.png", "5bg8f.png", "5bgp2.png", "5bnd7.png", "5dxnm.png", "5ep3n.png", "5expp.png", "5f3gf.png", "5fyem.png", "5g5e5.png", "5gcd3.png", "5mcy7.png", "5mf7c.png", "5mfff.png", "5mgn4.png", "5mnpd.png", "5n3w4.png", "5n245.png", "5n728.png", "5n732.png", "5ng6e.png", "5nggg.png", "5nm6d.png", "5nnff.png", "5np4m.png", "5npdn.png", "5nxnn.png", "5p3mm.png", "5p8fm.png", "5pm6b.png", "5wddw.png", "5x5nx.png", "5x7x5.png", "5xd2e.png", "5xwcg.png", "5ywwf.png", "5yxgp.png", "6b4w6.png", "6b46g.png", "6bdn5.png", "6bnnm.png", "6bxwg.png", "6c3n6.png", "6c3p5.png", "6cm6m.png", "6cwxe.png", "6dd2y.png", "6dmx7.png", "6e2dg.png", "6e6pn.png", "6e554.png", "6ecbn.png", "6end3.png", "6f2yc.png", "6f857.png", "6fg8c.png", "6fgdw.png", "6fn84.png", "6g45w.png", "6ge3p.png", "6gnm3.png", "6m5eg.png", "6mege.png", "6mn8n.png", "6mygb.png", "6n5fd.png", "6n6gg.png", "6n443.png", "6ng6n.png", "6ng6w.png", "6p2ge.png", "6p7gx.png", "6pfy4.png", "6pwcn.png", "6wb76.png", "6wg4n.png", "6wnyc.png", "6xen4.png", "6xpme.png", "6xxdx.png", "6ydyp.png", "7b4bm.png", "7bb7b.png", "7bwm2.png", "7cdge.png", "7cgym.png", "7d44m.png", "7dgc2.png", "7dwx4.png", "7dxbd.png", "7dyww.png", "7e2y7.png", "7f8b3.png", "7fde7.png", "7fmcy.png", "7g3nf.png", "7gce6.png", "7gmf3.png", "7gnge.png", "7gp47.png", "7m8px.png", "7nnnx.png", "7p852.png", "7pcd7.png", "7pn5g.png", "7w67m.png", "7wn74.png", "7wnpm.png", "7wyp4.png", "7xcyd.png", "7xd5m.png" };
            string rastgelekuzu = kuzu[Random.Range(0, kuzu.Length)];
            string imagePath = Path.Combine(Application.streamingAssetsPath, rastgelekuzu);
            byte[] botKontrolSayilarinResmi = File.ReadAllBytes(imagePath);
            string ilkBesKarakter = rastgelekuzu.Substring(0, 5);
            botkontrolTXT = ilkBesKarakter.ToString();
            TargetOyuncuBotKontrol(botKontrolSayilarinResmi);
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET OyuncuBotDurumu = 1 WHERE Id=@ýd;";
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                command.ExecuteNonQuery();
            }
        }
#endif
    }


    [TargetRpc]
    public void TargetOyuncuBotKontrol(byte[] botKontrolresim)
    {
        // Byte dizisini resme dönüþtürün
        Texture2D texture = new Texture2D(2, 2); // Texture boyutunu ayarlayýn
        texture.LoadImage(botKontrolresim); // Byte dizisini yükleyin
        GameManager.gm.botkontrolrastgeleimage.texture = texture;
        //GameManager.gm.botkontrolrastgeleimage.sprite = GameManager.gm.CaptCha[donenrastgelesayi].sprite;
        PanelAcikmi = true;
        GameManager.gm.OyuncuBotKontrolPaneli.SetActive(true);
    }

    [Command]

    public void oyuncuBotKontrolEt(string kullanicidanDonenText)
    {
#if UNITY_SERVER || UNITY_EDITOR

        if (botkontrolTXT == kullanicidanDonenText)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET OyuncuBotDurumu  = 0, OyuncuBotHataliGirisSayac = 0 WHERE Id=@ýd;";
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    birSonrakiBotKotntrolIcinKalanIslemSayisi = Random.Range(30, 40);
                }
            }
            TargetoyuncuBotKontrolEt();
        }
        else
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET OyuncuBotHataliGirisSayac = OyuncuBotHataliGirisSayac + 1 WHERE Id=@ýd;";
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                command.ExecuteNonQuery();
            }
        }
        int hatalýgirisSayac = 0;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT OyuncuBotHataliGirisSayac FROM Kullanici WHERE ID=@Id LIMIT 1;";
            command.Parameters.AddWithValue("@Id", oyuncuId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    hatalýgirisSayac = int.Parse(reader["OyuncuBotHataliGirisSayac"].ToString());
                }
                reader.Close();
            }
        }
        if (hatalýgirisSayac >= 3)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET OyuncuBotCezaPuani = OyuncuBotCezaPuani + 1, OyuncuBotHataliGirisSayac = 0 WHERE Id=@ýd;";
                command.Parameters.AddWithValue("@ýd", oyuncuId);
                command.ExecuteNonQuery();
            }
        }
#endif
    }
    [TargetRpc]
    public void TargetoyuncuBotKontrolEt()
    {
        GameManager.gm.OyuncuBotKontrolPaneli.SetActive(false);
        PanelAcikmi = false;
    }


    [Command]

    public void filoDostOlmaIstegiYollaSunucu(string GelenFiloKisaAd)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2)
        {
            bool zatenMuttefikiz = false;
            bool dusmanimizmi = false;
            bool zatenIstekAtilmis = false;
            int FiloID = -1;
            string FiloAd = "";

            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloId,FiloAd FROM Filolar WHERE FiloKisaltma=@FiloKisaltmasi;";
                command.Parameters.AddWithValue("@FiloKisaltmasi", GelenFiloKisaAd);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        FiloID = int.Parse(reader["FiloId"].ToString());
                        FiloAd = reader["FiloAd"].ToString();
                    }
                    reader.Close();
                }
            }
            if (FiloID > 0)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT count(ID) FROM DiplomasiMuttefik WHERE (MuttefikBirId=@MuttefikBirId And MuttefikIkiId=@MuttefikIkiId) or (MuttefikBirId=@MuttefikIkiId And MuttefikIkiId=@MuttefikBirId);";
                    command.Parameters.AddWithValue("@MuttefikBirId", oyuncuFiloId);
                    command.Parameters.AddWithValue("@MuttefikIkiId", FiloID);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && int.Parse(reader[0].ToString()) > 0)
                        {
                            zatenMuttefikiz = true;
                            TargetfiloDostOlmaIstegiYolla(FiloAd, 0);
                        }
                        reader.Close();
                    }
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT count(ID) FROM MuttefikIstekleri WHERE (IstekAtanFiloId=@IstekAtanFiloId And IstegiKabulEdecekFiloID=@IstegiKabulEdecekFiloID) or (IstekAtanFiloId=@IstegiKabulEdecekFiloID And IstegiKabulEdecekFiloID=@IstekAtanFiloId);";
                    command.Parameters.AddWithValue("@IstekAtanFiloId", oyuncuFiloId);
                    command.Parameters.AddWithValue("@IstegiKabulEdecekFiloID", FiloID);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && int.Parse(reader[0].ToString()) > 0)
                        {
                            zatenIstekAtilmis = true;
                            TargetfiloDostOlmaIstegiYolla(FiloAd, 5);
                        }
                        reader.Close();
                    }
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT count(ID) FROM DiplomasiSavas WHERE (SaldiranFiloId=@SaldiranFiloId And SavunanFiloId=@SavunanFiloId) or (SaldiranFiloId=@SavunanFiloId And SavunanFiloId=@SaldiranFiloId);";
                    command.Parameters.AddWithValue("@SaldiranFiloId", oyuncuFiloId);
                    command.Parameters.AddWithValue("@SavunanFiloId", FiloID);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && int.Parse(reader[0].ToString()) > 0)
                        {
                            dusmanimizmi = true;
                            TargetfiloDostOlmaIstegiYolla(FiloAd, 3);
                        }
                        reader.Close();
                    }
                }
                if (zatenMuttefikiz == false && dusmanimizmi == false && zatenIstekAtilmis == false)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO MuttefikIstekleri (IstekAtanFiloId,IstegiKabulEdecekFiloID,FiloKisaltmasi,FiloAdi) VALUES (@IstekAtanFiloId,@IstegiKabulEdecekFiloID,@FiloKisaltmasi,@FiloAdi);";
                        command.Parameters.AddWithValue("@IstekAtanFiloId", oyuncuFiloId);
                        command.Parameters.AddWithValue("@IstegiKabulEdecekFiloID", FiloID);
                        command.Parameters.AddWithValue("@FiloKisaltmasi", OyuncuFiloKisaltma);
                        command.Parameters.AddWithValue("@FiloAdi", OyuncuFiloAd);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            TargetfiloDostOlmaIstegiYolla(FiloAd, 1);
                        }
                    }
                }
            }
            else
            {
                TargetfiloDostOlmaIstegiYolla(FiloAd, 2);
            }
        }
#endif
    }
    [TargetRpc]
    public void TargetfiloDostOlmaIstegiYolla(string donenFiloAd, int savasAcmaSonucu)
    {
        if (savasAcmaSonucu == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], donenFiloAd + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 412]);
        }
        else if (savasAcmaSonucu == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 413]);
        }
        else if (savasAcmaSonucu == 2)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 414]);
        }
        else if (savasAcmaSonucu == 3)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], donenFiloAd + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 415]);
        }
        else if (savasAcmaSonucu == 5)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 416]);
        }
    }


    [Command]

    public void filoDusmanOlmaIstegiYollaSunucu(string FiloKisaAd)
    {
#if UNITY_SERVER || UNITY_EDITOR
        bool zatensavasliyor = false;
        bool dostumuzmu = false;
        int FiloID = -1;
        string FiloAd = "";
        if (OyuncuYetkiID <= 2)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloId,FiloAd FROM Filolar WHERE FiloKisaltma=@FiloKisaltmasi;";
                command.Parameters.AddWithValue("@FiloKisaltmasi", FiloKisaAd);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        FiloID = int.Parse(reader["FiloId"].ToString());
                        FiloAd = reader["FiloAd"].ToString();
                    }
                    reader.Close();
                }
            }
            if (FiloID > 0)
            {
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT count(ID) FROM DiplomasiSavas WHERE (SaldiranFiloId=@SaldiranFiloId And SavunanFiloId=@SavunanFiloId) or (SaldiranFiloId=@SavunanFiloId And SavunanFiloId=@SaldiranFiloId);";
                    command.Parameters.AddWithValue("@SaldiranFiloId", oyuncuFiloId);
                    command.Parameters.AddWithValue("@SavunanFiloId", FiloID);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && int.Parse(reader[0].ToString()) > 0)
                        {
                            zatensavasliyor = true;
                            TargetfiloDusmanOlmaIstegiYolla(FiloAd, 0);
                        }
                        reader.Close();
                    }
                }
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT count(ID) FROM DiplomasiMuttefik WHERE (MuttefikBirId=@MuttefikBirId And MuttefikIkiId=@MuttefikIkiId) or (MuttefikBirId=@MuttefikIkiId And MuttefikIkiId=@MuttefikBirId);";
                    command.Parameters.AddWithValue("@MuttefikBirId", oyuncuFiloId);
                    command.Parameters.AddWithValue("@MuttefikIkiId", FiloID);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && int.Parse(reader[0].ToString()) > 0)
                        {
                            dostumuzmu = true;
                            TargetfiloDusmanOlmaIstegiYolla(FiloAd, 3);
                        }
                        reader.Close();
                    }
                }
                if (zatensavasliyor == false && dostumuzmu == false)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO DiplomasiSavas (SaldiranFiloId,SaldiranFiloAd,SaldiranFiloKisaAd,SavunanFiloId,SavunanFiloAd,SavunanFiloKisaAd,SavasAcilanTarih) VALUES (@bizimFiloId,(select FiloAd from Filolar where FiloId=@bizimFiloId),(select FiloKisaltma from Filolar where FiloId=@bizimFiloId),@savunulanFiloID,(select FiloAd from Filolar where FiloId=@savunulanFiloID),(select FiloKisaltma from Filolar where FiloId=@savunulanFiloID),date('now'));";
                        command.Parameters.AddWithValue("@bizimFiloId", oyuncuFiloId);
                        command.Parameters.AddWithValue("@savunulanFiloID", FiloID);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Delete from MuttefikIstekleri where (IstekAtanFiloId = @birinciFilo and IstegiKabulEdecekFiloID=@ikinciFilo) or (IstegiKabulEdecekFiloID = @ikinciFilo and IstekAtanFiloId=@birinciFilo);";
                                command2.Parameters.AddWithValue("@birinciFilo", FiloID);
                                command2.Parameters.AddWithValue("@ikinciFilo", oyuncuFiloId);
                                command2.ExecuteNonQuery();
                            }
                            GameManager.gm.oyunDunyasiSavasDuyurusuGoster(OyuncuFiloAd + "[" + OyuncuFiloKisaltma + "]", FiloAd + "[" + FiloKisaAd + "]");
                            TargetfiloDusmanOlmaIstegiYolla(FiloAd, 1);
                        }
                    }
                }
                else
                {
                    TargetfiloDusmanOlmaIstegiYolla(FiloAd, 4);
                }
            }
            else
            {
                TargetfiloDusmanOlmaIstegiYolla(FiloAd, 2);
            }
        }
        else
        {
            TargetfiloDusmanOlmaIstegiYolla(FiloAd, 5);
        }
#endif
    }
    [TargetRpc]
    public void TargetfiloDusmanOlmaIstegiYolla(string donenFiloAd, int savasAcmaSonucu)
    {
        if (savasAcmaSonucu == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 427], donenFiloAd + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 417]);

        }
        else if (savasAcmaSonucu == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 418]);
        }
        else if (savasAcmaSonucu == 2)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 414]);
        }
        else if (savasAcmaSonucu == 3)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], donenFiloAd + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 419]);
        }
        else if (savasAcmaSonucu == 4)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 420]);
        }
        else if (savasAcmaSonucu == 5)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 421]);
        }
    }

    [Command]
    public void muttefikSayfasiVerileriniCek()
    {
#if UNITY_SERVER || UNITY_EDITOR
        // muttefik olma isteklerini ceken kod
        int[] MuttefikIstekAtanFiloId = new int[50];
        string[] MuttefikFiloKisaltmasi = new string[50];
        string[] MuttefikFiloAdi = new string[50];
        int i = 0;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT IstekAtanFiloId,FiloKisaltmasi,FiloAdi FROM MuttefikIstekleri WHERE IstegiKabulEdecekFiloID=@IstegiKabulEdecekFiloID;";
            command.Parameters.AddWithValue("@IstegiKabulEdecekFiloID", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    MuttefikIstekAtanFiloId[i] = int.Parse(reader["IstekAtanFiloId"].ToString());
                    MuttefikFiloKisaltmasi[i] = reader["FiloKisaltmasi"].ToString();
                    MuttefikFiloAdi[i] = reader["FiloAdi"].ToString();
                    i++;
                }
                reader.Close();
            }
        }
        // Baris Atma isteklerini ceken kod
        int[] BarisIstekAtanFiloId = new int[50];
        string[] BarisFiloKisaltmasi = new string[50];
        string[] BarisFiloAdi = new string[50];
        i = 0;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT IstekAtanFiloId,IstekAtilanFiloId,FiloAdi,FiloKisaltmasi FROM BarisIstekleri WHERE IstekAtilanFiloId=@IstegiKabulEdecekFiloID;";
            command.Parameters.AddWithValue("@IstegiKabulEdecekFiloID", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    BarisIstekAtanFiloId[i] = int.Parse(reader["IstekAtanFiloId"].ToString());
                    BarisFiloKisaltmasi[i] = reader["FiloKisaltmasi"].ToString();
                    BarisFiloAdi[i] = reader["FiloAdi"].ToString();
                    i++;
                }
                reader.Close();
            }
        }
        TargetmuttefikSayfasiVerileriniCek(MuttefikFiloKisaltmasi, MuttefikFiloAdi, BarisFiloKisaltmasi, BarisFiloAdi);

#endif
    }
    [TargetRpc]
    public void TargetmuttefikSayfasiVerileriniCek(string[] donenMuttefikFiloKisaltma, string[] donenMuttefikFiloAd, string[] donenBarisFiloKisaltma, string[] donenBarisFiloAd)
    {
        GameManager.gm.AddItemsFiloMuttefikIstekleri(donenMuttefikFiloAd, donenMuttefikFiloKisaltma, donenBarisFiloAd, donenBarisFiloKisaltma);
        GameManager.gm.FiloMuttefikIsteklerSayfasi.SetActive(false);
        GameManager.gm.FiloSavasIstatistikleriSayfasi.SetActive(false);
    }


    [Command]
    public void muttefikIstekGenislet(string filoKisaltmasi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        string kurucuAdi = "";
        int FiloTp = 0;
        int UyeSayisi = 0;
        int FiloId = 0;
        int Adasayisi = 0;
        string FiloAd = "";
        string FiloKisaltma = "";
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT FiloAd,UyeSayisi,FiloTp,FiloKisaltma,FiloId,AdaSayisi FROM Filolar WHERE FiloKisaltma=@FiloKisaltma;";
            command.Parameters.AddWithValue("@FiloKisaltma", filoKisaltmasi);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    FiloTp = int.Parse(reader["FiloTp"].ToString());
                    UyeSayisi = int.Parse(reader["UyeSayisi"].ToString());
                    FiloId = int.Parse(reader["FiloId"].ToString());
                    FiloAd = reader["FiloAd"].ToString();
                    FiloKisaltma = reader["FiloKisaltma"].ToString();
                    Adasayisi = int.Parse(reader["AdaSayisi"].ToString());
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT Kullanici_Adi FROM Kullanici WHERE FiloId=@FiloId And YetkiId = 1;";
            command.Parameters.AddWithValue("@FiloId", FiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    kurucuAdi = reader["Kullanici_Adi"].ToString();
                }
                reader.Close();
            }
        }
        TargetmuttefikIstekGenislet(kurucuAdi, FiloTp, UyeSayisi, FiloAd, FiloKisaltma, Adasayisi);
#endif
    }

    [TargetRpc]
    public void TargetmuttefikIstekGenislet(string filoLideriAdi, int FiloTp, int UyeSayisi, string FiloAd, string FiloKisaltma,int AdaSayisi)
    {
        GameManager.gm.FiloMuttefikIsteklerSayfasi.SetActive(true);
        GameManager.gm.FiloSavasIstatistikleriSayfasi.SetActive(false);
        GameManager.gm.FiloMuttefikIstatistikSayfasi.SetActive(false);
        GameManager.gm.FiloBarisOlmaSayfasi.SetActive(false);
        
        GameManager.gm.filoIsteklerFiloAdi.text = FiloAd;
        GameManager.gm.filoIsteklerKisaFiloAdi.text = FiloKisaltma;
        GameManager.gm.filoIsteklerFiloLideriAdi.text = filoLideriAdi;
        GameManager.gm.filoIsteklerFiloSeviyesi.text = FiloTp.ToString();
        GameManager.gm.filoIsteklerFiloUyeSayisi.text = UyeSayisi.ToString(); ;
        GameManager.gm.filoIsteklerFiloAdaSayisi.text = AdaSayisi.ToString(); ;
    }

    [Command]
    public void BarisIstekGenislet(string filoKisaltmasi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        string kurucuAdi = "";
        int FiloTp = 0;
        int UyeSayisi = 0;
        int FiloId = 0;
        int Adasayisi = 0;
        string FiloAd = "";
        string FiloKisaltma = "";

        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT FiloAd,UyeSayisi,FiloTp,FiloKisaltma,FiloId,AdaSayisi FROM Filolar WHERE FiloKisaltma=@FiloKisaltma;";
            command.Parameters.AddWithValue("@FiloKisaltma", filoKisaltmasi);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    FiloTp = int.Parse(reader["FiloTp"].ToString());
                    UyeSayisi = int.Parse(reader["UyeSayisi"].ToString());
                    FiloId = int.Parse(reader["FiloId"].ToString());
                    FiloAd = reader["FiloAd"].ToString();
                    FiloKisaltma = reader["FiloKisaltma"].ToString();
                    Adasayisi = int.Parse(reader["AdaSayisi"].ToString());
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT Kullanici_Adi FROM Kullanici WHERE FiloId=@FiloId And YetkiId = 1;";
            command.Parameters.AddWithValue("@FiloId", FiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    kurucuAdi = reader["Kullanici_Adi"].ToString();
                }
                reader.Close();
            }
        }
        TargetBarisIstekGenislet(kurucuAdi, FiloTp, UyeSayisi, FiloAd, FiloKisaltma, Adasayisi);
#endif
    }

    [TargetRpc]
    public void TargetBarisIstekGenislet(string filoLideriAdi, int FiloTp, int UyeSayisi, string FiloAd, string FiloKisaltma, int AdaSayisi)
    {
        GameManager.gm.FiloMuttefikIsteklerSayfasi.SetActive(false);
        GameManager.gm.FiloSavasIstatistikleriSayfasi.SetActive(false);
        GameManager.gm.FiloMuttefikIstatistikSayfasi.SetActive(false);
        GameManager.gm.FiloBarisOlmaSayfasi.SetActive(true);
        GameManager.gm.filoBarisIsteklerFiloAdi.text = FiloAd;
        GameManager.gm.filoBarisIsteklerKisaFiloAdi.text = FiloKisaltma;
        GameManager.gm.filoBarisIsteklerFiloLideriAdi.text = filoLideriAdi;
        GameManager.gm.filoBarisIsteklerFiloSeviyesi.text = FiloTp.ToString();
        GameManager.gm.filoBarisIsteklerFiloUyeSayisi.text = UyeSayisi.ToString(); ;
        GameManager.gm.filoBarisIsteklerFiloAdaSayisi.text = AdaSayisi.ToString(); ;
    }

    [Command]
    public void FiloMuttefikIstegiKabulEt(string filoTagi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2)
        {
            int dostOlunacakFiloId = 0;
            string dostOlunacakFiloAd = "";
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloId,FiloAd FROM Filolar WHERE FiloKisaltma=@FiloKisaltma;";
                command.Parameters.AddWithValue("@FiloKisaltma", filoTagi);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dostOlunacakFiloId = int.Parse(reader["FiloId"].ToString());
                        dostOlunacakFiloAd = reader["FiloAd"].ToString();
                    }
                    reader.Close();
                }
            }
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Delete from MuttefikIstekleri where (IstekAtanFiloId = @birinciFilo and IstegiKabulEdecekFiloID=@ikinciFilo) or (IstekAtanFiloId = @ikinciFilo and IstegiKabulEdecekFiloID=@birinciFilo);";
                command.Parameters.AddWithValue("@birinciFilo", dostOlunacakFiloId);
                command.Parameters.AddWithValue("@ikinciFilo", oyuncuFiloId);
                if (command.ExecuteNonQuery() == 1)
                {
                    using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command2.CommandText = "INSERT INTO DiplomasiMuttefik (MuttefikBirId,MuttefikIkiId,MuttefikOlunanTarih,MuttefikBirUzunAd,MuttefikBirKýsaAd,MuttefikIkiUzunAd,MuttefikIkiKýsaAd) VALUES (@dostFilo,@bizimFilo,date('now'),@MuttefikBirUzunAd,@MuttefikBirKýsaAd,@MuttefikIkiUzunAd,@MuttefikIkiKýsaAd);";
                        command2.Parameters.AddWithValue("@dostFilo", dostOlunacakFiloId);
                        command2.Parameters.AddWithValue("@bizimFilo", oyuncuFiloId);
                        command2.Parameters.AddWithValue("@MuttefikBirUzunAd", dostOlunacakFiloAd);
                        command2.Parameters.AddWithValue("@MuttefikBirKýsaAd", filoTagi);
                        command2.Parameters.AddWithValue("@MuttefikIkiUzunAd", OyuncuFiloAd);
                        command2.Parameters.AddWithValue("@MuttefikIkiKýsaAd", OyuncuFiloKisaltma);
                        if (command2.ExecuteNonQuery() == 1)
                        {
                            GameManager.gm.oyunDunyasiMuttefikDuyurusuGoster(OyuncuFiloAd + "[" + OyuncuFiloKisaltma + "]", dostOlunacakFiloAd + "[" + filoTagi + "]");
                            TargetFiloMuttefikIstegiKabulEt(1);
                        }
                    }
                }
                else
                {
                    TargetFiloMuttefikIstegiKabulEt(0);
                }
            }

        }
#endif
    }

    [TargetRpc]
    public void TargetFiloMuttefikIstegiKabulEt(int muttefikEklemeSonucu)
    {
        GameManager.gm.FiloMuttefikIstatistikSayfasi.SetActive(false);
        muttefikSayfasiVerileriniCek();

        if (muttefikEklemeSonucu == 1)
        {
           GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 422]);
        }
        else
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 423]);
        }
    }

    [Command]
    public void FiloMuttefikIstegiRedEt(string filoTagi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2)
        {
            int dostOlunacakFiloId = 0;
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloId FROM Filolar WHERE FiloKisaltma=@FiloKisaltma;";
                command.Parameters.AddWithValue("@FiloKisaltma", filoTagi);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dostOlunacakFiloId = int.Parse(reader["FiloId"].ToString());
                    }
                    reader.Close();
                }
            }
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Delete from MuttefikIstekleri where (IstekAtanFiloId = @birinciFilo and IstegiKabulEdecekFiloID=@ikinciFilo) or (IstekAtanFiloId = @ikinciFilo and IstegiKabulEdecekFiloID=@birinciFilo);";
                command.Parameters.AddWithValue("@birinciFilo", dostOlunacakFiloId);
                command.Parameters.AddWithValue("@ikinciFilo", oyuncuFiloId);
                if (command.ExecuteNonQuery() == 1)
                {
                    TargetFiloMuttefikIstegiRedEt(1);
                }
                else
                {
                    TargetFiloMuttefikIstegiRedEt(0);
                }
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetFiloMuttefikIstegiRedEt(int muttefikEklemeSonucu)
    {
        GameManager.gm.FiloMuttefikIstatistikSayfasi.SetActive(false);
        muttefikSayfasiVerileriniCek();
        if (muttefikEklemeSonucu == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 424]);
        }
        else
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 425]);
        }
    }

    [Command]
    public void filoMuttefikCekSunucu()
    {
#if UNITY_SERVER || UNITY_EDITOR
        // muttefikleri ceken kod
        int[] filoId = new int[50];
        string[] filoAd = new string[50];
        string[] filoKisaAd = new string[50];
        int i = 0;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT MuttefikBirUzunAd,MuttefikBirKýsaAd,MuttefikBirId,MuttefikIkiId,MuttefikIkiUzunAd,MuttefikIkiKýsaAd FROM DiplomasiMuttefik WHERE MuttefikBirId=@oyuncuFiloId OR MuttefikIkiId=@oyuncuFiloId;";
            command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (int.Parse(reader["MuttefikBirId"].ToString()) == oyuncuFiloId)
                    {
                        filoId[i] = int.Parse(reader["MuttefikIkiId"].ToString());
                        filoAd[i] = reader["MuttefikIkiUzunAd"].ToString();
                        filoKisaAd[i] = reader["MuttefikIkiKýsaAd"].ToString();
                    }
                    else if (int.Parse(reader["MuttefikIkiId"].ToString()) == oyuncuFiloId)
                    {
                        filoId[i] = int.Parse(reader["MuttefikBirId"].ToString());
                        filoAd[i] = reader["MuttefikBirUzunAd"].ToString();
                        filoKisaAd[i] = reader["MuttefikBirKýsaAd"].ToString();
                    }
                    i++;
                }
                reader.Close();
                TargetfiloMuttefikCekSunucu(filoAd, filoKisaAd, i);
            }
        }

#endif
    }
    [TargetRpc]
    public void TargetfiloMuttefikCekSunucu(string[] donenFiloAd, string[] donenFiloKisaAd, int donenSayi)
    {
        dostFilolarKisaAdList.Clear();
        foreach (var item in donenFiloKisaAd)
        {
            if (item != null)
            {
                dostFilolarKisaAdList.Add(item);
            }
        }
        GameManager.gm.AddItemsFiloMuttefik(donenFiloAd, donenSayi, donenFiloKisaAd);
    }


    [Command]

    public void filoDusmanCekSunucu()
    {
#if UNITY_SERVER || UNITY_EDITOR
        // muttefikleri ceken kod
        int[] filoId = new int[50];
        string[] filoAd = new string[50];
        string[] filoKisaAd = new string[50];
        int i = 0;
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT SaldiranFiloAd,SaldiranFiloKisaAd,SaldiranFiloId,SavunanFiloId,SavunanFiloAd,SavunanFiloKisaAd FROM DiplomasiSavas WHERE SaldiranFiloId=@oyuncuFiloId OR SavunanFiloId=@oyuncuFiloId;";
            command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (int.Parse(reader["SaldiranFiloId"].ToString()) == oyuncuFiloId)
                    {
                        filoId[i] = int.Parse(reader["SavunanFiloId"].ToString());
                        filoAd[i] = reader["SavunanFiloAd"].ToString();
                        filoKisaAd[i] = reader["SavunanFiloKisaAd"].ToString();
                    }
                    else if (int.Parse(reader["SavunanFiloId"].ToString()) == oyuncuFiloId)
                    {
                        filoId[i] = int.Parse(reader["SaldiranFiloId"].ToString());
                        filoAd[i] = reader["SaldiranFiloAd"].ToString();
                        filoKisaAd[i] = reader["SaldiranFiloKisaAd"].ToString();
                    }
                    i++;
                }
                reader.Close();
                TargetfiloDusmanCekSunucu(filoAd, filoKisaAd, i);
            }
        }

#endif
    }
    [TargetRpc]
    public void TargetfiloDusmanCekSunucu(string[] donenFiloAd, string[] donenFiloKisaAd, int donenSayi)
    {
        dusmanFilolarKisaAdList.Clear();
        foreach (var item in donenFiloKisaAd)
        {
            if (item != null)
            {
                dusmanFilolarKisaAdList.Add(item);
            }
        }
        GameManager.gm.AddItemsFiloDusman(donenFiloAd, donenSayi,donenFiloKisaAd);
    }

    [Command]
    public void AdaninKulelerininBilgileriniCekSunucu()
    {
#if UNITY_SERVER || UNITY_EDITOR
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Adalar Where AdaId = @harita;";
            command.Parameters.AddWithValue("@harita", harita);
            using (IDataReader reader = command.ExecuteReader())
            {
                int[] adaKuleleri = new int[12];
                bool[] adaKuleleriTamirButonlari = new bool[12];
                if (reader.Read())
                {
                    adaKuleleri[0] = int.Parse(reader["Kule1"].ToString());
                    adaKuleleri[1] = int.Parse(reader["Kule2"].ToString());
                    adaKuleleri[2] = int.Parse(reader["Kule3"].ToString());
                    adaKuleleri[3] = int.Parse(reader["Kule4"].ToString());
                    adaKuleleri[4] = int.Parse(reader["Kule5"].ToString());
                    adaKuleleri[5] = int.Parse(reader["Kule6"].ToString());
                    adaKuleleri[6] = int.Parse(reader["Kule7"].ToString());
                    adaKuleleri[7] = int.Parse(reader["Kule8"].ToString());
                    adaKuleleri[8] = int.Parse(reader["Kule9"].ToString());
                    adaKuleleri[9] = int.Parse(reader["Kule10"].ToString());
                    adaKuleleri[10] = int.Parse(reader["Kule11"].ToString());
                    adaKuleleri[11] = int.Parse(reader["Kule12"].ToString());
                }
                reader.Close();
                for (int i = 0; i < 12; i++)
                {
                    if (adaKuleleri[i] > 0 && GameManager.gm.HaritadakiFiloAdalari[harita - 5].transform.Find("Kule"+(i+1)).GetChild(0).GetComponent<KuleKontrol>().Can < GameManager.gm.HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().MaxCan && GameManager.gm.HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().kuleTamirDurumu == false)
                    {
                        adaKuleleriTamirButonlari[i] = true;
                    }
                    else
                    {
                        adaKuleleriTamirButonlari[i] = false;
                    }
                }
                TargetAdaninKulelerininBilgileriniCekSunucu(adaKuleleri, adaKuleleriTamirButonlari);
            }
        }

#endif
    }
    [TargetRpc]
    public void TargetAdaninKulelerininBilgileriniCekSunucu(int[] donenAdaKuleleri,bool[] adaKuleleriTamirButonlari)
    {
        for (int i = 0; i < 12; i++)
        {
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere1/TamirEtButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere1/SatinAlButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere2/TamirEtButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere2/SatinAlButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere3/TamirEtButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere3/SatinAlButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere4/TamirEtButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere4/SatinAlButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere5/TamirEtButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere5/SatinAlButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere6/TamirEtButton").gameObject.SetActive(false);
            GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere6/SatinAlButton").gameObject.SetActive(false);

            if (adaKuleleriTamirButonlari[i] == true)
            {
                GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere" + donenAdaKuleleri[i] + "/TamirEtButton").gameObject.SetActive(adaKuleleriTamirButonlari[i]);
            }
        }
        for (int i = 0; i < 12; i++)
        {
            for (int x = Mathf.Abs(donenAdaKuleleri[i]); x < 6; x++)
            {
                GameManager.gm.KuleIslemlerSlotlari[i].transform.Find("Pencere" + (x + 1) + "/SatinAlButton").gameObject.SetActive(true);
            }
        }
    }

    [Command]
    public void KuleDik(int kuleSeviyesi, int hangiKule)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2)
        {
            int filoAltin = 0;
            int[] adadakiKuleler = new int[12];
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloAltin FROM Filolar WHERE FiloId = @oyuncuFiloId;";
                command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        filoAltin = int.Parse(reader["FiloAltin"].ToString());
                    }
                    reader.Close();
                }
            }
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Adalar WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @adaId;";
                command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                command.Parameters.AddWithValue("@adaId", harita);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        adadakiKuleler[0] = int.Parse(reader["Kule1"].ToString());
                        adadakiKuleler[1] = int.Parse(reader["Kule2"].ToString());
                        adadakiKuleler[2] = int.Parse(reader["Kule3"].ToString());
                        adadakiKuleler[3] = int.Parse(reader["Kule4"].ToString());
                        adadakiKuleler[4] = int.Parse(reader["Kule5"].ToString());
                        adadakiKuleler[5] = int.Parse(reader["Kule6"].ToString());
                        adadakiKuleler[6] = int.Parse(reader["Kule7"].ToString());
                        adadakiKuleler[7] = int.Parse(reader["Kule8"].ToString());
                        adadakiKuleler[8] = int.Parse(reader["Kule9"].ToString());
                        adadakiKuleler[9] = int.Parse(reader["Kule10"].ToString());
                        adadakiKuleler[10] = int.Parse(reader["Kule11"].ToString());
                        adadakiKuleler[11] = int.Parse(reader["Kule12"].ToString());
                    }
                    reader.Close();
                }
            }

            if (kuleSeviyesi == 1 && filoAltin >= 10000)
            {
                if (adadakiKuleler[hangiKule - 1] == 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 10000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 10000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -1 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.KuleDikYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
            }
            else if (kuleSeviyesi == 2 && filoAltin >= 14000)
            {
                if (adadakiKuleler[hangiKule - 1] == 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 14000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 14000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -2 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.KuleDikYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
                else if (adadakiKuleler[hangiKule - 1] > 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 14000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 14000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -2 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.SunucuKuleYukseltmeYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
            }
            else if (kuleSeviyesi == 3 && filoAltin >= 19000)
            {
                if (adadakiKuleler[hangiKule - 1] == 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 19000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 19000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -3 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.KuleDikYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
                else if (adadakiKuleler[hangiKule - 1] > 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 19000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 19000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -3 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.SunucuKuleYukseltmeYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
            }
            else if (kuleSeviyesi == 4 && filoAltin >= 25000)
            {
                if (adadakiKuleler[hangiKule - 1] == 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 25000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 25000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -4 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.KuleDikYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
                else if (adadakiKuleler[hangiKule - 1] > 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 25000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 25000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -4 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.SunucuKuleYukseltmeYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
            }
            else if (kuleSeviyesi == 5 && filoAltin >= 35000)
            {
                if (adadakiKuleler[hangiKule - 1] == 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 35000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 35000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -5 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.KuleDikYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
                else if (adadakiKuleler[hangiKule - 1] > 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 35000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 35000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -5 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.SunucuKuleYukseltmeYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
            }
            else if (kuleSeviyesi == 6 && filoAltin >= 50000)
            {
                if (adadakiKuleler[hangiKule - 1] == 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 50000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 50000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -6 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.KuleDikYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
                else if (adadakiKuleler[hangiKule - 1] > 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 50000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 50000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                            {
                                command2.CommandText = "Update Adalar SET Kule" + hangiKule + " = -6 WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                                command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                                command2.Parameters.AddWithValue("@harita", harita);
                                if (command2.ExecuteNonQuery() == 1)
                                {
                                    TargetKuleDik(1);
                                    GameManager.gm.SunucuKuleYukseltmeYedek(kuleSeviyesi, hangiKule, harita, oyuncuFiloId, OyuncuFiloKisaltma);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                TargetKuleDik(0);
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetKuleDik(int donenDurum)
    {
        if (donenDurum == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 426]);
        }
        else if (donenDurum == 1)
        {
            AdaninKulelerininBilgileriniCekSunucu();
        }
    }

    [Command]
    public void KuleTamiret(int hangiKule)
    {
#if UNITY_SERVER || UNITY_EDITOR
        int filoAltin = 0;
        int[] adadakiKuleler = new int[12];
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT FiloAltin FROM Filolar WHERE FiloId = @oyuncuFiloId;";
            command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    filoAltin = int.Parse(reader["FiloAltin"].ToString());
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Adalar WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @adaId;";
            command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
            command.Parameters.AddWithValue("@adaId", harita);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    adadakiKuleler[0] = int.Parse(reader["Kule1"].ToString());
                    adadakiKuleler[1] = int.Parse(reader["Kule2"].ToString());
                    adadakiKuleler[2] = int.Parse(reader["Kule3"].ToString());
                    adadakiKuleler[3] = int.Parse(reader["Kule4"].ToString());
                    adadakiKuleler[4] = int.Parse(reader["Kule5"].ToString());
                    adadakiKuleler[5] = int.Parse(reader["Kule6"].ToString());
                    adadakiKuleler[6] = int.Parse(reader["Kule7"].ToString());
                    adadakiKuleler[7] = int.Parse(reader["Kule8"].ToString());
                    adadakiKuleler[8] = int.Parse(reader["Kule9"].ToString());
                    adadakiKuleler[9] = int.Parse(reader["Kule10"].ToString());
                    adadakiKuleler[10] = int.Parse(reader["Kule11"].ToString());
                    adadakiKuleler[11] = int.Parse(reader["Kule12"].ToString());
                }
                reader.Close();
            }
        }
        if (OyuncuYetkiID <= 2)
        {
            if (filoAltin >= 4000)
            {
                if (adadakiKuleler[hangiKule - 1] > 0)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "Update Filolar SET FiloAltin = FiloAltin - 4000 WHERE FiloId = @oyuncuFiloId and FiloAltin >= 4000;";
                        command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            TargetKuleTamiret(1);
                            GameManager.gm.HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + hangiKule).GetChild(0).GetComponent<KuleKontrol>().kuleTamirDurumu = true;
                        }
                        else
                        {
                            TargetKuleTamiret(2);
                        }
                    }
                }
            }
            else
            {
                TargetKuleTamiret(0);
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetKuleTamiret(int donenDurum)
    {
        if (donenDurum == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 426]);
        }
        else if (donenDurum == 1)
        {
            AdaninKulelerininBilgileriniCekSunucu();
        }
        else if (donenDurum == 2)
        {
            GameManager.gm.BildirimOlustur("HATA","Destek Ýle Ýletiþime Geçin");
        }
    }

    [Command]
    public void FiloIciAdalariYukle()
    {
#if UNITY_SERVER || UNITY_EDITOR

        int[] Harita5 = new int[12];
        int[] Harita5KulelerMaxCan = new int[12];
        int[] Harita5KulelerCan = new int[12];
        int[] Harita6 = new int[12];
        int[] Harita6KulelerMaxCan = new int[12];
        int[] Harita6KulelerCan = new int[12];
        int[] Harita7 = new int[12];
        int[] Harita7KulelerMaxCan = new int[12];
        int[] Harita7KulelerCan = new int[12];
        int[] Harita8 = new int[12];
        int[] Harita8KulelerMaxCan = new int[12];
        int[] Harita8KulelerCan = new int[12];
        int[] Harita9 = new int[12];
        int[] Harita9KulelerMaxCan = new int[12];
        int[] Harita9KulelerCan = new int[12];
        int[] filonunAdalarininHaritalari = new int[5];
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Adalar where AdaSahibiFiloId = @filoID;";
            command.Parameters.AddWithValue("@filoID", oyuncuFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (int.Parse(reader["AdaId"].ToString()) == 5)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            Harita5[i] = int.Parse(reader["Kule" + (i+1)].ToString());
                            if (Harita5[i] > 0)
                            {
                                Harita5KulelerMaxCan[i] = GameManager.gm.HaritadakiFiloAdalari[0].transform.Find("Kule" + (i+1)).GetChild(0).GetComponent<KuleKontrol>().MaxCan;
                                Harita5KulelerCan[i] = GameManager.gm.HaritadakiFiloAdalari[0].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().Can;
                            }
                            else
                            {
                                Harita5KulelerMaxCan[i] = 0;
                                Harita5KulelerCan[i] = 0;
                            }
                        }
                        filonunAdalarininHaritalari[0] = 1;
                    }
                    else if (int.Parse(reader["AdaId"].ToString()) == 6)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            Harita6[i] = int.Parse(reader["Kule" + (i + 1)].ToString());
                            if (Harita6[i] > 0)
                            {
                                Harita6KulelerMaxCan[i] = GameManager.gm.HaritadakiFiloAdalari[1].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().MaxCan;
                                Harita6KulelerCan[i] = GameManager.gm.HaritadakiFiloAdalari[1].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().Can;
                            }
                            else
                            {
                                Harita6KulelerMaxCan[i] = 0;
                                Harita6KulelerCan[i] = 0;
                            }
                        }
                        filonunAdalarininHaritalari[1] = 1;
                    }
                    else if (int.Parse(reader["AdaId"].ToString()) == 7)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            Harita7[i] = int.Parse(reader["Kule" + (i + 1)].ToString());
                            if (Harita7[i] > 0)
                            {
                                Harita7KulelerMaxCan[i] = GameManager.gm.HaritadakiFiloAdalari[2].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().MaxCan;
                                Harita7KulelerCan[i] = GameManager.gm.HaritadakiFiloAdalari[2].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().Can;
                            }
                            else
                            {
                                Harita7KulelerMaxCan[i] = 0;
                                Harita7KulelerCan[i] = 0;
                            }
                        }
                        filonunAdalarininHaritalari[2] = 1;
                    }
                    else if (int.Parse(reader["AdaId"].ToString()) == 8)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            Harita8[i] = int.Parse(reader["Kule" + (i + 1)].ToString());
                            if (Harita8[i] > 0)
                            {
                                Harita8KulelerMaxCan[i] = GameManager.gm.HaritadakiFiloAdalari[3].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().MaxCan;
                                Harita8KulelerCan[i] = GameManager.gm.HaritadakiFiloAdalari[3].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().Can;
                            }
                            else
                            {
                                Harita8KulelerMaxCan[i] = 0;
                                Harita8KulelerCan[i] = 0;
                            }
                        }
                        filonunAdalarininHaritalari[3] = 1;
                    }
                    else if (int.Parse(reader["AdaId"].ToString()) == 9)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            Harita9[i] = int.Parse(reader["Kule" + (i + 1)].ToString());
                            if (Harita9[i] > 0)
                            {
                                Harita9KulelerMaxCan[i] = GameManager.gm.HaritadakiFiloAdalari[4].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().MaxCan;
                                Harita9KulelerCan[i] = GameManager.gm.HaritadakiFiloAdalari[4].transform.Find("Kule" + (i + 1)).GetChild(0).GetComponent<KuleKontrol>().Can;
                            }
                            else
                            {
                                Harita9KulelerMaxCan[i] = 0;
                                Harita9KulelerCan[i] = 0;
                            }
                        }
                        filonunAdalarininHaritalari[4] = 1;
                    }

                }
                reader.Close();
            }
        }
        TargetFiloIciAdalariYukle(filonunAdalarininHaritalari, Harita5, Harita6, Harita7, Harita8, Harita9, Harita5KulelerMaxCan, Harita5KulelerCan, Harita6KulelerMaxCan, Harita6KulelerCan, Harita7KulelerMaxCan, Harita7KulelerCan, Harita8KulelerMaxCan, Harita8KulelerCan, Harita9KulelerMaxCan, Harita9KulelerCan);
#endif
    }

    [TargetRpc]
    public void TargetFiloIciAdalariYukle(int[] donenFilonunAdalari, int[] donenHarita5, int[] donenHarita6, int[] donenHarita7, int[] donenHarita8, int[] donenHarita9, int[] donenHarita5KulelerMaxCan, int[] donenHarita5KulelerCan, int[] donenHarita6KulelerMaxCan, int[] donenHarita6KulelerCan, int[] donenHarita7KulelerMaxCan, int[] donenHarita7KulelerCan, int[] donenHarita8KulelerMaxCan, int[] donenHarita8KulelerCan, int[] donenHarita9KulelerMaxCan, int[] donenHarita9KulelerCan)
    {
        GameManager.gm.AddItemsFiloAdalarSlot(donenFilonunAdalari, donenHarita5, donenHarita6, donenHarita7, donenHarita8, donenHarita9, donenHarita5KulelerMaxCan, donenHarita5KulelerCan, donenHarita6KulelerMaxCan, donenHarita6KulelerCan, donenHarita7KulelerMaxCan, donenHarita7KulelerCan, donenHarita8KulelerMaxCan, donenHarita8KulelerCan, donenHarita9KulelerMaxCan, donenHarita9KulelerCan);
    }
#if UNITY_SERVER || UNITY_EDITOR
    public void botKontrolKalanIslemSayisiGuncelle()
    {
        birSonrakiBotKotntrolIcinKalanIslemSayisi -= 1;
        if(birSonrakiBotKotntrolIcinKalanIslemSayisi <= 0)
        {
            OyuncuBotKontrol();
        }
    }
#endif

    [Command]

    public void DusmanFiloGenislet(string filoKisaltmasi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        string saldiranFiloAdi=" ",savunanFiloAdi = " ";
        int SaldiranFiloToplamOldurme = 0, SavunanFiloToplamOldurme = 0, SaldiranUyeSayisi = 0, SavunanUyeSayisi = 0, SaldiranAltin = 0, SavunanAltin = 0, SaldiranAdaSayisi = 0, SavunanAdaSayisi = 0;
        int SavunanFiloId = 0, SaldiranFiloId = 0;
        string SavunanFiloKisaAd = " ", SaldiranFiloKisaAd = " ";


        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM DiplomasiSavas WHERE ((SaldiranFiloKisaAd=@FiloKisaltma and SavunanFiloKisaAd=@oyuncuFiloKisaAd) or (SaldiranFiloKisaAd=@oyuncuFiloKisaAd and SavunanFiloKisaAd=@FiloKisaltma));";
            command.Parameters.AddWithValue("@FiloKisaltma", filoKisaltmasi);
            command.Parameters.AddWithValue("@oyuncuFiloKisaAd", OyuncuFiloKisaltma);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    saldiranFiloAdi = reader["SaldiranFiloAd"].ToString();
                    savunanFiloAdi = reader["SavunanFiloAd"].ToString();
                    SaldiranFiloToplamOldurme = int.Parse(reader["SaldiranFiloToplamOldurme"].ToString());
                    SavunanFiloToplamOldurme = int.Parse(reader["SavunanFiloToplamOldurme"].ToString());
                    SavunanFiloId = int.Parse(reader["SavunanFiloId"].ToString());
                    SaldiranFiloId = int.Parse(reader["SaldiranFiloId"].ToString());
                    SavunanFiloKisaAd = reader["SavunanFiloKisaAd"].ToString();
                    SaldiranFiloKisaAd = reader["SaldiranFiloKisaAd"].ToString();
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT UyeSayisi,FiloAltin FROM Filolar WHERE FiloId=@saldirenFiloId;";
            command.Parameters.AddWithValue("@saldirenFiloId", SaldiranFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    SaldiranUyeSayisi = int.Parse(reader["UyeSayisi"].ToString());
                    SaldiranAltin = int.Parse(reader["FiloAltin"].ToString());
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT count(AdaId) FROM Adalar WHERE AdaSahibiFiloId=@saldirenFiloId;";
            command.Parameters.AddWithValue("@saldirenFiloId", SaldiranFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    SaldiranAdaSayisi = int.Parse(reader[0].ToString());
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT UyeSayisi,FiloAltin FROM Filolar WHERE FiloId=@savunanFiloId;";
            command.Parameters.AddWithValue("@savunanFiloId", SavunanFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    SavunanUyeSayisi = int.Parse(reader["UyeSayisi"].ToString());
                    SavunanAltin = int.Parse(reader["FiloAltin"].ToString());
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT count(AdaId) FROM Adalar WHERE AdaSahibiFiloId=@savunanFiloId;";
            command.Parameters.AddWithValue("@savunanFiloId", SavunanFiloId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    SavunanAdaSayisi = int.Parse(reader[0].ToString());
                }
                reader.Close();
            }
        }
        TargetDusmanFiloGenislet(SavunanFiloId, SaldiranFiloId,saldiranFiloAdi, savunanFiloAdi,  SaldiranFiloToplamOldurme, SavunanFiloToplamOldurme , SaldiranUyeSayisi , SavunanUyeSayisi , SaldiranAltin , SavunanAltin , SaldiranAdaSayisi, SavunanAdaSayisi );
#endif
    }

    [TargetRpc]
    public void TargetDusmanFiloGenislet(int donenSavunanFiloId, int donenSaldiranFiloId, string donenSaldiranFiloAdi, string donensavunanFiloAdi,int donenSaldiranFiloToplamOldurme,int donenSavunanFiloToplamOldurme, int donenSaldiranUyeSayisi, int donenSavunanUyeSayisi,int donenSaldiranAltin, int donenSavunanAltin,int donenSaldiranAdaSayisi,int donenSavunanAdaSayisi)
    {
        int toplamoldurmemax = donenSavunanFiloToplamOldurme + donenSaldiranFiloToplamOldurme;
        int toplamuyesayisimax = donenSaldiranUyeSayisi + donenSavunanUyeSayisi;
        int toplaumAltinmax = donenSavunanAltin + donenSaldiranAltin;
        int toplaumAdamax = donenSavunanAdaSayisi + donenSaldiranAdaSayisi;
        GameManager.gm.BizimToplamOldurmeSLD.maxValue = toplamoldurmemax;
        GameManager.gm.KarsiToplamOldurmeSLD.maxValue = toplamoldurmemax;
        GameManager.gm.BizimUyeSayisiSLD.maxValue = toplamuyesayisimax;
        GameManager.gm.KarsiUyeSayisiSLD.maxValue = toplamuyesayisimax;
        GameManager.gm.BizimAltinSLD.maxValue = toplaumAltinmax;
        GameManager.gm.KarsiAltinSLD.maxValue = toplaumAltinmax;
        GameManager.gm.BizimAdaSLD.maxValue = toplaumAdamax;
        GameManager.gm.KarsiAdaSLD.maxValue = toplaumAdamax;

        GameManager.gm.FiloMuttefikIsteklerSayfasi.SetActive(false);
        GameManager.gm.FiloSavasIstatistikleriSayfasi.SetActive(true);
        GameManager.gm.FiloMuttefikIstatistikSayfasi.SetActive(false);
        GameManager.gm.FiloBarisOlmaSayfasi.SetActive(false);

        if (donenSavunanFiloId == oyuncuFiloId)
        {
            GameManager.gm.BizimClanAdTXT.text = donensavunanFiloAdi.ToString();
            GameManager.gm.BizimToplamOldurmeTXT.text = donenSavunanFiloToplamOldurme.ToString();
            GameManager.gm.BizimUyeSayisiTXT.text = donenSavunanUyeSayisi.ToString();
            GameManager.gm.BizimAltinTXT.text = donenSavunanAltin.ToString();
            GameManager.gm.BizimAdaTXT.text = donenSavunanAdaSayisi.ToString();

            GameManager.gm.KarsiClanAdTXT.text = donenSaldiranFiloAdi.ToString();
            GameManager.gm.KarsiToplamOldurmeTXT.text = donenSaldiranFiloToplamOldurme.ToString();
            GameManager.gm.KarsiUyeSayisiTXT.text = donenSaldiranUyeSayisi.ToString();
            GameManager.gm.KarsiAltinTXT.text = donenSaldiranAltin.ToString();
            GameManager.gm.KarsiAdaTXT.text = donenSaldiranAdaSayisi.ToString();

            GameManager.gm.BizimToplamOldurmeSLD.value = donenSavunanFiloToplamOldurme;
            GameManager.gm.BizimUyeSayisiSLD.value = donenSavunanUyeSayisi;
            GameManager.gm.BizimAltinSLD.value = donenSavunanAltin;
            GameManager.gm.BizimAdaSLD.value = donenSavunanAdaSayisi;

            GameManager.gm.KarsiToplamOldurmeSLD.value = donenSaldiranFiloToplamOldurme;
            GameManager.gm.KarsiUyeSayisiSLD.value = donenSaldiranAltin;
            GameManager.gm.KarsiAltinSLD.value = donenSaldiranAltin;
            GameManager.gm.KarsiAdaSLD.value = donenSaldiranAdaSayisi;
        }
        else 
        {
            GameManager.gm.BizimClanAdTXT.text = donenSaldiranFiloAdi.ToString();
            GameManager.gm.BizimToplamOldurmeTXT.text = donenSaldiranFiloToplamOldurme.ToString();
            GameManager.gm.BizimUyeSayisiTXT.text = donenSaldiranUyeSayisi.ToString();
            GameManager.gm.BizimAltinTXT.text = donenSaldiranAltin.ToString();
            GameManager.gm.BizimAdaTXT.text = donenSaldiranAdaSayisi.ToString();

            GameManager.gm.KarsiClanAdTXT.text = donensavunanFiloAdi;
            GameManager.gm.KarsiToplamOldurmeTXT.text = donenSavunanFiloToplamOldurme.ToString();
            GameManager.gm.KarsiUyeSayisiTXT.text = donenSavunanUyeSayisi.ToString();        
            GameManager.gm.KarsiAltinTXT.text = donenSavunanAltin.ToString();            
            GameManager.gm.KarsiAdaTXT.text = donenSavunanAdaSayisi.ToString();


            GameManager.gm.BizimToplamOldurmeSLD.value = donenSaldiranFiloToplamOldurme;
            GameManager.gm.BizimUyeSayisiSLD.value = donenSaldiranUyeSayisi;            
            GameManager.gm.BizimAltinSLD.value = donenSaldiranAltin;            
            GameManager.gm.BizimAdaSLD.value = donenSaldiranAdaSayisi;        

            GameManager.gm.KarsiToplamOldurmeSLD.value = donenSavunanFiloToplamOldurme;
            GameManager.gm.KarsiUyeSayisiSLD.value = donenSavunanUyeSayisi;
            GameManager.gm.KarsiAltinSLD.value = donenSavunanAltin;
            GameManager.gm.KarsiAdaSLD.value = donenSavunanAdaSayisi;
        }
    }




    [Command]

    public void MuttefikFiloGenislet(string filoKisaltmasi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        string MuttefikBirKisaAd = " ", MuttefikIkiKisaAd = " ";
        string MuttefikUzunAd = " ", MuttefikIkiUzunaAd = " ";
        int MuttefikBirId = 0, MuttefikIkýId = 0;
        int muttefikBirTp=0, MuttefikBirUyeSayisi= 0,muttefikBirAdaSayisi = 0;
        int muttefikIkiTp=0, MuttefikIkiUyeSayisi = 0, muttefikIkiAdaSayisi = 0;
        string MuttefikBirKurucu = " ", MuttefikIkýKurucu = " ";

        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM DiplomasiMuttefik WHERE ((MuttefikBirKýsaAd=@FiloKisaltma and MuttefikIkiKýsaAd=@oyuncuFiloKisaAd) or (MuttefikBirKýsaAd=@oyuncuFiloKisaAd and MuttefikIkiKýsaAd=@FiloKisaltma));";
            command.Parameters.AddWithValue("@FiloKisaltma", filoKisaltmasi);
            command.Parameters.AddWithValue("@oyuncuFiloKisaAd", OyuncuFiloKisaltma);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    MuttefikBirKisaAd = reader["MuttefikBirKýsaAd"].ToString();
                    MuttefikIkiKisaAd = reader["MuttefikIkiKýsaAd"].ToString();
                    MuttefikUzunAd = reader["MuttefikBirUzunAd"].ToString();
                    MuttefikIkiUzunaAd = reader["MuttefikIkiUzunAd"].ToString();
                    MuttefikBirId = int.Parse(reader["MuttefikBirId"].ToString());
                    MuttefikIkýId = int.Parse(reader["MuttefikIkiId"].ToString());
                  
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT UyeSayisi,FiloTp FROM Filolar WHERE FiloId=@MuttefikBirId;";
            command.Parameters.AddWithValue("@MuttefikBirId", MuttefikBirId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    MuttefikBirUyeSayisi = int.Parse(reader["UyeSayisi"].ToString());
                    muttefikBirTp = GameManager.gm.FiloSeviyesiHesapla(int.Parse(reader["FiloTp"].ToString()));
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT count(AdaId) FROM Adalar WHERE AdaSahibiFiloId=@MuttefikBirId;";
            command.Parameters.AddWithValue("@MuttefikBirId", MuttefikBirId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {

                    muttefikBirAdaSayisi = int.Parse(reader[0].ToString());
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT UyeSayisi,FiloTp FROM Filolar WHERE FiloId=@MuttefikIkiId;";
            command.Parameters.AddWithValue("@MuttefikIkiId", MuttefikIkýId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    MuttefikIkiUyeSayisi = int.Parse(reader["UyeSayisi"].ToString());
                    muttefikIkiTp = GameManager.gm.FiloSeviyesiHesapla(int.Parse(reader["FiloTp"].ToString()));
                }
                reader.Close();
            }
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT count(AdaId) FROM Adalar WHERE AdaSahibiFiloId=@MuttefikIkiId;";
            command.Parameters.AddWithValue("@MuttefikIkiId", MuttefikIkýId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    muttefikIkiAdaSayisi = int.Parse(reader[0].ToString());
                }
                reader.Close();
            }
        }

        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT Kullanici_Adi FROM Kullanici WHERE FiloId=@MuttefikBirId AND YetkiId = 1;";
            command.Parameters.AddWithValue("@MuttefikBirId", MuttefikBirId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    MuttefikBirKurucu = reader["Kullanici_Adi"].ToString();
                    
                }
                reader.Close();
            }
        }


        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT Kullanici_Adi FROM Kullanici WHERE FiloId=@MuttefikIkýId AND YetkiId = 1;";
            command.Parameters.AddWithValue("@MuttefikIkýId", MuttefikIkýId);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    MuttefikIkýKurucu = reader["Kullanici_Adi"].ToString();

                }
                reader.Close();
            }
        }

        TargetMuttefikFiloGenislet(MuttefikBirId, MuttefikIkýId, MuttefikBirKisaAd, MuttefikIkiKisaAd, MuttefikUzunAd, MuttefikIkiUzunaAd, MuttefikBirUyeSayisi, muttefikBirTp, muttefikBirAdaSayisi, MuttefikBirKurucu, MuttefikIkiUyeSayisi, muttefikIkiTp, muttefikIkiAdaSayisi, MuttefikIkýKurucu);
#endif                             
    }                               
                                    
    [TargetRpc]
    public void TargetMuttefikFiloGenislet(int donenMuttefikBirID,int donenMuttefikIkýId, string donenMuttefikBirKisaAd, string donenMuttefikIkiKisaAd, string donenMuttefikBirUzunAd, string donenMuttefikIkiUzunaAd,int donenMuttefikBirUyeSayisi, int donenmuttefikBirTp, int donenmuttefikBirAdaSayisi, string donenMuttefikBirKurucu, int donenMuttefikIkiUyeSayisi, int donenmuttefikIkiTp, int donenmuttefikIkiAdaSayisi,string donenMuttefikIkýKurucu)
    {
        GameManager.gm.FiloMuttefikIsteklerSayfasi.SetActive(false);
        GameManager.gm.FiloSavasIstatistikleriSayfasi.SetActive(false);
        GameManager.gm.FiloMuttefikIstatistikSayfasi.SetActive(true);
        if (donenMuttefikBirID == oyuncuId)
        {
            GameManager.gm.MuttefikIstatistikleriKlanTagi.text = donenMuttefikBirKisaAd;
            GameManager.gm.MuttefikIstatistikleriFiloAdi.text = donenMuttefikBirUzunAd;
            GameManager.gm.MuttefikIstatistikleriFiloLideri.text = donenMuttefikBirKurucu;
            GameManager.gm.MuttefikIstatistikleriFiloSeviyesi.text = donenmuttefikBirTp.ToString();
            GameManager.gm.MuttefikIstatistikleriUyeSayisi.text = donenMuttefikBirUyeSayisi.ToString();
            GameManager.gm.MuttefikIstatistikleriAdaSayisi.text = donenmuttefikBirAdaSayisi.ToString();
        }
        else
        {
            GameManager.gm.MuttefikIstatistikleriKlanTagi.text = donenMuttefikIkiKisaAd.ToString();
            GameManager.gm.MuttefikIstatistikleriFiloAdi.text = donenMuttefikIkiUzunaAd.ToString();
            GameManager.gm.MuttefikIstatistikleriFiloLideri.text = donenMuttefikIkýKurucu.ToString();
            GameManager.gm.MuttefikIstatistikleriFiloSeviyesi.text = donenmuttefikIkiTp.ToString();
            GameManager.gm.MuttefikIstatistikleriUyeSayisi.text = donenMuttefikIkiUyeSayisi.ToString();
            GameManager.gm.MuttefikIstatistikleriAdaSayisi.text = donenmuttefikIkiAdaSayisi.ToString();
            
        }
    }


    [Command]
    public void muttefikliktenCikar(string filoKisaltmasi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2 && oyuncuFiloId > 0 && OyuncuFiloKisaltma.Length > 0)
        {
            using (var command4 = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command4.CommandText = "Delete from DiplomasiMuttefik where (((MuttefikBirKýsaAd = @filoKisaltmasi and MuttefikIkiKýsaAd = @oyuncuFiloKisaltma) or (MuttefikBirKýsaAd = @oyuncuFiloKisaltma and MuttefikIkiKýsaAd = @filoKisaltmasi)) and (SELECT YetkiId From Kullanici where ID = @oyuncuýd));";
                command4.Parameters.AddWithValue("@filoKisaltmasi", filoKisaltmasi);
                command4.Parameters.AddWithValue("@oyuncuFiloKisaltma", OyuncuFiloKisaltma);
                command4.Parameters.AddWithValue("@oyuncuýd", oyuncuId);
                if (command4.ExecuteNonQuery() == 1)
                {
                    TargetmuttefikliktenCikar();
                }
            }
        }
#endif                             
    }

    [TargetRpc]
    public void TargetmuttefikliktenCikar()
    {
        GameManager.gm.FiloMuttefikIstatistikSayfasi.SetActive(false);
        GameManager.gm.FiloiciMuttefikAc();
        GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 429], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 430]);

    }


    [Command]

    public void filoBarismaIstegiYollaSunucu(string GelenFiloAd)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2)
        {
            bool zatenIstekAtilmis = false;
            int FiloID = -1;
            string FiloAd = "";

            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloId,FiloAd FROM Filolar WHERE FiloAd=@filoAd;";
                command.Parameters.AddWithValue("@filoAd", GelenFiloAd);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        FiloID = int.Parse(reader["FiloId"].ToString());
                        FiloAd = reader["FiloAd"].ToString();
                    }
                    reader.Close();
                }
            }
            if (FiloID > 0)
            {
               
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "SELECT count(Id) FROM BarisIstekleri WHERE (IstekAtanFiloId=@IstekAtanFiloId And IstekAtilanFiloId=@IstegiKabulEdecekFiloID) or (IstekAtanFiloId=@IstegiKabulEdecekFiloID And IstekAtilanFiloId=@IstekAtanFiloId);";
                    command.Parameters.AddWithValue("@IstekAtanFiloId", oyuncuFiloId);
                    command.Parameters.AddWithValue("@IstegiKabulEdecekFiloID", FiloID);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && int.Parse(reader[0].ToString()) > 0)
                        {
                            zatenIstekAtilmis = true;
                            TargetfiloBarismaIstegiYollaSunucu(FiloAd, 3);
                        }
                        reader.Close();
                    }
                }
               
                if (zatenIstekAtilmis == false)
                {
                    using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO BarisIstekleri (IstekAtanFiloId,IstekAtilanFiloId,FiloKisaltmasi,FiloAdi) VALUES (@IstekAtanFiloId,@IstegiKabulEdecekFiloID,@FiloKisaltmasi,@FiloAdi);";
                        command.Parameters.AddWithValue("@IstekAtanFiloId", oyuncuFiloId);
                        command.Parameters.AddWithValue("@IstegiKabulEdecekFiloID", FiloID);
                        command.Parameters.AddWithValue("@FiloKisaltmasi", OyuncuFiloKisaltma);
                        command.Parameters.AddWithValue("@FiloAdi", OyuncuFiloAd);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            TargetfiloBarismaIstegiYollaSunucu(FiloAd, 1);
                        }
                    }
                }
            }
            else
            {
                TargetfiloBarismaIstegiYollaSunucu(FiloAd, 2);
            }
        }
#endif
    }
    [TargetRpc]
    public void TargetfiloBarismaIstegiYollaSunucu(string donenFiloAd, int savasAcmaSonucu)
    {
        if (savasAcmaSonucu == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 431]);
        }
        else if (savasAcmaSonucu == 2)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 432]);
        }
        else if (savasAcmaSonucu == 3)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 433]);
        }
    }


#if UNITY_SERVER || UNITY_EDITOR
    public void FiloSavasdaysaSkorEklemeKontrolu(GameObject hedef)
    {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update DiplomasiSavas SET SaldiranFiloToplamOldurme = SaldiranFiloToplamOldurme + 1 WHERE SaldiranFiloId=@oyuncuFiloId and SavunanFiloId=@rakipFiloId;";
                command.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                command.Parameters.AddWithValue("@rakipFiloId", hedef.GetComponent<Player>().oyuncuFiloId);
                if (command.ExecuteNonQuery() == 0)
                {
                    using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command2.CommandText = "Update DiplomasiSavas SET SavunanFiloToplamOldurme = SavunanFiloToplamOldurme + 1 WHERE SavunanFiloId=@oyuncuFiloId and SaldiranFiloId=@rakipFiloId;";
                        command2.Parameters.AddWithValue("@oyuncuFiloId", oyuncuFiloId);
                        command2.Parameters.AddWithValue("@rakipFiloId", hedef.GetComponent<Player>().oyuncuFiloId);
                        command2.ExecuteNonQuery();
                    }
                }
            }
    }
#endif











    [Command]
    public void FiloBarisIstegiKabulEt(string filoTagi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2)
        {
            int dostOlunacakFiloId = 0;
            string dostOlunacakFiloAd = "";
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloId,FiloAd FROM Filolar WHERE FiloKisaltma=@FiloKisaltma;";
                command.Parameters.AddWithValue("@FiloKisaltma", filoTagi);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dostOlunacakFiloId = int.Parse(reader["FiloId"].ToString());
                        dostOlunacakFiloAd = reader["FiloAd"].ToString();
                    }
                    reader.Close();
                }
            }
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Delete from BarisIstekleri where (IstekAtanFiloId = @birinciFilo and IstekAtilanFiloId=@ikinciFilo) or (IstekAtanFiloId = @ikinciFilo and IstekAtilanFiloId=@birinciFilo);";
                command.Parameters.AddWithValue("@birinciFilo", dostOlunacakFiloId);
                command.Parameters.AddWithValue("@ikinciFilo", oyuncuFiloId);
                if (command.ExecuteNonQuery() == 1)
                {
                    using (var command2 = GameManager.gm.sqliteConnection.CreateCommand())
                    {
                        command2.CommandText = "Delete from DiplomasiSavas where (SaldiranFiloId = @birinciFilo and SavunanFiloId=@ikinciFilo) or (SaldiranFiloId = @ikinciFilo and SavunanFiloId=@birinciFilo);";
                        command2.Parameters.AddWithValue("@birinciFilo", dostOlunacakFiloId);
                        command2.Parameters.AddWithValue("@ikinciFilo", oyuncuFiloId);
                        if (command2.ExecuteNonQuery() == 1)
                        {
                            TargetFiloBarisIstegiKabulEt(1);
                        }
                    }
                }
                else
                {
                    TargetFiloBarisIstegiKabulEt(0);
                }
            }

        }
#endif
    }

    [TargetRpc]
    public void TargetFiloBarisIstegiKabulEt(int muttefikEklemeSonucu)
    {
        muttefikSayfasiVerileriniCek();

        if (muttefikEklemeSonucu == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 434]);
        }
        else
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 435]);
        }
    }

    [Command]
    public void FiloBarisIstegiRedEt(string filoTagi)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (OyuncuYetkiID <= 2)
        {
            int dostOlunacakFiloId = 0;
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT FiloId FROM Filolar WHERE FiloKisaltma=@FiloKisaltma;";
                command.Parameters.AddWithValue("@FiloKisaltma", filoTagi);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dostOlunacakFiloId = int.Parse(reader["FiloId"].ToString());
                    }
                    reader.Close();
                }
            }
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Delete from BarisIstekleri where (IstekAtanFiloId = @birinciFilo and IstekAtilanFiloId=@ikinciFilo) or (IstekAtanFiloId = @ikinciFilo and IstekAtilanFiloId=@birinciFilo);";
                command.Parameters.AddWithValue("@birinciFilo", dostOlunacakFiloId);
                command.Parameters.AddWithValue("@ikinciFilo", oyuncuFiloId);
                if (command.ExecuteNonQuery() == 1)
                {
                    TargetFiloBarisIstegiRedEt(1);
                }
                else
                {
                    TargetFiloBarisIstegiRedEt(0);
                }
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetFiloBarisIstegiRedEt(int muttefikEklemeSonucu)
    {
        muttefikSayfasiVerileriniCek();
        if (muttefikEklemeSonucu == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 436]);
        }
        else
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 437]);
        }
    }

    [Command]
    public void YetenekPuaniSatiAlSunucu()
    {
#if UNITY_SERVER || UNITY_EDITOR

        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Kullanici SET KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi + 1,Altin = Altin - ((HarcananYetenekPuaniSayisi + KalanYetenekPuaniSayisi) * 4000) WHERE ID=@oyuncuId and Altin >= ((HarcananYetenekPuaniSayisi + KalanYetenekPuaniSayisi) * 4000) and (HarcananYetenekPuaniSayisi + KalanYetenekPuaniSayisi) < 90;";
            command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
            if (command.ExecuteNonQuery() == 1)
            {
                SetOyuncuAltin(oyuncuAltin - (oyuncuHarcananYetenekPuaniSayisi + oyuncuKalanYetenekPuaniSayisi) * 4000);
                SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi + 1);
                TargetYetenekPuaniSatiAlSunucu(0);
            }
            else
            {
                TargetYetenekPuaniSatiAlSunucu(1);
            }
        }
#endif
    }

    [TargetRpc]
    public void TargetYetenekPuaniSatiAlSunucu(int donenDurum)
    {
        if (donenDurum == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 534]);
        }
        else if (donenDurum == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 535]);
        }
        GameManager.gm.oyuncuSatinalYetenekBedel.text = ((oyuncuHarcananYetenekPuaniSayisi + oyuncuKalanYetenekPuaniSayisi) * 4000).ToString();

    }



    [Command]
    public void YetenekPuaniKullanSunucu(int yukseltilecekYetenekID)
    {
#if UNITY_SERVER || UNITY_EDITOR
        switch (yukseltilecekYetenekID)
        {
            case 1:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekSaldiriHizi = YetenekSaldiriHizi + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekSaldiriHizi < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekSaldiriHizi(oyuncuYetenekSaldiriHizi + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 2:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekKritikHasar = YetenekKritikHasar + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1 ,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekKritikHasar < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetnekKiritikHasar(oyuncuYetenekKritikHasar + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 3:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekKritikvuruþihtimali = YetenekKritikvuruþihtimali + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1 ,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekKritikvuruþihtimali < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekKritikVurusIhtimali(oyuncuYetenekKiritikVurusIhtimali + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 4:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekRoketHasari = YetenekRoketHasari + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekRoketHasari < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekRoketHasari(oyuncuYetenekRoketHasari + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 5:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekZipkinHasari = YetenekZipkinHasari + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekZipkinHasari < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekZipkinHasari(oyuncuYetenekZipkinHasari + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 6:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekZirhDelme = YetenekZirhDelme + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekZirhDelme < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekZirhDelme(oyuncuYetenekZirhDelme + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 7:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekIsabetorani = YetenekIsabetorani + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekIsabetorani < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekIsabetOraný(oyuncuYetenekIsabetOrani + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 8:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekMaxCan = YetenekMaxCan + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekMaxCan < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekMaxCan(oyuncuYetenekMaxCan + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 9:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenkTamir = YetenkTamir + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenkTamir < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetnekTamir(oyuncuYetenekTamir + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 10:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekKalkan = YetenekKalkan + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekKalkan < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetnekKalkan(oyuncuYetenekKalkani + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 11:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekMenzil = YetenekMenzil + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekMenzil < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekMenzil(oyuncuYetenekMenzil + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 12:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekZirh = YetenekZirh + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekZirh < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekZirh(oyuncuYetenekZirh + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 13:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekBarut = YetenekBarut + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekBarut < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekBarut(oyuncuYetenekBarut + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 14:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekGemiHizi = YetenekGemiHizi + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekGemiHizi < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekGemiHizi(oyuncuYetnekGemiHizi + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 15:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekHizTasi = YetenekHizTasi + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekHizTasi < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekHizTasi(oyuncuYetenekHizTasi + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;

            case 16:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekPVEHasar = YetenekPVEHasar + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekPVEHasar < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekPVEHasar(oyuncuYetenekPVEHasar + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;
            case 17:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekZipkinMenzili = YetenekZipkinMenzili + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekZipkinMenzili < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekZipkinMenzili(oyuncuYetenekZipkinMenzili + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;
            case 18:
                using (var command = GameManager.gm.sqliteConnection.CreateCommand())
                {
                    command.CommandText = "Update Kullanici SET YetenekZipkinSaldiriHizi = YetenekZipkinSaldiriHizi + 1 ,KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi - 1,HarcananYetenekPuaniSayisi = HarcananYetenekPuaniSayisi + 1 WHERE ID=@oyuncuId and KalanYetenekPuaniSayisi >= 1 and YetenekZipkinSaldiriHizi < 10;";
                    command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        SetOyuncuYetenekZipkinSaldiriHizi(oyuncuYetenekZipkinSaldiriHizi + 1);
                        SetOyuncuKalanYetenekPuaniSayisi(oyuncuKalanYetenekPuaniSayisi - 1);
                        SetOyuncuHarcananYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + 1);
                    }
                }
                break;
        }

#endif
    }
    [TargetRpc]
    public void TargetYetenekPuaniKullanSunucu(int yetenekId)
    {
        if (yetenekId == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 272],GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 426]);
        }
        else if (yetenekId == 2)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 272], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 428]);
        }
        else if (yetenekId == 3)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 272], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 429]);
        }
        else if (yetenekId == 4)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 272], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 430]);
        }
        else if (yetenekId == 5)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 272], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 431]);
        }
        else if (yetenekId == 6)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 272], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 432]);
        }
    }

    [Command]
    public void YetenekPuaniSifirlaSunucu()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (oyuncuHarcananYetenekPuaniSayisi > 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET KalanYetenekPuaniSayisi = KalanYetenekPuaniSayisi + HarcananYetenekPuaniSayisi,Altin = Altin - (HarcananYetenekPuaniSayisi * 500), HarcananYetenekPuaniSayisi = 0,YetenekBarut = 0,YetenekGemiHizi = 0,YetenekHizTasi = 0,YetenekPVEHasar = 0,YetenekZipkinMenzili = 0,YetenekZipkinSaldiriHizi = 0,YetenekMaxCan = 0,YetenkTamir = 0,YetenekKalkan = 0,YetenekMenzil = 0,YetenekZirh = 0,YetenekSaldiriHizi = 0,YetenekKritikHasar = 0,YetenekKritikvuruþihtimali = 0,YetenekRoketHasari = 0,YetenekZipkinHasari = 0,YetenekZirhDelme = 0,YetenekIsabetorani = 0 WHERE ID=@oyuncuId and Altin >= (HarcananYetenekPuaniSayisi * 500) and HarcananYetenekPuaniSayisi > 0;";
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin - ((oyuncuHarcananYetenekPuaniSayisi + oyuncuKalanYetenekPuaniSayisi) * 500));
                    SetOyuncuKalanYetenekPuaniSayisi(oyuncuHarcananYetenekPuaniSayisi + oyuncuKalanYetenekPuaniSayisi);
                    SetOyuncuYetenekZipkinSaldiriHizi(0);
                    SetOyuncuYetenekHizTasi(0);
                    SetOyuncuYetenekGemiHizi(0);
                    SetOyuncuYetenekBarut(0);
                    SetOyuncuYetenekZirh(0);
                    SetOyuncuYetenekMenzil(0);
                    SetOyuncuYetnekKalkan(0);
                    SetOyuncuYetnekTamir(0);
                    SetOyuncuYetenekMaxCan(0);
                    SetOyuncuYetenekIsabetOraný(0);
                    SetOyuncuYetenekZirhDelme(0);
                    SetOyuncuYetenekZipkinHasari(0);
                    SetOyuncuYetenekRoketHasari(0);
                    SetOyuncuYetenekKritikVurusIhtimali(0);
                    SetOyuncuYetnekKiritikHasar(0);
                    SetOyuncuYetenekSaldiriHizi(0);
                    SetOyuncuYetenekZipkinMenzili(0);
                    SetOyuncuYetenekPVEHasar(0);
                    SetOyuncuHarcananYetenekPuaniSayisi(0);
                    TargetYetenekPuaniSifirlaSunucu(0);
                }
                else
                {
                    TargetYetenekPuaniSifirlaSunucu(1);
                }
            }
        }
        
#endif
    }
    [TargetRpc]
    public void TargetYetenekPuaniSifirlaSunucu(int donenDurum)
    {
        if (donenDurum == 0)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 536]);
        }
        else if (donenDurum == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 537]);
        }
    }

    [Command]
    public void SunucuBaglantiYukleme(int deger)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (deger != GameManager.gm.Version)
        {
            TargetSunucuBaglantiYukleme(1);
        }
#endif
    }

    [TargetRpc]
    public void TargetSunucuBaglantiYukleme(int donenDurum)
    {
        if (donenDurum == 1)
        {
            GameManager.gm.OyunGuncelleScreen.SetActive(true);
        }
    }

    [Command]
    public void X2LostCoinAktiflikDurumuYukle()
    {
#if UNITY_SERVER || UNITY_EDITOR
            TargetX2LostCoinAktiflikDurumuYukle(GameManager.gm.ikiyeKatlamaAktiflikDurumu,(10800f - (Time.time - GameManager.gm.ikiyeKatlamaBaslangicZamani)));
#endif
    }

    [TargetRpc]
    public void TargetX2LostCoinAktiflikDurumuYukle(bool LostCoinIkiyeKatlanmaAktiflikDurumu, float kalanZaman)
    {
        if (LostCoinIkiyeKatlanmaAktiflikDurumu == true)
        {
#if UNITY_ANDROID
            GameManager.gm.AndroidIkýyeKatlamaIcon.SetActive(true);
            GameManager.gm.AndroidIkýyeKatlamaKalanZamanTex.SetActive(true);
            GameManager.gm.AndroidIkýyeKatlamaKalanZamanTex.GetComponent<LostCoinKatlamaText>().kalanZaman = kalanZaman;
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinIkýyeKatlamaIcon.SetActive(true);
            GameManager.gm.WinIkýyeKatlamaKalanZamanText.SetActive(true);
            GameManager.gm.WinIkýyeKatlamaKalanZamanText.GetComponent<LostCoinKatlamaText>().kalanZaman = kalanZaman;
#endif
        }
        else
        {
#if UNITY_ANDROID
            GameManager.gm.AndroidIkýyeKatlamaIcon.SetActive(false);
            GameManager.gm.AndroidIkýyeKatlamaKalanZamanTex.SetActive(false);
            GameManager.gm.AndroidIkýyeKatlamaKalanZamanTex.GetComponent<LostCoinKatlamaText>().kalanZaman = 0;
#endif
#if UNITY_STANDALONE_WIN
            GameManager.gm.WinIkýyeKatlamaIcon.SetActive(false);
            GameManager.gm.WinIkýyeKatlamaKalanZamanText.SetActive(false);
            GameManager.gm.WinIkýyeKatlamaKalanZamanText.GetComponent<LostCoinKatlamaText>().kalanZaman = 0;
#endif
        }
    }

    [Command]
    public void TopSatSunucu(int miktar, int satilacakTopId)
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (satilacakTopId == 0)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                Oyuncutoplamsatisfiyatý = 25 * miktar;
                command.CommandText = "Update Kullanici SET onBeslikKGTopDepo = onBeslikKGTopDepo - " + miktar + ", Altin = Altin + " + Oyuncutoplamsatisfiyatý + " WHERE ID=@oyuncuId and onBeslikKGTopDepo >=@miktar;";
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                command.Parameters.AddWithValue("@miktar", miktar);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin + Oyuncutoplamsatisfiyatý);
                    SetOyuncuOnBesKilolukTopDepo(oyuncuOnBesKilolukTopDepo - miktar);
                    TargetTopSatSunucu(0);

                }
            }
        }
        else if (satilacakTopId == 1)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                Oyuncutoplamsatisfiyatý = 125 * miktar;
                command.CommandText = "Update Kullanici SET yirmiBeslikKGTopDepo = yirmiBeslikKGTopDepo - " + miktar + ", Altin = Altin + " + Oyuncutoplamsatisfiyatý + " WHERE ID=@oyuncuId and yirmiBeslikKGTopDepo >= @miktar;";
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                command.Parameters.AddWithValue("@miktar", miktar);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin + Oyuncutoplamsatisfiyatý);
                    SetOyuncuYirmiBesKilolukTopDepo(oyuncuYirmiBesKilolukTopDepo - miktar);
                    TargetTopSatSunucu(0);
                }
            }
        }
        else if (satilacakTopId == 2)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                Oyuncutoplamsatisfiyatý = 400 * miktar;
                command.CommandText = "Update Kullanici SET yirmiYediBucukKGTopDepo = yirmiYediBucukKGTopDepo - " + miktar + ", Altin = Altin + " + Oyuncutoplamsatisfiyatý + " WHERE ID=@oyuncuId and yirmiYediBucukKGTopDepo >= @miktar;";
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                command.Parameters.AddWithValue("@miktar", miktar);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin + Oyuncutoplamsatisfiyatý);
                    SetOyuncuYirmiYediBucukKilolukTopDepo(oyuncuYirmiYediBucukKilolukTopDepo - miktar);
                    TargetTopSatSunucu(0);

                }
            }
        }
        else if (satilacakTopId == 3)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                Oyuncutoplamsatisfiyatý = 4000 * miktar;
                command.CommandText = "Update Kullanici SET otuzKilolukTopDepo = otuzKilolukTopDepo - " + miktar + ", Altin = Altin + " + Oyuncutoplamsatisfiyatý + " WHERE ID=@oyuncuId and otuzKilolukTopDepo >= @miktar;";
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                command.Parameters.AddWithValue("@miktar", miktar);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin + Oyuncutoplamsatisfiyatý);
                    SetOyuncuOtuzKilolukTopDepo(oyuncuOtuzKilolukTopDepo - miktar);
                    TargetTopSatSunucu(0);

                }
            }
        }
        else if (satilacakTopId == 4)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                Oyuncutoplamsatisfiyatý = 15000 * miktar;
                command.CommandText = "Update Kullanici SET otuzBesKilolukTopDepo = otuzBesKilolukTopDepo - " + miktar + ", Altin = Altin + " + Oyuncutoplamsatisfiyatý + " WHERE ID=@oyuncuId and otuzBesKilolukTopDepo >= @miktar;";
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                command.Parameters.AddWithValue("@miktar", miktar);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin + Oyuncutoplamsatisfiyatý);
                    SetOyuncuOtuzBesKilolukTopDepo(oyuncuOtuzBesKilolukTopDepo - miktar);
                    TargetTopSatSunucu(0);
                }
            }
        }
        else if (satilacakTopId == 5)
        {
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                Oyuncutoplamsatisfiyatý = 35000 * miktar;
                command.CommandText = "Update Kullanici SET CifteVurusTopDepo = CifteVurusTopDepo - " + miktar + ", Altin = Altin + " + Oyuncutoplamsatisfiyatý + " WHERE ID=@oyuncuId and CifteVurusTopDepo >= @miktar;";
                command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
                command.Parameters.AddWithValue("@miktar", miktar);
                if (command.ExecuteNonQuery() == 1)
                {
                    SetOyuncuAltin(oyuncuAltin + Oyuncutoplamsatisfiyatý);
                    TargetSetCifteVurusTopDepo(oyuncuCifteVurusTopDepo - miktar);
                    TargetTopSatSunucu(0);
                }
            }
        }
        else
        {
            TargetTopSatSunucu(1);
        }
#endif
    }
    [TargetRpc]
    public void TargetTopSatSunucu(int donenSonuc)
    {
        if (donenSonuc == 0)
        {
            GameManager.gm.TopSatmaPaneli.SetActive(false);
            GameManager.gm.TopSatmaMiktarSlider.value = 1;
            GameManager.gm.TopSatmaMiktarAdetiTXT.text = "1";
            GameManager.gm.TopSatmaBedeliTopTXT.text = "0";
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 558]);
        }
        else if (donenSonuc == 1)
        {
            GameManager.gm.BildirimOlustur(GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 535]);
        }
    }


    [Command]
    public void GorevKontrolu()
    {
        if (haritaBirBesSandikToplaGoreviSayac >= 5 && haritaBirOnNpcKesGoreviSayac >= 10)
        {
            SetOyuncuAltin(oyuncuAltin + 1500000);
        }
    }

}