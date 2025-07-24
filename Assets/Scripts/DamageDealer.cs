using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    float damage = 10;
    public float getDamage()
    {
        return damage;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag != "SpawnPoint" && collision.gameObject.tag != "DoorTrigger" && collision.gameObject.tag != "Ammo" && collision.gameObject.tag != "RoomCameraTrigger")
        {
            Destroy(gameObject);
        }

    }

}
