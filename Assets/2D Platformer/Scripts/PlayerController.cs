using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;

        public bool deathState = false;

        private bool isGrounded;
        public Transform groundCheck;
        public LayerMask groundLayer;

        private SpriteRenderer sr;
        private Rigidbody2D rb;
        private Animator animator;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        void Update()
        {
            // Get input once
            moveInput = Input.GetAxisRaw("Horizontal");

            // Movement (better with Rigidbody2D)
            rb.linearVelocity = new Vector2(moveInput * movingSpeed, rb.linearVelocity.y);

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            // Flip sprite (simpler)
            if (moveInput != 0)
            {
                sr.flipX = moveInput > 0;
            }

            // Animation handling (single place = no conflicts)
            if (!isGrounded)
            {
                animator.SetInteger("playerState", 2); // Jump
            }
            else if (moveInput != 0)
            {
                animator.SetInteger("playerState", 1); // Run
            }
            else
            {
                animator.SetInteger("playerState", 0); // Idle
            }
        }

        private void CheckGround()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f ,groundLayer);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Coin")
            {
                Destroy(other.gameObject);
                SceneManager.LoadScene("Credit");
            }
            else if (other.gameObject.tag == "Trap")
            {
                Destroy(other.gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
