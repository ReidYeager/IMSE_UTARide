using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerManager))]
public class PlayerManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerManager pm = (PlayerManager)target;

        GUILayout.Label($"Status of Variables", EditorStyles.boldLabel);
        GUILayout.Label($"Player: {pm.Player}");
        GUILayout.Label($"OVRPlayer: {pm.OVRPlayer}");
        GUILayout.Label($"OVRController: {pm.OVRController}");
        GUILayout.Label($"CharController: {pm.CharController}");
        GUILayout.Label($"UIM: {pm.UIMS}");

        GUILayout.Space(20f);
        GUILayout.Label($"Player Tablet Functions", EditorStyles.boldLabel);

        // NOTE: do not click on the buttons of the scenes that are already currently loaded.

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
}
