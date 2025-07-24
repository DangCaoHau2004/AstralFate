using UnityEngine;
using TMPro;

public class UpdatePlayerStatus : MonoBehaviour
{
    Health health;
    Transform coinText;
    Transform healthText;
    void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        coinText = transform.Find("BackGround").Find("Coin").Find("Value");
        healthText = transform.Find("BackGround").Find("Hp").Find("Value");

    }


    void Update()
    {
        coinText.GetComponent<TextMeshProUGUI>().text = CoinManager.GetCoin().ToString();
        healthText.GetComponent<TextMeshProUGUI>().text = health.GetHealthInfor();
    }
}
