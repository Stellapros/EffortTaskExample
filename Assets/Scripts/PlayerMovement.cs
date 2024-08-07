using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const int V = 5;
    public float moveStepSize = V;
    public Rigidbody2D rb;


    private Vector2 moveDirection;


    // Update is called once per frame
    void Update() 
    {

        ///Processing inputs
        ProcessInputs();
    }

    void FixedUpdate()
    {

        ///Physics Calculations
        Move();
   
    }

    void ProcessInputs()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

     

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveStepSize, moveDirection.y * moveStepSize);
    }

}

