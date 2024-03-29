using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private PlayerMovementScript scr;

    private Rigidbody2D rig;

    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        scr = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = scr.transform.position - transform.position;
        move = new Vector3(move.x, 0, 0);

        if (move.magnitude < 4)
        {
            Vector3.Normalize(move);
            move *= speed * 2;
        }
        else
        {
            Vector3.Normalize(move);
            move *= speed;
        }

        rig.AddForce(ForceMode2D.Impulse)

        move.y = rig.velocity.y;

        rig.velocity = move;


    }
}
