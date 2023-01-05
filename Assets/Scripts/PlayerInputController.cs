using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Controls GameControls;
    public float Directtion;

    // Start is called before the first frame update
    private void Awake()
    {
        GameControls = new Controls();
        GameControls.PlatformerControls.Enable();      
    }
    // Update is called once per frame
    void Update()
    {
        Directtion = GameControls.PlatformerControls.Move.ReadValue<float>();
    }

    private void OnDisable()
    {

    }
}
