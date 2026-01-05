using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TutoManager : MonoBehaviour
{
    [Header("Textes")] 
    [SerializeField] [TextArea] private List<string> areaTexte = new List<string>();

    [Header("References")] 
    [SerializeField]private TextMeshProUGUI text;
    [SerializeField] private Button buttonNext;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject parent;


    
    private int currentId;
    
    private void Start()
    {
        UpdateText();
        buttonNext.onClick.AddListener(NextText);
    }

    private void Update()
    {
        
    }
    private void UpdateText()
    {
        text.maxVisibleCharacters = 0;
        text.text = areaTexte[currentId];
        StartCoroutine(ShowText());
        buttonText.text = "Next";
        if (currentId >= areaTexte.Count - 1)
        {
            buttonText.text = "OK";
        }
    }

    private void NextText()
    {
        if (currentId < areaTexte.Count - 1)
        {
            currentId++;
            UpdateText();
        }
        else
        {
            parent.SetActive(false);
            return;
        }
    }

    private IEnumerator ShowText()
    {
        var random = UnityEngine.Random.Range(0.07f, 0.02f); 
        while (text.maxVisibleCharacters < 300)
        {
            yield return new WaitForSeconds(random);
            text.maxVisibleCharacters ++;
        }
        yield break;
    }
}
