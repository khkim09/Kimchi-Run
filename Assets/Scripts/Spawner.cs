using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;

    [Header("References")]
    [SerializeField] public GameObject[] gameObjects;

    void Start()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay)); // 일정 시간 후 method 호출 (2초 후 "Spawn" method 호출)
    }

    void Spawn()
    {
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)]; // random building 선택
        Instantiate(randomObject, transform.position, Quaternion.identity); // random building - instance화
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
