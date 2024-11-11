using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Canvas canvas;
    public int KaydirmaTipi = 0;

    void Start()
    {
        if (KaydirmaTipi == 1)
        {
            KonumlariYukle();
        }
    }

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(new Vector2(position.x, position.y - (((RectTransform)transform).rect.height / 2)));
    }

    public void OnDrop(PointerEventData eventData)
    {
        PlayerPrefs.SetFloat("oldurmaTablosuKonumX", transform.position.x);
        PlayerPrefs.SetFloat("oldurmaTablosuKonumY", transform.position.y);
    }

    public void KonumlariYukle()
    {
        transform.position = new Vector2(PlayerPrefs.GetFloat("oldurmaTablosuKonumX", (canvas.GetComponent<RectTransform>().rect.width / 2)), PlayerPrefs.GetFloat("oldurmaTablosuKonumY", (canvas.GetComponent<RectTransform>().rect.height / 2)));
    }

    public void KonumlariSifirla()
    {
        PlayerPrefs.SetFloat("oldurmaTablosuKonumX", (canvas.GetComponent<RectTransform>().rect.width / 2));
        PlayerPrefs.SetFloat("oldurmaTablosuKonumY", (canvas.GetComponent<RectTransform>().rect.height / 2));
        transform.position = new Vector2(PlayerPrefs.GetFloat("oldurmaTablosuKonumX", (canvas.GetComponent<RectTransform>().rect.width / 2)), PlayerPrefs.GetFloat("oldurmaTablosuKonumY", (canvas.GetComponent<RectTransform>().rect.height / 2)));

    }
}
