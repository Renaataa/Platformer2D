using UnityEngine;

public class AudioBox : MonoBehaviour
{
    public AudioClip click;
    public AudioClip bonus;
    public AudioClip pickUpBootle;
    public AudioClip coin;
    public AudioClip crouch;
    public AudioClip flyingKick;
    public AudioClip hurt;
    public AudioClip kick;
    public AudioClip walk;
    public AudioClip jump;

    private void Start(){
        if(GameObject.FindGameObjectsWithTag("Audio").Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    public void AudioPlay(AudioClip ac){
        if(PlayerPrefs.GetString("Sound") != "no"){
            //GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = ac;
            GetComponent<AudioSource>().Play();
        }
    }
}
