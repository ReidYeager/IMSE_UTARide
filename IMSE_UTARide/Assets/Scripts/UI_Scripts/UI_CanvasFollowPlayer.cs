/* Author: Jonah Bui
 * Contributors:
 * Date: February 26, 2020
 * ------------------------
 * Purpose: Moves a GUI game object to follow a player game object while also keeping the UI facing the player.
 * Changelog:
 * - Refactored code to work around a pivot point gameObject. Allows the canvas to be positioned anywhere, while always rotating around the same point.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CanvasFollowPlayer : MonoBehaviour
{
    [Header("GameObjects")]
    [Tooltip("GameObjects containing canvases for the UI")]
    public List<GameObjectUI> UICanvasGameObject = new List<GameObjectUI>();
    public Transform player;
    [Tooltip("The main GameObject that contains all UI elements that follow the player.")]
    public Transform gui;

    [Header("UI Settings")]
    [Tooltip("Whether to follow the player's camera constantly without any slack.")]
    public bool delayedFollow;
    [Tooltip("How fast for the canvas to follow the player. Only matters if UI is delayed.")]
    public float UIDelta = 1f;
    [Tooltip("How far the player can look before the UI recenters in the player's view.")]
    public int degreeToRecenter = 20;

    private bool isUICentered;

    void Start()
    {
        // Position the GameObject UI initially in the same location as the player
        gui.position = player.position;
        gui.rotation = player.rotation; 
        foreach (GameObjectUI obj in UICanvasGameObject)
        {
            UI_SetupGUITransform(obj);
        }
        isUICentered = true;
    }

    void Update()
    {
        if (delayedFollow)
            UI_DelayedFollow(this.transform);
        else
            UI_Follow();
    }

    /* Description: Provide initialized variables with a value.
     */
    public void UI_SetupGUITransform(GameObjectUI g)
    {
        RectTransform rect = g.canvasGO.GetComponentInChildren<RectTransform>();
        if (rect != null)
        {
            // Set distance away from the pivot (player)
            rect.localPosition = g.position;
            rect.localScale = g.scale;
        }
    }

    /* Description: Do not want to constantly recenter UI, only when player looks past a certain
     * threshold. Constantly centering it makes it impossible to look around the UI
     */
    public void UI_DelayedFollow(Transform menuObject)
    {
        // Check if the user has passed the threshold required for recentering
        if (Mathf.Abs(this.transform.rotation.eulerAngles.y - player.rotation.eulerAngles.y) > degreeToRecenter)
            isUICentered = false;

        // Recenter the UI if not centered
        if (!isUICentered)
        {
            // Constantly move towards the players centerview until finally centered
            gui.rotation = Quaternion.RotateTowards(gui.rotation, player.rotation, UIDelta);

            // If the UI is back within the player center vision, stop trying to recenter the UI
            if (Mathf.Abs(this.transform.rotation.eulerAngles.y - player.rotation.eulerAngles.y) < 1f)
                isUICentered = true;
        }

        // Make sure UI follows the player
        gui.position = player.position;
    }

    /* Description: Follow the player constantly.
     */
    public void UI_Follow()
    {
        gui.position = player.position;
        gui.rotation = player.rotation; 
    }
}// Class
