using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneToGame : MonoBehaviour
{
	public void MoveToGameScene()
	{
		SceneManager.LoadScene("Kristian");
	}
}