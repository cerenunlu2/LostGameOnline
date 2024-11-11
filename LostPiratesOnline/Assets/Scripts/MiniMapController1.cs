using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMapController1 : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.gm.Kamera.GetComponent<DragCamera2D>().followTarget = null;
#if UNITY_ANDROID
                GameManager.gm.Dumen.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
        GameManager.gm.WindowsDumen.SetActive(true);
#endif

        Vector2 localCursor = new Vector2(0, 0);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RawImage>().rectTransform, eventData.pressPosition, eventData.pressEventCamera, out localCursor))
        {
            Texture tex = GetComponent<RawImage>().texture;
            Rect r = GetComponent<RawImage>().rectTransform.rect;
            //Using the size of the texture and the local cursor, clamp the X,Y coords between 0 and width - height of texture
            float coordX = Mathf.Clamp(0, (((localCursor.x - r.x) * tex.width) / r.width), tex.width);
            float coordY = Mathf.Clamp(0, (((localCursor.y - r.y) * tex.height) / r.height), tex.height);
            //Convert coordX and coordY to % (0.0-1.0) with respect to texture width and height
            float recalcX = coordX / tex.width;
            float recalcY = coordY / tex.height;
            localCursor = new Vector2(recalcX, recalcY);
            CastMiniMapRayToWorld(localCursor);
        }

    }
    private bool isDragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        GameManager.gm.Kamera.GetComponent<DragCamera2D>().followTarget = null;
#if UNITY_ANDROID
                GameManager.gm.Dumen.SetActive(true);
#endif
#if UNITY_STANDALONE_WIN
        GameManager.gm.WindowsDumen.SetActive(true);
#endif
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 localCursor = new Vector2(0, 0);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RawImage>().rectTransform, eventData.position, eventData.pressEventCamera, out localCursor))
            {
                Texture tex = GetComponent<RawImage>().texture;
                Rect r = GetComponent<RawImage>().rectTransform.rect;
                //Using the size of the texture and the local cursor, clamp the X,Y coords between 0 and width - height of texture
                float coordX = Mathf.Clamp(0, (((localCursor.x - r.x) * tex.width) / r.width), tex.width);
                float coordY = Mathf.Clamp(0, (((localCursor.y - r.y) * tex.height) / r.height), tex.height);
                //Convert coordX and coordY to % (0.0-1.0) with respect to texture width and height
                float recalcX = coordX / tex.width;
                float recalcY = coordY / tex.height;
                localCursor = new Vector2(recalcX, recalcY);
                CastMiniMapRayToWorld(localCursor);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void CastMiniMapRayToWorld(Vector2 localCursor)
    {
        Ray miniMapRay = GameManager.gm.MiniMapCamera.GetComponent<Camera>().ScreenPointToRay(new Vector2(localCursor.x * GameManager.gm.MiniMapCamera.GetComponent<Camera>().pixelWidth, localCursor.y * GameManager.gm.MiniMapCamera.GetComponent<Camera>().pixelHeight));
        GameManager.gm.MainCamera.transform.position = new Vector3(miniMapRay.origin.x, miniMapRay.origin.y, -10);
    }
}
