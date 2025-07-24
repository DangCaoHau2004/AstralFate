using UnityEngine;

public class Trespasser : MonoBehaviour
{
    Health health;


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
                health.SetPositionHitEff(other.transform.position);
                health.TakeDamage(damageDealer.getDamage());

                // Hiển thị hiệu ứng tại vị trí viên đạn
            }
        }
    }


}
