using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class LoadLevel : MonoBehaviour, IPointerDownHandler
{
    bool load = false;
    private void Start(){
        if(PlayerPrefs.GetInt("Level") + 1 >= Convert.ToInt32(gameObject.name)){
            GetComponent<Image>().color = Color.white;
            GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            load = true;
        }
    }
    public void OnPointerDown(PointerEventData eventData){
        if(load == true){
            SceneManager.LoadScene(Convert.ToInt32(gameObject.name).ToString());
        }
    }
}
