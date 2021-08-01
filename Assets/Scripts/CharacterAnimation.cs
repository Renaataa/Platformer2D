﻿using UnityEngine;
using System;
using TMPro;

public class CharacterAnimation : MonoBehaviour
{
    public Joystick joystick;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D coll;
    public GameObject damage;
    public int coin;
    int hit;
    int level = 0;
    float health = 10;
    bool activeBoost = false;
    bool protect = false;
    public int countHealth = 0;
    public int countJump = 0;
    public int countSpeed = 0;
    public int countProtect = 0;
    public GameObject panelBoost;
    public GameObject healthBoost;
    public GameObject jumpBoost;
    public GameObject speedBoost;
    public GameObject protectBoost;
    public float energy;
    public bool energyBonus = false;
    public GameObject PanelGameOver;
    public GameObject PanelWin;

    void Start(){
        panelBoost = GameObject.Find("PanelBoost");
        jumpBoost = GameObject.Find("JumpBoost");
        healthBoost = GameObject.Find("HealthBoost");
        speedBoost = GameObject.Find("SpeedBoost");
        protectBoost = GameObject.Find("ProtectBoost");

        panelBoost.SetActive(false);
        jumpBoost.SetActive(false);
        healthBoost.SetActive(false);
        speedBoost.SetActive(false);
        protectBoost.SetActive(false);

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        level = PlayerPrefs.GetInt("Level");

        //PlayerPrefs.SetInt("Coin", 100);
        coin = PlayerPrefs.GetInt("Coin");
        GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coin.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "win"){
            if(Convert.ToInt32(collision.gameObject.name) > level && level < 4){
                level++;
                PlayerPrefs.SetInt("Level", level);
            }
            PanelWin.SetActive(true);
            Time.timeScale = 0;
        }
        if(collision.gameObject.tag == "Coin"){
            coin += Convert.ToInt32(collision.gameObject.name);
            GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coin.ToString();
            GameObject.Find("CoinText").GetComponent<Animation>().Play();
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().coin);
            PlayerPrefs.SetInt("Coin", coin);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name == "Health"){
            countHealth++;
            panelBoost.SetActive(true);
            healthBoost.SetActive(true);
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().pickUpBootle);
            GameObject.Find("HealthBoostText").GetComponent<TextMeshProUGUI>().text = countHealth.ToString();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name == "Jump"){
            countJump++;
            panelBoost.SetActive(true);
            jumpBoost.SetActive(true);
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().pickUpBootle);
            GameObject.Find("JumpBoostText").GetComponent<TextMeshProUGUI>().text = countJump.ToString();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name == "Speed"){
            countSpeed++;
            panelBoost.SetActive(true);
            speedBoost.SetActive(true);
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().pickUpBootle);
            GameObject.Find("SpeedBoostText").GetComponent<TextMeshProUGUI>().text = countSpeed.ToString();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name == "Protect"){
            countProtect++;
            panelBoost.SetActive(true);
            protectBoost.SetActive(true);
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().pickUpBootle);
            GameObject.Find("ProtectBoostText").GetComponent<TextMeshProUGUI>().text = countProtect.ToString();
            Destroy(collision.gameObject);
        }
    }
    public void BoostHealth(){
        if(countHealth > 0){
            countHealth--;
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().bonus);
            GameObject.Find("HealthBoostText").GetComponent<TextMeshProUGUI>().text = countHealth.ToString();
            GameObject.Find("HealthBoost").GetComponent<Animation>().Play();
        
            health += 3;
            if(health > 10) health = 10;
            GameObject.Find("HealthBar").GetComponent<FillHealthBar>().CurrentValue = health*0.1f;
            GameObject.Find("Fill").GetComponent<Animation>().Play();
        }
    }
    public void BoostJump(){
        if(countJump > 0 && activeBoost == false){
            activeBoost = true;
            countJump--;
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().bonus);
            GameObject.Find("JumpBoostText").GetComponent<TextMeshProUGUI>().text = countJump.ToString();
            GameObject.Find("JumpBoost").GetComponent<Animation>().Play();
        
            GameObject.Find("Player").GetComponent<PlayerController>().jumpForce *= 1.5f;
            Invoke("OffJump", 5);
        }
    }
    public void BoostSpeed(){
        if(countSpeed > 0 && activeBoost == false){
            activeBoost = true;
            countSpeed--;
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().bonus);
            GameObject.Find("SpeedBoostText").GetComponent<TextMeshProUGUI>().text = countSpeed.ToString();
            GameObject.Find("SpeedBoost").GetComponent<Animation>().Play();
        
            GameObject.Find("Player").GetComponent<PlayerController>().speed *= 1.5f;
            Invoke("OffSpeed", 5);
        }
    }
    public void BoostProtect(){
        if(countProtect > 0 && activeBoost == false){
            activeBoost = true;
            countProtect--;
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().bonus);
            GameObject.Find("ProtectBoostText").GetComponent<TextMeshProUGUI>().text = countProtect.ToString();
            GameObject.Find("ProtectBoost").GetComponent<Animation>().Play();

            protect = true;
            Invoke("OffProtect", 5);
        }
    }
    void OffJump(){
        activeBoost = false;
        GameObject.Find("Player").GetComponent<PlayerController>().jumpForce /= 1.5f;
    }
    void OffSpeed(){
        activeBoost = false;
        GameObject.Find("Player").GetComponent<PlayerController>().speed /= 1.5f;
    }
    void OffProtect(){
        activeBoost = false;
        protect = false;
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemy" && protect == false){
            if(collision.gameObject.GetComponent<Angel>() == true){
                GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().AudioPlay(GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().attackAngel);
                health -=3;
            }
            else if(collision.gameObject.GetComponent<Fireball>() == true)
                health -=0.5f;
            else if(collision.gameObject.GetComponent<Wizard>() == true)
                health -=2;
            else if(collision.gameObject.GetComponent<Ghoul>() == true){
                GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().AudioPlay(GameObject.Find("AudioBoxEnemy").GetComponent<AudioBoxEnemy>().attackGhoul);
                health --;
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
                coll = collision.collider;
            }
            
            GameObject.Find("HealthBar").GetComponent<FillHealthBar>().CurrentValue = health*0.1f;
            GameObject.Find("Fill").GetComponent<Animation>().Play();
            GameObject.Find("Blood").GetComponent<Animation>().Play();

            rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation;
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().hurt);
            anim.SetInteger("hurt", 0);
            
            Invoke("AnimHurtOff", 0.25f);
            Invoke("HurtOff", 0.5f);

            if(health <= 0){
                PanelGameOver.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else if(collision.gameObject.GetComponent<Ghoul>() == true){
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
                coll = collision.collider;
                Invoke("HurtOff", 0.5f);
        }
    }
    void FixedUpdate(){
        GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coin.ToString();
    }
    void Update(){
        GameObject.Find("EnergyBar").GetComponent<FillEnergyBar>().CurrentValue = energy/50f;
        
        if(joystick.Horizontal < 0 || joystick.Horizontal > 0){
            //GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().walk);
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }

        if(joystick.Vertical > 0.3 && GetComponent<PlayerController>().isGrounded == true){
            if(!(joystick.Vertical <= 0)){
                HitOff();
                GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().jump);
                anim.SetInteger("Jump", 0);
            }
            Invoke("AnimJumpOff", 0.15f);
        }

        if((joystick.Vertical < -0.3) && GetComponent<PlayerController>().isGrounded == true){
            Crouch();
        }
    }

    void AnimHurtOff(){
        anim.SetInteger("hurt", -1);
    }
    void HurtOff(){
        if (coll) Physics2D.IgnoreCollision(coll, GetComponent<Collider2D>(), false);
        rb.constraints = RigidbodyConstraints2D.None|RigidbodyConstraints2D.FreezeRotation;
    }

    public void ButtonHit(){
        if(GetComponent<PlayerController>().isGrounded == false){
            FlyingKick();
        }
        if(GetComponent<PlayerController>().isGrounded == true){
            Hit();
        }
    }

    void FlyingKick(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().flyingKick);
        anim.SetInteger("Jump", -1);
        anim.SetInteger("hit", 3);

        DamageFlip(0.25f);

        Invoke("AnimJumpOff", 0.5f);
        Invoke("AnimHitOff", 0.1f);
    }
    void AnimJumpOff(){
        anim.SetInteger("Jump", -1);
    }

    void Hit(){
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().kick);
        hit = UnityEngine.Random.Range(0, 2);
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
        GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().crouch);
        anim.SetTrigger("crouch");

        GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.0001967549f, -0.1565886f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.1009637f, 0.2951798f);  
        rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;

        Invoke("CrouchOff", 0.5f);
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

    public void EnergyBonus(){
        if(energy >= 50)
        {
            energy = 0;
            energyBonus = true;
            GetComponent<PlayerController>().speed += 0.1f;
            Invoke("EnergyBonusOff", 5);
        }
    }

    void EnergyBonusOff(){
        energyBonus = false;
        GetComponent<PlayerController>().speed -= 0.1f;
    }
}
