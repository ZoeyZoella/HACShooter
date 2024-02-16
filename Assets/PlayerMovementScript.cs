using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D rig;
    private float horizontal;
    private bool grounded = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Mathf.Lerp(horizontal, Input.GetAxisRaw("Horizontal") * speed, 0.3f);

        grounded = isGrounded();

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 a = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position + a * bulletOffset, Quaternion.identity);
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
