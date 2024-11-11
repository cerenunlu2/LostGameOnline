using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FiloBasvuruAt : MonoBehaviour
{
    public int filoId = -1;
    public Text katilText;
    // Start is called before the first frame update
    public void Awake()
    {
        katilText.text = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 290];
    }

    // Update is called once per frame
    public void filoBasvur()
    {
        GameManager.gm.FiloBasvur(filoId);
    }

}
