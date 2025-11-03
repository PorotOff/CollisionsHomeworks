using System.Collections.Generic;
using UnityEngine;

public class ExplodableDetectionHandler : MonoBehaviour
{
    [SerializeField] private ExplodableDetector _explodableDetector;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Spawner _spawner;
    [Header("Explodable settings")]
    [SerializeField] private int _spawnableScaleDivider = 2;
    [SerializeField] private int _spawnableSplitChanceDivider = 2;
    [SerializeField] private int _spawnableExplosionRadiusMultiplier = 2;
    [SerializeField] private int _spawnableExplosionForceMultiplier = 2;

    private void OnEnable()
        => _explodableDetector.ExplodableDetected += OnExplodableDetected;

    private void OnDisable()
        => _explodableDetector.ExplodableDetected -= OnExplodableDetected;

    private void OnExplodableDetected(Explodable explodable)
    {
        if (explodable.CanSplit())
        {
            Vector3 position = explodable.transform.position;
            Quaternion rotation = explodable.transform.rotation;
            Vector3 scale = explodable.transform.localScale / _spawnableScaleDivider;
            float splitChance = explodable.SplitChance / _spawnableSplitChanceDivider;
            float explosionRadius = explodable.ExplosionRadius * _spawnableExplosionRadiusMultiplier;
            float explosionForce = explodable.ExplosionForce * _spawnableExplosionForceMultiplier;

            List<Explodable> spawnedExplodables = _spawner.Spawn(explodable, position, rotation, scale, splitChance, explosionRadius, explosionForce);

            foreach (var spawnedExplodable in spawnedExplodables)
                if (spawnedExplodable.TryGetComponent(out Rigidbody rigidbody))
                    _exploder.Explode(rigidbody, explosionRadius, explosionForce);
        }
        else
        {
            Vector3 explosionPosition = explodable.transform.position;
            float explosionRadius = explodable.ExplosionRadius;
            float explosionForce = explodable.ExplosionForce;

            _exploder.Explode(explosionPosition, explosionRadius, explosionForce);
        }

        Destroy(explodable.gameObject);
    }
}