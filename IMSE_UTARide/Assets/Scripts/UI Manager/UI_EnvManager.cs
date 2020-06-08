/* Author: Jonah Bui
 * Contributors:
 * Date: May 18, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: Sets the environmental variables that can be used to customize the player's scene.
 * 
 * Changelog:
 * May 29, 2020
 * ------------------------------------------------------------------------------------------------
 * - Made an UpdateTimeText function so that it can be called independently of Awake()
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_EnvManager : MonoBehaviour
{
    // Text display for environment app canvas
    public Text timeText;
    public Text rainText;

    private bool rainToggle = false;
    private WorldState.DayTimes timeOfDay = WorldState.DayTimes.NOON;

    // Use to increment time of day
    private int timeStep = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /* Description: Used to trigger lights on scene load.
     * Parameter(s): none
     * Returns: nothing
     */
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // On scene load, make sure the variables in the tablet match those in the WorldState
        try
        {
            timeOfDay = WorldState.instance.CurrentTime;
            timeStep = (int)timeOfDay;
            UpdateTimeText();

            if (WorldState.instance.currentWeather == WorldState.weatherStates.rainy)
            {
                rainToggle = true;
                rainText.text = "Rain: On";
            }
            else
            {
                rainToggle = false;
                rainText.text = "Rain: Off";
            }
        }
        catch
        {
            Debug.LogError("[IMSE] Could not find the WorldState Instance");
        }
    }

    /* Description: To be used by a button. Sets the time variable and updates the according UI
     * text in the Environment Manager app canvas.
     * Parameters: nothing
     * Return(s): nothing
     */
    public void SetTime()
    {
        timeStep = (timeStep + 1) % 4;
        UpdateTimeText();
        WorldState.instance.CurrentTime = timeOfDay;
    }

    /* Description: To be used by a button. Toggles the rain variable and updates UI texts/
     * display in the Environment Manager app canvas.
     * Parameters: nothing
     * Return(s): nothing
     */
    public void SetWeather()
    {
        rainToggle = !rainToggle;
        if (rainToggle)
        {
            rainText.text = "Rain: On";
            WorldState.instance.currentWeather = WorldState.weatherStates.rainy;
        }
        else
        {
            rainText.text = "Rain: Off";
            WorldState.instance.currentWeather = WorldState.weatherStates.sunny;
        }
    }

    /* Description: updates the text of the current time of day status to remain up-to-date.
     * Parameter(s): none
     * Returns: nothing
     */
    private void UpdateTimeText()
    {
        /* 0 - Morning
         * 1 - Noon
         * 2 - Evening
         * 3 - Night
         */
        switch (timeStep)
        {
            case 0:
                timeOfDay = WorldState.DayTimes.MORNING;
                timeText.text = "Time: Morning";
                break;
            case 1:
                timeOfDay = WorldState.DayTimes.NOON;
                timeText.text = "Time: Noon";
                break;
            case 2:
                timeOfDay = WorldState.DayTimes.EVENING;
                timeText.text = "Time: Evening";
                break;
            case 3:
                timeOfDay = WorldState.DayTimes.NIGHT;
                timeText.text = "Time: Night";
                break;
            default:
                timeOfDay = WorldState.DayTimes.NOON;
                timeText.text = "Time: Noon";
                Debug.LogWarning("[IMSE] Time specified is not available\n");
                break;
        }
    }
}
