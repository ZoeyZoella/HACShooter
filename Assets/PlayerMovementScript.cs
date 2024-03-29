using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D rig;
    private CapsuleCollider2D col;
    private Animator anim;
    private SpriteRenderer rend;

    private float horizontal;
    private bool grounded = false;
    private bool isRunning = false;

    public float speed = 3f;
    public float jumpSpeed = 8f;
    public LayerMask groundLayer;

    public GameObject bulletPrefab;
    public float bulletSpeed = 40f;
    public float bulletOffset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<CapsuleCollider2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
        rend = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Mathf.Lerp(horizontal, Input.GetAxisRaw("Horizontal") * speed, 0.3f);

        isRunning = Mathf.Abs(horizontal) > 0.05f;

        anim.SetBool("isRunning", isRunning);

        if (isRunning) rend.flipX = Mathf.Sign(horizontal) != 1f;

        grounded = isGrounded();
        anim.SetBool("isJumping", !grounded);

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 a = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position + col.offset.y * Vector3.up + a * bulletOffset, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = a * bulletSpeed;
            Destroy(bullet, 5);
        }
    }

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(horizontal, rig.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, 0.05f, groundLayer);
    }
}
