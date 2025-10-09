using System;
using UnityEngine;

public class ExplodeableCube : MonoBehaviour, IExplodeable
{
    public event Action Exploded;

    public void Explode()
    {
        Exploded?.Invoke();
        Destroy(gameObject);

        Debug.Log("Explodeable exploded");
    }
}