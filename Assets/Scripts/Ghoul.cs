using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    Rigidbody2D rb;
    public int distance;
    float maxDistance;
    float minDistance;
    float speed = 1f;
    float health = 10;

    private void Start (){
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        maxDistance = transform.position.x + distance;
        minDistance = transform.position.x - distance;
    }

    private void FixedUpdate(){
        transform.Translate(transform.right * speed * Time.deltaTime);
        GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().AudioPlay(GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().walkGhoul);
        
        if(transform.position.x > maxDistance){
            speed = -speed;
            sr.flipX = false;
        }
        else if(transform.position.x < minDistance){
            speed = -speed;
            sr.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "EnemyDamage"){
            Damage();
        }
    }

    public void Damage(){
        if(GameObject.Find("Player").GetComponent<CharacterAnimation>().energyBonus == true)
            health -= 5;
        else 
            health--;

        if(health <= 0){
            anim.SetTrigger("Ghoul");
            speed = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation;
            Invoke("Destroy", 0.6f);
        }
    }

    private void Destroy() {
        gameObject.SetActive(false);
        GameObject.Find("Player").GetComponent<CharacterAnimation>().energy += 10f;
    }
}
