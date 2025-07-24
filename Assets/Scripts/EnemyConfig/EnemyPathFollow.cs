using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPathFollow : MonoBehaviour
{
    [SerializeField] List<WayPoints> wayConfig;
    [SerializeField] float delay = 0f;     // Độ trễ sau mỗi waypoint
    [SerializeField] float moveSpeed = 5f; // Tốc độ di chuyển
    [SerializeField] bool haveMoveAnimation = true;

    List<GameObject> waypoint;

    int wayConfigIndex = 0;
    int waypointIndex = 0;

    EnemyAnimationController enemyAnimationController;

    void Start()
    {

        waypoint = wayConfig[wayConfigIndex].GetWaypoints();

        transform.localPosition = waypoint[waypointIndex].transform.localPosition;

        if (haveMoveAnimation)
        {
            enemyAnimationController = GetComponent<EnemyAnimationController>();
        }

        StartCoroutine(MoveToWayPoint());
    }

    IEnumerator MoveToWayPoint()
    {
        while (true)
        {
            if (waypointIndex < waypoint.Count)
            {
                Vector3 targetPosition = waypoint[waypointIndex].transform.localPosition;
                while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
                {
                    if (haveMoveAnimation && enemyAnimationController != null)
                    {
                        enemyAnimationController.SetMoveAnimation(true);
                    }

                    float delta = moveSpeed * Time.deltaTime;
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, delta);
                    yield return null;
                }

                transform.localPosition = targetPosition;

                if (haveMoveAnimation && enemyAnimationController != null)
                {
                    enemyAnimationController.SetMoveAnimation(false);
                }

                waypointIndex++;
                yield return new WaitForSeconds(delay);
            }
            else
            {
                waypoint = wayConfig[Random.Range(0, wayConfig.Count)].GetWaypoints();
                waypointIndex = 0;
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
