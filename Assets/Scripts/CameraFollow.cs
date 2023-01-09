using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public bool CameraShouldFollow = true;
    public Coroutine CameraFollowReferecne;
    // Update is called once per frame
    void Start()
    {
        CameraFollowReferecne = StartCoroutine(CameraFollowCoroutine());
    }
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
