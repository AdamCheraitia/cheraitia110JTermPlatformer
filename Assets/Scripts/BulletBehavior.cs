using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroys bullet on contact with enemy or ground
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            GameManager.EnemyCount++;
            print(GameManager.EnemyCount);
            GameManager.Score += 100;
            Destroy(gameObject);
            
        }
    }
}
