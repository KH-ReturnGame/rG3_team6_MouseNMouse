using UnityEngine;

public class RatMovement : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rigid;
    private Vector2 moveInput;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(x, y).normalized;
    }

    void FixedUpdate() {
        MoveRat();
    }

    void MoveRat() {
        rigid.linearVelocity = moveInput * moveSpeed;
    }
}