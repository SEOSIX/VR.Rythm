using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class RewindEnnemy : MonoBehaviour
{
    
    public Ennemy ennemyScript;
    public NavMeshAgent agent;
    
    public MonsterMusique monsterMusique;
    
    //General
    private bool canRewindEnnemy = true;
    
    //1St path Rewind
    [SerializeField] private GameObject startPoint1;
    

    private void GoBack()
    {
        ennemyScript.ReturnFromStart(startPoint1);
    }

    public void ButtonClick()
    {
        if (ennemyScript.inRewindZone1 && canRewindEnnemy)
        {
            GoBack();
            monsterMusique.MonsterMusiqueSource.clip = monsterMusique.FarAudio;
            monsterMusique.MonsterMusiqueSource.loop = true;
            StartCoroutine(Cooldown());
        }
        
    }
    
    IEnumerator Cooldown()
    {
        canRewindEnnemy = false;
        
        yield return new WaitForSeconds(30);

        canRewindEnnemy = true;
    }
    
}
