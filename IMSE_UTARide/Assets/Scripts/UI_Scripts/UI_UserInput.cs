/* Author: Jonah Bui
 * Contributors:
 * Date: March 24, 2020
 * ------------------------
 * Purpose: define functions that the user canvas perform on the UI.
 * NOTE: Needs to be refactored
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UserInput : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    // Purpose: Control the visibility of the menu GameObject
    // Parameters:
    // * menu: the GameObject holding the canvas child
    public static void UI_ToggleMenuVisibity(GameObject menu)
    {
        if (Input.GetKeyDown(UI_InputMapping.ToggleMenuVisibility))
            if (menu.activeSelf)
                menu.SetActive(false);
            else if(!menu.activeSelf)
                menu.SetActive(true);
    }

    // Purpose: Control the visibility of the menu GameObject
    // Parameters:
    // * menu: the GameObject holding the canvas child
    public static void UI_OVR_ToggleMenuVisibity(GameObject menu)
    {
        if (OVRInput.GetDown(UI_InputMapping.OVR_ToggleMenuVisibilty))
            if (menu.activeSelf)
                menu.SetActive(false);
            else
                menu.SetActive(true);
    }

    // Purpose: Tilts the menu GameObject for their visibility
    // Parameters:
    // * menu: the GameObject holding the canvas child
    // * maxTilt: the maximum rotation the canvas may go up to
    // * minTilt: the minimum rotation the canvas may go down to
    public static void UI_OVR_TiltMenu(GameObject menu, float maxTilt, float minTilt)
    {
        Vector2 input = OVRInput.Get(UI_InputMapping.OVR_RightJoyStick);

        // Initialize quaternions that the GameObject will tilt towards
        Quaternion maxRotation = Quaternion.Euler(
            maxTilt,
            0f,
            menu.transform.localRotation.eulerAngles.z
        );

        Quaternion minRotation = Quaternion.Euler(
                minTilt,
                0f,
                menu.transform.localRotation.eulerAngles.z
        );

        if (input.y > 0)
            menu.transform.localRotation = Quaternion.RotateTowards(menu.transform.localRotation, maxRotation, 1f);
        else if (input.y < 0)
            menu.transform.localRotation = Quaternion.RotateTowards(menu.transform.localRotation, minRotation, 1f);
    }
}
