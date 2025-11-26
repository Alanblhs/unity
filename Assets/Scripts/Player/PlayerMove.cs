using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D theRigidbody;
    public float moveSpeed = 5f;
    public Animator animator;

    public float pickupRange = 1.5f;

    private void Awake()
    {
        theRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        
        if (PlayerHealth.instance != null && PlayerHealth.instance.currentHealth <= 0)
        {
            theRigidbody.linearVelocity = Vector2.zero;
            animator.SetBool("Move", false);
            return;
        }

        Vector2 moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        theRigidbody.linearVelocity = moveInput * moveSpeed;

        animator.SetBool("Move", moveInput != Vector2.zero);

        if (moveInput.x > 0)
            transform.localScale = new Vector3(1, 1, 1); 
        else if (moveInput.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); 

        if (MapBounds.instance != null)
        {
            Vector3 pos = transform.position;

            pos.x = Mathf.Clamp(pos.x, MapBounds.instance.minBounds.x, MapBounds.instance.maxBounds.x);
            pos.y = Mathf.Clamp(pos.y, MapBounds.instance.minBounds.y, MapBounds.instance.maxBounds.y);

            transform.position = pos;
        }
    }
}
