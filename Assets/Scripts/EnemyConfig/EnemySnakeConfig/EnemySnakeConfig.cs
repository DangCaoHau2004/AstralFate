using UnityEngine;

public class EnemySnakeConfig : MonoBehaviour
{
    Health health;

    [SerializeField] ParticleSystem hitEffect;

    void Start()
    {
        health = transform.parent.gameObject.GetComponent<Health>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Ammo"))
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();

            if (damageDealer != null)
            {
                // tạo đối tượng lên Herachy và tạo hiệu ứng tại vị trí đạn    
                ParticleSystem instance = Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
                // xóa đối tượng sau khoảng thời gian là thời gian nó tỏa các hạt + thời gian các hạt tồn tại
                // vd: tỏa trong 3s nhưng tồn tại trong 3s thì GameObject sẽ tồn tại 6s 
                Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
                health.TakeDamage(damageDealer.getDamage());
            }
        }
        else if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            Vector2 pushDirection = (other.transform.position - transform.position).normalized;

            playerRb.AddForce(pushDirection * 2f, ForceMode2D.Impulse);

            ParticleSystem instance = Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);

        }
    }


}
