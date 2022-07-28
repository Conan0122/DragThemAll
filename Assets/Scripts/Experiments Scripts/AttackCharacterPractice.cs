//   can be named as AttackerMovement instead of AttackCharacter


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharacterPractice : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D myCollider;
    [SerializeField] LayerMask groundLayerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        // Check if ground is touched
        // Move enemy
        if (IsGrounded())
        {
            StartCoroutine(MoveEnemy());
        }
        else
        {
            Debug.Log("not grounded");
        }
    }

    IEnumerator MoveEnemy()
    {
        yield return new WaitForSeconds(.3f);

        #region Enemy Movement
        //  Move character based on spawned position
        if (GetComponent<Transform>().position.x < 0 && IsGrounded())
        {
            MoveEnemyInLeft();
        }
        else if (GetComponent<Transform>().position.x > 0 && IsGrounded())
        {
            MoveEnemyInRight();
        }
        #endregion
    }
    
    public void MoveEnemyInLeft()
    {
        rb.velocity = new Vector2(-2f, 0f); // Change hardcoded Speed and mention it in inspector
    }

    public void MoveEnemyInRight()
    {
        rb.velocity = new Vector2(2f, 0f);  // Change hardcoded Speed and mention it in inspector
    }

    bool IsGrounded()
    {
        float extraHeight = 0.2f;   // Extending raycast for more accuracy
        RaycastHit2D raycastHit = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + extraHeight, groundLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(myCollider.bounds.center, Vector2.down * (myCollider.bounds.extents.y + extraHeight), rayColor);
        return raycastHit.collider != null;
    }

}
