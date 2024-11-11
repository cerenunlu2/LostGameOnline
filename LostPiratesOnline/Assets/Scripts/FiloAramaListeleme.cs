using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FiloAramaListeleme : MonoBehaviour
{
    public Text OyuncuAd;
    public Text kabulText, RedText,profilText;



    public void Awake()
    {
        kabulText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 291];
        RedText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 292];
        profilText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 293];
    }

    public void filoOyuncuBasvurusuKabulEt()
    {
        GameManager.gm.FiloyaBasvuranOyuncuyuKabulEt(OyuncuAd.text);
    }
    public void filoOyuncuBasvurusuRedEt()
    {
        GameManager.gm.FiloyaBasvuranOyuncuRedEt(OyuncuAd.text);
    }

  
}
