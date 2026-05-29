using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
{
    public GameObject normalPanel;
    public GameObject settingPanel;
    public int speed;

    void Start(){
        normalPanel.SetActive(true);
        settingPanel.SetActive(false);
    }
    public void OnClickStartButton()
    {
        GameManager.Instance.ChangeToGameScene(speed, 60);
    }

    public void OnClickSettingButton(){
        if(!settingPanel.activeSelf){
            settingPanel.SetActive(true);
            normalPanel.SetActive(false);
        }

    }

    public void OnClickSettingExitButton(){
        if(settingPanel.activeSelf){
            settingPanel.SetActive(false);
            normalPanel.SetActive(true);
        }
    }
}
