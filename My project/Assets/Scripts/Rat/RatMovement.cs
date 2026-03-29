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