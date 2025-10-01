using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DisplayManager : MonoBehaviour
    {
        [SerializeField] private Image displayImage; 
        [SerializeField] private Sprite idleSprite;
        [SerializeField] private Sprite walkingSprite;

        private Ennemy enemy;

        void Start()
        {
            enemy = Ennemy.instance;
            if (displayImage != null && idleSprite != null)
            {
                displayImage.sprite = idleSprite;
            }
        }

        void Update()
        {
            if (enemy == null) return;
            
            if (enemy.IsWalking())
            {
                displayImage.sprite = walkingSprite;
            }
            if(enemy.IsStopped())
            {
                displayImage.sprite = idleSprite;
            }
        }
    }
}