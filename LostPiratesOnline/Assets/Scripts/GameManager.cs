using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using Mirror;
using System;
using UnityEngine.AI;
using TMPro;
using UnityEngine.EventSystems;
using MySql.Data.MySqlClient;
public class GameManager : NetworkBehaviour
{
    [Header("OyunIciUi")]
    public GameObject WindowsSurumUi;
    public GameObject AndroidSurumUi;

    public bool EtkinlikAktiflikDurumu = false;

    private bool tusisteniyor = false;
    public int istenentusid = -1;
    public Text SaldirButtontext, SaldirDurdurButtontext, GemiyeKilitlenButtontext, RoketAtButtontext, barutAktifKeyKodText, KalkanAktifKeyKodText, hiztasikeykodtext, haritaatlakeykodtext,tamirolkeykodtext;


    [Header("KisayolTuslari")]
    public KeyCode saldirkeykodu = KeyCode.Q, saldirdurdurkeykod = KeyCode.E, gemiyekilitlenkeykod = KeyCode.Space, roketAtKeyKod = KeyCode.F, barutAktifYapkeykod = KeyCode.Z, kalkanAktifYapkeykod = KeyCode.X, hiztasikeykod = KeyCode.C, haritaatlakeykod = KeyCode.V, tamirolkeykod = KeyCode.T;

    public CameraBounds[] kameraLimitleri;
    public Text OyuncuSeviye;
    public GameObject Dumen;
    public Text ValueText;
    public Slider slider;
    public Text ValueTextCan;
    public Slider sliderCan;
    static public GameManager gm;
    public int Version = 1;
    public Text oyuncuTecrubePuaniText;
    public Text KalkanText, BarutText, HizTasiText, RoketText,TamirText;
    public Text SeciliGulleText;
    public Text WindowsSeciliGulleText;
    public Text WindowsSeciliZipkinText;
    public GameObject Cursor;
    public Image DegistirilecekGulle;
    public Image WindowsDegistirilecekGulle;
    public Image WindowsDegistirilecekZipkin;
    public Sprite[] GulleSprite, zipkinSprite;
    public Text HaritaTXT;
    public GameObject ToplarMarket,CephaneMarket,VipMarket,PromosyonKoduMarket;
    public GameObject VipmArketSlot8, VipmArketSlot9, VipmArketSlot10;
    public GameObject dilDegistirme;

#if UNITY_SERVER || UNITY_EDITOR
    public SqliteConnection sqliteConnection;
#endif

    public GameObject BenimGemim;
    public string kullaniciAdi = "", sifre = "";
    public GameObject OyuniciAryuz;
    public GameObject GirisYapMenu;
    public GameObject Chat, mobilChat;
    public string kullanicilvl = "";
    public GameObject Kamera;
    public GameObject[] HaritaBirSistemGemi = new GameObject[2];
    public GameObject[] HaritaIkistemGemi = new GameObject[2];
    public GameObject[] HaritaUcSistemGemi = new GameObject[2];
    public GameObject[] HaritaDortSistemGemi = new GameObject[2];
    public GameObject[] HaritaBesSistemGemi = new GameObject[2];
    public GameObject[] HaritaAltiSistemGemi = new GameObject[2];
    public GameObject[] HaritaYediSistemGemi = new GameObject[2];
    public GameObject[] HaritaSekizSistemGemi = new GameObject[2];
    public GameObject[] HaritaDokuzSistemGemi = new GameObject[2];
    public GameObject BaskinHaritasiSistemGemi;
    public GameObject BaskinHaritasiAdmiralGemi;

    public GameObject EtkinlikHaritaBirSistemGemi;
    public GameObject EtkinlikHaritaIkistemGemi;
    public GameObject EtkinlikHaritaUcSistemGemi;
    public GameObject EtkinlikHaritaDortSistemGemi;
    public GameObject EtkinlikHaritaBesSistemGemi;
    public GameObject EtkinlikHaritaAltiSistemGemi;
    public GameObject EtkinlikHaritaYediSistemGemi;
    public GameObject EtkinlikHaritaSekizSistemGemi;
    public GameObject EtkinlikHaritaDokuzSistemGemi;
    public GameObject[] EtkinlikDenizYaratiklari;
    public GameObject EtkinlikAdmiral;
    public GameObject AdmiralBoss;

    public GameObject EtkinlikKiyametSistemGemisiEzik;
    public GameObject EtkinlikKiyametSistemGemisiOrta;
    public GameObject EtkinlikKiyametSistemGemisiGuclu;

    public GameObject HaritaBirYaratik;
    public GameObject HaritaIkiYaratik;
    public GameObject HaritaUcYaratik;
    public GameObject HaritaDortYaratik;
    public GameObject HaritaBesYaratik;
    public GameObject HaritaAltiYaratik;
    public GameObject HaritaYediYaratik;
    public GameObject HaritaSekizYaratik;
    public GameObject HaritaDokuzYaratik;
    public GameObject HaritaBirSandýk;

    public GameObject[] Kuleler;


    public GameObject hedefgemi;
    public GameObject SaldiriIptalButton;
    public Text SaldirilanNpcIsmi;
    public GameObject BenSaldiriyorum;
    public GameObject BenSaldiriyorumNpcSlider;
    public Text BenSaldýrýyorumCanText;
    public Text ObanaSaldiriyorIsmiText;
    public GameObject OabnaSaldiriyorBilgiPaneli;
    public GameObject ObanasaldiriyorSlider;
    public Text ObanaSaldiriyorCanText;
    public GameObject DayNightSystem;
    public GameObject PointLight2D;
    public Text AltinText;
    public Text SavasPuani, BatirilanBot, ToplananSandik, BatirilanOyuncu, AdamaVerilenHasar, OnlineSuresi, YapilanGorevler, KayitTarihi;
    public Text ProfilTecrubePuani, ProfilElitPuani, ProfilOyuncuAdi;
    public Toggle BeniHatirla;
    public InputField kullaniciAdiGirisYapText, sifreGirisYapText;
    public Text marketElitPuani, marketAltýn;
    public Text Lostcoin;
    public Text OdulText;
    public Text OdulText2;
    public GameObject SaldirButton;
    public List<string> seyirItems;
    public Text[] SeyirDefteriTextleri = new Text[20];
    public int i = 0;
    public int x;
    public DateTime SunucuZamani;
    public Text SunucuZamaniText;
    public int y;
    public DateTime dateTime;
    public Image[] depodakiToplarSlot;
    public Text[] DepoSlotText;
    public Image[] gemidekiToplarSlot;
    public Text[] GemiSlotText;
    public Sprite[] TopImage;
    public Sprite SlotBoslukKontrol;
    public Sprite SlotBoslukKontrol2;
    public String SatinAlinacakObje;
    public Text[] MiktarAdeti, MarketFiyatText;
    /// <summary>
    public GameObject Sozlesme;
    public GameObject oyuncuOlduPanel;
    public GameObject[] KullanButon;
    public GameObject MiktarButon;
    public GameObject haritaAtlaButtonAndroid, haritaAtlaButtonWindows;
    public GameObject SiralamMenu;
    public GameObject NormalSiralama, RutbeSiralamsi, FiloSiralamasi;
    public GameObject Etkinlik;
    public GameObject barutAcKapatButton, kalkanAcKapatButton, roketAcKapatButton;

    public Image myImage;
    public Image ParaGosterge;
    public Image DepoSlot1;
    public Image DepoSlot2;
    public Image DepoSlot3;

    public Sprite AltinIMG;
    public Sprite Gemi1Image;
    public Sprite Gemi2Image;
    public Sprite Gemi3Image;
    public Sprite Gemi4Image;
    public Sprite Gemi5Image;
    public Sprite Gemi6Image;
    public Sprite Gemi7Image;
    public Sprite Gemi8Image;
    public Sprite Gemi9Image;
    public Sprite Gemi10Image;
    public Sprite Gemi11Image;
    public Sprite Gemi12Image;
    public Sprite TasGulleImage;
    public Sprite DemirGulleImage;
    public Sprite SifaGulleTopImage;
    public Sprite AlevGulleImage;
    public Sprite HavaiFisekImage;
    public Sprite BarutImage;
    public Sprite KalkanImage;
    public Sprite RoketImage;
    public Sprite HizTasiImage;
    public Sprite PaslanmisZipkinImage;
    public Sprite GumusZipkinImage;
    public Sprite AltinZipkinImage;
    public Text[] SiralmaAdTXT;
    public Text[] SiralmaPuanTXT;
    public Button[] SiralmaProfilBTN;
    public Text Slot1TXT, Slot2TXT, Slot3TXT;
    public Text PanelSlot1TXT, PanelSlot2TXT, PanelSlot3TXT;
    public Text SayfaKontrolTasarimlar;
    public Text BaslikTextObjem;
    public Text OzellikTextObjem;
    public Text FiyatTextObjem;
    public Text dogmaSaniye;
    public Text SirlamaBaslik;
    public Text SirlamaBaslikAd;
    public Text FiloSirlamaBaslik;
    public Text FiloSirlamaBaslikAd;
    public GameObject[] TopSlot;
    public GameObject[] TasarýmSlot;
    public GameObject[] CephaneSlot;
    public int maxSeyirItems = 20;
    /// </summary>
    public Image GulleSlot1IMG, GulleSlot2IMG, GulleSlot3IMG, GulleSlot4IMG, GulleSlot5IMG;
    public Text GulleSlot1TXT, GulleSlot2TXT, GulleSlot3TXT, GulleSlot4TXT, GulleSlot5TXT, GulleSlot6TXT, GulleSlot7TXT, GulleSlot8TXT,GulleSlot9TXT, GulleSlot10TXT,GulleSlot11TXT, GulleSlot12TXT, GulleSlot13TXT, GulleSlot14TXT;
    public Text WindowsGulleSlot1TXT, WindowsGulleSlot2TXT, WindowsGulleSlot3TXT, WindowsGulleSlot4TXT, WindowsGulleSlot5TXT, WindowsGulleSlot6TXT, WindowsGulleSlot7TXT, WindowsGulleSlot8TXT;
    public Text WindowsZipkinSlot1TXT, WindowsZipkinSlot2TXT, WindowsZipkinSlot3TXT, WindowsZipkinSlot4TXT, WindowsZipkinSlot5TXT, WindowsZipkinSlot6TXT;
    public GameObject GüllePanel, GulleSaðOK, GulleSolOK;
    public GameObject WindowsGüllePanel,YukariOk;
    public GameObject WindowsZipkinPanel, ZipkinYukariOk;

    public bool OyunaGirisYapildimi = false;
    public float odulYazisiTimer = 5.0f;
    public Text mevcutRutbePuanText, altRutbePuanText, ustRutbePuanText;
    public Image mevcutRutbePuanImage, altRutbePuanImage, ustRutbePuanImage;
    public Sprite[] rutbeResimler;
    public Text GemiTopSayisiDurumuText;
    public Camera MiniMapCamera, MainCamera;
    public GameObject OldurmeEfekti;
    public GameObject  AyarlarMailDegistirmePaneli, AyarlarIsimDegistermePaneli, AyarlarSifreDegistirmePaneli;
    public GameObject FiloKurma, FiloArama, FiloBaslangic;
    public Text FiloKisaAd, FiloUzunAd, FiloAciklama;
    public GameObject FiloIcý, FiloAna, FiloKurulacakKisim;
    public Text  BildirimPaneliBaslikText, BildirimPaneliMesajText;
    public GameObject BildirimPaneli;
    public GameObject FiloiciGenel, FiloiciUyeler, FiloiciMuttefik, FiloiciAdalar, FiloiciBagis;
    public Text FiloIcýFiloAdTXT, FiloÝciFiloLeveliTXT, FiloÝciFiloSonrakiLeveliTXT;
    public Text FiloAdiTXTArama, FiloKisaltmasiTXTArama;
    public GameObject[] FiloAramaSlot;
    public GameObject[] FiloBasvurular;
    public Text FiloyaBagisAltinTXT;
    public string[] FiloUyelerininAdlari;
    public GameObject OyundanCýkmaSiyahEkraný;
    public Text OyundanCýkmaSiyahEkranýTXT;
    public float OyundanCýkýstimer = 10f;
    public GameObject OyunUI;
    public TMP_Dropdown FiloUyelerDropDown;
    public Text ProfilOyuncuFiloAdi;
    public GameObject MarketSatinAlButton;
    List<string> SeyirDefteriliste = new List<string>();
    List<string> GuncellemlerListe = new List<string>();
    public float SalýrýyorumBilgiMenutimer = 0.0f;
    public float sonBizeSaldirilanZaman = 0.0f;
    public InputField FiloiciGenelBakisAciklama;
    public Slider KameraKaydýrmaSlider;
    List<string> FiloSeyirDefteriliste = new List<string>();
    public Text FiloKasasiAltinTxt;
    public Sprite[] FiloÝconlarý;
    public Image IconRgpt, IconProfilIMG;
    public Image BayrakIconProfilBackround, BayrakRBTbacround;
    public AudioSource GemiGulleAtmaSesi, GemiZipkinAtmaSesi;
    public Toggle arkaPlanSes;
    public GameObject NormalAyarlar,KlavyeAyarlari;
    public GameObject YenidenÝsinlanTXT, YenidenÝsinlanBTN;
    public Slider FiloSlider;
    public Text FiloSliderTpTXT;
    public GameObject BuyukMapSt1GemiIcon, BuyukMapSt2GemiIcon, BuyukMapSt3GemiIcon, BuyukMapSt4GemiIcon, BuyukMapSt5GemiIcon, BuyukMapSt6GemiIcon, BuyukMapSt7GemiIcon, BuyukMapSt8GemiIcon, BuyukMapSt9GemiIcon;
  [Header("WindowsUI")]
    public Text WindowsKalkanText, WindowsBarutText, WindowsHizTasiText, WindowsRoketText;
    public GameObject WindowsbarutAcKapatButton, WindowskalkanAcKapatButton, WindowsroketAcKapatButton;
    public GameObject WindowsSaldirButton, WindowsSaldirIptalButton;
    public Slider SesSlider;
    public GameObject SesAcikIMG, SesKapaliIMG;
    public Text WinCanTXT, WinTecrubePuaniTxt, WinLvlTXT, WinAltinTXT;
    public Slider WinCanSLÝDER, WinTecrubePuaniSlider;
    public Text WinHangiMapTXT, WinZamanTXT;
    public DateTime WinSunucuZamani;
    public GameObject WinMiniMapArkaPlan;
    public GameObject WindowsDumen;
    public InputField winchatinputfield;
    public GameObject Profil, Market, Tershane, SeyirDefteri, Siralama, Ayarlar, YetenekSistemi;
    public Dropdown TershaneDropDown;
    public Dropdown SiralamaDropDown;
    public Image TershaneKullanilanGemiIMG;
    public Text SiralamaBirinciAdTXT, SiralamaIkinciAdTXT, SiralamaUcuncuAdTXT;
    public Text SiralamaBirinciPuanTXT, SiralamaIkinciPuanTXT, SiralamaUcuncuPuanTXT;
    public Text SiralamaBirinciFiloTXT, SiralamaIkinciFiloTXT, SiralamaUcuncuFiloTXT;
    public GameObject AyarlarGrafik, AyarlarSes, AyarlarKontrol, AyarlarDestek, AyarlarDil, AyarlarHesapAyarlari;
    public GameObject AyarlarDestekYeniDestekTalebi, AyarlarDestekTaleplerim, AyarlarMesajlasma;
    public Dropdown destekTalebiOlusturDropdown;
    public GameObject destekTalebiOlusturINPT;
    public GameObject destekTalebiOlusturBTN;
    public GameObject destekTalebiMesajINPT;
    public GameObject destekTalebiMesajGonderBTN;
    public GameObject destekTalebiKilitleBTN;
    public GameObject GulleSecmePanel, ZipkinSecmePanel;
    public Text KullaniciAdiKayitOl, SifreKayitOl, EpostaKayitOl, EpostaTekrarKayitOl;
    public Dropdown dildegitirmeDropdown;
    public Dropdown AyarlardildegitirmeDropdown;
    public Text winTamirText;
    public GameObject SifremiUnuttumPaneli, GirisYapPaneli,KayitIOlPaneli;
    public GameObject WindowsUstPanelVipIcon, WindowsUstPanelSandikKatlamaIcon, WindowsUstPanelAltinBossIcon, WindowsUstPanelTpBossIcon;
    public GameObject AndroidUstPanelVipIcon, AndroidUstPanelSandikKatlamaIcon, AndroidUstPanelAltinBossIcon, AndroidUstPanelTpBossIcon;
    public GameObject Paket1Enebled, Paket2Enebled;
    public GameObject PremiumPaketEnebled, SandikKatlamaEnebled;
    public GameObject AltýnBossPaketEnebled, XpBossEnebled;
    public Text VersionTxt;
    public GameObject AyarlarIsimDegistirBTN, AyarlarSifreDegistirBTN, AyarlarmailDegistirBTN;
    public Text AyarlarIsimDegistirYeniNickTXT, AyarlarIsimDegistirMailText;
    public GameObject kullaniciMailDegistrimeOnayKoduPaneli;
    public Text KullanicidanDonenYeniMail;
    public GameObject AyarlarMailDegistirmeGonderBTN, AyarlarMailDegistirmeMailINPT;
    public Text AyarlarSifreDegistirMail, SifreDegistirSifre, SifreDegistirTekrarSifre;
    public Text BenimSiralamamSira, BenimSiralamamAd, BenimSiralamamPuan;
    public Text AyarlarDestekKullaniciId;
    public FixedJoystick kamerakaydirmajoyistik;
    public GameObject SunucuTahtasiObje;
    public Toggle kamerakaydirmajoyistikAyari;
    public Toggle SunucuTahtasi;
    public Text EtkinlikSayaciWin, EtkinlikSayaciAndroid;
    public Text OyuncuCephanePaketiAdet;
    public Text EtkinlikBirincisiAd, EtkinlikÝkincisiAd, EtkinlikUcuncuAd, EtkinlikKendiSýramAd;
    public Text EtkinlikBirincisiPuan, EtkinlikÝkincisiPuan, EtkinlikUcuncuPuan;
    public Text  EtkinlikKendiSýramPuan;
    public Text  EtkinlikKendiSýram;
    public Slider OyuncuEtkinlikPuaniSlider;
    public Image EtkinlikYildiz1, EtkinlikYildiz2, EtkinlikYildiz3;
    public Image[] GununOduluSlot;
    public Image[] GecmisOdulSlot;
    public GameObject GunlukGirisPaneli;
    public GameObject OyuncuBotKontrolPaneli;
    public Text RandomText;
    public Text RandomInput;
    public RawImage botkontrolrastgeleimage;
    public Text kullanicidanDonTextenText;
    public Text MaksimumUyesayisi;
    public GameObject FiloiciUyelerPaneli;
    public GameObject FiloDusmanPaneli, FiloIsteklerPaneli,FiloMuttefikPaneli;
    public GameObject FiloDusmanButton, FiloMuttefikButton, FiloIsteklerButton;
    public GameObject FiloDusmanAdiGirINPT, FiloDostAdiGirINPT, FiloDusmanIstegiYollaBTN, FiloDostIstegiYollaBTN;
    public GameObject FiloOfisi;
    public GameObject DunyaBildirimi;
    public Text DunyaBildirimiTXT;
    public GameObject[] KuleIslemlerSlotlari;
    public GameObject[] HaritadakiFiloAdalari;
    public Sprite[] AdaKuleSprite;
    public GameObject FiloSavasIstatistikleriSayfasi, FiloMuttefikIsteklerSayfasi,FiloMuttefikIstatistikSayfasi,FiloBarisOlmaSayfasi;
    public Text AdaEkstraBonusu;
    public Text BizimClanAdTXT, KarsiClanAdTXT, BizimToplamOldurmeTXT, KarsiToplamOldurmeTXT, BizimUyeSayisiTXT, KarsiUyeSayisiTXT, BizimAltinTXT, KarsiAltinTXT, BizimAdaTXT, KarsiAdaTXT;
    public Slider BizimToplamOldurmeSLD, KarsiToplamOldurmeSLD, BizimUyeSayisiSLD, KarsiUyeSayisiSLD, BizimAltinSLD, KarsiAltinSLD, BizimAdaSLD, KarsiAdaSLD;
    public Text MuttefikIstatistikleriKlanTagi, MuttefikIstatistikleriFiloAdi, MuttefikIstatistikleriFiloLideri, MuttefikIstatistikleriFiloSeviyesi, MuttefikIstatistikleriUyeSayisi, MuttefikIstatistikleriAdaSayisi;
    List<string> DunyaBildirimiTextleri = new List<string>();
    public float enSonBildirimYapilanZaman = -40f;
    public Text[] YetenekPuanlariSlot;
    public Text YetenekBedeliTXT;
    public Text oyuncuSatinalYetenekBedel;
    public GameObject OyunGuncelleScreen;
    public GameObject WinIkýyeKatlamaIcon, AndroidIkýyeKatlamaIcon;
    public GameObject WinIkýyeKatlamaKalanZamanText, AndroidIkýyeKatlamaKalanZamanTex;
    public Image TopSatmaPaneliTopIMG;
    public Slider TopSatmaMiktarSlider;
    public Text TopSatmaBedeliTopTXT;
    public GameObject TopSatmaPaneli;
    public Text TopSatmaMiktarAdetiTXT;
    public GameObject TopSatisKullanButton;
    public GameObject GorevlerScroll, EtkinlikScroll;
    public Text PvpHaritasiButtonText, PveHaritasiButtonText;
    public GameObject CikButtonPVE, CikButtonPVP;
    public GameObject GirButtonPVE, GirButtonPVP;
    public GameObject[] OzelGemiSatinAlBTN, OzelGemiKullanBTN, OzelGemiFiyat;
    public Dropdown GemiMarketiTasarýmlarDropDown;
    public GameObject OzelTasarimlarScroll, EpTasarimlarScroll;
    public Text HaritaBirOnNpcKesmeGoreviText,HaritaBirBesSandikToplamaGoreviText;
    public Text HaritaIkiOnNpcKesmeGoreviText,HaritaIkiBesNpcCanavarKesGoreviText, HaritaIkiOnSandikToplamaGoreviText;
    public Sprite[] etkinlikSandiklari;

    public Text HaritaUcOnBesNpcKesmeGoreviText,HaritaUcSekizNpcCanavarKesGoreviText, HaritaUcYirmiSandikToplamaGoreviText;
    public Text HaritaDortYirmiNpcKesmeGoreviText,HaritaDortOnIkiNpcCanavarKesGoreviText, HaritaDortKirkSandikToplamaGoreviText;
    public Text HaritaBesOtuzNpcKesmeGoreviText,HaritaBesOnSekizNpcCanavarKesGoreviText, HaritaBesAtmisSandikToplamaGoreviText;

    public GameObject FiloiciUyeleritemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloiciUyelercontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloiciUyeleritemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloiciUyeleritemHeight;        // Öðe yüksekliði
    public float FiloiciUyelerspacing;           // Öðeler arasýndaki boþluk
    public float FiloiciUyelercontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloiciUyeleritemList;  // Oluþturulan öðelerin listesi
    public float FiloiciUyelercontentHeight;         // Ýçerik yüksekliði


    public GameObject FiloiciBasvurulartemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloiciBasvurularcontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloiciBasvurularitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloiciBasvurularitemHeight;        // Öðe yüksekliði
    public float FiloiciBasvurularspacing;           // Öðeler arasýndaki boþluk
    public float FiloiciBasvurularcontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloiciBasvurularitemList;  // Oluþturulan öðelerin listesi
    public float FiloiciBasvurularcontentHeight;         // Ýçerik yüksekliði


    public GameObject SeyirDefteriitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform SeyirDeftericontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int SeyirDefteriitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float SeyirDefteriitemHeight;        // Öðe yüksekliði
    public float SeyirDefterispacing;           // Öðeler arasýndaki boþluk
    public float SeyirDeftericontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> SeyirDefteriitemList;  // Oluþturulan öðelerin listesi
    public float SeyirDeftericontentHeight;         // Ýçerik yüksekliði


    public GameObject GuncellemeleritemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform Guncellemelercontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int GuncellemeleritemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float GuncellemeleritemHeight;        // Öðe yüksekliði
    public float Guncellemelerspacing;           // Öðeler arasýndaki boþluk
    public float GuncellemelercontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> GuncellemeleritemList;  // Oluþturulan öðelerin listesi
    public float GuncellemelercontentHeight;         // Ýçerik yüksekliði


    public GameObject FiloGenelSiralamaitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloGenelSiralamacontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloGenelSiralamaitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloGenelSiralamaitemHeight;        // Öðe yüksekliði
    public float FiloGenelSiralamaspacing;           // Öðeler arasýndaki boþluk
    public float FiloGenelSiralamacontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloGenelSiralamaitemList;  // Oluþturulan öðelerin listesi
    public float FiloGenelSiralamacontentHeight;         // Ýçerik yüksekliði



    public GameObject FiloAramaitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloAramacontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloAramatemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloAramaitemHeight;        // Öðe yüksekliði
    public float FiloAramaspacing;           // Öðeler arasýndaki boþluk
    public float FiloAramacontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloAramaitemList;  // Oluþturulan öðelerin listesi
    public float FiloAramacontentHeight;         // Ýçerik yüksekliði


    public GameObject FiloBagisSeyirDefteriitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloBagisSeyirDeftericontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloBagisSeyirDefteriitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloBagisSeyirDefteriitemHeight;        // Öðe yüksekliði
    public float FiloBagisSeyirDefterispacing;           // Öðeler arasýndaki boþluk
    public float FiloBagisSeyirDeftericontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloBagisSeyirDefteriitemList;  // Oluþturulan öðelerin listesi
    public float FiloBagisSeyirDeftericontentHeight;         // Ýçerik yüksekliði


    public GameObject[] FiloCevrimiciitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloCevrimicicontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloCevrimiciitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloCevrimiciitemHeight;        // Öðe yüksekliði
    public float FiloCevrimicispacing;           // Öðeler arasýndaki boþluk
    public float FiloCevrimicicontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloCevrimiciitemList;  // Oluþturulan öðelerin listesi
    public float FiloCevrimicicontentHeight;         // Ýçerik yüksekliði


    public GameObject MenuSiralamaitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform MenuSiralamacontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int MenuSiralamaitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float MenuSiralamaitemHeight;        // Öðe yüksekliði
    public float MenuSiralamaspacing;           // Öðeler arasýndaki boþluk
    public float MenuSiralamacontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> MenuSiralamaitemList;  // Oluþturulan öðelerin listesi
    public float MenuSiralamacontentHeight;         // Ýçerik yüksekliði


    public GameObject DestekTalebiDurumuitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform DestekTalebiDurumucontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int DestekTalebiDurumuitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float DestekTalebiDurumuitemHeight;        // Öðe yüksekliði
    public float DestekTalebiDurumuspacing;           // Öðeler arasýndaki boþluk
    public float DestekTalebiDurumucontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> DestekTalebiDurumuitemList;  // Oluþturulan öðelerin listesi
    public float DestekTalebiDurumucontentHeight;         // Ýçerik yüksekliði


    public GameObject DestekTalebiKullaniciMesajitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform DestekTalebiKullaniciMesajcontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int DestekTalebiKullaniciMesajitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float DestekTalebiKullaniciMesajitemHeight;        // Öðe yüksekliði
    public float DestekTalebiKullaniciMesajspacing;           // Öðeler arasýndaki boþluk
    public float DestekTalebiKullaniciMesajcontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> DestekTalebiKullaniciMesajitemList;  // Oluþturulan öðelerin listesi
    public float DestekTalebiKullaniciMesajcontentHeight;         // Ýçerik yüksekliði


    public GameObject DestekTalebiAdminMesajitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform DestekTalebiAdminMesajcontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int DestekTalebiAdminMesajitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float DestekTalebiAdminMesajitemHeight;        // Öðe yüksekliði
    public float DestekTalebiAdminMesajspacing;           // Öðeler arasýndaki boþluk
    public float DestekTalebiAdminMesajcontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> DestekTalebiAdminMesajitemList;  // Oluþturulan öðelerin listesi
    public float DestekTalebiAdminMesajcontentHeight;         // Ýçerik yüksekliði



    public GameObject FiloSiralamasiitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloSiralamasicontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloSiralamasiitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloSiralamasiitemHeight;        // Öðe yüksekliði
    public float FiloSiralamasispacing;           // Öðeler arasýndaki boþluk
    public float FiloSiralamasicontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloSiralamasiitemList;  // Oluþturulan öðelerin listesi
    public float FiloSiralamasicontentHeight;         // Ýçerik yüksekliði


    public GameObject FiloMuttefikBasvurulariitemPrefab;   // Oluþturulacak öðelerin prefabý
    public GameObject BarisTeklifleriitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloMuttefikBasvurularicontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloMuttefikBasvurulariitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloMuttefikBasvurulariitemHeight;        // Öðe yüksekliði
    public float FiloMuttefikBasvurularispacing;           // Öðeler arasýndaki boþluk
    public float FiloMuttefikBasvurularicontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloMuttefikBasvurulariitemList;  // Oluþturulan öðelerin listesi
    public float FiloMuttefikBasvurularicontentHeight;         // Ýçerik yüksekliði


    public GameObject FiloMuttefikitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloMuttefikcontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloMuttefikitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloMuttefikitemHeight;        // Öðe yüksekliði
    public float FiloMuttefikspacing;           // Öðeler arasýndaki boþluk
    public float FiloMuttefikcontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloMuttefikitemList;  // Oluþturulan öðelerin listesi
    public float FiloMuttefikcontentHeight;         // Ýçerik yüksekliði



