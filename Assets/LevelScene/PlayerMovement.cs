using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D PlayerBody;
    public float speed = 1f;
    void Start()
    {
        PlayerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerBody.velocity = new Vector2(-1 * speed, 0);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            PlayerBody.velocity = new Vector2(speed, 0);
        }
        else
        {
            PlayerBody.velocity = new Vector2(0, 0);
        }
    }
}
