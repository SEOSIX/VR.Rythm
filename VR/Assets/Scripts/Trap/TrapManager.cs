using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class TrapManager : MonoBehaviour
    {
        
        public static TrapManager instance { get; private set; }
        public List<Trap> traps = new List<Trap>();


        private void Awake()
        {
            instance = this;
        }

        public void ActivateTrap(int index)
        {
            if (index >= 0 && index < traps.Count)
                traps[index].ActivateTrap();
        }
    }
}