    public GameObject FiloDusmanitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloDusmancontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloDusmanitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloDusmanitemHeight;        // Öðe yüksekliði
    public float FiloDusmanspacing;           // Öðeler arasýndaki boþluk
    public float FiloDusmancontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloDusmanitemList;  // Oluþturulan öðelerin listesi
    public float FiloDusmancontentHeight;         // Ýçerik yüksekliði



    public GameObject FiloDiplomasiPanelitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloDiplomasiPanelcontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloDiplomasiPanelitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloDiplomasiPanelitemHeight;        // Öðe yüksekliði
    public float FiloDiplomasiPanelspacing;           // Öðeler arasýndaki boþluk
    public float FiloDiplomasiPanelcontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloDiplomasiPanelitemList;  // Oluþturulan öðelerin listesi
    public float FiloDiplomasiPanelcontentHeight;         // Ýçerik yüksekliði


    public GameObject FiloAdalarSlotitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform FiloAdalarSlotcontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int FiloAdalarSlotitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float FiloAdalarSlotitemHeight;        // Öðe yüksekliði
    public float FiloAdalarSlotspacing;           // Öðeler arasýndaki boþluk
    public float FiloAdalarSlotcontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> FiloAdalarSlotitemList;  // Oluþturulan öðelerin listesi
    public float FiloAdalarSlotcontentHeight;         // Ýçerik yüksekliði

    public GameObject OlumTablosuitemPrefab;   // Oluþturulacak öðelerin prefabý
    public RectTransform OlumTablosucontent;  // ScrollView içindeki içeriðin RectTransform bileþeni
    public int OlumTablosuitemCount;           // Baþlangýçta oluþturulacak öðe sayýsý
    public float OlumTablosuitemHeight;        // Öðe yüksekliði
    public float OlumTablosuspacing;           // Öðeler arasýndaki boþluk
    public float OlumTablosucontentPadding;    // Ýçerik içindeki boþluk

    public List<GameObject> OlumTablosuitemList;  // Oluþturulan öðelerin listesi
    public float OlumTablosucontentHeight;         // Ýçerik yüksekliði

    public Image imageCooldownHiz;
    public Image imageCooldownSaldiri;
    public Image WindowsimageCooldownHiz;
    public Image WindowsimageCooldownSaldiri;




    public TMP_Text textColldownHiz;
    public TMP_Text textColldownSaldiri;
    public TMP_Text WindowstextColldownHiz;
    public TMP_Text WindowstextColldownSaldiri;


    public bool ÝsCooldownHiz = false;
    public float cooldowntimeHiz = 5.0f;
    public float cooldowntimerHiz = 5.0f;

    public bool ÝsCooldownSaldiri = false;
    public float cooldowntimeSaldiri = 10.0f;
    public float cooldowntimerSaldiri = 10.0f;

    public int tutulanTopId = -1;
    public GameObject miniMapArkaPlan;
    public Sprite[] miniMapArkaPlanlar;
    public int destekNo = -1;
    public int Sira = 0;
    public int EtkinlikSira = 1;
    public bool oyundancikiscancel = false;
    public int EtkinlikKiyametGemisiSayac = 0;
    public float BotkontrolActiveTimer = 0.0f;


#if UNITY_SERVER || UNITY_EDITOR
    private string server = "45.141.151.74";
    private string database = "sql_lostpirateso";
    private string uid = "sql_lostpirateso";
    private string password = "3YhDfCMyp5S4z2fm";
    private string connectionString;
    public MySqlConnection connection;
#endif
    public int KiyametEtkinligiNpcSayýsý = 0;
    public int[] EtkinlikHaritaNpcSayac;
    public int[] AdmiralHaritaNpcSayac;
    public int BaskinHaritasiAdmiralNpcSayacPVE, BaskinHaritasiAdmiralNpcSayacPVP;

    public Text filoIsteklerFiloAdi;
    public Text filoIsteklerKisaFiloAdi;
    public Text filoIsteklerFiloLideriAdi;
    public Text filoIsteklerFiloSeviyesi;
    public Text filoIsteklerFiloUyeSayisi;
    public Text filoIsteklerFiloAdaSayisi;


    public Text filoBarisIsteklerFiloAdi;
    public Text filoBarisIsteklerKisaFiloAdi;
    public Text filoBarisIsteklerFiloLideriAdi;
    public Text filoBarisIsteklerFiloSeviyesi;
    public Text filoBarisIsteklerFiloUyeSayisi;
    public Text filoBarisIsteklerFiloAdaSayisi;

    public GameObject[] FiloAdalari;
    public String[] FiloAdalariSahibiKisaAdlari;
    public Text[] miniHaritaFiloKisaAdlari;
    private int denkGelinenOyuncununHaritasi = 1;
    private Vector2 denkGelinenOyuncununKonumu = new Vector2();

    public bool ikiyeKatlamaAktiflikDurumu = false;
    public float ikiyeKatlamaBaslangicZamani = 0f;

    void Awake()
    {
#if UNITY_SERVER || UNITY_EDITOR
        Application.targetFrameRate = 30;
#endif
        gm = this;
    }
    private void Start()
    {
        VersionTxt.text = "Version : " + Version;
        EtkinlikHaritaNpcSayac = new int[10];
        AdmiralHaritaNpcSayac = new int[10];
        FiloAdalariSahibiKisaAdlari = new string[5];
#if UNITY_STANDALONE_WIN
        WindowsSurumUi.SetActive(true);
        TuslariYukle();
#endif
#if UNITY_ANDROID
        AndroidSurumUi.SetActive(true);
#endif
        if (PlayerPrefs.GetInt("Isaret", 0) == 1)
        {
            BeniHatirla.isOn = true;
            kullaniciAdiGirisYapText.text = PlayerPrefs.GetString("benihatirlakadi", "");
            sifreGirisYapText.text = PlayerPrefs.GetString("benihatirlasifre", "");
        }
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            sqliteConnection = new SqliteConnection("URI=file:OyuncuVerileri.db");
            sqliteConnection.Open();
            using (var command = sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Sunucu where id = 1;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SunucuZamani = System.DateTime.Parse(reader["Sunucu"].ToString());
#if UNITY_ANDROID
                        SunucuZamaniText.text = SunucuZamani.ToString("dd/MM/yyyy");
#endif
#if UNITY_STANDALONE_WIN
                        WinZamanTXT.text = SunucuZamani.ToString("dd/MM/yyyy");
#endif
                    }

                    reader.Close();
                }
            }
            InvokeRepeating("RutbePuanGuncelle", 0f, 68400f);
            InvokeRepeating("GunlukSavasPuaniTablosunuSifirla", 0f, 68400f);
            OyuncuAktiflikDurumuSifirla();
            FiloAdalariYukle();
            
            NpcDogur();
            BaskinHaritasiNpcDogur();
            if (EtkinlikAktiflikDurumu)
            {
                EtkinlikNpcDogur();
            }
            //InvokeRepeating("EtkinlikKiyametNpcDogurEzik", 0f, 3f);
#if UNITY_SERVER && !UNITY_EDITOR
            InvokeRepeating("WebSitesiOdemeKontrol", 0f, 300f);
#endif
            //  InvokeRepeating("SistemMesajiYolla", 600f, 600f);
        }
#endif
    }

#if UNITY_SERVER || UNITY_EDITOR
    public void GunlukSavasPuaniTablosunuSifirla()
    {
        using (var command = sqliteConnection.CreateCommand())
        {
            command.CommandText = "Delete from GunlukSavasPuaniTablosu;";
            command.ExecuteNonQuery();
        }
    }
    public void OyuncuAktiflikDurumuSifirla()
    {
        using (var command = sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Kullanici SET AktiflikDurumu = 0;";
            command.ExecuteNonQuery();
        }
    }
    public void RutbePuanGuncelle()
    {
        using (var command = sqliteConnection.CreateCommand())
        {
            command.CommandText = "" +
                "Update Kullanici SET RutbePuan = ((TecrubePuani/100) + (ElitPuan/300) + SavasPuani)  WHERE TecrubePuani < 60 and OyunYetkisiDurumu = 0;" +
                "Update Kullanici SET RutbePuan = ((TecrubePuani/100) + (ElitPuan/300) + SavasPuani) * 2 WHERE TecrubePuani >= 60 and TecrubePuani < 400 and OyunYetkisiDurumu = 0;" +
                "Update Kullanici SET RutbePuan = ((TecrubePuani/100) + (ElitPuan/300) + SavasPuani) * 3 WHERE TecrubePuani >= 400 and TecrubePuani < 3000 and OyunYetkisiDurumu = 0;" +
                "Update Kullanici SET RutbePuan = ((TecrubePuani/100) + (ElitPuan/300) + SavasPuani) * 4 WHERE TecrubePuani >= 3000 and TecrubePuani < 8000 and OyunYetkisiDurumu = 0;" +
                "Update Kullanici SET RutbePuan = ((TecrubePuani/100) + (ElitPuan/300) + SavasPuani) * 5 WHERE TecrubePuani >= 8000 and TecrubePuani < 25000 and OyunYetkisiDurumu = 0;" +
                "Update Kullanici SET RutbePuan = ((TecrubePuani/100) + (ElitPuan/300) + SavasPuani) * 6 WHERE TecrubePuani >= 25000 and TecrubePuani < 50000 and OyunYetkisiDurumu = 0;" +
                "Update Kullanici SET RutbePuan = ((TecrubePuani/100) + (ElitPuan/300) + SavasPuani) * 7 WHERE TecrubePuani >= 50000 and TecrubePuani < 100000 and OyunYetkisiDurumu = 0;" +
                "Update Kullanici SET RutbePuan = ((TecrubePuani/100) + (ElitPuan/300) + SavasPuani) * 8 WHERE TecrubePuani >= 100000 and OyunYetkisiDurumu = 0;" +
                "";
            command.ExecuteNonQuery();
        }
    }
    public void EtkinlikBossDogumKontrolu()
    {
        for (int i = 0; i < 9; i++)
        {
            if (EtkinlikHaritaNpcSayac[i] >= 250)
            {
                EtkinlikAdmiralDogurSunucu("Frozen", "EtkinlikAdmiral - " + (i + 1), (i + 1));
                EtkinlikHaritaNpcSayac[i] = 0;
            }
        }
    }

    public void AdmiralDogumKontrolu()
    {
        for (int i = 0; i < 9; i++)
        {
            if (AdmiralHaritaNpcSayac[i] == 300)
            {
                BossDogurSunucu(i + 1);
                AdmiralHaritaNpcSayac[i] = 0;
            }
        }
    }
    public void BaskinHaritalariAdmiralDogumKontrolu()
    {
        if (BaskinHaritasiAdmiralNpcSayacPVE == 300)
        {
            BossDogurSunucu(98);
            BaskinHaritasiAdmiralNpcSayacPVE = 0;
        }
        if (BaskinHaritasiAdmiralNpcSayacPVP == 300)
        {
            BossDogurSunucu(97);
            BaskinHaritasiAdmiralNpcSayacPVP = 0;
        }
    }
