using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] int openingDirection;
    // 1 là cần cửa dưới
    // 2 là cần cửa trên
    // 3 là cần cửa trái
    // 4 là cần cửa phải
    [SerializeField] RoomType rooms;
    int rand;
    bool spawned = false;

    GameObject allRooms;
    int roomCount;
    int minRoom = 5;
    // khai báo private để ko thể sửa trên unity 
    public static bool haveRoomBoss = false;
    public static bool haveRoomMiniBoss = false;
    public static bool haveRoomTrader = false;


    void Start()
    {
        allRooms = GameObject.FindGameObjectWithTag("Rooms");
        Invoke("Spawn", 0.5f);

    }

    void Spawn()
    {
        roomCount = allRooms.transform.childCount;

        if (roomCount >= minRoom)
        {
            if (!spawned) // Kiểm tra nếu phòng chưa được spawn và chưa có phòng nối
            {
                if (openingDirection == 1)
                {
                    if (!haveRoomBoss)
                    {
                        GameObject room = Instantiate(rooms.bossEndBottomRooms, transform.position, rooms.bossEndBottomRooms.transform.rotation);
                        room.transform.parent = allRooms.transform;
                        haveRoomBoss = true;

                    }
                    else
                    {
                        GameObject room = Instantiate(rooms.endBottomRooms, transform.position, rooms.endBottomRooms.transform.rotation);
                        room.transform.parent = allRooms.transform;
                    }

                }
                else if (openingDirection == 2)
                {

                    if (!haveRoomBoss)
                    {
                        GameObject room = Instantiate(rooms.bossEndTopRooms, transform.position, rooms.bossEndTopRooms.transform.rotation);
                        room.transform.parent = allRooms.transform;
                        haveRoomBoss = true;
                    }
                    else
                    {
                        GameObject room = Instantiate(rooms.endTopRooms, transform.position, rooms.endTopRooms.transform.rotation);
                        room.transform.parent = allRooms.transform;
                    }
                }
                else if (openingDirection == 3)
                {

                    if (!haveRoomBoss)
                    {
                        GameObject room = Instantiate(rooms.bossEndLeftRooms, transform.position, rooms.bossEndLeftRooms.transform.rotation);
                        room.transform.parent = allRooms.transform;
                        haveRoomBoss = true;
                    }
                    else
                    {
                        GameObject room = Instantiate(rooms.endLeftRooms, transform.position, rooms.endLeftRooms.transform.rotation);
                        room.transform.parent = allRooms.transform;
                    }


                }
                else if (openingDirection == 4)
                {

                    if (!haveRoomBoss)
                    {
                        GameObject room = Instantiate(rooms.bossEndRightRooms, transform.position, rooms.bossEndRightRooms.transform.rotation);
                        room.transform.parent = allRooms.transform;
                        haveRoomBoss = true;
                    }
                    else
                    {
                        GameObject room = Instantiate(rooms.endRightRooms, transform.position, rooms.endRightRooms.transform.rotation);
                        room.transform.parent = allRooms.transform;
                    }

                }
                spawned = true;
            }
        }
        else
        {
            if (!spawned) // Kiểm tra nếu phòng chưa được spawn và chưa có phòng nối
            {
                if (openingDirection == 1)
                {

                    if (2 < roomCount && roomCount < minRoom && (!haveRoomTrader || !haveRoomMiniBoss))
                    {
                        if (!haveRoomTrader)
                        {
                            GameObject room = Instantiate(rooms.traderRoom, transform.position, rooms.traderRoom.transform.rotation);
                            room.transform.parent = allRooms.transform;
                            haveRoomTrader = true;
                        }
                        else if (!haveRoomMiniBoss)
                        {
                            GameObject room = Instantiate(rooms.miniBossEndBottomRooms, transform.position, rooms.miniBossEndBottomRooms.transform.rotation);
                            room.transform.parent = allRooms.transform;
                            haveRoomMiniBoss = true;
                        }
                    }
                    else
                    {
                        rand = Random.Range(0, rooms.bottomRooms.Length);
                        GameObject room = Instantiate(rooms.bottomRooms[rand], transform.position, rooms.bottomRooms[rand].transform.rotation);
                        room.transform.parent = allRooms.transform;
                    }
                }
                else if (openingDirection == 2)
                {


                    if (2 < roomCount && roomCount < minRoom && (!haveRoomTrader || !haveRoomMiniBoss))
                    {
                        if (!haveRoomTrader)
                        {
                            GameObject room = Instantiate(rooms.traderRoom, transform.position, rooms.traderRoom.transform.rotation);
                            room.transform.parent = allRooms.transform;
                            haveRoomTrader = true;
                        }
                        else if (!haveRoomMiniBoss)
                        {
                            GameObject room = Instantiate(rooms.miniBossEndTopRooms, transform.position, rooms.miniBossEndTopRooms.transform.rotation);
                            room.transform.parent = allRooms.transform;
                            haveRoomMiniBoss = true;
                        }
                    }
                    else
                    {
                        rand = Random.Range(0, rooms.topRooms.Length);
                        GameObject room = Instantiate(rooms.topRooms[rand], transform.position, rooms.topRooms[rand].transform.rotation);
                        room.transform.parent = allRooms.transform;
                    }
                }
                else if (openingDirection == 3)
                {

                    if (2 < roomCount && roomCount < minRoom && (!haveRoomTrader || !haveRoomMiniBoss))
                    {
                        if (!haveRoomTrader)
                        {
                            GameObject room = Instantiate(rooms.traderRoom, transform.position, rooms.traderRoom.transform.rotation);
                            room.transform.parent = allRooms.transform;
                            haveRoomTrader = true;
                        }
                        else if (!haveRoomMiniBoss)
                        {
                            GameObject room = Instantiate(rooms.miniBossEndLeftRooms, transform.position, rooms.miniBossEndLeftRooms.transform.rotation);
                            room.transform.parent = allRooms.transform;
                            haveRoomMiniBoss = true;
                        }
                    }
                    else
                    {
                        rand = Random.Range(0, rooms.leftRooms.Length);
                        GameObject room = Instantiate(rooms.leftRooms[rand], transform.position, rooms.leftRooms[rand].transform.rotation);
                        room.transform.parent = allRooms.transform;
                    }

                }
                else if (openingDirection == 4)
                {

                    if (2 < roomCount && roomCount < minRoom && (!haveRoomTrader || !haveRoomMiniBoss))
                    {
                        if (!haveRoomTrader)
                        {
                            GameObject room = Instantiate(rooms.traderRoom, transform.position, rooms.traderRoom.transform.rotation);
                            room.transform.parent = allRooms.transform;
                            haveRoomTrader = true;
                        }
                        else if (!haveRoomMiniBoss)
                        {
                            GameObject room = Instantiate(rooms.miniBossEndRightRooms, transform.position, rooms.miniBossEndRightRooms.transform.rotation);
                            room.transform.parent = allRooms.transform;
                            haveRoomMiniBoss = true;
                        }
                    }
                    else
                    {
                        rand = Random.Range(0, rooms.rightRooms.Length);
                        GameObject room = Instantiate(rooms.rightRooms[rand], transform.position, rooms.rightRooms[rand].transform.rotation);
                        room.transform.parent = allRooms.transform;
                    }
                }
                spawned = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {

            RoomSpawner otherSpawner = other.GetComponent<RoomSpawner>();

            if (otherSpawner != null && !otherSpawner.spawned && !spawned)
            {

                if (gameObject.name == "Left" || gameObject.name == "Right")
                {
                    Destroy(gameObject);
                }
            }
            // Nếu cái kia đã spawn thì mình bị hủy
            else if (otherSpawner == null || otherSpawner.spawned)
            {
                Destroy(gameObject);
            }
        }

    }

}
