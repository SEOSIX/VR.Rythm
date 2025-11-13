using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isOpen = false;
    private bool isAnimating = false;

    public void Toggle()
    {
        if (isAnimating) return;

        isAnimating = true;
        if (!isOpen)
        {
            animator.SetTrigger("Open");
            Debug.Log("Ordinateur activé");
        }
        else
        {
            animator.SetTrigger("Close");
            Debug.Log("Ordinateur désactivé");
        }

        isOpen = !isOpen;
    }
    public void OnAnimationEnd()
    {
        isAnimating = false;
        Debug.Log("Animation terminée");
    }
}