using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    public GameObject fireball;
    float health = 20;

    private void Start(){
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        if(GameObject.Find("Player").transform.position.x < transform.position.x)
            sr.flipX = false;
        else if(GameObject.Find("Player").transform.position.x > transform.position.x)
            sr.flipX = true;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().AudioPlay(GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().wizard);
            anim.SetInteger("Wizard", 1);
            Progress.StartNewProgress(this.gameObject, 0.9f, SpawnFireball);
        }
        
        if(other.gameObject.tag == "EnemyDamage"){
            Damage();
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            anim.SetInteger("Wizard", 0);
            Progress.StartNewProgress(this.gameObject, 0, SpawnFireball);
        }
    }

    void SpawnFireball(){
        if(sr.flipX == false){
            fireball.GetComponent<SpriteRenderer>().flipX = false;
            Instantiate(fireball, new Vector2(transform.position.x - 0.3f, transform.position.y), Quaternion.identity);
        }
        else if(sr.flipX == true){
            fireball.GetComponent<SpriteRenderer>().flipX = true;
            Instantiate(fireball, new Vector2(transform.position.x + 0.3f, transform.position.y), Quaternion.identity);
        }
    }

    public void Damage(){
        if(GameObject.Find("Player").GetComponent<CharacterAnimation>().energyBonus == true)
            health -= 5;
        else 
            health--;
            
        if(health <= 0){
            anim.SetInteger("Wizard", 2); 
            Invoke("Destroy", 0.5f);
        }
    }

    private void Destroy() {
        gameObject.SetActive(false);
        GameObject.Find("Player").GetComponent<CharacterAnimation>().energy += 20f;
    }
}
