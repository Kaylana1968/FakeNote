using System.Collections.Generic;
using UnityEngine;

public class LevelLauncher : MonoBehaviour
{
  [SerializeField] GameObject tapNote;

  public Level level;
  public List<Transform> columns = new();
  public List<Transform> notes = new();

  readonly float speed = 5f;

  float elapsedTime;
  int currentNoteIndex;

  void Start()
  {
    elapsedTime = 0;
    currentNoteIndex = 0;

    foreach (Transform child in transform)
    {
      columns.Add(child);
    }
  }

  void FixedUpdate()
  {
    elapsedTime += Time.deltaTime;

    if (currentNoteIndex < level.noteRows.Count)
    {
      while (level.noteRows[currentNoteIndex].time <= elapsedTime)
      {
        NoteRow noteRow = level.noteRows[currentNoteIndex];
        foreach (Note note in noteRow.notes)
        {
          GameObject newNote = Instantiate(tapNote, Vector3.zero, Quaternion.Euler(new Vector3(90f, 0f, 0f)));
          newNote.transform.parent = columns[note.column];
          newNote.transform.localPosition = new Vector3(0f, 0f, 20f);
          notes.Add(newNote.transform);
        }

        currentNoteIndex++;

        if (currentNoteIndex >= level.noteRows.Count) break;
      }
    }

    float step = speed * Time.deltaTime;
    Vector3 movement = step * new Vector3(0f, 0f, -1f);

    foreach (Transform note in notes)
    {
      note.position += movement;
    }
  }
}
