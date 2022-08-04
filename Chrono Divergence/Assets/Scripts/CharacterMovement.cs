using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private NavMeshAgent navAgent;
    private MouseInput mouseInput;
    private Camera mainCamera;
    public Tilemap map;
    private Vector3 destination;

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
        mouseInput.Mouse.MouseClick.performed += ctx => MouseClick(ctx);
        mainCamera = Camera.main;
        destination = transform.position;
    }

    private void MouseClick(InputAction.CallbackContext ctx)
    {
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        if (map.HasTile(gridPosition))
        {
            destination = mousePosition;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        }
    }
}
