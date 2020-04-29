using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    float refractiveIndex = 0;

    public void SetRefractiveIndex(float ior)
    {
        refractiveIndex = ior;
    }

    public float GetRefractiveIndex()
    {
        return refractiveIndex;
    }
}
