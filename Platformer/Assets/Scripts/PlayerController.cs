using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;

  

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    // Shooting
    public float bulletSpeed = 1f;
    public GameObject bulletPrefab;
    private Transform bulletSpawn;

    // Lives
    public int player1LivesCount = 3;
    public GameObject Player1Lives;

    public int player2LivesCount = 3;
    public GameObject Player2Lives;


    public bool gameover = false;
    // Use this for initialization
    void Start()
    {
        bulletSpawn = transform.GetChild(0);

        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            if (this.tag == "Player 1")
            {
                Player1();
            }
            else if (this.tag == "Player 2")
            {
                Player2();
            }
        }
        
        
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    // Controls for Player 1
    private void Player1()
    {
        // Movement controls
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                moveDirection = 0;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Shooting
        if (Input.GetKeyDown(KeyCode.V))
        {
            shootBullet();
        }
    }

    // Controls for Player 2
    private void Player2()
    {
        // Movement controls
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
        }
        else
        {
            if (isGrounded && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                moveDirection = 0;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Shooting
        if (Input.GetKeyDown(KeyCode.M))
        {
            shootBullet();
        }
    }


    private void shootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(transform.localScale.x * bulletSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            if (tag == "Player 1")
            {
                Player1Lives.transform.GetChild(player1LivesCount - 1).gameObject.SetActive(false);
                player1LivesCount -= 1;
                if (player1LivesCount == 0)
                {
                    FindObjectOfType<GameController>().GetComponent<GameController>().gameEnd("Player 2");
                }
            }
            if (tag == "Player 2")
            {
                Player2Lives.transform.GetChild(player2LivesCount - 1).gameObject.SetActive(false);
                player2LivesCount -= 1;
                if (player2LivesCount == 0)
                {
                    FindObjectOfType<GameController>().GetComponent<GameController>().gameEnd("Player 1");
                }
            }
        }
        
    }
}