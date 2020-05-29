/* Author: Jonah Bui
 * Contributors:
 * Date: May 25, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: allows the user to load a scene using a button in the editor rather than having
 * to test functionality by building the scene.
 * 
 * Changelog:
 */
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UI_AppFunctions))]
public class UI_ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UI_AppFunctions app = (UI_AppFunctions) target;
        // NOTE: do not press the buttons that would load you into the same scene you are
        // currently in. Doing so will cause the game to glitch out. Must safeguard. To fix....

        // Spawn into city
        if (GUILayout.Button("Order Ride"))
        {
            app.LoadSceneViaButton(2);
            app.UnloadSceneViaButton(1);
            WorldState.instance.SetTimeOfDay();
        }

        // Return to indoor environment
        if (GUILayout.Button("Return Home"))
        {
            app.LoadSceneViaButton(1);
            app.UnloadSceneViaButton(2);
        }
    }
}
