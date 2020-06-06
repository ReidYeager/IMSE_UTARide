/* Author: Jonah Bui
 * Contributors:
 * Date: March 24, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: Control the UI elements functions, appearance, and to maintain them in a single 
 * controlled environment.
 * 
 * Changes:
 * May 18, 2020:
 * ------------------------------------------------------------------------------------------------
 *  - Removed functions such as user controls for tilt, hiding menus, and update documentation.
 *  
 * May 29, 2020:
 * ------------------------------------------------------------------------------------------------
 *  - Added two gameobject references to indoor canvas gameobject and outdoor canvas gameobject.
 *    Also made a list for the canvases contained in each of those gameobjects.
 *  - Depending on the current active scene, the indoor and outdoor canvases will be activated.
 *  - Updated CloseApp() and OpenApp() functions to utilize the new canvas gameobjects.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_ManagerScript : MonoBehaviour
{
    [Header("Canvas GameObjects")]
    //The GameObject that holds all UI elements
    public GameObject indoorGameObject;
    public GameObject outdoorGameObject;

    [Header("UI Settings")]
    [Tooltip("Use these to load the according canvas GameObjects depending on the current" +
        "active scene")]
    public int indoorBuildIndex;
    public int outdoorBuildIndex;
    // Use this list to store the GameObjects containing canvases of apps to load and the home
    // screen.
    public List<GameObject> indoorUIs = new List<GameObject>();
    public List<GameObject> outdoorUIs = new List<GameObject>();

    private void Start()
    {
        ChangeTabletCanvas(SceneManager.GetActiveScene());
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
        if (SceneManager.GetActiveScene().name == "Indoor")
        {
            for (int i = 0; i < indoorUIs.Count; i++)
            {
                Canvas canvas = indoorUIs[i].GetComponent<Canvas>();
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
        if (SceneManager.GetActiveScene().name == "City")
        {
            for (int i = 0; i < outdoorUIs.Count; i++)
            {
                Canvas canvas = outdoorUIs[i].GetComponent<Canvas>();
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
    }

    /* Description: To be used by a button. Disables all the UI GameObjects except the first one in 
     * the list. Note: the first canvas should be the home screen.
     * Parameters: nothing
     * Return(s): nothing
     */
    public void CloseApp()
    {
        if (SceneManager.GetActiveScene().name == "Indoor")
        {
            for (int i = 0; i < indoorUIs.Count; i++)
            {
                Canvas canvas = indoorUIs[i].GetComponent<Canvas>();
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
        if (SceneManager.GetActiveScene().name == "City")
        {
            for (int i = 0; i < outdoorUIs.Count; i++)
            {
                Canvas canvas = outdoorUIs[i].GetComponent<Canvas>();
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
        Debug.Log($"[IMSE] Active scene: {SceneManager.GetActiveScene().name}");
    }

    /* Description: To be used two canvases that are specified to be loaded in their respecitve 
     * scenes. Shows the appropriate canvas GameObject depending on the current active scene.
     * Parameters: nothing
     * Return(s): nothing
     */
    private void ChangeTabletCanvas(Scene scene)
    {
        try
        {
            Canvas indoorCanvas = indoorGameObject.GetComponent<Canvas>();
            Canvas outdoorCanvas = outdoorGameObject.GetComponent<Canvas>();

            if (scene == SceneManager.GetSceneByBuildIndex(indoorBuildIndex))
            {
                indoorCanvas.enabled = true;
                outdoorCanvas.enabled = false;
                CloseApp();
            }
            else if (scene == SceneManager.GetSceneByBuildIndex(outdoorBuildIndex))
            {
                indoorCanvas.enabled = false;
                outdoorCanvas.enabled = true;
                CloseApp();
            }
            else
            {
                indoorCanvas.enabled = false;
                outdoorCanvas.enabled = false;
            }
        }
        catch
        {
            Debug.LogError("[IMSE] Error: canvas GameObject could not be retrieved ");
        }
    }
}
