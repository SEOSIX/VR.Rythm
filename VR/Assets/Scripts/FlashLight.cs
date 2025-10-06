using System;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    private bool lightActive;

    private void Start()
    {
        lightActive = false;
    }


    public void ActivateLight(GameObject light)
    {
        lightActive = !lightActive;
        light.SetActive(lightActive);
    }
    
    
}
