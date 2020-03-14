using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 0649
public class LevelManager : MonoSingleton<LevelManager> {
    [SerializeField, NaughtyAttributes.ReorderableList] private List<string> levels;

    [SerializeField] private int levelOverrideIndex;
    [SerializeField] private bool levelOverride;

    [SerializeField] private bool designMode;

    private string currentLevel;

    protected new void Awake() {
        base.Awake();
    }

    public string GetNextLevel() {
        if(levelOverride) {
            return levels[levelOverrideIndex];
        }
        return levels[Random.Range(0, levels.Count)];
    }

    public void LoadLevel(string name) {
        if(!designMode) {
            if(!string.IsNullOrWhiteSpace(currentLevel)) {
                SceneManager.UnloadSceneAsync(currentLevel);
            }

            currentLevel = name;
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }
    }

    public void UnloadLevel() {
        if(!string.IsNullOrWhiteSpace(currentLevel)) {
            SceneManager.UnloadSceneAsync(currentLevel);
        }

        currentLevel = null;
    }
}
