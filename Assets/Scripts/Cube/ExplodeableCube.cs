using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplodableCube : MonoBehaviour
{
    private float _maxSplitChance = 100f;
    
    public float SplitChance { get; private set; }

    public void Initialize(float splitChance)
        => SplitChance = splitChance;

    private void Awake()
        => SplitChance = _maxSplitChance;

    public bool CanSplit()
        => Random.Range(0, _maxSplitChance) <= SplitChance;
}