using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    bool attack = false;
    private float health = 30;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    } 

    private void FixedUpdate(){
        if(attack == true){
            anim.SetInteger("Angel", 1);
            transform.position = Vector2.Lerp(transform.position, GameObject.Find("Player").transform.position, Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
            attack = true;

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
            anim.SetInteger("Angel", 2);
            attack = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;
            Destroy(gameObject, 0.6f);
        }
    }

    private void OnDestroy() {
        GameObject.Find("Player").GetComponent<CharacterAnimation>().energy += 30f;
    }
}
