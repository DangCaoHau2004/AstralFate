using UnityEngine;

public class ShowShopUi : MonoBehaviour
{
    UiShop uiShop;
    void Awake()
    {
        uiShop = GameObject.FindGameObjectWithTag("UiShop").GetComponent<UiShop>();
        uiShop.HideShopUi();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            uiShop.ShowShopUi();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // tại sao lại cần Player ở đây thì là do kể cả ta có dùng chuột để click ra ngoài thì cũng bị destroy nếu ko có tag
        if (collision.gameObject.tag == "Player")
        {

            uiShop.HideShopUi();

        }
    }

}
