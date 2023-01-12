using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Controls GameControls;
    public float Directtion;
    public Vector2 MousePosiion;
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
        MousePosiion = GameControls.PlatformerControls.MousePosition.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        GameControls.PlatformerControls.Disable();
    }
}
