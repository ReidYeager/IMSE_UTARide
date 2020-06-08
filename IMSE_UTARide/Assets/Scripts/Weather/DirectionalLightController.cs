/* Author: Jonah Bui
 * Contributors:
 * Date: May 28, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: changes the lighting of the main light source in the scene according to the time
 * set in the WorldState instance.
 * 
 * Changelog:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectionalLightController : MonoBehaviour
{
    public Light mainLight;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /* Description: trigger event on scene load t up update the lighting in the scene.
     * Parameter(s):
     * Returns:
     */
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int)WorldState.sceneNames.City)
        {
            AdjustLighting();
        }
    }

    /* Description: updates the lighting based off of the time in WorlState.instance.currenTime variable.
     * Parameter(s): none
     * Returns: nothing
     */
    private void AdjustLighting()
    {
        try
        {
            switch (WorldState.instance.CurrentTime)
            {
                case WorldState.DayTimes.MORNING:
                    break;
                case WorldState.DayTimes.NOON:
                    break;
                case WorldState.DayTimes.EVENING:
                    break;
                case WorldState.DayTimes.NIGHT:
                    mainLight.color = WorldState.instance.worldWeatherPalettes[2].skyGradients[0].Evaluate(1f);
                    break;
                default:
                    mainLight.color = WorldState.instance.worldWeatherPalettes[2].skyGradients[0].Evaluate(1f);
                    break;
            }
        }
        catch
        {
            Debug.LogError("[IMSE] WorldState could not be found.");
        }
    }
}
