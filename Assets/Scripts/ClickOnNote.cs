using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ClickOnNote : MonoBehaviour
{
	[SerializeField] TMP_Text ScoreText;
	[SerializeField] GameObject container;
	[SerializeField] LevelLauncher levelLauncher;
	[SerializeField] ParticleSystem[] particleSystems;

	InputSystem_Actions inputs;
	List<InputAction> inputActions;
	List<Action<InputAction.CallbackContext>> canceledCallbacks;
	int score;

	void Awake()
	{
		inputs = new();
		inputs.Enable();

		inputActions = new()
		{
			inputs.Keyboard.A,
			inputs.Keyboard.Z,
			inputs.Keyboard.E,
			inputs.Keyboard.I,
			inputs.Keyboard.O,
			inputs.Keyboard.P
		};

		for (int i = 0; i < inputActions.Count; i++)
		{
			int localIndex = i;
			inputActions[localIndex].performed += context => Click(localIndex);
		}

		canceledCallbacks = new List<Action<InputAction.CallbackContext>>(new Action<InputAction.CallbackContext>[inputActions.Count]);
	}

	void CheckDistance(Transform note)
	{
		float distance = Mathf.Abs(note.position.z - transform.position.z);

		if (distance < 0.3)
		{
			Debug.Log("Perfect");
			score += 300;
		}
		else if (distance < 0.6)
		{
			Debug.Log("Good");
			score += 100;
		}
		else
		{
			Debug.Log("Miss");
			// container.SetActive(true);
			// Time.timeScale = 0f;
		}
	}

	void Click(int columnIndex)
	{
		Transform column = levelLauncher.columns[columnIndex];
		Transform firstNote = column.GetChild(0);

		NoteData noteData = firstNote.GetComponent<NoteData>();
		Debug.Log(noteData.isFake);

		ParticleSystem particleSystem = particleSystems[columnIndex];
		ParticleSystem.MainModule main = particleSystem.main;

		CheckDistance(firstNote);

		if (firstNote.childCount > 0)
		{
			main.loop = true;

			void Callback(InputAction.CallbackContext context)
			{
				Transform endNote = firstNote.GetChild(0);

				CheckDistance(endNote);

				levelLauncher.notes.Remove(firstNote);
				Destroy(firstNote.gameObject);

				particleSystem.Stop();

				inputActions[columnIndex].canceled -= canceledCallbacks[columnIndex];
			}

			canceledCallbacks[columnIndex] = Callback;
			inputActions[columnIndex].canceled += Callback;
		}
		else
		{
			main.loop = false;

			levelLauncher.notes.Remove(firstNote);
			Destroy(firstNote.gameObject);
		}
		particleSystem.Play();
	}

	private void Update()
	{
		ScoreText.text = "Score: " + score;
	}

	public void RetryButton()
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentSceneName);
		Debug.Log(currentSceneName);

	}
}
