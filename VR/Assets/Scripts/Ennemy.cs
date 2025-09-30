using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour, IEnnemy
{
    
    public static Ennemy instance;

    [SerializeField] private string Name;
    [SerializeField] private AudioSource Scream;
    [SerializeField] private Slider progressBar;

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private int loopsToWait;  
    private int currentLoops; 
    private bool isStopped = false;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        ClockManager.OnLoopComplete += OnLoopComplete;
        SetNewRandomLoops();
    }

    void OnDisable()
    {
        ClockManager.OnLoopComplete -= OnLoopComplete;
    }

    private void OnLoopComplete()
    {
        if (isStopped) return;

        currentLoops++;
        if (currentLoops >= loopsToWait)
        {
            Walknig(0.1f); 
            SetNewRandomLoops();
        }
    }

    private void SetNewRandomLoops()
    {
        currentLoops = 0;
        loopsToWait = Random.Range(2, 2); 
    }

    public void Walknig(float progressAmount)
    {
        progressBar.value = Mathf.Clamp01(progressBar.value + progressAmount);
        if (startPoint != null && endPoint != null)
        {
            transform.position = Vector3.Lerp(
                startPoint.position,
                endPoint.position,
                progressBar.value
            );
        }
        if (progressBar.value <= 0f)
        {
            Attacking();
        }
    }
    
    public void Stop(int timeToStop)
    {
        if (!isStopped)
        {
            StartCoroutine(StopRoutine(timeToStop));
        }
    }

    private System.Collections.IEnumerator StopRoutine(int duration)
    {
        isStopped = true;
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }


    public void Attacking()
    {
        if (progressBar.value <= 0f)
        {
            Debug.Log("GameOver");
        }
        //Une fois dans la pièce il peut attaquer le jouer et GameOver et screamer joué
    }

    public void ReturnFromStart()
    {
        progressBar.value = 0;
        ResetPosition();
        SetNewRandomLoops();
    }
    
    private void ResetPosition()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
    }
}
