using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Camera cam;
    Vector2 rawInput;
    [SerializeField] float speed = 10f;
    float delayPLayerMove = 0f;
    [SerializeField] GameObject playerImage;
    Shooter shooter;

    float maxSpeed = 10f;

    // Cài đặt thời gian cooldown cho việc bắn

    public static Player instance;

    void Awake()
    {
        KeepOldPlayer();
        // lấy chính đối tượng component của class này 
        shooter = GetComponent<Shooter>();

    }



    void KeepOldPlayer()
    {
        if (instance != null && instance != this)
        {
            // Cập nhật vị trí của instance cũ theo vị trí mới
            instance.transform.position = this.transform.position;
            instance.transform.rotation = this.transform.rotation;
            // cập nhật lại audio ở map mới 
            instance.gameObject.GetComponent<Health>().SetAudioPlayer(FindAnyObjectByType<AudioPlayer>());
            // Hủy player mới (this) vì instance đã có sẵn
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Update()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (delayPLayerMove <= 0)
        {
            Move();
        }
        else
        {
            delayPLayerMove -= Time.deltaTime;
        }
        LookAtMouse();
        // logic bắn
        if (Input.GetMouseButton(0))
        {
            shooter.SetAngle(playerImage.transform.rotation);
            shooter.isFiring = true;
        }
        else
        {
            shooter.isFiring = false;
        }
    }

    private void LookAtMouse()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(cam.transform.position.z);
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        float angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

        playerImage.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }


    void Move()
    {
        Vector3 delta = rawInput * speed * Time.deltaTime;
        Vector2 newPos = transform.position + delta;
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    public void SetDelayPlayerMove(float delay)
    {
        delayPLayerMove = delay;
    }

    public bool IncreaseSpeed(int value)
    {

        if (speed + value <= maxSpeed)
        {
            speed += value;
            return true;
        }
        return false;
    }

}