#endif



    public void FiloAdalariYukle()
    {
#if UNITY_SERVER || UNITY_EDITOR
        int[,] adadakiKuleler = new int[5, 12];
        using (var command = sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Adalar;";
            using (IDataReader reader = command.ExecuteReader())
            {
                int i = 0;
                while (reader.Read())
                {
                    FiloAdalari[i].GetComponent<FiloAdasiOfisi>().AdaSahibiFiloId = int.Parse(reader["AdaSahibiFiloId"].ToString());
                    FiloAdalariSahibiKisaAdlari[i] = reader["AdaSahibiFiloKisaAd"].ToString();
                    adadakiKuleler[i, 0] = int.Parse(reader["Kule1"].ToString());
                    adadakiKuleler[i, 1] = int.Parse(reader["Kule2"].ToString());
                    adadakiKuleler[i, 2] = int.Parse(reader["Kule3"].ToString());
                    adadakiKuleler[i, 3] = int.Parse(reader["Kule4"].ToString());
                    adadakiKuleler[i, 4] = int.Parse(reader["Kule5"].ToString());
                    adadakiKuleler[i, 5] = int.Parse(reader["Kule6"].ToString());
                    adadakiKuleler[i, 6] = int.Parse(reader["Kule7"].ToString());
                    adadakiKuleler[i, 7] = int.Parse(reader["Kule8"].ToString());
                    adadakiKuleler[i, 8] = int.Parse(reader["Kule9"].ToString());
                    adadakiKuleler[i, 9] = int.Parse(reader["Kule10"].ToString());
                    adadakiKuleler[i, 10] = int.Parse(reader["Kule11"].ToString());
                    adadakiKuleler[i, 11] = int.Parse(reader["Kule12"].ToString());

                    //Kule1
                    if (int.Parse(reader["Kule1"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule1"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 1).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 1";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 1 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 1;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule1"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule1"].ToString())), 1, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule2
                    if (int.Parse(reader["Kule2"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule2"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 2).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 2";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 2 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 2;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule2"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule2"].ToString())), 2, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule3
                    if (int.Parse(reader["Kule3"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule3"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 3).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 3";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 3 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 3;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule3"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule3"].ToString())), 3, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule4
                    if (int.Parse(reader["Kule4"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule4"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 4).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 4";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 4 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 4;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule4"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule4"].ToString())), 4, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule5
                    if (int.Parse(reader["Kule5"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule5"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 5).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 5";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 5 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 5;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule5"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule5"].ToString())), 5, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule6
                    if (int.Parse(reader["Kule6"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule6"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 6).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 6";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 6 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 6;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule6"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule6"].ToString())), 6, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule7
                    if (int.Parse(reader["Kule7"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule7"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 7).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 7";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 7 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 7;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule7"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule7"].ToString())), 7, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule8
                    if (int.Parse(reader["Kule8"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule8"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 8).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 8";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 8 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 8;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule8"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule8"].ToString())), 8, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule9
                    if (int.Parse(reader["Kule9"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule9"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 9).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 9";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 9 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 9;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule9"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule9"].ToString())), 9, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule10
                    if (int.Parse(reader["Kule10"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule10"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 10).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 10";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 10 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 10;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule10"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule10"].ToString())), 10, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule11
                    if (int.Parse(reader["Kule11"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule11"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 11).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 11";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 11 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 11;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule11"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule11"].ToString())), 11, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    //Kule12
                    if (int.Parse(reader["Kule12"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule12"].ToString()) - 1], HaritadakiFiloAdalari[i].transform.Find("Kule" + 12).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 12";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[i];
                        kule.name = "Tower 12 (" + (i + 5) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (i + 5);
                        kule.GetComponent<KuleKontrol>().hangiKule = 12;

                        NetworkServer.Spawn(kule);
                    }
                    else if (int.Parse(reader["Kule12"].ToString()) < 0)
                    {
                        StartCoroutine(SunucuKuleDikmeSayacBaslat(Mathf.Abs(int.Parse(reader["Kule12"].ToString())), 12, (i + 5), int.Parse(reader["AdaSahibiFiloId"].ToString()), FiloAdalariSahibiKisaAdlari[i]));
                    }
                    i++;
                }
                reader.Close();
                ClienRpcFiloAdalariYukleEleDegistirenAda(FiloAdalariSahibiKisaAdlari);
            }
        }
#endif
    }

    public void FiloAdalariYukleEleDegistirenAda(int harita)
    {
#if UNITY_SERVER || UNITY_EDITOR
        int[,] adadakiKuleler = new int[1, 12];
        using (var command = sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Adalar where AdaId = @harita;";
            command.Parameters.AddWithValue("@harita", harita);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    FiloAdalari[harita - 5].GetComponent<FiloAdasiOfisi>().AdaSahibiFiloId = int.Parse(reader["AdaSahibiFiloId"].ToString());
                    FiloAdalariSahibiKisaAdlari[harita - 5] = reader["AdaSahibiFiloKisaAd"].ToString();
                    adadakiKuleler[0, 0] = int.Parse(reader["Kule1"].ToString());
                    adadakiKuleler[0, 1] = int.Parse(reader["Kule2"].ToString());
                    adadakiKuleler[0, 2] = int.Parse(reader["Kule3"].ToString());
                    adadakiKuleler[0, 3] = int.Parse(reader["Kule4"].ToString());
                    adadakiKuleler[0, 4] = int.Parse(reader["Kule5"].ToString());
                    adadakiKuleler[0, 5] = int.Parse(reader["Kule6"].ToString());
                    adadakiKuleler[0, 6] = int.Parse(reader["Kule7"].ToString());
                    adadakiKuleler[0, 7] = int.Parse(reader["Kule8"].ToString());
                    adadakiKuleler[0, 8] = int.Parse(reader["Kule9"].ToString());
                    adadakiKuleler[0, 9] = int.Parse(reader["Kule10"].ToString());
                    adadakiKuleler[0, 10] = int.Parse(reader["Kule11"].ToString());
                    adadakiKuleler[0, 11] = int.Parse(reader["Kule12"].ToString());
                    //Kule1
                    if (int.Parse(reader["Kule1"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule1"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 1).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 1";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 1 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 1;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule2
                    if (int.Parse(reader["Kule2"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule2"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 2).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 2";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 2 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 2;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule3
                    if (int.Parse(reader["Kule3"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule3"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 3).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 3";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 3 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 3;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule4
                    if (int.Parse(reader["Kule4"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule4"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 4).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 4";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 4 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 4;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule5
                    if (int.Parse(reader["Kule5"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule5"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 5).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 5";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 5 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 5;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule6
                    if (int.Parse(reader["Kule6"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule6"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 6).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 6";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 6 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 6;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule7
                    if (int.Parse(reader["Kule7"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule7"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 7).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 7";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 7 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 7;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule8
                    if (int.Parse(reader["Kule8"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule8"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 8).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 8";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 8 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 8;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule9
                    if (int.Parse(reader["Kule9"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule9"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 9).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 9";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 9 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 9;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule10
                    if (int.Parse(reader["Kule10"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule10"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 10).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 10";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 10 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 10;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule11
                    if (int.Parse(reader["Kule11"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule11"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 11).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 11";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 11 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 11;
                        NetworkServer.Spawn(kule);
                    }
                    //Kule12
                    if (int.Parse(reader["Kule12"].ToString()) > 0)
                    {
                        GameObject kule = Instantiate(Kuleler[int.Parse(reader["Kule12"].ToString()) - 1], HaritadakiFiloAdalari[harita - 5].transform.Find("Kule" + 12).transform);
                        kule.GetComponent<KuleKontrol>().geminame = "Tower 12";
                        kule.GetComponent<KuleKontrol>().filoKisaltmasi = FiloAdalariSahibiKisaAdlari[harita - 5];
                        kule.name = "Tower 12 (" + (harita) + ")";
                        kule.GetComponent<KuleKontrol>().HaritaKac = (harita);
                        kule.GetComponent<KuleKontrol>().hangiKule = 12;
                        NetworkServer.Spawn(kule);
                    }
                }
                reader.Close();
                ClienRpcFiloAdalariYukleEleDegistirenAda(FiloAdalariSahibiKisaAdlari);
            }
        }
#endif
    }

    [ClientRpc]
    public void ClienRpcFiloAdalariYukleEleDegistirenAda(string[] adaSahibiFiloKisaAdlari)
    {
        FiloAdalariSahibiKisaAdlari = adaSahibiFiloKisaAdlari;
        for (int i = 0; i < FiloAdalariSahibiKisaAdlari.Length; i++)
        {
            miniHaritaFiloKisaAdlari[i].text = FiloAdalariSahibiKisaAdlari[i];
        }
    }

    public void ApplyCooldownHiz()
    {
        cooldowntimerHiz -= Time.deltaTime;
        if (cooldowntimerHiz < 0.0f)
        {
            ÝsCooldownHiz = false;
            WindowstextColldownHiz.gameObject.SetActive(false);
            textColldownHiz.gameObject.SetActive(false);

            imageCooldownHiz.fillAmount = 0.0f;
            WindowsimageCooldownHiz.fillAmount = 0.0f;
        }
        else
        {
            textColldownHiz.text = ((int)(cooldowntimerHiz)).ToString();
            WindowstextColldownHiz.text = ((int)(cooldowntimerHiz)).ToString();
            imageCooldownHiz.fillAmount = cooldowntimerHiz / cooldowntimeHiz;
            WindowsimageCooldownHiz.fillAmount = cooldowntimerHiz / cooldowntimeHiz;

        }
    }

    public void UseSpellHiz()
    {
        if (ÝsCooldownHiz)
        {
            //return false;
        }
        else
        {
            ÝsCooldownHiz = true;
            textColldownHiz.gameObject.SetActive(true);
            WindowstextColldownHiz.gameObject.SetActive(true);
            cooldowntimerHiz = cooldowntimeHiz;
            // return true;
        }
    }

    public void ApplyCooldownSaldiri()
    {
        cooldowntimerSaldiri -= Time.deltaTime;
        if (cooldowntimerSaldiri < 0.0f)
        {
            ÝsCooldownSaldiri = false;
            textColldownSaldiri.gameObject.SetActive(false);
            WindowstextColldownSaldiri.gameObject.SetActive(false);

            imageCooldownSaldiri.fillAmount = 0.0f;
            WindowsimageCooldownSaldiri.fillAmount = 0.0f;

        }
        else
        {
            textColldownSaldiri.text = ((int)(cooldowntimerSaldiri)).ToString();
            WindowstextColldownSaldiri.text = ((int)(cooldowntimerSaldiri)).ToString();

            imageCooldownSaldiri.fillAmount = cooldowntimerSaldiri / cooldowntimeSaldiri;
            WindowsimageCooldownSaldiri.fillAmount = cooldowntimerSaldiri / cooldowntimeSaldiri;

        }
    }

    public void UseSpellSaldiri()
    {
        if (ÝsCooldownSaldiri)
        {
            //return false;
        }
        else
        {
            ÝsCooldownSaldiri = true;
            textColldownSaldiri.gameObject.SetActive(true);
            cooldowntimerSaldiri = cooldowntimeSaldiri;
            // return true;
        }
    }
    public void GüllnonACtive()
    {
        if (GüllePanel != null)
        {
            Animator animator = GüllePanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
    public void WindowsGüllnonACtive()
    {
        if (GüllePanel != null)
        {
            Animator animator = WindowsGüllePanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
    public void WindowsZipkinnonACtive()
    {
        if (GüllePanel != null)
        {
            Animator animator = WindowsZipkinPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
    private void Update()
    {
#if !UNITY_SERVER || UNITY_EDITOR
        if (OyunaGirisYapildimi)
        {
            SoldakiSaldiriBilgiMenusu();
            BanasaldiranBilgiMenusu();

#if UNITY_STANDALONE_WIN
            BasilanTuslariKontrol();
            TusAtama();
#endif
        }
#endif
#if UNITY_SERVER || UNITY_EDITOR
        if (DunyaBildirimiTextleri.Count > 0 && Time.time - enSonBildirimYapilanZaman > 40)
        {
            enSonBildirimYapilanZaman = Time.time;
            oyunDunyasiBildirimGoster(DunyaBildirimiTextleri[0]);
            DunyaBildirimiTextleri.RemoveAt(0);
        }
#endif

    }



    public void SoldakiSaldiriBilgiMenusu()
    {
        if (SalýrýyorumBilgiMenutimer >= -10.0f)
        {
            SalýrýyorumBilgiMenutimer -= Time.deltaTime;
        }
        if (hedefgemi != null && SalýrýyorumBilgiMenutimer >= 9.0f)
        {
            if (hedefgemi.tag == "SistemGemisi")
            {
                SaldirilanNpcIsmi.text = hedefgemi.GetComponent<SistemGemisiKontrol>().geminame;
                BenSaldýrýyorumCanText.text = hedefgemi.GetComponent<SistemGemisiKontrol>().Can + "/" + hedefgemi.GetComponent<SistemGemisiKontrol>().MaxCan.ToString();
                CephaneSpriteDegistir(BenimGemim.GetComponent<Player>().seciligulleid);
            }
            else if (hedefgemi.tag == "EtkinlikGemisi")
            {
                SaldirilanNpcIsmi.text = hedefgemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame;
                BenSaldýrýyorumCanText.text = hedefgemi.GetComponent<EtkinlikSistemGemileriKontrol>().Can + "/" + hedefgemi.GetComponent<EtkinlikSistemGemileriKontrol>().MaxCan.ToString();
                CephaneSpriteDegistir(BenimGemim.GetComponent<Player>().seciligulleid);
            }
            else if (hedefgemi.tag == "EtkinlikBossu")
            {
                SaldirilanNpcIsmi.text = hedefgemi.GetComponent<EtkinlikBossKontrol>().geminame;
                BenSaldýrýyorumCanText.text = hedefgemi.GetComponent<EtkinlikBossKontrol>().Can + "/" + hedefgemi.GetComponent<EtkinlikBossKontrol>().MaxCan.ToString();
                CephaneSpriteDegistir(BenimGemim.GetComponent<Player>().seciligulleid);
            }
            else if (hedefgemi.tag == "DenizYaratigi")
            {
                SaldirilanNpcIsmi.text = hedefgemi.GetComponent<SistemYaratikKontrol>().geminame;
                BenSaldýrýyorumCanText.text = hedefgemi.GetComponent<SistemYaratikKontrol>().Can + "/" + hedefgemi.GetComponent<SistemYaratikKontrol>().MaksCan.ToString();
                CephaneSpriteDegistir(BenimGemim.GetComponent<Player>().seciliZipkinId + 5);
            }
            else if (hedefgemi.tag == "Oyuncu")
            {
                SaldirilanNpcIsmi.text = hedefgemi.GetComponent<Player>().oyuncuadi;
                BenSaldýrýyorumCanText.text = hedefgemi.GetComponent<Player>().Can + "/" + hedefgemi.GetComponent<Player>().MaksCan.ToString();
                CephaneSpriteDegistir(BenimGemim.GetComponent<Player>().seciligulleid);
            }
            else if (hedefgemi.tag == "Tower")
            {
                SaldirilanNpcIsmi.text = hedefgemi.GetComponent<KuleKontrol>().geminame;
                BenSaldýrýyorumCanText.text = hedefgemi.GetComponent<KuleKontrol>().Can + "/" + hedefgemi.GetComponent<KuleKontrol>().MaxCan.ToString();
                CephaneSpriteDegistir(BenimGemim.GetComponent<Player>().seciligulleid);
            }
            BenSaldiriyorum.SetActive(true);
        }
        else if (SalýrýyorumBilgiMenutimer < 0f && SalýrýyorumBilgiMenutimer > -0.5f)
        {
            BenSaldiriyorum.SetActive(false);
            SaldiriDurdur();

        }
    }
    public void BanasaldiranBilgiMenusu()
    {
        if (BenimGemim != null)
        {
            if (BenimGemim.GetComponent<Player>().sonsaldiranoyuncu != null && Time.time - sonBizeSaldirilanZaman <= 6.0f)
            {
                if (BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.CompareTag("SistemGemisi"))
                {
                    OabnaSaldiriyorBilgiPaneli.SetActive(true);
                    ObanaSaldiriyorIsmiText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<SistemGemisiKontrol>().geminame;
                    ObanaSaldiriyorCanText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<SistemGemisiKontrol>().Can + "/" + BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<SistemGemisiKontrol>().MaxCan.ToString();
                }
                if (BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.CompareTag("EtkinlikGemisi"))
                {
                    OabnaSaldiriyorBilgiPaneli.SetActive(true);
                    ObanaSaldiriyorIsmiText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<EtkinlikSistemGemileriKontrol>().geminame;
                    ObanaSaldiriyorCanText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<EtkinlikSistemGemileriKontrol>().Can + "/" + BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<EtkinlikSistemGemileriKontrol>().MaxCan.ToString();
                }
                if (BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.CompareTag("EtkinlikBossu"))
                {
                    OabnaSaldiriyorBilgiPaneli.SetActive(true);
                    ObanaSaldiriyorIsmiText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<EtkinlikBossKontrol>().geminame;
                    ObanaSaldiriyorCanText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<EtkinlikBossKontrol>().Can + "/" + BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<EtkinlikBossKontrol>().MaxCan.ToString();
                }
                if (BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.CompareTag("Tower"))
                {
                    OabnaSaldiriyorBilgiPaneli.SetActive(true);
                    ObanaSaldiriyorIsmiText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<KuleKontrol>().geminame;
                    ObanaSaldiriyorCanText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<KuleKontrol>().Can + "/" + BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<KuleKontrol>().MaxCan.ToString();
                }
                else if (BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.CompareTag("Oyuncu"))
                {
                    OabnaSaldiriyorBilgiPaneli.SetActive(true);
                    ObanaSaldiriyorIsmiText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<Player>().oyuncuadi;
                    ObanaSaldiriyorCanText.text = BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<Player>().Can + "/" + BenimGemim.GetComponent<Player>().sonsaldiranoyuncu.GetComponent<Player>().MaksCan.ToString();
                }
            }
            else
            {
                BenimGemim.transform.Find("OyuncuCanvas/Cember").gameObject.GetComponent<Image>().color = Color.white;
                OabnaSaldiriyorBilgiPaneli.SetActive(false);
            }
        }
       
    }

    public void Saldir()
    {
        if (hedefgemi != null)
        {
            if (hedefgemi.CompareTag("SistemGemisi") && BenimGemim.GetComponent<Player>().seviye >= 5 && BenimGemim.GetComponent<Player>().harita <= 2)
            {
                BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 375]);
            }
            else
            {
                BenimGemim.GetComponent<Player>().Saldir(true);
            }
        }
    }
    public void SaldiriDurdur()
    {
        SaldirButton.SetActive(true);
        SaldiriIptalButton.SetActive(false);
        WindowsSaldirButton.SetActive(true);
        WindowsSaldirIptalButton.SetActive(false);
        BenimGemim.GetComponent<Player>().Saldir(false);
        // MyShip.GetComponent<Player>().Saldir(false);
        // MyShip.GetComponent<Player>().RoketSaldir(false);
        // roketAtisIzin = false;
    }
    public void OpenWebsite()
    {
        Application.OpenURL("https://lostpiratesonline.com/");
    }

    private void BaskinHaritasiNpcDogur()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            for (int i = 0; i < 30; i++)
            {
                BaskinHaritasiNpcDogurSunucu("baskinharitasi-" + i, 98);
                BaskinHaritasiNpcDogurSunucu("baskinharitasi-" + i, 97);
                
            }
        }
#endif
    }

    private void NpcDogur()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
                for (int i = 0; i < 30; i++)
                {
                    NpcDogurSunucu("npcgemi1-" + i, 1);
                    NpcDogurSunucu("npcgemi1-" + i, 1);
                    NpcDogurSunucu("npcgemi2-" + i, 2);
                    NpcDogurSunucu("npcgemi2-" + i, 2);
                    NpcDogurSunucu("npcgemi3-" + i, 3);
                    NpcDogurSunucu("npcgemi3-" + i, 3);
                    NpcDogurSunucu("npcgemi4-" + i, 4);
                    NpcDogurSunucu("npcgemi4-" + i, 4);
                    NpcDogurSunucu("npcgemi5-" + i, 5);
                    NpcDogurSunucu("npcgemi5-" + i, 5);
                    NpcDogurSunucu("npcgemi6-" + i, 6);
                    NpcDogurSunucu("npcgemi6-" + i, 6);
                    NpcDogurSunucu("npcgemi7-" + i, 7);
                    NpcDogurSunucu("npcgemi7-" + i, 7);
                    NpcDogurSunucu("npcgemi8-" + i, 8);
                    NpcDogurSunucu("npcgemi8-" + i, 8);
                    NpcDogurSunucu("npcgemi9-" + i, 9);
                    NpcDogurSunucu("npcgemi9-" + i, 9);
                }
                for (int i = 0; i < 10; i++)
                {
                    SandikUret("Sandik1-" + i, 1);
                    SandikUret("Sandik2-" + i, 2);
                    SandikUret("Sandik3-" + i, 3);
                    SandikUret("Sandik4-" + i, 4);
                    SandikUret("Sandik5-" + i, 5);
                    SandikUret("Sandik6-" + i, 6);
                    SandikUret("Sandik7-" + i, 7);
                    SandikUret("Sandik8-" + i, 8);
                    SandikUret("Sandik9-" + i, 9);
                }
                for (int i = 0; i < 7; i++)
                {
                    YaratikUret("npcyaratik1-" + i, 1);
                    YaratikUret("npcyaratik2-" + i, 2);
                    YaratikUret("npcyaratik3-" + i, 3);
                    YaratikUret("npcyaratik4-" + i, 4);
                    YaratikUret("npcyaratik5-" + i, 5);
                    YaratikUret("npcyaratik6-" + i, 6);
                    YaratikUret("npcyaratik7-" + i, 7);
                    YaratikUret("npcyaratik8-" + i, 8);
                    YaratikUret("npcyaratik9-" + i, 9);
                }
            }
           /* else
            {
                for (int i = 0; i < 10; i++)
                {
                    SandikUret("Sandik1-" + i, 1);
                    SandikUret("Sandik2-" + i, 2);
                    SandikUret("Sandik3-" + i, 3);
                    SandikUret("Sandik4-" + i, 4);
                    SandikUret("Sandik5-" + i, 5);
                    SandikUret("Sandik6-" + i, 6);
                    SandikUret("Sandik7-" + i, 7);
                    SandikUret("Sandik8-" + i, 8);
                    SandikUret("Sandik9-" + i, 9);
                }
                for (int i = 0; i < 2; i++)
                {
                    YaratikUret("npcyaratik1-" + i, 1);
                    YaratikUret("npcyaratik2-" + i, 2);
                    YaratikUret("npcyaratik3-" + i, 3);
                    YaratikUret("npcyaratik4-" + i, 4);
                    YaratikUret("npcyaratik5-" + i, 5);
                    YaratikUret("npcyaratik6-" + i, 6);
                    YaratikUret("npcyaratik7-" + i, 7);
                    YaratikUret("npcyaratik8-" + i, 8);
                    YaratikUret("npcyaratik9-" + i, 9);
                }*/
#endif
    }

    private void EtkinlikNpcDogur()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            for (int i = 0; i < 60; i++)
            {
                EtkinlikNpcDogurSunucu("Etkinliknpcgemi1-" + i, 1);
                EtkinlikNpcDogurSunucu("Etkinliknpcgemi2-" + i, 2);
                EtkinlikNpcDogurSunucu("Etkinliknpcgemi3-" + i, 3);
                EtkinlikNpcDogurSunucu("Etkinliknpcgemi4-" + i, 4);
                EtkinlikNpcDogurSunucu("Etkinliknpcgemi5-" + i, 5);
                EtkinlikNpcDogurSunucu("Etkinliknpcgemi6-" + i, 6);
                EtkinlikNpcDogurSunucu("Etkinliknpcgemi7-" + i, 7);
                EtkinlikNpcDogurSunucu("Etkinliknpcgemi8-" + i, 8);
                EtkinlikNpcDogurSunucu("Etkinliknpcgemi9-" + i, 9);
            }
            for (int i = 0; i < 15; i++)
            {
                YaratikUret("npcyaratik1-" + i, 1);
                YaratikUret("npcyaratik2-" + i, 2);
                YaratikUret("npcyaratik3-" + i, 3);
                YaratikUret("npcyaratik4-" + i, 4);
                YaratikUret("npcyaratik5-" + i, 5);
                YaratikUret("npcyaratik6-" + i, 6);
                YaratikUret("npcyaratik7-" + i, 7);
                YaratikUret("npcyaratik8-" + i, 8);
                YaratikUret("npcyaratik9-" + i, 9);
               //EtkinlikDenizYaratigiDogurSunucu("EtkinlikDenizYaratigi1-"+i,1);
               //EtkinlikDenizYaratigiDogurSunucu("EtkinlikDenizYaratigi2-"+i,2);
               //EtkinlikDenizYaratigiDogurSunucu("EtkinlikDenizYaratigi3-"+i,3);
               //EtkinlikDenizYaratigiDogurSunucu("EtkinlikDenizYaratigi4-"+i,4);
               //EtkinlikDenizYaratigiDogurSunucu("EtkinlikDenizYaratigi5-"+i,5);
               //EtkinlikDenizYaratigiDogurSunucu("EtkinlikDenizYaratigi6-"+i,6);
               //EtkinlikDenizYaratigiDogurSunucu("EtkinlikDenizYaratigi7-"+i,7);
               //EtkinlikDenizYaratigiDogurSunucu("EtkinlikDenizYaratigi8-"+i,8);
               //EtkinlikDenizYaratigiDogurSunucu("EtkinlikDenizYaratigi9-"+i,9);
            }
            for (int i = 0; i < 10; i++)
            {
                SandikUret("Sandik1-" + i, 1);
                SandikUret("Sandik2-" + i, 2);
                SandikUret("Sandik3-" + i, 3);
                SandikUret("Sandik4-" + i, 4);
                SandikUret("Sandik5-" + i, 5);
                SandikUret("Sandik6-" + i, 6);
                SandikUret("Sandik7-" + i, 7);
                SandikUret("Sandik8-" + i, 8);
                SandikUret("Sandik9-" + i, 9);
            }
        }
#endif
    }

    [ClientRpc]
    public void ClientRpcLostCoinIkiyeKatlama(bool LostCoinIkiyeKatlanmaAktiflikDurumu, float kalanZaman)
    {
        if (LostCoinIkiyeKatlanmaAktiflikDurumu == true)
        {
#if UNITY_ANDROID
            AndroidIkýyeKatlamaIcon.SetActive(true);
            AndroidIkýyeKatlamaKalanZamanTex.SetActive(true);
            AndroidIkýyeKatlamaKalanZamanTex.GetComponent<LostCoinKatlamaText>().kalanZaman = kalanZaman;

#endif
#if UNITY_STANDALONE_WIN
            WinIkýyeKatlamaIcon.SetActive(true);
            WinIkýyeKatlamaKalanZamanText.SetActive(true);
            WinIkýyeKatlamaKalanZamanText.GetComponent<LostCoinKatlamaText>().kalanZaman = kalanZaman;
#endif
        }
        else
        {
#if UNITY_ANDROID
            AndroidIkýyeKatlamaIcon.SetActive(false);
            AndroidIkýyeKatlamaKalanZamanTex.SetActive(false);
            AndroidIkýyeKatlamaKalanZamanTex.GetComponent<LostCoinKatlamaText>().kalanZaman = 0;

#endif
#if UNITY_STANDALONE_WIN
            WinIkýyeKatlamaIcon.SetActive(false);
            WinIkýyeKatlamaKalanZamanText.SetActive(false);
            WinIkýyeKatlamaKalanZamanText.GetComponent<LostCoinKatlamaText>().kalanZaman = 0;
#endif
        }
    }

#if UNITY_SERVER || UNITY_EDITOR
    public void IkiKatiLostCoinBaslatSunucu()
    {
        StartCoroutine(SunucuIkiKatiLostCoinSayacBaslat());
    }
    public IEnumerator SunucuIkiKatiLostCoinSayacBaslat()
    {
        ikiyeKatlamaAktiflikDurumu = true;
        ikiyeKatlamaBaslangicZamani = Time.time;
        ClientRpcLostCoinIkiyeKatlama(ikiyeKatlamaAktiflikDurumu,10800f);
        yield return new WaitForSecondsRealtime(10800f);
        ikiyeKatlamaAktiflikDurumu = false;
        ClientRpcLostCoinIkiyeKatlama(ikiyeKatlamaAktiflikDurumu,0);
    }

    public void KuleDikYedek(int kuleSeviyesi, int kuleYeri, int dogacakHarita, int filoId, string filoKisaltmasi)
    {
        StartCoroutine(SunucuKuleDikmeSayacBaslat(kuleSeviyesi, kuleYeri, dogacakHarita, filoId, filoKisaltmasi));
    }
    public IEnumerator SunucuKuleDikmeSayacBaslat(int kuleSeviyesi, int kuleYeri, int dogacakHarita, int filoId, string filoKisaltmasi)
    {
        yield return new WaitForSecondsRealtime(1200f);
        if (HaritadakiFiloAdalari[dogacakHarita - 5].transform.Find("AdaOfisiCarpismaNoktasi").GetComponent<FiloAdasiOfisi>().AdaSahibiFiloId == filoId)
        {
            GameObject kule = Instantiate(Kuleler[kuleSeviyesi - 1], HaritadakiFiloAdalari[dogacakHarita - 5].transform.Find("Kule" + kuleYeri).transform);
            kule.GetComponent<KuleKontrol>().geminame = "Tower " + kuleYeri;
            kule.GetComponent<KuleKontrol>().filoKisaltmasi = filoKisaltmasi;
            kule.name = "Tower " + kuleYeri + " (" + dogacakHarita + ")";
            kule.GetComponent<KuleKontrol>().HaritaKac = dogacakHarita;
            kule.GetComponent<KuleKontrol>().hangiKule = kuleYeri;
            NetworkServer.Spawn(kule);
            using (var command = sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Adalar SET Kule" + kuleYeri + " = " + kuleSeviyesi + " WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                command.Parameters.AddWithValue("@oyuncuFiloId", filoId);
                command.Parameters.AddWithValue("@harita", dogacakHarita);
                command.ExecuteNonQuery();
            }
        }
    }

#endif

#if UNITY_SERVER || UNITY_EDITOR
            public void SunucuKuleYukseltmeYedek(int kuleSeviyesi, int kuleYeri, int dogacakHarita, int filoId, string filoKisaltmasi)
    {
        StartCoroutine(SunucuKuleYukseltmeSayacBaslat(kuleSeviyesi, kuleYeri, dogacakHarita, filoId, filoKisaltmasi));
    }
    public IEnumerator SunucuKuleYukseltmeSayacBaslat(int kuleSeviyesi, int kuleYeri, int dogacakHarita, int filoId, string filoKisaltmasi)
    {
        //eski kuleyi yýkar
        NetworkServer.Destroy(HaritadakiFiloAdalari[dogacakHarita - 5].transform.Find("Kule" + kuleYeri).gameObject.transform.GetChild(0).gameObject);
        yield return new WaitForSecondsRealtime(1200f);
        // yeni kuleyi dikme iþlemlerini baþlatýr
        if (HaritadakiFiloAdalari[dogacakHarita - 5].transform.Find("AdaOfisiCarpismaNoktasi").GetComponent<FiloAdasiOfisi>().AdaSahibiFiloId == filoId)
        {
            GameObject kule = Instantiate(Kuleler[kuleSeviyesi - 1], HaritadakiFiloAdalari[dogacakHarita - 5].transform.Find("Kule" + kuleYeri).transform);
            kule.GetComponent<KuleKontrol>().geminame = "Tower " + kuleYeri;
            kule.GetComponent<KuleKontrol>().filoKisaltmasi = filoKisaltmasi;
            kule.name = "Tower " + kuleYeri + " (" + dogacakHarita + ")";
            kule.GetComponent<KuleKontrol>().HaritaKac = dogacakHarita;
            kule.GetComponent<KuleKontrol>().hangiKule = kuleYeri;
            NetworkServer.Spawn(kule);
            using (var command = sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Adalar SET Kule" + kuleYeri + " = " + kuleSeviyesi + " WHERE AdaSahibiFiloId = @oyuncuFiloId and AdaId = @harita;";
                command.Parameters.AddWithValue("@oyuncuFiloId", filoId);
                command.Parameters.AddWithValue("@harita", dogacakHarita);
                command.ExecuteNonQuery();
            }
        }
    }
#endif

#if UNITY_SERVER || UNITY_EDITOR
    public void BossDogurSunucu(int dogacakHarita)
    {
        if (dogacakHarita == 3)
        {
            //Harita 3
            GameObject gemi3 = Instantiate(AdmiralBoss);
            gemi3.GetComponent<EtkinlikBossKontrol>().geminame = "Bachelor’s Delight";
            gemi3.name = "Bachelor’s Delight " + dogacakHarita;
            gemi3.GetComponent<NavMeshAgent>().Warp(NavMeshPos(dogacakHarita));
            gemi3.GetComponent<EtkinlikBossKontrol>().HaritaKac = dogacakHarita;
            gemi3.GetComponent<EtkinlikBossKontrol>().MaxCan = 100000;
            gemi3.GetComponent<EtkinlikBossKontrol>().Can = 100000;
            gemi3.GetComponent<EtkinlikBossKontrol>().hasar = 50;
            NetworkServer.Spawn(gemi3);
            AdmiralMesaji(dogacakHarita);
        }
        else if (dogacakHarita == 4)
        {
            //Harita 4
            GameObject gemi4 = Instantiate(AdmiralBoss);
            gemi4.GetComponent<EtkinlikBossKontrol>().geminame = "Bachelor’s Delight";
            gemi4.name = "Bachelor’s Delight " + dogacakHarita;
            gemi4.GetComponent<NavMeshAgent>().Warp(NavMeshPos(dogacakHarita));
            gemi4.GetComponent<EtkinlikBossKontrol>().HaritaKac = dogacakHarita;
            gemi4.GetComponent<EtkinlikBossKontrol>().MaxCan = 200000;
            gemi4.GetComponent<EtkinlikBossKontrol>().Can = 200000;
            gemi4.GetComponent<EtkinlikBossKontrol>().hasar = 100;
            NetworkServer.Spawn(gemi4);
            AdmiralMesaji(dogacakHarita);
        }
        else if (dogacakHarita == 5)
        {
            //Harita 5
            GameObject gemi5 = Instantiate(AdmiralBoss);
            gemi5.GetComponent<EtkinlikBossKontrol>().geminame = "Bachelor’s Delight";
            gemi5.name = "Bachelor’s Delight " + dogacakHarita;
            gemi5.GetComponent<NavMeshAgent>().Warp(NavMeshPos(dogacakHarita));
            gemi5.GetComponent<EtkinlikBossKontrol>().HaritaKac = dogacakHarita;
            gemi5.GetComponent<EtkinlikBossKontrol>().MaxCan = 400000;
            gemi5.GetComponent<EtkinlikBossKontrol>().Can = 400000;
            gemi5.GetComponent<EtkinlikBossKontrol>().hasar = 200;
            NetworkServer.Spawn(gemi5);
            AdmiralMesaji(dogacakHarita);
        }
        else if (dogacakHarita == 6)
        {
            //Harita 6
            GameObject gemi6 = Instantiate(AdmiralBoss);
            gemi6.GetComponent<EtkinlikBossKontrol>().geminame = "Bachelor’s Delight";
            gemi6.name = "Bachelor’s Delight " + dogacakHarita;
            gemi6.GetComponent<NavMeshAgent>().Warp(NavMeshPos(dogacakHarita));
            gemi6.GetComponent<EtkinlikBossKontrol>().HaritaKac = dogacakHarita;
            gemi6.GetComponent<EtkinlikBossKontrol>().MaxCan = 800000;
            gemi6.GetComponent<EtkinlikBossKontrol>().Can = 800000;
            gemi6.GetComponent<EtkinlikBossKontrol>().hasar = 400;
            NetworkServer.Spawn(gemi6);
            AdmiralMesaji(dogacakHarita);
        }
        else if (dogacakHarita == 7)
        {
            //Harita 7
            GameObject gemi7 = Instantiate(AdmiralBoss);
            gemi7.GetComponent<EtkinlikBossKontrol>().geminame = "Bachelor’s Delight";
            gemi7.name = "Bachelor’s Delight " + dogacakHarita;
            gemi7.GetComponent<NavMeshAgent>().Warp(NavMeshPos(dogacakHarita));
            gemi7.GetComponent<EtkinlikBossKontrol>().HaritaKac = dogacakHarita;
            gemi7.GetComponent<EtkinlikBossKontrol>().MaxCan = 1200000;
            gemi7.GetComponent<EtkinlikBossKontrol>().Can = 1200000;
            gemi7.GetComponent<EtkinlikBossKontrol>().hasar = 600;
            NetworkServer.Spawn(gemi7);
            AdmiralMesaji(dogacakHarita);
        }
        else if (dogacakHarita == 8)
        {
            //Harita 8
            GameObject gemi8 = Instantiate(AdmiralBoss);
            gemi8.GetComponent<EtkinlikBossKontrol>().geminame = "Bachelor’s Delight";
            gemi8.name = "Bachelor’s Delight " + dogacakHarita;
            gemi8.GetComponent<NavMeshAgent>().Warp(NavMeshPos(dogacakHarita));
            gemi8.GetComponent<EtkinlikBossKontrol>().HaritaKac = dogacakHarita;
            gemi8.GetComponent<EtkinlikBossKontrol>().MaxCan = 1600000;
            gemi8.GetComponent<EtkinlikBossKontrol>().Can = 1600000;
            gemi8.GetComponent<EtkinlikBossKontrol>().hasar = 800;
            NetworkServer.Spawn(gemi8);
            AdmiralMesaji(dogacakHarita);
        }
        else if (dogacakHarita == 9)
        {
            //Harita 9
            GameObject gemi9 = Instantiate(AdmiralBoss);
            gemi9.GetComponent<EtkinlikBossKontrol>().geminame = "Baskin Haritasi Admiral";
            gemi9.name = "Bachelor’s Delight " + dogacakHarita;
            gemi9.GetComponent<NavMeshAgent>().Warp(NavMeshPos(dogacakHarita));
            gemi9.GetComponent<EtkinlikBossKontrol>().HaritaKac = dogacakHarita;
            gemi9.GetComponent<EtkinlikBossKontrol>().MaxCan = 2000000;
            gemi9.GetComponent<EtkinlikBossKontrol>().Can = 2000000;
            gemi9.GetComponent<EtkinlikBossKontrol>().hasar = 1000;
            NetworkServer.Spawn(gemi9);
            AdmiralMesaji(dogacakHarita);
        }
        else if (dogacakHarita == 98)
        {
            //Harita 98
            GameObject gemi98 = Instantiate(BaskinHaritasiAdmiralGemi);
            gemi98.GetComponent<EtkinlikBossKontrol>().geminame = "PVE Admiral";
            gemi98.name = "PVE Admiral " + dogacakHarita;
            gemi98.GetComponent<NavMeshAgent>().Warp(NavMeshPos(dogacakHarita));
            gemi98.GetComponent<EtkinlikBossKontrol>().HaritaKac = dogacakHarita;
            gemi98.GetComponent<EtkinlikBossKontrol>().MaxCan = 3000000;
            gemi98.GetComponent<EtkinlikBossKontrol>().Can = 3000000;
            gemi98.GetComponent<EtkinlikBossKontrol>().hasar = 1000;

            gemi98.GetComponent<EtkinlikBossKontrol>().odulElitPuanOrani = 4;

            NetworkServer.Spawn(gemi98);
            AdmiralMesaji(dogacakHarita);

        }
        else if (dogacakHarita == 97)
        {
            //Harita 97
            GameObject gemi97 = Instantiate(BaskinHaritasiAdmiralGemi);
            gemi97.GetComponent<EtkinlikBossKontrol>().geminame = "PVP Admiral";
            gemi97.name = "PVP Admiral " + dogacakHarita;
            gemi97.GetComponent<NavMeshAgent>().Warp(NavMeshPos(dogacakHarita));
            gemi97.GetComponent<EtkinlikBossKontrol>().HaritaKac = dogacakHarita;
            gemi97.GetComponent<EtkinlikBossKontrol>().MaxCan = 3000000;
            gemi97.GetComponent<EtkinlikBossKontrol>().Can = 3000000;
            gemi97.GetComponent<EtkinlikBossKontrol>().hasar = 1000;

            gemi97.GetComponent<EtkinlikBossKontrol>().odulElitPuanOrani = 4;

            NetworkServer.Spawn(gemi97);
            AdmiralMesaji(dogacakHarita);
        }

       
    }
#endif


    private void EtkinlikKiyametNpcDogurEzik()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            if (KiyametEtkinligiNpcSayýsý < 100)
            {
                int harita = UnityEngine.Random.Range(1, 8);
                GameObject gemi = Instantiate(EtkinlikKiyametSistemGemisiEzik);
                gemi.GetComponent<SistemOyuncuKontrol>().geminame = "Anne Bonny";
                gemi.name = "Anne Bonny " + harita;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemOyuncuKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
                KiyametEtkinligiNpcSayýsý++;
            }
        }
#endif
    }

    private void EtkinlikKiyametNpcDogurOrta()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            int harita = UnityEngine.Random.Range(2, 8);
            GameObject gemi = Instantiate(EtkinlikKiyametSistemGemisiOrta);
            gemi.GetComponent<SistemOyuncuKontrol>().geminame = "Calico Jack";
            gemi.name = "Calico Jack " + harita;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<SistemOyuncuKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
#endif
    }

    private void EtkinlikKiyametNpcDogurGuclu()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly || isServer)
        {
            int harita = UnityEngine.Random.Range(3, 8);
            GameObject gemi = Instantiate(EtkinlikKiyametSistemGemisiGuclu);
            gemi.GetComponent<SistemOyuncuKontrol>().geminame = "Edward Teach";
            gemi.name = "Edward Teach " + harita;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<SistemOyuncuKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
#endif
    }

#if UNITY_SERVER || UNITY_EDITOR
    // Gemi Doðurma
    public void NpcDogurSunucu(string name, int harita)
    {
        if (harita == 1)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                GameObject gemi = Instantiate(HaritaBirSistemGemi[0]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Fisherman";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
            else
            {
                GameObject gemi = Instantiate(HaritaBirSistemGemi[1]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Merchant";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
        }
        else if (harita == 2)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                GameObject gemi = Instantiate(HaritaIkistemGemi[0]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Fisherman II";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
            else
            {
                GameObject gemi = Instantiate(HaritaIkistemGemi[1]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "BigSalBoath";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
        }
        else if (harita == 3)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                GameObject gemi = Instantiate(HaritaUcSistemGemi[0]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "BigSalBoath II";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
            else
            {
                GameObject gemi = Instantiate(HaritaUcSistemGemi[1]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "White Flag";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
        }
        else if (harita == 4)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                GameObject gemi = Instantiate(HaritaDortSistemGemi[0]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "White Flag II";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
            else
            {
                GameObject gemi = Instantiate(HaritaDortSistemGemi[1]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Oasberg";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
        }
        else if (harita == 5)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                GameObject gemi = Instantiate(HaritaBesSistemGemi[0]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Oasberg II";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
            else
            {
                GameObject gemi = Instantiate(HaritaBesSistemGemi[1]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Viking";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
        }
        else if (harita == 6)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                GameObject gemi = Instantiate(HaritaAltiSistemGemi[0]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Viking II";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
            else
            {
                GameObject gemi = Instantiate(HaritaAltiSistemGemi[1]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Brutal";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
        }
        else if (harita == 7)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                GameObject gemi = Instantiate(HaritaYediSistemGemi[0]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Brutal II";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
            else
            {
                GameObject gemi = Instantiate(HaritaYediSistemGemi[1]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Black Pearl";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
        }
        else if (harita == 8)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {

                GameObject gemi = Instantiate(HaritaSekizSistemGemi[0]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Black Pearl II";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
            else
            {
                GameObject gemi = Instantiate(HaritaSekizSistemGemi[1]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "White Pearl";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
        }
        else if (harita == 9)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                GameObject gemi = Instantiate(HaritaDokuzSistemGemi[0]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "White Pearl II";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
            else
            {
                GameObject gemi = Instantiate(HaritaDokuzSistemGemi[1]);
                gemi.GetComponent<SistemGemisiKontrol>().geminame = "Blue Crystal";
                gemi.name = name;
                gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
                gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
                NetworkServer.Spawn(gemi);
            }
        }
    }

    public void EtkinlikNpcDogurSunucu(string name, int harita)
    {
        if (harita == 1)
        {
            GameObject gemi = Instantiate(EtkinlikHaritaBirSistemGemi);
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame = "Father Christmas";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 2)
        {
            GameObject gemi = Instantiate(EtkinlikHaritaIkistemGemi);
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame = "Father Christmas";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 3)
        {
            GameObject gemi = Instantiate(EtkinlikHaritaUcSistemGemi);
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame = "Father Christmas";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 4)
        {
            GameObject gemi = Instantiate(EtkinlikHaritaDortSistemGemi);
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame = "Father Christmas";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 5)
        {
            GameObject gemi = Instantiate(EtkinlikHaritaBesSistemGemi);
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame = "Father Christmas";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 6)
        {
            GameObject gemi = Instantiate(EtkinlikHaritaAltiSistemGemi);
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame = "Father Christmas";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 7)
        {
            GameObject gemi = Instantiate(EtkinlikHaritaYediSistemGemi);
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame = "Father Christmas";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 8)
        {
            GameObject gemi = Instantiate(EtkinlikHaritaSekizSistemGemi);
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame = "Father Christmas";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 9)
        {
            GameObject gemi = Instantiate(EtkinlikHaritaDokuzSistemGemi);
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().geminame = "Father Christmas";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikSistemGemileriKontrol>().HaritaKac = harita;
            NetworkServer.Spawn(gemi);
        }
    }

    public void EtkinlikDenizYaratigiDogurSunucu(string name, int harita)
    {
        GameObject gemi = Instantiate(EtkinlikDenizYaratiklari[harita - 1]);
        gemi.GetComponent<SistemYaratikKontrol>().geminame = "Snowman";
        gemi.name = name;
        gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
        gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 1;
        NetworkServer.Spawn(gemi);
    }


    public void EtkinlikAdmiralDogurSunucu(string bossAdi, string name, int harita)
    {
        if (harita == 1)
        {
            GameObject gemi = Instantiate(EtkinlikAdmiral);
            gemi.GetComponent<EtkinlikBossKontrol>().geminame = bossAdi;
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikBossKontrol>().HaritaKac = harita;
            gemi.GetComponent<EtkinlikBossKontrol>().MaxCan = 100000;
            gemi.GetComponent<EtkinlikBossKontrol>().Can = 100000;
            gemi.GetComponent<EtkinlikBossKontrol>().saldirihizi = 6;
            gemi.GetComponent<EtkinlikBossKontrol>().hasar = 100;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 2)
        {
            GameObject gemi = Instantiate(EtkinlikAdmiral);
            gemi.GetComponent<EtkinlikBossKontrol>().geminame = bossAdi;
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikBossKontrol>().HaritaKac = harita;
            gemi.GetComponent<EtkinlikBossKontrol>().MaxCan = 300000;
            gemi.GetComponent<EtkinlikBossKontrol>().Can = 300000;
            gemi.GetComponent<EtkinlikBossKontrol>().saldirihizi = 6;
            gemi.GetComponent<EtkinlikBossKontrol>().hasar = 200;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 3)
        {
            GameObject gemi = Instantiate(EtkinlikAdmiral);
            gemi.GetComponent<EtkinlikBossKontrol>().geminame = bossAdi;
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikBossKontrol>().HaritaKac = harita;
            gemi.GetComponent<EtkinlikBossKontrol>().MaxCan = 700000;
            gemi.GetComponent<EtkinlikBossKontrol>().Can = 700000;
            gemi.GetComponent<EtkinlikBossKontrol>().saldirihizi = 6;
            gemi.GetComponent<EtkinlikBossKontrol>().hasar = 250;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 4)
        {
            GameObject gemi = Instantiate(EtkinlikAdmiral);
            gemi.GetComponent<EtkinlikBossKontrol>().geminame = bossAdi;
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikBossKontrol>().HaritaKac = harita;
            gemi.GetComponent<EtkinlikBossKontrol>().MaxCan = 1100000;
            gemi.GetComponent<EtkinlikBossKontrol>().Can = 1100000;
            gemi.GetComponent<EtkinlikBossKontrol>().saldirihizi = 6;
            gemi.GetComponent<EtkinlikBossKontrol>().hasar = 300;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 5)
        {
            GameObject gemi = Instantiate(EtkinlikAdmiral);
            gemi.GetComponent<EtkinlikBossKontrol>().geminame = bossAdi;
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikBossKontrol>().HaritaKac = harita;
            gemi.GetComponent<EtkinlikBossKontrol>().MaxCan = 1800000;
            gemi.GetComponent<EtkinlikBossKontrol>().Can = 1800000;
            gemi.GetComponent<EtkinlikBossKontrol>().saldirihizi = 6;
            gemi.GetComponent<EtkinlikBossKontrol>().hasar = 350;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 6)
        {
            GameObject gemi = Instantiate(EtkinlikAdmiral);
            gemi.GetComponent<EtkinlikBossKontrol>().geminame = bossAdi;
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikBossKontrol>().HaritaKac = harita;
            gemi.GetComponent<EtkinlikBossKontrol>().MaxCan = 2100000;
            gemi.GetComponent<EtkinlikBossKontrol>().Can = 2100000;
            gemi.GetComponent<EtkinlikBossKontrol>().saldirihizi = 6;
            gemi.GetComponent<EtkinlikBossKontrol>().hasar = 400;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 7)
        {
            GameObject gemi = Instantiate(EtkinlikAdmiral);
            gemi.GetComponent<EtkinlikBossKontrol>().geminame = bossAdi;
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikBossKontrol>().HaritaKac = harita;
            gemi.GetComponent<EtkinlikBossKontrol>().MaxCan = 2500000;
            gemi.GetComponent<EtkinlikBossKontrol>().Can = 2500000;
            gemi.GetComponent<EtkinlikBossKontrol>().saldirihizi = 6;
            gemi.GetComponent<EtkinlikBossKontrol>().hasar = 450;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 8)
        {
            GameObject gemi = Instantiate(EtkinlikAdmiral);
            gemi.GetComponent<EtkinlikBossKontrol>().geminame = bossAdi;
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikBossKontrol>().HaritaKac = harita;
            gemi.GetComponent<EtkinlikBossKontrol>().MaxCan = 3000000;
            gemi.GetComponent<EtkinlikBossKontrol>().Can = 3000000;
            gemi.GetComponent<EtkinlikBossKontrol>().saldirihizi = 6;
            gemi.GetComponent<EtkinlikBossKontrol>().hasar = 500;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 9)
        {
            GameObject gemi = Instantiate(EtkinlikAdmiral);
            gemi.GetComponent<EtkinlikBossKontrol>().geminame = bossAdi;
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<EtkinlikBossKontrol>().HaritaKac = harita;
            gemi.GetComponent<EtkinlikBossKontrol>().MaxCan = 3500000;
            gemi.GetComponent<EtkinlikBossKontrol>().Can = 3500000;
            gemi.GetComponent<EtkinlikBossKontrol>().saldirihizi = 6;
            gemi.GetComponent<EtkinlikBossKontrol>().hasar = 550;
            NetworkServer.Spawn(gemi);
        }
        EtkinlikBossMesaji(harita, "Agnora");
    }

    public void BaskinHaritasiNpcDogurSunucu(string name, int harita)
    {
        if (harita == 98)
        {
            GameObject gemi = Instantiate(BaskinHaritasiSistemGemi);
            gemi.GetComponent<SistemGemisiKontrol>().geminame = "BaskinNpc";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
            gemi.GetComponent<SistemGemisiKontrol>().odulElitPuanOrani = 4;
            gemi.GetComponent<SistemGemisiKontrol>().MaxCan = 10000;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 97)
        {
            GameObject gemi = Instantiate(BaskinHaritasiSistemGemi);
            gemi.GetComponent<SistemGemisiKontrol>().geminame = "BaskinNpc";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPos(harita));
            gemi.GetComponent<SistemGemisiKontrol>().HaritaKac = harita;
            gemi.GetComponent<SistemGemisiKontrol>().odulElitPuanOrani = 5;
            NetworkServer.Spawn(gemi);
        }
    }

    public Vector2 NavMeshPosDogurma(int harita)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(KonumOlusturucuDogurma(harita), out hit, Mathf.Infinity, NavMesh.AllAreas);
        return hit.position;
    }

    private Vector2 KonumOlusturucuDogurma(int harita)
    {
        bool konumOlusturuldu = false;
        Vector2 olusturulanKonum = new Vector2();
        if (harita == 1)
        {
            olusturulanKonum = new Vector2(UnityEngine.Random.Range(-108, -2), UnityEngine.Random.Range(63, 170));
            return olusturulanKonum;
        }
        else if (harita == 2)
        {
            olusturulanKonum = new Vector2(UnityEngine.Random.Range(52, 158), UnityEngine.Random.Range(63, 170));
            return olusturulanKonum;
        }
        else if (harita == 3)
        {
            olusturulanKonum = new Vector2(UnityEngine.Random.Range(212, 318), UnityEngine.Random.Range(63, 170));
            return olusturulanKonum;
        }
        else if (harita == 4)
        {
            olusturulanKonum = new Vector2(UnityEngine.Random.Range(371, 478), UnityEngine.Random.Range(63, 170));
            return olusturulanKonum;
        }
        else if (harita == 5)
        {
            while (konumOlusturuldu == false)
            {
                olusturulanKonum = new Vector2(UnityEngine.Random.Range(-108, -2), UnityEngine.Random.Range(223, 329));
                if ((olusturulanKonum.x > -55 && olusturulanKonum.x < -20 && olusturulanKonum.y > 294 && olusturulanKonum.y < 321) == false)
                {
                    konumOlusturuldu = true;
                }
            }
            return olusturulanKonum;
        }
        else if (harita == 6)
        {
            while (konumOlusturuldu == false)
            {
                olusturulanKonum = new Vector2(UnityEngine.Random.Range(52, 158), UnityEngine.Random.Range(223, 329));
                if ((olusturulanKonum.x > 111 && olusturulanKonum.x < 145 && olusturulanKonum.y > 297 && olusturulanKonum.y < 320) == false)
                {
                    konumOlusturuldu = true;
                }
            }
            return olusturulanKonum;
        }
        else if (harita == 7)
        {
            while (konumOlusturuldu == false)
            {
                olusturulanKonum = new Vector2(UnityEngine.Random.Range(212, 318), UnityEngine.Random.Range(223, 329));
                if ((olusturulanKonum.x > 227 && olusturulanKonum.x < 261 && olusturulanKonum.y > 250 && olusturulanKonum.y < 274) == false)
                {
                    konumOlusturuldu = true;
                }
            }
            return olusturulanKonum;
        }
        else if (harita == 8)
        {
            while (konumOlusturuldu == false)
            {
                olusturulanKonum = new Vector2(UnityEngine.Random.Range(371, 478), UnityEngine.Random.Range(223, 329));
                if ((olusturulanKonum.x > 385 && olusturulanKonum.x < 415 && olusturulanKonum.y > 281 && olusturulanKonum.y < 306) == false)
                {
                    konumOlusturuldu = true;
                }
            }
            return olusturulanKonum;
        }
        else if (harita == 9)
        {
            while (konumOlusturuldu == false)
            {
                olusturulanKonum = new Vector2(UnityEngine.Random.Range(-108, -2), UnityEngine.Random.Range(382, 490));
                  if ((olusturulanKonum.x > -90 && olusturulanKonum.x < -58 && olusturulanKonum.y > 444 && olusturulanKonum.y < 469) == false)
                  {
                      konumOlusturuldu = true;
                  }
            }
            return olusturulanKonum;
        }
        else
        {
            return new Vector2(0, 0);
        }
    }
    public Vector2 NavMeshPos(int harita)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(KonumOlusturucu(harita), out hit, Mathf.Infinity, NavMesh.AllAreas);
        return hit.position;
    }
    private Vector2 KonumOlusturucu(int harita)
    {
        if (harita == 1)
        {
            return new Vector2(UnityEngine.Random.Range(-108, -2), UnityEngine.Random.Range(63, 170));
        }
        else if (harita == 2)
        {
            return new Vector2(UnityEngine.Random.Range(52, 158), UnityEngine.Random.Range(63, 170));
        }
        else if (harita == 3)
        {
            return new Vector2(UnityEngine.Random.Range(212, 318), UnityEngine.Random.Range(63, 170));
        }
        else if (harita == 4)
        {
            return new Vector2(UnityEngine.Random.Range(371, 478), UnityEngine.Random.Range(63, 170));
        }
        else if (harita == 5)
        {
            return new Vector2(UnityEngine.Random.Range(-108, -2), UnityEngine.Random.Range(223, 329));
        }
        else if (harita == 6)
        {
            return new Vector2(UnityEngine.Random.Range(52, 158), UnityEngine.Random.Range(223, 329));
        }
        else if (harita == 7)
        {
            return new Vector2(UnityEngine.Random.Range(212, 318), UnityEngine.Random.Range(223, 329));
        }
        else if (harita == 8)
        {
            return new Vector2(UnityEngine.Random.Range(371, 478), UnityEngine.Random.Range(223, 329));
        }
        else if (harita == 9)
        {
            return new Vector2(UnityEngine.Random.Range(-108, -2), UnityEngine.Random.Range(382, 490));
        }
        else if (harita == 98)
        {
            return new Vector2(UnityEngine.Random.Range(531, 638), UnityEngine.Random.Range(63, 170));
        }
        else if (harita == 97)
        {
            return new Vector2(UnityEngine.Random.Range(531, 638), UnityEngine.Random.Range(223, 329));
        }
        else
        {
            return new Vector2(0, 0);
        }
    }

    // Deniz Yaratýðý Doðurma
    public void YaratikUret(string name, int harita)
    {
        if (harita == 1)
        {
            GameObject gemi = Instantiate(HaritaBirYaratik);
            gemi.GetComponent<SistemYaratikKontrol>().geminame = "Piscine";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 1;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 2)
        {
            GameObject gemi = Instantiate(HaritaIkiYaratik);
            gemi.GetComponent<SistemYaratikKontrol>().geminame = "Big Piscine ";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 2;
            NetworkServer.Spawn(gemi);
        }
        if (harita == 3)
        {
            GameObject gemi = Instantiate(HaritaUcYaratik);
            gemi.GetComponent<SistemYaratikKontrol>().geminame = "Shark";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 3;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 4)
        {
            GameObject gemi = Instantiate(HaritaDortYaratik);
            gemi.GetComponent<SistemYaratikKontrol>().geminame = "Big Shark";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 4;
            NetworkServer.Spawn(gemi);
        }
        if (harita == 5)
        {
            GameObject gemi = Instantiate(HaritaBesYaratik);
            gemi.GetComponent<SistemYaratikKontrol>().geminame = "Tentakel";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 5;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 6)
        {
            GameObject gemi = Instantiate(HaritaAltiYaratik);
            gemi.GetComponent<SistemYaratikKontrol>().geminame = "Tentakel 2";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 6;
            NetworkServer.Spawn(gemi);
        }
        if (harita == 7)
        {
            GameObject gemi = Instantiate(HaritaYediYaratik);
            gemi.GetComponent<SistemYaratikKontrol>().geminame = "Kraken";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 7;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 8)
        {
            GameObject gemi = Instantiate(HaritaSekizYaratik);
            gemi.GetComponent<SistemYaratikKontrol>().geminame = "Kraken 2";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 8;
            NetworkServer.Spawn(gemi);
        }
        else if (harita == 9)
        {
            GameObject gemi = Instantiate(HaritaDokuzYaratik);
            gemi.GetComponent<SistemYaratikKontrol>().geminame = "Octopus";
            gemi.name = name;
            gemi.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            gemi.GetComponent<SistemYaratikKontrol>().HaritaKac = 9;
            NetworkServer.Spawn(gemi);
        }
    }
    public void SandikUret(string name, int harita)
    {
        if (harita == 1)
        {
            GameObject sandik = Instantiate(HaritaBirSandýk);
            sandik.name = name;
            sandik.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            sandik.GetComponent<SandikKontrol>().HaritaKac = 1;
            NetworkServer.Spawn(sandik);

        }
        else if (harita == 2)
        {
            GameObject sandik = Instantiate(HaritaBirSandýk);
            sandik.name = name;
            sandik.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            sandik.GetComponent<SandikKontrol>().HaritaKac = 2;
            NetworkServer.Spawn(sandik);

        }
        else if (harita == 3)
        {
            GameObject sandik = Instantiate(HaritaBirSandýk);
            sandik.name = name;
            sandik.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            sandik.GetComponent<SandikKontrol>().HaritaKac = 3;
            NetworkServer.Spawn(sandik);

        }
        else if (harita == 4)
        {
            GameObject sandik = Instantiate(HaritaBirSandýk);
            sandik.name = name;
            sandik.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            sandik.GetComponent<SandikKontrol>().HaritaKac = 4;
            NetworkServer.Spawn(sandik);

        }
        else if (harita == 5)
        {
            GameObject sandik = Instantiate(HaritaBirSandýk);
            sandik.name = name;
            sandik.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            sandik.GetComponent<SandikKontrol>().HaritaKac = 5;
            NetworkServer.Spawn(sandik);

        }
        else if (harita == 6)
        {
            GameObject sandik = Instantiate(HaritaBirSandýk);
            sandik.name = name;
            sandik.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            sandik.GetComponent<SandikKontrol>().HaritaKac = 6;
            NetworkServer.Spawn(sandik);

        }
        else if (harita == 7)
        {
            GameObject sandik = Instantiate(HaritaBirSandýk);
            sandik.name = name;
            sandik.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            sandik.GetComponent<SandikKontrol>().HaritaKac = 7;
            NetworkServer.Spawn(sandik);
        }
        else if (harita == 8)
        {
            GameObject sandik = Instantiate(HaritaBirSandýk);
            sandik.name = name;
            sandik.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            sandik.GetComponent<SandikKontrol>().HaritaKac = 8;
            NetworkServer.Spawn(sandik);

        }
        else if (harita == 9)
        {
            GameObject sandik = Instantiate(HaritaBirSandýk);
            sandik.name = name;
            sandik.GetComponent<NavMeshAgent>().Warp(NavMeshPosDogurma(harita));
            sandik.GetComponent<SandikKontrol>().HaritaKac = 9;
            NetworkServer.Spawn(sandik);

        }
    }
#endif

    [ClientRpc]
    public void oyuncularadonensohbetveri(string message, string kullaniciadi,string oyuncufilokislaltma, int oyuncuýd)
    {
#if UNITY_STANDALONE_WIN
        if (kullaniciadi != "")
        {
            if (oyuncuýd == 16 || oyuncuýd == 17)
            {
                if (oyuncufilokislaltma != "")
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#ffff00>[" + oyuncufilokislaltma + "]</color>" + "<color=#ffff00>[" + kullaniciadi + "]</color>" + ": " + "<color=#00FF1D>" + message + "</color>");
                }
                else
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#ffff00>[" + kullaniciadi + "]</color>" + ": " + "<color=#00FF1D>" + message + "</color>");
                }
            }
            else
            {
                if (oyuncufilokislaltma != "")
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#ffff00>[" + oyuncufilokislaltma + "]</color>" + kullaniciadi + ": " + message);
                }
                else
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + kullaniciadi + ": " + message);
                }
            }
        }
        else
        {
            if (oyuncuýd == 16 || oyuncuýd == 17)
            {
                if (oyuncufilokislaltma != "")
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>[" + oyuncufilokislaltma + "]</color>" + "<color=#00FF1D>" + message + "</color>");
                }
                else
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>" + message + "</color>");
                }
            }
            else
            {
                if (oyuncufilokislaltma != "")
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>[" + oyuncufilokislaltma + "]</color>"  + message);
                }
                else
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + message);
                }
            }
        }
#endif
#if UNITY_ANDROID
        if (kullaniciadi.Length > 0)
        {
            if (oyuncuýd == 16 || oyuncuýd == 17)
            {
                if (oyuncufilokislaltma != "")
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#B44800>[" + oyuncufilokislaltma + "]</color>" + "<color=#ffff00>[" + kullaniciadi + "]</color>" + ": " + "<color=#00FF1D>" + message + "</color>");
                }
                else
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#ffff00>[" + kullaniciadi + "]</color>" + ": " + "<color=#00FF1D>" + message + "</color>");
                }
            }
            else
            {
                if (oyuncufilokislaltma.Length > 0)
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#B44800>[" + oyuncufilokislaltma + "]</color>" + kullaniciadi + ": " + message);
                }   
                else
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + kullaniciadi + ": " + message);
                }
            }
        }
        else
        {
            if (oyuncuýd == 16 || oyuncuýd == 17)
            {
                if (oyuncufilokislaltma.Length > 0)
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#B44800>[" + oyuncufilokislaltma + "]</color>" + "<color=#00FF1D>" + message + "</color>");
                }
                else
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>" + message + "</color>");
                }
            }
            else
            {
                if (oyuncufilokislaltma.Length > 0)
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>[" + oyuncufilokislaltma + "]</color>" + message);
                }
                else
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + message);
                }
            }
        }
#endif
    }

    [ClientRpc]
    public void Filodandonensohbetveri(string message, string kullaniciadi, string oyuncufilokislaltma, int oyuncuýd, int filoId)
    {
#if UNITY_STANDALONE_WIN

        if (filoId == BenimGemim.GetComponent<Player>().oyuncuFiloId)
        {
            Chat.GetComponent<Chat>().AddMessageToFiloChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#ffff00>[" + oyuncufilokislaltma + "]</color>" + kullaniciadi + ": " + message);
        }
#endif
#if UNITY_ANDROID
        if (kullaniciadi.Length > 0)
        {
            if (filoId == BenimGemim.GetComponent<Player>().oyuncuFiloId)
            {
                mobilChat.GetComponent<Chat>().AddMessageToFiloChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#B44800>[" + oyuncufilokislaltma + "]</color>" + kullaniciadi + ": " + message);
            }
        }
#endif
    }

    public void KameraOyuncuyaKilitlensin()
    {
#if UNITY_ANDROID
        Dumen.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
        WindowsDumen.SetActive(false);
#endif
        Kamera.GetComponent<DragCamera2D>().followTarget = BenimGemim;
    }

    public void GulleDegistir(int gulleId)
    {
        BenimGemim.GetComponent<Player>().seciligulleid = gulleId;
        CephaneSpriteDegistir(gulleId);
    }

    public void ZipkinDegistir(int zipkinId)
    {
        BenimGemim.GetComponent<Player>().seciliZipkinId = zipkinId;
        CephaneSpriteDegistir(5 + zipkinId);
    }
    public void CephaneSpriteDegistir(int donenSayi)
    {
        if (donenSayi == 0)
        {
            DegistirilecekGulle.sprite = GulleSprite[0];
            WindowsDegistirilecekGulle.sprite = GulleSprite[0];
            SeciliGulleText.text = "";
            WindowsSeciliGulleText.text = "";
        }
        else if (donenSayi == 1)
        {
            DegistirilecekGulle.sprite = GulleSprite[1];
            WindowsDegistirilecekGulle.sprite = GulleSprite[1];
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuDemirGulle);
            WindowsSeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuDemirGulle);
        }
        else if (donenSayi == 2)
        {
            DegistirilecekGulle.sprite = GulleSprite[2];
            WindowsDegistirilecekGulle.sprite = GulleSprite[2];
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuAlevGulle);
            WindowsSeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuAlevGulle);
        }
        else if (donenSayi == 3)
        {
            DegistirilecekGulle.sprite = GulleSprite[3];
            WindowsDegistirilecekGulle.sprite = GulleSprite[3];
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuSifaGulle);
            WindowsSeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuSifaGulle);
        }
        else if (donenSayi == 4)
        {
            DegistirilecekGulle.sprite = GulleSprite[4];
            WindowsDegistirilecekGulle.sprite = GulleSprite[4];
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuHavaiFisekGulle);
            WindowsSeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuHavaiFisekGulle);
        }
        else if (donenSayi == 5)
        {
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuPaslanmisZipkin);
            DegistirilecekGulle.sprite = zipkinSprite[0];
            WindowsDegistirilecekZipkin.sprite = zipkinSprite[0];
            WindowsSeciliZipkinText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuPaslanmisZipkin);
        }
        else if (donenSayi == 6)
        {
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuGumusZipkin);
            DegistirilecekGulle.sprite = zipkinSprite[1];
            WindowsDegistirilecekZipkin.sprite = zipkinSprite[1];
            WindowsSeciliZipkinText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuGumusZipkin);
        }
        else if (donenSayi == 7)
        {
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuAltinZipkin);
            DegistirilecekGulle.sprite = zipkinSprite[2];
            WindowsDegistirilecekZipkin.sprite = zipkinSprite[2];
            WindowsSeciliZipkinText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().oyuncuAltinZipkin);

        }
        else if (donenSayi == 8)
        {
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().OyuncuHallowenZipkin);
            DegistirilecekGulle.sprite = zipkinSprite[3];
            WindowsDegistirilecekZipkin.sprite = zipkinSprite[3];
            WindowsSeciliZipkinText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().OyuncuHallowenZipkin);

        }
        else if (donenSayi == 9)
        {
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().OyuncuNoelZipkin);
            DegistirilecekGulle.sprite = zipkinSprite[4];
            WindowsDegistirilecekZipkin.sprite = zipkinSprite[4];
            WindowsSeciliZipkinText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().OyuncuNoelZipkin);

        }
        else if (donenSayi == 10)
        {
            DegistirilecekGulle.sprite = GulleSprite[5];
            WindowsDegistirilecekGulle.sprite = GulleSprite[5];
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().OyuncuHallowenGulle);
            WindowsSeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().OyuncuHallowenGulle);
        }
        else if (donenSayi == 11)
        {
            DegistirilecekGulle.sprite = GulleSprite[6];
            WindowsDegistirilecekGulle.sprite = GulleSprite[6];
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().OyuncuNoelGullesi);
            WindowsSeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().OyuncuNoelGullesi);
        }
        else if (donenSayi == 12)
        {
            DegistirilecekGulle.sprite = GulleSprite[7];
            WindowsDegistirilecekGulle.sprite = GulleSprite[7];
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().KalpliGulle);
            WindowsSeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().KalpliGulle);
        }
        else if (donenSayi == 13)
        {
            SeciliGulleText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().KalpliZipkin);
            DegistirilecekGulle.sprite = zipkinSprite[5];
            WindowsDegistirilecekZipkin.sprite = zipkinSprite[5];
            WindowsSeciliZipkinText.text = SayiKisaltici(BenimGemim.GetComponent<Player>().KalpliZipkin);

        }
    }
    

    public void GirisYap()
    {
        OyunUI.SetActive(true);
        kullaniciAdi = kullaniciAdiGirisYapText.text;
        sifre = sifreGirisYapText.text;
        BenimGemim.GetComponent<Player>().KullaniciGiris(kullaniciAdiGirisYapText.text, sifreGirisYapText.text, Version);
        KameraOyuncuyaKilitlensin();
    }


    public void SatinAl(int SatinAlinacakObjeID)
    {
        if (SatinAlinacakObjeID < 12 || SatinAlinacakObjeID >= 17 || SatinAlinacakObjeID == 30 || SatinAlinacakObjeID == 31|| SatinAlinacakObjeID == 32)
        {
            if (SatinAlinacakObjeID == 17)
            {
                BenimGemim.GetComponent<Player>().SunucuyaSatinAlmaIstegiYolla(SatinAlinacakObjeID, int.Parse(MiktarAdeti[12].text));
            }
            else if (SatinAlinacakObjeID == 18)
            {
                BenimGemim.GetComponent<Player>().SunucuyaSatinAlmaIstegiYolla(SatinAlinacakObjeID, int.Parse(MiktarAdeti[13].text));
            }
            else if (SatinAlinacakObjeID == 19)
            {
                BenimGemim.GetComponent<Player>().SunucuyaSatinAlmaIstegiYolla(SatinAlinacakObjeID, int.Parse(MiktarAdeti[14].text));
            }
            else if (SatinAlinacakObjeID == 20)
            {
                BenimGemim.GetComponent<Player>().SunucuyaSatinAlmaIstegiYolla(SatinAlinacakObjeID, int.Parse(MiktarAdeti[15].text));
            }
            else if (SatinAlinacakObjeID >= 30)
            {
                BenimGemim.GetComponent<Player>().SunucuyaSatinAlmaIstegiYolla(SatinAlinacakObjeID, int.Parse(MiktarAdeti[SatinAlinacakObjeID - 15].text));
                Debug.Log(MiktarAdeti[SatinAlinacakObjeID - 15].text);
            }
            else if (SatinAlinacakObjeID >= 21 || SatinAlinacakObjeID <= 29)
            {
                BenimGemim.GetComponent<Player>().SunucuyaSatinAlmaIstegiYolla(SatinAlinacakObjeID, 1);
            }
            
            else
            {
                BenimGemim.GetComponent<Player>().SunucuyaSatinAlmaIstegiYolla(SatinAlinacakObjeID, int.Parse(MiktarAdeti[SatinAlinacakObjeID].text));
            }
        }
        else
        {
            BenimGemim.GetComponent<Player>().SunucuyaSatinAlmaIstegiYolla(SatinAlinacakObjeID, 1);
        }
    }

    /// ////////////////////////////////////////////////////


    // Belirtilen sayýda öðe ekler
    public void AddItems(List<string> gelenVeriler, List<int> gelenSeviye, List<int> gelenYetkiId)
    {

        List<String> yetkiler = new List<string>();
        yetkiler.Add(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 377]);
        yetkiler.Add(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 378]);
        yetkiler.Add(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 379]);
        yetkiler.Add(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 380]);
        // Daha önce oluþturulan tüm öðeleri sil
        foreach (Transform child in FiloiciUyelercontent.transform)
        {
            Destroy(child.gameObject);
        }
        FiloiciUyeleritemList.Clear();
        FiloiciUyelercontentHeight = 0;
        for (int i = 0; i < gelenVeriler.Count; i++)
        {
            GameObject item = Instantiate(FiloiciUyeleritemPrefab);
            item.transform.SetParent(FiloiciUyelercontent.transform, false);
            item.transform.Find("OyuncuAdi").GetComponent<Text>().text = gelenVeriler[i];
            item.transform.Find("Seviye").GetComponent<Text>().text = gelenSeviye[i].ToString();
            if(BenimGemim.GetComponent<Player>().OyuncuYetkiID == 1)
            {
                // yetkileri dropdownlara yükler
                item.transform.Find("Yetki").GetComponent<Dropdown>().AddOptions(yetkiler);

                // yetkileri dropdowndan seçer
                if (gelenYetkiId[i] == 1)
                {
                    item.transform.Find("Yetki").GetComponent<Dropdown>().value = 0;
                    item.transform.Find("Yetki").GetComponent<Dropdown>().options.RemoveAt(1);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().options.RemoveAt(1);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().options.RemoveAt(1);
                }
                else if (gelenYetkiId[i] == 2)
                {
                    item.transform.Find("Yetki").GetComponent<Dropdown>().value = 1;
                    item.transform.Find("FilodanAtButton").gameObject.SetActive(true);
                }
                else if (gelenYetkiId[i] == 3)
                {
                    item.transform.Find("Yetki").GetComponent<Dropdown>().value = 2;
                    item.transform.Find("FilodanAtButton").gameObject.SetActive(true);
                }
                else if (gelenYetkiId[i] == 4)
                {
                    item.transform.Find("Yetki").GetComponent<Dropdown>().value = 3;
                    item.transform.Find("FilodanAtButton").gameObject.SetActive(true);
                }
            }
            else
            {
                // yetkileri dropdownlara doldurur
                if (gelenYetkiId[i] == 1)
                {
                    yetkiler.Clear();
                    yetkiler.Add(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 377]);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().AddOptions(yetkiler);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().value = 0;
                }
                else if (gelenYetkiId[i] == 2)
                {
                    yetkiler.Clear();
                    yetkiler.Add(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 378]);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().AddOptions(yetkiler);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().value = 0;
                }
                else if (gelenYetkiId[i] == 3)
                {
                    yetkiler.Clear();
                    yetkiler.Add(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 379]);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().AddOptions(yetkiler);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().value = 0;
                }
                else if (gelenYetkiId[i] == 4)
                {
                    yetkiler.Clear();
                    yetkiler.Add(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 380]);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().AddOptions(yetkiler);
                    item.transform.Find("Yetki").GetComponent<Dropdown>().value = 0;
                }
            }
            // filodan at butonunu yardýmcý kaptan için açar
            if (gelenYetkiId[i] > 2 && BenimGemim.GetComponent<Player>().OyuncuYetkiID == 2)
            {
                item.transform.Find("FilodanAtButton").gameObject.SetActive(true);
            }
            
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloiciUyelercontentHeight - FiloiciUyelercontentPadding);
            FiloiciUyeleritemList.Add(item);
            FiloiciUyelercontentHeight += FiloiciUyeleritemHeight + FiloiciUyelerspacing;
        }
        FiloiciUyelercontent.sizeDelta = new Vector2(FiloiciUyelercontent.sizeDelta.x, FiloiciUyelercontentHeight + FiloiciUyelercontentPadding * 2);
    }





    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItem(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeight();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeight()
    {
        FiloiciUyelercontentHeight = 0;
        for (int i = 0; i < FiloiciUyeleritemList.Count; i++)
        {
            GameObject item = FiloiciUyeleritemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloiciUyeleritemHeight + FiloiciUyelerspacing));
            FiloiciUyelercontentHeight += FiloiciUyeleritemHeight + FiloiciUyelerspacing;
        }
        FiloiciUyelercontent.sizeDelta = new Vector2(FiloiciUyelercontent.sizeDelta.x, FiloiciUyelercontentHeight + FiloiciUyelercontentPadding * 2);
    }

    /// //////////////////////////////////////////////////////////////////////////////

    // Belirtilen sayýda öðe ekler
    public void AddItemsFiloBasvurular(List<string> gelenVeriler, List<int> gelenSeviye)
    {
        // Daha önce oluþturulan tüm öðeleri sil
        foreach (Transform child in FiloiciBasvurularcontent.transform)
        {
            Destroy(child.gameObject);
        }
        FiloiciBasvurularitemList.Clear();
        FiloiciBasvurularcontentHeight = 0;

        for (int i = 0; i < gelenVeriler.Count; i++)
        {
            GameObject item = Instantiate(FiloiciBasvurulartemPrefab);
            item.transform.SetParent(FiloiciBasvurularcontent.transform, false);
            item.transform.Find("OyuncuAdi").GetComponent<Text>().text = gelenVeriler[i];
            item.transform.Find("Seviye").GetComponent<Text>().text = gelenSeviye[i].ToString();
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloiciBasvurularcontentHeight - FiloiciBasvurularcontentPadding);
            FiloiciBasvurularitemList.Add(item);
            FiloiciBasvurularcontentHeight += FiloiciBasvurularitemHeight + FiloiciBasvurularspacing;
        }
        FiloiciBasvurularcontent.sizeDelta = new Vector2(FiloiciBasvurularcontent.sizeDelta.x, FiloiciBasvurularcontentHeight + FiloiciBasvurularcontentPadding * 2);
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloBasvurular(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloBasvurular();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloBasvurular()
    {
        FiloiciBasvurularcontentHeight = 0;
        for (int i = 0; i < FiloiciBasvurularitemList.Count; i++)
        {
            GameObject item = FiloiciBasvurularitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloiciBasvurularitemHeight + FiloiciBasvurularspacing));
            FiloiciBasvurularcontentHeight += FiloiciBasvurularitemHeight + FiloiciBasvurularspacing;
        }
        FiloiciBasvurularcontent.sizeDelta = new Vector2(FiloiciBasvurularcontent.sizeDelta.x, FiloiciBasvurularcontentHeight + FiloiciBasvurularcontentPadding * 2);
    }

    public void AddItemsSeyirDefteri(string gelenVeriler)
    {
        SeyirDefteriliste.Insert(0, gelenVeriler);
        // Daha önce oluþturulan tüm öðeleri sil
        foreach (Transform child in SeyirDeftericontent.transform)
        {
            Destroy(child.gameObject);
        }
        SeyirDefteriitemList.Clear();
        SeyirDeftericontentHeight = 0;
        for (int i = 0; i < SeyirDefteriliste.Count; i++)
        {
            GameObject item = Instantiate(SeyirDefteriitemPrefab);
            item.transform.SetParent(SeyirDeftericontent.transform, false);
            item.transform.Find("SeyirdefteriTxt").GetComponent<Text>().text = SeyirDefteriliste[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -SeyirDeftericontentHeight - SeyirDeftericontentPadding);
            SeyirDefteriitemList.Add(item);
            SeyirDeftericontentHeight += SeyirDefteriitemHeight + SeyirDefterispacing;
        }

        SeyirDeftericontent.sizeDelta = new Vector2(SeyirDeftericontent.sizeDelta.x, SeyirDeftericontentHeight + SeyirDeftericontentPadding * 2);
    }

    //Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemSeyirDefteri(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightSeyirDefteri();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightSeyirDefteri()
    {
        SeyirDeftericontentHeight = 0;
        for (int i = 0; i < SeyirDefteriitemList.Count; i++)
        {
            GameObject item = SeyirDefteriitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (SeyirDefteriitemHeight + SeyirDefterispacing));
            SeyirDeftericontentHeight += SeyirDefteriitemHeight + SeyirDefterispacing;
        }
        SeyirDeftericontent.sizeDelta = new Vector2(SeyirDeftericontent.sizeDelta.x, SeyirDeftericontentHeight + SeyirDeftericontentPadding * 2);
    }

    public void AddItemsGuncellemeler(string gelenVeriler)
    {
        GuncellemlerListe.Insert(0, gelenVeriler);
        // Daha önce oluþturulan tüm öðeleri sil
        foreach (Transform child in Guncellemelercontent.transform)
        {
            Destroy(child.gameObject);
        }
        GuncellemeleritemList.Clear();
        GuncellemelercontentHeight = 0;
        for (int i = 0; i < GuncellemlerListe.Count; i++)
        {
            GameObject item = Instantiate(GuncellemeleritemPrefab);
            item.transform.SetParent(Guncellemelercontent.transform, false);
            item.transform.Find("GuncellemelerTxt").GetComponent<Text>().text = GuncellemlerListe[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -GuncellemelercontentHeight - GuncellemelercontentPadding);
            GuncellemeleritemList.Add(item);
            GuncellemelercontentHeight += GuncellemeleritemHeight + Guncellemelerspacing;
        }

        Guncellemelercontent.sizeDelta = new Vector2(Guncellemelercontent.sizeDelta.x, GuncellemelercontentHeight + GuncellemelercontentPadding * 2);
    }

    //Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemGuncellemeler(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightGuncellemeler();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightGuncellemeler()
    {
        GuncellemelercontentHeight = 0;
        for (int i = 0; i < SeyirDefteriitemList.Count; i++)
        {
            GameObject item = SeyirDefteriitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (GuncellemeleritemHeight + Guncellemelerspacing));
            GuncellemelercontentHeight += GuncellemeleritemHeight + Guncellemelerspacing;
        }
        Guncellemelercontent.sizeDelta = new Vector2(Guncellemelercontent.sizeDelta.x, GuncellemelercontentHeight + GuncellemelercontentPadding * 2);
    }
   

    public void AddItemsFiloArama(int gelenFiloId, string glenFiloAd, string gelenFiloKisaltma, string gelenFiloSayisi, int gelenverisayisi)
    {
        // Daha önce oluþturulan tüm öðeleri sil
        GameObject item = Instantiate(FiloAramaitemPrefab);
        item.transform.SetParent(FiloAramacontent.transform, false);
        //      item.transform.Find("gelenFiloId").GetComponent<Text>().text = gelenFiloId[i].ToString();
        item.transform.GetComponent<FiloBasvuruAt>().filoId = gelenFiloId;
        item.transform.Find("KlanUzunIsmi").GetComponent<Text>().text = glenFiloAd;
        item.transform.Find("FiloKisaAd").GetComponent<Text>().text = gelenFiloKisaltma;
        item.transform.Find("KisiSayisi").GetComponent<Text>().text = gelenFiloSayisi;
        item.transform.Find("Katilbutton").gameObject.SetActive(true);
        item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloAramacontentHeight - FiloAramacontentPadding);
        FiloAramaitemList.Add(item);
        FiloAramacontentHeight += FiloAramaitemHeight + FiloAramaspacing;
        FiloAramacontent.sizeDelta = new Vector2(FiloAramacontent.sizeDelta.x, FiloAramacontentHeight + FiloAramacontentPadding * 2);
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloArama(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloFiloArama();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloFiloArama()
    {
        FiloAramacontentHeight = 0;
        for (int i = 0; i < FiloAramaitemList.Count; i++)
        {
            GameObject item = FiloAramaitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloAramaitemHeight + FiloAramaspacing));
            FiloAramacontentHeight += FiloAramaitemHeight + FiloAramaspacing;
        }
        FiloAramacontent.sizeDelta = new Vector2(FiloAramacontent.sizeDelta.x, FiloAramacontentHeight + FiloAramacontentPadding * 2);
    }


    public void AddItemsFiloBagisSeyirDefteri( int[] gelenbagismiktari, string[] gelenoyuncuadi)
    {
        // Daha önce oluþturulan tüm öðeleri sil
        foreach (Transform child in FiloBagisSeyirDeftericontent.transform)
        {
            Destroy(child.gameObject);
        }
        FiloBagisSeyirDefteriitemList.Clear();
        FiloBagisSeyirDeftericontentHeight = 0;
        for (int i = 0; i < gelenbagismiktari.Length; i++)
        {
            if (gelenbagismiktari[i] > 0)
            {
                FiloSeyirDefteriliste.Insert(i, gelenoyuncuadi[i] + dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 381] + gelenbagismiktari[i] + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 382]);
                GameObject item = Instantiate(FiloBagisSeyirDefteriitemPrefab);
                item.transform.SetParent(FiloBagisSeyirDeftericontent.transform, false);
                item.transform.Find("FiloSeyirDefteriText").GetComponent<Text>().text = FiloSeyirDefteriliste[i];
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloBagisSeyirDeftericontentHeight - FiloBagisSeyirDeftericontentPadding);
                FiloBagisSeyirDefteriitemList.Add(item);
                FiloBagisSeyirDeftericontentHeight += FiloBagisSeyirDefteriitemHeight + FiloBagisSeyirDefterispacing;
            }
        }

        FiloBagisSeyirDeftericontent.sizeDelta = new Vector2(FiloBagisSeyirDeftericontent.sizeDelta.x, FiloBagisSeyirDeftericontentHeight + FiloBagisSeyirDeftericontentPadding * 2);
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloBagisSeyirDefteri(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloBagisSeyirDefteri();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloBagisSeyirDefteri()
    {
        FiloBagisSeyirDeftericontentHeight = 0;
        for (int i = 0; i < FiloBagisSeyirDefteriitemList.Count; i++)
        {
            GameObject item = FiloBagisSeyirDefteriitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloBagisSeyirDefteriitemHeight + FiloBagisSeyirDefterispacing));
            FiloBagisSeyirDeftericontentHeight += FiloBagisSeyirDefteriitemHeight + FiloBagisSeyirDefterispacing;
        }
        FiloBagisSeyirDeftericontent.sizeDelta = new Vector2(FiloBagisSeyirDeftericontent.sizeDelta.x, FiloBagisSeyirDeftericontentHeight + FiloBagisSeyirDeftericontentPadding * 2);
    }
    public void AddItemsFiloCevrimicicUye(string[] gelenoyuncuadi,int[] gelenoyuncutecrubesi,int gelenuyesayisi)
    {
        int gelecekPrefabDegeri = 0; 
        foreach (Transform child in FiloCevrimicicontent.transform)
        {
            Destroy(child.gameObject);
        }
        FiloCevrimiciitemList.Clear();
        FiloCevrimicicontentHeight = 0;
        for (int i = 0; i < gelenuyesayisi; i++)
        {
            gelecekPrefabDegeri = gelecekPrefabDegeri % 2;
            GameObject item = Instantiate(FiloCevrimiciitemPrefab[gelecekPrefabDegeri]);
            item.transform.SetParent(FiloCevrimicicontent.transform, false);
            item.transform.Find("OyuncuAdi").GetComponent<Text>().text = gelenoyuncuadi[i].ToString();
            item.transform.Find("OyuncuLeveli").GetComponent<Text>().text = gelenoyuncutecrubesi[i].ToString();
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloCevrimicicontentHeight - FiloCevrimicicontentPadding);
            FiloCevrimiciitemList.Add(item);
            FiloCevrimicicontentHeight += FiloCevrimiciitemHeight + FiloCevrimicispacing;
            gelecekPrefabDegeri++;
        }
        FiloCevrimicicontent.sizeDelta = new Vector2(FiloCevrimicicontent.sizeDelta.x, FiloCevrimicicontentHeight + FiloCevrimicicontentPadding * 2);
      

    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloCevrimicicUye(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloCevrimicicUye();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloCevrimicicUye()
    {
        FiloCevrimicicontentHeight = 0;
        for (int i = 0; i < FiloCevrimiciitemList.Count; i++)
        {
            GameObject item = FiloCevrimiciitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloCevrimiciitemHeight + FiloCevrimicispacing));
            FiloCevrimicicontentHeight += FiloCevrimiciitemHeight + FiloCevrimicispacing;
        }
        FiloCevrimicicontent.sizeDelta = new Vector2(FiloCevrimicicontent.sizeDelta.x, FiloCevrimicicontentHeight + FiloCevrimicicontentPadding * 2);
    }

    public void AddItemsDestekTalebiDurumu(int[] gelenKonu, int[] gelenDurum, int gelenTalepSayisi, int[] gelenDestekNo)
    {
        foreach (Transform child in DestekTalebiDurumucontent.transform)
        {
            Destroy(child.gameObject);
        }
        DestekTalebiDurumuitemList.Clear();
        DestekTalebiDurumucontentHeight = 0;
        for (int i = 0; i < gelenTalepSayisi; i++)
        {
            GameObject item = Instantiate(DestekTalebiDurumuitemPrefab);
            item.GetComponent<DetayOku>().KonuId = gelenKonu[i];
            item.GetComponent<DetayOku>().DestekNo = gelenDestekNo[i];
            item.GetComponent<DetayOku>().TamamlanmaDurumu = gelenDurum[i];
            item.transform.SetParent(DestekTalebiDurumucontent.transform, false);
            if (gelenKonu[i] == 1)
            {
                item.transform.Find("KonuTXT").GetComponent<Text>().text = destekTalebiOlusturDropdown.options[1].text;
            }
            else if (gelenKonu[i] == 2)
            {
                item.transform.Find("KonuTXT").GetComponent<Text>().text = destekTalebiOlusturDropdown.options[2].text;
            }
            else if (gelenKonu[i] == 3)
            {
                item.transform.Find("KonuTXT").GetComponent<Text>().text = destekTalebiOlusturDropdown.options[3].text;
            }
            else if (gelenKonu[i] == 4)
            {
                item.transform.Find("KonuTXT").GetComponent<Text>().text = destekTalebiOlusturDropdown.options[4].text;
            }
            else if (gelenKonu[i] == 5)
            {
                item.transform.Find("KonuTXT").GetComponent<Text>().text = destekTalebiOlusturDropdown.options[5].text;
            }
            else if (gelenKonu[i] == 6)
            {
                item.transform.Find("KonuTXT").GetComponent<Text>().text = destekTalebiOlusturDropdown.options[6].text;
            }
            if (gelenDurum[i] == 0)
            {
                item.transform.Find("Durum").transform.Find("Text (Legacy)").GetComponent<Text>().text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 298];
            }
            else if (gelenDurum[i] == 1)
            {
                item.transform.Find("Durum").transform.Find("Text (Legacy)").GetComponent<Text>().text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 297];
                item.transform.Find("Durum").GetComponent<Image>().color = Color.green;
            }
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -DestekTalebiDurumucontentHeight - DestekTalebiDurumucontentPadding);
            DestekTalebiDurumuitemList.Add(item);
            DestekTalebiDurumucontentHeight += DestekTalebiDurumuitemHeight + DestekTalebiDurumuspacing;
        }
        DestekTalebiDurumucontent.sizeDelta = new Vector2(DestekTalebiDurumucontent.sizeDelta.x, DestekTalebiDurumucontentHeight + DestekTalebiDurumucontentPadding * 2);
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemDestekTalebiDurumu(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightDestekTalebiDurumu();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightDestekTalebiDurumu()
    {
        DestekTalebiDurumucontentHeight = 0;
        for (int i = 0; i < DestekTalebiDurumuitemList.Count; i++)
        {
            GameObject item = DestekTalebiDurumuitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (DestekTalebiDurumuitemHeight + DestekTalebiDurumuspacing));
            DestekTalebiDurumucontentHeight += DestekTalebiDurumuitemHeight + DestekTalebiDurumuspacing;
        }
        DestekTalebiDurumucontent.sizeDelta = new Vector2(DestekTalebiDurumucontent.sizeDelta.x, DestekTalebiDurumucontentHeight + DestekTalebiDurumucontentPadding * 2);
    }

    public void AddItemsDestekTalebiKullaniciMesaj(string gelenmseaj, int gelenOyuncuId)
    {
        GameObject item = Instantiate(DestekTalebiKullaniciMesajitemPrefab);
        item.transform.SetParent(DestekTalebiKullaniciMesajcontent.transform, false);
        if (BenimGemim.GetComponent<Player>().oyuncuId != gelenOyuncuId)
        {
            item.transform.Find("Backround").GetComponent<Image>().color = Color.green;
            item.transform.Find("Mesaj").GetComponent<Text>().text = gelenmseaj.ToString();
        }
        else
        {
            item.transform.Find("Mesaj").GetComponent<Text>().text = gelenmseaj.ToString();
        }
        item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -DestekTalebiKullaniciMesajcontentHeight - DestekTalebiKullaniciMesajcontentPadding);
        DestekTalebiKullaniciMesajitemList.Add(item);
        DestekTalebiKullaniciMesajcontentHeight += DestekTalebiKullaniciMesajitemHeight + DestekTalebiKullaniciMesajspacing;
        DestekTalebiKullaniciMesajcontent.sizeDelta = new Vector2(DestekTalebiKullaniciMesajcontent.sizeDelta.x, DestekTalebiKullaniciMesajcontentHeight + DestekTalebiKullaniciMesajcontentPadding * 2);
        if (BenimGemim.GetComponent<Player>().oyuncuId == 16)
        {
            GameManager.gm.AyarlarDestekKullaniciId.gameObject.SetActive(true);
            GameManager.gm.AyarlarDestekKullaniciId.text = gelenOyuncuId.ToString();
        }
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemDestekTalebiKullaniciMesaj(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightDestekTalebiKullaniciMesaj();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightDestekTalebiKullaniciMesaj()
    {
        DestekTalebiKullaniciMesajcontentHeight = 0;
        for (int i = 0; i < DestekTalebiKullaniciMesajitemList.Count; i++)
        {
            GameObject item = DestekTalebiKullaniciMesajitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (DestekTalebiKullaniciMesajitemHeight + DestekTalebiKullaniciMesajspacing));
            DestekTalebiKullaniciMesajcontentHeight += DestekTalebiKullaniciMesajitemHeight + DestekTalebiKullaniciMesajspacing;
        }
        DestekTalebiKullaniciMesajcontent.sizeDelta = new Vector2(DestekTalebiKullaniciMesajcontent.sizeDelta.x, DestekTalebiKullaniciMesajcontentHeight + DestekTalebiKullaniciMesajcontentPadding * 2);
    }

    public void AddItemsFiloMuttefikIstekleri(string[] filoMuttefikAd, string[] gelenMuttefikFiloKisaltma, string[] filoBarisAd, string[] gelenBarisFiloKisaltma)
    {
        foreach (Transform child in FiloMuttefikBasvurularicontent.transform)
        {
            Destroy(child.gameObject);
        }
        FiloMuttefikBasvurulariitemList.Clear();
        FiloMuttefikBasvurularicontentHeight = 0;
        for (int i = 0; i < filoMuttefikAd.Length; i++)
        {
            if (filoMuttefikAd[i] != null && filoMuttefikAd[i].Length > 0)
            {
                GameObject item = Instantiate(FiloMuttefikBasvurulariitemPrefab);
                item.GetComponent<MuttefikIstekleriButton>().filoKisaltmasi = gelenMuttefikFiloKisaltma[i].ToString();
                item.transform.SetParent(FiloMuttefikBasvurularicontent.transform, false);
                item.transform.Find("FiloAdi").GetComponent<Text>().text = filoMuttefikAd[i].ToString();
                item.transform.Find("FiloKisaltmasi").GetComponent<Text>().text = "[" + gelenMuttefikFiloKisaltma[i].ToString() + "]";
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloMuttefikBasvurularicontentHeight - FiloMuttefikBasvurularicontentPadding);
                FiloMuttefikBasvurulariitemList.Add(item);
                FiloMuttefikBasvurularicontentHeight += FiloMuttefikBasvurulariitemHeight + FiloMuttefikBasvurularispacing;
            }
        }
        for (int i = 0; i < filoBarisAd.Length; i++)
        {
            if (filoBarisAd[i] != null && filoBarisAd[i].Length > 0)
            {
                GameObject item = Instantiate(BarisTeklifleriitemPrefab);
                item.GetComponent<BarisTeklifleriButton>().filoKisaltmasi = gelenBarisFiloKisaltma[i].ToString();
                item.transform.SetParent(FiloMuttefikBasvurularicontent.transform, false);
                item.transform.Find("FiloAdi").GetComponent<Text>().text = filoBarisAd[i].ToString();
                item.transform.Find("FiloKisaltmasi").GetComponent<Text>().text = "[" + gelenBarisFiloKisaltma[i].ToString() + "]";
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloMuttefikBasvurularicontentHeight - FiloMuttefikBasvurularicontentPadding);
                FiloMuttefikBasvurulariitemList.Add(item);
                FiloMuttefikBasvurularicontentHeight += FiloMuttefikBasvurulariitemHeight + FiloMuttefikBasvurularispacing;
            }
        }
        FiloMuttefikBasvurularicontent.sizeDelta = new Vector2(FiloMuttefikBasvurularicontent.sizeDelta.x, FiloMuttefikBasvurularicontentHeight + FiloMuttefikBasvurularicontentPadding * 2);
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloMuttefikIstekleri(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloMuttefikIstekleri();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloMuttefikIstekleri()
    {
        FiloMuttefikBasvurularicontentHeight = 0;
        for (int i = 0; i < FiloMuttefikBasvurulariitemList.Count; i++)
        {
            GameObject item = FiloMuttefikBasvurulariitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloMuttefikBasvurulariitemHeight + FiloMuttefikBasvurularispacing));
            DestekTalebiKullaniciMesajcontentHeight += DestekTalebiKullaniciMesajitemHeight + DestekTalebiKullaniciMesajspacing;
        }
        FiloMuttefikBasvurularicontent.sizeDelta = new Vector2(FiloMuttefikBasvurularicontent.sizeDelta.x, FiloMuttefikBasvurularicontentHeight + FiloMuttefikBasvurularicontentPadding * 2);
    }

    public void AddItemsFiloMuttefik(string[] filoAd,int donenSayi,string[] filoKisaAd)
    {
        foreach (Transform child in FiloMuttefikcontent.transform)
        {
            Destroy(child.gameObject);
        }
        FiloMuttefikitemList.Clear();
        FiloMuttefikcontentHeight = 0;
            for (int i = 0; i < donenSayi; i++)
            {
                GameObject item = Instantiate(FiloMuttefikitemPrefab);
                item.GetComponent<MuttefikIstatistikleriButton>().filoKisaltmasi = filoKisaAd[i].ToString();
                item.transform.SetParent(FiloMuttefikcontent.transform, false);
                item.transform.Find("FiloAdi").GetComponent<Text>().text = filoAd[i];
                item.transform.Find("FiloKisaltmasi").GetComponent<Text>().text = filoKisaAd[i];
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloMuttefikcontentHeight - FiloMuttefikcontentPadding);
                FiloMuttefikitemList.Add(item);
                FiloMuttefikcontentHeight += FiloMuttefikitemHeight + FiloMuttefikspacing;
            }
            FiloMuttefikcontent.sizeDelta = new Vector2(FiloMuttefikcontent.sizeDelta.x, FiloMuttefikcontentHeight + FiloMuttefikcontentPadding * 2);
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloMuttefik(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloMuttefik();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloMuttefik()
    {
        FiloMuttefikcontentHeight = 0;
        for (int i = 0; i < FiloMuttefikitemList.Count; i++)
        {
            GameObject item = FiloMuttefikitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloMuttefikitemHeight + FiloMuttefikspacing));
            FiloMuttefikcontentHeight += FiloMuttefikitemHeight + FiloMuttefikspacing;
        }
        FiloMuttefikcontent.sizeDelta = new Vector2(FiloMuttefikcontent.sizeDelta.x, FiloMuttefikcontentHeight + FiloMuttefikcontentPadding * 2);
    }

    public void AddItemsFiloDusman(string[] filoAd, int donenSayi,string[] donenFiloKisaAd)
    {
        foreach (Transform child in FiloDusmancontent.transform)
        {
            Destroy(child.gameObject);
        }
        FiloDusmanitemList.Clear();
        FiloDusmancontentHeight = 0;
            for (int i = 0; i < donenSayi; i++)
            {
                GameObject item = Instantiate(FiloDusmanitemPrefab);
                item.transform.SetParent(FiloDusmancontent.transform, false);
            item.GetComponent<DusmanFiloButton>().filoKisaltmasi = donenFiloKisaAd[i].ToString();
            item.transform.Find("FiloAdi").GetComponent<Text>().text = filoAd[i];
                item.transform.Find("FiloKisaltmasi").GetComponent<Text>().text = donenFiloKisaAd[i];
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloDusmancontentHeight - FiloDusmancontentPadding);
                FiloDusmanitemList.Add(item);
                FiloDusmancontentHeight += FiloDusmanitemHeight + FiloDusmanspacing;
            }
            FiloDusmancontent.sizeDelta = new Vector2(FiloDusmancontent.sizeDelta.x, FiloDusmancontentHeight + FiloDusmancontentPadding * 2);
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloDusman(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloDusman();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloDusman()
    {
        FiloDusmancontentHeight = 0;
        for (int i = 0; i < FiloDusmanitemList.Count; i++)
        {
            GameObject item = FiloDusmanitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloDusmanitemHeight + FiloDusmanspacing));
            FiloDusmancontentHeight += FiloDusmanitemHeight + FiloDusmanspacing;
        }
        FiloDusmancontent.sizeDelta = new Vector2(FiloDusmancontent.sizeDelta.x, FiloDusmancontentHeight + FiloDusmancontentPadding * 2);
    }

    public void AddItemsFiloDiplomasiPanel()
    {
        foreach (Transform child in FiloDiplomasiPanelcontent.transform)
        {
            Destroy(child.gameObject);
        }
        FiloDiplomasiPanelitemList.Clear();
        FiloDiplomasiPanelcontentHeight = 0;
        for (int i = 0; i < FiloDiplomasiPanelitemList.Count; i++)
        {
            GameObject item = Instantiate(FiloDiplomasiPanelitemPrefab);
            item.transform.SetParent(FiloDiplomasiPanelcontent.transform, false);
        
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloDiplomasiPanelcontentHeight - FiloDiplomasiPanelcontentPadding);
            FiloDiplomasiPanelitemList.Add(item);
            FiloDiplomasiPanelcontentHeight += FiloDiplomasiPanelitemHeight + FiloDiplomasiPanelspacing;
        }
        FiloDiplomasiPanelcontent.sizeDelta = new Vector2(FiloDiplomasiPanelcontent.sizeDelta.x, FiloDiplomasiPanelcontentHeight + FiloDiplomasiPanelcontentPadding * 2);
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloDiplomasiPanel(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloDiplomasiPanel();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloDiplomasiPanel()
    {
        FiloDusmancontentHeight = 0;
        for (int i = 0; i < FiloDiplomasiPanelitemList.Count; i++)
        {
            GameObject item = FiloDiplomasiPanelitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloDiplomasiPanelitemHeight + FiloDiplomasiPanelspacing));
            FiloDiplomasiPanelcontentHeight += FiloDusmanitemHeight + FiloDusmanspacing;
        }
        FiloDiplomasiPanelcontent.sizeDelta = new Vector2(FiloDiplomasiPanelcontent.sizeDelta.x, FiloDiplomasiPanelcontentHeight + FiloDiplomasiPanelcontentPadding * 2);
    }
    
    
    public void AddItemsFiloAdalarSlot(int[] donenFilonunAdalari, int[] donenHarita5, int[] donenHarita6, int[] donenHarita7, int[] donenHarita8, int[] donenHarita9, int[] donenHarita5KulelerMaxCan, int[] donenHarita5KulelerCan, int[] donenHarita6KulelerMaxCan, int[] donenHarita6KulelerCan, int[] donenHarita7KulelerMaxCan, int[] donenHarita7KulelerCan, int[] donenHarita8KulelerMaxCan, int[] donenHarita8KulelerCan, int[] donenHarita9KulelerMaxCan, int[] donenHarita9KulelerCan)
    {
        foreach (Transform child in FiloAdalarSlotcontent.transform)
        {
            Destroy(child.gameObject);
        }
        FiloAdalarSlotitemList.Clear();
        FiloAdalarSlotcontentHeight = 0;
    
        if (donenFilonunAdalari[0] == 1)
        {
            GameObject item = Instantiate(FiloAdalarSlotitemPrefab);
            item.transform.SetParent(FiloAdalarSlotcontent.transform, false);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloAdalarSlotcontentHeight - FiloAdalarSlotcontentPadding);
            item.transform.Find("HaritaTXT").GetComponent<Text>().text = "HARÝTA 5";
            for (int i = 0; i < 12; i++)
            {
                item.transform.Find("Pencere" + (i+1) + "/KuleIMG").GetComponent<Image>().sprite = AdaKuleSprite[Mathf.Abs(donenHarita5[i])];
                if (donenHarita5[i] > 0)
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().maxValue = donenHarita5KulelerMaxCan[i];
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().value = donenHarita5KulelerCan[i];
                }
                else
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").gameObject.SetActive(false);
                }
            }
            FiloAdalarSlotitemList.Add(item);
            FiloAdalarSlotcontentHeight += FiloAdalarSlotitemHeight + FiloAdalarSlotspacing;
        }
        if (donenFilonunAdalari[1] == 1)
        {
            GameObject item = Instantiate(FiloAdalarSlotitemPrefab);
            item.transform.SetParent(FiloAdalarSlotcontent.transform, false);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloAdalarSlotcontentHeight - FiloAdalarSlotcontentPadding);
            item.transform.Find("HaritaTXT").GetComponent<Text>().text = "HARÝTA 6";
            for (int i = 0; i < 12; i++)
            {
                item.transform.Find("Pencere" + (i + 1) + "/KuleIMG").GetComponent<Image>().sprite = AdaKuleSprite[Mathf.Abs(donenHarita6[i])];
                if (donenHarita6[i] > 0)
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().maxValue = donenHarita6KulelerMaxCan[i];
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().value = donenHarita6KulelerCan[i];
                }
                else
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").gameObject.SetActive(false);
                }
            }
            FiloAdalarSlotitemList.Add(item);
            FiloAdalarSlotcontentHeight += FiloAdalarSlotitemHeight + FiloAdalarSlotspacing;
        }
        if (donenFilonunAdalari[2] == 1)
        {
            GameObject item = Instantiate(FiloAdalarSlotitemPrefab);
            item.transform.SetParent(FiloAdalarSlotcontent.transform, false);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloAdalarSlotcontentHeight - FiloAdalarSlotcontentPadding);
            item.transform.Find("HaritaTXT").GetComponent<Text>().text = "HARÝTA 7";
            for (int i = 0; i < 12; i++)
            {
                item.transform.Find("Pencere" + (i + 1) + "/KuleIMG").GetComponent<Image>().sprite = AdaKuleSprite[Mathf.Abs(donenHarita7[i])];
                if (donenHarita7[i] > 0)
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().maxValue = donenHarita7KulelerMaxCan[i];
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().value = donenHarita7KulelerCan[i];
                }
                else
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").gameObject.SetActive(false);
                }
            }
            FiloAdalarSlotitemList.Add(item);
            FiloAdalarSlotcontentHeight += FiloAdalarSlotitemHeight + FiloAdalarSlotspacing;
        }
        if (donenFilonunAdalari[3] == 1)
        {
            GameObject item = Instantiate(FiloAdalarSlotitemPrefab);
            item.transform.SetParent(FiloAdalarSlotcontent.transform, false);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloAdalarSlotcontentHeight - FiloAdalarSlotcontentPadding);
            item.transform.Find("HaritaTXT").GetComponent<Text>().text = "HARÝTA 8";
            for (int i = 0; i < 12; i++)
            {
                item.transform.Find("Pencere" + (i + 1) + "/KuleIMG").GetComponent<Image>().sprite = AdaKuleSprite[Mathf.Abs(donenHarita8[i])];
                if (donenHarita8[i] > 0)
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().maxValue = donenHarita8KulelerMaxCan[i];
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().value = donenHarita8KulelerCan[i];
                }
                else
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").gameObject.SetActive(false);
                }
            }
            FiloAdalarSlotitemList.Add(item);
            FiloAdalarSlotcontentHeight += FiloAdalarSlotitemHeight + FiloAdalarSlotspacing;
        }
        if (donenFilonunAdalari[4] == 1)
        {
            GameObject item = Instantiate(FiloAdalarSlotitemPrefab);
            item.transform.SetParent(FiloAdalarSlotcontent.transform, false);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloAdalarSlotcontentHeight - FiloAdalarSlotcontentPadding);
            item.transform.Find("HaritaTXT").GetComponent<Text>().text = "HARÝTA 9";
            for (int i = 0; i < 12; i++)
            {
                item.transform.Find("Pencere" + (i + 1) + "/KuleIMG").GetComponent<Image>().sprite = AdaKuleSprite[Mathf.Abs(donenHarita9[i])];
                if (donenHarita9[i] > 0)
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().maxValue = donenHarita9KulelerMaxCan[i];
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").GetComponent<Slider>().value = donenHarita9KulelerCan[i];
                }
                else
                {
                    item.transform.Find("Pencere" + (i + 1) + "/Slider").gameObject.SetActive(false);
                }
            }
            FiloAdalarSlotitemList.Add(item);
            FiloAdalarSlotcontentHeight += FiloAdalarSlotitemHeight + FiloAdalarSlotspacing;
        }
        FiloAdalarSlotcontent.sizeDelta = new Vector2(FiloAdalarSlotcontent.sizeDelta.x, FiloAdalarSlotcontentHeight + FiloAdalarSlotcontentPadding * 2);
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloAdalarSlot(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloAdalarSlot();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloAdalarSlot()
    {
        FiloAdalarSlotcontentHeight = 0;
        for (int i = 0; i < FiloAdalarSlotitemList.Count; i++)
        {
            GameObject item = FiloDiplomasiPanelitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloAdalarSlotitemHeight + FiloAdalarSlotspacing));
            FiloAdalarSlotcontentHeight += FiloAdalarSlotitemHeight + FiloAdalarSlotspacing;
        }
        FiloAdalarSlotcontent.sizeDelta = new Vector2(FiloAdalarSlotcontent.sizeDelta.x, FiloAdalarSlotcontentHeight + FiloAdalarSlotcontentPadding * 2);
    }

    public void ElitPuanaGöreGemiKullandir()
    {

        if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 1500)
        {
            KullanButon[0].SetActive(true);
        }
        if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 7000)
        {
            KullanButon[1].SetActive(true);
        }
         if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 20000)
        {
            KullanButon[2].SetActive(true);
        }
         if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 100000)
        {
            KullanButon[3].SetActive(true);
        }
         if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 300000)
        {
            KullanButon[4].SetActive(true);
        }
         if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 700000)
        {
            KullanButon[5].SetActive(true);
        }
         if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 1400000)
        {
            KullanButon[6].SetActive(true);
        }
         if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 2800000)
        {
            KullanButon[7].SetActive(true);
        }
         if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 5000000)
        {
            KullanButon[8].SetActive(true);
        }
        if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 9000000)
        {
            KullanButon[9].SetActive(true);
        }
        if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 14000000)
        {
            KullanButon[10].SetActive(true);
        }
        if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 18000000)
        {
            KullanButon[11].SetActive(true);
        }
        if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 22000000)
        {
            KullanButon[12].SetActive(true);
        }
        if (BenimGemim.GetComponent<Player>().oyuncuElitPuan >= 30000000)
        {
            KullanButon[13].SetActive(true);
        }
    }

    public void AddItemsMenuSiralama(string gelenoyuncuadi, string gelenPuan, int gelenUyeSayisi)
    {

        Sira++;
        if (gelenoyuncuadi == kullaniciAdi)
        {
            BenimSiralamamSira.text = Sira.ToString();
            BenimSiralamamPuan.text = gelenPuan.ToString();
            BenimSiralamamAd.text = gelenoyuncuadi.ToString();
        }
        if (Sira < 251)
        {
            if (Sira == 1)
            {
                SiralamaBirinciAdTXT.text = gelenoyuncuadi.ToString();
                SiralamaBirinciPuanTXT.text = gelenPuan.ToString();
            }
            else if (Sira == 2)
            {
                SiralamaIkinciAdTXT.text = gelenoyuncuadi.ToString();
                SiralamaIkinciPuanTXT.text = gelenPuan.ToString();
            }
            else if (Sira == 3)
            {
                SiralamaUcuncuAdTXT.text = gelenoyuncuadi.ToString();
                SiralamaUcuncuPuanTXT.text = gelenPuan.ToString();
            }
            else
            {
                GameObject item = Instantiate(MenuSiralamaitemPrefab);
                item.transform.SetParent(MenuSiralamacontent.transform, false);
                item.transform.Find("Ad").GetComponent<Text>().text = gelenoyuncuadi.ToString();
                item.transform.Find("TecrubePuaniTXT").GetComponent<Text>().text = gelenPuan.ToString();
                item.transform.Find("Sira").GetComponent<Text>().text = Sira.ToString();
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -MenuSiralamacontentHeight - MenuSiralamacontentPadding);
                MenuSiralamaitemList.Add(item);
                MenuSiralamacontentHeight += MenuSiralamaitemHeight + MenuSiralamaspacing;
            }
            MenuSiralamacontent.sizeDelta = new Vector2(MenuSiralamacontent.sizeDelta.x, MenuSiralamacontentHeight + MenuSiralamacontentPadding * 2);
        }
       
    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemMenuSirlama(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightMenuSirlama();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightMenuSirlama()
    {
        MenuSiralamacontentHeight = 0;
        for (int i = 0; i < MenuSiralamaitemList.Count; i++)
        {
            GameObject item = MenuSiralamaitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (MenuSiralamaitemHeight + MenuSiralamaspacing));
            MenuSiralamacontentHeight += MenuSiralamaitemHeight + MenuSiralamaspacing;
        }
        MenuSiralamacontent.sizeDelta = new Vector2(MenuSiralamacontent.sizeDelta.x, MenuSiralamacontentHeight + MenuSiralamacontentPadding * 2);
    }

    public void AddItemsFiloSiralama(string gelenfiloadi, string gelenPuan, int gelenUyeSayisi)
    {
        Sira++;
        GameObject item = Instantiate(FiloSiralamasiitemPrefab);
        item.transform.SetParent(FiloSiralamasicontent.transform, false);
        item.transform.Find("Ad").GetComponent<Text>().text = gelenfiloadi.ToString();
        item.transform.Find("TecrubePuaniTXT").GetComponent<Text>().text = gelenPuan.ToString();
        item.transform.Find("Sira").GetComponent<Text>().text = Sira.ToString();
        item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -FiloSiralamasicontentHeight - FiloSiralamasicontentPadding);
        FiloSiralamasiitemList.Add(item);
        FiloSiralamasicontentHeight += FiloSiralamasiitemHeight + FiloSiralamasispacing;
        if (gelenfiloadi == BenimGemim.GetComponent<Player>().OyuncuFiloAd)
        {
            item.transform.GetComponent<Image>().color = Color.green;
        }
        FiloSiralamasicontent.sizeDelta = new Vector2(FiloSiralamasicontent.sizeDelta.x, FiloSiralamasicontentHeight + FiloSiralamasicontentPadding * 2);

    }

    // Belirtilen öðeyi listeden kaldýrýr
    public void RemoveItemFiloSirlama(List<GameObject> listGameObject)
    {
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject item = listGameObject[0];
            listGameObject.RemoveAt(0);
            Destroy(item);
            RecalculateContentHeightFiloSirlama();
        }
    }

    // Ýçerik yüksekliðini yeniden hesaplar ve boyutunu ayarlar
    public void RecalculateContentHeightFiloSirlama()
    {
        FiloSiralamasicontentHeight = 0;
        for (int i = 0; i < FiloSiralamasiitemList.Count; i++)
        {
            GameObject item = FiloSiralamasiitemList[i];
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * (FiloSiralamasiitemHeight + FiloSiralamasispacing));
            FiloSiralamasicontentHeight += FiloSiralamasiitemHeight + FiloSiralamasispacing;
        }
        FiloSiralamasicontent.sizeDelta = new Vector2(FiloSiralamasicontent.sizeDelta.x, FiloSiralamasicontentHeight + FiloSiralamasicontentPadding * 2);
    }

    public IEnumerator Parlat()
    {
        OldurmeEfekti.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        OldurmeEfekti.SetActive(false);
    }

    public void MarketToplarAc()
    {
        ToplarMarket.SetActive(true);
        CephaneMarket.SetActive(false);
        VipMarket.SetActive(false);
        PromosyonKoduMarket.SetActive(false);
    }
    public void MarketCephaneAc()
    {
        ToplarMarket.SetActive(false);
        CephaneMarket.SetActive(true);
        VipMarket.SetActive(false);
        PromosyonKoduMarket.SetActive(false);
    }
    public void MarketVipMarketAc()
    {
        ToplarMarket.SetActive(false);
        CephaneMarket.SetActive(false);
        VipMarket.SetActive(true);
        PromosyonKoduMarket.SetActive(false);

#if UNITY_ANDROID
        VipmArketSlot10.SetActive(true);
        VipmArketSlot9.SetActive(true);
        VipmArketSlot8.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
            VipmArketSlot10.SetActive(false);
        VipmArketSlot9.SetActive(false);
        VipmArketSlot8.SetActive(false);
#endif
    }
    public void MarketPromosyonKoduMarketAc()
    {
        ToplarMarket.SetActive(false);
        CephaneMarket.SetActive(false);
        VipMarket.SetActive(false);
        PromosyonKoduMarket.SetActive(true);
    }
    public void ProfilAc()
    {
        BenimGemim.GetComponent<Player>().ProfilYukle(BenimGemim.GetComponent<Player>().oyuncuadi);
        Profil.SetActive(true);
        Market.SetActive(false);
        Tershane.SetActive(false);
        SeyirDefteri.SetActive(false);
        Siralama.SetActive(false);
        Ayarlar.SetActive(false);
        YetenekSistemi.SetActive(false);
        FiloAna.SetActive(false);
        Etkinlik.SetActive(false);
    }
    public void ProfilKapa()
    {
        Profil.SetActive(false);
    }
    public void MarketAc()
    {
        Profil.SetActive(false);
        Market.SetActive(true);
        Tershane.SetActive(false);
        SeyirDefteri.SetActive(false);
        Siralama.SetActive(false);
        Ayarlar.SetActive(false);
        YetenekSistemi.SetActive(false);
        FiloAna.SetActive(false);
        Etkinlik.SetActive(false);
    }
    public void MarketKapa()
    {
        Market.SetActive(false);
    }
    public void TershaneAc()
    {
        Profil.SetActive(false);
        Market.SetActive(false);
        Tershane.SetActive(true);
        SeyirDefteri.SetActive(false);
        Siralama.SetActive(false);
        Ayarlar.SetActive(false);
        YetenekSistemi.SetActive(false);
        ElitPuanaGöreGemiKullandir();
        FiloAna.SetActive(false);

    }
    public void TeraneKapa()
    {
        Tershane.SetActive(false);
    }
    public void SeyirDefteriAc()
    {
        Profil.SetActive(false);
        Market.SetActive(false);
        Tershane.SetActive(false);
        SeyirDefteri.SetActive(true);
        Siralama.SetActive(false);
        Ayarlar.SetActive(false);
        YetenekSistemi.SetActive(false);
        FiloAna.SetActive(false);
        Etkinlik.SetActive(false);
    }

    public void SeyirDefteriKapa()
    {
        SeyirDefteri.SetActive(false);
    }
    public void SiralamaAc()
    {
        Profil.SetActive(false);
        Market.SetActive(false);
        Tershane.SetActive(false);
        SeyirDefteri.SetActive(false);
        Siralama.SetActive(true);
        Ayarlar.SetActive(false);
        YetenekSistemi.SetActive(false);
        NormalSirlamaAc();
        FiloAna.SetActive(false);
        Etkinlik.SetActive(false);
    }
    public void SiralamaKapa()
    {
        Siralama.SetActive(false);
    }
    public void AyarlarAc()
    {
        Profil.SetActive(false);
        Market.SetActive(false);
        Tershane.SetActive(false);
        SeyirDefteri.SetActive(false);
        Siralama.SetActive(false);
        Ayarlar.SetActive(true);
        YetenekSistemi.SetActive(false);
        FiloAna.SetActive(false);
        Etkinlik.SetActive(false);
    }
    public void AyarlarKapa()
    {
        Ayarlar.SetActive(false);
    }
    public void YetenekSistemiAc()
    {
        Profil.SetActive(false);
        Market.SetActive(false);
        Tershane.SetActive(false);
        SeyirDefteri.SetActive(false);
        Siralama.SetActive(false);
        Ayarlar.SetActive(false);
        YetenekSistemi.SetActive(true);
        FiloAna.SetActive(false);
        Etkinlik.SetActive(false);

    }
    public void YetenekSistemiKapa()
    {
        YetenekSistemi.SetActive(false);
    }
    public void SozlesmeAc()
    {
        Sozlesme.SetActive(true);
    }
    public void SozlesmeKapa()
    {
        Sozlesme.SetActive(false);
    }
    public void RutbeSirlamasiAc()
    {
        RutbeSiralamsi.SetActive(true);
        NormalSiralama.SetActive(false);
        FiloSiralamasi.SetActive(false);

    }
    public void NormalSirlamaAc()
    {
   
        // 0=tecrubePuani, 1=savasPuani, 2=elitPuan
        // TODO: burada sýralama türüne göre yazý yazdýrýlacak tecrübepuaný vb
        if (SiralamaDropDown.value == 0 )
        {
            FiloSiralamasi.SetActive(false);
            NormalSiralama.SetActive(true);
            RutbeSiralamsi.SetActive(false);
            BenimGemim.GetComponent<Player>().SiralamaYukle(0);
            SirlamaBaslik.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 180];
            SirlamaBaslikAd.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 174];
        }
        else if (SiralamaDropDown.value == 1)
        {
            FiloSiralamasi.SetActive(false);
            NormalSiralama.SetActive(true);
            RutbeSiralamsi.SetActive(false);
            BenimGemim.GetComponent<Player>().SiralamaYukle(1);
            SirlamaBaslik.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 182];
            SirlamaBaslikAd.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 174];
        }
        else if (SiralamaDropDown.value == 2)
        {
            FiloSiralamasi.SetActive(false);
            NormalSiralama.SetActive(true);
            RutbeSiralamsi.SetActive(false);
            BenimGemim.GetComponent<Player>().SiralamaYukle(2);
            SirlamaBaslik.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 183];
            SirlamaBaslikAd.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 174];
        }
        else if (SiralamaDropDown.value == 3)
        {
            FiloSiralamasi.SetActive(true);
            NormalSiralama.SetActive(false);
            RutbeSiralamsi.SetActive(false);
            BenimGemim.GetComponent<Player>().SiralamaYukle(3);
            FiloSirlamaBaslikAd.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 374];
            FiloSirlamaBaslik.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 180];
        }
        else if (SiralamaDropDown.value == 4)
        {
            RutbeSirlamasiAc();
        }
    }

    public void FiloAc()
    {
        if (BenimGemim.GetComponent<Player>().oyuncuFiloId > 0)
        {
            BenimGemim.GetComponent<Player>().FiloBilgileriniCek();
            FiloAna.SetActive(true);
            FiloIcý.SetActive(true);
            FiloKurulacakKisim.SetActive(false);
            Profil.SetActive(false);
            Market.SetActive(false);
            Tershane.SetActive(false);
            SeyirDefteri.SetActive(false);
            Siralama.SetActive(false);
            Ayarlar.SetActive(false);
            YetenekSistemi.SetActive(false);
            Etkinlik.SetActive(false);
            FiloicigenelAc();
        }
        else
        {
            BenimGemim.GetComponent<Player>().FiloSiralamasi();
            FiloKurulacakKisim.SetActive(true);
            FiloAna.SetActive(true);
            FiloIcý.SetActive(false);
            FiloBaslangic.SetActive(true);
            FiloKurma.SetActive(false);
            FiloArama.SetActive(false);
            Profil.SetActive(false);
            Market.SetActive(false);
            Tershane.SetActive(false);
            SeyirDefteri.SetActive(false);
            Siralama.SetActive(false);
            Ayarlar.SetActive(false);
            YetenekSistemi.SetActive(false);
            Etkinlik.SetActive(false);
        }
    }
    public void FiloKapa()
    {
        FiloAna.SetActive(false);
    }
    public void FiloKurAc()
    {
        FiloBaslangic.SetActive(false);
        FiloKurma.SetActive(true);
        FiloArama.SetActive(false);
    }
    public void FiloAraAc()
    {
        FiloBaslangic.SetActive(false);
        FiloKurma.SetActive(false);
        FiloArama.SetActive(true);
    }
    public void FiloKurButton()
    {
        if (BenimGemim.GetComponent<Player>().oyuncuAltin > 1000)
        {
            if (FiloUzunAd.text.Length > 2 && FiloUzunAd.text.Length < 20)
            {
                BenimGemim.GetComponent<Player>().FiloKurmaIstegiYollaSunucuya(FiloUzunAd.text, FiloKisaAd.text, FiloAciklama.text);
            }
            else
            {
                BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 396]);
            }
        }
        else
        {
            BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 395]);
        }
    }
    // market toplam altin hesaplama
    public void MarketToplamAltinHesaplaVeYazdir(int satinAlinacakOgeMarketId)
    {
      //  StartCoroutine(MarketToplamBedelYazdir(satinAlinacakOgeMarketId));
    }
    public void EtkinlikAc()
    {
        if (EtkinlikAktiflikDurumu == true)
        {
            
            Etkinlik.SetActive(true);
            EtkinlikScroll.SetActive(true);
            GorevlerScroll.SetActive(false);
            Profil.SetActive(false);
            YetenekSistemi.SetActive(false);
            Market.SetActive(false);
            Ayarlar.SetActive(false);
            SeyirDefteri.SetActive(false);
            Siralama.SetActive(false);
            FiloAna.SetActive(false);
            BenimGemim.GetComponent<Player>().SiralamaYukle(4);
        }
        else
        {
            Etkinlik.SetActive(true);
            EtkinlikScroll.SetActive(false);
            GorevlerScroll.SetActive(true);
            Profil.SetActive(false);
            YetenekSistemi.SetActive(false);
            Market.SetActive(false);
            Ayarlar.SetActive(false);
            SeyirDefteri.SetActive(false);
            Siralama.SetActive(false);
            FiloAna.SetActive(false);
        }

    }
    public void etkinlikIciEtkinlikPaneliAc()
    {
        EtkinlikScroll.SetActive(true);
        GorevlerScroll.SetActive(false);
    }
    public void etkinlikIciGorevlerPaneliAc()
    {
        EtkinlikScroll.SetActive(false);
        GorevlerScroll.SetActive(true);
    }
    public void EtkinlikKapa()
    {
        Etkinlik.SetActive(false);
    }
    public void FiloiciUyelerAc()
    {
        FiloiciAdalar.SetActive(false);
        FiloiciMuttefik.SetActive(false);
        FiloiciUyeler.SetActive(true);
        FiloiciGenel.SetActive(false);
        if (BenimGemim.GetComponent<Player>().OyuncuYetkiID <= 2)
        {
            BenimGemim.GetComponent<Player>().FiloBasvurulariniGoster();
        }
        BenimGemim.GetComponent<Player>().FiloUyeleriniGoster();
        FiloiciUyelerPaneli.SetActive(true);
    }
    public void FiloicigenelAc()
    {
        FiloiciAdalar.SetActive(false);
        FiloiciMuttefik.SetActive(false);
        FiloiciUyeler.SetActive(false);
        FiloiciGenel.SetActive(true);
        BenimGemim.GetComponent<Player>().FiloBagisCek();
        FiloiciUyelerPaneli.SetActive(true);

    }
    public void FiloiciAdalarAc()
    {
        FiloiciAdalar.SetActive(true);
        FiloiciMuttefik.SetActive(false);
        FiloiciUyeler.SetActive(false);
        FiloiciGenel.SetActive(false);
        FiloiciUyelerPaneli.SetActive(true);
        BenimGemim.GetComponent<Player>().FiloIciAdalariYukle();
    }
    public void FiloiciMuttefikAc()
    {
        FiloiciAdalar.SetActive(false);
        FiloiciMuttefik.SetActive(true);
        FiloiciUyeler.SetActive(false);
        FiloiciGenel.SetActive(false);
        FiloiciUyelerPaneli.SetActive(false);
        BenimGemim.GetComponent<Player>().muttefikSayfasiVerileriniCek();
        BenimGemim.GetComponent<Player>().filoMuttefikCekSunucu();
        BenimGemim.GetComponent<Player>().filoDusmanCekSunucu();
    }
    public void AyarlarGrafikAc()
    {
        AyarlarGrafik.SetActive(true);
        AyarlarDil.SetActive(false);
        AyarlarSes.SetActive(false);
        AyarlarKontrol.SetActive(false);
        AyarlarDestek.SetActive(false);
        AyarlarHesapAyarlari.SetActive(false);
    }
    public void AyarlarSesAc()
    {
        AyarlarGrafik.SetActive(false);
        AyarlarDil.SetActive(false);
        AyarlarSes.SetActive(true);
        AyarlarKontrol.SetActive(false);
        AyarlarDestek.SetActive(false);
        AyarlarHesapAyarlari.SetActive(false);

    }
    public void AyarlarDilAc()
    {
        AyarlarGrafik.SetActive(false);
        AyarlarDil.SetActive(true);
        AyarlarSes.SetActive(false);
        AyarlarKontrol.SetActive(false);
        AyarlarDestek.SetActive(false);
        AyarlarHesapAyarlari.SetActive(false);

    }
    public void AyarlarKontrolAc()
    {
        AyarlarGrafik.SetActive(false);
        AyarlarDil.SetActive(false);
        AyarlarSes.SetActive(false);
        AyarlarKontrol.SetActive(true);
        AyarlarDestek.SetActive(false);
        AyarlarHesapAyarlari.SetActive(false);

    }
    public void AyarlarDestekAc()
    {
        AyarlarGrafik.SetActive(false);
        AyarlarDil.SetActive(false);
        AyarlarSes.SetActive(false);
        AyarlarKontrol.SetActive(false);
        AyarlarDestek.SetActive(true);
        AyarlarHesapAyarlari.SetActive(false);

    }
    public void AyarlarHesapAyarlariAc()
    {
        AyarlarGrafik.SetActive(false);
        AyarlarDil.SetActive(false);
        AyarlarSes.SetActive(false);
        AyarlarKontrol.SetActive(false);
        AyarlarDestek.SetActive(false);
        AyarlarHesapAyarlari.SetActive(true);

        AyarlarIsimDegistermePaneli.SetActive(false);
        AyarlarIsimDegistirBTN.SetActive(true);
        AyarlarSifreDegistirBTN.SetActive(true);
        AyarlarmailDegistirBTN.SetActive(true);
        AyarlarSifreDegistirmePaneli.SetActive(false);
        AyarlarMailDegistirmePaneli.SetActive(false);
    }
    public void AyarlarIsimDegistirmePaneliAc()
    {
        AyarlarIsimDegistermePaneli.SetActive(true);
        AyarlarIsimDegistirBTN.SetActive(false);
        AyarlarSifreDegistirBTN.SetActive(false);
        AyarlarmailDegistirBTN.SetActive(false);
        AyarlarSifreDegistirmePaneli.SetActive(false);
        AyarlarMailDegistirmePaneli.SetActive(false);
    }
    public void AyarlarMailDegistirmePaneliAc()
    {
        AyarlarIsimDegistermePaneli.SetActive(false);
        AyarlarIsimDegistirBTN.SetActive(false);
        AyarlarSifreDegistirBTN.SetActive(false);
        AyarlarmailDegistirBTN.SetActive(false);
        AyarlarSifreDegistirmePaneli.SetActive(false);
        AyarlarMailDegistirmePaneli.SetActive(true);
    }
    public void AyarlarSifreDegistirmePaneliAc()
    {
        AyarlarIsimDegistermePaneli.SetActive(false);
        AyarlarIsimDegistirBTN.SetActive(false);
        AyarlarSifreDegistirBTN.SetActive(false);
        AyarlarmailDegistirBTN.SetActive(false);
        AyarlarSifreDegistirmePaneli.SetActive(true);
        AyarlarMailDegistirmePaneli.SetActive(false);
    }
    public void AyarlarDestekYeniDestekTalebiOlusturPaneliAc()
    {
        AyarlarDestekYeniDestekTalebi.SetActive(true);
        AyarlarDestekTaleplerim.SetActive(false);
        AyarlarMesajlasma.SetActive(false);
    }
    public void AyarlarDestekTalepleriAc()
    {
        AyarlarDestekYeniDestekTalebi.SetActive(false);
        AyarlarDestekTaleplerim.SetActive(true);
        AyarlarMesajlasma.SetActive(false);
        BenimGemim.GetComponent<Player>().SunucuDestekTalepleriniCek();
    }
    public void AyarlarDestekMesajlasmaAc()
    {
        AyarlarDestekYeniDestekTalebi.SetActive(false);
        AyarlarDestekTaleplerim.SetActive(false);
        AyarlarMesajlasma.SetActive(true);
    }
    public bool IsTextInteger(string text)
    {
        return int.TryParse(text, out _);
    }

    public void DestekTalebiDropdownKontrol()
    {
        if (destekTalebiOlusturDropdown.value != 0)
        {
            destekTalebiOlusturBTN.SetActive(true);
            destekTalebiOlusturINPT.SetActive(true);
        }
        else 
        {
            destekTalebiOlusturBTN.SetActive(false);
            destekTalebiOlusturINPT.SetActive(false);
        }
    }
    public void destekTalebiGonderButton()
    {
        if (destekTalebiOlusturINPT.GetComponent<InputField>().text !="")
        {
            BenimGemim.GetComponent<Player>().SunucudestekTalebiGonder(destekTalebiOlusturDropdown.value, destekTalebiOlusturINPT.GetComponent<InputField>().text);
        }
        else
        {
            BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 260]);
        }
    }

    /*  IEnumerator MarketToplamBedelYazdir(int SatinAlinacakObje)
      {
          if(int.Parse(MiktarAdeti[SatinAlinacakObje].text) > 0)
          {
              yield return new WaitForSeconds(0.01f);
              switch (SatinAlinacakObje)
              {
                  case 0:
                      if((int.Parse(MiktarAdeti[SatinAlinacakObje].text) * 400) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti[SatinAlinacakObje].text) * 400).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "YirmiBesKilolukTop":
                      if ((int.Parse(MiktarAdeti.text) * 1500) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 1500).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "YirmiYediBucukKilolukTop":
                      if ((int.Parse(MiktarAdeti.text) * 2500) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 2500).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "OtuzKilolukTop":
                      if ((int.Parse(MiktarAdeti.text) * 1000) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 1000).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "OtuzBesKilolukTop":
                      if ((int.Parse(MiktarAdeti.text) * 5000) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 5000).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "TasGulle":
                      if ((int.Parse(MiktarAdeti.text) * 1) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 1).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "DemirGulle":
                      if ((int.Parse(MiktarAdeti.text) * 10) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 10).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "AlevGulle":
                      if ((int.Parse(MiktarAdeti.text) * 1) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 1).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "SifaGulle":
                      if ((int.Parse(MiktarAdeti.text) * 1) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 1).ToString();
                      }
                      break;
                  case "HavaiFisek":
                      if ((int.Parse(MiktarAdeti.text) * 2) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 2).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "Roket":
                      if ((int.Parse(MiktarAdeti.text) * 500) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 500).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "Hýz":
                      if ((int.Parse(MiktarAdeti.text) * 13) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 13).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "Barut":
                      if ((int.Parse(MiktarAdeti.text) * 150) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 150).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "Kalkan":
                      if ((int.Parse(MiktarAdeti.text) * 500) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 500).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "PaslanmisZipkin":
                      if ((int.Parse(MiktarAdeti.text) * 100) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 100).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "GumusZipkin":
                      if ((int.Parse(MiktarAdeti.text) * 19) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 19).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
                  case "AltinZipkin":
                      if ((int.Parse(MiktarAdeti.text) * 25) > 0)
                      {
                          MarketFiyatText.text = (int.Parse(MiktarAdeti.text) * 25).ToString();
                      }
                      else
                      {
                          MarketFiyatText.text = "0";
                          MiktarAdeti.text = "0";
                      }
                      break;
              }
          }
         else
          {
              MiktarAdeti.text = "0";
          }
      }*/

    public void GemiKullanButton(int gemiID)
    {
        if (BenimGemim.GetComponent<Player>().Can == BenimGemim.GetComponent<Player>().MaksCan)
        {
            BenimGemim.GetComponent<Player>().SunucudaGemiDegistir(gemiID);
            BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 257], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 261]);
        }
        else
        {
            BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 262]);
           
        }

    }

    public void HaritaAtlaButton()
    {
        Kamera.GetComponent<DragCamera2D>().followTarget = null;
        BenimGemim.GetComponent<Player>().HaritaAtlamaIstegiSunucu(0);
    }

    public void HaritaAtlaBaskinHaritasiPVEButton()
    {
            Kamera.GetComponent<DragCamera2D>().followTarget = null;
            BenimGemim.GetComponent<Player>().HaritaAtlamaIstegiSunucu(1);
           
    }
    public void HaritaAtlaBaskinHaritasiPVPButton()
    {
            Kamera.GetComponent<DragCamera2D>().followTarget = null;
            BenimGemim.GetComponent<Player>().HaritaAtlamaIstegiSunucu(2);
           
    }

    public void HaritaAtlaBaskinCikHaritasiPVEButton()
    {
        Kamera.GetComponent<DragCamera2D>().followTarget = null;
        BenimGemim.GetComponent<Player>().HaritaAtlamaIstegiSunucu(3);
      
    }
    public void HaritaAtlaBaskinCikHaritasiPVPButton()
    {
        Kamera.GetComponent<DragCamera2D>().followTarget = null;
        BenimGemim.GetComponent<Player>().HaritaAtlamaIstegiSunucu(3);
      

    }

    public void OdulYazisiSayacBaslat()
    {
        StartCoroutine(BenimGemim.GetComponent<Player>().OduluEkrandaYazdir());
    }

    public int OyuncuSeviyesiHesapla(int tecrubePuani)
    {
        if (tecrubePuani < 60)
        {
            if(!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 60;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "60";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "1";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 60;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "60";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "1";
#endif
            }
            return 1;
        }
        else if (tecrubePuani < 400)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 400;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "400";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "2";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 400;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "400";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "2";
#endif
            }
            return 2;
        }
        else if (tecrubePuani < 3000)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 3000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "3000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "3";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 3000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "3000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "3";
