using System.Collections.Generic;
using UnityEngine;

public class LevelLauncher : MonoBehaviour
{
  [SerializeField] GameObject tapNote;

  readonly List<Vector3> spawnPoints = new()
  {
    new(0.6f, 0f, 90f),
    new(1.7f, 0f, 90f),
    new(2.8f, 0f, 90f),
    new(3.9f, 0f, 90f),
    new(5.0f, 0f, 90f),
    new(6.1f, 0f, 90f),
  };

  public Level level;
  float elapsedTime;
  int currentNoteIndex;

  void Start()
  {
    elapsedTime = 0;
    currentNoteIndex = 0;
  } 

  void FixedUpdate()
  {
    elapsedTime += Time.deltaTime;

    if (currentNoteIndex >= level.notes.Count) return;

    while (level.notes[currentNoteIndex].time < elapsedTime)
    {
      Note note = level.notes[currentNoteIndex];
      GameObject gameObject = Instantiate(tapNote, spawnPoints[note.column], Quaternion.Euler(new Vector3(90f, 0f, 0f)));
      gameObject.transform.parent = transform;

      currentNoteIndex++;

      if (currentNoteIndex >= level.notes.Count) return;
    }
  }
}
