using System;
using UnityEngine;
using UnityEngine.Events;
public class VRButton : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;          //Button's event action OnPess(); just like a unityEvent
    public UnityEvent onRelease;        //Button's event action OnReleased(); just like a unityEvent
    private GameObject presser;


    private bool Ispressed;

    private void Start()
    {
        Ispressed = false;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (!Ispressed)
        {
            // gaffe aux get component évitables
            Renderer buttonColor = button.GetComponent<Renderer>();
            
            buttonColor.material.color = Color.blue;
            presser = other.gameObject;
            onPress.Invoke();
            Ispressed = true;
            //jouer le sons lorqu'on appui
            // -> TODO ?
        }
    }

    private void OnTriggerExit(Collider other)
    {
        presser = other.gameObject;
        if (other.gameObject == presser)
        {
            Ispressed = false;
            Renderer buttonColor = button.GetComponent<Renderer>();
            buttonColor.material.color = Color.white;
        }
    }
}
