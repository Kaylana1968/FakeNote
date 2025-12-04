using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LevelLauncher : MonoBehaviour
{
	[SerializeField] GameObject tapNote;
	[SerializeField] GameObject noteTrail;
	[SerializeField] float musicStartAt = 0f;

	AudioSource audioSource;
	public Level level;
	public List<Transform> columns = new();
	public List<Transform> notes = new();

	readonly float startPauseTime = 2f;
	readonly float speed = 5f;
	readonly List<Color> colors = new()
	{
		new Color(1f, 0.5f, 0f),
		new Color(1f, 1f, 0f),
		new Color(0f, 1f, 0f),
		new Color(0f, 1f, 1f),
		new Color(0f, 0f, 1f),
		new Color(1f, 0f, 1f)
	};
	readonly Quaternion angle = Quaternion.Euler(new Vector3(90f, 0f, 0f));

	double musicStartDSPTime;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();

		if (level != null && level.audio != null)
		{
			musicStartDSPTime = AudioSettings.dspTime + startPauseTime;

			audioSource.clip = level.audio;
			audioSource.time = musicStartAt;
			audioSource.PlayScheduled(musicStartDSPTime);
		}

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

				NoteData data = newNote.AddComponent<NoteData>();
				data.time = noteRow.time;
				data.isFake = note.isFake;

				Color color = colors[note.column];
				newNote.GetComponent<SpriteRenderer>().color = data.isFake ? Color.red : color;

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

					endNote.GetComponent<SpriteRenderer>().color = data.isFake ? Color.red : color;
					trail.GetComponent<SpriteRenderer>().color = data.isFake ? new Color(1f, 0f, 0f, 0.5f) : new Color(color.r, color.g, color.b, 0.5f);
				}
			}
		}
	}

	void Update()
	{
		double musicTime = AudioSettings.dspTime - musicStartDSPTime + musicStartAt;

		foreach (Transform note in notes)
		{
			NoteData data = note.GetComponent<NoteData>();

			float targetZ = (data.time - (float)musicTime) * speed;

			note.localPosition = new Vector3(0f, 0f, targetZ + 3.5f);
		}
	}
}
