using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    
    public static ClockManager instance{get; private set;}
    public Image[] images;
    public int groupSize = 2;
    public float interval = 0.5f;

    
    private bool inTempo = false;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartBlinking();
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log(IsInTempo());
        }
    }
    
    void StartBlinking()
    {
        StopAllCoroutines();
        StartCoroutine(BlinkGroup());
    }

    IEnumerator BlinkGroup()
    {
        int total = images.Length;
        while (true)
        {
            for (int i = 0; i < total; i++)
            {
                SetAlpha(images[i], 0);
            }

            for (int i = 0; i < total; i += groupSize)
            {
                for (int j = i; j < i + groupSize && j < total; j++)
                {
                    IsInTempo();
                    SetAlpha(images[j], 1);
                }
                inTempo = true;
                yield return new WaitForSeconds(interval);
                inTempo = false;
            }

            for (int i = 0; i < total; i++)
            {
                SetAlpha(images[i], 0);
            }

            yield return new WaitForSeconds(interval);
        }
    }

    void SetAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
    public bool IsInTempo()
    {
        return inTempo;
    }

}
