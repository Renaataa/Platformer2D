using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    public GameObject panelPause;

    public void PauseOn(){
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff(){
        panelPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetLevel(){
        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void Menu(){
        SceneManager.LoadScene("Menu");
    }

    public void Play(){
        SceneManager.LoadScene("main");
    }

    public void Level(){
        SceneManager.LoadScene("Level");
    }

    public void Exit(){
        Application.Quit();
    }
}
