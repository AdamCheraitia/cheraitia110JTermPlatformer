using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public SpawnerController SC;
    private BoxCollider2D bc2d;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("Peter, where'd the horse go?");
            sr.enabled = false;
            bc2d.enabled = false;
            Invoke("TurnOn", 5f);
        }
    }
    public void TurnOn()
    {
        sr.enabled = true;
        bc2d.enabled = true;
    }
}
