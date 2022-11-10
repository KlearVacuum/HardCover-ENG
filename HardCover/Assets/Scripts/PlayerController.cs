using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float maxMoveForce;
    [Range(0, 1)] [SerializeField] float stopDampenRatio;
    Vector2 moveForce;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
    }

    private void FixedUpdate()
    {
        MovementForce();
    }

    void MovementInput()
    {
        moveForce.x += Input.GetAxisRaw("Horizontal") * moveSpeed * rb.mass * Time.deltaTime;
    }

    void MovementForce()
    {
        if (moveForce != Vector2.zero)
        {
            if (rb.velocity.magnitude < maxMoveForce)
            {
                rb.AddForce(moveForce);
            }

            moveForce = Vector2.zero;
        }
        else
        {
            rb.velocity *= stopDampenRatio;
        }
    }
}