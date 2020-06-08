/* Author: Jonah Bui
 * Contributors:
 * Date: May 28, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: changes the lighting of the streetlights in the scene according to the time
 * set in the WorldState instance.
 * 
 * Changelog:
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class StreetLightManager : MonoBehaviour
{
    public GameObject pointLightGO;
    public GameObject spotLightGO;

    private void Awake()
    {
        try
        {
            Light pointLight = pointLightGO.GetComponent<Light>();
            Light spotLight = spotLightGO.GetComponent<Light>();
            pointLight.enabled = false;
            spotLight.enabled = false;
        }
        catch
        {
            Debug.Log("[IMSE] Light objects could not be found.");
        }

    }

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
        ToggleLights();
    }

    /* Description: Enables/ disables street lights depending on the time provided by WorldState.instance
     * Parameter(s): none
     * Returns: nothing
     */
    private void ToggleLights()
    {
        try
        {
            Light pointLight = pointLightGO.GetComponent<Light>();
            Light spotLight = spotLightGO.GetComponent<Light>();
            if (WorldState.instance.CurrentTime == WorldState.DayTimes.EVENING || WorldState.instance.CurrentTime == WorldState.DayTimes.NIGHT)
            {
                pointLight.enabled = true;
                spotLight.enabled = true;
            }
            else
            {
                pointLight.enabled = false;
                spotLight.enabled = false;
            }
        }
        catch
        {
            Debug.Log("[IMSE] Light objects could not be found.");
        }
    }
}
