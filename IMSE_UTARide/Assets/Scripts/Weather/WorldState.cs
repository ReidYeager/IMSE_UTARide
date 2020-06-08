/* Author: Yeager
 * Contributors: Jonah Bui
 * Date: March 24, 2020
 */
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WorldState : MonoBehaviour
{
    public static WorldState instance { get; private set; }

    // SCENES
    // ==========================================
    public enum sceneNames {
        Indoor = 1,
        City = 2
    }

    // WEATHER
    // ==========================================
    public enum weatherStates {
        sunny = 1,
        rainy = 2
    }

    public weatherStates currentWeather { get; set; } = weatherStates.sunny;
    public WorldPalette[] worldWeatherPalettes;

    // TIME OF DAY
    // ==========================================
    public enum DayTimes
    {
        MORNING,
        NOON,
        EVENING,
        NIGHT
    };
    [Header("Time Customization")]
    [SerializeField] private DayTimes currentTime = DayTimes.NOON;
    public DayTimes CurrentTime
    {
        get { return currentTime;  }
        set { Debug.Log($"[IMSE] Current time: {currentTime}"); currentTime = value; }
    }

//===============================================\\
//===============================================//
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        SetWeather(currentWeather);
    }

    private void Start()
    {
        LoadScene(sceneNames.Indoor);
    }


    // SCENE MANAGEMENT
    // ==============================================

    // Load scene additively
    public void LoadScene(sceneNames _scene, UnityAction<Scene, LoadSceneMode> _sceneLoadedCallback = null)
    {
        if (_sceneLoadedCallback != null)
            SceneManager.sceneLoaded += _sceneLoadedCallback;

        SceneManager.LoadSceneAsync((int)_scene, LoadSceneMode.Additive);
    }
    public void UnloadScene(sceneNames _scene)
    {
        SceneManager.UnloadSceneAsync((int)_scene);
    }
    // Overrides for button UI to use.
    public void LoadScene(int _scene, UnityAction<Scene, LoadSceneMode> _sceneLoadedCallback = null)
    {
        if (_sceneLoadedCallback != null)
            SceneManager.sceneLoaded += _sceneLoadedCallback;

        SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive);
    }
    public void UnloadScene(int _scene)
    {
        SceneManager.UnloadSceneAsync(_scene);
    }

    // Used to set new active scene on load
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set new active scene so that UIs know which canvas do display.
        // Must set active here since the scene must be fully loaded to set as active.
        SceneManager.SetActiveScene(scene);

        // Set the new weather only if player is in the city
        if (scene.buildIndex == (int)sceneNames.City)
        {
            SetTimeOfDay();
        }
        Debug.Log($"[IMSE] Current active scene is {SceneManager.GetActiveScene().name}");
    }


    // WEATHER
    // ==============================================
    public void SetWeather(weatherStates _weather)
    {
        currentWeather = _weather;
        Debug.Log($"Current weather now {currentWeather}");
    }   

    // TIME
    // ==============================================

    // Sunny palette expected @ index 0
    // Rainy palette expected @ index 1
    // Night palette expected @ index 2
    // More TBA...
    public void SetTimeOfDay()
    {
        // Get new colors from weather palette
        // Set lighting depending on time of day provided by currentTime
        switch (currentTime)
        {
            case DayTimes.MORNING:
                // Add stuff here
                break;
            case DayTimes.NOON:
                // Add stuff here
                break;
            case DayTimes.EVENING:
                // Add stuff here
                break;
            case DayTimes.NIGHT:
                RenderSettings.skybox = worldWeatherPalettes[2].skybox;
                // Need to modify lighting also
                break;
            default:
                break;
        }
    }
}
