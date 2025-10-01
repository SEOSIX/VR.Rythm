using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DefaultNamespace;

public class Ennemy : MonoBehaviour, IEnnemy
{
    public static Ennemy instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string enemyName;
    [SerializeField] private AudioSource scream;
    [SerializeField] private Slider progressBar;

    [Header("Path")]
    [SerializeField] private Transform[] pathPoints;

    private int loopsToWait;
    private int currentLoops;
    private bool isStopped = false;

    private Vector3 lastPosition;
    private bool isMoving;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (transform.position != lastPosition)
        {
            isMoving = true;
            lastPosition = transform.position;
        }
        else
        {
            isMoving = false;
        }
    }

    private void OnEnable()
    {
        ClockManager.OnLoopComplete += OnLoopComplete;
        SetNewRandomLoops();
    }

    private void OnDisable()
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
        int chance = 5;
        if (UnityEngine.Random.Range(0, 100) < chance)
        {
            loopsToWait = 1;
        }
        else
        {
            loopsToWait = UnityEngine.Random.Range(2, 6);
        }
        Debug.Log($"{enemyName} attend {loopsToWait} loops avant de bouger.");
    }

    public void Walknig(float speed)
    {
        if (pathPoints == null || pathPoints.Length < 2) return;

        progressBar.value = Mathf.Clamp01(progressBar.value + speed);

        float pathProgress = progressBar.value * (pathPoints.Length - 1);
        int segmentIndex = Mathf.FloorToInt(pathProgress);
        float t = pathProgress - segmentIndex;

        if (segmentIndex < pathPoints.Length - 1)
        {
            transform.position = Vector3.Lerp(pathPoints[segmentIndex].position, pathPoints[segmentIndex + 1].position, t);
        }
        else
        {
            transform.position = pathPoints[pathPoints.Length - 1].position;
        }

        if (progressBar.value >= 1f)
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

    private IEnumerator StopRoutine(int duration)
    {
        isStopped = true;
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }

    public void Attacking()
    {
        Debug.Log("GameOver");
        //if (scream != null) scream.Play();
    }

    public void ReturnFromStart()
    {
        progressBar.value = 0;
        ResetPosition();
        SetNewRandomLoops();
    }

    private void ResetPosition()
    {
        if (pathPoints != null && pathPoints.Length > 0)
        {
            transform.position = pathPoints[0].position;
        }
    }

    public bool IsStopped()
    {
        return isStopped && !isMoving;
    }

    public bool IsWalking()
    {
        return isMoving && !isStopped;
    }
}