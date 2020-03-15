using UnityEngine;

public class DataManager : MonoSingleton<DataManager> {
    [Header("Player Data")]
    [SerializeField] private int level = 0;

    public int Level {
        get { return PlayerPrefs.GetInt("Level", level); }
        set { PlayerPrefs.SetInt("Level", value); }
    }

    public void WriteToPlayerPrefs() {
        Level = level;
    }
}
