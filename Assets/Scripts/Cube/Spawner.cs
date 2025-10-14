using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ExplodableDetector _explodableDetector;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Transform _spawnablesContainer;
    [SerializeField, Range(1f, 10f)] private int _minSpawnCount = 2;
    [SerializeField, Range(1f, 10f)] private int _maxSpawnCount = 6;
    [SerializeField] private float _spawnableScaleDivider = 2f;
    [SerializeField] private float _splitChanceDivider = 2f;
    [SerializeField] private float _explosionRadiusMultiplier = 1.4f;
    [SerializeField] private float _explosionForceMultiplier = 1.4f;

    private void OnValidate()
    {
        if (_minSpawnCount > _maxSpawnCount)
            _maxSpawnCount = _minSpawnCount;
    }

    private void OnEnable()
        => _explodableDetector.ExplodableDetected += OnExplodableDetected;

    private void OnDisable()
        => _explodableDetector.ExplodableDetected -= OnExplodableDetected;

    private void OnExplodableDetected(ExplodableCube explodable)
    {
        if (explodable.CanSplit())
        {
            List<ExplodableCube> spawnedExplodables = Spawn(explodable);

            foreach (var spawnedExplodable in spawnedExplodables)
                if (spawnedExplodable.TryGetComponent(out Rigidbody rigidbody))
                    _exploder.Explode(rigidbody);
        }
        else
        {
            Vector3 explosionPosition = explodable.transform.position;
            float explosionRadius = explodable.ExplosionRadius * _explosionRadiusMultiplier;
            float explosionForce = explodable.ExplosionForce * _explosionForceMultiplier;

            _exploder.Explode(explosionPosition, explosionRadius, explosionForce);
        }

        Destroy(explodable.gameObject);
    }

    private List<ExplodableCube> Spawn(ExplodableCube explodable)
    {
        List<ExplodableCube> explodables = new List<ExplodableCube>();

        int spawnablesCount = Random.Range(_minSpawnCount, _maxSpawnCount);

        for (int i = 0; i < spawnablesCount; i++)
        {
            Vector3 spawnablePosition = explodable.transform.position;
            Quaternion spawnableRotation = Quaternion.identity;
            Vector3 spawnableScale = explodable.transform.localScale / _spawnableScaleDivider;
            float splitChance = explodable.SplitChance / _splitChanceDivider;
            float explosionRadius = explodable.ExplosionRadius * _explosionRadiusMultiplier;
            float explosionForce = explodable.ExplosionForce * _explosionForceMultiplier;

            ExplodableCube spawnable = Instantiate(explodable, spawnablePosition, spawnableRotation, _spawnablesContainer);
            spawnable.Initialize(splitChance, explosionRadius, explosionForce);
            spawnable.transform.localScale = spawnableScale;

            explodables.Add(spawnable);
        }

        return explodables;
    }
}