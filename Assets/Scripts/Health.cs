using UnityEngine;
using System.Collections;



public class Health : MonoBehaviour
{
    SceneLoader sceneLoader;

    [SerializeField] float maxHealth = 50;
    [SerializeField] float health = 50;
    [SerializeField] int coin = 0;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool isPlayer = false;

    // hp tối đa chỉ có thể đạt tới 
    float maxHealthReach = 300;

    Vector3 positionHitEff;
    [SerializeField] bool wantChangePosHitEff = false;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
        positionHitEff = transform.position;
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

    }

    // Va chạm đạn hoặc quái loại rắn (cũng được để là trigger)
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Ammo")
        {

            DamageDealer damageDealer = other.GetComponent<DamageDealer>();

            if (damageDealer != null)
            {
                TakeDamage(damageDealer.getDamage());
            }
        }
        // là con rắn
        else if (other.tag == "MiniBoss")
        {
            TakeDamage(20);
            StartCoroutine(ResetVelocity(gameObject.GetComponent<Rigidbody2D>(), 0.2f));

        }

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        string thisTag = gameObject.tag;
        string otherTag = collision.gameObject.tag;

        // Chỉ xử lý nếu gameObject là Player hoặc Enemy
        if (thisTag != "Player" && thisTag != "Enemy") return;

        // Nếu va chạm với Enemy, Boss, hoặc MiniBoss thì xét tiếp
        if (otherTag == "Enemy" || otherTag == "Boss" || otherTag == "MiniBoss")
        {

            if (thisTag == "Player")
            {
                TakeDamage(20); // Player mất máu
            }
            else if (thisTag == "Enemy")
            {
                TakeDamage(health); // Enemy chết luôn
            }

            // Nếu đối tượng va chạm là Enemy, hủy đối tượng Enemy ngay lập tức
            if (otherTag == "Enemy")
            {
                Destroy(collision.gameObject); // Hủy đối tượng Enemy
            }
        }
    }


    public float GetHealth()
    {
        return health;
    }
    public void TakeDamage(float damage)
    {

        PlayHitEffect();
        audioPlayer.PlayDamageClip();
        health -= damage;

        // tại sao ko dùng trong update thì cái này nó ko cần kiểm tra quá nhiều nó chỉ cần kiểm tra mỗi khi máu thay đổi điều này giúp tối ưu hơn
        if (health <= 0)
        {
            // cho bằng 0 để hiển thị trên màn hình hp đã hết cho player
            health = 0;
            Die();
        }
    }
    void Die()
    {
        if (isPlayer)
        {
            sceneLoader.LoadGameOver();
            // scoreKepper.ModifyScore(score);
        }
        else
        {
            // levelManager.LoadGameOver();
            CoinManager.AddCoin(coin);
        }
        Destroy(gameObject);
    }
    void PlayHitEffect()
    {
        Vector3 pos = transform.position;
        if (wantChangePosHitEff)
        {
            pos = positionHitEff;
        }
        if (hitEffect != null)
        {
            // tạo đối tượng lên Herachy     
            ParticleSystem instance = Instantiate(hitEffect, pos, Quaternion.identity);
            // xóa đối tượng sau khoảng thời gian là thời gian nó tỏa các hạt + thời gian các hạt tồn tại
            // vd: tỏa trong 3s nhưng tồn tại trong 3s thì GameObject sẽ tồn tại 6s 
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }


    public void SetPositionHitEff(Vector3 pos)
    {
        positionHitEff = pos;
    }

    public bool IncreaseMaxHealth(int value)
    {
        if (maxHealth + value > maxHealthReach)
        {
            return false;
        }
        maxHealth = maxHealth + value;
        health += value;
        return true;
    }


    // để cho player sau khi load bằng singleton có thể load lại audio player
    public void SetAudioPlayer(AudioPlayer audioPlayer)
    {
        this.audioPlayer = audioPlayer;
    }



    public bool HealMaxHealth()
    {
        health = maxHealth;
        return true;
    }

    // để hiển thị hp ra màn hình 

    public string GetHealthInfor()
    {
        return health + "/" + maxHealth;
    }
    IEnumerator ResetVelocity(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector2.zero;
    }
}
