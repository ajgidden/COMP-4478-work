using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;

    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        //sets the horizontal value based on if the play chooses left or right
        horizontal = Input.GetAxisRaw("Horizontal");

    }

    //will give it velocity based on the direction it is going
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

}
