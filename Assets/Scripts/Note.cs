using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Note
{
  [Range(0, 5)]
  public int column;
  public float time;
  public List<ChildNote> followingNotes;
}

[Serializable]
public class ChildNote
{
  [Range(0, 5)]
  public int column;
  public float time;
}
