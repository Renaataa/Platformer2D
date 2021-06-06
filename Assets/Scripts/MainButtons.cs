using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainButtons : MonoBehaviour
{
    public GameObject panelPause;
    public GameObject panelSettings;
    
    public void PauseOn(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        panelPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetScene(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ResetLevel(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        SceneManager.LoadScene(Application.loadedLevel);
        Time.timeScale = 1;
    }
    public void ResetGame(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        PlayerPrefs.DeleteAll();
    }

    public void Menu(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        SceneManager.LoadScene("Menu");
    }

    public void Play(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        SceneManager.LoadScene((PlayerPrefs.GetInt("Level") + 1).ToString());
        Time.timeScale = 1;
    }

    public void Level(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        SceneManager.LoadScene("Level");
    }

    public void Exit(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        Application.Quit();
    }

    public void OnMusic(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);

        if(PlayerPrefs.GetString("Music") != "no"){
            PlayerPrefs.SetString("Music", "no");
            GameObject.Find("TextMusic").GetComponent<TextMeshProUGUI>().text = "Music: on";
        }
        else{
            PlayerPrefs.SetString("Music", "yes");
            GameObject.Find("TextMusic").GetComponent<TextMeshProUGUI>().text = "Music: off";
        }
        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void OnSound(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);

        if(PlayerPrefs.GetString("Sound") != "no"){
            PlayerPrefs.SetString("Sound", "no");
            GameObject.Find("TextSound").GetComponent<TextMeshProUGUI>().text = "Sound: off";
        }
        else{
            PlayerPrefs.SetString("Sound", "yes");
            GameObject.Find("TextSound").GetComponent<TextMeshProUGUI>().text = "Sound: on";
        }
        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void OnSettings(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        panelSettings.SetActive(true);

        if(PlayerPrefs.GetString("Music") == "no"){
            GameObject.Find("TextMusic").GetComponent<TextMeshProUGUI>().text = "Music: off";
        }
        else{
            GameObject.Find("TextMusic").GetComponent<TextMeshProUGUI>().text = "Music: on";
        }

        if(PlayerPrefs.GetString("Sound") == "no"){
            GameObject.Find("TextSound").GetComponent<TextMeshProUGUI>().text = "Sound: off";
        }
        else{
            GameObject.Find("TextSound").GetComponent<TextMeshProUGUI>().text = "Sound: on";
        }
    }

    public void OffSettings(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        panelSettings.SetActive(false);
    }
}
