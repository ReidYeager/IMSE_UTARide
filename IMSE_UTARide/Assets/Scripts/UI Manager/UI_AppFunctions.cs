/* Author: Jonah Bui
 * Contributors:
 * Date: March 25, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: Provide functions that can be utilized by the apps (really just canvases labelled as
 * apps).
 * 
 * Changelog:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_AppFunctions : MonoBehaviour
{
    public void LoadSceneViaButton(int index)
    {
        WorldState.instance.LoadScene(index);
        WorldState.instance.SetTimeOfDay();
    }

    public void UnloadSceneViaButton(int index)
    {
        WorldState.instance.UnloadScene(index);
    }
}