#endif
            }
            return 3;
        }
        else if (tecrubePuani < 8000)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 8000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "8000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "4";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 8000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "8000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "4";
#endif
            }
            return 4;
        }
        else if (tecrubePuani < 25000)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 25000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "25000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "5";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 25000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "25000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "5";
#endif
            }
            return 5;
        }
        else if (tecrubePuani < 50000)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 50000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "50000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "6";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 50000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "50000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "6";
#endif
            }
            return 6;
        }
        else if (tecrubePuani < 100000)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 100000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "100000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "7";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 100000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "100000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "7";
#endif
            }
            return 7;
        }
        else if (tecrubePuani < 300000)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 300000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "300000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "8";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 300000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "300000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "8";
#endif
            }
            return 8;
        }
        else if (tecrubePuani < 600000)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 600000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "600000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "9";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 600000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "600000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "9";
#endif
            }
            return 9;
        }
        else if (tecrubePuani < 1200000)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 1200000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "1200000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "10";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 1200000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "1200000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "10";
#endif
            }
            return 10;
        }
        else if (tecrubePuani >= 1200000)
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 1200000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "1200000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "10";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 1200000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "1200000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "10";
#endif
            }
            return 10;
        }
        else
        {
            if (!isServerOnly)
            {
#if UNITY_ANDROID
            slider.maxValue = 2000000;
            oyuncuTecrubePuaniText.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "2000000";
            slider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
            OyuncuSeviye.text = "1";
#endif
#if UNITY_STANDALONE_WIN
                WinTecrubePuaniSlider.maxValue = 2000000;
                WinTecrubePuaniTxt.text = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan.ToString() + "/" + "2000000";
                WinTecrubePuaniSlider.value = BenimGemim.GetComponent<Player>().oyuncuTecrubePuan;
                WinLvlTXT.text = "1";
#endif
            }
            return 1;
        }
    }
    public void BarutAcKapatButton()
    {
        BenimGemim.GetComponent<Player>().OyuncuBarutAcKapat();
    }
    public void KalkanAcKapatButton()
    {
        BenimGemim.GetComponent<Player>().OyuncuKalkanAcKapat();
    }
    public void RoketAcKapatButton()
    {
        BenimGemim.GetComponent<Player>().OyuncuRoketAcKapat();
    }
    public void HizTasiButton()
    {
        if (BenimGemim.GetComponent<Player>().oyuncuHizTasi > 0)
        {
            BenimGemim.GetComponent<Player>().SunucuHizTasiKullan();
        }
    }

    public string SayiKisaltici(int kisaltilacakSayi)
    {
        if (kisaltilacakSayi < 10000)
        {
            return kisaltilacakSayi.ToString();
        }
        else if (kisaltilacakSayi < 1000000)
        {
            return (kisaltilacakSayi / 1000) + "K";
        }
        else if (kisaltilacakSayi < 1000000000)
        {
            return (kisaltilacakSayi / 1000000) + "M";
        }
        else
        {
            return "0";
        }
    }

    public void PromosyonKoduSunucuyaYollaButton(Text yollanacakPromosyonKodu)
    {
        if (BenimGemim.GetComponent<Player>().seviye >= 3)
        {
            BenimGemim.GetComponent<Player>().PromosyonKoduSunucuyaYolla(yollanacakPromosyonKodu.text);
        }
    }

    public void BildirimOlustur(string baslik, string mesaj)
    {
        BildirimPaneliBaslikText.text = baslik;
        BildirimPaneliMesajText.text = mesaj;
        BildirimPaneli.SetActive(true);
    }

    public int FiloSeviyesiHesapla(int tecrubePuani)
    {
        if (tecrubePuani < 15000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 60000;
            FiloÝciFiloLeveliTXT.text = "1";
            FiloÝciFiloSonrakiLeveliTXT.text = "2";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString() + " / 60000";
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/50";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 50;
#endif
            return 1;
        }

        else if (tecrubePuani < 60000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 105000;
            FiloÝciFiloLeveliTXT.text = "2";
            FiloÝciFiloSonrakiLeveliTXT.text = "3";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString() + " / 105000";
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/51";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 51;
#endif
            return 2;
        }
        else if (tecrubePuani < 105000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 180000;
            FiloÝciFiloLeveliTXT.text = "3";
            FiloÝciFiloSonrakiLeveliTXT.text = "4";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString() + " / 180000";
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/52";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 52;

#endif
            return 3;
        }
        else if (tecrubePuani < 180000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 360000;
            FiloÝciFiloLeveliTXT.text = "4";
            FiloÝciFiloSonrakiLeveliTXT.text = "5";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString() + " / 360000";
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/53";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 53;

#endif
            return 4;
        }
        else if (tecrubePuani < 360000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 720000;
            FiloÝciFiloLeveliTXT.text = "5";
            FiloÝciFiloSonrakiLeveliTXT.text = "6";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString() + " / 720000";
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/54";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 54;

#endif
            return 5;

        }
        else if (tecrubePuani < 720000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 1500000;
            FiloÝciFiloLeveliTXT.text = "6";
            FiloÝciFiloSonrakiLeveliTXT.text = "7";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString() + " / 1500000";
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/55";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 55;

#endif
            return 6;
        }
        else if (tecrubePuani < 1500000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 3100000;
            FiloÝciFiloLeveliTXT.text = "7";
            FiloÝciFiloSonrakiLeveliTXT.text = "8";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString() + " / 3100000";
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/56";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 56;

#endif
            return 7;
        }
        else if (tecrubePuani < 3100000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 1500000;
            FiloÝciFiloLeveliTXT.text = "8";
            FiloÝciFiloSonrakiLeveliTXT.text = "9";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString() + " / 1500000";
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/57";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 57;

#endif
            return 8;
        }
        else if (tecrubePuani < 6200000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 6200000;
            FiloÝciFiloLeveliTXT.text = "9";
            FiloÝciFiloSonrakiLeveliTXT.text = "10";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString() + " / 6200000";
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/58";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 58;

#endif
            return 9;
        }
        else if (tecrubePuani >= 6200000)
        {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            FiloSlider.maxValue = 6200000;
            FiloÝciFiloLeveliTXT.text = "10";
            FiloÝciFiloSonrakiLeveliTXT.text = "";
            FiloSlider.value = tecrubePuani;
            FiloSliderTpTXT.text = tecrubePuani.ToString();
            MaksimumUyesayisi.text = dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 397] + " " + BenimGemim.GetComponent<Player>().filoUyeSayisi + "/59";
            BenimGemim.GetComponent<Player>().maxFiloUyeSayisi = 58;

#endif
            return 10;
        }
        else
        {
            return 1;
        }
    }

    public void FiloAraButton()
    {
        if (FiloKisaltmasiTXTArama.text.Length > 0)
        {
            BenimGemim.GetComponent<Player>().FiloAramaIstegiYolla(FiloKisaltmasiTXTArama.text, 0);
        }
        else if (FiloAdiTXTArama.text.Length > 0)
        {
            BenimGemim.GetComponent<Player>().FiloAramaIstegiYolla(FiloAdiTXTArama.text, 1);
        }
        else
        {
            BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 263]);
        }
    }
    public void FiloBasvur(int FiloID)
    {
        BenimGemim.GetComponent<Player>().FiloBasvuruAtmaIstegiYolla(FiloID);
    }
    public IEnumerator OyundanCikis()
    {
        BenimGemim.GetComponent<Player>().target = BenimGemim.transform.position;

        OyundanCýkmaSiyahEkraný.SetActive(true);
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "10";
            yield return new WaitForSeconds(1f);
        }
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "9";
            yield return new WaitForSeconds(1f);
        }
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "8";
            yield return new WaitForSeconds(1f);
        }
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "7";
            yield return new WaitForSeconds(1f);
        }
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "6";
            yield return new WaitForSeconds(1f);
        }
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "5";
            yield return new WaitForSeconds(1f);
        }
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "4";
            yield return new WaitForSeconds(1f);
        }
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "3";
            yield return new WaitForSeconds(1f);
        }
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "2";
            yield return new WaitForSeconds(1f);
        }
        if (oyundancikiscancel == false)
        {
            OyundanCýkmaSiyahEkranýTXT.text = "1";
            yield return new WaitForSeconds(1f);
            BenimGemim.GetComponent<Player>().OyuncuOyunuKapatmaDonusSunucuya();
        }
        else
        {
            OyundanCýkmaSiyahEkraný.SetActive(false);
        }
    }

    public void oyundancikisiiptalet()
    {
        oyundancikiscancel = true;
    }
    public void OyundanCikButton()
    {
        oyundancikiscancel = false;
        StartCoroutine(OyundanCikis());
    }
    public void FiloyaBagisYap()
    {
        if (  FiloyaBagisAltinTXT.text.Length > 0 && int.Parse(FiloyaBagisAltinTXT.text) > 0) 
        {
            BenimGemim.GetComponent<Player>().FiloyaBagisYap(int.Parse(FiloyaBagisAltinTXT.text));
        }
       
    }
    public void FilodanCik()
    {
        BenimGemim.GetComponent<Player>().FilodanCikma();
    }
    public void FilodanOyuncuAt(string oyuncuAd)
    {
        BenimGemim.GetComponent<Player>().FilodanOyuncuAt(oyuncuAd);
    }
    public void FiloyaBasvuranOyuncuyuKabulEt(string oyuncuAd)
    {
        BenimGemim.GetComponent<Player>().FiloyaBasvuranOyuncuyuFiloyaEkle(oyuncuAd);
    }
    public void FiloyaBasvuranOyuncuRedEt(string oyuncuAd)
    {
        BenimGemim.GetComponent<Player>().FiloyaBasvuranOyuncuyuReddet(oyuncuAd);
    }
    public void FiloAciklamasiniDegistir()
    {
        BenimGemim.GetComponent<Player>().FiloAciklamasiniDegistir(FiloiciGenelBakisAciklama.text);
    }
    public void FiloAyarlariIconSec(int SeciliIcon)
    {
        for (int i = 0; i < 26; i++)
        {
            if (SeciliIcon == i)
            {
                IconProfilIMG.sprite = FiloÝconlarý[i];
            }
        }
    }
