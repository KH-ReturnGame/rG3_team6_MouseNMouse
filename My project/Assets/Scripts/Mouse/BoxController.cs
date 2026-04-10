using UnityEngine;

public class BoxController : MonoBehaviour
{
    // 박스 내부에 'Nail' 태그를 가진 자식들이 있는지 감시
    void Update()
    {
        // 자식 오브젝트 개수 세기
        int currentNails = 0;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Nail")) currentNails++;
        }

        // 못이 모두 제거되었다면 상자가 열림
        if (currentNails == 0)
        {
            OpenBox();
        }

            // 화면의 세계 좌표 경계값을 계산 (메인 카메라 기준)
        Vector2 minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // 왼쪽 아래
        Vector2 maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // 오른쪽 위

        // 오브젝트의 절반 크기를 고려하여 제한 (선택 사항)
        float objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + objectWidth, maxBounds.x - objectWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + objectHeight, maxBounds.y - objectHeight);

        transform.position = new Vector2(clampedX, clampedY);
    }

    void OpenBox()
    {
        if (!GameManager.Instance.canCapture)
        {
            GameManager.Instance.canCapture = true;
            Debug.Log("이제 마우스가 쥐를 잡을 수 있음");
            
            //상자 이미지를 투명하게 하거나 제거
            //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); 
        }
    }
}