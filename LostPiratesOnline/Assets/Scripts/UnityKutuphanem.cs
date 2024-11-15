/** Bu kutuphane Muhammed Yasin Sekman tarafından oluşturulmuştur.
 * Unity kullanımını kolaylaştırmayı amaçlamaktadır
 * İçerisinde Mirror multiplayer, physics, animations kodları yer almaktadır
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Data;

public class UnityKutuphanem : NetworkBehaviour
{

    // Sunucuda veri tabanı kullanımı örneği
    [Command]
    public void SunucudakiVeriTabanindanVeriCekmeVeOyuncuyaGonderme()
    {
        // bu satir bu kodun build alinirken silinmesine yarar böylelikle oyuncular sunucu tabanlı kodları göremez
#if UNITY_SERVER || UNITY_EDITOR
        // sunucuda veritabanına bağlanabildiyse anlamını taşır
        using (var command = GameManager.gm.sqliteConnection.CreateCommand())
        {
            // veritabanında çalışmasını istediğimiz kodu buraya yazariz, parametre kullanmamizin amaci sunucudaki veritabanin güvenliğini arttirmak için
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

        // veri güncelleme
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
    // Sadece sunucuda çalışacak kodlar için eklenecek
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
