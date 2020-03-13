using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 2)]
public class GameSettings : ScriptableObject {
    [Header("Velocity")]
    [SerializeField] private float minVelocity;

    [Header("Input")]
    [SerializeField] private float inputSensitivity;

    public float MinVelocity {
        get { return PlayerPrefs.GetFloat("MinVelocity", minVelocity); }
        set { PlayerPrefs.SetFloat("MinVelocity", value); }
    }

    public float InputSensitivity {
        get { return PlayerPrefs.GetFloat("InputSensitivity", inputSensitivity); }
        set { PlayerPrefs.SetFloat("InputSensitivity", value); }
    }

    public void WriteToPlayerPrefs() {
        MinVelocity = minVelocity;
        InputSensitivity = inputSensitivity;
    }
}
