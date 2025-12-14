using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    // j't'encourage à passer par des index en enum, et éviter les checks de string, si tu renames ta scene ca marchera plus
    
    public void LoadGame()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.LogError("Attention pas dans le menu");
        }
    }
    
    public void LoadMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex < 0)
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
