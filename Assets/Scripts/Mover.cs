using UnityEngine;

public class Mover : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.left * GameManager.GM.CalculateGameSpeed() * 0.8f * Time.deltaTime;
    }
}
