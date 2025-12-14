using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    
    [Header("Réglages de l'horloge")]
    public int startHour = 1;
    public int startMinute = 45;
    public float timeMultiplier = 5f;

    [Header("Affichage")] public TextMeshProUGUI uiText;
    public TextMeshProUGUI tmpText;
    private float gameTimeInMinutes;

    void Start()
    {
        gameTimeInMinutes = startHour * 60f + startMinute;
    }

    void Update()
    {
        gameTimeInMinutes += Time.deltaTime * timeMultiplier;
        if (gameTimeInMinutes >= 12 * 60f) // gaffe au 12 en dur, ca pourrait clairement changer, et là faudrait bricoler en code
            gameTimeInMinutes -= 12 * 60f;

        int hours = Mathf.FloorToInt(gameTimeInMinutes / 60f);
        int minutes = Mathf.FloorToInt(gameTimeInMinutes % 60f);

        string timeString = string.Format("{0:00}:{1:00}", hours, minutes);
        if (uiText != null)
            uiText.text = timeString;

        if (tmpText != null)
            tmpText.text = timeString;
    }
}
