using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VM_Destructor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other){
        if(other.transform.gameObject.tag == "Destructor"){
            Destroy(this.gameObject);
        }
    }
}
