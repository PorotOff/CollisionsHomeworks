using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private ExplodableDetector _explodeableDetector;

    private void OnEnable()
    {
        _explodeableDetector.OnExplodeableDetected += Explode;
    }

    private void OnDisable()
    {
        _explodeableDetector.OnExplodeableDetected -= Explode;
    }

    private void Explode(IExplodeable explodeable)
    {
        explodeable.Explode();
        Debug.Log("Exploder explodes");
    }
}