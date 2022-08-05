using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterMovement3D : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private NavMeshAgent navAgent;
    private MouseInput mouseInput;
    private Camera mainCamera;
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
        RaycastHit hit; 
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition); 
        if ( Physics.Raycast (ray,out hit,1000.0f))
        {
            if (hit.transform.gameObject.CompareTag("Floor"))
            {
                destination = hit.point;
                navAgent.destination = destination;
            }
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            navAgent.Move(Vector3.zero);
            //transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        }
    }
}
