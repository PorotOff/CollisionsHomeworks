using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplodeableCube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private float _maxSplitChance = 100f;
    
    public float SplitChance { get; private set; }

    public void Initialize(float splitChance)
    {
        _rigidbody = GetComponent<Rigidbody>();
        SplitChance = splitChance;
    }

    private void Awake()
        => SplitChance = _maxSplitChance;

    public bool CanSplit()
        => Random.Range(0, _maxSplitChance) <= SplitChance;

    public void TakeExplosion(Vector3 explosionPoint)
        => _rigidbody.AddExplosionForce(_explosionForce, explosionPoint, _explosionRadius);
}