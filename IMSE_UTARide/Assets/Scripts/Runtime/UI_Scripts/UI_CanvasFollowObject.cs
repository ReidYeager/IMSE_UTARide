/* Author: Jonah Bui
 * Contributors:
 * Date: March 24, 2020
 * ------------------------
 * Purpose: Make a list of GameObjects containing canvases follow an object.
 * Changelog:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CanvasFollowObject: MonoBehaviour
{
    [Header("GameObjects for UI")]
    [Tooltip("GameObjects containing canvases for the UI")]
    public List<GameObjectUI> UICanvasGameObject = new List<GameObjectUI>();
    [Tooltip("The parent object the UI will follow")]
    public GameObject attachedObject;
    void Start()
    {
        foreach (GameObjectUI obj in UICanvasGameObject)
        {
            // Make the canvas a child of the object to be attached to
            // Ensures the canvas follows the tablet as it moves
            obj.canvasGO.transform.SetParent(attachedObject.transform, false);

            UI_ZeroOutTransform(obj.canvasGO.transform);
            UI_SetTransformToObject(obj);
        }
    }

    void Update()
    {
    }

    // Zeros out the given transform
    void UI_ZeroOutTransform(Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void UI_SetTransformToObject(GameObjectUI g)
    {
        foreach (Transform child in g.canvasGO.transform)
        {
            RectTransform rect = child.GetComponentInChildren<RectTransform>();
            if (rect != null)
            {
                // Scale the UI element to fit the attached object (tablet)
                rect.localScale = g.scale;
                // Position the UI element relative to the attached GameObject
                rect.localPosition = g.position;
            }
        }
    }
}
