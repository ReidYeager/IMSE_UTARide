/* Author: Jonah Bui
 * Contributors:
 * Date: March 24, 2020
 * ------------------------
 * Purpose: Make a GameObject holding a child GameObject canvas follow an object. 
 * Changelog:
 * April 17th, 2020
 * ------------------------------------------------------------------------------------------------
 * - Refactored code around a single canvas GameObject to simplify code and provide better modularity
 */
using UnityEngine;
using UnityEngine.UI;

public class UI_CanvasFollowObject : MonoBehaviour
{
    [Header("Position Offset")]
    [Tooltip("Set UI local position relative to object")]
    public Vector3 position;

    [Header("Rotation Offset")]
    [Tooltip("Set UI local angle relative to object")]
    public Vector3 rotation;

    [Header("GameObjects")]
    [Tooltip("The parent object the UI will follow")]
    public GameObject objectToFollow;
    void Start()
    {
        // Set the canvas in the inital position of the object-to-follow so it appears in the
        // correct view on startup
        FollowObject();

        // Offset the canvas away from the parent object. Use position and rotation vectors
        // to transform the canvas into the appropriate position for the user to interact
        // with the UI on the object-to-follow
        RectTransform canvas_rect = this.transform.GetChild(0).GetComponent<RectTransform>();
        canvas_rect.localPosition = position;
        canvas_rect.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    private void FollowObject()
    {
        // By attaching the parent object to the object-to-follow, the parent object will always 
        // follow the object-to-follow and follow the same orietnation
        this.transform.SetParent(objectToFollow.transform, false);

        // Set the absolute position of the parent object to the object-to-follow so that it is
        // centered to on it and has the same orientation
        this.transform.position = objectToFollow.transform.position;
        this.transform.rotation = objectToFollow.transform.rotation;
    }
}
