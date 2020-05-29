/* Author: Jonah Bui
 * Contributors:
 * Date: March 24, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: Make a GameObject holding a child GameObject canvas follow an object. 
 * 
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

    [Header("Relative Scaling")]
    [Tooltip("Set the scale of the UI object")]
    public Vector3 scale;   // To implement

    [Header("GameObjects")]
    public Canvas indoorCanvas;
    public Canvas outdoorCanvas;
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
        float scale = 0.001f * 0.75f;
        RectTransform canvasRectIndoor = indoorCanvas.GetComponent<RectTransform>();
        canvasRectIndoor.localPosition = position;
        canvasRectIndoor.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        canvasRectIndoor.localScale = new Vector3(scale, scale, scale);

        RectTransform canvasRectOutdoor = outdoorCanvas.GetComponent<RectTransform>();
        canvasRectOutdoor.localPosition = position;
        canvasRectOutdoor.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        canvasRectOutdoor.localScale = new Vector3(scale, scale, scale);
    }

    private void FollowObject()
    {
        // By attaching the parent object to the object-to-follow, the parent object will always 
        // follow the object-to-follow and follow the same orietnation
        indoorCanvas.transform.SetParent(objectToFollow.transform, false);
        outdoorCanvas.transform.SetParent(objectToFollow.transform, false);

        // Set the absolute position of the parent object to the object-to-follow so that it is
        // centered to on it and has the same orientation
        indoorCanvas.transform.position = objectToFollow.transform.position;
        indoorCanvas.transform.rotation = objectToFollow.transform.rotation;
        outdoorCanvas.transform.position = objectToFollow.transform.position;
        outdoorCanvas.transform.rotation = objectToFollow.transform.rotation;
    }
}
