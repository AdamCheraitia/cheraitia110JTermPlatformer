using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public bool CameraShouldFollow = true;
    public Coroutine CameraFollowReferecne;

    void Start()
    {
        CameraFollowReferecne = StartCoroutine(CameraFollowCoroutine());
    }
    //This makes the Camera follow the player, I've written code for this before but not like this
    private void Update()
    {
        if(CameraShouldFollow && CameraFollowReferecne == null)
        {
            CameraFollowReferecne = StartCoroutine(CameraFollowCoroutine());
        }
    }
    public IEnumerator CameraFollowCoroutine()
    {
        while (CameraShouldFollow == true)
        {
            transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
            yield return null;
        }
        CameraFollowReferecne = null;
    }
}
