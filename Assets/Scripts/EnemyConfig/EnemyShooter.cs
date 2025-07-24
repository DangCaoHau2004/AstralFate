using UnityEngine;
using System.Collections.Generic;
public class EnemyShooter : MonoBehaviour
{
    Transform player;


    // trong trường hơp boss là rắn thì nó ko tạo tiếng khi bắn do nó có thể di chuyển ra ngoài 
    // map khiến cho đạn vẫn được bắn và hiển thị tiếng khá khó chịu
    [SerializeField] bool muteSoundEffShoot = false;
    [SerializeField] float damage = 10f;
    [SerializeField] float bulletLifeTime = 5f;
    [SerializeField] bool haveShootAnimation = false;

    [SerializeField] float fireModeDelay = 1f;
    [SerializeField] Vector3 bulletSpawnTranForm;


    enum FireMode { Straight, Spiral, Spread }



    [SerializeField] List<FireMode> availableFireModes = new List<FireMode> { FireMode.Straight, FireMode.Spiral, FireMode.Spread };

    int currentFireModeIndex = 0;
    FireMode currentFireMode;

    [SerializeField] float switchModeInterval = 5f;
    float lastSwitchTime = 0f;

    [Header("Straight Bullet")]
    [SerializeField] GameObject enemyStraightBullet;
    // Tốc độ bay 
    [SerializeField] float straightSpeed = 5f;
    // thời gian tồn tại 
    // tốc độ bắn
    [SerializeField] float straightFireRate = 1f;

    [SerializeField] float offsetAngle = 0f;


    float lastFireTimeStraight = 0f;
    Quaternion straightAngle = Quaternion.Euler(0, 0, 0);


    [Header("Spiral Bullet")]
    [SerializeField] GameObject spiralBullet;
    [SerializeField] float spiralFireRate = 0.5f;
    [SerializeField] int angleStep = 5;
    [SerializeField] int spirealPerShot = 1;
    [SerializeField] float spiralSpeed = 5f;
    float lastFireTimeSpiral = 0f;

    float spiralAngle = 0f;





    [Header("Spread Bullet")]
    [SerializeField] GameObject spreadPrefab;
    [SerializeField] float spreadSpeed = 5f;
    [SerializeField] int spreadCount = 5; // Số lượng đạn
    [SerializeField] float spreadAngle = 45f; // Góc tỏa ra tổng cộng
    [SerializeField] float spreadFireRate = 1f;
    [SerializeField] bool spreadFollowPlayer = false;
    float lastFireTimeSpread = 0;


    AudioPlayer audioPlayer;

    EnemyAnimationController enemyAnimationController;

    void Awake()
    {
        enemyAnimationController = gameObject.GetComponent<EnemyAnimationController>();
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
    }

    void Start()
    {
        currentFireMode = availableFireModes[currentFireModeIndex];
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Update()
    {
        // Đổi chế độ sau mỗi interval
        if (Time.time >= lastSwitchTime + switchModeInterval)
        {
            SwitchFireMode();
            lastSwitchTime = Time.time;
        }
        // Chặn bắn nếu đang trong thời gian cooldown sau khi đổi mode
        if (Time.time < lastSwitchTime + fireModeDelay)
        {

            if (haveShootAnimation && enemyAnimationController != null)
            {
                enemyAnimationController.SetAttackAnimation(false);

            }
            return;
        }

        // Bắn theo chế độ hiện tại
        if (currentFireMode == FireMode.Straight)
        {
            StraightFire();
        }
        else if (currentFireMode == FireMode.Spiral)
        {

            SpiralFire();
        }
        else if (currentFireMode == FireMode.Spread)
        {

            SpreadFire();
        }
    }

    void StraightFire()
    {


        // Tính hướng bắn theo player
        Vector2 direction = player.position - transform.position;
        straightAngle = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offsetAngle);

        // Kiểm tra nếu đủ thời gian để bắn lại
        if (Time.time >= lastFireTimeStraight + straightFireRate && player != null)
        {
            if (!muteSoundEffShoot)
            {
                audioPlayer.PlayShootingClip();

            }

            // Nếu có animation bắn, kích hoạt animation
            if (haveShootAnimation && enemyAnimationController != null)
            {
                enemyAnimationController.SetAttackAnimation(true);
            }

            // Tạo đạn tại vị trí của đối tượng hiện tại
            GameObject instance = Instantiate(enemyStraightBullet, transform.TransformPoint(bulletSpawnTranForm), straightAngle);

            // Gán damage cho đạn
            instance.GetComponent<DamageDealer>().SetDamage(damage);

            // Lấy Rigidbody2D và thiết lập vận tốc
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            rb.linearVelocity = instance.transform.up * straightSpeed;

            // Hủy đạn sau thời gian sống
            Destroy(instance, bulletLifeTime);

            // Cập nhật thời gian lần bắn
            lastFireTimeStraight = Time.time;
        }
    }

    void SpiralFire()
    {
        if (Time.time >= lastFireTimeSpiral + spiralFireRate && player != null)
        {
            if (!muteSoundEffShoot)
            {
                audioPlayer.PlayShootingClip();

            }

            if (haveShootAnimation && enemyAnimationController != null)
            {
                enemyAnimationController.SetAttackAnimation(true);

            }
            for (int i = 0; i < spirealPerShot; i++)
            {
                float currentAngle = spiralAngle + (360f / spirealPerShot) * i;
                float rad = currentAngle * Mathf.Deg2Rad;

                Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                GameObject bullet = Instantiate(spiralBullet, transform.TransformPoint(bulletSpawnTranForm), Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * spiralSpeed;
                bullet.GetComponent<DamageDealer>().SetDamage(damage);
                Destroy(bullet, bulletLifeTime);
            }

            lastFireTimeSpiral = Time.time;
            spiralAngle += angleStep;
            spiralAngle %= 360f;
        }
    }



    void SpreadFire()
    {
        if (Time.time >= lastFireTimeSpread + spreadFireRate && player != null)
        {
            if (!muteSoundEffShoot)
            {
                audioPlayer.PlayShootingClip();

            }

            if (haveShootAnimation && enemyAnimationController != null)
            {
                enemyAnimationController.SetAttackAnimation(true);

            }
            float angleStep = spreadAngle / (spreadCount - 1);
            float startAngle = spreadAngle / 2;

            for (int i = 0; i < spreadCount; i++)
            {
                float angle = startAngle - angleStep * i;

                // Góc bắn tính theo hướng nhìn của enemy (transform.up)
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Vector2 baseDirection = (player.transform.position - transform.position).normalized;
                Vector2 direction = spreadFollowPlayer ? (rotation * baseDirection) : (rotation * transform.up);

                GameObject bullet = Instantiate(spreadPrefab, transform.TransformPoint(bulletSpawnTranForm), Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = direction.normalized * spreadSpeed;
                Destroy(bullet, bulletLifeTime);

            }

            lastFireTimeSpread = Time.time;
        }
    }



    void SwitchFireMode()
    {
        currentFireModeIndex = (currentFireModeIndex + 1) % availableFireModes.Count;
        currentFireMode = availableFireModes[currentFireModeIndex];
    }

}
