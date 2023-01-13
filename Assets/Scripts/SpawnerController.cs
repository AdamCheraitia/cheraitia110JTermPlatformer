using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject Item;
    public GameObject SpawnPoint;
    //D i d y o u k n o w: Start is called before the first frame update
    void Start()
    {
        Invoke("HorseWalksIn", 5);
    }

    
    private void HorseWalksIn()
    {
        //This code was originally for a universal Item Spawner I wanted to make and take to other projects,
        //but ItemBehavor took that role so this is just here to spawn the item once
        GameObject CurrentItem = Instantiate(Item);
        print("Peter. The horse is here.");
        CurrentItem.transform.position = SpawnPoint.transform.position;
        Destroy(this);
    }

}
