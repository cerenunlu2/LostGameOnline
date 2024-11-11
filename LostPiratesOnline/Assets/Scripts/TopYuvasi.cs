using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopYuvasi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int topId = -1;
    [Header("UI")]
    public Image image;
    // Start is called before the first frame update

    [HideInInspector] public Transform parentAfterDrag; 

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.gm.tutulanTopId = topId;
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    public void TopAktarGemiye(int AktarilacakYer)
    {
         if (GameManager.gm.BenimGemim.GetComponent<Player>().oyuncuDonanilmisTopSayisi < GameManager.gm.BenimGemim.GetComponent<Player>().oyuncuMaksTopYuvasi || AktarilacakYer == 0)
          {
              GameManager.gm.BenimGemim.GetComponent<Player>().TopAktar(topId, AktarilacakYer);
          }
    }
    public void TopSec()
    {
        if (gameObject.GetComponent<TopYuvasi>().topId > -1)
        {
            for (int x = 0; x < 20; x++)
            {
                GameManager.gm.depodakiToplarSlot[x].gameObject.transform.Find("SelectedSlot").gameObject.SetActive(false);
            }
            GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuTopSecmeDUrumu = true;
            transform.Find("SelectedSlot").gameObject.SetActive(true);
            GameManager.gm.TopSatmaMiktarSlider.maxValue = int.Parse(transform.Find("Text (Legacy)").GetComponent<Text>().text);
            GameManager.gm.TopSatmaMiktarSlider.minValue = 1;
            if (gameObject.GetComponent<TopYuvasi>().topId == 0)
            {
                GameManager.gm.TopSatmaPaneliTopIMG.sprite = GameManager.gm.TopImage[0];
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuTopSatisFiyati = 25;
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuSatilacakTopID = 0;
                GameManager.gm.TopSatisKullanButton.GetComponent<TopYuvasi>().topId = 0;
            }
            else if (gameObject.GetComponent<TopYuvasi>().topId == 1)
            {
                GameManager.gm.TopSatmaPaneliTopIMG.sprite = GameManager.gm.TopImage[1];
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuTopSatisFiyati = 125;
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuSatilacakTopID = 1;
                GameManager.gm.TopSatisKullanButton.GetComponent<TopYuvasi>().topId = 1;
            }
            else if (gameObject.GetComponent<TopYuvasi>().topId == 2)
            {
                GameManager.gm.TopSatmaPaneliTopIMG.sprite = GameManager.gm.TopImage[2];
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuTopSatisFiyati = 400;
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuSatilacakTopID = 2;
                GameManager.gm.TopSatisKullanButton.GetComponent<TopYuvasi>().topId = 2;
            }
            else if (gameObject.GetComponent<TopYuvasi>().topId == 3)
            {
                GameManager.gm.TopSatmaPaneliTopIMG.sprite = GameManager.gm.TopImage[3];
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuTopSatisFiyati = 4000;
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuSatilacakTopID = 3;
                GameManager.gm.TopSatisKullanButton.GetComponent<TopYuvasi>().topId = 3;
            }
            else if (gameObject.GetComponent<TopYuvasi>().topId == 4)
            {
                GameManager.gm.TopSatmaPaneliTopIMG.sprite = GameManager.gm.TopImage[4];
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuTopSatisFiyati = 15000;
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuSatilacakTopID = 4;
                GameManager.gm.TopSatisKullanButton.GetComponent<TopYuvasi>().topId = 4;
            }
            else if (gameObject.GetComponent<TopYuvasi>().topId == 5)
            {
                GameManager.gm.TopSatmaPaneliTopIMG.sprite = GameManager.gm.TopImage[5];
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuTopSatisFiyati = 35000;
                GameManager.gm.BenimGemim.GetComponent<Player>().OyuncuSatilacakTopID = 5;
                GameManager.gm.TopSatisKullanButton.GetComponent<TopYuvasi>().topId = 5;
            }
            GameManager.gm.TopSatisFiyatBelirle();
        }
    }
}
