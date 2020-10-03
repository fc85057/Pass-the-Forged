using System;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    public static event Action<GameObject> OnBackgroundSpawned;

    [SerializeField]
    private GameObject backgroundPrefab;

    private void Start()
    {
        BackgroundTrigger.OnPlayerEntered += SpawnBackground;
        // OnBackgroundSpawned(this.gameObject);
    }

    private void SpawnBackground()
    {
        Vector3 newBackgroundPosition = new Vector3(transform.position.x + 57.5f,
            transform.position.y, transform.position.z);
        GameObject newBackground = Instantiate(backgroundPrefab, newBackgroundPosition, Quaternion.identity);
        OnBackgroundSpawned(newBackground);
        BackgroundTrigger.OnPlayerEntered -= SpawnBackground;
    }

}
