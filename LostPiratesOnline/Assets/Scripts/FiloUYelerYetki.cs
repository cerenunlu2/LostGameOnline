using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FiloUYelerYetki : MonoBehaviour
{
    public Dropdown yetkiDorpdown;
    public Text SlotName,ProfilText;

    public void Awake()
    {
        ProfilText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 293];
    }
    public void UyelerYetkiDegistirme()
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().YetkiVer(yetkiDorpdown.value,SlotName.text);
    }
   
}
