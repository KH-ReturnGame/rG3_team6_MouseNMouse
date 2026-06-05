using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sliders : MonoBehaviour
{
    public Slider cheeseNum, ratSpeed, timer;
    public TextMeshProUGUI CheeseNum, RatSpeed, Timer;
    void Start(){
        cheeseNum.onValueChanged.AddListener(cheeseNumChanged);
        ratSpeed.onValueChanged.AddListener(ratSpeedChanged);
        timer.onValueChanged.AddListener(timerChanged);

        cheeseNum.value = GameManager.Instance.cheeseNum;
        ratSpeed.value = GameManager.Instance.ratSpeed;
        timer.value = GameManager.Instance.gameTime;
        
        cheeseNumChanged(cheeseNum.value);
        ratSpeedChanged(ratSpeed.value);
        timerChanged(timer.value);
    }

    void cheeseNumChanged(float value){
        int intValue = (int)value;
        GameManager.Instance.cheeseNum = intValue;
        CheeseNum.text = $"치즈 {value.ToString("F0")} 개";
    }
    void ratSpeedChanged(float value){
        int intValue = (int)value;
        GameManager.Instance.ratSpeed = intValue;
        RatSpeed.text = $"쥐 속도 {value.ToString("F0")}";
    }
    void timerChanged(float value){
        int intValue = (int)value;
        GameManager.Instance.gameTime = intValue;
        Timer.text = $"{value.ToString("F0")} 초";
    }
}
