using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InsectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] insectPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            StopAllCoroutines();
        }
    }
    public IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            int insectIndex = Random.Range(0, insectPrefabs.Length);
            Instantiate(insectPrefabs[insectIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }
}
