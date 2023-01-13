using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Where do I even begin with this one? There's just A LOT here and its not even like the code controling it JUST VARIABLES!
    public enum PlayerState
    {
        Alive,
        Dead
    }
    public PlayerState CurrentState;
    public Rigidbody2D rb2d;
    public float speed;
    public PlayerInputController PIC;
    public Vector2 Movement;
    public float JumpForce;
    public LayerMask GroundMask;
    public bool OnGround;
    public Vector2 BoxCastSize;
    public Transform ArmAnchor;
    public Transform Arm;
    public GameObject Barrel;
    private float angle;
    public GameObject Bullet;
    private Vector2 RotateVector;
    private bool CanShoot = true;
    public int Ammo = 30;
    public int MaxAmmo = 120;
    public int health = 5;
    public Animator PlayerAnimator;
    public int NonZeroDirection;
    public TMP_Text HealthText;
    public TMP_Text AmmoText;
    //D I D Y O U K N O W: Start is called before the first frame update
    void Start()
    {
        PIC.GameControls.PlatformerControls.Jump.performed += Jump;
        PIC.GameControls.PlatformerControls.Shoot.performed += Shoot;
    }
    void Update()
    {
        //So aside from assigning the health text, this here controls the Boxcast (raycast but box) for if the player can jump
        HealthText.text = "Health: " + health.ToString();
        AmmoText.text = Ammo.ToString() + "/" + MaxAmmo.ToString();
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, BoxCastSize, 0, Vector2.down, 0, GroundMask);
        BoxCastDrawer.Draw(hit, transform.position, BoxCastSize, 0, Vector2.down, 0);
        if(hit.transform != null)
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
        //This chunk here is I think the entire code for roating the are to shoot
        RotateVector = Camera.main.ScreenToWorldPoint(PIC.MousePosiion) - transform.position;
        RotateVector.Normalize();
        angle = Mathf.Atan2(RotateVector.y, RotateVector.x) * Mathf.Rad2Deg;
        ArmAnchor.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //This was a failed attempt at making the player rotate based on the direction you're moving;
        //it's gonna have to be more complex now since the arm rotates too, but not by much
        if(PIC.Directtion > 0)
        {
            //NonZeroDirection = 1;
            //<GetComponent>transform.localScale
        }
        if(PIC.Directtion == 0)
        {
            PlayerAnimator.SetBool("IsWalking", false);
        }
        else
        {
            PlayerAnimator.SetBool("IsWalking", true);
        }
        //These two "if" statements function as the reload mechanic for the player.
        if(Ammo < 30 && Input.GetKeyDown(KeyCode.E))
        {
            Invoke("DawnOfFriday", 2f);
            print("animation goes here");
        }
        if(Ammo <= 0 && Input.GetMouseButtonDown(1))
        {
            Invoke("DawnOfFriday", 2f);
            print("animation goes here");
        }
        EvaluateState();
        //I don't need to explain what this does, so I wont
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }

    void FixedUpdate()
    {
        //So, this is controling the player's movement. It basically uses the variables from the PlayerInputController to do movement.
        float XMovement = speed * Time.deltaTime * PIC.Directtion; //something
        Movement = new Vector2(XMovement, rb2d.velocity.y);
        rb2d.velocity = Movement;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        //This here Jump function uses AddForce to make you jump. Also it plays a sound!
        if(OnGround == true)
        {
            Vector2 Jump = new Vector2(Movement.x, JumpForce);
            rb2d.AddForce(Jump, ForceMode2D.Impulse);
            var clip = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.Jump.ToString());
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
    private void Shoot(InputAction.CallbackContext obj)
    {
        if (CanShoot && Ammo > 0)
        {
            //This is a lot. This is what goes into making you shoot.
            CanShoot = false;
            PlayerAnimator.SetBool("Shoot", true);
            Invoke("NotFriday", 0.1f);
            print("Today is Friday in California. Huh? Shoot!");
            GameObject CurretBullet = Instantiate(Bullet);
            Ammo--;
            CurretBullet.transform.position = Barrel.transform.position;
            CurretBullet.GetComponent<Rigidbody2D>().velocity = RotateVector * 15;
            CurretBullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            var clip3 = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.Shoot.ToString());
            AudioSource.PlayClipAtPoint(clip3, transform.position);
            Destroy(CurretBullet, 8f);
        }
        
    }
    private void EvaluateState()
    {
        //Here this here makes the player dead. As of writing this it says "health == 0" so I should change that
        if(health <= 0)
        {
            CurrentState = PlayerState.Dead;
        }
    }
    private void NotFriday()
    {
        CanShoot = true;
        PlayerAnimator.SetBool("Shoot", false);
    }

    private void DawnOfFriday()
    {
        print("reloading");
        MaxAmmo = MaxAmmo - (30 - Ammo);
        Ammo = 30;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Enemy")
        {
            //This basically says that you touched and enemy and lost health. Also, its what sends you to the Lose Screen.
            health--;
            var clip = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.PlayerHit.ToString());
            AudioSource.PlayClipAtPoint(clip, transform.position);
            if (health <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Health Pickup that lets you gain health.
        if (collision.gameObject.tag == "Health")
        {
            health++;
            if(health > 5)
            {
                health = 5;
            }
            var clip2 = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.PickUp.ToString());
            AudioSource.PlayClipAtPoint(clip2, transform.position);
            Debug.Log("Touched");
        }
        //I am very proud to say that I made a functioning ammo system FIRST TRY! I was very happen when I saw those numbers change, a feeling like no other :)
        if (collision.gameObject.tag == "Ammo")
        {
            MaxAmmo = MaxAmmo + 10;
            var clip2 = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.PickUp.ToString());
            AudioSource.PlayClipAtPoint(clip2, transform.position);
        }
    }

}
