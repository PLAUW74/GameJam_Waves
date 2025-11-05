using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    public Vector2 velocity;
    public float speed = 2f;

    public void Move(Vector2 direction)
    {
        velocity = direction.normalized * speed;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
