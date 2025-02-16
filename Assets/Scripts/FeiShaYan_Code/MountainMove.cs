using UnityEngine;
using UnityEngine.UI;

public class MountainMove : MonoBehaviour
{
    public Slider slider;  // Reference to the Slider component
    public RectTransform imageRectTransform;  // Reference to the Image's RectTransform
    public Vector2 minPosition;  // Minimum position of the image
    public Vector2 maxPosition;  // Maximum position of the image

    void Start()
    {
        // Ensure the necessary components are assigned
        if (slider == null)
        {
            slider = GetComponentInParent<Slider>();
        }
        if (imageRectTransform == null)
        {
            imageRectTransform = GetComponent<RectTransform>();
        }

        // Optionally, you can set default positions based on the Canvas size
        // Here we assume the canvas is set up in a way that min/max positions are valid
    }

    void Update()
    {
        // Calculate the interpolated position based on slider value
        float t = slider.value;
        Vector2 newPosition = Vector2.Lerp(minPosition, maxPosition, t);

        // Set the new position to the Image's RectTransform
        imageRectTransform.anchoredPosition = newPosition;
    }
}