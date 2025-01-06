using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private int jumpCnt; // double jump 구현 위한 변수

    // 사전 작업
    /*
     * 'Player' - RigidBody2D 생성 (Physics 물리 법칙 적용을 위한 작업)
     * 'Player', 'Ground' - BoxCollider2D 생성 (collider 생성 - 경계면 생성, player 무한 낙하 방지)
     */
    [Header("References")]
    [SerializeField] private Rigidbody2D playerRigidBody; // 'Player' 물리 법칙 적용

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < 2) // double jump 구현
        {
            playerRigidBody.linearVelocity = Vector2.zero; // 일정한 jump force를 위해 veloctiy 초기화 (내려오는 순간에서의 중력 무시를 위한 작업)
            playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // y축으로만 force 적용
            jumpCnt++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCnt = 0;
        }
    }
}
