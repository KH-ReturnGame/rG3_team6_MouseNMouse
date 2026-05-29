using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sliders : MonoBehaviour
{
    public Slider cheeseNum, ratSpeed, timer;
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
    }
    void ratSpeedChanged(float value){
        int intValue = (int)value;
        GameManager.Instance.ratSpeed = intValue;
    }
    void timerChanged(float value){
        int intValue = (int)value;
        GameManager.Instance.gameTime = intValue;
    }
}
