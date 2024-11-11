using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuttefikIstekleriButton : MonoBehaviour
{
    public string filoKisaltmasi;
    public void muttefikIstekGenisletButton()
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().muttefikIstekGenislet(filoKisaltmasi);
    }
}
