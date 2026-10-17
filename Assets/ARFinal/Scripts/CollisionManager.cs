using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static event Action<Collision> CollidingWithGround;
    public static event Action HitGround;
    public static event Action StoppedCollidingWithGround;
    public static event Action CollidedWIthEnd;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CollidingWithGround?.Invoke(collision);
            UpdateParent(collision);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        CollidedWIthEnd?.Invoke();
    }

    private void UpdateParent(Collision collision)
    {
        if(transform.parent != collision.gameObject.transform.parent) transform.parent = collision.gameObject.transform.parent;
    }
}
