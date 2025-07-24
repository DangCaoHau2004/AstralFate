using UnityEngine;
using System.Collections.Generic;

public class EnemySnakeMover : MonoBehaviour
{
    [SerializeField] GameObject headPrefab;
    [SerializeField] GameObject bodyPrefab;
    [SerializeField] GameObject tailPrefab;
    [SerializeField] int totalLength = 5;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float followSpeed = 10f;
    [SerializeField] float segmentSpacing = 0.5f;

    [SerializeField] List<WayPoints> wayConfig;

    List<Transform> segments = new List<Transform>();
    List<GameObject> waypoint = new List<GameObject>();
    int wayConfigIndex = 0;
    int waypointIndex = 0;

    void Start()
    {
        // Lấy danh sách waypoint từ WayPoints ScriptableObject
        waypoint = wayConfig[wayConfigIndex].GetWaypoints();

        // Tạo đầu rắn tại localPosition của waypoint đầu tiên
        Vector3 spawnPosition = waypoint[0].transform.localPosition;
        GameObject head = Instantiate(headPrefab, Vector3.zero, Quaternion.identity, transform);
        head.transform.localPosition = spawnPosition;
        segments.Add(head.transform);

        // Tạo thân rắn
        for (int i = 0; i < totalLength - 2; i++)
        {
            GameObject body = Instantiate(bodyPrefab, spawnPosition, Quaternion.identity, transform);
            body.transform.localPosition = spawnPosition;
            segments.Add(body.transform);
        }

        // Tạo đuôi rắn
        GameObject tail = Instantiate(tailPrefab, spawnPosition, Quaternion.identity, transform);
        tail.transform.localPosition = spawnPosition;
        segments.Add(tail.transform);
    }

    void Update()
    {
        // Nếu đi hết waypoint -> chọn waypoint mới
        if (waypointIndex >= waypoint.Count)
        {
            wayConfigIndex = Random.Range(0, wayConfig.Count);
            waypoint = wayConfig[wayConfigIndex].GetWaypoints();
            waypointIndex = 0;
        }

        Transform head = segments[0];
        Vector3 target = waypoint[waypointIndex].transform.localPosition;
        Vector3 headPos = head.localPosition;

        head.localPosition = Vector3.MoveTowards(headPos, target, moveSpeed * Time.deltaTime);

        Vector3 direction = target - head.localPosition;
        if (direction.magnitude > 0.01f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            head.localRotation = Quaternion.Euler(0, 0, angle - 90f);
        }

        if (Vector3.Distance(head.localPosition, target) < 0.01f)
        {
            waypointIndex++;
        }

        // Di chuyển từng đoạn thân theo localPosition
        for (int i = 1; i < segments.Count; i++)
        {
            Transform currentSegment = segments[i];
            Vector3 currentPos = currentSegment.localPosition;
            Vector3 targetPos = segments[i - 1].localPosition;

            Vector3 directionToTarget = targetPos - currentPos;
            float dist = directionToTarget.magnitude;

            if (dist > segmentSpacing)
            {
                Vector3 moveDir = directionToTarget.normalized;
                currentSegment.localPosition += moveDir * followSpeed * Time.deltaTime;

                if (moveDir.magnitude > 0)
                {
                    float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
                    currentSegment.localRotation = Quaternion.Euler(0, 0, angle - 90f);
                }
            }
        }
    }
}
