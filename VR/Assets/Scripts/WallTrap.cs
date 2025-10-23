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
            if (DisplayManager.instance != null && DisplayManager.instance.AllCorect)
            {
                wallCube.SetActive(true);
                Debug.Log("Tous corrects : le mur s'active !");
            }
            else
            {
                wallCube.SetActive(false);
                Debug.Log("Pas tous corrects : le mur reste inactif.");
            }
        }

        public override void TrapTriggered()
        {
            base.TrapTriggered();
        }

        public override void OnDurationEnded()
        {
            base.OnDurationEnded();
            wallCube.SetActive(false);
        }
    }
}