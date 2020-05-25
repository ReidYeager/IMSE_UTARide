/* Author: Jonah Bui
 * Contributors:
 * Date: February 26, 2020
 * ------------------------
 * Purpose: Moves a GUI game object to follow a player game object while also keeping the UI 
 * transformed in offset position.
 * Changelog:
 * March XX, 2020
 * ------------------------------------------------------------------------------------------------
 * - Refactored code to work around a pivot point gameObject. Allows the canvas to be positioned 
 * anywhere, while always rotating around the same point.
 * 
 * April 17th, 2020
 * ------------------------------------------------------------------------------------------------
 * - Refactored code to work around a GameObject that contains a single UI element to reduce 
 * overhead.
 * - Put all thiss in this gameobject tree form:
 * >GameObject
 * >>GameObject w/Canvas
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CanvasFollowPlayer : MonoBehaviour
{
    [Header("Position Offset")]
    [Tooltip("Set UI local position relative to object")]
    public Vector3 position;

    [Header("Rotation Offset")]
    [Tooltip("Set UI local angle relative to object")]
    public Vector3 rotation;

    [Header("GameObjects")]
    [Tooltip("GameObject containing a canvas child object")]
    public GameObject player;

    [Header("UI Settings")]
    [Tooltip("Whether to follow the player's camera constantly without any slack.")]
    public bool delayedFollow;
    [Tooltip("How fast for the canvas to follow the player. Only matters if UI is delayed.")]
    public float UIDelta = 1f;
    [Tooltip("How far the player can look before the UI recenters in the player's view.")]
    public int degreeToRecenter = 20;

    // Use this to determine whether the UI is centered in its offset position
    // If the variable is false, use it to reposition the UI
    private bool isUICentered;

    // Store the transform of the canvas that will follow player to avoid repeated GetComponents
    private RectTransform canvas_rect;
    void Start()
    {
        canvas_rect = this.transform.GetChild(0).GetComponent<RectTransform>();

        // Set transform of parent gameobject to object-to-follow, so it's positioned correctly
        this.transform.SetParent(player.transform, false);

        // Position the parent GameObject in the same position as the player and orientate it
        // to face the player position so that it the UI is properly aligned with player.
        this.transform.position = player.transform.position;
        this.transform.rotation = player.transform.rotation;

        // Set the transform of the Canvas's RectTransform
        canvas_rect.localPosition = position;
        canvas_rect.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        //canvas_rect.localScale = scale;

        // Set the UI centered initially so it centers on player on startup
        isUICentered = false;
    }

    void Update()
    {
        if (delayedFollow)
            UI_DelayedFollow();
    }

    /* Description: Do not want to constantly recenter UI, only when player looks past a certain
     * threshold. Constantly centering it makes it impossible to look around the UI that is not
     * in the player's center of vision.
     */
    public void UI_DelayedFollow()
    {
        // UI is not centered to player view, so set variable to false in order to start centering
        // Occurs if the direction the player is looking compared to the direction of the canvas is
        // greater than the threshold.
        if (Mathf.Abs(canvas_rect.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) > degreeToRecenter)
            isUICentered = false;

        // Recenter the UI if not centered. Only stop when UI is positioned in the correct offset
        // position.
        if (!isUICentered)
        {
            // Constantly move UI towards the players centerview until finally centered so UI is visuble
            canvas_rect.rotation = Quaternion.RotateTowards(canvas_rect.rotation, player.transform.rotation, UIDelta);

            // If the UI is back within the player center vision, stop trying to recenter the UI
            if (Mathf.Abs(canvas_rect.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) < 1f)
                isUICentered = true;
        }
    }
}// Class
