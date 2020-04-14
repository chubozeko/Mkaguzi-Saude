using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    void Start()
    {
        // transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Random.Range(-1, 1);
        movement.y = Random.Range(-1, 1);

        if (Mathf.FloorToInt(movement.x) == 1 || Mathf.FloorToInt(movement.x) == -1)
        {
            animator.SetFloat("Horizontal", movement.x);
        } else
        {
            animator.SetFloat("Horizontal", 0f);
        }

        if (Mathf.FloorToInt(movement.y) == 1 || Mathf.FloorToInt(movement.y) == -1)
        {
            animator.SetFloat("Vertical", movement.y);
        } else
        {
            animator.SetFloat("Vertical", 0f);
        }

        // Animation
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
