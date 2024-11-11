using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoketKontrol : MonoBehaviour
{
    public float speed = 20f;
    public float havalanmaSuresi = 0.8f;
    public float havalanmaHizi = 5f;
    public float hedefeDogruHareketZorlugu = 0.2f;

    public GameObject hedef;
    public GameObject sahibi;
    public GameObject HasarText;
    public int hasar;

    private Rigidbody2D rb;
    private float havalanmaZamanlayici = 0.0f;
    private bool havalanmaBitti = false;

    private Vector2 flightDirection; // Roketin uçuþ yönü

    private float flightDuration; // Roketin havada kalma süresi
    public float maxFlightDuration = 2f; // Roketin maksimum havada kalma süresi
    public float donusHizi = 30f;


    public Vector2 sunrise;
    public Transform sunset;
    public float journeyTime = 1.0F;
    private float startTime = -1;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (hedef == null || sahibi == null)
        {
            Destroy(gameObject);
        }
        else if (hedef != null && transform.position.y > hedef.transform.Find("Gemi").transform.position.y)
        {
            havalanmaBitti = true;
        }
    }

    private void Update()
    {
        if (hedef == null || sahibi == null || hedef.transform.Find("Gemi").gameObject.activeSelf == false)
        {
            Destroy(gameObject);
        }
        else if (!havalanmaBitti)
        {
            if (hedef.transform.position.x > transform.position.x)
            {
                flightDirection = new Vector2(Random.Range(0, (hedef.transform.position.x - transform.position.x) / 2), 1).normalized;
            }
            else
            {
                flightDirection = new Vector2(Random.Range((hedef.transform.position.x - transform.position.x) / 2, 0), 1).normalized;
            }
            flightDuration = maxFlightDuration;

            flightDuration -= Time.deltaTime;
            rb.velocity = flightDirection * speed;

            havalanmaZamanlayici += Time.deltaTime;
            if (havalanmaZamanlayici > havalanmaSuresi)
            {
                havalanmaBitti = true;
            }
        }
        else if (hedef != null)
        {
            if (startTime == -1)
            {
                startTime = Time.time;
                sunrise = transform.position;
                sunset = hedef.transform.Find("Gemi").transform;
            }
            Vector2 center = (sunrise + new Vector2(sunset.position.x, sunset.position.y)) * 0.5F;
            center -= new Vector2(0, 2);
            Vector2 riseRelCenter = sunrise - center;
            Vector2 setRelCenter = new Vector2(sunset.position.x, sunset.position.y) - center;
            float fracComplete = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += new Vector3(center.x, center.y, 0);
            /*
            // hedef yönünü hesapla
            Vector2 targetDirection = hedef.transform.Find("Gemi").transform.position - transform.position;

            // roketi hedefe doðru rastgele hareketlerle hareket ettir
            Vector2 randomTargetDirection = targetDirection + Random.insideUnitCircle * hedefeDogruHareketZorlugu;

            // roketi hedef yönünde döndür
            float angle = Mathf.Atan2(randomTargetDirection.y, randomTargetDirection.x) * Mathf.Rad2Deg;
            rb.rotation = angle;

            // roketi hedefe doðru hareket ettir
            rb.velocity = randomTargetDirection.normalized * speed;*/
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject != null && collisionInfo.gameObject.tag == "Merkez" && collisionInfo.gameObject.transform.parent.gameObject == hedef)
        {
            if ((hedef.CompareTag("SistemGemisi") && hedef.GetComponent<SistemGemisiKontrol>().Can <= 0) || (hedef.CompareTag("SistemOyuncuGemisi") && hedef.GetComponent<SistemOyuncuKontrol>().Can <= 0) || (hedef.CompareTag("EtkinlikGemisi") && hedef.GetComponent<EtkinlikSistemGemileriKontrol>().Can <= 0) || (hedef.CompareTag("EtkinlikBossu") && hedef.GetComponent<EtkinlikBossKontrol>().Can <= 0))
            {
                StartCoroutine(GameManager.gm.GemiYokEt(hedef));
            }
            HasarTextYarat(collisionInfo.gameObject, hasar);
            Destroy(gameObject);
        }
    }

    private void HasarTextYarat(GameObject hedefimsi, int hasar)
    {
        string hasartext = hasar.ToString();
        GameObject DamageTextInstance = Instantiate(HasarText);
        DamageTextInstance.transform.position = hedefimsi.transform.position;
        DamageTextInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = hasartext;
    }
}
