// Basic touch inputs
// Working fine and better than other scripts
// Need to do final adjustment
// Only script to be finalized



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPractice2 : MonoBehaviour
{
    [SerializeField] GameObject[] characterStorage;

    float deltaX, deltaY;
    Rigidbody2D rb;
    Camera mainCamera;
    Collider2D myCollider;
    Vector2 dropCharacter;


    bool moveAllowed = false;
    bool dropped;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        myCollider = GetComponent<Collider2D>();
        dropCharacter = this.transform.position;
    }

    private void Update()
    {
        //  New updated method
        DragCharacters();
    }

    void DragCharacters()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = mainCamera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (myCollider == Physics2D.OverlapPoint(touchPos))
                    {
                        Debug.Log("My new touch began");

                        //  Get the current touch position w.r.t. character
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;
                        rb.gravityScale = 0;
                        rb.velocity = new Vector2(0, 0);
                        moveAllowed = true;
                    }
                    break;

                case TouchPhase.Moved:
                    if (myCollider == Physics2D.OverlapPoint(touchPos) || moveAllowed)
                    {
                        Debug.Log("My new touch Moved");
                        rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    }
                    break;

                case TouchPhase.Ended:
                    if (myCollider == Physics2D.OverlapPoint(touchPos) || moveAllowed)
                    {
                        Debug.Log("My new touch Ended");
                        rb.gravityScale = 1;
                        moveAllowed = false;

                        //  Storing character under basket/ character storage
                        foreach (var storage in characterStorage)
                        {
                            if (Mathf.Abs(this.transform.localPosition.x -
                            storage.transform.localPosition.x) <= 1.5f &&
                            Mathf.Abs(this.transform.localPosition.y -
                            storage.transform.localPosition.y) <= 1.5f)
                            {
                                this.transform.position = new Vector2(storage.transform.position.x, storage.transform.position.y);
                                rb.bodyType = RigidbodyType2D.Kinematic;    // Can use gravity instead of bodytype
                            }
                        }
                    }
                    break;
            }
        }
    }
}
