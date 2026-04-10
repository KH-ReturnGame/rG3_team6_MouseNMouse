using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseInteraction : MonoBehaviour
{
    public float interactionTime = 1.5f; 
    private float currentHoldTime = 0f;  
    private GameObject targetNail;       

    void Update()
    {
        // 1. 마우스를 처음 눌렀을 때 (Down)
        if (Input.GetMouseButtonDown(0))
        {
            HandleFirstClick();
        }

        // 2. 마우스를 계속 누르고 있을 때 (Hold) - 못을 뽑는 중일 때만 작동
        if (Input.GetMouseButton(0) && targetNail != null)
        {
            UpdateNailProgress();
        }
        
        // 3. 마우스에서 손을 떼면 초기화 (Up)
        if (Input.GetMouseButtonUp(0))
        {
            ResetProgress();
        }
    }

    void HandleFirstClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            // [포획 로직] 클릭한 게 쥐라면? 즉시 잡기 체크!
            if (hit.collider.CompareTag("Rat"))
            {
                if (GameManager.Instance.canCapture)
                {
                    Debug.Log("쥐를 잡았습니다! 마우스 승리!");
                    SceneManager.LoadScene("GameOver");
                    // 2주차 씬 이동 로직 예정
                }
                else
                {
                    Debug.Log("아직 상자가 닫혀 있어 쥐를 잡을 수 없습니다!");
                }
                return; // 쥐를 잡았거나 시도했으면 아래 못 로직은 건너뜀
            }

            // [못 로직] 클릭한 게 못이라면? 홀드 시작
            if (hit.collider.CompareTag("Nail"))
            {
                targetNail = hit.collider.gameObject;
                currentHoldTime = 0f;
            }
        }
    }

    void UpdateNailProgress()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // 여전히 같은 못 위에 마우스가 있는지 확인
        if (hit.collider != null && hit.collider.gameObject == targetNail)
        {
            currentHoldTime += Time.deltaTime;
            
            if (currentHoldTime >= interactionTime)
            {
                Destroy(targetNail);
                Debug.Log("못을 뽑았습니다!");
                ResetProgress();
            }
        }
        else
        {
            // 상자가 밀려나서 마우스가 못을 벗어나면 초기화
            ResetProgress();
        }
    }

    void ResetProgress()
    {
        targetNail = null;
        currentHoldTime = 0f;
    }
}