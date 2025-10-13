using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var hit in hits)
            if (hit.attachedRigidbody != null)
                hit.attachedRigidbody.AddExplosionForce(_explosionForce, hit.transform.position, _explosionRadius);
    }
}