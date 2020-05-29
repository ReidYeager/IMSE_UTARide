/* Author: Jonah Bui
 * Contributors:
 * Date: March 29, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: keeps the player persistent throughout multiple scenes and controls the location of the
 * player.
 * 
 * Changelog:
 * 
 * 
 * ------------------------------------------------------------------------------------------------
 * To-Do:
 * - The player camera glitches when transitioning between scenes. Consider disabling the player
 *   until the player is fully loaded in. Or find some other method.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    private GameObject player;

    // Used to get player on scene load
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /* Description: Finds the player in the Indoor scene on load up. Also positions the
     * player in the correct place when loaded into the City scene. Needed because player must
     * persist between scenes or else OVR will throw errors causing the game to glitch.
     * Parameter(s): nothing
     * Returns: nothing
     */
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Get the player
        if (scene.name == "Indoor")
        {
            Debug.Log("[IMSE] Indoor scene loaded.");
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = new Vector3(0f, 1f, -8f);
            // Need player to persist since multiple OVR GameObjects loaded asynchronously may
            // cause errors.
            if (player != null)
            {
                Debug.Log("[IMSE] Player found.");
                DontDestroyOnLoad(player);
            }
            else
            {
                Debug.LogWarning("[IMSE] Player could not be found.");
            }
        }
        // Position player
        if (scene.name == "City")
        {
            player.transform.position = new Vector3(-4f, 1f, 0f);
        }

        // Set new active scene so that UIs know which canvas do display.
        // Must set active here since the scene must be fully loaded to set as active.
        SceneManager.SetActiveScene(scene);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
