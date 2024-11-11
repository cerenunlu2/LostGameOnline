using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilGoster : MonoBehaviour
{
    public Text profiliGoruntuleText;
    public void Awake()
    {
        if (GameManager.gm.Sira != 1 && GameManager.gm.Sira != 2 && GameManager.gm.Sira != 3 && GameManager.gm.Sira != 0)
        {
            profiliGoruntuleText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 294];
        }
    }

    public void IstenilenOyuncununProfiliniAc(Text istenilenOyuncununAdi)
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().ProfilYukle(istenilenOyuncununAdi.text);
        GameManager.gm.SiralamMenu.SetActive(false);
        GameManager.gm.Ayarlar.SetActive(false);
        GameManager.gm.Profil.SetActive(true);
        GameManager.gm.FiloAna.SetActive(false);
    }
}
