using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;

    [SerializeField] private GameObject _spawnablePrefab;
    [SerializeField] private int _spawnableScaleDivider = 2;

    [SerializeField, Range(1f, 10f)] private int _minSpawnCount = 2;
    [SerializeField, Range(1f, 10f)] private int _maxSpawnCount = 6;
    private float _maxSpawnChance = 100f;
    private float _spawnChance;
    [SerializeField] private int _spawnChanceDivider = 2;

    private void OnValidate()
    {
        if (_minSpawnCount > _maxSpawnCount)
            _maxSpawnCount = _minSpawnCount;
    }

    private void Awake()
        => _spawnChance = _maxSpawnChance;

    private void Start()
        => PrintSpawnChance();

    private void OnEnable()
        => _exploder.OnExploded += Spawn;

    private void OnDisable()
        => _exploder.OnExploded -= Spawn;

    private void Spawn(ExplodeableCube explodeable)
    {
        if (Random.Range(0, _maxSpawnChance) <= _spawnChance)
        {
            int spawnablesCount = Random.Range(_minSpawnCount, _maxSpawnCount);

            for (int i = 0; i < spawnablesCount; i++)
            {
                Vector3 spawnablePosition = explodeable.transform.position;
                Quaternion spawnableRotation = Quaternion.identity;
                Vector3 spawnableScale = explodeable.transform.localScale / _spawnableScaleDivider;

                GameObject spawnable = Instantiate(_spawnablePrefab, spawnablePosition, spawnableRotation);
                spawnable.transform.localScale = spawnableScale;
            }

            _spawnChance /= _spawnChanceDivider;
            PrintSpawnChance();
        }
    }
    
    private void PrintSpawnChance()
        => Debug.Log($"Spawn chance = {_spawnChance}");
}