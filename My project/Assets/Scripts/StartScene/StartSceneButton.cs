using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
{
    public GameObject normalPanel;
    public GameObject settingPanel;
    public GameObject howToPlay;

    void Start(){
        normalPanel.SetActive(true);
        settingPanel.SetActive(false);
        howToPlay.SetActive(false);
    }
    public void OnClickStartButton()
    {
        GameManager.Instance.restTime = GameManager.Instance.gameTime;
        GameManager.Instance.ChangeToGameScene(GameManager.Instance.ratSpeed, GameManager.Instance.gameTime);
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

    public void OnClickHowToPlayButton(){
        if(!howToPlay.activeSelf){
            howToPlay.SetActive(true);
            normalPanel.SetActive(false);
        }
    }

    public void OnClickHowToPlayExitButton(){
        if(howToPlay.activeSelf){
            howToPlay.SetActive(false);
            normalPanel.SetActive(true);
        }
    }
}
