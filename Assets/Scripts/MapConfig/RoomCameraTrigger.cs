using UnityEngine;
using Unity.Cinemachine;

public class RoomCameraTrigger : MonoBehaviour
{
    CinemachineCamera virtualCamera;

    void Awake()
    {
        virtualCamera = transform.parent.Find("CmCamera").GetComponent<CinemachineCamera>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // nếu như có tương tác với player 
        if (other.gameObject.tag == "Player")
        {
            EnableOnlyThisCamera();
        }
    }

    void EnableOnlyThisCamera()
    {
        CinemachineCamera[] allVCams = GameObject.FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        // đặt toàn bộ bằng 0
        foreach (var cam in allVCams)
        {
            cam.Priority = 0;
        }
        // chỉ có cái được đặt trong cùng một GameObject hay cùng 1 room 
        virtualCamera.Priority = 10;
    }
}
