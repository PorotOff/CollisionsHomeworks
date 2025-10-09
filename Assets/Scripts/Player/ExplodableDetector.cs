using System;
using UnityEngine;

public class ExplodableDetector : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Camera _camera;
    private Ray _ray;

    public event Action<IExplodeable> OnExplodeableDetected;

    private void OnEnable()
    {
        _inputHandler.OnExploding += Detect;
    }

    private void OnDisable()
    {
        _inputHandler.OnExploding -= Detect;
    }

    void Update()
    {
        Debug.DrawRay(_ray.origin, _ray.direction * Mathf.Infinity, Color.red);
    }

    private void Detect()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        

        if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log("Ray hitted");

            if (hit.collider.TryGetComponent(out IExplodeable explodeable))
            {
                OnExplodeableDetected?.Invoke(explodeable);
                Debug.Log("Explodeable detected");
            }
        }
    }
}