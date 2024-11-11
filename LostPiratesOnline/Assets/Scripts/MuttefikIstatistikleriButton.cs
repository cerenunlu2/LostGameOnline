using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuttefikIstatistikleriButton : MonoBehaviour
{
    public string filoKisaltmasi;
    public void MuttefikIstatistikGenislet()
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().MuttefikFiloGenislet(filoKisaltmasi);
    }
}
