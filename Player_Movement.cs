using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private BoxCollider2D boxCollider2D;

    [Min(0)]
    [Header("Player Settings:")]
    public float movementSpeed = 5;
    [Min(0)]
    public float jumpStrength = 10;

    //  [SerializeField]
    private float oldY, newY, differenceY;
    public float distToGround;
    public bool isJumping, isGrounded = true;
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerBody.freezeRotation = true;
        playerBody.gravityScale = 3;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    void Update()
    {

        Vector3 movevector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        transform.Translate(movevector * movementSpeed * Time.deltaTime);

        if (!isJumping && isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        DetectGround();

        newY = transform.position.y;

        differenceY = (oldY - newY) / 2;

        if (differenceY < 0)
        {
            boxCollider2D.isTrigger = true;
        }
        else
            boxCollider2D.isTrigger = false;
    }

    private void FixedUpdate()
    {
        oldY = transform.position.y;
    }

    private void Jump()
    {
        isJumping = true;
        isGrounded = false;
        playerBody.velocity = new Vector2(playerBody.velocity.x, jumpStrength);
        SoundController.instance.PlaySound(SoundController.instance.shoot);
    }
    private void DetectGround()
    {
        // Bit shift the index of the layer (8) to get a bit mask // whut
        int layerMask = 1 << 8;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            distToGround = Vector3.Distance(transform.position, hit.point);
            Debug.DrawLine(transform.position, hit.point, Color.cyan);

            if (distToGround < 0.5f && differenceY > 0 && isJumping)
            {
                isJumping = false;
                isGrounded = true;
                differenceY = 0;
            }

            else
            {
                Debug.DrawLine(transform.position, -Vector3.up * 100, Color.red);
            }
        }
    }
}
