using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour, IEnnemy
{
    public static Ennemy instance { get; private set; }

    [Header("Settings")]
    public string enemyName;
    [SerializeField] private AudioSource scream;

    [Header("Navigation")]
    [SerializeField] private Transform targetPoint;
    private NavMeshAgent agent;

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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float dist = agent.remainingDistance;
        if (agent != null && targetPoint != null && !isStopped)
        {
            agent.SetDestination(targetPoint.position);
        }

        if (transform.position != lastPosition)
        {
            isMoving = true;
            lastPosition = transform.position;
        }
        else
            isMoving = false;
        
        if (dist != Mathf.Infinity && agent.pathStatus==NavMeshPathStatus.PathComplete && agent.remainingDistance !=0)
        {
            Attacking();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            StopMovement();
            StartCoroutine(WaitForWallDeactivation(other.gameObject));
        }
    }

    private IEnumerator WaitForWallDeactivation(GameObject wall)
    {
        yield return new WaitUntil(() => wall.activeSelf == false);

        ResumeMovement();
    }
    public void StopMovement()
    {
        if (agent == null) return;

        isStopped = true;
        agent.isStopped = true;
        agent.ResetPath();
    }

    public void ResumeMovement()
    {
        if (agent == null || targetPoint == null) return;

        isStopped = false;
        agent.isStopped = false;
        agent.SetDestination(targetPoint.position);
    }

    public void Walknig(float speed)
    {
        if (agent == null || targetPoint == null || isStopped) return;

        agent.speed = speed;
        agent.isStopped = false;
        agent.SetDestination(targetPoint.position);
    }

    public void Stop(float timeToStop)
    {
    }

    public void Attacking()
    {
        GameOverScript.LoadScene(2);
    }

    public void ReturnFromStart()
    {
        if (agent != null && targetPoint != null)
        {
            agent.Warp(targetPoint.position);
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
