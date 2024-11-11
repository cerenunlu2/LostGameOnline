using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;
using UnityEngine.UI;

public class SandikKontrol : NetworkBehaviour
{
#if UNITY_SERVER || UNITY_EDITOR
    public int HaritaKac = 1;
    bool icerigirdi = false;
#endif
    public void Start()
    {
        GetComponent<NavMeshAgent>().updateRotation = false;
        GetComponent<NavMeshAgent>().updateUpAxis = false;
        transform.eulerAngles = new(0, 0, 0);
        if (isClient)
        {
            transform.GetComponent<SpriteRenderer>().sprite = GameManager.gm.etkinlikSandiklari[Random.Range(0, 1)];
        }
    }



#if UNITY_SERVER || UNITY_EDITOR
    void OnTriggerEnter2D(Collider2D sandigaCarpanOyuncu)
    {
        if (sandigaCarpanOyuncu.CompareTag("Oyuncu") && new Vector2(Mathf.RoundToInt(sandigaCarpanOyuncu.GetComponent<Player>().target.x), Mathf.RoundToInt(sandigaCarpanOyuncu.GetComponent<Player>().target.y)) == new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)) && isServer && icerigirdi == false)
        {
            SandikAc(sandigaCarpanOyuncu.gameObject);
        }
    }

    public void SandikAc(GameObject sandigaCarpanOyuncu)
    {
        sandigaCarpanOyuncu.GetComponent<Player>().SureliSandikPaketiKontrol();
        icerigirdi = true;
        string satir = "";
        int deger = 1;
        int odulTipi = -1;
        int rastgelesayi = Random.Range(1, 101);
        int etkinlikCarpan = 1;
        if (GameManager.gm.EtkinlikAktiflikDurumu)
        {
            etkinlikCarpan = 2;
        }
        if (rastgelesayi < 15)//altin
        {
            deger = (Random.Range(8, 10) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "Altin = Altin + " + deger;
            odulTipi = 0;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuAltin(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuAltin + deger);
        }
        else if (rastgelesayi < 25)//tecrubepuani
        {
            deger = (Random.Range(2, 4) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "TecrubePuani = TecrubePuani + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuTerubePuani(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuTecrubePuan + deger);
            odulTipi = 1;
        }
        else if (rastgelesayi < 33)
        {
            deger = (Random.Range(15, 20) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "ElitPuan = ElitPuan + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetElitPuani(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuElitPuan + deger);
            odulTipi = 2;
        }
        else if (rastgelesayi < 39)
        {
            deger = (Random.Range(90, 120) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "DemirGulle = DemirGulle + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuDemirGullesi(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuDemirGulle + deger);
            odulTipi = 3;
        }
        else if (rastgelesayi < 43)
        {
            deger = (Random.Range(15, 20) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "AlevGulle = AlevGulle + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuAlevGullesi(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuAlevGulle + deger);
            odulTipi = 4;
        }
        else if (rastgelesayi < 47)
        {
            deger = (Random.Range(15, 20) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "HavaiFisekGulle = HavaiFisekGulle + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuHavaiFisek(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuHavaiFisekGulle + deger);
            odulTipi = 5;
        }

        else if (rastgelesayi < 52)
        {
            deger = (Random.Range(15, 20) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "SifaGulle = SifaGulle + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuSifaGullesi(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuSifaGulle + deger);
            odulTipi = 6;
        }
        else if (rastgelesayi < 58)
        {
            deger = (Random.Range(10, 20) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "PaslanmisZipkin = PaslanmisZipkin + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuPaslanmisZipkin(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuPaslanmisZipkin + deger);
            odulTipi = 7;
        }
        else if (rastgelesayi < 62)
        {
            deger = (Random.Range(1, 3) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "GumusZipkin = GumusZipkin + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuGumusZipkin(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuGumusZipkin + deger);
            odulTipi = 8;
        }
        else if (rastgelesayi < 66)
        {
            deger = (Random.Range(1, 2) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "AltinZipkin = AltinZipkin + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuAltinZipkin(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuAltinZipkin + deger);
            odulTipi = 9;
        }
        else if (rastgelesayi < 73)
        {
            deger = (Random.Range(4, 6) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "Barut = Barut + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuBarut(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuBarut + deger);
            odulTipi = 10;
        }
        else if (rastgelesayi < 80)
        {
            deger = (Random.Range(4, 6) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "Kalkan = Kalkan + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuKalkan(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuKalkan + deger);
            odulTipi = 11;
        }
        else if (rastgelesayi < 87)
        {
            deger = (Random.Range(2, 4) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "Roket = Roket + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuRoket(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuRoket + deger);
            odulTipi = 12;
        }
        else if (rastgelesayi < 94)
        {
            deger = (Random.Range(4, 6) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "HizTasi = HizTasi + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuHiztasi(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuHizTasi + deger);
            odulTipi = 13;
        }
        else if (rastgelesayi < 100)
        {
            deger = (Random.Range(10, 15) * etkinlikCarpan * HaritaKac) * (sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu + 1);
            satir = "Tamir = Tamir + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuTamir(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuTamir + deger);
            odulTipi = 14;
        }
        else if (rastgelesayi == 100)
        {
            deger = Random.Range(1, 2) + sandigaCarpanOyuncu.GetComponent<Player>().OyuncuSandikKatlamaDurumu;
            satir = "LostCoin = LostCoin + " + deger;
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuLostCoin(sandigaCarpanOyuncu.GetComponent<Player>().oyuncuLostCoin + deger);
            odulTipi = 15;
        }
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Kullanici SET " + satir + ",ToplananSandik = ToplananSandik + 1 WHERE kullanici_adi=@kullaniciadi;";
            command.Parameters.AddWithValue("@kullaniciadi", sandigaCarpanOyuncu.GetComponent<Player>().oyuncuadi);
            command.ExecuteNonQuery();
            NetworkIdentity opponentIdentity = sandigaCarpanOyuncu.GetComponent<NetworkIdentity>();
            TargetSandikDonus(opponentIdentity.connectionToClient, deger, odulTipi);
            StartCoroutine(SandikOldur());
        }
        sandigaCarpanOyuncu.GetComponent<Player>().botKontrolKalanIslemSayisiGuncelle();
        if (HaritaKac == 1&& sandigaCarpanOyuncu.GetComponent<Player>().oyuncuTecrubePuan > 400)
        {
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuHaritaBirBesSandikToplamaGorevi(GameManager.gm.BenimGemim.GetComponent<Player>().haritaBirBesSandikToplaGoreviSayac + 1);
            sandigaCarpanOyuncu.GetComponent<Player>().GorevKontrolu();
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET HaritaBirBesSandikToplamaGoreviSayac = HaritaBirBesSandikToplamaGoreviSayac + 1 WHERE kullanici_adi=@kullaniciadi;";
                command.Parameters.AddWithValue("@kullaniciadi", sandigaCarpanOyuncu.GetComponent<Player>().oyuncuadi);
                command.ExecuteNonQuery();
                NetworkIdentity opponentIdentity = sandigaCarpanOyuncu.GetComponent<NetworkIdentity>();
            }
        }
        else if (HaritaKac == 2 && sandigaCarpanOyuncu.GetComponent<Player>().oyuncuTecrubePuan > 600)
        {
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuHaritaIkiOnSandikToplamaGorevi(GameManager.gm.BenimGemim.GetComponent<Player>().HaritaIkiOnSandikToplaSayac + 1);
            sandigaCarpanOyuncu.GetComponent<Player>().GorevKontrolu();
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET HaritaIkiOnSandikToplaSayac = HaritaIkiOnSandikToplaSayac + 1 WHERE kullanici_adi=@kullaniciadi;";
                command.Parameters.AddWithValue("@kullaniciadi", sandigaCarpanOyuncu.GetComponent<Player>().oyuncuadi);
                command.ExecuteNonQuery();
                NetworkIdentity opponentIdentity = sandigaCarpanOyuncu.GetComponent<NetworkIdentity>();
            }
        }
        else if (HaritaKac == 3 && sandigaCarpanOyuncu.GetComponent<Player>().oyuncuTecrubePuan > 900)
        {
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuHaritaUcYirmiSandikToplamaGorevi(GameManager.gm.BenimGemim.GetComponent<Player>().HaritaUcYirmiSandikToplaSayac + 1);
            sandigaCarpanOyuncu.GetComponent<Player>().GorevKontrolu();
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET HaritaUcYirmiSandikToplaSayac = HaritaUcYirmiSandikToplaSayac + 1 WHERE kullanici_adi=@kullaniciadi;";
                command.Parameters.AddWithValue("@kullaniciadi", sandigaCarpanOyuncu.GetComponent<Player>().oyuncuadi);
                command.ExecuteNonQuery();
                NetworkIdentity opponentIdentity = sandigaCarpanOyuncu.GetComponent<NetworkIdentity>();
            }
        }
        else if (HaritaKac == 4 && sandigaCarpanOyuncu.GetComponent<Player>().oyuncuTecrubePuan > 1200)
        {
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuHaritaDortKirkSandikToplamaGorevi(GameManager.gm.BenimGemim.GetComponent<Player>().HaritaDortKirkSandikToplaSayac + 1);
            sandigaCarpanOyuncu.GetComponent<Player>().GorevKontrolu();
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET HaritaDortKirkSandikToplaSayac = HaritaDortKirkSandikToplaSayac + 1 WHERE kullanici_adi=@kullaniciadi;";
                command.Parameters.AddWithValue("@kullaniciadi", sandigaCarpanOyuncu.GetComponent<Player>().oyuncuadi);
                command.ExecuteNonQuery();
                NetworkIdentity opponentIdentity = sandigaCarpanOyuncu.GetComponent<NetworkIdentity>();
            }
        }
        else if (HaritaKac == 5 && sandigaCarpanOyuncu.GetComponent<Player>().oyuncuTecrubePuan > 4000)
        {
            sandigaCarpanOyuncu.GetComponent<Player>().SetOyuncuHaritaBesAltmisSandikToplamaGorevi(GameManager.gm.BenimGemim.GetComponent<Player>().HaritaBesAtmisSandikToplaSayac + 1);
            sandigaCarpanOyuncu.GetComponent<Player>().GorevKontrolu();
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Kullanici SET HaritaBesAtmisSandikToplaSayac = HaritaBesAtmisSandikToplaSayac + 1 WHERE kullanici_adi=@kullaniciadi;";
                command.Parameters.AddWithValue("@kullaniciadi", sandigaCarpanOyuncu.GetComponent<Player>().oyuncuadi);
                command.ExecuteNonQuery();
                NetworkIdentity opponentIdentity = sandigaCarpanOyuncu.GetComponent<NetworkIdentity>();
            }
        }
    }

    IEnumerator SandikOldur()
    {
        yield return new WaitForSeconds(0.02f);
        GameManager.gm.SandikUret(name, HaritaKac);
        NetworkServer.Destroy(gameObject);
       
    }
#endif

    [TargetRpc]
    public void TargetSandikDonus(NetworkConnection target, int donenVeriOdul, int donenVeriOdulTipi)
    {
        string yazdirilacakOdulYazisi = "";
        if (donenVeriOdulTipi == 0)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 250];
        }
        else if (donenVeriOdulTipi == 1)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 252];
        }
        else if (donenVeriOdulTipi == 2)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 251];
        }
        else if (donenVeriOdulTipi == 3)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 319];
        }
        else if (donenVeriOdulTipi == 4)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 320];
        }
        else if (donenVeriOdulTipi == 5)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 321];
        }
        else if (donenVeriOdulTipi == 6)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 322];
        }
        else if (donenVeriOdulTipi == 7)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 323];
        }
        else if (donenVeriOdulTipi == 8)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 324];
        }
        else if (donenVeriOdulTipi == 9)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 325];
        }
        else if (donenVeriOdulTipi == 10)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 326];
        }
        else if (donenVeriOdulTipi == 11)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 327];
        }
        else if (donenVeriOdulTipi == 12)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 328];
        }
        else if (donenVeriOdulTipi == 13)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 329];
        }
        else if (donenVeriOdulTipi == 14)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 330];
        }
        else if (donenVeriOdulTipi == 15)
        {
            yazdirilacakOdulYazisi = donenVeriOdul + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 331];
        }

        if (GameManager.gm.OdulText.text == "")
        {
            GameManager.gm.OdulText.GetComponent<Text>().text = yazdirilacakOdulYazisi;
            GameManager.gm.OdulYazisiSayacBaslat();
        }
        else if (GameManager.gm.OdulText.GetComponent<Text>().text != null)
        {
            GameManager.gm.OdulText2.GetComponent<Text>().text = yazdirilacakOdulYazisi;
            GameManager.gm.OdulYazisiSayacBaslat();
            return;
        }
        GameManager.gm.AddItemsSeyirDefteri(yazdirilacakOdulYazisi);
    }
}
