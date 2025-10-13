using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private ExplodableDetector _explodeableDetector;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    private void OnEnable()
    {
        _explodeableDetector.ExplodeableDetected += Detonate;
        _spawner.NotSpawned += _exploder.Explode;
    }

    private void OnDisable()
    {
        _explodeableDetector.ExplodeableDetected -= Detonate;
        _spawner.NotSpawned -= _exploder.Explode;
    }

    private void Detonate(ExplodeableCube explodeable)
        => _spawner.Spawn(explodeable);
}