using UnityEngine;

public class DataManager : MonoSingleton<DataManager> {
    [Header("Player Data")]
    [SerializeField] private int level = 0;
    [SerializeField] private int vibration = 0;

    public int Level {
        get { return PlayerPrefs.GetInt("Level", level); }
        set { PlayerPrefs.SetInt("Level", value); }
    }

    public int Vibration {
        get { return PlayerPrefs.GetInt("Vibration", vibration); }
        set { PlayerPrefs.SetInt("Vibration", value); }
    }

    public void WriteToPlayerPrefs() {
        Level = level;
    }
}
