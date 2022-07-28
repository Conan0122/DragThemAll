//  using Basic Touch inputs


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCharacter : MonoBehaviour
{
    bool moveAllowed;
    Collider2D col;
    Rigidbody2D rb;
    int currentGravity;

    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        currentGravity = (int)rb.gravityScale;
        // Debug.Log("Default gravity of character is:- " + currentGravity);
    }

    void Update()
    {
        MoveCreatures();
    }

    private void MoveCreatures()
    {
        if (Input.touchCount > 0)
        {
            DragCreatures();
        }
    }

    // void OnDrag()
    // {
    //     DragCreatures();
    //     Debug.Log("hey");
    // }

    void DragCreatures()
    {
        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        if (touch.phase == TouchPhase.Began)
        {
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
            if (col == touchedCollider)
            {
                moveAllowed = true;
                rb.gravityScale = 0;
                Debug.Log("Gravity on drag is:- " + rb.gravityScale);

            }
        }

        if (touch.phase == TouchPhase.Moved)
        {
            if (moveAllowed)
            {
                transform.position = new Vector2(touchPosition.x, touchPosition.y);
            }
        }

        if (touch.phase == TouchPhase.Ended)
        {
            moveAllowed = false;
            rb.gravityScale = currentGravity;
        }
    }
}
