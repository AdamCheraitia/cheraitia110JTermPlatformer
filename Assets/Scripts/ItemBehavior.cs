using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public SpawnerController SC;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("Peter, where'd the horse go?");
            SC.CurrentState = SpawnerController.ItemPresent.ItemUsed;
            Destroy(gameObject);
        }
    }

}
