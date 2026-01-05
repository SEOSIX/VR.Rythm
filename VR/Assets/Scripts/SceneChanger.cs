using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    public void LoadGame()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogError("Attention pas dans le menu");
        }
    }
    
    public void LoadMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            Debug.LogError("Attention pas dans le jeux");
        }
        
    }
    
    public void LoadDeathMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex < 0)
        {
            SceneManager.LoadScene("DeathScene");
        }
        else
        {
            Debug.LogError("Attention pas dans le jeux");
        }
        
    }
    
    public void Quit()
    {
        
        Application.Quit();
        
    }

}