#if UNITY_STANDALONE_WIN
    public void BasilanTuslariKontrol()
    {
        // TODO chat aktifse çalýþmamasý yapýlacak
        if (!GameManager.gm.FiloAna.activeSelf && !Etkinlik.activeSelf && !winchatinputfield.isFocused && !Profil.activeSelf && !Market.activeSelf && !Tershane.activeSelf && !SeyirDefteri.activeSelf && !Siralama.activeSelf && !Ayarlar.activeSelf && !YetenekSistemi.activeSelf )
        {
            if (Input.GetKeyDown(saldirkeykodu))
            {
                Saldir();
            }
            else if (Input.GetKeyDown(saldirdurdurkeykod))
            {
                SaldiriDurdur();
            }
            else if (Input.GetKeyDown(gemiyekilitlenkeykod))
            {
                if (Kamera.GetComponent<DragCamera2D>().followTarget == null)
                {
                    Kamera.GetComponent<DragCamera2D>().followTarget = BenimGemim;
                    WindowsDumen.SetActive(false);
                }
                else
                {
                    Kamera.GetComponent<DragCamera2D>().followTarget = null;
                }
            }
            else if (Input.GetKeyDown(roketAtKeyKod))
            {
                RoketAcKapatButton();
            }
            else if (Input.GetKeyDown(barutAktifYapkeykod))
            {
                BarutAcKapatButton();
            }
            else if (Input.GetKeyDown(kalkanAktifYapkeykod))
            {
                KalkanAcKapatButton();
            }
            else if (Input.GetKeyDown(hiztasikeykod))
            {
                HizTasiButton();
            }
            else if (Input.GetKeyDown(haritaatlakeykod))
            {
                HaritaAtlaButton();
            }
            else if (Input.GetKeyDown(tamirolkeykod))
            {
                tamirButton();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                winchatinputfield.Select();
                winchatinputfield.ActivateInputField();
            }
        }
    }
