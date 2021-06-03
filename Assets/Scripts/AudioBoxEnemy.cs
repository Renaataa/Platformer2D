using UnityEngine;

public class AudioBoxEnemy : MonoBehaviour
{
    public AudioClip attackAngel;
    public AudioClip flyingAngel;

    public AudioClip explosingFireball;
    public AudioClip flyingFireball;

    public AudioClip attackGhoul;
    public AudioClip walkGhoul;

    public AudioClip wizard;
    private void Start(){
        if(GameObject.FindGameObjectsWithTag("AudioEnemy").Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    public void AudioPlay(AudioClip ac){
        if(PlayerPrefs.GetString("Sound") != "no"){
            GetComponent<AudioSource>().clip = ac;
            GetComponent<AudioSource>().Play();
        }
    }
}
