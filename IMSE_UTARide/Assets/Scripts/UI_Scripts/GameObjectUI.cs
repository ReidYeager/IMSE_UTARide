/* Author: Jonah Bui
 * Contributors:
 * Date: March 24, 2020
 * ------------------------
 * Purpose: A class used to store information about a GameObject and it's transoform relative to 
 * it's parent GameObject.
 * Changelog:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectUI
{
    [Header("Canvas GameObject")]
    [Tooltip("GameObjects containing canvases for the UI")]
    public GameObject canvasGO;
    [Header("Position Offset")]
    [Tooltip("Use to position UI element away from object")]
    public Vector3 position;

    [Header("Rotation Offset")]
    [Tooltip("Use to angle the UI element locally")]
    public Vector3 rotation;

    [Header("Scale")]
    [Tooltip("Use to scale the UI relative to a GameObject")]
    public Vector3 scale;

}
