using UnityEngine;
using System.Collections;

public class NonOverlappingSpawner : MonoBehaviour
{
    public GameObject cheesePrefab;
    public float cheeseRadius = 0.5f; // 치즈의 크기(반지름)
    public float padding = 0.1f;
    public int maxAttempts = 100;    // 한 오브젝트당 최대 시도 횟수 (무한루프 방지)

    void Start()
    {
        SpawnNonOverlappingCheese();
    }

    void SpawnNonOverlappingCheese()
    {
        Camera mainCam = Camera.main;
        int spawnedCount = 0;

        for (int i = 0; i < GameManager.Instance.cheeseNum; i++)
        {
            bool positionFound = false;
            int attempts = 0;

            while (!positionFound && attempts < maxAttempts)
            {
                attempts++;

                // 1. 랜덤 좌표 생성
                float randomX = Random.Range(padding, 1f - padding);
                float randomY = Random.Range(padding, 1f - padding);
                Vector3 spawnPos = mainCam.ViewportToWorldPoint(new Vector3(randomX, randomY, 10f));
                spawnPos.z = 0f;

                // 2. 해당 위치에 다른 치즈(충돌체)가 있는지 원형으로 검사
                // 이 기능을 쓰려면 치즈 프리팹에 'Circle Collider 2D'가 있어야 합니다.
                Collider2D hit = Physics2D.OverlapCircle(spawnPos, cheeseRadius);

                if (hit == null) // 아무것도 없다면 생성!
                {
                    GameObject cheese = Instantiate(cheesePrefab, spawnPos, Quaternion.identity);
                    cheese.name = "Cheese";
                    positionFound = true;
                    spawnedCount++;
                }
            }
        }

        Debug.Log($"총 {spawnedCount}개의 치즈를 겹치지 않게 생성했습니다.");
    }
}