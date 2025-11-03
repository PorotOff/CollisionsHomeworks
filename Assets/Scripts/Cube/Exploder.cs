using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(Rigidbody rigidbody, float radius, float force)
        => rigidbody.AddExplosionForce(force, rigidbody.transform.position, radius);

    public void Explode(Vector3 position, float radius, float force)
    {
        List<Collider> hits = Physics.OverlapSphere(position, radius).ToList();

        foreach (var hit in hits)
        {
            if (hit.attachedRigidbody)
                hit.attachedRigidbody.AddExplosionForce(force, position, radius);
        }
    }
}