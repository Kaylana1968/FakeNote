using System;
using System.Collections.Generic;
using UnityEngine;



public class SecretLevelMenu : MonoBehaviour
{
	public List<LevelParameters> levelList = new();
	public Transform container;
	public SecretLevelBlock blockPrefab;

	void Start()
	{
		foreach (var levelBlock in levelList)
		{
			var block = Instantiate(blockPrefab, container);
			block.Setup(levelBlock);
		}
	}
}
