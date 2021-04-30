using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D coll;
    public GameObject damage;
    int hit;
    float health = 10;
    public float energy;
    public bool energyBonus = false;

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemy"){
            if(collision.gameObject.GetComponent<Angel>() == true)
                health -=3;
            else if(collision.gameObject.GetComponent<Fireball>() == true)
                health -=0.5f;
            else if(collision.gameObject.GetComponent<Wizard>() == true)
                health -=2;
            else if(collision.gameObject.GetComponent<Ghoul>() == true){
                health --;
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
                coll = collision.collider;
            }
            
            GameObject.Find("HealthBar").GetComponent<FillHealthBar>().CurrentValue = health*0.1f;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation;

            anim.SetInteger("hurt", 0);
            Invoke("AnimHurtOff", 0.25f);
            Invoke("HurtOff", 0.5f);
        }
    }

    void Update(){
        GameObject.Find("EnergyBar").GetComponent<FillEnergyBar>().CurrentValue = energy/50f;
        
        if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))){
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && GetComponent<PlayerController2>().isGrounded == true){
            if(!Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKey(KeyCode.DownArrow)){
                HitOff();
                anim.SetInteger("Jump", 0);
            }
            Invoke("AnimJumpOff", 0.15f);
        }
        if(GetComponent<PlayerController2>().isGrounded == false && Input.GetKeyDown(KeyCode.S)){
            FlyingKick();
        }

        if((Input.GetKeyDown(KeyCode.DownArrow)) && GetComponent<PlayerController2>().isGrounded == true){
            Crouch();
        }

        if(Input.GetKeyDown(KeyCode.Space) && GetComponent<PlayerController2>().isGrounded == true){
            Hit();
        }

        if(Input.GetKeyDown(KeyCode.W)){
            EnergyBonus();
        }
    }

    void AnimHurtOff(){
        anim.SetInteger("hurt", -1);
    }
    void HurtOff(){
        if (coll) Physics2D.IgnoreCollision(coll, GetComponent<Collider2D>(), false);
        rb.constraints = RigidbodyConstraints2D.None|RigidbodyConstraints2D.FreezeRotation;
    }

    void FlyingKick(){
        anim.SetInteger("Jump", -1);
        anim.SetInteger("hit", 3);

        DamageFlip(0.25f);

        Invoke("AnimJumpOff", 0.5f);
    }
    void AnimJumpOff(){
        anim.SetInteger("Jump", -1);
    }

    void Hit(){
        hit = Random.Range(0, 2);
        anim.SetInteger("hit", hit);

        DamageFlip(0.3f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;

        Invoke("AnimHitOff", 0.1f);
        Invoke("HitOff", 0.6f);
    }
    void AnimHitOff(){
        anim.SetInteger("hit", -1);
    }
    void HitOff(){
        rb.constraints = RigidbodyConstraints2D.None|RigidbodyConstraints2D.FreezeRotation;
    }

    void Crouch(){
        anim.SetTrigger("crouch");

        GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.0001967549f, -0.1565886f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.1009637f, 0.2951798f);  
        rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;

        Invoke("CrouchOff", 0.35f);
    }
    void CrouchOff(){
        GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.005184233f, -0.07877457f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.2300652f, 0.4508078f);
        rb.constraints = RigidbodyConstraints2D.None|RigidbodyConstraints2D.FreezeRotation;
    }

    void DamageFlip(float distance){
        if(transform.localScale.x < 0)
            Instantiate(damage, new Vector2(transform.position.x - distance, transform.position.y), Quaternion.identity);
        else if(transform.localScale.x > 0)
            Instantiate(damage, new Vector2(transform.position.x + distance, transform.position.y), Quaternion.identity);
    }

    void EnergyBonus(){
        if(energy >= 50)
        {
            energy = 0;
            energyBonus = true;
            GetComponent<PlayerController2>().speed++;
            Invoke("EnergyBonusOff", 5);
        }
    }

    void EnergyBonusOff(){
        energyBonus = false;
        GetComponent<PlayerController2>().speed--;
    }
}
