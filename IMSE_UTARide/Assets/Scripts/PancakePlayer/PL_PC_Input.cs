using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_PC_Input : MonoBehaviour
{
    public static PL_PC_Input Instance { get; private set; }

    public Vector2 movementInput { get; private set; } = Vector2.zero;
    public float pitch { get; private set; } = 0;
    public float yaw   { get; private set; } = 0;

    public bool DEBUG_lockMouse = false;
    public float camPitchSensativity = 1;
    public float camYawSensativity = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Debug.LogWarning($"{this.gameObject.name} has a duplicate PL_PC_Input component");
    }

    void Update()
    {
        movementInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        pitch = Mathf.Clamp(pitch - Input.GetAxisRaw("Mouse Y") * camPitchSensativity, -89, 89);
        yaw = (yaw + Input.GetAxisRaw("Mouse X") * camYawSensativity) % 360;
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            Cursor.visible = !DEBUG_lockMouse;
            Cursor.lockState = DEBUG_lockMouse ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}
