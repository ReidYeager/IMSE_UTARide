using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VM_Move : MonoBehaviour
{
    public float velocity = 5.0f;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(direction * Time.deltaTime * velocity, Space.World); 
    }
}
