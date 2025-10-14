using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private Utils _utils;

    public void Explode(Rigidbody rigidbody)
    {
        rigidbody.AddExplosionForce(_explosionForce, rigidbody.transform.position, _explosionRadius);
        _utils.DrawDebugSphere(rigidbody.transform.position, _explosionRadius);
    }

    public void Explode(Vector3 position, float radius, float force)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);

        foreach (var hit in hits)
            if (hit.attachedRigidbody != null)
                hit.attachedRigidbody.AddExplosionForce(force, position, radius);

        _utils.DrawDebugSphere(position, radius);
    }
}