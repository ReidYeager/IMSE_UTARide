/* Author: Jonah Bui
 * Contributors:
 * Date: March 29, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: keeps the player persistent throughout multiple scenes and controls the location of the
 * player.
 * 
 * Changelog:
 *  June 5, 2020
 *      - This script should now be held in the WorldManager gameObject found in the scene Persistent.
 *        It will create a player gameobject when the scene loads instead of having to add one to the
 *        scene manually. Also redcues management of the player when the scene loads.
 *  June 1, 2020
 *      - Fixed issue where player does not consistently spawn in the same place due to colliders in
 *        the loaded scene. Fixed by disabling collider on load up.
 * ------------------------------------------------------------------------------------------------
 * To-Do:
 * - NullReference Error when player changes scene. Fix
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    // Assign the player prefab "UI_GameObjects+Player"
    public GameObject playerPrefab;

    // The gameObject holoding the OVRPlayerController. Used to modify player's location
    private GameObject player;
    // The gameObject OVRPlayerController. Used to control movement.
    private GameObject ovrPlayer;

    private OVRPlayerController ovrController;
    private CharacterController charController;

    private UI_ManagerScript UIM;

    // Editor values
    [SerializeField] private int e_appIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // --------------------------------------------------------------------------------------------
    // Player On Load-Scene Management
    // --------------------------------------------------------------------------------------------

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
        if (player != null)
            FreePlayer(player);
        // Set new active scene so that UIs know which canvas do display.
        // Must set active here since the scene must be fully loaded to set as active.
        if (scene.name == "Indoor")
        {
            Debug.Log("[IMSE] Indoor scene loaded.");
            InstantiatePlayer();
            try
            {
                player.transform.position = new Vector3(0f, 1f, -9f);
                ovrPlayer.SetActive(true);
                ovrPlayer.transform.localPosition = Vector3.zero;
            }
            catch
            {
                Debug.LogError("[IMSE] The player/ ovrPlayer could not be referenced.");
            }

        }
        if (scene.name == "City")
        {
            Debug.Log("[IMSE] Indoor scene loaded.");
            InstantiatePlayer();
            try
            {
                player.transform.position = new Vector3(-4f, 4f, 0f);
                ovrPlayer.SetActive(true);
                ovrPlayer.transform.localPosition = Vector3.zero;
            }
            catch
            {
                Debug.LogError("[IMSE] The player/ ovrPlayer could not be referenced.");
            }
        }
    }

    // --------------------------------------------------------------------------------------------
    // Player Management Functions
    // --------------------------------------------------------------------------------------------

    /* Description: Creates a new player gameobject and assigns the necessary variables needed to
     * maange it within this script.
     * Parameter(s): nothing
     * Returns: 
     *      newPlayer : GameObject
     *          A prefab of the player gameObject. Should be "UI_GameObjects+Player" that is 
     *          assigned in the editor.
     */
    private GameObject InstantiatePlayer()
    {
        GameObject newPlayer = Instantiate(playerPrefab);

        // Find all the necessary components and assign them to private variables to be called on later.
        player = newPlayer.transform.GetChild(0).gameObject;
        if (player == null)
        {
            Debug.LogError("[IMSE] The player object in UI_GameObjects+Player could not be found.");
            return null;
        }

        ovrPlayer = player.transform.GetChild(0).gameObject;
        if (ovrPlayer == null)
        {
            Debug.LogError("[IMSE] The ovrPlayer object in UI_GameObjects+Player could not be found.");
            return null;
        }
                      
        ovrController = ovrPlayer.GetComponent<OVRPlayerController>();
        if (ovrController == null)
        {
            Debug.LogError("[IMSE] The ovrController object in UI_GameObjects+Player could not be found.");
            return null;
        }

        charController = ovrPlayer.GetComponent<CharacterController>();
        if (charController == null)
        {
            Debug.LogError("[IMSE] The charController object in UI_GameObjects+Player could not be found.");
            return null;
        }
        UIM = newPlayer.transform.GetChild(1).GetChild(1).GetComponent<UI_ManagerScript>();
        if (UIM == null)
        {
            Debug.LogError("[IMSE] The UIM object in UI_GameObjects+Player could not be found.");
            return null;
        }
    
        // Set the buttons for scene loading in the tablet
        Button orderRide = newPlayer.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>();
        if (orderRide != null)
        {
            orderRide.onClick.AddListener(() => LoadSceneViaButton(2));
            orderRide.onClick.AddListener(() => UnloadSceneViaButton(1));
        }
        else
        {
            Debug.LogError("[IMSE] Order Ride button could not be found. Cannot assign functions.");
            return null;
        }
        Button returnHome = newPlayer.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(3).GetComponent<Button>();
        if (returnHome != null)
        {
            returnHome.onClick.AddListener(() => LoadSceneViaButton(1));
        }
        else
        {
            Debug.LogError("[IMSE] Return Home button could not be found. Cannot assign functions..");
            return null;
        }
        return newPlayer;
    }

    private void FreePlayer(GameObject player)
    {
        Destroy(player);
        ovrPlayer = null;
        ovrController = null;
        charController = null;
        UIM = null;
    }

    public void LoadSceneViaButton(int index)
    {
        player.SetActive(false);
        FreePlayer(player);
        // Consider adding a message saying a scene is loading
        if (index == 1)
        { 
            
        }

        WorldState.instance.LoadScene(index);
        WorldState.instance.SetTimeOfDay();
    }

    public void UnloadSceneViaButton(int index)
    {
        WorldState.instance.UnloadScene(index);
    }

    // --------------------------------------------------------------------------------------------
    // Getters/ setters
    // --------------------------------------------------------------------------------------------
    public GameObject Player
    {
        get { return player; }
    }
    public GameObject OVRPlayer
    {
        get { return ovrPlayer; }
    }
    public OVRPlayerController OVRController
    {
        get { return ovrController; }
    }
    public CharacterController CharController
    {
        get { return charController; }
    }
    public UI_ManagerScript UIMS
    {
        get { return UIM; }
    }
}
