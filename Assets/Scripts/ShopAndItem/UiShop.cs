using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiShop : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] int timeToDestroyNotification = 5;
    Transform container;
    Transform shopItem;
    Transform notification;
    Transform notificationTemplate;
    Transform player;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        container = transform.Find("Container");
        shopItem = container.Find("ShopItem");
        shopItem.gameObject.SetActive(false);

        notification = transform.Find("Notification");
        notificationTemplate = notification.Find("Template");
        notificationTemplate.gameObject.SetActive(false);

    }



    void Start()
    {
        CreateItemButton(Item.itemType.IncreaseAttack, "Tăng 5 damage", item.GetCost(Item.itemType.IncreaseAttack), 0);
        CreateItemButton(Item.itemType.IncreaseAttackSpeed, "Tăng 10% tốc độ bắn", item.GetCost(Item.itemType.IncreaseAttackSpeed), 1);
        CreateItemButton(Item.itemType.IncreaseMaxHealth, "Tăng 50hp tối đa", item.GetCost(Item.itemType.IncreaseMaxHealth), 2);
        CreateItemButton(Item.itemType.IncreaseSpeed, "Tăng 1 tốc độ chạy", item.GetCost(Item.itemType.IncreaseSpeed), 3);
        CreateItemButton(Item.itemType.HealHealth, "Hồi 100% hp", item.GetCost(Item.itemType.HealHealth), 4);
    }

    void CreateItemButton(Item.itemType itemType, string itemName, int itemCost, int positionIndex)
    {
        Sprite itemSprite = item.GetSprite(itemType);
        Transform shopItemTransform = Instantiate(shopItem, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 180f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        shopItemTransform.gameObject.SetActive(true);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("itemCost").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
        Button itemButton = shopItemTransform.GetComponent<Button>();
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(() => BuyItem(itemType, itemName));
        }

    }
    void BuyItem(Item.itemType itemType, string itemName)
    {
        int cost = item.GetCost(itemType);

        if (CoinManager.GetCoin() >= cost)
        {
            bool result = true;

            switch (itemType)
            {
                case Item.itemType.IncreaseAttack:
                    result = player.GetComponent<Shooter>().IncreaseAttack(5);
                    break;

                case Item.itemType.IncreaseAttackSpeed:
                    result = player.GetComponent<Shooter>().IncreaseAttackSpeed(10);
                    break;

                case Item.itemType.IncreaseMaxHealth:
                    result = player.GetComponent<Health>().IncreaseMaxHealth(50);
                    break;

                case Item.itemType.IncreaseSpeed:
                    result = player.GetComponent<Player>().IncreaseSpeed(1);
                    break;

                case Item.itemType.HealHealth:
                    result = player.GetComponent<Health>().HealMaxHealth();
                    break;
            }


            if (!result)
            {
                CreateNotification($"Đã đạt tối đa ko thể mua {itemName}");
                return;
            }

            // Trừ xu và báo mua thành công
            CoinManager.RemoveCoin(cost);
            CreateNotification($"Mua {itemName} thành công");
        }
        else
        {
            CreateNotification("Không đủ xu");
        }
    }

    public void ShowShopUi()
    {
        gameObject.SetActive(true);
    }
    public void HideShopUi()
    {
        gameObject.SetActive(false);
    }

    public void CreateNotification(String content)
    {
        // xóa nếu như có tồn tại thông báo trước đó 
        if (notification.childCount > 1)
        {
            Transform lastChild = notification.GetChild(notification.childCount - 1);

            Destroy(lastChild.gameObject);
        }
        Transform notificationTransform = Instantiate(notificationTemplate, notification);
        notificationTransform.Find("Content").GetComponent<TextMeshProUGUI>().SetText(content);
        notificationTransform.gameObject.SetActive(true);
        Destroy(notificationTransform.gameObject, timeToDestroyNotification);
    }

}
