using System;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
  tap,
  fake,
  hold,
  release
}

[Serializable]
public class NoteRow
{
  public float time;
  public List<Note> notes;
}

[Serializable]
public class Note
{
  [Range(0, 5)]
  public int column;
  public NoteType noteType;
}
