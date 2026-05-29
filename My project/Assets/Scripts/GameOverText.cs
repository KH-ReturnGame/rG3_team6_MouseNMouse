using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro 사용을 위해 필요

public class GameOverText : MonoBehaviour
{
    private TextMeshProUGUI gameOverText;

    void Start()
    {
        // 텍스트 컴포넌트 가져오기
        gameOverText = GetComponent<TextMeshProUGUI>();
        
        // GameManager의 captured 값에 따라 텍스트 설정
        if (GameManager.Instance.captured)
        {
            gameOverText.text = "mouse win \n ->";
        }
        else
        {
            gameOverText.text = "mouse win \n <-";
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("StartScene");
        }
    }
}