using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public PlayerInputController PIC;
    public Vector2 Movement;
    public float JumpForce;
    public float JumpCheckHeight;
    public LayerMask GroundMask;
    public bool OnGround;
    public Vector2 BoxCastSize;
    // Start is called before the first frame update
    void Start()
    {
        PIC.GameControls.PlatformerControls.Jump.performed += Jump;
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}
