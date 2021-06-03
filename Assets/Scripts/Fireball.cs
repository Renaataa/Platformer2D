using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().AudioPlay(GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().flyingFireball);
        Destroy(gameObject, 1);
    }

    private void FixedUpdate(){
        if(sr.flipX == true)
            rb.AddForce(transform.right * 2, ForceMode2D.Force);
        else if(sr.flipX == false)
            rb.AddForce(-transform.right * 2, ForceMode2D.Force);
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "ground"){
            GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().AudioPlay(GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().explosingFireball);
            Destroy(gameObject);
        }
            
    }
}
