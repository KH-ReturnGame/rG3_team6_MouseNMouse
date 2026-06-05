using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    void Update(){
        GameManager.Instance.gameTime -= Time.deltaTime;
        timer.text = GameManager.Instance.gameTime.ToString("F0");
        if(GameManager.Instance.gameTime <= 0){
            GameManager.Instance.captured = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}
