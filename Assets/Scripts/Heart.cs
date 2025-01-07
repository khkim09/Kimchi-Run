using UnityEngine;

public class Heart : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Sprite onHeart;
    [SerializeField] private Sprite offHeart;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int heartNum;

    void Update()
    {
        if (GameManager.GM.lives >= heartNum)
        {
            spriteRenderer.sprite = onHeart;
        }
        else
        {
            spriteRenderer.sprite = offHeart;
        }
    }
}
