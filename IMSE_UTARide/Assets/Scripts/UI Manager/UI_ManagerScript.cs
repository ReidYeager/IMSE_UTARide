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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeTabletCanvas(scene);
    }

    /* Description: To be used by a button. Using a list provided, enables the selected GameObject
     * to be visible.
     * Parameters: 
     *  appIndex : int
     *      An integer used to index into the list "UIs". This is the canvas gameobjec to enable.
     * Return(s): nothing
     */
    public void OpenApp(List<GameObject> UIs, int appIndex)
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
    public void CloseApp(List<GameObject> UIs)
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
                CloseApp(indoorUIs);
            }
            else if (scene == SceneManager.GetSceneByBuildIndex(outdoorBuildIndex))
            {
                indoorCanvas.enabled = false;
                outdoorCanvas.enabled = true;
                CloseApp(outdoorUIs);
            }
            else
            {
                indoorCanvas.enabled = false;
                outdoorCanvas.enabled = false;
            }
        }
        catch
        {
            Debug.LogError("Error: canvas GameObject could not be retrieved ");
        }
    }
}
