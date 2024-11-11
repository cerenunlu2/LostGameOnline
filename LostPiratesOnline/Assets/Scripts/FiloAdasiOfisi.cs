using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using TMPro;


public class FiloAdasiOfisi : MonoBehaviour
{
    public GameObject KuleButton;
    public int AdaSahibiFiloId = -1;
    public List<int> AdayaHasarVerenFiloIDler = new List<int>();
    public List<int> AdayaHasarVerenFiloHasarlar = new List<int>();
    public int harita;
    public float enSonUyariYapilanSure = -300;


    void OnTriggerEnter2D(Collider2D adayaGirenOyuncu)
    {
        
        if (GameManager.gm.BenimGemim != null  )
        {
            if ( adayaGirenOyuncu.CompareTag("Oyuncu"))
            {
                if (GameManager.gm.FiloAdalariSahibiKisaAdlari[harita - 5] == GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuFiloKisaltma)
                {
                    if (GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuYetkiID <= 2)
                    {
                         KuleButton.SetActive(true);
                    }
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D adayaCkanOyuncu)
    {
        if (GameManager.gm.BenimGemim != null && adayaCkanOyuncu.CompareTag("Oyuncu"))
        {
            KuleButton.SetActive(false);
        }
    }

    //Kule dikme kodu
    public void KuleDikSeviye1(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleDik(1, hangiKule);
    }
    public void kuleDikSeviye2(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleDik(2, hangiKule);
    }
    public void kuleDikSeviye3(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleDik(3, hangiKule);
    }
    public void kuleDikSeviye4(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleDik(4, hangiKule);
    }
    public void kuleDikSeviye5(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleDik(5, hangiKule);
    }
    public void kuleDikSeviye6(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleDik(6, hangiKule);
    }

    //Kule tamir kodu
    public void KuleTamirEtSeviye1(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleTamiret(hangiKule);
    }
    public void KuleTamirEtSeviye2(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleTamiret(hangiKule);
    }
    public void KuleTamirEtSeviye3(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleTamiret(hangiKule);
    }
    public void KuleTamirEtSeviye4(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleTamiret(hangiKule);
    }
    public void KuleTamirEtSeviye5(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleTamiret(hangiKule);
    }
    public void KuleTamirEtSeviye6(int hangiKule)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().KuleTamiret(hangiKule);
    }

#if UNITY_SERVER || UNITY_EDITOR
    public void AdayaVerilenHasariHesapla(int filoId, int hasar)
    {
        int index = AdayaHasarVerenFiloIDler.IndexOf(filoId);
        if (index != -1)
        {
            int eskiHasar = (int)AdayaHasarVerenFiloHasarlar[index];
            AdayaHasarVerenFiloHasarlar[index] = eskiHasar + hasar;
        }
        else
        {
            AdayaHasarVerenFiloIDler.Add(filoId);
            AdayaHasarVerenFiloHasarlar.Add(hasar);
        }
    }

    public void AdaElDegistirmeKontrol()
    {
        int[] adadakiKuleler = new int[12];
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Adalar WHERE AdaId = @adaId;";
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
        if(adadakiKuleler[0] <= 0 && adadakiKuleler[1] <= 0 && adadakiKuleler[2] <= 0 && adadakiKuleler[3] <= 0 && adadakiKuleler[4] <= 0 && adadakiKuleler[5] <= 0 && adadakiKuleler[6] <= 0 && adadakiKuleler[7] <= 0 && adadakiKuleler[8] <= 0 && adadakiKuleler[9] <= 0 && adadakiKuleler[10] <= 0 && adadakiKuleler[11] <= 0)
        {
            int enYuksekHasariVuranFiloId = 0;
            int enYuksekHasar = 0;
            for (int i = 0; i < AdayaHasarVerenFiloIDler.Count; i++)
            {
               if (AdayaHasarVerenFiloHasarlar[i] > enYuksekHasar)
                {
                    enYuksekHasariVuranFiloId = AdayaHasarVerenFiloIDler[i];
                    enYuksekHasar = AdayaHasarVerenFiloHasarlar[i];
                    
                }
            }
            String filoAdi = "", filoKisaltmasi = "";
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Filolar WHERE FiloId = @filoId;";
                command.Parameters.AddWithValue("@filoId", enYuksekHasariVuranFiloId);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        filoAdi = reader["FiloAd"].ToString();
                        filoKisaltmasi = reader["FiloKisaltma"].ToString();
                    }
                    reader.Close();
                }
            }
            using (var command = GameManager.gm.sqliteConnection.CreateCommand())
            {
                command.CommandText = "Update Adalar SET AdaSahibiFiloId = @yeniAdaSahibiFiloId,AdaSahibiFiloKisaAd=(select FiloKisaltma from Filolar where FiloId=@yeniAdaSahibiFiloId), Kule1=1,Kule3=1,Kule5=1,Kule7=1,Kule9=1,Kule11=1  WHERE AdaId = @harita;";
                command.Parameters.AddWithValue("@yeniAdaSahibiFiloId", enYuksekHasariVuranFiloId);
                command.Parameters.AddWithValue("@harita", harita);
                if (command.ExecuteNonQuery() == 1)
                {
                    GameManager.gm.oyunDunyasiAdaFethiDuyurusuGoster(filoAdi + "[" + filoKisaltmasi + "]",harita);
                    GameManager.gm.FiloAdalariYukleEleDegistirenAda(harita);
                    AdayaHasarVerenFiloIDler.Clear();
                    AdayaHasarVerenFiloHasarlar.Clear();

                }
            }
        }
    }

    public void AdaSaldiriAltindaBildirimiKontrol()
    {
        if (Time.time - enSonUyariYapilanSure > 300)
        {
            enSonUyariYapilanSure = Time.time;
           GameManager.gm.oyunDunyasiAdaSaldiriAltindaDuyurusuGoster(harita);
        }
    }

#endif
}
