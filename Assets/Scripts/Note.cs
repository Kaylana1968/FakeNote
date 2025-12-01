using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note
{
  [Range(0, 5)]
  public int column;
  public float time;
  public Note nextNote;
}
