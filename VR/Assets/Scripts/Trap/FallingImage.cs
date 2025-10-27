using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FallingImage : MonoBehaviour
    {
        public int imageType;
        private Image img;
        public int spawnIndex;

        private void Awake()
        {
            img = GetComponent<Image>();
        }

        public void SetColor(Color colorToSet)
        {
            if (img != null)
            {
                img.color = colorToSet;
            }
        }
    }
}