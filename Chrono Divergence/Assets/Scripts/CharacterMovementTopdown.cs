using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterMovementTopdown : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private MouseInput mouseInput;
    private Camera mainCamera;
    public Tilemap collisionMap;
    [SerializeField] private Vector3 destination;
    
    private void Awake()
    {
        mouseInput = new MouseInput();
    }

    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }
    
    private void Start()
    {
        mouseInput.Keyboard.Move.performed += ctx => OnMovement(ctx);
        mainCamera = Camera.main;
        destination = transform.position;
    }

    private void OnMovement(InputAction.CallbackContext ctx)
    {
        Debug.Log("Test 1");
        if (Vector2.Distance(transform.position, destination) < 0.1f)
        {
            Debug.Log("Test 1");
            Vector3 checkedOffset = GetDirectionOffset(ctx.ReadValue<Vector2>());
            
            if (Physics2D.OverlapBox(new Vector2(transform.position.x + checkedOffset.x, transform.position.y + checkedOffset.y), Vector2.one * 0.5f, 0))
            {
                Debug.Log("Place is not free!");
                destination = transform.position;
            }
            else
            {
                Debug.Log("Place accepted");
                destination = transform.position + checkedOffset;
            }
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        }
    }

    private Vector2 GetDirectionOffset(Vector2 vectorInput)
    {
        if (vectorInput == Vector2.right)
        {
            return new Vector2(1, 0);
        }
        else if (vectorInput == Vector2.left)
        {
            return new Vector2(-1, 0);
        }
        else if (vectorInput == Vector2.down)
        {
            return new Vector2(0, -1);
        }
        else if (vectorInput == Vector2.up)
        {
            return new Vector2(0, 1);
        }
        else
        {
            return Vector2.zero;
        }
    }
}
