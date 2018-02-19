using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorRampPost : MonoBehaviour
{
    private Material mat;
    public Gradient ColorRamp;
    public Texture2D ColorTexture;
    Gradient tempRamp;
    Texture2D rampTexture;

    int GradientResolution = 512;

    void Awake()
    {
        mat = new Material(Shader.Find("Custom/ColorRamp"));
        tempRamp = new Gradient();
    }

    void Update()
    {
        if(ColorTexture == null && ColorRamp != null && !QuickCompare(tempRamp, ColorRamp))
        {
            tempRamp.SetKeys(ColorRamp.colorKeys, ColorRamp.alphaKeys);
            GenerateRampTexture();
        }
    }

    void GenerateRampTexture()
    {
        if(rampTexture == null)
            rampTexture = new Texture2D(GradientResolution, 1, TextureFormat.ARGB32, false);
        for(int i=0; i< GradientResolution; i++)
        {
            rampTexture.SetPixel(i, 0, ColorRamp.Evaluate(i / (float)GradientResolution));
        }
        rampTexture.Apply();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(ColorTexture == null)
            mat.SetTexture("_Ramp", rampTexture);
        else
            mat.SetTexture("_Ramp", ColorTexture);
        Graphics.Blit(source, destination, mat);
    }

    bool QuickCompare(Gradient gradient, Gradient otherGradient, int testInterval = 24)
    {
        // Tests the gradient at a couple points to see if they are the same, may fail is various cases
        if (gradient == null || otherGradient == null)
        {
            Debug.Log("One gradient null");
            return false;
        }

        for (int i = 0; i < testInterval; i++)
        {
            float time = (float)i / (float)(testInterval - 1);
            if (gradient.Evaluate(time) != otherGradient.Evaluate(time))
            {
                Debug.Log("Gradient didn't match at pt: " + i);
                return false;
            }
        }
        // All the test points match
        Debug.Log("Pt matched");
        return true;
    }
}
