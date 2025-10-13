using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class WallTrap : Trap
    {
        [SerializeField] private GameObject wallCube;
        public override void ActivateTrap()
        {
            base.ActivateTrap();
            wallCube.SetActive(true);
        }

        public override void TrapTriggered()
        {
            
        }

        public override void OnDurationEnded()
        {
            base.OnDurationEnded();
            wallCube.SetActive(false);
        }
    }
}