[System.Serializable]
public class PlayerData
{
    public int levelsCleared;
    public PlayerData(GameHandler data)
    {
        levelsCleared = data.levelsCleared;
    }
}
