using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterMovementTopdown : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private MouseInput mouseInput;
    [SerializeField] private Vector3 destination;
    [SerializeField] private LayerMask collisionLayers;
    private Vector3 checkedOffset;

    public float MoveSpeed => moveSpeed;
    
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
        destination = transform.position;
    }

    private void Update()
    {
        Move();
        if (Vector2.Distance(transform.position, destination) > 0.0001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        checkedOffset = GetDirectionOffset(mouseInput.Keyboard.Move.ReadValue<Vector2>());
        
        if (Vector2.Distance(transform.position, destination) < 0.1f)
        {
            GameObject objectInFront = null;
            int originalLayer = gameObject.layer;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            if (Physics2D.OverlapBox(
                new Vector2(transform.position.x + checkedOffset.x * 0.5f,
                    transform.position.y + checkedOffset.y * 0.5f), Vector2.one * 0.5f, 0, collisionLayers))
            {
                objectInFront = Physics2D
                    .OverlapBox(
                        new Vector2(transform.position.x + checkedOffset.x * 0.5f,
                            transform.position.y + checkedOffset.y * 0.5f), Vector2.one * 0.5f, 0, collisionLayers).gameObject;
            }
            gameObject.layer = originalLayer;
            
            if(objectInFront)
            {
                if (objectInFront != this.gameObject)
                {
                    Component[] tempMonoArray = objectInFront.GetComponents<Component>();
                    List<IMovable> movableObject = objectInFront.GetInterfaces<IMovable>();
                    if (movableObject.Count > 0)
                    {
                        if (movableObject[0].MoveInDirection(checkedOffset))
                        {
                            destination = new Vector3(transform.position.x + checkedOffset.x, transform.position.y + checkedOffset.y, transform.position.z).Round(0);
                        }
                        else
                        {
                            destination = transform.position.Round(0);
                        }
                    }
                    else
                    {
                        destination = transform.position.Round(0);
                    }
                }
            }
            else
            {
                destination = new Vector3(transform.position.x + checkedOffset.x, transform.position.y + checkedOffset.y, transform.position.z);
                destination = destination.Round(0);
            }
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
