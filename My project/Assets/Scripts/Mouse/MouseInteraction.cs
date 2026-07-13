using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseInteraction : MonoBehaviour
{
    [Header("Nail Settings")]
    [Tooltip("못을 몇 번 클릭해야 뽑힐지 설정합니다.")]
    public int maxClickCount = 5; 

    [Header("Penalty Settings")]
    [Tooltip("헛손질(실패) 시 포획 불가 패널티 시간(초)입니다.")]
    public float captureCooldown = 1f;

    private int currentClickCount = 0;  // 현재 클릭한 횟수
    private GameObject targetNail;       // 현재 타겟이 된 못
    
    private float lastMissTime = -100f;  // 마지막으로 헛손질(실패)한 시간

    public Slider cooldown;

    void Start(){
        cooldown.enabled = false;
    }
    void Update()
    {
        // 마우스를 처음 누르는 순간(Down)에만 처리를 수행합니다.
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }

        cooldown.value -= Time.deltaTime;
    }

    void HandleClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // 1. 현재 패널티(쿨타임) 중인지 확인
        bool isCoolingDown = Time.time < lastMissTime + captureCooldown;

        // 2. [포획 성공 로직] 클릭한 게 '쥐'일 때
        if (hit.collider != null && hit.collider.CompareTag("Rat"))
        {
            if (isCoolingDown)
            {
                Debug.Log($"헛손질 패널티 중입니다! 남은 시간: {(lastMissTime + captureCooldown) - Time.time:F1}초");
                return; 
            }

            if (GameManager.Instance.canCapture)
            {
                Debug.Log("쥐를 잡았습니다! 마우스 승리!");
                GameManager.Instance.captured = true;
                
                // ★ [추가] 씬이 넘어가기 직전에 GameManager에게 효과음 재생을 명령합니다!
                GameManager.Instance.PlayCaptureSound();

                SceneManager.LoadScene("GameOver");
                return;
            }
            else
            {
                Debug.Log("아직 상자가 닫혀 있어 쥐를 잡을 수 없습니다!");
                // 상자가 닫혀있을 때 쥐를 누르는 건 패널티 대상이 아니므로 그냥 리턴합니다.
                return;
            }
        }

        // 3. [헛손질 패널티 판단 로직] 쥐를 잡을 수 있는 타이밍인데 쥐가 아닌 다른 곳을 클릭한 경우
        if (GameManager.Instance.canCapture)
        {
            // 이미 쿨타임 중이 아니라면 새로운 패널티 적용
            if (!isCoolingDown)
            {
                lastMissTime = Time.time;
                cooldown.enabled = true;
                cooldown.value = 10;
                Debug.Log("헛손질! 쥐를 놓쳤습니다. 1초 동안 포획 불가 패널티!");
            }
        }

        // 4. [못 로직] 클릭한 게 못이라면? (패널티 처리와 별개로 못 카운트는 쌓이도록 유지)
        if (hit.collider != null && hit.collider.CompareTag("Nail"))
        {
            GameObject clickedNail = hit.collider.gameObject;

            if (targetNail != clickedNail)
            {
                targetNail = clickedNail;
                currentClickCount = 0;
            }

            currentClickCount++;
            Debug.Log($"못 클릭함! ({currentClickCount} / {maxClickCount})");

            if (currentClickCount >= maxClickCount)
            {
                Destroy(targetNail);
                Debug.Log("못을 뽑았습니다!");
                ResetProgress();
            }
        }
        else
        {
            // 허공을 클릭하면 못 진행 상황 초기화
            ResetProgress();
        }
    }

    void ResetProgress()
    {
        targetNail = null;
        currentClickCount = 0;
    }
}