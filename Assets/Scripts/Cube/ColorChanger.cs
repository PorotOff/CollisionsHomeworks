using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private void Awake()
        => _meshRenderer = GetComponent<MeshRenderer>();

    private void Start()
        => _meshRenderer.material.color = Random.ColorHSV();
}