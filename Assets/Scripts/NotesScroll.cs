using UnityEngine;

public class NotesScroll : MonoBehaviour
{
    readonly float speed = 2f;

    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;

        transform.position += step * new Vector3(0f, 0f, -1f);
    }
}
