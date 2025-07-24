using UnityEngine;
using System.Collections.Generic;

public class RoomMiniBossManager : MonoBehaviour
{
    List<GameObject> allBoss = new List<GameObject>();

    bool isBossSpawned = false;
    bool isBossDefeated = false;
    bool isDoorOpened = false;



    void Update()
    {
        if (!isBossSpawned)
        {
            SpawnBoss();
        }

        if (isBossSpawned && !isBossDefeated)
        {
            CheckBossDefeat();
        }

        if (isBossDefeated && !isDoorOpened)
        {
            OpenBossRoomDoor();
            isDoorOpened = true;
        }

    }

    void SpawnBoss()
    {
        // Spawn boss chỉ một lần, không cần tìm lại trong Update
        if (allBoss.Count == 0)
        {
            allBoss.AddRange(GameObject.FindGameObjectsWithTag("MiniBoss"));
            if (allBoss.Count > 0)
            {
                isBossSpawned = true;
            }
        }
    }

    void CheckBossDefeat()
    {
        // Kiểm tra tất cả các boss, loại bỏ các phần tử null
        bool allDefeated = true;
        foreach (var boss in allBoss)
        {

            if (boss != null) // Nếu boss chưa bị xóa, có nghĩa là nó vẫn tồn tại
            {
                allDefeated = false;
                break;  // Nếu tìm thấy bất kỳ boss nào còn sống, thoát khỏi vòng lặp
            }
        }

        if (allDefeated)
        {
            isBossDefeated = true;
        }
    }



    // Mở cửa nếu boss bị đánh bại
    void OpenBossRoomDoor()
    {

        GameObject bossRoom = GameObject.FindGameObjectWithTag("BossRoom");

        Transform[] children = bossRoom.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag("Door"))
            {
                DoorBossController doorTrigger = child.GetComponent<DoorBossController>();
                if (doorTrigger != null)
                {
                    doorTrigger.OpenDoor();
                }
            }
        }

    }
}
