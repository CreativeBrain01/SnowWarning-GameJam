using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject lightProjectile;
    GameObject curLight;
    Vector2 input;
    Vector3 velocity;
    Vector2 mousePos { get => Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); }

    PlayerControls playerControls;
    float dt { get => Time.deltaTime; }

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        GetComponent<PlayerInput>().onActionTriggered += HandleAction;
    }

    void Update()
    {
        velocity.y = input.y * jumpForce;
        velocity.x = input.x * speed;
        transform.Translate(velocity * dt);

        velocity = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input.x = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        input.y = context.ReadValueAsButton() ? 1 : 0;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (playerControls.Player.Light.triggered)
        {
            if(curLight != null)
            {
                Destroy(curLight);
            }
            curLight = Instantiate(lightProjectile, transform.position, Quaternion.identity);
            curLight.GetComponent<ShootLight>().destination = mousePos;
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
