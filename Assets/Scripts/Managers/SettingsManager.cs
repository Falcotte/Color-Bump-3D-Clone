using UnityEngine;

public class SettingsManager : MonoSingleton<SettingsManager> {
    public static GameSettings GameSettings {
        get {
            return Instance.gameSettingsPresets[Instance.GameSettingsIndex];
        }
    }

    public GameSettings[] gameSettingsPresets;

    [SerializeField] private int gameSettingsIndex;

    public int GameSettingsIndex {
        get { return PlayerPrefs.GetInt("GameSettingsIndex", 0); }
        set {
            PlayerPrefs.SetInt("GameSettingsIndex", value);
        }
    }
}
