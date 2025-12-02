using UnityEngine;

public class ClickOnNote : MonoBehaviour
{
	[SerializeField]
	LevelLauncher levelLauncher;
	private InputSystem_Actions inputs;

	private void Awake()
	{
		inputs = new InputSystem_Actions();

		inputs.Enable();

		inputs.Keyboard.A.performed += context =>
		{
			Debug.Log("A a été pressé");
			Transform column = levelLauncher.columns[0];
			Transform firstNote = column.GetChild(0);

			float distance = Mathf.Abs(firstNote.position.z - transform.position.z);

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

			levelLauncher.notes.Remove(firstNote);
			Destroy(firstNote.gameObject);

		};
	}
}
