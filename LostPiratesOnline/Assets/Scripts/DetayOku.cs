using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetayOku : MonoBehaviour
{
    public int KonuId = -1;
    public int DestekNo = -1;
    public int TamamlanmaDurumu = 0;
    public Text detayiOkuText;

    public void Awake()
    {
        detayiOkuText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 295];
    }
    public void DetaylariOku()
    {
        if(DestekNo > 0)
        {
            GameManager.gm.BenimGemim.GetComponent<Player>().DestekTalebininDetaylariniYukle(DestekNo, TamamlanmaDurumu);
            GameManager.gm.destekNo = DestekNo;

        }
    }
}
