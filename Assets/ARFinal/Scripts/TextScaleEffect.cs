using UnityEngine;

public class TextScaleEffect : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float scaleAmount;
    [SerializeField] private float scaleSpeed;
    private RectTransform rectTransform;
    private float minScale;
    private float maxScale;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        float scaleDelta = rectTransform.localScale.x * scaleAmount;

        minScale = rectTransform.localScale.x - scaleDelta;
        maxScale = rectTransform.localScale.x + scaleDelta;
    }

    private void Update()
    {
        UpdateScale();
    }

    private void UpdateScale()
    {
        float t = Mathf.PingPong(Time.time * scaleSpeed, 1);
        float scale = Mathf.Lerp(minScale, maxScale, t);

        rectTransform.localScale = Vector3.one * scale;
    }
}
