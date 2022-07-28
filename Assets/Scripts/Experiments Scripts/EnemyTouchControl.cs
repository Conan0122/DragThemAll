// Use this script when finally done with Drag and Drop prototype
// main script for drag and drop


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchControl : MonoBehaviour
{
    Rigidbody2D rb;
    // Collider2D collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Character Collider
        // collider = GetComponent<CircleCollider2D>();
    }




}
