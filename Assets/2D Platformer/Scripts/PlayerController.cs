using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float force = 0;
        public float accel = 5.5f;
        public float mass = 1;
        public float movingSpeed;
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
                mass = rb.mass;
                accel = 5.5f;
                force = mass * accel;
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            }

            // Slam
            if (Input.GetKeyDown(KeyCode.LeftControl) && !isGrounded)
            {
                mass *= 2f;
                accel = 3f;
                force = mass * accel;
                rb.AddForce(Vector2.down * force, ForceMode2D.Impulse);
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
