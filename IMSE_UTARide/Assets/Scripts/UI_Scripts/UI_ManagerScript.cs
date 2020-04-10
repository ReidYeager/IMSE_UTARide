/* Author: Jonah Bui
 * Contributors:
 * Date: March 24, 2020
 * ------------------------
 * Purpose: Control the UI elements functions, appearance, and to maintain them in a controlled environment.
 * NOTE: Needs to be refactored
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ManagerScript : MonoBehaviour
{
    //The GameObject that holds all UI elements
    public GameObject gui;

    // maxRotation, minRotation: Used to quaternions for how far the user can tilt their UI for viewability depending on their location
    // maxTilt, minTilt: Used to set the bounds for how far the UI may tilt
    [Header("UI Settings")]
    private Quaternion maxRotation, minRotation;
    [Tooltip("How far the canvas may rotate back or forwards")]
    public float maxTilt = 20f;
    public float minTilt = -20f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        // User controls
        UI_UserInput.UI_ToggleMenuVisibity(gui);
        UI_UserInput.UI_OVR_ToggleMenuVisibity(gui);
        UI_UserInput.UI_OVR_TiltMenu(gui, maxTilt, minTilt);
    }
}
