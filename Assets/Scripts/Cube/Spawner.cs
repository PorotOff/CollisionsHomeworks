using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ExplodeableCube _spawnablePrefab;
    [SerializeField] private Transform _spawnablesContainer;
    [SerializeField] private int _spawnableScaleDivider = 2;
    [SerializeField, Range(1f, 10f)] private int _minSpawnCount = 2;
    [SerializeField, Range(1f, 10f)] private int _maxSpawnCount = 6;

    public event Action Spawned;
    public event Action NotSpawned;

    private void OnValidate()
    {
        if (_minSpawnCount > _maxSpawnCount)
            _maxSpawnCount = _minSpawnCount;
    }

    public void Spawn(ExplodeableCube explodeable)
    {
        if (explodeable.CanSplit())
        {
            int spawnablesCount = UnityEngine.Random.Range(_minSpawnCount, _maxSpawnCount);

            for (int i = 0; i < spawnablesCount; i++)
            {
                Vector3 spawnablePosition = explodeable.transform.position;
                Quaternion spawnableRotation = Quaternion.identity;
                Vector3 spawnableScale = explodeable.transform.localScale / _spawnableScaleDivider;

                ExplodeableCube spawnable = Instantiate(_spawnablePrefab, spawnablePosition, spawnableRotation, _spawnablesContainer);
                spawnable.transform.localScale = spawnableScale;
                spawnable.DivideSplitChance();
                
                spawnable.Exploded += OnCubeExploded;
            }

            Spawned?.Invoke();
        }
        else
        {
            NotSpawned?.Invoke();
        }
    }
    
    private void OnCubeExploded(ExplodeableCube explodeable)
    {
        explodeable.Exploded -= OnCubeExploded;
        Destroy(explodeable);
    }
}