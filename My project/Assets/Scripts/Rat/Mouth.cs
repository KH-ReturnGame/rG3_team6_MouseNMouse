using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == "Cheese"){
            Destroy(other.gameObject);
            GameManager.Instance.restCheese--;
            if(GameManager.Instance.restCheese <= 0){
                GameManager.Instance.captured = false;
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
