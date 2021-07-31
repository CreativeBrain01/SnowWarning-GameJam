using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject lightProjectile;
    public float fireRate;
    float fireTimer;
    GameObject curLight;
    Vector2 input;
    Vector3 velocity;
    Vector2 mousePos { get => Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); }

    PlayerControls playerControls;
    float dt { get => Time.deltaTime; }

    private void Awake()
    {
        playerControls = new PlayerControls();
        fireTimer = fireRate;
    }

    private void Start()
    {
        GetComponent<PlayerInput>().onActionTriggered += HandleAction;
    }

    void Update()
    {
        velocity.x = input.x * speed;
        transform.Translate(velocity * dt);

        velocity = Vector2.zero;

        if(fireTimer < fireRate)
        {
            fireTimer += dt;
        }

        if(transform.position.y < -6.5f)
        {
            Destroy(gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input.x = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.action.triggered && fireTimer >= fireRate)
        {
            curLight = Instantiate(lightProjectile, transform.position, Quaternion.identity);
            curLight.GetComponent<ShootLight>().destination = mousePos;
            fireTimer = 0;
        }
    }

    private void HandleAction(InputAction.CallbackContext context)
    {
        if (context.action.name == "light")
        {
            OnFire(context);
        }
        if(context.action.name == "jump")
        {
            OnJump(context);
        }
        if (context.action.name == "move")
        {
            OnMove(context);
        }
    }
}
