using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class WallTrap : Trap
    {
        [Header("Settings & Visuals")]
        [SerializeField] private GameObject wallCube;

        [Header("State")]
        public bool isActivating;

        #region === Unity Lifecycle ===

        private void Start()
        {
            if (wallCube != null)
            {
                wallCube.SetActive(false);
            }
        }

        private void OnEnable()
        {
            // On s'abonne aux deux événements du DisplayManager
            if (DisplayManager.instance != null)
            {
                DisplayManager.instance.OnAllPatternsCleared += ShowWall;
                DisplayManager.instance.OnPatternFailed += CancelTrap;
            }
        }

        private void OnDisable()
        {
            // On se désabonne pour éviter les fuites de mémoire
            if (DisplayManager.instance != null)
            {
                DisplayManager.instance.OnAllPatternsCleared -= ShowWall;
                DisplayManager.instance.OnPatternFailed -= CancelTrap;
            }
        }

        #endregion

        #region === Trap Logic ===

        public override void ActivateTrap()
        {
            // Ce piège passe en mode "attente de réussite"
            isActivating = true; 
            base.ActivateTrap();
        }

        private void ShowWall()
        {
            // Si ce n'est pas ce piège spécifique qui a été lancé, on ignore
            if (!isActivating) return;

            if (wallCube != null)
            {
                wallCube.SetActive(true);
            }
            
            StartCoroutine(DurationEnded());
            isActivating = false;
        }

        private void CancelTrap()
        {
            // Si le mini-jeu échoue, on réinitialise l'état
            isActivating = false;
        }

        public override void OnDurationEnded()
        {
            // Appelé quand la coroutine de durée est finie
            if (wallCube != null)
            {
                wallCube.SetActive(false);
            }
            isActivating = false;
        }

        #endregion
    }
}