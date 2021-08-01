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

    Rigidbody2D rb;

    public bool canJump;

    [SerializeField] float gracePeriod;
    float graceTimer = 0;

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
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!GameManager.play) return;

        if(fireTimer < fireRate)
        {
            fireTimer += dt;
        }

        if(Camera.main.WorldToScreenPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }

        if (rb.IsTouchingLayers(LayerMask.GetMask("Terrain", "WallRight", "WallLeft")))
        {
            canJump = true;
        }
        else if(graceTimer < gracePeriod)
        {
            graceTimer += dt;
        }
        else
        {
            canJump = false;
        }

        if (rb.IsTouchingLayers(LayerMask.GetMask("WallRight", "WallLeft")))
        {
            rb.gravityScale = 0.4f;
        }
        else
        {
            rb.gravityScale = 2;
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.play) return;

        velocity.x = input.x * speed;
        transform.Translate(velocity * dt);

        velocity = Vector2.zero;
    }

    public void OnDestroy()
    {
        GameManager.state = GameManager.eState.PlayerDead;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!GameManager.play) return;

        input.x = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!GameManager.play) return;

        if (context.action.triggered && canJump)
        {
            if(rb.IsTouchingLayers(LayerMask.GetMask("WallRight")))
            {
                rb.AddForce(new Vector2 (0.75f, 0.75f) * jumpForce);
            }
            else if (rb.IsTouchingLayers(LayerMask.GetMask("WallLeft")))
            {
                rb.AddForce(new Vector2(-0.75f, 0.75f) * jumpForce);
            }
            else
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!GameManager.play) return;

        if (context.action.triggered && fireTimer >= fireRate)
        {
            curLight = Instantiate(lightProjectile, transform.position, Quaternion.identity);
            curLight.GetComponent<ShootLight>().destination = mousePos;
            fireTimer = 0;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        graceTimer = 0;
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
