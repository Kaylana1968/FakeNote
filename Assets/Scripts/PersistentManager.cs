using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Object.DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
        Instance = null;
    }
}
