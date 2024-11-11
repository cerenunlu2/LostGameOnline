using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class SimpleTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SimpleTooltipStyle simpleTooltipStyle;
    public string infoLeft = "Hello";
    public string infoRight = "";
    private STController tooltipController;
    private EventSystem eventSystem;
    private bool cursorInside = false;
    private bool isUIObject = false;
    private bool showing = false;
    public int YetenekID = 0;


    private void Awake()
    {
        if (YetenekID == 1)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 490] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 508] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 2)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 491] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 509] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 3)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 492] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 510] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 4)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 493] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 511] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 5)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 494] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 512] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 6)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 495] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 513] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 7)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 496] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 514] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 8)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 497] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 515] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 9)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 498] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 516] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 10)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 499] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 517] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 11)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 500] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 518] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 12)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 501] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 519] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 13)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 502] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 520] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 14)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 503] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 521] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 15)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 504] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 522] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 16)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 505] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 523] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 17)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 506] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 524] + "</color>";
            infoRight = "";
        }
        else if (YetenekID == 18)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 507] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 525] + "</color>";
            infoRight = "";
        }
        else if (GetComponent<TopYuvasi>().topId == 0)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 10] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 83] + "</color>";
            infoRight = "";
        }
        else if (GetComponent<TopYuvasi>().topId == 1)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 11] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 84] + "</color>";
            infoRight = "";
        }
        else if (GetComponent<TopYuvasi>().topId == 2)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 12] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 85] + "</color>";
            infoRight = "";
        }
        else if (GetComponent<TopYuvasi>().topId == 3)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 13] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 86] + "</color>";
            infoRight = "";
        }
        else if (GetComponent<TopYuvasi>().topId == 4)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 14] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 87] + "</color>";
            infoRight = "";
        }
        else if (GetComponent<TopYuvasi>().topId == 5)
        {
            infoLeft = GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 582] + "<br>  <color=#ffffff>" + GameManager.gm.dilDegistirme.GetComponent<Dildegistirme>().EkranaYazilacakOlan[PlayerPrefs.GetInt("dil"), 87] + "</color>";
            infoRight = "";
        }
        eventSystem = FindObjectOfType<EventSystem>();
        tooltipController = FindObjectOfType<STController>();

        // Add a new tooltip prefab if one does not exist yet
        if (!tooltipController)
        {
            tooltipController = AddTooltipPrefabToScene();
        }
        if (!tooltipController)
        {
            Debug.LogWarning("Could not find the Tooltip prefab");
            Debug.LogWarning("Make sure you don't have any other prefabs named `SimpleTooltip`");
        }

        if (GetComponent<RectTransform>())
            isUIObject = true;

        // Always make sure there's a style loaded
        if (!simpleTooltipStyle)
            simpleTooltipStyle = Resources.Load<SimpleTooltipStyle>("STDefault");
        if (GameManager.gm.Tershane.activeSelf)
        {
            if (GetComponent<TopYuvasi>().topId == -1)
            {
                isUIObject = false;
            }
        }

    }

    private void Update()
    {
        if (!cursorInside)
            return;
       

        tooltipController.ShowTooltip();
    }

    public static STController AddTooltipPrefabToScene()
    {
        return Instantiate(Resources.Load<GameObject>("SimpleTooltip")).GetComponentInChildren<STController>();
    }

    private void OnMouseOver()
    {
        if (isUIObject)
            return;

        if (eventSystem)
        {
            if (eventSystem.IsPointerOverGameObject())
            {
                HideTooltip();
                return;
            }
        }
        ShowTooltip();
    }

    private void OnMouseExit()
    {
        if (isUIObject)
            return;
        HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isUIObject)
            return;
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isUIObject)
            return;
        HideTooltip();
    }

    public void ShowTooltip()
    {
        showing = true;
        cursorInside = true;

        // Update the text for both layers
        tooltipController.SetCustomStyledText(infoLeft, simpleTooltipStyle, STController.TextAlign.Left);
        tooltipController.SetCustomStyledText(infoRight, simpleTooltipStyle, STController.TextAlign.Right);

        // Then tell the controller to show it
        tooltipController.ShowTooltip();
    }

    public void HideTooltip()
    {
        if (!showing)
            return;
        showing = false;
        cursorInside = false;
        tooltipController.HideTooltip();
    }

    private void Reset()
    {
        // Load the default style if none is specified
        if (!simpleTooltipStyle)
            simpleTooltipStyle = Resources.Load<SimpleTooltipStyle>("STDefault");

        // If UI, nothing else needs to be done
        if (GetComponent<RectTransform>())
            return;

        // If has a collider, nothing else needs to be done
        if (GetComponent<Collider>())
            return;

        // There were no colliders found when the component is added so we'll add a box collider by default
        // If you are making a 2D game you can change this to a BoxCollider2D for convenience
        // You can obviously still swap it manually in the editor but this should speed up development
        gameObject.AddComponent<BoxCollider>();
    }
}
