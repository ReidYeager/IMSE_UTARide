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
    public weatherStates currentWeather { get; private set; } = weatherStates.sunny;
    public WorldPalette[] worldWeatherPalettes;

    // TIME OF DAY
    // ==========================================

    // Number of seconds starting at midnight(0.0f) to 11:59:59 (86399)
    public float timeOfDay { get; private set; } = 0.0f;


//===============================================\\
//===============================================//
    private void Awake()
    {
        if (instance == null)
            instance = this;

        SetWeather(weatherStates.sunny);
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


// WEATHER
// ==============================================

    public void SetWeather(weatherStates _weather)
    {
        currentWeather = _weather;
        Debug.Log($"Current weather now {currentWeather}");
    }   

// TIME
// ==============================================

    public void SetTimeOfDay(float _time)
    {
        timeOfDay = _time;
        // Get new colors from weather palette
        // Set skybox from weather palette
    }




}

