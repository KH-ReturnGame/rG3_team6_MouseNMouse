using UnityEngine;

public class BoxController : MonoBehaviour
{
    [Header("상자 설정")]
    public bool isReal = false;         // 진짜 상자인지 체크 (인스펙터에서 설정)
    public GameObject resultSprite;     // 자식으로 넣은 당첨/꽝 이미지 오브젝트 연결

    private SpriteRenderer bodyRenderer;
    private bool isOpened = false;

    void Start()
    {
        bodyRenderer = GetComponentInChildren<SpriteRenderer>();
        // 시작할 때 결과 이미지는 숨겨둡니다.
        if (resultSprite != null) resultSprite.SetActive(false);
    }

    void Update()
    {
        if (isOpened) return;

        // 자식 중 'Nail' 태그를 가진 오브젝트 개수 확인
        int currentNails = 0;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Nail")) currentNails++;
        }

        if (currentNails == 0)
        {
            OpenBox();
        }

        // [화면 제한 유지] 카메라 시야 밖으로 나가지 못하게 막음
        Vector2 minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (bodyRenderer != null)
        {
            float objectWidth = bodyRenderer.bounds.extents.x;
            float objectHeight = bodyRenderer.bounds.extents.y;
            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + objectWidth, maxBounds.x - objectWidth);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + objectHeight, maxBounds.y - objectHeight);
            transform.position = new Vector2(clampedX, clampedY);
        }
    }

    void OpenBox()
    {
        isOpened = true;

        // 1. 결과 스프라이트(당첨/꽝) 활성화하여 보여주기
        if (resultSprite != null) resultSprite.SetActive(true);

        // 2. 진짜일 때만 쥐 포획 가능 상태로 변경
        if (isReal)
        {
            GameManager.Instance.canCapture = true;
            if (bodyRenderer != null) bodyRenderer.color = new Color(1, 1, 1, 0.5f);
            Debug.Log("★진짜 상자! 이제 쥐를 잡으세요!");
        }
        else
        {
            // 가짜 상자는 붉은색으로 변하게 처리
            if (bodyRenderer != null) bodyRenderer.color = new Color(1, 0.5f, 0.5f, 0.5f);
            Debug.Log("꽝! 가짜였습니다.");
        }
    }
}