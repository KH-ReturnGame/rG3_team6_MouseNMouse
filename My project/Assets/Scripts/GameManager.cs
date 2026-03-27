using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header ("Game")]
    public float timer;
    public int cheeseNum;

    [Header ("Rat")]
    public float ratSpeed;

    [Header ("Mouse")]
    public float mouseSpeed;
    public bool canCapture;

    void Awake(){
        if(GameManager.Instance == null){
            GameManager.Instance = this;
        }

        else{
            Debug.Log("게임매니저가 있네요");
            Destroy(gameObject);
        }
        
    }
}
