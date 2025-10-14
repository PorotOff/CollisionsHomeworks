using System.Collections;
using UnityEngine;

public class Utils : MonoBehaviour
{
    [SerializeField] private GameObject _defaultDebugSpherePrefab;

    public void DrawDebugSphere(Vector3 position, float radius)
    {
        _defaultDebugSpherePrefab.transform.position = position;
        _defaultDebugSpherePrefab.transform.localScale = Vector3.one * radius;

        StartCoroutine(DrawSphere(_defaultDebugSpherePrefab));
    }
    
    private IEnumerator DrawSphere(GameObject sphere)
    {
        float timer = 1f;
        float elapsedTime = 0;

        GameObject instantiated = Instantiate(sphere);

        while (elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(instantiated);
    }
}