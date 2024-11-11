using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GulleHareket : MonoBehaviour
{
    public Vector2 sunrise;
    public Transform sunset;
    public float journeyTime = 1.0F;
    private float startTime = -1;
    public float hedefledenOrtalamaYukseklik = 0.5f;


    public GameObject hedef;
    public GameObject HasarTextPrefab, BarutluHasarTextPrefab, KalkanliHasarTextPrefab, BarutVeKalkanliHasarTextPrefab, SifaHasarTextPrefab;
    public int hasar = 0;
    public bool barutAktifDurum = false, kalkanAktifDurum = false;

    private void Start()
    {
        if (Vector2.Distance(transform.position, hedef.transform.Find("Gemi").transform.position) > 7)
        {
            journeyTime = 0.7f;
        }
        else if (Vector2.Distance(transform.position, hedef.transform.Find("Gemi").transform.position) > 4)
        {
            journeyTime = 0.6f;
        }
        else if (Vector2.Distance(transform.position, hedef.transform.Find("Gemi").transform.position) > 2)
        {
            journeyTime = 0.4f;
        }
        else if (Vector2.Distance(transform.position, hedef.transform.Find("Gemi").transform.position) <= 2)
        {
            journeyTime = 0.2f;
        }

    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_ANDROID
        if (hedef != null && hedef.transform.Find("Gemi").gameObject.activeSelf)
        {
            if (startTime == -1)
            {
                startTime = Time.time;
                sunrise = transform.position;
                sunset = hedef.transform.Find("Gemi").transform;
            }
            Vector2 center = (sunrise + new Vector2(sunset.position.x, sunset.position.y)) * hedefledenOrtalamaYukseklik;
            center -= new Vector2(0, 3);
            Vector2 riseRelCenter = sunrise - center;
            Vector2 setRelCenter = new Vector2(sunset.position.x, sunset.position.y) - center;
            float fracComplete = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += new Vector3(center.x, center.y, 0);
        }
        else
        {
            Destroy(gameObject);
        }

#endif
    }

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
#if UNITY_STANDALONE_WIN || UNITY_ANDROID || UNITY_EDITOR
        if (collisionInfo.gameObject != null && collisionInfo.gameObject.tag == "Merkez" && collisionInfo.gameObject.transform.parent.gameObject == hedef)
        {
            if (hasar > 0 && barutAktifDurum && kalkanAktifDurum)
            {
                HasarTextYarat(collisionInfo.gameObject, hasar, 3);
            }
            else if (hasar > 0 && barutAktifDurum)
            {
                HasarTextYarat(collisionInfo.gameObject, hasar, 1);
            }
            else if (hasar > 0 && kalkanAktifDurum)
            {
                HasarTextYarat(collisionInfo.gameObject, hasar, 2);
            }
            else if (hasar > 0)
            {
                HasarTextYarat(collisionInfo.gameObject, hasar, 0);
            }
            else if (hasar < 0)
            {
                HasarTextYarat(collisionInfo.gameObject, hasar, 4);
            }
            if ((hedef.CompareTag("SistemGemisi") && hedef.GetComponent<SistemGemisiKontrol>().Can <= 0) || (hedef.CompareTag("Tower") && hedef.GetComponent<KuleKontrol>().Can <= 0) || (hedef.CompareTag("SistemOyuncuGemisi") && hedef.GetComponent<SistemOyuncuKontrol>().Can <= 0) || (hedef.CompareTag("Oyuncu") && hedef.GetComponent<Player>().Can <= 0) || (hedef.CompareTag("EtkinlikGemisi") && hedef.GetComponent<EtkinlikSistemGemileriKontrol>().Can <= 0) || (hedef.CompareTag("EtkinlikBossu") && hedef.GetComponent<EtkinlikBossKontrol>().Can <= 0))
            {
                StartCoroutine(GameManager.gm.GemiYokEt(hedef));
            }
            Destroy(gameObject);
        }
#endif
    }

    // NORMAL:0, BARUT:1, KALKAN:2, BARUTveKALKAN:3, SIFA:4;
    private void HasarTextYarat(GameObject hedefimsi, int hasar, int yaziId)
    {
        if (yaziId == 0)
        {
            GameObject DamageTextInstance = Instantiate(HasarTextPrefab);
            DamageTextInstance.transform.position = hedefimsi.transform.position;
            DamageTextInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = hasar.ToString();
        }
        else if (yaziId == 1)
        {
            GameObject DamageTextInstance = Instantiate(BarutluHasarTextPrefab);
            DamageTextInstance.transform.position = hedefimsi.transform.position;
            DamageTextInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = hasar.ToString();
        }
        else if (yaziId == 2)
        {
            GameObject DamageTextInstance = Instantiate(KalkanliHasarTextPrefab);
            DamageTextInstance.transform.position = hedefimsi.transform.position;
            DamageTextInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = hasar.ToString();
        }
        else if (yaziId == 3)
        {
            GameObject DamageTextInstance = Instantiate(BarutVeKalkanliHasarTextPrefab);
            DamageTextInstance.transform.position = hedefimsi.transform.position;
            DamageTextInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = hasar.ToString();
        }
        else if (yaziId == 4)
        {
            GameObject DamageTextInstance = Instantiate(SifaHasarTextPrefab);
            DamageTextInstance.transform.position = hedefimsi.transform.position;
            DamageTextInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "+" + (-hasar).ToString();
        }

    }
}
