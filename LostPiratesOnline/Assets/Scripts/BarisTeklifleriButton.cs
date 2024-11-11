using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarisTeklifleriButton : MonoBehaviour
{
    public string filoKisaltmasi;
    public void BarisOlmaGenisletButton()
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().BarisIstekGenislet(filoKisaltmasi);
    }
}
