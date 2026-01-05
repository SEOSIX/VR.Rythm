using System.Collections;
using UnityEngine;

public class DisplayBug : MonoBehaviour
{
    [SerializeField] private GameObject display;
    [SerializeField] private int RandomTick;
    
    
    private int countPoping;

    
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private IEnumerator DisplayBog()
    {
        int currentCountPoping = 0;
        while (currentCountPoping < countPoping)
        {
            display.SetActive(false);
            yield return new WaitForSeconds(0.01f);
            display.SetActive(true);
            currentCountPoping++;
        }
    }
}
