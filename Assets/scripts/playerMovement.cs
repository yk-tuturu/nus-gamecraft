using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public Vector2 movementInput;
    public Vector2 direction = Vector2.down;
    BoxCollider2D box;
    Rigidbody2D rb;
    Animator anim;

    public float collisionOffset = 0.005f;
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public LayerMask interactMask;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (movementInput != Vector2.zero) {
            int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );

            if (count == 0) {
                rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * moveSpeed);
            }
        }
    }

    void updateDirection() {
        if (movementInput == Vector2.zero) {
            return;
        }
        if (movementInput.x != 0) {
            if (movementInput.x > 0) {
                direction = Vector2.right;
            } else {
                direction = Vector2.left;
            }
        } else {
            if (movementInput.y > 0) {
                direction = Vector2.up;
            } else {
                direction = Vector2.down;
            }
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
        updateDirection();
    }

    void OnInteract() {
        Vector2 g_position = gameObject.transform.position;
        Vector2 box_center = g_position + box.offset;
        float laserLength;
        if (direction.x == 0) {
            laserLength = box.size.y / 2 + 0.1f;
        } else {
            laserLength = box.size.x / 2 + 0.1f;
        }
        RaycastHit2D hit = Physics2D.Raycast(box_center, direction , laserLength, interactMask);
        if (hit.collider != null) {
            hit.collider.GetComponent<Interactable>().Interact();
        }
    }
}
