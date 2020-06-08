/* Author: Jonah Bui
 * Contributors:
 * Date: June 7, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: extends the inspector for the Slide script. Redesigns the default inspector for ease of
 * use mainly. Also controls the canvas provided by the slide.
 * ------------------------------------------------------------------------------------------------
 * Changelog:
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(Slide))]
public class SlideEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        Slide s = (Slide)target;

        // Update size of canvas if scaling changed in inspector
        s.Scale(s.canvasScale);

        if (s.scheme != null)
            s.Scheme(s.scheme);

        // Disable the canvas if the script is also disabledin inspector
        if (s.enabled)
        {
            s.canvas.enabled = true;
        }
        else
        {
            s.canvas.enabled = false;
        }

        // Update slide text and images here
        GUILayout.Label("Slide Text", EditorStyles.boldLabel);
        EditorStyles.textArea.wordWrap = true;
        s.text.text = GUILayout.TextArea(s.text.text, GUILayout.MinHeight(100f));

        serializedObject.ApplyModifiedProperties();
    }
}
