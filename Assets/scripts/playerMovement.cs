using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public Vector2 movementInput;
    public Vector2 direction = Vector2.down;
    public Sprite frontSprite;
    public Sprite backSprite;
    BoxCollider2D box;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spr;

    public float collisionOffset = 0.005f;
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public LayerMask interactMask;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public bool freeze = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (movementInput != Vector2.zero && !freeze) {
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
        if (movementInput == Vector2.zero || freeze) {
            return;
        }

        if (movementInput.x > 0) {
            spr.flipX = true;
        } else if (movementInput.x < 0) {
            spr.flipX = false;
        }

        if (movementInput.y > 0) {
            spr.sprite = backSprite;
        } else if (movementInput.y < 0) {
            spr.sprite = frontSprite;
        }
        // if (movementInput.x != 0) {
        //     if (movementInput.x > 0) {
        //         direction = Vector2.right;
        //         spr.flipX = true;
        //     } else {
        //         direction = Vector2.left;
        //         spr.flipX = false;
        //     }
        // } else {
        //     if (movementInput.y > 0) {
        //         direction = Vector2.up;
        //         spr.sprite = backSprite;
        //     } else {
        //         direction = Vector2.down;
        //         spr.sprite = frontSprite;
        //     }
        // }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
        updateDirection();
    }

    void OnInteract() {
        if (freeze) {
            return; 
        }

        Vector2 g_position = gameObject.transform.position;
        Vector2 box_center = g_position + box.offset;

        float laserLengthY = box.size.y / 2 + 0.2f;
        float laserLengthX = box.size.x / 2 + 0.2f;
        
        int hori = 0;
        int vert = 0; 
        if (spr.flipX) {
            hori = 1;
        } else { hori = -1; }

        if (spr.sprite == backSprite) {
            vert = 1; 
        } else { vert = -1; }
        RaycastHit2D hitHori = Physics2D.Raycast(box_center, new Vector3(hori, 0, 0) , laserLengthX, interactMask);
        RaycastHit2D hitVert = Physics2D.Raycast(box_center, new Vector3(0, vert, 0) , laserLengthY, interactMask);
        if (hitHori.collider != null) {
            hitHori.collider.GetComponent<Interactable>().Interact();
        }

        if (hitVert.collider != null) {
            hitVert.collider.GetComponent<Interactable>().Interact();
        }
    }

    public void FreezePlayer() {
        freeze = true;
    }

    public void UnfreezePlayer() {
        freeze = false;
    }
}
