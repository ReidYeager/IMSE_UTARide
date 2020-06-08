/* Author: Jonah Bui
 * Contributors:
 * Date: June 7, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: Used to create a slide on an empty gameObject. To be used with the TutorialManager.
 * ------------------------------------------------------------------------------------------------
 * Changelog:
 * 
 */
using UnityEngine;
using UnityEngine.UI;
public class Slide : MonoBehaviour
{
    [Header("Slide Settings")]
    public bool enableImage = true;
    public Vector3 canvasScale;
    public SlideColorScheme scheme;
    
    [Header("UI Components")]
    public Canvas canvas;
    public Text text;
    public Image image;
    public Image panel;

    // Constants that determine the size of the canvas relative to player
    private const float CANVAS_SCALE = 0.005f;
    private const float CANVAS_WIDTH = 1920f;
    private const float CANVAS_HEIGHT = 1080f;

    private void Reset()
    {
        // Add a canvas for the slide to display text and images. Also set default settings for VR
        canvas = this.gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        GameObject mainCameraGO = GameObject.FindGameObjectWithTag("MainCamera");
        Camera mainCamera = null;
        if(mainCameraGO != null)
            mainCamera = mainCameraGO.GetComponent<Camera>();
        if (mainCamera != null)
            canvas.worldCamera = mainCamera;
        else
        {
            mainCamera = GameObject.FindObjectOfType<Camera>();
            canvas.worldCamera = mainCamera;
            Debug.LogWarning($"[IMSE] Could not find main camera in scene. Assigning {mainCamera.name} as default camera.");
        }
        //// Resize canvas to more appropriate player size
        RectTransform rt = this.GetComponent<RectTransform>();
        canvasScale = new Vector3(0.5f, 0.5f, 0.5f);
        Scale(canvasScale);
        rt.sizeDelta = new Vector2(CANVAS_WIDTH, CANVAS_HEIGHT);

        // Add panel for visual aid
        GameObject panelGO = new GameObject();
        panelGO.name = "BackgroundFill";
        panelGO.transform.SetParent(this.transform);
        panel = panelGO.AddComponent<Image>();
        panel.sprite = Resources.Load<Sprite>(ResourcePaths.PANEL_LIGHT);
        panel.color = new Color(1f, 1f, 1f, 1f);
        RectTransform rt_panelGO = panelGO.GetComponent<RectTransform>();
        rt_panelGO.localScale = Vector3.one;
        rt_panelGO.localPosition = Vector3.zero;
        rt_panelGO.anchorMin = Vector2.zero;
        rt_panelGO.anchorMax = Vector2.one;
        rt_panelGO.offsetMin = Vector2.zero;
        rt_panelGO.offsetMax = Vector2.zero;

        // Create a default text componenet
        GameObject textGO = new GameObject();
        textGO.name = "Text";
        textGO.transform.SetParent(this.transform);
        text = textGO.AddComponent<Text>();
        text.color = Color.black;
        text.fontSize = 64;
        RectTransform rt_textGO = textGO.GetComponent<RectTransform>();
        rt_textGO.localScale = Vector3.one;
        rt_textGO.localPosition = Vector3.zero;
        rt_textGO.anchorMin = new Vector2(0f, 0f);
        rt_textGO.anchorMax = new Vector2(0.6f, 1f);
        rt_textGO.pivot = new Vector2(0f, 0.5f);
        rt_textGO.offsetMin = new Vector2(5f, 5f);
        rt_textGO.offsetMax = new Vector2(-5f, -5f);

        // Create a default image component
        GameObject imageGO = new GameObject();
        imageGO.name = "Image";
        imageGO.transform.SetParent(this.transform);
        image = imageGO.AddComponent<Image>();
        RectTransform rt_imageGO = imageGO.GetComponent<RectTransform>();
        rt_imageGO.localScale = new Vector3(1f, 1f ,1f );
        rt_imageGO.localPosition = Vector3.zero;
        rt_imageGO.anchorMin = new Vector2(0.6f, 0f);
        rt_imageGO.anchorMax = Vector2.one;
        rt_imageGO.offsetMin = Vector2.zero;
        rt_imageGO.offsetMax = Vector2.zero;
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        canvas.enabled = true;
    }

    private void OnDisable()
    {
        canvas.enabled = false;
    }

    public void Scale(Vector3 scale)
    {
        RectTransform rt = this.GetComponent<RectTransform>();
        rt.localScale = new Vector3(
            CANVAS_SCALE*scale.x, 
            CANVAS_SCALE*scale.y, 
            CANVAS_SCALE*scale.z
        );
    }

    public void Scheme(SlideColorScheme scheme)
    {
        panel.color = scheme.mainColor;
        text.color = scheme.textColor;

        text.fontSize = scheme.fontSize;
        text.fontStyle = scheme.fontStyle;
    }
}
