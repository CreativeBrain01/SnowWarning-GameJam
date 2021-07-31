using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLight : MonoBehaviour
{
    public Vector2 destination;

    [SerializeField] 
    [Tooltip("Affects the rate of interpolation of the projectile's movement")] float speed;

    public float Speed { get => 1 - 1 / (speed + 1); }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, destination, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collide");
        destination = transform.position;
    }
}
