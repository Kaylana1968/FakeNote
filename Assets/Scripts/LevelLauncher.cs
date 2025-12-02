using System.Collections.Generic;
using UnityEngine;

public class LevelLauncher : MonoBehaviour
{
  [SerializeField] GameObject tapNote;
  [SerializeField] GameObject noteTrail;

  public Level level;
  public List<Transform> columns = new();
  public List<Transform> notes = new();

	readonly float startPauseTime = 3f;
  readonly float speed = 20f;
  readonly List<Color> colors = new()
  {
    new Color(0.7f, 0f, 0f),
    new Color(0f, 0.7f, 0f),
    new Color(0f, 0f, 0.7f),
    new Color(0.7f, 0.7f, 0f),
    new Color(0f, 0.7f, 0.7f),
    new Color(0.7f, 0f, 0.7f)
  };
  readonly Quaternion angle = Quaternion.Euler(new Vector3(90f, 0f, 0f));

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
				GameObject newNote = Instantiate(tapNote, new Vector3(0f, 0f, posZ), angle);
				newNote.transform.parent = columns[note.column];
				newNote.transform.localPosition = new Vector3(0f, 0f, newNote.transform.localPosition.z);
				notes.Add(newNote.transform);

        Color color = colors[note.column];
        newNote.GetComponent<SpriteRenderer>().color = color;

        if (note.type == NoteType.hold)
        {
          float endPosZ = (startPauseTime + note.endTime) * speed;

				  GameObject endNote = Instantiate(tapNote, new Vector3(0f, 0f, endPosZ), angle);
				  endNote.transform.parent = newNote.transform;
          endNote.transform.localPosition = new Vector3(0f, endNote.transform.localPosition.y, 0f);

          GameObject trail = Instantiate(noteTrail, Vector3.zero, angle);
          trail.transform.parent = newNote.transform;
          trail.transform.position = Vector3.Lerp(newNote.transform.position, endNote.transform.position, 0.5f);
          trail.transform.position = new Vector3(trail.transform.position.x, -0.001f, trail.transform.position.z);
          trail.transform.localScale = new Vector3(0.75f, Mathf.Abs(newNote.transform.position.z - endNote.transform.position.z) - 0.5f, 1f);

          endNote.GetComponent<SpriteRenderer>().color = color;
          trail.GetComponent<SpriteRenderer>().color = new Color(color.a, color.g, color.b, 0.5f);
        }        
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
