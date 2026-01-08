using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform targetPoint; // Note: targetPoint semble faire doublon avec la liste de points
    private NavMeshAgent agent;

    private bool isStopped = false;
    private Vector3 lastPosition;
    private bool isMoving;
    
    public int selfIntPoint = 0;
    public List<GameObject> pathPointList = new List<GameObject>();
    private Transform pathPointTransform;
    
    // 1
    public bool inRewindZone1 = false;

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
        
        if (pathPointList.Count > 0 && pathPointList[selfIntPoint] != null)
        {
            UpdatePathTarget();
        }
        else
        {
            Debug.LogWarning("La liste pathPointList est vide ou mal configurée sur " + gameObject.name);
        }
    }

    private void Update()
    {   

        if (agent != null && !isStopped)
        {
            AgentPathCheck();
        }


        if (agent != null)
        {
            isMoving = agent.velocity.magnitude > 0.1f;
        }

    }

    private void AgentPathCheck()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (selfIntPoint < pathPointList.Count - 1)
            {
                selfIntPoint++;
                UpdatePathTarget();
            }
            else if (selfIntPoint == pathPointList.Count - 1)
            {
                Attacking();
            }
        }
    }
    
    private void UpdatePathTarget()
    {
        if (pathPointList[selfIntPoint] != null)
        {
            pathPointTransform = pathPointList[selfIntPoint].transform;
            agent.SetDestination(pathPointTransform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            StopMovement();
            StartCoroutine(WaitForWallDeactivation(other.gameObject));
        }
        
        if (other.CompareTag("RewindZone"))
        {
            inRewindZone1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RewindZone"))
        {
            inRewindZone1 = false;
        }
    }

    private IEnumerator WaitForWallDeactivation(GameObject wall)
    {
        yield return new WaitUntil(() => wall == null || wall.activeSelf == false);
        ResumeMovement();
    }

    public void StopMovement()
    {
        if (agent == null) return;

        isStopped = true;
        agent.isStopped = true;
    }

    public void ResumeMovement()
    {
        if (agent == null) return;

        isStopped = false;
        agent.isStopped = false;
        
        UpdatePathTarget();
    }

    public void Walking(float speed)
    {
        if (agent == null || isStopped) return;

        agent.speed = speed;
        agent.isStopped = false;
        UpdatePathTarget();
    }

    public void Stop(float timeToStop)
    {
        //???
    }

    public void Attacking()
    {
        Debug.Log("GAME OVER : L'ennemi a atteint la fin !");
        GameOverScript.LoadScene(2);
    }
    
    public void ReturnFromStart(GameObject warpTarget)
    {
        if (agent != null && warpTarget != null)
        {
            selfIntPoint = 0; // On réinitialise l'index du chemin
            agent.Warp(warpTarget.transform.position);
            UpdatePathTarget();
        }
    }

    public bool IsStopped()
    {
        return isStopped || !isMoving;
    }

    public bool IsWalking()
    {
        return isMoving && !isStopped;
    }
}