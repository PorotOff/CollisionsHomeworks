using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplodeableCube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private int _splitChanceDivider = 2;

    private float _maxSplitChance = 100f;
    private float _splitChance;

    public event Action<ExplodeableCube> Exploded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _splitChance = _maxSplitChance;
    }

    private void Start()
        => _rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);

    public bool CanSplit()
        => UnityEngine.Random.Range(0, _maxSplitChance) <= _splitChance;

    public void DivideSplitChance()
        => _splitChance /= _splitChanceDivider;

    public void Explode()
        => Exploded?.Invoke(this);
}