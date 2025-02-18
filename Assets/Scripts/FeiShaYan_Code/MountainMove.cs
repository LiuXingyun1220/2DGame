using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MountainMove : MonoBehaviour
{
    public Slider slider;  // Reference to the Slider component
    public RectTransform imageRectTransform;  // Reference to the Image's RectTransform
    public Vector2 minPosition;  // Minimum position of the image
    public Vector2 maxPosition;  // Maximum position of the image
    private float t;

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
    }

    void Update()
    {
        Scene scene = SceneManager.GetSceneByName(FeiShaYanManager.SectionalViewScene);
        if (scene.IsValid())
        {
            t = slider.value / 5;
        }
        else
        {
            t = FeiShaYanManager.GetHeightData() / 5;
        }
        // Calculate the interpolated position based on slider value
        Vector2 newPosition = Vector2.Lerp(minPosition, maxPosition, t);

        // Set the new position to the Image's RectTransform
        imageRectTransform.anchoredPosition = newPosition;
    }
}