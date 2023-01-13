using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        WalkBackAndForth,
        PlayerInRange


    }
    public EnemyState CurrentState;
    private GameObject player;
    public float Eyesight;
    public Transform[] Path;
    public int CurrentPointOnPath;
    public float Speed;
    public LayerMask JohnSeena;
    private SpriteRenderer sr;
    private BoxCollider2D bc2d;


    //Did you know: Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bc2d.enabled == true)
        {
            if (CurrentState == EnemyState.PlayerInRange)
            {
                //Some how, in unholy coding fashion this mucho texto is a Raycast for if the Player is in range. And yes they can go through walls, would be too easy if they couldn't
                RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Vector2.Distance(transform.position, player.transform.position), JohnSeena);
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.black);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), Speed * Time.deltaTime);
                if (hit.transform == player.transform)
                {
                    print("I'll be watching you!~");
                }
            }
            else if (CurrentState == EnemyState.WalkBackAndForth)
            {
                //Waling back and forth, need I say more?
                transform.position = Vector2.MoveTowards(transform.position, Path[CurrentPointOnPath].position, Speed * Time.deltaTime);
                if (transform.position == Path[CurrentPointOnPath].position)
                {
                    if (CurrentPointOnPath < Path.Length - 1)
                    {
                        CurrentPointOnPath++;
                    }
                    else
                    {
                        CurrentPointOnPath = 0;
                    }
                }
            }
        }        
        EvaluateState();
    }
    private void EvaluateState()
    {
        //This is what determines the state the enemy is in, looks so simple compared to what's above.
        if (Vector2.Distance(transform.position, player.transform.position) <= Eyesight)
        {
            CurrentState = EnemyState.PlayerInRange;

        }
        else
        {
            CurrentState = EnemyState.WalkBackAndForth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            //This is code borrowed from the Item Behavior to respawn th enemies
            sr.enabled = false;
            bc2d.enabled = false;
            Invoke("Respawn", 10f);
            var clip = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.RobotScream.ToString());
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
    //This time the hores isn't here, but a red circle is :o
    private void Respawn()
    {
        sr.enabled = true;
        bc2d.enabled = true;
        var clip = SoundManager.SoundManagerInstance.GetAudioClipFromDictionary(SoundManager.SoundEffectName.RobotIdle.ToString());
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
