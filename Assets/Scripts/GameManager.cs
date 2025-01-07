using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public static GameManager GM;
    [SerializeField] public GameState gameState = GameState.Intro; // 초기 게임 상태 (Intro로 지정)
    [SerializeField] public int lives = 3; // 'Player' 체력을 GameManager에서 관리

    [Header("References")]
    [SerializeField] public GameObject IntroUI;
    [SerializeField] public GameObject enemySpawner;
    [SerializeField] public GameObject foodSpawner;
    [SerializeField] public GameObject goldSpawner;
    [SerializeField] public Player playerScript;

    void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
    }

    void Start()
    {
        IntroUI.SetActive(true); // 게임 시작화면 -> IntroUI 활성화
    }

    void Update()
    {
        if (gameState == GameState.Intro && Input.GetKeyDown(KeyCode.Space)) // 'Intro' -> 'Playing'
        {
            gameState = GameState.Playing; // 게임 상태 변경 (Playing)
            IntroUI.SetActive(false); // IntroUI 비활성화
            enemySpawner.SetActive(true); // 나머지 spawner 활성화
            foodSpawner.SetActive(true);
            goldSpawner.SetActive(true);
        }

        if (gameState == GameState.Playing && lives == 0) // 'Playing' -> 'Dead'
        {
            playerScript.KillPlayer();
            gameState = GameState.Dead; // 게임 상태 변경 (Dead)
            enemySpawner.SetActive(false); // 나머지 spawner 비활성화
            foodSpawner.SetActive(false);
            goldSpawner.SetActive(false);
        }

        if (gameState == GameState.Dead && Input.GetKeyDown(KeyCode.Space)) // 'Dead' -> 'Intro'
        {
            SceneManager.LoadScene("main"); // scene 화면을 main으로 돌리면 알아서 loop화
        }
    }
}
