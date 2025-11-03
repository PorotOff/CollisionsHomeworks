using System;
using UnityEngine;

public class ExplodableDetector : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayLength = 100f;

    public event Action<Explodable> ExplodableDetected;

    private void OnEnable()
        => _inputHandler.Exploding += Detect;

    private void OnDisable()
        => _inputHandler.Exploding -= Detect;

    private void Detect()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayLength))
            if (hit.collider.TryGetComponent(out Explodable explodable))
                ExplodableDetected?.Invoke(explodable);
    }
}