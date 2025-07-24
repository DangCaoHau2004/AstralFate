using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    // số enemy tối thiểu spawn 
    [SerializeField] int minEnemyInRoom = 3;
    [SerializeField] int maxEnemyInRoom = 6;

    // thời gian delay để spawn enemy 
    [SerializeField] float timeDelaySpawnEnemy = 1f;
    [SerializeField] bool isBossRoom = false;
    List<GameObject> allDoor;
    Player player;
    // lấy các điểm spawn enemy 
    List<GameObject> spawnEnemy;
    bool isSpawned = false;

    void Awake()
    {
        allDoor = FindSiblingsWithTag("Door");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // các điểm spawn 
        spawnEnemy = FindSiblingsWithTag("SpawnEnemy");
        // xáo trộn danh sách 
        System.Random random = new System.Random();
        spawnEnemy = spawnEnemy.OrderBy(x => random.Next()).ToList();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isSpawned)
        {

            // tạm dừng player ko cho di chuyen
            player.SetDelayPlayerMove(timeDelaySpawnEnemy / 2f);
            // dong cua thường
            if (!isBossRoom)
            {
                for (int i = 0; i < allDoor.Count; i++)
                {
                    allDoor[i].GetComponent<DoorController>().CloseDoor();
                }
            }

            // đóng cửa boss 

            else
            {
                allDoor[0].GetComponent<DoorBossController>().CloseDoor();
            }




            //nếu như nó bị ko hợp lệ thì nó sẽ bằng giá trị tối đa
            if (maxEnemyInRoom < minEnemyInRoom || maxEnemyInRoom > spawnEnemy.Count)
            {
                maxEnemyInRoom = spawnEnemy.Count;
            }
            int numberEnemy = Random.Range(minEnemyInRoom, maxEnemyInRoom);
            StartCoroutine(SpawnEnemiesWithDelay(numberEnemy));
        }
    }



    IEnumerator SpawnEnemiesWithDelay(int numberEnemy)
    {
        for (int i = 0; i < numberEnemy; i++)
        {
            spawnEnemy[i].GetComponent<SpawnEnemy>().Spawn(timeDelaySpawnEnemy, transform.parent, spawnEnemy[i].transform.position, spawnEnemy[i].transform.rotation);
        }

        // đảm bảo nó đã thực hiện xong cái spawn ở trên và delay bằng đúng khoảng đó thì mới chuyển true
        // bắt buộc phải vậy do logic mở cửa là nếu đã spawn và ko còn quái

        yield return new WaitForSeconds(timeDelaySpawnEnemy);

        isSpawned = true;
    }




    public bool GetIsSpawned()
    {
        return isSpawned;
    }

    // lặp lấy phần tử cùng cấp
    List<GameObject> FindSiblingsWithTag(string tag)
    {
        List<GameObject> result = new List<GameObject>();

        Transform parent = transform.parent;
        foreach (Transform sibling in parent)
        {
            if (sibling != transform && sibling.CompareTag(tag))
            {
                result.Add(sibling.gameObject);
            }
        }
        return result;
    }
}
