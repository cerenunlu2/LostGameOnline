using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LostCoinKatlamaText : MonoBehaviour
{

    public float kalanZaman = 0f, guncellenenZaman = 0f;

    private void Awake()
    {
        guncellenenZaman = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - guncellenenZaman > 1 && kalanZaman > 0)
        {
            kalanZaman -= (Time.time - guncellenenZaman);
            guncellenenZaman = Time.time;
            int saat = Mathf.FloorToInt(kalanZaman / 3600);
            int dakika = Mathf.FloorToInt((kalanZaman % 3600) / 60);
            GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}", saat, dakika);
        }
    }
}
