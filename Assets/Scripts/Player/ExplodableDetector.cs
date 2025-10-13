using System;
using UnityEngine;

public class ExplodableDetector : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayLength = 100f;

    private Ray _ray;
    public event Action<ExplodableCube> ExplodableDetected;

    private void OnEnable()
        => _inputHandler.Exploding += Detect;

    private void OnDisable()
        => _inputHandler.Exploding -= Detect;

    private void Detect()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit hit, _rayLength))
            if (hit.collider.TryGetComponent(out ExplodableCube explodable))
                ExplodableDetected?.Invoke(explodable);
    }
}