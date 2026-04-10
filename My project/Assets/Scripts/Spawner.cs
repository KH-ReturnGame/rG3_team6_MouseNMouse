using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject rat;
    public GameObject box;

    void Awake(){
        Instantiate(rat, Vector3.zero, Quaternion.Euler(0, 0, 0));
        Instantiate(box, new Vector3(5, 0 , 0), Quaternion.Euler(0, 0, 0));
    }
}
