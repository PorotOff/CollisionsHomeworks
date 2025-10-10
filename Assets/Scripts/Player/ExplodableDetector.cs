using System;
using UnityEngine;

public class ExplodableDetector : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    [SerializeField] private Camera _camera;
    private Ray _ray;
    [SerializeField] private float _rayLength = 100f;

    public event Action<ExplodeableCube> OnExplodeableDetected;

    private void OnEnable()
        => _inputHandler.OnExploding += Detect;

    private void OnDisable()
        => _inputHandler.OnExploding -= Detect;

    private void Detect()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit hit, _rayLength))
            if (hit.collider.gameObject.TryGetComponent(out ExplodeableCube explodeable))
                OnExplodeableDetected?.Invoke(explodeable);
    }
}