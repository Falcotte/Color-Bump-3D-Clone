using UnityEngine;

#pragma warning disable 0649
[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 2)]
public class GameSettings : ScriptableObject {
    [Header("Velocity")]
    [SerializeField] private float minVelocity;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float deceleration;    //How fast does the player return to minVelocity

    [Header("Input")]
    [SerializeField] private float inputXSensitivity;
    [SerializeField] private float inputYSensitivity;

    public float MinVelocity {
        get { return PlayerPrefs.GetFloat("MinVelocity", minVelocity); }
        set { PlayerPrefs.SetFloat("MinVelocity", value); }
    }

    public float MaxVelocity {
        get { return PlayerPrefs.GetFloat("MaxVelocity", maxVelocity); }
        set { PlayerPrefs.SetFloat("MaxVelocity", value); }
    }

    public float Deceleration {
        get { return PlayerPrefs.GetFloat("Deceleration", deceleration); }
        set { PlayerPrefs.SetFloat("Deceleration", value); }
    }

    public float InputXSensitivity {
        get { return PlayerPrefs.GetFloat("InputXSensitivity", inputXSensitivity) / 10f; }
        set { PlayerPrefs.SetFloat("InputXSensitivity", value); }
    }

    public float InputYSensitivity {
        get { return PlayerPrefs.GetFloat("InputYSensitivity", inputYSensitivity) / 10f; }
        set { PlayerPrefs.SetFloat("InputYSensitivity", value); }
    }

    public void WriteToPlayerPrefs() {
        MinVelocity = minVelocity;
        Deceleration = deceleration;
        InputXSensitivity = inputXSensitivity;
        InputYSensitivity = inputYSensitivity;
    }
}
