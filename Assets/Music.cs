using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private void Start()
    {
        if(PlayerPrefs.GetString("Music") == "no")
        {
            GetComponent<AudioSource>().enabled = false;
        }
    }
}
