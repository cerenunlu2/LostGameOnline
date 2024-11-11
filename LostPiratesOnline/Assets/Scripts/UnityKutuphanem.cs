/** Bu kutuphane Muhammed Yasin Sekman taraf�ndan olu�turulmu�tur.
 * Unity kullan�m�n� kolayla�t�rmay� ama�lamaktad�r
 * ��erisinde Mirror multiplayer, physics, animations kodlar� yer almaktad�r
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Data;

public class UnityKutuphanem : NetworkBehaviour
{

    // Sunucuda veri taban� kullan�m� �rne�i
    [Command]
    public void SunucudakiVeriTabanindanVeriCekmeVeOyuncuyaGonderme()
    {
        // bu satir bu kodun build alinirken silinmesine yarar b�ylelikle oyuncular sunucu tabanl� kodlar� g�remez
#if UNITY_SERVER || UNITY_EDITOR
        // sunucuda veritaban�na ba�lanabildiyse anlam�n� ta��r
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            // veritaban�nda �al��mas�n� istedi�imiz kodu buraya yazariz, parametre kullanmamizin amaci sunucudaki veritabanin g�venli�ini arttirmak i�in
            command.CommandText = "SELECT * FROM Kullanici WHERE Kullanici_Adi=@kullaniciadi;";
            command.Parameters.AddWithValue("@kullaniciadi", name);
            // IdataReader kullanarak veritabanindan veriler okunur
            using (IDataReader reader = command.ExecuteReader())
            {
                // eger veri okunduysa if blogunun icine girer
                if (reader.Read())
                {
                    // okunan veri istenilen degiskene atanir
                    string okunanVeri = reader["kolonAdi"].ToString();
                    OyuncudaCalismasiIstenilenFonksiyon(okunanVeri);
                }
                // ve okuyucu kapatilir boylelikle tekrar kullanmak icin hazir olur
                reader.Close();
            }
        }

        // veri g�ncelleme
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "Update Kullanici SET GuncellenecekVeri = YeniVeri WHERE Kullanici_Adi=@kullanici_adi;";
            command.Parameters.AddWithValue("@kullanici_adi", name);
            command.ExecuteNonQuery();
        }
        // veri ekleme
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Kullanici (Kullanici_Adi,KayitTarihi) VALUES (@kullanici_adi,date('now'));";
            command.Parameters.AddWithValue("@kullanici_adi", name);
            command.ExecuteNonQuery();
        }
#endif
    }

    // TargetRpc kullanarak belirli bir oyuncuda istenilen fonksiyonun calismasi saglanir
    [TargetRpc]
    public void OyuncudaCalismasiIstenilenFonksiyon(string varsaOyuncuyaGonderilecekVeriler)
    {
        // bu kod oyunuya veri yollayarak ekrana o verilerin yazilmasini saglar
        Debug.Log(varsaOyuncuyaGonderilecekVeriler);
    }


    //----------------------------------- MIRROR BOLUMU -----------------------------------
    // Sadece sunucuda �al��acak kodlar i�in eklenecek
#if UNITY_SERVER || UNITY_EDITOR
#endif

    // Fonksiyonu oyuncudan sunucuya yollar
    [Command]
    public void SunucudaCalistir()
    {

    }

    // sunucu ile oyuncunun ortak degiskenlerini degistirmek icin kullanilacak kod
    int degisken = 0;
    public void SetOyuncuDegisken(int deger)
    {
        degisken = deger;
#if UNITY_SERVER || UNITY_EDITOR
        if (isServer)
        {
            TargetSetOyuncuDegisken(degisken);
        }
#endif
        if (isLocalPlayer)
        {
            // oyuncuda calisacak kodlar
        }
    }

    [TargetRpc]
    public void TargetSetOyuncuDegisken(int donenVeri)
    {
        if (!isServer)
        {
            SetOyuncuDegisken(donenVeri);
        }
    }
}
