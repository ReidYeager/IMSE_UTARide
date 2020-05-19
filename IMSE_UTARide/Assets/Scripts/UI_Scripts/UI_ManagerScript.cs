/* Author: Jonah Bui
 * Contributors:
 * Date: March 24, 2020
 * ------------------------
 * Purpose: Control the UI elements functions, appearance, and to maintain them in a single 
 * controlled environment.
 * 
 * Changes:
 * May 18th, 2020:
 *  - Removed functions such as user controls for tilt, hiding menus, and update documentation.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ManagerScript : MonoBehaviour
{
    //The GameObject that holds all UI elements
    public GameObject gui;

    [Header("UI Settings")]

    // Use this list to store the GameObjects containing canvases of apps to load and the home
    // screen.
    public List<GameObject> UIs = new List<GameObject>();

    void Start()
    {
        // Hide every UI at startup except the first canvas UI. Don't want to draw everything
        // on start up for performance reasons.
        CloseApp();
    }

    /* Description: To be used by a button. Using a list provided, enables the selected GameObject
     * to be visible.
     * Parameters: 
     *  appIndex : int
     *      An integer used to index into the list "UIs". This is the canvas gameobjec to enable.
     * Return(s): nothing
     */
    public void OpenApp(int appIndex)
    {
        for (int i = 0; i < UIs.Count; i++)
        {
            Canvas canvas = UIs[i].GetComponent<Canvas>();
            if (i == appIndex && canvas != null)
            {
                canvas.enabled = true;
            }
            else if (canvas != null)
            {
                canvas.enabled = false;
            }
        }
    }

    /* Description: To be used by a button. Disables all the UI GameObjects except the first one in 
     * the list. Note: the first canvas should be the home screen.
     * Parameters: nothing
     * Return(s): nothing
     */
    public void CloseApp()
    {
        for (int i = 0; i < UIs.Count; i++)
        {
            Canvas canvas = UIs[i].GetComponent<Canvas>();
            if (i == 0 && canvas != null)
            {
                canvas.enabled = true;
            }
            else if (canvas != null)
            {
                canvas.enabled = false;
            }
        }
    }
}
