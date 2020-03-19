using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_PC_Controller : MonoBehaviour
{
    private PL_PC_Input input;
    public Camera cam;
    public CharacterController cc;
    public float walkSpeed = 4;
    public float gravity   = 10;
    private float curGravity = 0;

    void Start()
    {
        input = PL_PC_Input.Instance;
        if (cc == null)
            cc = this.gameObject.GetComponent<CharacterController>() ?? this.gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        if (cc.isGrounded)
            curGravity = 0;
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, input.yaw, 0);
        cam.transform.localRotation = Quaternion.Euler(input.pitch, 0, 0);
    }

    void HandleMovement()
    {
        Vector3 fwd = new Vector3(transform.forward.x, 0, transform.forward.z).normalized * input.movementInput.y;
        Vector3 rit = new Vector3(transform.right.x, 0, transform.right.z).normalized * input.movementInput.x;
        Vector3 final = (fwd + rit).normalized * walkSpeed;
        final += new Vector3(0, (curGravity -= gravity * Time.deltaTime), 0);
        cc.Move(final * Time.deltaTime);
    }

}

