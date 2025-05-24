using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RippleShaderButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Material rippleMaterial;
    [SerializeField] private float rippleDuration = 0.5f;

    private Image buttonImage;
    private Material originalMaterial;
    private float rippleTimer = -1f;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalMaterial = buttonImage.material;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 获取点击位置的UV坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPos);

        Vector2 uv = new Vector2(
            (localPos.x / GetComponent<RectTransform>().rect.width) + 0.5f,
            (localPos.y / GetComponent<RectTransform>().rect.height) + 0.5f
        );

        // 应用材质参数
        rippleMaterial.SetVector("_RippleCenter", uv);
        buttonImage.material = rippleMaterial;
        rippleTimer = rippleDuration;
    }

    void Update()
    {
        if (rippleTimer > 0)
        {
            rippleTimer -= Time.deltaTime;
            rippleMaterial.SetFloat("_RippleTime", 1 - (rippleTimer / rippleDuration));
            if (rippleTimer <= 0)
            {
                buttonImage.material = originalMaterial;
            }
        }
    }
}