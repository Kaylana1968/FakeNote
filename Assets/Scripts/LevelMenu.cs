using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public string[] sceneNames;
    public Transform container;
    public LevelBlock blockPrefab;

    void Start()
    {
        foreach (var sceneName in sceneNames)
        {
            var block = Instantiate(blockPrefab, container);
            block.Setup(sceneName);
        }
    }
}
