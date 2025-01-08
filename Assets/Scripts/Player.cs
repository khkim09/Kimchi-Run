using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isJumping;
    [SerializeField] private int jumpCnt; // double jump 구현 위한 변수

    // 사전 작업
    /*
     * 'Player' - RigidBody2D 생성 (Physics 물리 법칙 적용을 위한 작업)
     * 'Player', 'Ground' - BoxCollider2D 생성 (collider 생성 - 경계면 생성, player 무한 낙하 방지)
     */
    
    [Header("References")]
    [SerializeField] private Rigidbody2D playerRigidBody; // 'Player' 물리 법칙 적용
    [SerializeField] private Animator playerAnimator; // 'Player' 애니메이션 적용
    [SerializeField] private BoxCollider2D playerCollider; // 'Player' death 구현
    [SerializeField] private bool isInvincible = false; // 무적 상태

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < 2) // double jump 구현
        {
            playerRigidBody.linearVelocity = Vector2.zero; // 일정한 jump force를 위해 veloctiy 초기화 (내려오는 순간에서의 중력 무시를 위한 작업)
            playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // y축으로만 force 적용
            jumpCnt++;
            isJumping = true;
            playerAnimator.SetInteger("state", 1); // jumping animation
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") // 땅에 착지 (jump move를 위한 count 초기화)
        {
            if (isJumping) // landing animation
            {
                playerAnimator.SetInteger("state", 2);
            }
            jumpCnt = 0;
            isJumping = false;
        }
    }

    public void KillPlayer()
    {
        playerAnimator.SetInteger("state", 1); // 마지막 jump animation 수행
        playerCollider.enabled = false; // 'Player' 캐릭터 추락을 위해 collider 해제
        playerAnimator.enabled = false; // 'Player'의 animator도 해제
        playerRigidBody.linearVelocity = Vector2.zero; // 현재 'player'위치에서 마지막 jump motion을 위해 velocity 초기화
        playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // 마지막 jump motion
        
        if (transform.position.y < -7) // 추락 후 gameObject 삭제
        {
            Destroy(gameObject);
        }
    }

    void Hit()
    {
        GameManager.GM.lives -= 1;
        if (GameManager.GM.lives == 0)
            KillPlayer();
    }

    void Heal()
    {
        if (GameManager.GM.lives >= 3)
            return;
        GameManager.GM.lives += 1;
    }

    void StartInvincible()
    {
        isInvincible = true; // 5s 간 무적
        Invoke("StopInvincible", 5.0f); // StopInvincible method 호출 (무적 off)
    }

    void StopInvincible()
    {
        isInvincible = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            if (!isInvincible) // 무적 상태 아닐 경우에만 체력 - 1
            {
                Destroy(collider.gameObject);
                Hit();
            }
        }
        else if (collider.tag == "Food")
        {
            Destroy(collider.gameObject);
            Heal();
        }
        else if (collider.tag == "Golden")
        {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}
