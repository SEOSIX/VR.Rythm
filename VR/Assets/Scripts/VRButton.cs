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
            button.transform.position = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            //jouer le sons lorqu'on appui
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == presser)
        {
            button.transform.position = new Vector3(0, 0.69f, 0);
            onRelease.Invoke();
        }
    }
}
