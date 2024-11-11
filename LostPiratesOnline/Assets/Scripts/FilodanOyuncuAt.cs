using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilodanOyuncuAt : MonoBehaviour
{
    public Text OyuncuAd, FilodanAtText;


    public void Awake()
    {
        FilodanAtText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 296];
    }
    public void FilodanAt()
    {
        GameManager.gm.FilodanOyuncuAt(OyuncuAd.text);
    }
}
