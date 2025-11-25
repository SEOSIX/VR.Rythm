using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class WallTrap : Trap
    {
        [SerializeField] private GameObject wallCube;

        public bool isActivating;
        
        
        private void OnEnable()
        {
            if (DisplayManager.instance != null)
            {
                DisplayManager.instance.OnAllPatternsCleared += ShowWall;
                Debug.Log("correct");
            }
            else
            {
                Debug.Log("null");
            }
        }

        
        private void OnDisable()
        {
            if (DisplayManager.instance != null)
                DisplayManager.instance.OnAllPatternsCleared -= ShowWall;
        }

        public override void ActivateTrap()
        {
            base.ActivateTrap();
            isActivating = true;
        }

        public override void TrapTriggered()
        {
            base.TrapTriggered();
        }
        
        private void ShowWall()
        {
            if (!isActivating)
                return;
            wallCube.SetActive(true);
            TrapManager.ChangeColor(Color.red);
            isActivating = false;
        }
        
        public override void OnDurationEnded()
        {
            base.OnDurationEnded();
            wallCube.SetActive(false);
            TrapManager.ChangeColor(Color.green);
        }
    }
}