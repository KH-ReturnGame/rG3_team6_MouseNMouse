using UnityEngine;

public class RatMovement : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Vector2 moveInput;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(x, y).normalized;
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

    void FixedUpdate() {
        MoveRat();
    }

    void MoveRat() {
        rigid.linearVelocity = moveInput * GameManager.Instance.ratSpeed;
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == "Cheese"){
            Destroy(other.gameObject);
            GameManager.Instance.restCheese--;
        }
    }
}