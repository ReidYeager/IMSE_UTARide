/* Author: Jonah Bui
 * Contributors:
 * Date: June 12, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: Used to set the vehicle that the player has to find. It will also display information
 * on the tablet about the current ride the player has to find.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Need to implement icon for vehicle
// Need to randomize vehicles for players to find

public class UI_VehicleSelection : MonoBehaviour
{
    public Button buttonLeft;
    public Button buttonRight;

    public Text licensePlate;
    public Text vehicleType;
    public Text vehicleColor;

    public List<GameObject> vehiclePrefabs = new List<GameObject>();
    private GameObject currentVehicle;
    private int currentVehicleIndex;
    private Vector3 spawnPos;
    private Vector3 defaultPos;
    private Vector3 defaultRot;
    private Transform vehicleSpawner;

    private void Awake()
    {
        currentVehicleIndex = 0;
        buttonLeft.onClick.AddListener(() => NextVehicle(1));
        buttonRight.onClick.AddListener(() => NextVehicle(0));

        defaultPos = new Vector3(36f, 0f, 2f);
        defaultRot = new Vector3(0f, 90f, 0f);

        try
        {
            vehicleSpawner = GameObject.FindGameObjectWithTag("VehicleSpawnpoint").transform;
            if (vehicleSpawner == null)
                throw new NullReferenceException();
        }
        catch
        {
            Debug.LogError("[IMSE] Could not find vehicle spawner.");
        }
    }

    private void Start()
    {
        try
        {
            spawnPos = vehicleSpawner.transform.position;
        }
        catch
        {
            Debug.LogError("[IMSE] Could not find vehicle spawn position.");
            spawnPos = defaultPos;
        }
        CreateVehicle(vehiclePrefabs[currentVehicleIndex]);
    }

    private void OnDestroy()
    {
        buttonLeft.onClick.RemoveListener(() => NextVehicle(1));
        buttonRight.onClick.RemoveListener(() => NextVehicle(0));
        if(currentVehicle != null)
            Destroy(currentVehicle);
    }

    void Update()
    {
        
    }

    public void NextVehicle(int direction)
    {
        // 0 - right, 1 - left
        if (currentVehicle != null)
            Destroy(currentVehicle);

        if (direction == 1)
        {
            if (currentVehicleIndex > 0)
                currentVehicleIndex--;
            else
                currentVehicleIndex = vehiclePrefabs.Count - 1;
        }
        else
        {
            if (currentVehicleIndex < vehiclePrefabs.Count - 1)
                currentVehicleIndex++;
            else
                currentVehicleIndex = 0;
        }
        CreateVehicle(vehiclePrefabs[currentVehicleIndex]);
    }

    public void CreateVehicle(GameObject vehiclePrefab)
    {
        currentVehicle = Instantiate(vehiclePrefab);
        currentVehicle.transform.eulerAngles = defaultRot;
        currentVehicle.transform.position = spawnPos;
        SetVehicleInfo(currentVehicle);
        currentVehicle.transform.SetParent(vehicleSpawner);
    }

    public void SetVehicleInfo(GameObject vehiclePrefab)
    {
        VehicleInfo vi = vehiclePrefab.GetComponent<VehicleScript>().vehicleInfo;
        try
        {
            licensePlate.text = $"License Plate: {vi.licensePlate}";
            vehicleType.text = $"Vehicle Type: {vi.vehicleType}";
            vehicleColor.text = $"Vehicle Color: {vi.vehicleColor}";
        }
        catch
        {
            Debug.LogWarning("[IMSE] Could not find vehicle info or UI elements for vehicle not set in inspector.");
        }

    }
}
