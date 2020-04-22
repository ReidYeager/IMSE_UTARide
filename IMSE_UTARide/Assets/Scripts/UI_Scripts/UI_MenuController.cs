/* Author: Jonah Bui
 * Contributors:
 * Date: March 27, 2020
 * ------------------------
 * Purpose: Attach to a menu UI object to control its appearance/ functions and its submenus.
 * Changelog:
 * NOTE: not yet fully implemented
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuController : MonoBehaviour
{
    [Header("GameObjects")]
    [Tooltip("GameObjects containing canvases for the UI")]
    public GameObject mainCanvas;
    public List<GameObject> submenus = new List<GameObject>();

    [Header("Menu Settings")]
    [Tooltip("Whether to disable the menu on game startup")]
    public bool disableOnStartup = false;
    void Start()
    {
        if (disableOnStartup)
            mainCanvas.SetActive(false);

        ToggleSubmenus(0);
    }

    void Update()
    {
    }

    public void ToggleSubmenus(int menuIndex)
    {
        foreach (GameObject go in submenus)
        {
            go.SetActive(false);
        }
        submenus[menuIndex].SetActive(true);
    }
}
