using UnityEngine;

public class ExplodeableCube : MonoBehaviour
{
    public void Explode()
        => Destroy(gameObject);
}