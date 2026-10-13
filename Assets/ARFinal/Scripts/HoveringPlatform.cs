using System;
using System.Collections.Generic;
using UnityEngine;

public class HoveringPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> targetTransforms;
    [SerializeField] private float transitSpeed;

    private void Update()
    {

    }

    private void MoveToPoint(Vector3 position)
    {
        float distanceToPoint = Vector3.Distance(transform.position, position);
    }
}
