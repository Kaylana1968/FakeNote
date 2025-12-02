using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickOnNote : MonoBehaviour
{
	[SerializeField] LevelLauncher levelLauncher;
	[SerializeField] ParticleSystem[] particleSystems;

	InputSystem_Actions inputs;
	List<InputAction> inputActions;
	List<Action<InputAction.CallbackContext>> canceledCallbacks;

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
		}
		else if (distance < 0.6)
		{
			Debug.Log("Good");
		}
		else
		{
			Debug.Log("Miss");
		}
  }

	void Click(int columnIndex)
	{
		Transform column = levelLauncher.columns[columnIndex];
		Transform firstNote = column.GetChild(0);

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
}
