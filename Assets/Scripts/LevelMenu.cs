using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelParameters
{
    public string levelName;
    public string levelDescription;
    public string levelDificulty;
    public Level level;
}



public class LevelMenu : MonoBehaviour
{
    public List<LevelParameters> levelList = new();
    public Transform container;
    public LevelBlock blockPrefab;

    void Start()
    {
        foreach (var levelBlock in levelList)
        {
            var block = Instantiate(blockPrefab, container);
            block.Setup(levelBlock);
        }
    }
}
