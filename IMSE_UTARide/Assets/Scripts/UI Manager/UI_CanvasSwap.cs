/* Author: Jonah Bui
 * Contributors:
 * Date: May 18th, 2020
 * ------------------------
 * Purpose: Used to hide/ show canvases for use with a list of buttons to click on. Clicking each
 * button should show the according canvas attached to it and hide the rest.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CanvasSwap : MonoBehaviour
{
    // Root canvas that controls when the child panels should be displayed
    public Canvas parentCanvas;

    // List of canvases to display
    public List<GameObject> canvases = new List<GameObject>();

    // Use to check if the canvases are already being displayed to avoid redrawing them multiple
    // times.
    private bool canvasesEnabled;

    private int currentCanvas;
    // Start is called before the first frame update
    void Start()
    {
        currentCanvas = 0;
        canvasesEnabled = false;
        HideCanvases();
    }

    // Update is called once per frame
    void Update()
    {
        // Enable or disable the canvases according to the root canvas's status.
        try
        {
            if (!parentCanvas.enabled && canvasesEnabled)
            {
                HideCanvases();
                canvasesEnabled = false;
            }
            else if (parentCanvas.enabled && !canvasesEnabled)
            {
                ShowCanvas(currentCanvas);
                canvasesEnabled = true;
            }
        }
        catch
        {
        }
    }

    /* Description: To be used by a button. Using a list provided, enables the selected GameObject
     * to be visible.
     * Parameters: 
     *  index : int
     *      An integer used to index into the list canvases. This is the canvas gameobject to enable.
     * Return(s): nothing
     */
    public void ShowCanvas(int index)
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            Canvas canvas = canvases[i].GetComponent<Canvas>();
            if (i == index && canvas != null)
            {
                canvas.enabled = true;
            }
            else if (canvas != null)
            {
                canvas.enabled = false;
            }
        }
        currentCanvas = index;
    }

    /* Description: Hides all the canvases from player view using a list provided.
     * Parameters: nothing
     * Return(s): nothing
     */
    private void HideCanvases()
    {
        foreach (GameObject canvasObj in canvases)
        {
            Canvas canvas = canvasObj.GetComponent<Canvas>();
            try
            {
                canvas.enabled = false;
            }
            catch
            {
                Debug.LogError("Error");
            }
        }
    }
}
