using UnityEngine;

[CreateAssetMenu(menuName = "Room Type", fileName = "New List Room")]
public class RoomType : ScriptableObject
{
    [Header("Enemy")]

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject endBottomRooms;
    public GameObject endTopRooms;
    public GameObject endLeftRooms;
    public GameObject endRightRooms;

    [Header("Trader")]

    public GameObject traderRoom;


    [Header("Boss")]
    public GameObject bossEndBottomRooms;
    public GameObject bossEndTopRooms;
    public GameObject bossEndLeftRooms;
    public GameObject bossEndRightRooms;

    [Header("MiniBoss")]

    public GameObject miniBossEndBottomRooms;
    public GameObject miniBossEndTopRooms;
    public GameObject miniBossEndLeftRooms;
    public GameObject miniBossEndRightRooms;
}
