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
        public List<Trap> traps = new List<Trap>();
        [SerializeField] private Button startRythmutton;
        public static Image lastSelected;
        
        private void Awake()
        {
            instance = this;
        }


        public void WaitForActivating(int i)
        {
            startRythmutton.onClick.AddListener(() => ActivateTrap(i));
            //Monitor.SetActive(true);
        }
        
        
        public void ActivateTrap(int index)
        {
            traps[index].ActivateTrap();
            Debug.Log($"trap {index} astarted");
        }

        public void OnButtonClicked(Image btn)
        {
            lastSelected = btn;
        }

        public static void ChangeColor(Color color)
        {
            lastSelected.color = color;
        }
    }
}