#endif
    public IEnumerator GemiYokEt(GameObject hedef)
    {
        hedef.transform.Find("OlmeAnimasyon").gameObject.SetActive(true);
        hedef.transform.Find("OyuncuAdi").gameObject.SetActive(false);
        hedef.transform.Find("OyuncuCanvas").gameObject.SetActive(false);
        Color spriteColor = hedef.transform.Find("Gemi").GetComponent<SpriteRenderer>().color;
        spriteColor.a = 0f;
        hedef.transform.Find("Gemi").GetComponent<SpriteRenderer>().color = spriteColor;
        hedef.transform.Find("MiniMapIcon").gameObject.SetActive(false);
        yield return null;
    }

    public void SesDegistirmeKontrol()
    {
        if (SesSlider.value == 0)
        {
            SesKapaliIMG.SetActive(true);
            GemiGulleAtmaSesi.volume = 0;
            GemiZipkinAtmaSesi.volume = 0;
        }
        else if (SesSlider.value > 0)
        {
            SesKapaliIMG.SetActive(false);
            GemiGulleAtmaSesi.volume = SesSlider.value;
            GemiZipkinAtmaSesi.volume = SesSlider.value;
            PlayerPrefs.SetFloat("audioVolumeGulle", GemiGulleAtmaSesi.volume);
        }
    }

