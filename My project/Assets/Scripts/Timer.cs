using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    void Update(){
        GameManager.Instance.restTime -= Time.deltaTime;
        timer.text = GameManager.Instance.restTime.ToString("F0");
        if(GameManager.Instance.restTime <= 0){
            GameManager.Instance.captured = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}
