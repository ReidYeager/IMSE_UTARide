using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UI_AppFunctions))]
public class UI_ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UI_AppFunctions app = (UI_AppFunctions) target;

        if (GUILayout.Button("Order Ride"))
            app.LoadSceneViaButton(2);
    }
}
