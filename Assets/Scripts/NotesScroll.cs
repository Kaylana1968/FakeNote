using UnityEngine;

public class NotesScroll : MonoBehaviour
{
    Vector3 targetPosition = new(0f, 0f, -100f);

    readonly float speed = 15f;

    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
