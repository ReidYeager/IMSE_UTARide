/* Author: Jonah Bui
 * Contributors:
 * Date: June 6, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: 
 * ------------------------------------------------------------------------------------------------
 * Changelog:
 * 
 * June 7, 2020
 * ------------------------------------------------------------------------------------------------
 * - Updated TutorialManager to work with new Slide gameObject.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public enum Transition
    { 
        FORWARD,
        BACKWARD
    };

    public Vector3 worldPosition;
    public List<GameObject> slideGOs = new List<GameObject>();
    public SlideColorScheme scheme;

    public string textToParse;

    [Tooltip("The gameObject to place all the slides to create")]
    public GameObject outputSlides;
   
    // Use to keep track of what text/image to display.
    private int currentSlideIndex;

    // Editor vars
    public int slideCount;
    public bool toggleSlides;
    public string textDelimiter;

    /* Description: Use to set the text and image of a canvas.
     * Parameter(s): 
     *  text : string
     *      A message to display to the user.
     *  image : Sprite
     *      An image to display to the user
     * Return(s): nothing
     */
    private void ChangeSlide(GameObject slideGO, Transition t)
    {
        if (t == Transition.FORWARD)
        {
            if (currentSlideIndex + 1 < slideGOs.Count)
            {
                currentSlideIndex++;
                EnableSlide(slideGOs, currentSlideIndex);
            }
        }
        else if (t == Transition.BACKWARD)
        {
            if (currentSlideIndex - 1 >= 0)
            {
                currentSlideIndex--;
                EnableSlide(slideGOs, currentSlideIndex);
            }
        }
    }

    private void Awake()
    {
        currentSlideIndex = 0;    
    }

    private void Start()
    {
        EnableSlide(slideGOs, 0);
    }

    void Update()
    {
        // Transition to the next text of the tutorial if the player clicks A on the OVR controller
        if ((OVRInput.GetDown(UI_InputMapping.OVR_A) || Input.GetKeyDown(KeyCode.Escape)))
        {
            try
            {
                ChangeSlide(slideGOs[currentSlideIndex], Transition.FORWARD);
                Debug.Log($"[IMSE] Tutorial index is now {currentSlideIndex}");
            }
            catch
            {
                Debug.LogWarning($"[IMSE] Slide could not be retrieved.");
            }
        }
    }

    private void EnableSlide(List<GameObject> slideGOs, int index)
    {
        for (int i = 0; i < slideGOs.Count; i++)
        {
            try
            {
                if(index == i)
                    slideGOs[i].GetComponent<Slide>().enabled = true;
                else
                    slideGOs[i].GetComponent<Slide>().enabled = false;
            }
            catch
            {
                Debug.LogWarning($"[IMSE] Slide could not be retrieved.");
            }

        }
    }
}
