using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {
    private static T instance;
    public static T Instance {
        get {
            if(instance == null) {
                instance = FindObjectOfType<T>();
                if(instance == null) {
                    Debug.LogError($"Cannot find the instance of {typeof(T).FullName}");
                }
            }
            return instance;
        }
    }

    protected void Awake() {
        if(instance == null) {
            instance = this as T;
        }
        else {
            Debug.LogError($"Duplicate instance of {typeof(T).FullName}, destroying...");
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit() {
        instance = null;
    }
}
