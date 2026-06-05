using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("소환할 프리팹들")]
    public GameObject ratPrefab;
    public GameObject realBoxPrefab;
    public GameObject fakeBoxPrefab;
    
    [Header("소환 설정")]
    [Range(1, 10)]
    public int fakeBoxCount = 3; // 생성할 가짜 상자의 개수

    void Start()
    {
        Camera cam = Camera.main;

        // 1. 쥐 생성 (화면 중앙)
        Instantiate(ratPrefab, Vector3.zero, Quaternion.identity);

        // 2. 진짜 상자 1개 생성 (화면 내 랜덤 위치)
        SpawnAtRandomViewport(realBoxPrefab, cam);

        // 3. 가짜 상자들 생성 (화면 내 랜덤 위치)
        for (int i = 0; i < fakeBoxCount; i++)
        {
            SpawnAtRandomViewport(fakeBoxPrefab, cam);
        }
    }

    // 카메라 화면(Viewport) 비율을 기준으로 랜덤 좌표를 구해 소환하는 함수
    void SpawnAtRandomViewport(GameObject prefab, Camera cam)
    {
        // 화면의 20% ~ 80% 사이 구간에만 소환되도록 여백을 둠
        Vector3 randomPos = cam.ViewportToWorldPoint(new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), 10f));
        randomPos.z = 0; // 2D 환경이므로 Z축은 0으로 고정
        Instantiate(prefab, randomPos, Quaternion.identity);
    }
}