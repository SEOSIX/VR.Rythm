using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class RewindEnnemy : MonoBehaviour
{
    
    public Ennemy ennemyScript;
    
    public MonsterMusique monsterMusique;
    public MusicManager musicManager;
    
    //General
    private bool canRewindEnnemy = true;
    
    [SerializeField] private Material canRewindMat;
    [SerializeField] private Material canNotRewindMat;
    [SerializeField] private AudioClip alarmSound;
    
    //1St path Rewind
    [SerializeField] private GameObject startPoint1;
    [SerializeField] private Renderer pressRend;
    

    private void Start()
    {
        pressRend.material = canRewindMat;
    }

    private void GoBack()
    {
        ennemyScript.ReturnFromStart(startPoint1);
        ennemyScript.selfIntPoint = 0;
    }

    public void ButtonClick()
    {
        musicManager.PlaySoundEffect(alarmSound);
        
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
        pressRend.material = canNotRewindMat;
        
        yield return new WaitForSeconds(30);
        
        canRewindEnnemy = true;
        pressRend.material = canRewindMat;
    }
    
}
