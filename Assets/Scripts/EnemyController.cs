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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentState == EnemyState.PlayerInRange)
        {
            print("Player is nearby");
        }
        else if(CurrentState == EnemyState.WalkBackAndForth)
        {
            print("why are we still here? just to walk?");
            transform.position = Vector2.MoveTowards(transform.position, Path[CurrentPointOnPath].position, Speed * Time.deltaTime);
            if(transform.position == Path[CurrentPointOnPath].position)
            {
                if(CurrentPointOnPath < Path.Length -1)
                {
                    CurrentPointOnPath++;
                }
                else
                {
                    CurrentPointOnPath = 0;
                }
            }
        }
        EvaluateState();
    }
    private void EvaluateState()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= Eyesight)
        {
            CurrentState = EnemyState.PlayerInRange;

        }
        else
        {
            CurrentState = EnemyState.WalkBackAndForth;
        }
    }
}
