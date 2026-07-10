using UnityEngine;
using UnityEngine.SceneManagement;

public class RatMovement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        // 1. 입력 받기
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(x, y).normalized;
    }

    void FixedUpdate() {
        MoveRat();
        RotateRat();
        ClampPosition();
       // 화면 밖 제한을 물리 연산 직후(FixedUpdate) 처리하여 떨림 방지
    }

    void MoveRat() {
        rigid.linearVelocity = moveInput * GameManager.Instance.ratSpeed;
    }

    void RotateRat() {
        // 이동 입력이 있을 때만 회전 (움직임을 멈췄을 때 원래 바라보던 방향 유지)
        if (moveInput.sqrMagnitude > 0.01f) {
            // moveInput 벡터의 각도를 계산 (라디안 -> 디그리 변환)
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            
            // 만약 에셋(이미지)의 앞방향이 '오른쪽' 기준이라면 아래 그대로 사용
            // 만약 에셋의 앞방향이 '위쪽' 기준이라면 (angle - 90f)로 수정해야 합니다.
            rigid.MoveRotation(angle + 90); 
        }
    }
    void ClampPosition() {
        // 화면의 세계 좌표 경계값을 계산 (메인 카메라 기준)
        Vector2 minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // 왼쪽 아래
        Vector2 maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // 오른쪽 위

        // 오브젝트의 절반 크기를 고려하여 제한
        float objectWidth = spriteRenderer.bounds.extents.x/2;
        float objectHeight = spriteRenderer.bounds.extents.y/2;

        // 물리 이동 반영 후의 rigid.position을 제한
        float clampedX = Mathf.Clamp(rigid.position.x, minBounds.x + objectWidth, maxBounds.x - objectWidth);
        float clampedY = Mathf.Clamp(rigid.position.y, minBounds.y + objectHeight, maxBounds.y - objectHeight);

        rigid.position = new Vector2(clampedX, clampedY);
    }
    

}