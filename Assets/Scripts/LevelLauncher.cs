using System.Collections.Generic;
using UnityEngine;

public class LevelLauncher : MonoBehaviour
{
  [SerializeField] GameObject tapNote;

  public Level level;
  public List<Transform> columns = new();
  public List<Transform> notes = new();

	readonly float startPauseTime = 3f;
  readonly float speed = 5f;

  void Start()
  {
    foreach (Transform child in transform)
    {
      columns.Add(child);
    }

		foreach (NoteRow noteRow in level.noteRows)
    {
			float posZ = (startPauseTime + noteRow.time) * speed;

      foreach (Note note in noteRow.notes)
        {
          GameObject newNote = Instantiate(tapNote, new Vector3(0f, 0f, posZ), Quaternion.Euler(new Vector3(90f, 0f, 0f)));
          newNote.transform.parent = columns[note.column];
          newNote.transform.localPosition = new Vector3(0f, 0f, newNote.transform.localPosition.z);
          notes.Add(newNote.transform);
        }
    }
  }

  void FixedUpdate()
  {
    float step = speed * Time.deltaTime;
    Vector3 movement = step * new Vector3(0f, 0f, -1f);

    foreach (Transform note in notes)
    {
      note.position += movement;
    }
  }
}
