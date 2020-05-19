/* Author: Jonah Bui
 * Contributors:
 * Date: May 18th, 2020
 * ------------------------
 * Purpose: Sets the environmental variables that can be used to customize the player's scene.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DayTimes
{ 
    MORNING,
    NOON,
    EVENING,
    NIGHT
};


public class UI_EnvManager : MonoBehaviour
{
    public Text timeText;
    public Text rainText;

    private static bool rainToggle = false;
    private static DayTimes timeOfDay = DayTimes.NOON;
    private int timeStep = 0;

    public void Start()
    {
        timeStep = 0;
    }

    /* Description: To be used by a button. Sets the time variable and updates the according UI
     * text in the Environment Manager app.
     * Parameters: nothing
     * Return(s): nothing
     */
    public void SetTime()
    {
        timeStep = (timeStep + 1) % 4;
        /* 0 - Morning
         * 1 - Noon
         * 2 - Evening
         * 3 - Night
         */
        switch (timeStep)
        {
            case 0:
                timeOfDay = DayTimes.MORNING;
                timeText.text = "Time: Morning";
                break;
            case 1:
                timeOfDay = DayTimes.NOON;
                timeText.text = "Time: Noon";
                break;
            case 2:
                timeOfDay = DayTimes.EVENING;
                timeText.text = "Time: Evening";
                break;
            case 3:
                timeOfDay = DayTimes.NIGHT;
                timeText.text = "Time: Night";
                break;
            default:
                timeOfDay = DayTimes.NOON;
                timeText.text = "Time: Noon";
                Debug.LogError("WARNING: Time specified is not available\n");
                break;
        }
    }

    public void SetWeather()
    {
        rainToggle = !rainToggle;
        if (rainToggle)
            rainText.text = "Rain: On";
        else
            rainText.text = "Rain: Off";
    }
}
