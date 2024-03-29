using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 point = collision.GetContact(0).point;

        GameObject exp = Instantiate(explosion, new Vector3(point.x, point.y, 0), Quaternion.identity);

        Destroy(exp, 5);

        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.collider.gameObject);
        }

        Destroy(gameObject);
    }
}
