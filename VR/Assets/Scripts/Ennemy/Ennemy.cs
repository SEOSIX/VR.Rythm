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
    }

    // de la logique dans l'update -> red flag, fais des fonctions, là on sait pas ce que ca fait
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
        // j'pense tu peux soit mettre un return, soit un else if, t'as pas besoin de
        // comparer si tu rentres dans le premier, le deuxieme if sera forcément faux
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

    public void Walking(float speed)
    {
        if (agent == null || targetPoint == null || isStopped) return;

        agent.speed = speed;
        agent.isStopped = false;
        agent.SetDestination(targetPoint.position);
    }

    // elle servira ? sinon retire la d'ici et de ton interface
    public void Stop(float timeToStop)
    {
    }

    public void Attacking()
    {
        // gaffe aux magic numbers, hésite pas à créer un enum Scenes {menu = 0, game =1 } par exemple
        GameOverScript.LoadScene(2);
    }
    

    public void ReturnFromStart(GameObject warpTarget)
    {
        if (agent != null && warpTarget != null)
        {
            agent.Warp(warpTarget.transform.position);
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
