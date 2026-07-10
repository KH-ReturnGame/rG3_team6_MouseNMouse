using UnityEngine;

public class Boarder : MonoBehaviour
{
    private SpriteRenderer bodyRenderer;
    void Start()
    {
        bodyRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    // [화면 제한 유지] 카메라 시야 밖으로 나가지 못하게 막음
        
    void Update(){
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
        
}
