using UnityEngine;

public class Shooter : MonoBehaviour
{
    // GameObject đạn 
    [SerializeField] GameObject projectilePrefabs;
    // Tốc độ bay 
    [SerializeField] float projectileSpeed = 10f;
    // thời gian tồn tại 
    [SerializeField] float projectileLifeTime = 5f;

    // tốc độ bắn
    [SerializeField] float fireRate = 1f;
    [SerializeField] float damage = 10f;

    Quaternion angle = Quaternion.Euler(0, 0, 0);
    public bool isFiring;
    AudioPlayer audioPlayer;
    float lastFireTime = 0f;


    float maxFireRate = 0.1f;


    float maxDAMAGE = 50f;

    void Awake()
    {
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring)
        {
            if (Time.time >= lastFireTime + fireRate)
            {
                audioPlayer.PlayShootingClip();
                GameObject instance = Instantiate(projectilePrefabs, transform.position, angle);
                // gán damage cho đạn
                instance.GetComponent<DamageDealer>().SetDamage(damage);
                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = -instance.transform.up * projectileSpeed;
                }

                Destroy(instance, projectileLifeTime);
                lastFireTime = Time.time;
            }

        }
    }



    public float GetFiringRate()
    {
        return fireRate;
    }

    public void SetAngle(Quaternion angle)
    {
        this.angle = angle;
    }

    public bool IncreaseAttackSpeed(int value)
    {
        float decreaseValue = (value / 100f) * fireRate;
        if (fireRate - decreaseValue > maxFireRate)
        {
            fireRate -= decreaseValue;
            return true;
        }
        return false;
    }

    public bool IncreaseAttack(int value)
    {
        if (damage + value > maxDAMAGE)
        {
            return false;
        }

        damage += value;
        return true;
    }

}
