using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class RewindEnnemy : MonoBehaviour
{

    public Ennemy ennemyScript;
    public NavMeshAgent agent;
    
    
    //General
    private bool canRewindEnnemy = true;
    
    //1St path Rewind
    [SerializeField] private GameObject startPoint1;
    
    // pense à bien retirer les fonctions de base de monobehavior,
    // elles créent des register sur les monobehaviors, qui du coup appellent du code inutiles
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void GoBack()
    {
        ennemyScript.ReturnFromStart(startPoint1);
    }

    public void ButtonClick()
    {
        if (ennemyScript.inRewindZone1 && canRewindEnnemy)
        {
            GoBack();
            ennemyScript.inRewindZone1 = false;
        }
        
    }
    
    IEnumerator Cooldown()
    {
        canRewindEnnemy = false;
        
        yield return new WaitForSeconds(30); // gaffe aux magic numbers, soit une const, soit un scriptable, soit un serializefield, etc

        canRewindEnnemy = true;
    }
    
}
