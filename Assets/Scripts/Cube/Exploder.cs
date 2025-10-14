using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explode(Rigidbody rigidbody)
        => rigidbody.AddExplosionForce(_explosionForce, rigidbody.transform.position, _explosionRadius);
}