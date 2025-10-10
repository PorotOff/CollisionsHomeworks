using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class ExplodeableCube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void TakeExplosion(Vector3 explosionEpicenter)
        => _rigidbody.AddExplosionForce(_explosionForce, explosionEpicenter, _explosionRadius);

    public void ChangeColor(Color color)
        => _meshRenderer.material.color = color;

    public void Explode()
        => Destroy(gameObject);
}