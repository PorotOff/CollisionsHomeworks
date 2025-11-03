using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnablesContainer;
    [SerializeField, Range(1f, 10f)] private int _minSpawnCount = 2;
    [SerializeField, Range(1f, 10f)] private int _maxSpawnCount = 6;

    private void OnValidate()
    {
        if (_minSpawnCount > _maxSpawnCount)
            _maxSpawnCount = _minSpawnCount;
    }

    public List<Explodable> Spawn(Explodable explodable, Vector3 position, Quaternion rotation, Vector3 scale, float splitChance, float explosionRadius, float explosionForce)
    {
        List<Explodable> explodables = new List<Explodable>();

        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount);

        for (int i = 0; i < spawnCount; i++)
        {
            Explodable spawnable = Instantiate(explodable, position, rotation, _spawnablesContainer);
            spawnable.Initialize(splitChance, explosionRadius, explosionForce);
            spawnable.transform.localScale = scale;

            explodables.Add(spawnable);
        }

        return explodables;
    }
}