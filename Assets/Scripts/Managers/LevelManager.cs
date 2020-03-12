using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField, NaughtyAttributes.ReorderableList] private List<string> levels;

    [SerializeField] private int levelOverrideIndex;
    [SerializeField] private bool levelOverride;

    [SerializeField] private bool designMode;

    private string currentLevel;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
