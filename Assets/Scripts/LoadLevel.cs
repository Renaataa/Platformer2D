using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LoadLevel : MonoBehaviour, IPointerDownHandler
{
    bool load = false;
    
    private void Start(){
        if(PlayerPrefs.GetInt("Level") + 1 >= Convert.ToInt32(gameObject.name)){
            gameObject.GetComponentInChildren<Image>().color =  new Color(0.651f, 0.549f, 0.345f);
            load = true;
        }
    }
    public void OnPointerDown(PointerEventData eventData){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().click);
        if(load == true){
            SceneManager.LoadScene(Convert.ToInt32(gameObject.name).ToString());
            Time.timeScale = 1;
        }
    }
}
