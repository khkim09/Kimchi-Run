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
    [SerializeField] public float livingTime; // 'Player' 생존 시간

    [Header("References")]
    [SerializeField] public GameObject IntroUI;
    [SerializeField] public GameObject DeadUI;
    [SerializeField] public GameObject enemySpawner;
    [SerializeField] public GameObject foodSpawner;
    [SerializeField] public GameObject goldSpawner;
    [SerializeField] public Player playerScript;
    [SerializeField] public GameObject PlayingUI;

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

    public float CalculateScore() // 점수 실시간 update
    {
        return 10 * (Time.time - livingTime);
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore"); // PlayerPrefs class - 사용자 disk에 data 저장

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save(); // 사용자 disk에 저장
        }
    }

    public int GetHighScore() // 최고 점수 호출
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public float CalculateGameSpeed() // game speed 조절 (난이도 상승)
    {
        if (gameState != GameState.Playing)
        {
            return 5f;
        }
        float speed = 8f + (0.7f * Mathf.Floor(CalculateScore() / 100f));
        
        return Mathf.Min(speed, 30f); // 최고 속도 20
    }

    void Update()
    {
        if (gameState == GameState.Intro && Input.GetKeyDown(KeyCode.Space)) // 'Intro' -> 'Playing'
        {
            gameState = GameState.Playing; // 게임 상태 변경 (Playing)
            IntroUI.SetActive(false); // IntroUI 비활성화
            PlayingUI.SetActive(true);
            enemySpawner.SetActive(true); // 나머지 spawner 활성화
            foodSpawner.SetActive(true);
            goldSpawner.SetActive(true);
            livingTime = Time.time; // 시작 시간 저장
        }

        if (gameState == GameState.Playing && lives == 0) // 'Playing' -> 'Dead'
        {
            playerScript.KillPlayer();
            gameState = GameState.Dead; // 게임 상태 변경 (Dead)
            DeadUI.SetActive(true);
            enemySpawner.SetActive(false); // 나머지 spawner 비활성화
            foodSpawner.SetActive(false);
            goldSpawner.SetActive(false);
            SaveHighScore();
        }

        if (gameState == GameState.Dead && Input.GetKeyDown(KeyCode.Space)) // 'Dead' -> 'Intro'
        {
            SceneManager.LoadScene("main"); // scene 화면을 main으로 돌리면 알아서 loop화
        }
    }
}
