
public class CoinManager
{
    static int coin = 0;
    public static int GetCoin() => coin;
    public static void AddCoin(int value) => coin += value;
    public static void RemoveCoin(int value) => coin -= value;
}
