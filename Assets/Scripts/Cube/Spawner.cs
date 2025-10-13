using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnablesContainer;
    [SerializeField] private int _spawnableScaleDivider = 2;
    [SerializeField] private int _splitChanceDivider = 2;
    [SerializeField, Range(1f, 10f)] private int _minSpawnCount = 2;
    [SerializeField, Range(1f, 10f)] private int _maxSpawnCount = 6;
    
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
                float splitChance = explodeable.SplitChance / _splitChanceDivider;

                ExplodeableCube spawnable = Instantiate(explodeable, spawnablePosition, spawnableRotation, _spawnablesContainer);
                spawnable.Initialize(splitChance);
                spawnable.transform.localScale = spawnableScale;
                spawnable.TakeExplosion(transform.position);
            }
        }
        else
        {
            NotSpawned?.Invoke();
        }

        Destroy(explodeable.gameObject);
    }
}