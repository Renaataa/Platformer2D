using UnityEngine;
using System;

public class CharacterAnimation : MonoBehaviour
{
    public Joystick joystick;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D coll;
    public GameObject damage;
    int hit;
    int level = 0;
    float health = 10;
    bool protect = false;
    public float energy;
    public bool energyBonus = false;
    public GameObject PanelGameOver;
    public GameObject PanelWin;

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        level = PlayerPrefs.GetInt("Level");
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
        if(collision.gameObject.name == "Health"){
            health += 3;
            if(health > 10) health = 10;
            GameObject.Find("HealthBar").GetComponent<FillHealthBar>().CurrentValue = health*0.1f;
            GameObject.Find("Fill").GetComponent<Animation>().Play();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name == "Jump"){
            GameObject.Find("Player").GetComponent<PlayerController>().jumpForce *= 1.5f;
            sr.color = new Color(169f/255, 245f/255, 118f/255);
            Invoke("OffJump", 5);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name == "Speed"){
            GameObject.Find("Player").GetComponent<PlayerController>().speed *= 1.5f;
            sr.color = new Color(85f/255, 168f/255, 219f/255);
            Invoke("OffSpeed", 5);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name == "Protect"){
            protect = true;
            sr.color = new Color(169f/255, 245f/255, 118f/255);
            Invoke("OffProtect", 5);
            Destroy(collision.gameObject);
        }
    }
    void OffJump(){
        GameObject.Find("Player").GetComponent<PlayerController>().jumpForce /= 1.5f;
        sr.color = Color.white;
    }
    void OffSpeed(){
        GameObject.Find("Player").GetComponent<PlayerController>().speed /= 1.5f;
        sr.color = Color.white;
    }
    void OffProtect(){
        protect = false;
        sr.color = Color.white;
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

    void Update(){
        GameObject.Find("EnergyBar").GetComponent<FillEnergyBar>().CurrentValue = energy/50f;
        
        if(joystick.Horizontal < 0 || joystick.Horizontal > 0){
            GameObject.Find("AudioBox").GetComponent<AudioBox>().AudioPlay(GameObject.Find("AudioBox").GetComponent<AudioBox>().walk);
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
