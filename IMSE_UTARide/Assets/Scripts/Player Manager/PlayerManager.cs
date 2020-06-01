/* Author: Jonah Bui
 * Contributors:
 * Date: March 29, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: keeps the player persistent throughout multiple scenes and controls the location of the
 * player.
 * 
 * Changelog:
 *  June 1, 2020
 *      Fixed issue where player does not consistently spawn in the same place due to colliders in
 *      the loaded scene. Fixed by disabling collider on load up.
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
    private GameObject ovrPlayer;

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
        // Disable player gravity to prevent free fall and disable collider so that player does
        // not collide with any scene objects on load up causing position to be displaced.
        OVRPlayerController controller = ovrPlayer.GetComponent<OVRPlayerController>();
        CharacterController charController = ovrPlayer.GetComponent<CharacterController>();
        try
        {
            controller.GravityModifier = 0f;
            controller.Acceleration = 0.1f;
            controller.Damping = 0.3f;
            charController.enabled = false;
            // Set new active scene so that UIs know which canvas do display.
            // Must set active here since the scene must be fully loaded to set as active.
            if (scene.name == "Indoor")
            {
                SceneManager.SetActiveScene(scene);
                Debug.Log("[IMSE] Indoor scene loaded.");
                this.transform.position = new Vector3(0f, 2f, -9f);
            }
            if (scene.name == "City")
            {
                SceneManager.SetActiveScene(scene);
                Debug.Log("[IMSE] Indoor scene loaded.");
                this.transform.position = new Vector3(-4f, 4f, 0f);       
            }
            ovrPlayer.transform.localPosition = Vector3.zero;
            charController.enabled = true;
            controller.GravityModifier = 1f;
        }
        catch
        {
            Debug.LogWarning("[IMSE] Could not find OVRPlayerController or CharacterController.");
        }
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
    private void Start()
    {
        player = this.gameObject.transform.GetChild(0).gameObject;
        if (player == null)
        {
            Debug.LogError("[IMSE] The player object in UI_GameObjects+Player could not be found.");
        }
        ovrPlayer = player.transform.GetChild(0).gameObject;
        if (player == null)
        {
            Debug.LogError("[IMSE] The ovrPlayer object in UI_GameObjects+Player could not be found.");
        }
    }
}
