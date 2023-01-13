using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject Item;
    public GameObject SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("HorseWalksIn", 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    
    private void HorseWalksIn()
    {
        GameObject CurrentItem = Instantiate(Item);
        print("Peter. The horse is here.");
        CurrentItem.transform.position = SpawnPoint.transform.position;
        Destroy(this);
    }

}
