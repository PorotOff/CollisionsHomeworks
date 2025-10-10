using System;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private ExplodableDetector _explodeableDetector;
    public event Action<ExplodeableCube> OnExploded;

    private void OnEnable()
        => _explodeableDetector.OnExplodeableDetected += Explode;

    private void OnDisable()
        => _explodeableDetector.OnExplodeableDetected -= Explode;

    private void Explode(ExplodeableCube explodeable)
    {
        explodeable.Explode();
        OnExploded?.Invoke(explodeable);
    }
}