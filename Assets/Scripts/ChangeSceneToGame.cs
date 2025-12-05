using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneToGame : MonoBehaviour
{
	public void MoveToGameScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
}