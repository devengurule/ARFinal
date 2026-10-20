using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private GameObject XROrigin;
    public static event Action<Collision> CollidingWithGround;
    public static event Action HitGround;
    public static event Action StoppedCollidingWithGround;
    public static event Action CollidedWIthEnd;

    private void Awake()
    {
        XROrigin = GameObject.FindGameObjectWithTag("XROrigin");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CollidingWithGround?.Invoke(collision);

            foreach (ContactPoint contact in collision.contacts)
            {
                Vector3 surfaceNormal = collision.contacts[0].normal;

                float finalAngle = FindAngleBetweenVectors(surfaceNormal, Vector3.up);

                if (finalAngle >= 0 && finalAngle <= 1f)
                {
                    AttachToPlatform(collision);
                    return;
                }
            }

            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            HitGround?.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StoppedCollidingWithGround?.Invoke();
        AttachToXROrigin();
    }

    private void OnTriggerEnter(Collider other)
    {
        CollidedWIthEnd?.Invoke();
    }

    private void AttachToPlatform(Collision collision)
    {
        if (transform.parent != collision.gameObject.transform.parent) transform.parent = collision.gameObject.transform.parent;
    }

    private void AttachToXROrigin()
    {
        if (XROrigin == null) return;
        if (transform.parent != XROrigin.transform) transform.parent = XROrigin.transform;
    }

    private float FindAngleBetweenVectors(Vector3 a, Vector3 b)
    {
        float dotProduct = Vector3.Dot(a, b);
        float magnitude = a.magnitude * b.magnitude;
        float x = Mathf.Clamp(dotProduct / magnitude, 0f, 1f);

        float finalAngle = Mathf.Abs(Mathf.Acos(x) * Mathf.Rad2Deg);

        return finalAngle;
    }
}
