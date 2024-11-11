﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerExampleScript : MonoBehaviour
{
    private Renderer r;
    void Start()
    {
        r = GetComponent<Renderer>();
    }
    public void ChooseColorButtonClick()
    {
        ColorPicker.Create(r.sharedMaterial.color, "Choose the cube's color!", SetColor, ColorFinished, true);
    }
  
    private void SetColor(Color currentColor)
    {
        r.sharedMaterial.color = currentColor;
    }

    private void ColorFinished(Color finishedColor)
    {
        Debug.Log("You chose the color " + ColorUtility.ToHtmlStringRGBA(finishedColor));
        GameManager.gm.BayrakIconProfilBackround.color = finishedColor;
        GameManager.gm.BayrakRBTbacround.color = finishedColor;
    }
}
