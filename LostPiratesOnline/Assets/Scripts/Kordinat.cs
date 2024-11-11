using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Kordinat : MonoBehaviour
{
    public TextMeshProUGUI kordinatText;
    public TextMeshProUGUI kordinatTextAndroid;
     string[] dikeyKordinatHarfleri = { "CF", "CE", "CD", "CC", "CB", "CA", "BZ", "BY", "BX", "BW", "BV", "BU", "BT", "BS", "BR", "BP", "BO", "BN", "BM", "BL", "BK", "BJ", "BI", "BH", "BG", "BF", "BE", "BD", "BC", "BB", "BA", "AZ", "AY", "AX", "AW", "AV", "AU", "AT", "AS", "AR", "AP", "AO", "AN", "AM", "AL", "AK", "AJ", "AI", "AH", "AG", "AF", "AE", "AD", "AC", "AB", "AA"};
     void LateUpdate()
     {
 #if !UNITY_SERVER
         if(GameManager.gm.BenimGemim != null)
         {
            if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 1)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) + 55; // 55 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 60) / 2); // 60 x 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 2)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) - 25; // 49 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 60) / 2); // 60 x 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 3)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) - 105; // 105 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 60) / 2); // 60 x 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 4)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) - 185; // 49 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 60) / 2); // 60 x 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 5)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) + 55; // 55 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 221) / 2); // 221 y 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 6)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) - 25; // 49 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 221) / 2); // 60 x 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 7)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) - 105; // 105 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 221) / 2); // 60 x 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 8)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) - 185; // 49 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 221) / 2); // 60 x 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 9)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) + 55; // 55 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 381) / 2); // 381 y 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 97)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) - 265; // 55 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 221) / 2); // 221 y 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
            else if (GameManager.gm.BenimGemim.GetComponent<Player>().harita == 98)
            {
                int yazdirilacakKonumX = (int)(transform.position.x / 2) - 265; // 49 x 0a denk gelsin die
                if (yazdirilacakKonumX < 0)
                {
                    yazdirilacakKonumX = 0;
                }
                int yazdirilacakKonumY = (int)((transform.position.y - 60) / 2); // 60 x 0a denk gelsin die
                if (yazdirilacakKonumY < 0)
                {
                    yazdirilacakKonumY = 0;
                }
                if (yazdirilacakKonumY >= 0 && yazdirilacakKonumY <= 55)
                {
#if UNITY_ANDROID
                    kordinatTextAndroid.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
#if UNITY_STANDALONE_WIN
                    kordinatText.text = yazdirilacakKonumX + "/" + dikeyKordinatHarfleri[yazdirilacakKonumY];
#endif
                }
            }
        }
 #endif
     }
}
