using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TrapManager : MonoBehaviour
    {
        public static TrapManager instance { get; private set; }
        
        [Header("Public var")]
        public List<Trap> traps = new List<Trap>();
        [Header("Serialized var")]
        [SerializeField] private Button startRythmutton;
        [SerializeField] private GameObject monitor;
        
        public static Image lastSelected;

        private bool isPressed = false;
        public static bool tryActivateTrap = false;
        
        private void Awake()
        {
            instance = this;
            monitor.SetActive(false);
        }


        public void WaitForActivating(int i)
        {
            startRythmutton.onClick.RemoveAllListeners();
            if (isPressed)
            {
                startRythmutton.gameObject.SetActive(true);
            }
            startRythmutton.onClick.AddListener(() => ActivateTrap(i));
            monitor.SetActive(true);
            tryActivateTrap = true;
        }
        
        public void ForceResetButton()
        {
            if (startRythmutton != null)
            {
                startRythmutton.onClick.RemoveAllListeners();
                
                startRythmutton.gameObject.SetActive(false); // Enleve le button start de rythm
                
            }
        }
        
        public void ActivateTrap(int index)
        {
            traps[index].ActivateTrap();
            isPressed = true;
            startRythmutton.onClick.RemoveAllListeners();
        }

        public void OnButtonClicked(Image btn)
        {
            lastSelected = btn;
        }
    }
}