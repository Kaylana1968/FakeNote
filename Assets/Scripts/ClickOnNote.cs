using UnityEngine;

public class ClickOnNote : MonoBehaviour
{
	[SerializeField] LevelLauncher levelLauncher;
    [SerializeField] ParticleSystem[] particleSystems;

	private InputSystem_Actions inputs;

	private void Awake()
	{
		inputs = new InputSystem_Actions();

		inputs.Enable();

		inputs.Keyboard.A.performed += context =>
		{
			Click(0);
		};

        inputs.Keyboard.Z.performed += context =>
		{
			Click(1);
		};

        inputs.Keyboard.E.performed += context =>
		{
			Click(2);
		};

        inputs.Keyboard.I.performed += context =>
		{
			Click(3);
		};

        inputs.Keyboard.O.performed += context =>
		{
			Click(4);
		};

        inputs.Keyboard.P.performed += context =>
		{
			Click(5);
		};
	}

    private void Click(int columnIndex)
    {
        Transform column = levelLauncher.columns[columnIndex];
        Transform firstNote = column.GetChild(0);

        float distance = Mathf.Abs(firstNote.position.z - transform.position.z);

        particleSystems[columnIndex].Play();

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
    }
}
