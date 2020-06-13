using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VM_Generator : MonoBehaviour
{
   // generator position
    private Vector3 generatorPosition = new Vector3(0.0f, 0.0f, 0.0f);
    // vehicle prefab
    public GameObject[] CarPrefab = new GameObject[4];
    public GameObject[] BusPrefab = new GameObject[2];
    public GameObject PoliceCarPrefab;
    public GameObject TaxiPrefab;
  

    // generating timer
    public float timeGap = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Generate", 0.0f, 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate(){
        // int i = (int)Mathf.Floor(Random.Range(0.0f, timeGap)); 
        // switch(i){
        //     case 0: 
        //         GameObject.Instantiate(CarPrefab[0], generatorPosition, Quaternion.identity); break;
        //     case 1: 
        //         GameObject.Instantiate(CarPrefab[1], generatorPosition, Quaternion.identity); break;
        //     case 2: 
        //         GameObject.Instantiate(CarPrefab[2], generatorPosition, Quaternion.identity); break;
        //     case 3: 
        //         GameObject.Instantiate(CarPrefab[3], generatorPosition, Quaternion.identity); break;
        //     case 4: 
        //         GameObject.Instantiate(BusPrefab[0], generatorPosition, Quaternion.identity); break;
        //     case 5: 
        //         GameObject.Instantiate(BusPrefab[1], generatorPosition, Quaternion.identity); break;
        //     case 6: 
        //         GameObject.Instantiate(PoliceCarPrefab, generatorPosition, Quaternion.identity); break;
        //     case 7: 
        //         GameObject.Instantiate(TaxiPrefab, generatorPosition, Quaternion.identity); break;
        //     defaulf: break;
        // }

        GameObject.Instantiate(BusPrefab[0], generatorPosition, Quaternion.identity);
    }
}
