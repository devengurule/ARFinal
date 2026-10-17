using UnityEngine;

public class PointToCamera : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward, Camera.main.transform.up);
    }
}