using UnityEngine;

[RequireComponent(typeof(ExplodeableCube))]
public class Spawner : MonoBehaviour
{
    private ExplodeableCube _explodeableCube;

    [SerializeField] private GameObject _spawnablePrefab;
    [SerializeField] private MeshRenderer _spawnableMeshRenderer;
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
    {
        _explodeableCube = GetComponent<ExplodeableCube>();
        _spawnChance = _maxSpawnChance;
    }

    private void OnEnable()
    {
        _explodeableCube.Exploded += Spawn;
    }

    private void OnDisable()
    {
        _explodeableCube.Exploded -= Spawn;
    }

    private void Spawn()
    {
        if (Random.Range(0, _maxSpawnChance) < _spawnChance)
        {
            int spawnablesCount = Random.Range(_minSpawnCount, _maxSpawnCount);

            for (int i = 0; i < spawnablesCount; i++)
            {
                Vector3 spawnablePosition = transform.position;
                Quaternion spawnableRotation = Quaternion.identity;
                Vector3 spawnableScale = transform.localScale / _spawnableScaleDivider;
                Color spawnableColor = Random.ColorHSV();

                _spawnablePrefab.transform.localScale = spawnableScale;
                _spawnableMeshRenderer.material.color = spawnableColor;
                Instantiate(_spawnablePrefab, spawnablePosition, spawnableRotation);

                _spawnChance /= _spawnChanceDivider;

                Debug.Log("New spawnable spawned");
            }
        }
    }
}