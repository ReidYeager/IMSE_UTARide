/* Author: Jonah Bui
 * Contributors:
 * Date: June 6, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: provides an interface to the the player gameobject by displaying its status and
 * provides visibility of functions to the user without needing to compile and build the game.
 */
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerManager))]
public class PlayerManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();

        SerializedProperty m_appIndex = serializedObject.FindProperty("e_appIndex");
        SerializedProperty m_playePrefab = serializedObject.FindProperty("playerPrefab");
        GUIStyle textfieldStyle = new GUIStyle(EditorStyles.textField);
        PlayerManager pm = (PlayerManager)target;

        GUILayout.BeginHorizontal();
        pm.playerPrefab = (GameObject) EditorGUILayout.ObjectField("Player Prefab", pm.playerPrefab, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        // Use to visualize the state of gameObjects in the scene
        GUILayout.Label($"Status of Variables", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label($"OVRPlayer: ", GUILayout.Width(100f));
            if (pm.OVRPlayer != null)
            {
                textfieldStyle.normal.textColor = Color.green;
                GUILayout.Label($"Found", textfieldStyle);
            }
            else
            {
                textfieldStyle.normal.textColor = Color.red;
                GUILayout.Label($"Not found", textfieldStyle);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label($"OVRController: ", GUILayout.Width(100f));
            if (pm.OVRController != null)
            {
                textfieldStyle.normal.textColor = Color.green;
                GUILayout.Label($"Found", textfieldStyle);
            }
            else
            {
                textfieldStyle.normal.textColor = Color.red;
                GUILayout.Label($"Not found", textfieldStyle);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label($"CharController: ", GUILayout.Width(100f));
            if (pm.CharController != null)
            {
                textfieldStyle.normal.textColor = Color.green;
                GUILayout.Label($"Found", textfieldStyle);
            }
            else
            {
                textfieldStyle.normal.textColor = Color.red;
                GUILayout.Label($"Not found", textfieldStyle);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label($"UIM: ", GUILayout.Width(100f));
            if (pm.UIMS != null)
            {
                textfieldStyle.normal.textColor = Color.green;
                GUILayout.Label($"Found", textfieldStyle);
            }
            else
            {
                textfieldStyle.normal.textColor = Color.red;
                GUILayout.Label($"Not found", textfieldStyle);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label($"UIVS: ", GUILayout.Width(100f));
            if (pm.UIVS != null)
            {
                textfieldStyle.normal.textColor = Color.green;
                GUILayout.Label($"Found", textfieldStyle);
            }
            else
            {
                textfieldStyle.normal.textColor = Color.red;
                GUILayout.Label($"Not found", textfieldStyle);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        GUILayout.Label($"Player Tablet Functions", EditorStyles.boldLabel);
        // NOTE: do not click on the buttons of the scenes that are already currently loaded.

        GUILayout.BeginHorizontal();
        {
            // Spawn into city
            if (GUILayout.Button("Order Ride"))
            {
                pm.LoadSceneViaButton(2);
                pm.UnloadSceneViaButton(1);
                WorldState.instance.SetTimeOfDay();
            }

            // Return to indoor environment
            if (GUILayout.Button("Return Home"))
            {
                pm.LoadSceneViaButton(1);
                pm.UnloadSceneViaButton(2);
            }
        }
        GUILayout.EndHorizontal();
        EditorGUILayout.HelpBox("Do not click buttons that would send the player to a scene that is" +
            "already loaded. Ex: don't \"Order Ride\" in the outdoor environment.", MessageType.Warning);
        GUILayout.Space(5f);

        // Player app controls
        GUILayout.Label("App management");
        //// Open app on tablet
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Open App @ Index"))
            {
                pm.UIMS.OpenApp(m_appIndex.intValue);
                Debug.Log($"[IMSE] Opening app @ index {m_appIndex.intValue}.");
            }
            m_appIndex.intValue = EditorGUILayout.IntField(m_appIndex.intValue);
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
        //// Close app on tablet
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Close App"))
            {
                pm.UIMS.CloseApp();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(5f);

        GUILayout.Label("Ride Modifications");
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Previous Vehicle"))
            {
                pm.UIVS.NextVehicle(1);
            }
            if (GUILayout.Button("Next Vehicle"))
            {
                pm.UIVS.NextVehicle(0);
            }
        }
        GUILayout.EndHorizontal();
    }
}