#if UNITY_STANDALONE_WIN

    public void KisayolTusuAta(int id)
    {
        tusisteniyor = true;
        istenentusid = id;
    }
    public void KisayolTusuAtamaKapat()
    {
        tusisteniyor = false;
        istenentusid = -1;
    }

    public void TusAtama()
    {
        if (tusisteniyor && Input.inputString.Length > 0)
        {
            if (istenentusid == 0)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {

                    if (Input.GetKey(vKey) && vKey != saldirkeykodu && vKey != saldirdurdurkeykod && vKey != roketAtKeyKod && vKey != gemiyekilitlenkeykod && vKey != barutAktifYapkeykod && vKey != kalkanAktifYapkeykod && vKey != hiztasikeykod && vKey != haritaatlakeykod && vKey != tamirolkeykod)
                    {
                        saldirkeykodu = vKey;
                        SaldirButtontext.text = vKey.ToString();
                        PlayerPrefs.SetString("saldirkeykod", vKey.ToString());
                    }
                }

            }
            if (istenentusid == 1)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey) && vKey != saldirkeykodu && vKey != saldirdurdurkeykod && vKey != roketAtKeyKod && vKey != gemiyekilitlenkeykod && vKey != barutAktifYapkeykod && vKey != kalkanAktifYapkeykod && vKey != hiztasikeykod && vKey != haritaatlakeykod && vKey != tamirolkeykod)
                    {
                        saldirdurdurkeykod = vKey;
                        SaldirDurdurButtontext.text = vKey.ToString();
                        PlayerPrefs.SetString("saldiridurdurkeykod", vKey.ToString());
                    }
                }
            }
            if (istenentusid == 2)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey) && vKey != saldirkeykodu && vKey != saldirdurdurkeykod && vKey != roketAtKeyKod && vKey != gemiyekilitlenkeykod && vKey != barutAktifYapkeykod && vKey != kalkanAktifYapkeykod && vKey != hiztasikeykod && vKey != haritaatlakeykod && vKey != tamirolkeykod)
                    {
                        roketAtKeyKod = vKey;
                        RoketAtButtontext.text = vKey.ToString();
                        PlayerPrefs.SetString("roketatkeykod", vKey.ToString());
                    }
                }
            }
            if (istenentusid == 3)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey) && vKey != saldirkeykodu && vKey != saldirdurdurkeykod && vKey != roketAtKeyKod && vKey != gemiyekilitlenkeykod && vKey != barutAktifYapkeykod && vKey != kalkanAktifYapkeykod && vKey != hiztasikeykod && vKey != haritaatlakeykod && vKey != tamirolkeykod)
                    {
                        gemiyekilitlenkeykod = vKey;
                        GemiyeKilitlenButtontext.text = vKey.ToString();
                        PlayerPrefs.SetString("gemiyekilitlenkeykod", vKey.ToString());

                    }
                }
            }
            if (istenentusid == 4)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey) && vKey != saldirkeykodu && vKey != saldirdurdurkeykod && vKey != roketAtKeyKod && vKey != gemiyekilitlenkeykod && vKey != barutAktifYapkeykod && vKey != kalkanAktifYapkeykod && vKey != hiztasikeykod && vKey != haritaatlakeykod && vKey != tamirolkeykod)
                    {
                        haritaatlakeykod = vKey;
                        haritaatlakeykodtext.text = vKey.ToString();
                        PlayerPrefs.SetString("haritaatlakeykod", vKey.ToString());
                    }
                }
            }
            if (istenentusid == 5)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey) && vKey != saldirkeykodu && vKey != saldirdurdurkeykod && vKey != roketAtKeyKod && vKey != gemiyekilitlenkeykod && vKey != barutAktifYapkeykod && vKey != kalkanAktifYapkeykod && vKey != hiztasikeykod && vKey != haritaatlakeykod && vKey != tamirolkeykod)
                    {
                        hiztasikeykod = vKey;
                        hiztasikeykodtext.text = vKey.ToString();
                        PlayerPrefs.SetString("hiztasikeykod", vKey.ToString());
                    }
                }
            }
            if (istenentusid == 6)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey) && vKey != saldirkeykodu && vKey != saldirdurdurkeykod && vKey != roketAtKeyKod && vKey != gemiyekilitlenkeykod && vKey != barutAktifYapkeykod && vKey != kalkanAktifYapkeykod && vKey != hiztasikeykod && vKey != haritaatlakeykod && vKey != tamirolkeykod)
                    {
                        barutAktifYapkeykod = vKey;
                        barutAktifKeyKodText.text = vKey.ToString();
                        PlayerPrefs.SetString("barutAktifYapkeykod", vKey.ToString());
                    }
                }
            }
            if (istenentusid == 7)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey) && vKey != saldirkeykodu && vKey != saldirdurdurkeykod && vKey != roketAtKeyKod && vKey != gemiyekilitlenkeykod && vKey != barutAktifYapkeykod && vKey != kalkanAktifYapkeykod && vKey != hiztasikeykod && vKey != haritaatlakeykod && vKey != tamirolkeykod)
                    {
                        kalkanAktifYapkeykod = vKey;
                        KalkanAktifKeyKodText.text = vKey.ToString();
                        PlayerPrefs.SetString("kalkanAktifYapkeykod", vKey.ToString());
                    }
                }
            }
            if (istenentusid == 8)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey) && vKey != saldirkeykodu && vKey != saldirdurdurkeykod && vKey != roketAtKeyKod && vKey != gemiyekilitlenkeykod && vKey != barutAktifYapkeykod && vKey != kalkanAktifYapkeykod && vKey != hiztasikeykod && vKey != haritaatlakeykod && vKey != tamirolkeykod)
                    {
                        tamirolkeykod = vKey;
                        tamirolkeykodtext.text = vKey.ToString();
                        PlayerPrefs.SetString("TamirAktifYapkeykod", vKey.ToString());
                    }
                }
            }
            tusisteniyor = false;
        }
    }

    private void TuslariYukle()
    {
       
            if (PlayerPrefs.GetString("saldirkeykod").Length > 0)
            {
                saldirkeykodu = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("saldirkeykod"));
                SaldirButtontext.text = saldirkeykodu.ToString();
            }
            else
            {
                PlayerPrefs.SetString("saldirkeykod", "Q");
                SaldirButtontext.text = PlayerPrefs.GetString("saldirkeykod").ToString();
            }
            if (PlayerPrefs.GetString("saldiridurdurkeykod").Length > 0)
            {
                saldirdurdurkeykod = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("saldiridurdurkeykod"));
                SaldirDurdurButtontext.text = saldirdurdurkeykod.ToString();
            }
            else
            {
                PlayerPrefs.SetString("saldiridurdurkeykod", "E");
                SaldirDurdurButtontext.text = PlayerPrefs.GetString("saldiridurdurkeykod").ToString();
            }
            if (PlayerPrefs.GetString("gemiyekilitlenkeykod").Length > 0)
            {
                gemiyekilitlenkeykod = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("gemiyekilitlenkeykod"));
                GemiyeKilitlenButtontext.text = gemiyekilitlenkeykod.ToString();
            }
            else
            {
                PlayerPrefs.SetString("gemiyekilitlenkeykod", "Space");
                GemiyeKilitlenButtontext.text = PlayerPrefs.GetString("gemiyekilitlenkeykod").ToString();
            }
            if (PlayerPrefs.GetString("roketatkeykod").Length > 0)
            {
                roketAtKeyKod = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("roketatkeykod"));
                RoketAtButtontext.text = roketAtKeyKod.ToString();
            }
            else
            {
                PlayerPrefs.SetString("roketatkeykod", "F");
                RoketAtButtontext.text = PlayerPrefs.GetString("roketatkeykod").ToString();
            }
            if (PlayerPrefs.GetString("barutAktifYapkeykod").Length > 0)
            {
                barutAktifYapkeykod = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("barutAktifYapkeykod"));
                barutAktifKeyKodText.text = barutAktifYapkeykod.ToString();
            }
            else
            {
                PlayerPrefs.SetString("barutAktifYapkeykod", "Z");
                barutAktifKeyKodText.text = PlayerPrefs.GetString("barutAktifYapkeykod").ToString();
            }
            if (PlayerPrefs.GetString("kalkanAktifYapkeykod").Length > 0)
            {
                kalkanAktifYapkeykod = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("kalkanAktifYapkeykod"));
                KalkanAktifKeyKodText.text = kalkanAktifYapkeykod.ToString();
            }
            else
            {
                PlayerPrefs.SetString("kalkanAktifYapkeykod", "X");
                KalkanAktifKeyKodText.text = PlayerPrefs.GetString("kalkanAktifYapkeykod").ToString();
            }
            if (PlayerPrefs.GetString("hiztasikeykod").Length > 0)
            {
                hiztasikeykod = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("hiztasikeykod"));
                hiztasikeykodtext.text = hiztasikeykod.ToString();
            }
            else
            {
                PlayerPrefs.SetString("hiztasikeykod", "C");
                hiztasikeykodtext.text = PlayerPrefs.GetString("hiztasikeykod").ToString();
            }
            if (PlayerPrefs.GetString("haritaatlakeykod").Length > 0)
            {
                haritaatlakeykod = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("haritaatlakeykod"));
                haritaatlakeykodtext.text = haritaatlakeykod.ToString();
            }
            else
            {
                PlayerPrefs.SetString("haritaatlakeykod", "V");
                haritaatlakeykodtext.text = PlayerPrefs.GetString("haritaatlakeykod").ToString();
            }
            if (PlayerPrefs.GetString("TamirAktifYapkeykod").Length > 0)
            {
                tamirolkeykod = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("TamirAktifYapkeykod"));
                tamirolkeykodtext.text = tamirolkeykod.ToString();
            }
            else
            {
                PlayerPrefs.SetString("TamirAktifYapkeykod", "T");
                tamirolkeykodtext.text = PlayerPrefs.GetString("TamirAktifYapkeykod").ToString();
            }

    }
#endif

    public void YenidenDog()
    {
        BenimGemim.GetComponent<Player>().SunucuOyuncuyuYenidenDogur();
    }

#if UNITY_SERVER || UNITY_EDITOR
    void WebSitesiOdemeKontrol()
    {
        bool odemeYapilmasiGerekenOyuncuVar = false;
        int oyuncuId = -1;
        int paytrId = -1;
        int yuklenecekLostCoinMiktari = 0;
        string oyuncuAdi = "-1";
        // Veritabaný baðlantýsý oluþtur
        connectionString = "Server=" + server + ";Database=" + database + ";Uid=" + uid + ";Pwd=" + password + ";";
        connection = new MySqlConnection(connectionString);
        try
        {
            // Veritabanýna baðlan
            connection.Open();
            string selectQuery = "SELECT * FROM `balances` limit 1";
            MySqlCommand cmd = new MySqlCommand(selectQuery, connection);

            // Verileri okumak için bir veri okuyucu (DataReader) oluþtur
            MySqlDataReader dataReader = cmd.ExecuteReader();

            // Verileri oku ve konsola yazdýr
            if (dataReader.Read())
            {
                // eger birine lostcoin verilmesi gerekiyorsa
                yuklenecekLostCoinMiktari = dataReader.GetInt32("balance");
                oyuncuId = dataReader.GetInt32("user_id");
                paytrId = dataReader.GetInt32("paytr_id");
                odemeYapilmasiGerekenOyuncuVar = true;
            }

            // Ýlk veri okuyucuyu kapat
            dataReader.Close();

            if (odemeYapilmasiGerekenOyuncuVar)
            {
                string selectQuery2 = "SELECT * FROM `users` where user_id = " + oyuncuId;
                MySqlCommand cmd2 = new MySqlCommand(selectQuery2, connection);

                // Verileri okumak için bir veri okuyucu (DataReader) oluþtur
                MySqlDataReader dataReader2 = cmd2.ExecuteReader();

                // Verileri oku ve konsola yazdýr
                if (dataReader2.Read())
                {
                    oyuncuAdi = dataReader2.GetString("user_username");

                    dataReader2.Close(); // Ýkinci veri okuyucuyu kapat

                    string deleteQuery = "DELETE FROM `balances` WHERE paytr_id = " + paytrId;
                    MySqlCommand cmd3 = new MySqlCommand(deleteQuery, connection);
                    int rowsAffected = cmd3.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        PaketYukle(oyuncuAdi, yuklenecekLostCoinMiktari, paytrId, oyuncuId);
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Veritabaný baðlantýsý baþarýsýz! Hata: " + ex.Message);
        }
        finally
        {
            // Baðlantýyý kapat
            connection.Close();
        }
    }

    public void PaketYukle(string kullaniciAdi, int yuklenecekLostCoinMiktari, int paytrId, int oyuncuId)
    {
        int yeniYuklenecekCoinMiktari = yuklenecekLostCoinMiktari;
        if (ikiyeKatlamaAktiflikDurumu)
        {
            yeniYuklenecekCoinMiktari = yuklenecekLostCoinMiktari * 2;
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Kullanici SET LostCoin = LostCoin + " + yeniYuklenecekCoinMiktari + " WHERE Kullanici_Adi=@kullaniciAdi;";
            command.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            if (command.ExecuteNonQuery() == 1)
            {
                if (GameObject.Find(kullaniciAdi))
                {
                    Player player = GameObject.Find(kullaniciAdi).GetComponent<Player>();
                    player.SetOyuncuLostCoin(player.GetComponent<Player>().oyuncuLostCoin + yeniYuklenecekCoinMiktari);
                }
            }
            command.CommandText = "INSERT INTO WebSitesiLostCoinOdemeleri (paytrId,oyuncuId,yuklenecekLostCoinMiktari,tarih) VALUES (@paytrId,@oyuncuId,@yuklenecekLostCoinMiktari,@now);";
            command.Parameters.AddWithValue("@paytrId", paytrId);
            command.Parameters.AddWithValue("@yuklenecekLostCoinMiktari", yeniYuklenecekCoinMiktari);
            command.Parameters.AddWithValue("@oyuncuId", oyuncuId);
            command.Parameters.AddWithValue("@now", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            command.ExecuteNonQuery();
        }
    }

    private void SistemMesajiYolla()
    {
        SistemSohbetUyarýYolla("", "", "", -142);
    }

    private void EtkinlikBossMesaji(int harita, string bossAdi)
    {
        oyuncularadonensohbetveri("<color=#ffff00>Admiral " + bossAdi + ": Map " + harita + "</color>", "", "", -1);
    }

    private void AdmiralMesaji(int dogacakHarita)
    {
        oyuncularadonensohbetveri("<color=#ffff00>HARRRR </color>", "Bachelor’s Delight Map:"+dogacakHarita, "", -1);
    }
#endif

    [ClientRpc]
    public void SistemSohbetUyarýYolla(string message, string kullaniciadi, string oyuncufilokislaltma, int oyuncuýd)
    {
        // Merhaba korsan! Bu platform üzerinden yeni filolar keþfedebilir ya da filon için yeni üyeler bulma konusunda adýmlar atabilirsin.
        // Tehdit edici, küfürlü, kýþkýrtýcý, dini, siyasi veya ýrkçý içerikli kelimeler kullanmanýn yasak olduðunu unutmayýn.
#if UNITY_STANDALONE_WIN
        if (kullaniciadi != "")
        {
            if (oyuncuýd == 16 || oyuncuýd == 17)
            {
                if (oyuncufilokislaltma != "")
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#ffff00>[" + oyuncufilokislaltma + "]</color>" + "<color=#ffff00>[" + kullaniciadi + "]</color>" + ": " + "<color=#00FF1D>" + message + "</color>");
                }
                else
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#ffff00>[" + kullaniciadi + "]</color>" + ": " + "<color=#00FF1D>" + message + "</color>");
                }
            }
            else
            {
                if (oyuncufilokislaltma != "")
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#ffff00>[" + oyuncufilokislaltma + "]</color>" + kullaniciadi + ": " + message);
                }
                else
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + kullaniciadi + ": " + message);
                }
            }
        }
        else
        {
            if (oyuncuýd == 16 || oyuncuýd == 17)
            {
                if (oyuncufilokislaltma != "")
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>[" + oyuncufilokislaltma + "]</color>" + "<color=#00FF1D>" + message + "</color>");
                }
                else
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>" + message + "</color>");
                }
            }
            else
            {
                if (oyuncufilokislaltma != "")
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>[" + oyuncufilokislaltma + "]</color>" + message);
                }
                else
                {
                    Chat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + message);
                }
            }
        }
