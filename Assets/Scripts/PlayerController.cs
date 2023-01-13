using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        PIC.GameControls.PlatformerControls.Jump.performed += Jump;
        PIC.GameControls.PlatformerControls.Shoot.performed += Shoot;
    }

    // Update is called once per frame
    void Update()
    {
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
        RotateVector = Camera.main.ScreenToWorldPoint(PIC.MousePosiion) - transform.position;
        RotateVector.Normalize();
        angle = Mathf.Atan2(RotateVector.y, RotateVector.x) * Mathf.Rad2Deg;
        ArmAnchor.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        if(Ammo < 30 && Input.GetKeyDown(KeyCode.E))
        {
            Invoke("DawnOfFriday", 2f);
            print("animation goes here");
        }
        EvaluateState();
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
        float XMovement = speed * Time.deltaTime * PIC.Directtion; //something
        Movement = new Vector2(XMovement, rb2d.velocity.y);
        rb2d.velocity = Movement;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
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
            CanShoot = false;
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
        if(health == 0)
        {
            CurrentState = PlayerState.Dead;
        }
    }
    private void NotFriday()
    {
        CanShoot = true;
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
        if (collision.gameObject.tag == "Health")
        {
            health++;
            var clip2 = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.PickUp.ToString());
            AudioSource.PlayClipAtPoint(clip2, transform.position);
            Debug.Log("Touched");
        }

        if (collision.gameObject.tag == "Ammo")
        {
            MaxAmmo = MaxAmmo + 10;
            var clip2 = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.PickUp.ToString());
            AudioSource.PlayClipAtPoint(clip2, transform.position);
        }
    }

}
