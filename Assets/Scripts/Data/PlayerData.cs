using UnityEngine;

[System.Serializable]
public static class PlayerData
{
    public static string playerName;
    public static int playerLevel;
    public static int playerMoney;
    public static Sprite playerAnimal;
    public static Rigidbody[] cars;
    public static Rigidbody currentCar;

    public static string GetName()
    {
        return playerName;
    }
}
