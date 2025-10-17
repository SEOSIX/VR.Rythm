using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FallingImage : MonoBehaviour
    {
        public int imageType;
        private Image img;

        private void Awake()
        {
            img = GetComponent<Image>();
        }

        public void SetGreen()
        {
            if (img != null)
            {
                img.color = Color.green;
                Destroy(gameObject, 0.5f);
            }
        }
    }
}