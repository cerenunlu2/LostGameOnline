using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZipkinHareket : MonoBehaviour
{
    public Vector3 sunrise;
    public Transform sunset;
    public float journeyTime = 1.0F;
    private float startTime = -1;
    public GameObject hedef;
    public GameObject HasarTextPrefab;
    public int hasar = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_ANDROID
        if (hedef != null)
        {
            if (startTime == -1)
            {
                startTime = Time.time;
                sunrise = transform.position;
                sunset = hedef.transform.Find("Gemi").gameObject.transform;
            }
            Vector3 center = (sunrise + sunset.position) * 0.5F;
            center -= new Vector3(0, 1, 0);
            Vector3 riseRelCenter = sunrise - center;
            Vector3 setRelCenter = sunset.position - center;
            float fracComplete = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;
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
        if (collisionInfo.gameObject != null && collisionInfo.gameObject.tag == "DenizYaratigi" && collisionInfo.gameObject.name == hedef.name)
        {
            if (hasar > 0)
            {
                HasarTextYarat(collisionInfo.gameObject, hasar);
            }
            Destroy(gameObject);
        }
#endif
    }

    private void HasarTextYarat(GameObject hedefimsi, int hasar)
    {
        GameObject DamageTextInstance = Instantiate(HasarTextPrefab);
        DamageTextInstance.transform.position = hedefimsi.transform.position;
        DamageTextInstance.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = hasar.ToString();

    }
}

