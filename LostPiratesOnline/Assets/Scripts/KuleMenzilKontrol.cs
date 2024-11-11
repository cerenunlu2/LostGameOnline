using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuleMenzilKontrol : MonoBehaviour
{
#if UNITY_SERVER || UNITY_EDITOR
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Oyuncu") && other.gameObject.GetComponent<Player>().OyuncuFiloKisaltma != transform.parent.GetComponent<KuleKontrol>().filoKisaltmasi)
        {
            transform.parent.GetComponent<KuleKontrol>().SetSonSaldiranOyuncu(other.gameObject,0);
            transform.parent.GetComponent<KuleKontrol>().gulleYarat();

        }
    }
#endif
}
