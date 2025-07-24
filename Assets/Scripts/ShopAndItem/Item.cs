using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _increaseAttack;
    [SerializeField] private Sprite _increaseAttackSpeed;
    [SerializeField] private Sprite _increaseMaxHealth;
    [SerializeField] private Sprite _increaseSpeed;
    [SerializeField] private Sprite _healHealth;

    public enum itemType
    {
        IncreaseAttack,
        IncreaseAttackSpeed,
        IncreaseMaxHealth,
        IncreaseSpeed,
        HealHealth,
    }

    public int GetCost(itemType type)
    {
        switch (type)
        {
            case itemType.IncreaseAttack:
            case itemType.IncreaseAttackSpeed:
            case itemType.IncreaseMaxHealth:
            case itemType.IncreaseSpeed:
                return 5;
            case itemType.HealHealth:
                return 20;
            default:
                return 0;
        }
    }

    public Sprite GetSprite(itemType type)
    {
        switch (type)
        {
            case itemType.IncreaseAttack:
                return _increaseAttack;
            case itemType.IncreaseAttackSpeed:
                return _increaseAttackSpeed;
            case itemType.IncreaseMaxHealth:
                return _increaseMaxHealth;
            case itemType.IncreaseSpeed:
                return _increaseSpeed;
            case itemType.HealHealth:
                return _healHealth;
            default:
                return null;
        }
    }
}
