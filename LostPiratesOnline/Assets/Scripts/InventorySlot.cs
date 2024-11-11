using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public int AktarilacakYer = -1;

    public void OnDrop(PointerEventData eventData)
    {
        int aktarilacakTopSayisi = 0;
        if (AktarilacakYer == 1)
        {
            aktarilacakTopSayisi = (GameManager.gm.BenimGemim.GetComponent<Player>().oyuncuMaksTopYuvasi - GameManager.gm.BenimGemim.GetComponent<Player>().oyuncuDonanilmisTopSayisi);
        }
        else
        {
            if (GameManager.gm.tutulanTopId == 0)
            {
                aktarilacakTopSayisi = GameManager.gm.BenimGemim.GetComponent<Player>().oyuncuOnBesKilolukTopGemi;
            }
            else if(GameManager.gm.tutulanTopId == 1)
            {
                aktarilacakTopSayisi = GameManager.gm.BenimGemim.GetComponent<Player>().oyuncuYirmiBesKilolukTopGemi;
            }
            else if(GameManager.gm.tutulanTopId == 2)
            {
                aktarilacakTopSayisi = GameManager.gm.BenimGemim.GetComponent<Player>().oyuncuYirmiYediBucukKilolukTopGemi;
            }
            else if (GameManager.gm.tutulanTopId == 3)
            {
                aktarilacakTopSayisi = GameManager.gm.BenimGemim.GetComponent<Player>().oyuncuOtuzKilolukTopGemi;
            }
            else if (GameManager.gm.tutulanTopId == 4)
            {
                aktarilacakTopSayisi = GameManager.gm.BenimGemim.GetComponent<Player>().oyuncuOtuzBesKilolukTopGemi;
            }
        }
        if (aktarilacakTopSayisi > 0)
        {
            for (int i = 0; i <= aktarilacakTopSayisi; i++)
            {
                GameManager.gm.BenimGemim.GetComponent<Player>().TopAktar(GameManager.gm.tutulanTopId, AktarilacakYer);
            }
        }
        GameManager.gm.tutulanTopId = -1;
    }
}
