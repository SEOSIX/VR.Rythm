using UnityEngine;

public class TriggerMonsterMusique : MonoBehaviour
{
    public event System.Action<Collider> EnteredTrigger;
    public event System.Action<Collider> ExitedTrigger;

    private void OnTriggerEnter(Collider other)
    {
        EnteredTrigger?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ExitedTrigger?.Invoke(other);
    }
}
