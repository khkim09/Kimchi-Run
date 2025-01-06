using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 1.0f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}
