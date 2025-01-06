using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float scrollSpeed;

    [Header("References")]
    [SerializeField] public MeshRenderer meshRenderer;

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0); // frame 단위 -> 1s 단위 update
    }
}
