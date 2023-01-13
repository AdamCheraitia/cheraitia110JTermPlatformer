using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Controls GameControls;
    public float Directtion;
    public Vector2 MousePosiion;
    //Enables the controls for the player
    private void Awake()
    {
        GameControls = new Controls();
        GameControls.PlatformerControls.Enable();      
    }
    //Assigns variables for moving and mouse position
    void Update()
    {
        Directtion = GameControls.PlatformerControls.Move.ReadValue<float>();
        MousePosiion = GameControls.PlatformerControls.MousePosition.ReadValue<Vector2>();
    }
    //Disables when the player object is not present
    private void OnDisable()
    {
        GameControls.PlatformerControls.Disable();
    }
}
