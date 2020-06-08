/* Author: Jonah Bui
 * Contributors:
 * Date: June 6, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: extends the TutorialManager inspector. Can be used to generate new slides or control
 * already existing ones.
 * ------------------------------------------------------------------------------------------------
 * Changelog:
 * 
 * June 7, 2020
 * ------------------------------------------------------------------------------------------------
 * - Updated TutorialManagerEditor to work with new Slide gameObject.
 */
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TutorialManager))]
public class TutorialManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SerializedProperty m_textToParse = serializedObject.FindProperty("textToParse");
        SerializedProperty m_toggleSlides = serializedObject.FindProperty("toggleSlides");
        SerializedProperty m_slideCount = serializedObject.FindProperty("slideCount");
        SerializedProperty m_textDelimiter = serializedObject.FindProperty("textDelimiter");
        TutorialManager tm = (TutorialManager)target;

        // World Position Input
        tm.worldPosition = EditorGUILayout.Vector3Field("World Position", tm.worldPosition);
        if(tm.outputSlides != null)
            tm.outputSlides.transform.position = tm.worldPosition;

        // Slide Color Scheme Input
        GUILayout.Space(10f);
        tm.scheme = (SlideColorScheme)EditorGUILayout.ObjectField("Slide Color Scheme", tm.scheme, typeof(SlideColorScheme), true);
        
        // Output Slide Object Input
        tm.outputSlides = (GameObject)EditorGUILayout.ObjectField("Output Slide Object", tm.outputSlides, typeof(GameObject), true);

        // Slides List Input
        GUILayout.Space(10f);
        m_toggleSlides.boolValue = EditorGUILayout.Foldout(m_toggleSlides.boolValue, "Slides");
        if (m_toggleSlides.boolValue)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Size");
            m_slideCount.intValue = EditorGUILayout.IntField(m_slideCount.intValue);

            if (m_slideCount.intValue > tm.slideGOs.Count)
                while (m_slideCount.intValue > tm.slideGOs.Count)
                {
                    GameObject slideHolder = null;
                    tm.slideGOs.Add(slideHolder);
                }   
            if (m_slideCount.intValue < tm.slideGOs.Count)
                while (m_slideCount.intValue < tm.slideGOs.Count)
                {
                    tm.slideGOs.RemoveAt(tm.slideGOs.Count - 1);
                    EditorUtility.SetDirty(target);
                }
            GUILayout.EndHorizontal();
            for (int i = 0; i < tm.slideGOs.Count; i++)
                tm.slideGOs[i] = (GameObject)EditorGUILayout.ObjectField($"Slide {i}", tm.slideGOs[i], typeof(GameObject), true);
        }

        // Text To Parse Input
        GUILayout.Space(10f);
        GUILayout.Label("Generate Slides", EditorStyles.boldLabel);
        GUILayout.Label("Text To Parse");
        GUIStyle wrap = new GUIStyle(EditorStyles.textArea);
        wrap.wordWrap = true;
        m_textToParse.stringValue = EditorGUILayout.TextArea(m_textToParse.stringValue, wrap, GUILayout.MinHeight(100f));

        // Character delimiters
        GUILayout.BeginHorizontal();
        GUILayout.Label("Delimit Character");
        m_textDelimiter.stringValue = EditorGUILayout.TextField(m_textDelimiter.stringValue);
        GUILayout.EndHorizontal();

        // Parse Text Button
        if (GUILayout.Button("Parse Text"))
        {
            // If not output object presented, make one 
            if (tm.outputSlides == null)
            {
                GameObject slideHolder = new GameObject();
                tm.outputSlides = slideHolder;
                slideHolder.name = "Slides Output";
                slideHolder.transform.SetParent(tm.transform);
            }

            // If no color scheme specifie, assign a default one
            if (tm.scheme == null)
            {
                tm.scheme = Resources.Load<SlideColorScheme>(ResourcePaths.SCS_NIGHT);
                if (tm.scheme == null)
                    Debug.LogWarning("[IMSE] Default color scheme could not be found.");
            }

            // Clear out current list if it isnt empty
            while(tm.slideGOs.Count > 0)
                tm.slideGOs.RemoveAt(tm.slideGOs.Count-1);

            // Split the string and create them into slides
            string[] parsedString = m_textToParse.stringValue.Split(m_textDelimiter.stringValue[0]);

            Debug.Log($"{parsedString.Length}");

            for (int i = 0; i < parsedString.Length; i++)
            {
                // Create new slide 
                GameObject slideHolder = new GameObject();
                Slide slide = slideHolder.AddComponent<Slide>();

                // Set slide default parameters
                slide.scheme = tm.scheme;
                slide.Scheme(tm.scheme);
                slide.canvasScale = Vector3.one * 0.5f;
                slide.text.text = parsedString[i];

                // Place new slides under output gameObject
                slideHolder.transform.SetParent(tm.outputSlides.transform);
                slideHolder.name = $"Slide {i}";    
                tm.slideGOs.Add(slideHolder);
            }
            Selection.activeGameObject = tm.gameObject;
            // Update slides foldout
            m_slideCount.intValue = parsedString.Length;

            SlideEditor[] call = Resources.FindObjectsOfTypeAll<SlideEditor>();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
