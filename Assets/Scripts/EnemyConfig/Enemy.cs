using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float stopingDistance = 20;
    [SerializeField] float retreatDistance = 10;

    [SerializeField] float offsetAngle = 0f;

    [SerializeField] bool canChangeDirection = true;

    [SerializeField] bool haveMoveAnimation = false;

    // để đảm bảo rằng nó có animation di chuyển chứ ko phải chỉ có animation bắn, ... 

    EnemyAnimationController enemyAnimationController;

    Transform player;

    void Awake()
    {
        enemyAnimationController = gameObject.GetComponent<EnemyAnimationController>();
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (haveMoveAnimation && enemyAnimationController != null)
            {
                enemyAnimationController.SetMoveAnimation(true);

            }
        }
        else if (distance < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            if (haveMoveAnimation && enemyAnimationController != null)
            {
                enemyAnimationController.SetMoveAnimation(true);

            }
        }
        else
        {
            if (haveMoveAnimation && enemyAnimationController != null)
            {
                enemyAnimationController.SetMoveAnimation(false);
            }
        }


        if (canChangeDirection)
        {
            // Xoay về phía player
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + offsetAngle);
        }
    }

}
