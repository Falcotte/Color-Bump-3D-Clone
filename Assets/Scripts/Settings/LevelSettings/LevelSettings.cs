using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class LevelSettings : MonoSingleton<LevelSettings> {
    public static Level Level {
        get {
            if(Instance != null) {
                return Instance.level;
            }
            return null;
        }
    }
    public Level level;
}
