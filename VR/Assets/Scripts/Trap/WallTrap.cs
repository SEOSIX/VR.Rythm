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
            isActivating = false;
        }
        
        public override void OnDurationEnded()
        {
            base.OnDurationEnded();
            wallCube.SetActive(false);
        }
    }
}