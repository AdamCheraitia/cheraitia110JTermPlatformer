using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public enum ItemPresent
    {
        ItemWaiting,
        ItemUsed
    }
    public ItemPresent CurrentState;
    public GameObject Item;
    public GameObject SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EvaluateState();
    }

    private void FixedUpdate()
    {
        
    }

    private void EvaluateState()
    {
        if (CurrentState == ItemPresent.ItemUsed)
        {
            Invoke("HorseWalksIn", 5);
            CurrentState = ItemPresent.ItemWaiting;
        }
        if (!Item)
        {
            CurrentState = ItemPresent.ItemUsed;
        }
    }

    private void HorseWalksIn()
    {
        GameObject CurrentItem = Instantiate(Item);
        print("Peter. The horse is here.");
        CurrentItem.transform.position = SpawnPoint.transform.position;
    }

}
