//  Drag and Drop script 
//  Old Input system
//  Just for Prototype phase
//  Change to single object to be able to drag


using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPractice : MonoBehaviour
{

    // modified
    float startPosX, startPosY;
    Collider2D cd;
    Camera mainCamera;
    // end modified

    bool isDragActive = false;
    Vector2 screenPosition;
    Vector2 worldPosition;

    Draggable lastDragged;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        mainCamera = Camera.main;
        // TouchPractice[] controllers = FindObjectsOfType<TouchPractice>();
        // mainCamera = Camera.main;

        //  Singleton for TouchPractice
        // if (controllers.Length > 1)
        // {
        //     Destroy(gameObject);
        // }
    }

    private void Update()
    {
        // EnemyDrag(); // Drag with raycast
        TouchDrag(); // Working drag
    }

    void TouchDrag()
    {

        Touch touch = Input.GetTouch(0);















        // #region Multiple touch code

        // int touches = Input.touchCount;

        // if (touches > 0)
        // {
        //     for (int i = 0; i < touches; i++)
        //     {
        //         Touch touch = Input.GetTouch(i);
        //         Vector2 touchPos = mainCamera.ScreenToWorldPoint(touch.position); // modified

        //         TouchPhase phase = touch.phase;

        //         switch (phase)
        //         {
        //             case TouchPhase.Began:

        //                 // modified
        //                 if (cd == Physics2D.OverlapPoint(touchPos))
        //                 {
        //                     Debug.Log("New Touch began at index " + touch.fingerId);
        //                     startPosX = touchPos.x - transform.position.x;
        //                     startPosY = touchPos.y - transform.position.y;
        //                     isDragActive = true;
        //                     rb.gravityScale = 0;
        //                 }

        //                 break;

        //             case TouchPhase.Moved:

        //                 // modified
        //                 if (cd == Physics2D.OverlapPoint(touchPos) || isDragActive)
        //                 {
        //                     Debug.Log("New Touch Moved at index" + touch.fingerId);
        //                     rb.MovePosition(new Vector2(touchPos.x - startPosX, touchPos.y - startPosY));
        //                 }

        //                 break;

        //             case TouchPhase.Ended:

        //                 // modified
        //                 isDragActive = false;


        //                 Debug.Log("New Touch ended at index" + touch.fingerId);
        //                 rb.gravityScale = 1;
        //                 break;
        //         }
        //     }
        // }
        // #endregion
    }

    #region EnemyDrag_withRaycast
    void EnemyDrag()
    {
        if (isDragActive)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Drop();
                return;
            }

        }

        if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        if (isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    lastDragged = draggable;
                    InitDrag();
                }
            }
        }
    }

    void InitDrag()
    {
        isDragActive = true;
    }

    void Drag()
    {
        rb.gravityScale = 0;
        lastDragged.transform.position = new Vector2(screenPosition.x, worldPosition.y);
        Debug.Log("drag detected");
    }

    void Drop()
    {
        rb.gravityScale = 1;
        Debug.Log("drag removed");
        isDragActive = false;
    }

    #endregion

}
