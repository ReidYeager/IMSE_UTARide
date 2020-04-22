using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject cubeSpawner;
    public void Cube()
    {
        Instantiate(cubePrefab, cubeSpawner.transform.position,Quaternion.identity);
    }
}
