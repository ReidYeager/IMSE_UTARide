/* Author: Bishesh Shrestha
 * Contributors: Jonah Bui
 * Date: March 24, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: Make a list of GameObjects containing canvases follow an object.
 * 
 * Changelog: 
 * March 23, 2020
 * ------------------------------------------------------------------------------------------------
 * - Added an option for a fixed minimap camera or one that follows the player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Minimap : MonoBehaviour
{
    // In order to make a minimap, get a camera and make it target a texture and display it as a 
    // raw image in a Canvas
    
    [Header("GameObjects")]
    [Tooltip("GameObjects containing canvases for the UI")]
    public GameObject player;
    public GameObject minimapCamera;

    [Header("Minimap Options")]
    [Tooltip("GameObjects containing canvases for the UI")]
    public bool fixedCamera;
    [Tooltip("Only use if fixed camera enabled")]
    public Vector3 fixedPosition;
    public Vector3 angle;
    public float heightAbovePlayer;

    void Start()
    {
        // Set the fixed position of the camera
        if (fixedCamera)
        {
            minimapCamera.transform.position = fixedPosition;
        }

        // Angle the camera to display the correct view for the player minimap
        minimapCamera.transform.rotation = Quaternion.Euler(angle.x,angle.y,angle.z);
    }

    void Update()
    {
        // Follow the player around 
        if (!fixedCamera)
        {
            minimapCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+heightAbovePlayer, player.transform.position.z);
        }
    }
}
