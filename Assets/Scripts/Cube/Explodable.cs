using UnityEngine;

public class Explodable : MonoBehaviour
{
    private float _maxSplitChance = 100f;

    [field: SerializeField] public float ExplosionRadius { get; private set; } = 10f;
    [field: SerializeField] public float ExplosionForce { get; private set; } = 10f;
    
    public float SplitChance { get; private set; } = 100f;

    public void Initialize(float splitChance, float explosionRadius, float explosionForce)
    {
        SplitChance = splitChance;
        ExplosionRadius = explosionRadius;
        ExplosionForce = explosionForce;
    }

    public bool CanSplit()
        => Random.Range(0, _maxSplitChance) <= SplitChance;
}