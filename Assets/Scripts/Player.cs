using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float jumpForce;
    [SerializeField] public bool isJumping;

    // 사전 작업
    /*
     * 'Player' - RigidBody2D 생성 (Physics 물리 법칙 적용을 위한 작업)
     * 'Player', 'Ground' - BoxCollider2D 생성 (collider 생성 - 경계면 생성, player 무한 낙하 방지)
     */
    [Header("References")]
    [SerializeField] public Rigidbody2D playerRigidBody; // 'Player' 물리 법칙 적용

    void Update()
    {
        // "Jump" move
        if (Input.GetKeyDown(KeyCode.Space)) // "space" key 입력 시
        {
            playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // y축으로만 움직임 생성
        }
    }
}
