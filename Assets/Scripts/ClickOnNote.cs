using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using NUnit.Framework;

public class ClickOnNote : MonoBehaviour
{
	[SerializeField] TMP_Text ScoreText;
	[SerializeField] TMP_Text ComboText;
	[SerializeField] LevelLauncher levelLauncher;
	[SerializeField] EndMenu endMenu;
	[SerializeField] ParticleSystem[] particleSystems;
    [SerializeField] Transform validationBar;


	InputSystem_Actions inputs;
	List<InputAction> inputActions;
	List<Action<InputAction.CallbackContext>> canceledCallbacks;
	int score;
	int perfect;
	int good;
	int miss;
	int combo;
	int bestCombo;

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

	int CheckDistance(Transform note)
	{
       
		float distance = Mathf.Abs(note.position.z - transform.position.z);

        if (distance >= 0.75)
        {
			miss +=1;
			if (combo > bestCombo)
            {
                bestCombo = combo;
            }
			combo = 0;
            return -1;
        }
        else if (distance < 0.3)
        {
			perfect += 1;
			combo +=1 ;
            return 3;
        }
        else if (distance < 0.6)
        {
			good +=1;
			if (combo > bestCombo)
            {
                bestCombo = combo;
            }
			combo = 0;
            return 1;
        }
        else
        {
			miss +=1;
			if (combo > bestCombo)
            {
                bestCombo = combo;
            }
			combo = 0;
            return 0;
            // container.SetActive(true);
            // Time.timeScale = 0f;
        }
            
	}

	void Click(int columnIndex)
	{
		Transform column = levelLauncher.columns[columnIndex];
		if(column.childCount > 0){
			Transform firstNote = column.GetChild(0);

			ParticleSystem particleSystem = particleSystems[columnIndex];
			ParticleSystem.MainModule main = particleSystem.main;

			int scoreCalc ;

			scoreCalc = CheckDistance(firstNote);

			if (firstNote == null)
			{
				Debug.Log("Coucou");
			}
			if (firstNote.childCount > 0)
			{
				if (scoreCalc >= 0){
					main.loop = true;
				}

				void Callback(InputAction.CallbackContext context)
				{
					Transform endNote = firstNote.GetChild(0);

					scoreCalc = CheckDistance(endNote);
					if (scoreCalc >= 0){
						levelLauncher.notes.Remove(firstNote);
						Destroy(firstNote.gameObject);

					}
						particleSystem.Stop();

						inputActions[columnIndex].canceled -= canceledCallbacks[columnIndex];
				}

				canceledCallbacks[columnIndex] = Callback;
				inputActions[columnIndex].canceled += Callback;
			}
			else
			{
				if (scoreCalc >= 0){
					main.loop = false;
					levelLauncher.notes.Remove(firstNote);
					Destroy(firstNote.gameObject);
				}
			}
			NoteData noteData = firstNote.GetComponent<NoteData>();
			if (scoreCalc >= 0)
			{
				IsFake(noteData, scoreCalc * 100);
				particleSystem.Play();
			}
		}
	}

	private void Update()
	{
		ScoreText.text = "Score: " + score;
		ComboText.text = "Combo: " + combo;
        DestroyNotes(0);
        DestroyNotes(1);
        DestroyNotes(2);
        DestroyNotes(3);
        DestroyNotes(4);
        DestroyNotes(5);
		if (levelLauncher.notes.Count == 0)
        {
        	endMenu.DisplayEndMenu(combo, bestCombo, score, perfect, good, miss);
        }
		
		
	}
	

    public void IsFake(NoteData noteData, int rank)
    {
        if (noteData.isFake)
        {
            score -= 500;
            if (score < 0) score = 0;
        }else
        {
            score += rank;
        }
    }

    void DestroyNotes(int columnIndex)
    {
        foreach (Transform note in levelLauncher.columns[columnIndex])
        {
            if (note.childCount > 0)
            {
                Transform endNote = note.GetChild(0);
                float dotEnd = Vector3.Dot((endNote.position - validationBar.position).normalized, validationBar.forward);
                if (dotEnd < 0)
                {
					miss +=1;
					if (combo > bestCombo)
					{
						bestCombo = combo;
					}
					combo = 0;
                    levelLauncher.notes.Remove(note);
                    Destroy(note.gameObject);
                }
            }else
            {
                float dot = Vector3.Dot((note.position - validationBar.position).normalized, validationBar.forward);
                if (dot < 0){

					miss +=1;
					if (combo > bestCombo)
					{
						bestCombo = combo;
					}
					combo = 0;
                    levelLauncher.notes.Remove(note);
                    Destroy(note.gameObject);
                }
            }
        }
    }
}
