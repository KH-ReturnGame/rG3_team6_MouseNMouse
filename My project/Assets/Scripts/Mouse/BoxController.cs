using UnityEngine;

public class BoxController : MonoBehaviour
{
    // 자식(Body)에 있는 SpriteRenderer를 저장해둘 변수
    private SpriteRenderer bodyRenderer;

    void Start()
    {
        // 시작할 때 자식 오브젝트들 중에서 SpriteRenderer를 찾아 저장해둡니다.
        bodyRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // 1. 자식 오브젝트 중 못(Nail) 개수 세기
        int currentNails = 0;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Nail")) currentNails++;
        }

        // 2. 못이 모두 제거되었다면 상자가 열림
        if (currentNails == 0)
        {
            OpenBox();
        }

        // 3. 화면의 세계 좌표 경계값을 계산 (메인 카메라 기준)
        Vector2 minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // 왼쪽 아래
        Vector2 maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // 오른쪽 위

        // 4. bodyRenderer가 안전하게 존재하는지 확인 후 위치 제한 계산
        if (bodyRenderer != null)
        {
            float objectWidth = bodyRenderer.bounds.extents.x;
            float objectHeight = bodyRenderer.bounds.extents.y;

            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + objectWidth, maxBounds.x - objectWidth);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + objectHeight, maxBounds.y - objectHeight);

            transform.position = new Vector2(clampedX, clampedY);
        }
        else
        {
            // 혹시라도 렌더러가 없다면 크기 계산 없이 중심점 기준으로만 제한
            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
            transform.position = new Vector2(clampedX, clampedY);
        }
    }

    void OpenBox()
    {
        if (!GameManager.Instance.canCapture)
        {
            GameManager.Instance.canCapture = true;
            Debug.Log("이제 마우스가 쥐를 잡을 수 있음");
            
            // 상자 이미지를 투명하게 만들 때도 안전하게 접근
            if (bodyRenderer != null)
            {
                bodyRenderer.color = new Color(1, 1, 1, 0.5f); 
            }
        }
    }
}