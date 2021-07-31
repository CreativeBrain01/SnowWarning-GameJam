using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShootLight : MonoBehaviour
{
    public Vector2 destination;
    Light2D light;

    public float diminishRate;

    [SerializeField] 
    [Tooltip("Affects the rate of interpolation of the projectile's movement")] float speed;

    public float Speed { get => 1 - 1 / (speed + 1); }

    private void Start()
    {
        light = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(((Vector2)transform.position - destination).magnitude < 0.1)
        {
            if(light.intensity > 0)
            {
                light.intensity -= diminishRate * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        transform.position = Vector2.Lerp(transform.position, destination, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        destination = transform.position;
    }
}
