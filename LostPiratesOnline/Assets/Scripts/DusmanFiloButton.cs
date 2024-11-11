using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DusmanFiloButton : MonoBehaviour
{
    public string filoKisaltmasi;
    public void FiloDusmanGenisletButton()
    {
        GameManager.gm.BenimGemim.GetComponent<Player>().DusmanFiloGenislet(filoKisaltmasi);
    }
}
