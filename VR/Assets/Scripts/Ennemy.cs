using System;
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

    [Header("Path")]
    [SerializeField] private Transform[] pathPoints;

    private int loopsToWait;
    private int currentLoops;
    private bool isStopped = false;
    [HideInInspector]
    public float currentStopTime;

    private Vector3 lastPosition;
    private bool isMoving;
    
    private float progress = 0f;

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
        progress = Mathf.Clamp01(progress + speed);

        float pathProgress = progress * (pathPoints.Length - 1);
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

        if (progress >= 1f)
        {
            Attacking();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            for (int i = 0; i < TrapManager.instance.traps.Count; i++)
            {
             currentStopTime += TrapManager.instance.traps[i].TrapDuration;
            }
        }
    }

    public void Stop(int timeToStop)
    {
        if (!isStopped)
        {
            StartCoroutine(StopRoutine(timeToStop));
            currentStopTime = timeToStop;
        }
    }

    private IEnumerator StopRoutine(float duration)
    {
        isStopped = true;
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }

    public void Attacking()
    {
        Debug.Log("GameOver");
    }

    public void ReturnFromStart()
    {
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