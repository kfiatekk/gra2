using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Animator myAnimator;
    private Vector2 input;
    private Rigidbody2D rb;
    private LayerMask SolidObjectLayer;
    private float elo;
    private bool canMove = true;
    private Sprite OriginalSprite;
    private RuntimeAnimatorController originalController;
    public bool IsOnStairs { get; private set; }
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    public Sprite newSprite; // The new sprite you want to switch to
    public AnimatorOverrideController overrideController;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        if (spriteRenderer == null) // Ensure spriteRenderer is set
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer component is missing from PlayerController!");

        OriginalSprite = spriteRenderer.sprite;

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalController = myAnimator.runtimeAnimatorController;
    }

    private void Update()
    {
        if (canMove)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                myAnimator.SetFloat("LastMoveX", input.x);
            }

            bool isMoving = input.x != 0 || input.y != 0;
            myAnimator.SetBool("IsMoving", isMoving);
        }
        else
        {
            input = Vector2.zero;
            myAnimator.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {
        if (canMove && input != Vector2.zero)
        {
            elo = myAnimator.GetFloat("LastMoveX");

            if (input.x == 0 && input.y != 0)
            {
                myAnimator.SetFloat("MoveX", elo);
            }
            else
            {
                myAnimator.SetFloat("MoveX", input.x);
            }

            myAnimator.SetFloat("MoveY", input.y);
            Vector2 moveDirection = input.normalized * moveSpeed;

            if (!IsCollisionAhead(moveDirection * Time.fixedDeltaTime))
            {
                rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
            }
        }
    }

    private bool IsCollisionAhead(Vector2 moveDirection)
    {
        Vector2 currentPosition = rb.position;
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, moveDirection.normalized, moveDirection.magnitude, SolidObjectLayer);

        return hit.collider != null;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
    public Vector2 GetInput()
    {
        return input;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StairStep"))
        {
            IsOnStairs = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("StairStep"))
        {
            IsOnStairs = false;
        }
    }

    // 27.03.2025 AI-Tag
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    public void ChangePlayerSprite()
    {
        if (spriteRenderer == null || newSprite == null)
        {
            Debug.LogWarning("SpriteRenderer or newSprite is not assigned!");
            return;
        }

        Debug.Log("Before sprite change: " + spriteRenderer.sprite.name); // Debug
        spriteRenderer.sprite = newSprite;
        Debug.Log("Changed player sprite to: " + spriteRenderer.sprite.name); // Debug

        if (myAnimator == null || overrideController == null)
        {
            Debug.LogWarning("Animator or OverrideController is not assigned!");
            return;
        }

        myAnimator.runtimeAnimatorController = overrideController;
        Debug.Log("Animator override applied.");
    }
    public void ChangePlayerBack()
    {
        spriteRenderer.sprite = OriginalSprite;
        myAnimator.runtimeAnimatorController = originalController; 

    }

}
