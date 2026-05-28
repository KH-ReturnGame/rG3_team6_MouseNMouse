using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 게임에서 사용할 전역 변수들
    public float ratSpeed = 5f;
    public float gameTime = 60f;
    public int cheeseNum = 10;
    
    // RatMovement.cs에서 치즈를 먹을 때마다 깎일 변수
    public int restCheese = 10; 
    
    public bool canCapture = false;

    public bool captured = false;

    private void Awake()
    {
        // 씬이 바뀌어도 GameManager가 파괴되지 않도록 유지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 시작 씬의 MenuController에서 호출할 함수
    public void ChangeToGameScene(float speed, float time)
    {
        ratSpeed = speed;
        gameTime = time;
        
        // 게임 시작 시 초기화해야 할 데이터들
        restCheese = cheeseNum; // 생성할 치즈 개수만큼 남은 치즈 개수를 설정
        canCapture = false;

        // 게임 씬으로 이동
        SceneManager.LoadScene("GameScene"); 
    }
}