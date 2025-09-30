using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour, IEnnemy
{
    
    public static Ennemy instance;

    [SerializeField] private string Name;
    [SerializeField] private AudioSource Scream;
    [SerializeField] private Slider progressBar;

    private int loopsToWait; 
    private int currentLoops;

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
        currentLoops++;
        if (currentLoops >= loopsToWait)
        {
            Walknig(1f);
            SetNewRandomLoops(); 
        }
    }

    private void SetNewRandomLoops()
    {
        currentLoops = 0;
        loopsToWait = Random.Range(2, 2);
    }

    public void Walknig(float speed)
    {
        progressBar.value += 0.1f;
    }
    public void Stop(int timeToStop)
    {
        //arrète sa course pour x time
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
        SetNewRandomLoops();
        
        //reset la position de l'ennemi dans la scene, efface sa progression
    }
}
