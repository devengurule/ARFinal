using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> targetTransforms;
    [SerializeField] private float transitDuration;
    [SerializeField] private float postTransitWaitDuration;

    private void Start()
    {
        Debug.Log(0);
        StartCoroutine(CycleTransformPositions());
    }

    private IEnumerator CycleTransformPositions()
    {
        int index = 0;
        while (true)
        {
            if (index == targetTransforms.Count) index = 0;

            yield return StartCoroutine(MoveToPoint(targetTransforms[index].position));

            yield return new WaitForSeconds(postTransitWaitDuration);

            index = (index + 1) % targetTransforms.Count;
        }
    }

    private IEnumerator MoveToPoint(Vector3 position)
    {
        Vector3 startPosition = transform.position;

        float elapsedTime = 0;

        while (elapsedTime <= transitDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitDuration);

            Vector3 newPosition = Vector3.Lerp(startPosition, position, t);

            transform.position = newPosition;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
