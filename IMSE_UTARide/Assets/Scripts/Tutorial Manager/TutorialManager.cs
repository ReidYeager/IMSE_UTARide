using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // List of text and images to display to user. Make sure the lists elements correlate with one
    // another. Ex: at index 1 the image and text are meant to be displayed together. It is alright
    // to leave the image list blank but not the string list. The image list will be filled with 
    // null images to match the amount of texts elements.
    public List<string> dialogue = new List<string>();
    public List<Sprite> images = new List<Sprite>();

    // The text UI element used to display text to the user.
    public Text displayText;
    // The image UI element used to display an image to the user.
    public Image displayImage;
    // Use to keep track of what text/image to display.
    private int currentSlideIndex;

    // To add later
    // public bool loadDialogueFromFile;


    /* Description: Use to set the text and image of a canvas.
     * Parameter(s): 
     *  text : string
     *      A message to display to the user.
     *  image : Sprite
     *      An image to display to the user
     * Return(s): nothing
     */
    private void SetSlide(string text, Sprite image)
    {
        displayText.text = text;
        displayImage.sprite = image;
    }

    private void Awake()
    {
        currentSlideIndex = 0;    
    }

    private void Start()
    {
        // Display the first elements when the scene is loaded up
        if (dialogue.Count > 0 && images.Count > 0)
            SetSlide(dialogue[0], images[0]);
        else
            Debug.LogWarning("[IMSE] Dialogue and images are missing from tutorial display.");
        if (dialogue.Count > images.Count)
        {
            Debug.LogWarning("[IMSE] Dialgoue does not have the correct amount of images to display with.");
            int toAdd = dialogue.Count - images.Count;
            // If there is more text than dialogue, fill in the list of images to match the count
            // of the text list.
            for (int i = 0; i < toAdd; i++)
            {
                images.Add(null);
            }
            SetSlide(dialogue[0], images[0]);
        }
        currentSlideIndex++;
    }

    void Update()
    {
        // Transition to the next text of the tutorial if the player clicks A on the OVR controller
        if ((OVRInput.GetDown(UI_InputMapping.OVR_A) || Input.GetKeyDown(KeyCode.Escape)) && currentSlideIndex < dialogue.Count)
        {
            Debug.Log($"[IMSE] Tutorial index is {currentSlideIndex}");
            SetSlide(dialogue[currentSlideIndex], images[currentSlideIndex]);
            currentSlideIndex++;
        }
    }
}
