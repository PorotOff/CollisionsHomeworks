using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private int _explodeInputIndex = 0;
    public event Action Exploding;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_explodeInputIndex))
            Exploding?.Invoke();
    }
}