#endif
#if UNITY_ANDROID
        if (kullaniciadi.Length > 0)
        {
            if (oyuncuýd == 16 || oyuncuýd == 17)
            {
                if (oyuncufilokislaltma != "")
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#B44800>[" + oyuncufilokislaltma + "]</color>" + "<color=#ffff00>[" + kullaniciadi + "]</color>" + ": " + "<color=#00FF1D>" + message + "</color>");
                }
                else
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#ffff00>[" + kullaniciadi + "]</color>" + ": " + "<color=#00FF1D>" + message + "</color>");
                }
            }
            else
            {
                if (oyuncufilokislaltma.Length > 0)
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#B44800>[" + oyuncufilokislaltma + "]</color>" + kullaniciadi + ": " + message);
                }   
                else
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + kullaniciadi + ": " + message);
                }
            }
        }
        else
        {
            if (oyuncuýd == 16 || oyuncuýd == 17)
            {
                if (oyuncufilokislaltma.Length > 0)
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#B44800>[" + oyuncufilokislaltma + "]</color>" + "<color=#00FF1D>" + message + "</color>");
                }
                else
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>" + message + "</color>");
                }
            }
            else
            {
                if (oyuncufilokislaltma.Length > 0)
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + "<color=#00FF1D>[" + oyuncufilokislaltma + "]</color>" + message);
                }
                else
                {
                    mobilChat.GetComponent<Chat>().AddMessageToChat(System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + " - " + message);
                }
            }
        }
#endif
    }

    public void GunArttir()
    {
        SunucuZamani = SunucuZamani.AddDays(1);
        SunucuZamaniText.text = SunucuZamani.ToString("dd/MM/yyyy");
#if UNITY_SERVER || UNITY_EDITOR
        if (isServerOnly)
        {
            using (var command2 = sqliteConnection.CreateCommand())
            {
                command2.CommandText = "UPDATE Sunucu SET Sunucu = @dunyazamani WHERE id = 1";
                command2.Parameters.AddWithValue("@dunyazamani", SunucuZamani);
                command2.ExecuteNonQuery();
            }
        }
#endif
    }

    public void DestekMesajGonderButton()
    {
        if (destekTalebiMesajINPT.GetComponent<InputField>().text.Length > 0)
        {
            BenimGemim.GetComponent<Player>().SunucudestekMesajGonder(destekNo, destekTalebiMesajINPT.GetComponent<InputField>().text);
        }
    }
    public void destekKilitleButton()
    {
        BenimGemim.GetComponent<Player>().destekKilitle(destekNo);
    }

    public void KayitOl()
    {
        if (KullaniciAdiKayitOl.text.Length > 4 && KullaniciAdiKayitOl.text.Length < 16)
        {
            if (SifreKayitOl.text.Length > 4 && SifreKayitOl.text.Length < 16)
            {
                if (EpostaKayitOl.text == EpostaTekrarKayitOl.text && EpostaKayitOl.text.Length > 0)
                {
                    BenimGemim.GetComponent<Player>().KayitOl(KullaniciAdiKayitOl.text, SifreKayitOl.text, EpostaKayitOl.text, Version);
                }
                else
                {
                    BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 264]);
                }
            }
            else
            {
                BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 370]);

            }

        }
        else
        {
            
            BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 373]);

        }


    }
    public void tamirButton()
    {
        BenimGemim.GetComponent<Player>().tamirOlBaslat();
    }
    public void SifremiUnuttumButton(Text eposta)
    {
        if(eposta.text.Length > 0)
        {
            BenimGemim.GetComponent<Player>().MailYollaSunucu(eposta.text);
            GirisYapPaneli.SetActive(true);
            SifremiUnuttumPaneli.SetActive(false);
        }
        else
        {
            Debug.Log("e posta giriniz");
        }
        
    }
    public void PremiumMarketSatinAl(int satinalinacakPaketId)
    {
        BenimGemim.GetComponent<Player>().PremiumPaketSatinAl(satinalinacakPaketId);
    }
    public void AyarlarNickDegistirOnaylaButton()
    {
        if (AyarlarIsimDegistirMailText.text.Length > 0 && AyarlarIsimDegistirYeniNickTXT.text.Length > 4 && AyarlarIsimDegistirYeniNickTXT.text.Length < 16)
        {
            BenimGemim.GetComponent<Player>().AyarlarNickDegistirmeOnayla(AyarlarIsimDegistirYeniNickTXT.text, AyarlarIsimDegistirMailText.text);
        }
        else
        {
            BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 353]);
        }
    }
    public void AyarlarMailDegistirButton(Text kullanicidonenmail)
    {
        BenimGemim.GetComponent<Player>().AyarlarMailDegistirme(kullanicidonenmail.text);
        kullaniciMailDegistrimeOnayKoduPaneli.SetActive(true);
        AyarlarmailDegistirBTN.SetActive(false);
        AyarlarMailDegistirmeMailINPT.SetActive(false);
    }

    public void AyarlarMailDegistirOnayButton(Text kullanicidandonenonaykodu)
    {
        BenimGemim.GetComponent<Player>().MailDegistirmeOnayKoduSunucuyaYolla(int.Parse(kullanicidandonenonaykodu.text), KullanicidanDonenYeniMail.text);
   
    }
    public void AyarlarSifreDegistirButton()
    {
        if (SifreDegistirSifre.text.Length > 4 && SifreDegistirSifre.text.Length < 16)
        {
            if (SifreDegistirSifre.text == SifreDegistirTekrarSifre.text)
            {
                BenimGemim.GetComponent<Player>().SifreDegistirmeIstegiSunucuyaYolla(SifreDegistirSifre.text, AyarlarSifreDegistirMail.text);
            }
            else
            {
                BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 271]);
            }
        }
        else
        {
            BildirimOlustur(dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 258], dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 272]);
        }

    }

    public void SetJoyistick()
    {
        if (kamerakaydirmajoyistikAyari.isOn)
        {
            kamerakaydirmajoyistik.gameObject.SetActive(true);
        }
        else
        {
            kamerakaydirmajoyistik.gameObject.SetActive(false);
        }
    }
    public void SetSuncuTahtasi()
    {
        if (SunucuTahtasi.isOn)
        {
            SunucuTahtasiObje.gameObject.SetActive(true);
        }
        else
        {
            SunucuTahtasiObje.gameObject.SetActive(false);
        }
    }
    public void SetEtkinlikKiyametGemisiSayac()
    {
        EtkinlikKiyametGemisiSayac++;
        if (EtkinlikKiyametGemisiSayac % 5 == 0)
        {
            EtkinlikKiyametNpcDogurOrta();
        }
        if (EtkinlikKiyametGemisiSayac % 20 == 0)
        {
            EtkinlikKiyametNpcDogurGuclu();
        }
    }

    [ClientRpc]
    public void oyunDunyasiBildirimGoster(string duyuru)
    {
        if (duyuru.Contains("409"))
        {
            int baslangicIndex = 0;
            int bitisIndex = duyuru.IndexOf(",Filo2=");
            string saldiranFiloAd = "";
            string savunanFiloAd = "";

            if (baslangicIndex != -1 && bitisIndex != -1)
            {
                int uzunluk = bitisIndex - 6;
                string cekilenMetin = duyuru.Substring(6, uzunluk);
                saldiranFiloAd = cekilenMetin;
            }

            baslangicIndex = bitisIndex;
            bitisIndex = duyuru.IndexOf(",CeviriKodu=409");

            if (baslangicIndex != -1 && bitisIndex != -1)
            {
                int uzunluk = bitisIndex - (baslangicIndex + ",Filo2=".Length);
                string cekilenMetin = duyuru.Substring(baslangicIndex + ",Filo2=".Length, uzunluk);
                savunanFiloAd = " " + cekilenMetin;
            }
            DunyaBildirimi.SetActive(true);
            DunyaBildirimiTXT.text = saldiranFiloAd + dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 408] + savunanFiloAd + dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 409];
            Animator animator = DunyaBildirimi.GetComponent<Animator>();
            animator.Play("KayanYazi");
        }
        else if (duyuru.Contains("410"))
        {
            int baslangicIndex = 0;
            int bitisIndex = duyuru.IndexOf(",Filo2=");
            string muttefikOlanFiloAd = "";
            string muttefikOlunanFiloAd = "";

            if (baslangicIndex != -1 && bitisIndex != -1)
            {
                int uzunluk = bitisIndex - 6;
                string cekilenMetin = duyuru.Substring(6, uzunluk);
                muttefikOlanFiloAd = cekilenMetin;
            }

            baslangicIndex = bitisIndex;
            bitisIndex = duyuru.IndexOf(",CeviriKodu=410");

            if (baslangicIndex != -1 && bitisIndex != -1)
            {
                int uzunluk = bitisIndex - (baslangicIndex + ",Filo2=".Length);
                string cekilenMetin = duyuru.Substring(baslangicIndex + ",Filo2=".Length, uzunluk);
                muttefikOlunanFiloAd = " " + cekilenMetin;
            }
            DunyaBildirimi.SetActive(true);
            DunyaBildirimiTXT.text = muttefikOlanFiloAd + dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 408] + muttefikOlunanFiloAd +  dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 410];
            Animator animator = DunyaBildirimi.GetComponent<Animator>();
            animator.Play("KayanYazi");

        }
        else if (duyuru.Contains("411"))
        {
            int baslangicIndex = 0;
            int bitisIndex = duyuru.IndexOf(",Filo2=");
            string fethedenFilo = "";
            string fethedilenHarita = "";

            if (baslangicIndex != -1 && bitisIndex != -1)
            {
                int uzunluk = bitisIndex - 6;
                string cekilenMetin = duyuru.Substring(6, uzunluk);
                fethedenFilo = cekilenMetin;
            }

            baslangicIndex = bitisIndex;
            bitisIndex = duyuru.IndexOf(",CeviriKodu=411");

            if (baslangicIndex != -1 && bitisIndex != -1)
            {
                int uzunluk = bitisIndex - (baslangicIndex + ",Filo2=".Length);
                string cekilenMetin = duyuru.Substring(baslangicIndex + ",Filo2=".Length, uzunluk);
                fethedilenHarita = " " + cekilenMetin;
            }
            DunyaBildirimi.SetActive(true);
            DunyaBildirimiTXT.text = fethedenFilo + dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 408] + fethedilenHarita + dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 411];
            Animator animator = DunyaBildirimi.GetComponent<Animator>();
            animator.Play("KayanYazi");

        }
        else if (duyuru.Contains("478"))
        {
            int baslangicIndex = 0;
            int bitisIndex = duyuru.IndexOf(",Filo2=");
            string saldirilanHarita = "";

            if (baslangicIndex != -1 && bitisIndex != -1)
            {
                int uzunluk = bitisIndex - 6;
                string cekilenMetin = duyuru.Substring(6, uzunluk);
                saldirilanHarita = cekilenMetin;
            }
           
            DunyaBildirimi.SetActive(true);
            DunyaBildirimiTXT.text = saldirilanHarita + dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 478];
            Animator animator = DunyaBildirimi.GetComponent<Animator>();
            animator.Play("KayanYazi");

        }


    }

#if UNITY_SERVER || UNITY_EDITOR

    public void oyunDunyasiSavasDuyurusuGoster(string savasAcanFiloAdi, string SavasAcilanFiloAdi)
    {
            DunyaBildirimiTextleri.Add("Filo1=" + savasAcanFiloAdi +",Filo2=" + SavasAcilanFiloAdi + ",CeviriKodu=409");
    }

    public void oyunDunyasiMuttefikDuyurusuGoster(string MuttefikOlanFiloAdi, string MuttefikOlunanFiloAdi)
    {
        DunyaBildirimiTextleri.Add("Filo1=" + MuttefikOlanFiloAdi + ",Filo2=" + MuttefikOlunanFiloAdi + ",CeviriKodu=410");
    }

    public void oyunDunyasiAdaFethiDuyurusuGoster(string fethedenFilo, int Harita)
    {
        DunyaBildirimiTextleri.Add("Filo1=" + fethedenFilo + ",Filo2=" + Harita + ",CeviriKodu=411");
    }

    public void oyunDunyasiAdaSaldiriAltindaDuyurusuGoster(int Harita)
    {
        DunyaBildirimiTextleri.Add("Filo1=" + Harita + ",Filo2=" + Harita + ",CeviriKodu=478");
    }
#endif

    public void oyuncuBotKontrolEtButton()
    {
        BenimGemim.GetComponent<Player>().oyuncuBotKontrolEt(kullanicidanDonTextenText.text);
    }
    public void FiloDostolButton(Text FiloKisaAd)
    {
        BenimGemim.GetComponent<Player>().filoDostOlmaIstegiYollaSunucu(FiloKisaAd.text);
    }
    public void FiloDusmanlButton(Text FiloKisaAd)
    {
        BenimGemim.GetComponent<Player>().filoDusmanOlmaIstegiYollaSunucu(FiloKisaAd.text);
    }
    public void FiloMuttefikKabuletButton(Text FiloTagý)
    {
        BenimGemim.GetComponent<Player>().FiloMuttefikIstegiKabulEt(FiloTagý.text);
    }
    public void FiloMuttefikRedEtButton(Text FiloTagi)
    {
        BenimGemim.GetComponent<Player>().FiloMuttefikIstegiRedEt(FiloTagi.text);
    }
    public void FiloMuttefikleriAcButton()
    {
        if (FiloMuttefikitemList.Count>0)
        {
            if (!FiloMuttefikPaneli.activeSelf)
            {
                FiloMuttefikPaneli.SetActive(true);
                Vector3 sonGameObjectPozisyonu = FiloMuttefikPaneli.transform.position; // Son GameObject'in pozisyonunu al
                sonGameObjectPozisyonu.y -= 400f;
                FiloDusmanPaneli.transform.position = sonGameObjectPozisyonu;
                sonGameObjectPozisyonu.y += 280;
                FiloDusmanButton.transform.position = sonGameObjectPozisyonu;
                sonGameObjectPozisyonu = FiloDusmanButton.transform.position;
                sonGameObjectPozisyonu.y -= 55f;
                FiloDusmanAdiGirINPT.transform.position = sonGameObjectPozisyonu;
                sonGameObjectPozisyonu = FiloDusmanAdiGirINPT.transform.position;
                sonGameObjectPozisyonu.y -= 44f;
                FiloDusmanIstegiYollaBTN.transform.position = sonGameObjectPozisyonu;

                if (FiloDusmanPaneli.activeSelf)
                {
                    GameObject sonGameObject2 = FiloDusmanitemList[FiloDusmanitemList.Count - 1]; // Son GameObject'i al
                    Vector3 sonGameObjectPozisyonu2 = sonGameObject2.transform.position; // Son GameObject'in pozisyonunu al
                    sonGameObjectPozisyonu2.y -= 260;
                    FiloIsteklerPaneli.transform.position = sonGameObjectPozisyonu2;
                    sonGameObjectPozisyonu2.y += 200;
                    FiloIsteklerButton.transform.position = sonGameObjectPozisyonu2;
                }
                else if (!FiloDusmanPaneli.activeSelf)
                {
                    Vector3 sonGameObjectPozisyonu2 = FiloDusmanIstegiYollaBTN.transform.position; // Son GameObject'in pozisyonunu al
                    sonGameObjectPozisyonu2.y -= 58;
                    FiloIsteklerButton.transform.position = sonGameObjectPozisyonu2;
                    sonGameObjectPozisyonu2 = FiloIsteklerButton.transform.position;
                    sonGameObjectPozisyonu2.y -= 214;
                    FiloIsteklerPaneli.transform.position = sonGameObjectPozisyonu2;
                }

            }
            else if (FiloMuttefikPaneli.activeSelf)
            {
                FiloMuttefikPaneli.SetActive(false);
                Vector3 sonGameObjectPozisyonu = FiloDostIstegiYollaBTN.transform.position; // Son GameObject'in pozisyonunu al
                sonGameObjectPozisyonu.y -= 59;
                FiloDusmanButton.transform.position = sonGameObjectPozisyonu;
                sonGameObjectPozisyonu.y -= 290;
                FiloDusmanPaneli.transform.position = sonGameObjectPozisyonu;

                sonGameObjectPozisyonu = FiloDusmanButton.transform.position;
                sonGameObjectPozisyonu.y -= 55f;
                FiloDusmanAdiGirINPT.transform.position = sonGameObjectPozisyonu;
                sonGameObjectPozisyonu = FiloDusmanAdiGirINPT.transform.position;
                sonGameObjectPozisyonu.y -= 44f;
                FiloDusmanIstegiYollaBTN.transform.position = sonGameObjectPozisyonu;

                if (FiloDusmanPaneli.activeSelf)
                {
                    GameObject sonGameObject2 = FiloDusmanitemList[FiloDusmanitemList.Count - 1]; // Son GameObject'i al
                    Vector3 sonGameObjectPozisyonu2 = sonGameObject2.transform.position; // Son GameObject'in pozisyonunu al
                    sonGameObjectPozisyonu2.y -= 260;
                    FiloIsteklerPaneli.transform.position = sonGameObjectPozisyonu2;
                    sonGameObjectPozisyonu2.y += 200;
                    FiloIsteklerButton.transform.position = sonGameObjectPozisyonu2;
                }
                else if (!FiloDusmanPaneli.activeSelf)
                {
                    Vector3 sonGameObjectPozisyonu2 = FiloDusmanIstegiYollaBTN.transform.position; // Son GameObject'in pozisyonunu al
                    sonGameObjectPozisyonu2.y -= 260;
                    FiloIsteklerPaneli.transform.position = sonGameObjectPozisyonu2;
                    sonGameObjectPozisyonu2.y += 200;
                    FiloIsteklerButton.transform.position = sonGameObjectPozisyonu2;
                }
            }
            if (FiloDusmanPaneli.activeSelf)
            {
                FiloDusmanPaneli.SetActive(false);
                Vector3 sonGameObjectPozisyonu2 = FiloDusmanIstegiYollaBTN.transform.position; // Son GameObject'in pozisyonunu al
                sonGameObjectPozisyonu2.y -= 260;
                FiloIsteklerPaneli.transform.position = sonGameObjectPozisyonu2;
                sonGameObjectPozisyonu2.y += 200;
                FiloIsteklerButton.transform.position = sonGameObjectPozisyonu2;
            }
            if (FiloIsteklerPaneli.activeSelf)
            {
                FiloIsteklerPaneli.SetActive(false);
            }

        }
    }
    public void FiloDusmanlariAcButton()
    {
        if (FiloDusmanitemList.Count > 0)
        {
            if (!FiloDusmanPaneli.activeSelf)
            {
                FiloDusmanPaneli.SetActive(true);
                GameObject sonGameObject2 = FiloDusmanitemList[FiloDusmanitemList.Count - 1]; // Son GameObject'i al
                Vector3 sonGameObjectPozisyonu2 = sonGameObject2.transform.position; // Son GameObject'in pozisyonunu al
                sonGameObjectPozisyonu2.y -= 260;
                FiloIsteklerPaneli.transform.position = sonGameObjectPozisyonu2;
                sonGameObjectPozisyonu2.y += 200;
                FiloIsteklerButton.transform.position = sonGameObjectPozisyonu2;
            }
            else if (FiloDusmanPaneli.activeSelf)
            {
                FiloDusmanPaneli.SetActive(false);
                Vector3 sonGameObjectPozisyonu2 = FiloDusmanIstegiYollaBTN.transform.position; // Son GameObject'in pozisyonunu al
                sonGameObjectPozisyonu2.y -= 260;
                FiloIsteklerPaneli.transform.position = sonGameObjectPozisyonu2;
                sonGameObjectPozisyonu2.y += 200;
                FiloIsteklerButton.transform.position = sonGameObjectPozisyonu2;
            }



            if (FiloMuttefikPaneli.activeSelf)
            {
                FiloMuttefikPaneli.SetActive(false);
                Vector3 sonGameObjectPozisyonu = FiloDostIstegiYollaBTN.transform.position; // Son GameObject'in pozisyonunu al
                sonGameObjectPozisyonu.y -= 59;
                FiloDusmanButton.transform.position = sonGameObjectPozisyonu;
                sonGameObjectPozisyonu.y -= 290;
                FiloDusmanPaneli.transform.position = sonGameObjectPozisyonu;

                sonGameObjectPozisyonu = FiloDusmanButton.transform.position;
                sonGameObjectPozisyonu.y -= 55f;
                FiloDusmanAdiGirINPT.transform.position = sonGameObjectPozisyonu;
                sonGameObjectPozisyonu = FiloDusmanAdiGirINPT.transform.position;
                sonGameObjectPozisyonu.y -= 44f;
                FiloDusmanIstegiYollaBTN.transform.position = sonGameObjectPozisyonu;

                if (FiloDusmanPaneli.activeSelf)
                {
                    GameObject sonGameObject2 = FiloDusmanitemList[FiloDusmanitemList.Count - 1]; // Son GameObject'i al
                    Vector3 sonGameObjectPozisyonu2 = sonGameObject2.transform.position; // Son GameObject'in pozisyonunu al
                    sonGameObjectPozisyonu2.y -= 260;
                    FiloIsteklerPaneli.transform.position = sonGameObjectPozisyonu2;
                    sonGameObjectPozisyonu2.y += 200;
                    FiloIsteklerButton.transform.position = sonGameObjectPozisyonu2;
                }
                else if (!FiloDusmanPaneli.activeSelf)
                {
                    Vector3 sonGameObjectPozisyonu2 = FiloDusmanIstegiYollaBTN.transform.position; // Son GameObject'in pozisyonunu al
                    sonGameObjectPozisyonu2.y -= 260;
                    FiloIsteklerPaneli.transform.position = sonGameObjectPozisyonu2;
                    sonGameObjectPozisyonu2.y += 200;
                    FiloIsteklerButton.transform.position = sonGameObjectPozisyonu2;
                }
              
            }
             if (FiloIsteklerPaneli.activeSelf)
            {
                FiloIsteklerPaneli.SetActive(false);
            }

        }
    }

    public void FiloIsteklerAcButton()
    {
            if (!FiloIsteklerPaneli.activeSelf)
            {
                FiloIsteklerPaneli.SetActive(true);
            }
            else if (FiloIsteklerPaneli.activeSelf)
            {
                FiloIsteklerPaneli.SetActive(false);
            }
    }

    public void muttefikliktenCikarButton(Text KlanTagi)

    {
        BenimGemim.GetComponent<Player>().muttefikliktenCikar(KlanTagi.text);
    }

    public void FiloBarisImzalaButton(Text FiloKisaAd)
    {
        BenimGemim.GetComponent<Player>().filoBarismaIstegiYollaSunucu(FiloKisaAd.text);
    }

    public void FiloBarisIstegiKabulEtButton(Text klanKisaAd)
    {
        BenimGemim.GetComponent<Player>().FiloBarisIstegiKabulEt(klanKisaAd.text);
    }
    public void FiloBarisIstegiRedEtButton(Text klanKisaAd)
    {
        BenimGemim.GetComponent<Player>().FiloBarisIstegiRedEt(klanKisaAd.text);
    }

    public void YetenekPuaniSatinAlButton()
    {
        BenimGemim.GetComponent<Player>().YetenekPuaniSatiAlSunucu();
    }


    public void YetenekPuaniKullanButton(int yukseltilecekYetenekID)
    {
        BenimGemim.GetComponent<Player>().YetenekPuaniKullanSunucu(yukseltilecekYetenekID);
    }

    public void YetenekPuaniSifirla()
    {
            BenimGemim.GetComponent<Player>().YetenekPuaniSifirlaSunucu();
    }
#if UNITY_SERVER || UNITY_EDITOR

    public void OyuncuDenkgelIsinla(string denkGelinecekOyuncuAdi)
    {
        if (GameObject.Find(denkGelinecekOyuncuAdi))
        {
            Player player = GameObject.Find(denkGelinecekOyuncuAdi).GetComponent<Player>();
            denkGelinenOyuncununHaritasi = player.GetComponent<Player>().harita;
            denkGelinenOyuncununKonumu = player.transform.position;
            player.GetComponent<NavMeshAgent>().Warp(new Vector2(-143, 0));
            player.GetComponent<Player>().target = player.transform.position;
            player.GetComponent<Player>().GemiHareketEttir(player.GetComponent<Player>().target, player.GetComponent<Player>().target);
            player.GetComponent<Player>().TargetAtlamaIstegiSunucu(99, player.transform.position);
        }

    }
    public void OyuncuKov(string kovulacakOyuncuAdi)
    {
        if (GameObject.Find(kovulacakOyuncuAdi))
        {
            Player player = GameObject.Find(kovulacakOyuncuAdi).GetComponent<Player>();
            player.GetComponent<NavMeshAgent>().Warp(denkGelinenOyuncununKonumu);
            player.GetComponent<Player>().target = player.transform.position;
            player.GetComponent<Player>().GemiHareketEttir(player.GetComponent<Player>().target, player.GetComponent<Player>().target);
            player.GetComponent<Player>().TargetAtlamaIstegiSunucu(denkGelinenOyuncununHaritasi, player.transform.position);
        }
    }
#endif
    public void GuncellemeRouter()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.morphesgames.lostpirates&hl=en_US&pli=1");
    }
    public void TopSatmaPaneliAc()
    {
        if (BenimGemim.GetComponent<Player>().OyuncuTopSecmeDUrumu == true)
        {
            TopSatmaPaneli.SetActive(true);
        }
    }
    public void TopSatmaPaneliKapa()
    {
        TopSatmaPaneli.SetActive(false);
    }
    public void TopSatisFiyatBelirle()
    {
        BenimGemim.GetComponent<Player>().Oyuncutoplamsatisfiyatý = int.Parse(TopSatmaMiktarSlider.value.ToString()) * BenimGemim.GetComponent<Player>().OyuncuTopSatisFiyati;
        TopSatmaBedeliTopTXT.text = BenimGemim.GetComponent<Player>().Oyuncutoplamsatisfiyatý.ToString();
        TopSatmaMiktarAdetiTXT.text = TopSatmaMiktarSlider.value.ToString();
    }
    public void TopSatButton()
    {
        BenimGemim.GetComponent<Player>().TopSatSunucu(int.Parse(TopSatmaMiktarSlider.value.ToString()),BenimGemim.GetComponent<Player>().OyuncuSatilacakTopID);
    }

    [ClientRpc]
    public void SunucuTahtasiGuncelle(string message)
    {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
        Chat.GetComponent<Chat>().AddMessageToKillTable(message);
#endif
    }

    public void GemiMarketiTasarýmlarýDegistir()
    {
        if (GemiMarketiTasarýmlarDropDown.value == 0)
        {
            OzelTasarimlarScroll.SetActive(false);
            EpTasarimlarScroll.SetActive(true);
           
        }
        else if (GemiMarketiTasarýmlarDropDown.value == 1)
        {
            OzelTasarimlarScroll.SetActive(true);
            EpTasarimlarScroll.SetActive(false);
        }
    }
}