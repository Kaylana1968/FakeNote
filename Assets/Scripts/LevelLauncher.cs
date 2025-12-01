using System.Collections.Generic;
using UnityEngine;

public class LevelLauncher : MonoBehaviour
{
  [SerializeField] GameObject tapNote;
  [SerializeField] GameObject gamePanel;

  readonly float planeSize = 10f;

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

      Instantiate(tapNote, new Vector3((planeSize / 2f + planeSize * note.column) / 6f, 0.0f, 0.0f), Quaternion.identity);

      currentNoteIndex++;

      if (currentNoteIndex >= level.notes.Count) return;
    }
  }
}
