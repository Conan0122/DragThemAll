//      Handling Cancel Btn mechanism

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelSelectionButton : MonoBehaviour
{
    #region Variable Initialization

    DefenderSpawner defenderSpawner;
    DefenderButton[] defenderButtons;
    Touch touch;
    Collider2D myCollider;
    Vector2 touchPos;
    Camera mainCamera;
    Animator myAnimator;

    #endregion

    void Start()
    {
        defenderButtons = FindObjectsOfType<DefenderButton>();
        defenderSpawner = FindObjectOfType<DefenderSpawner>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        mainCamera = Camera.main;

        this.gameObject.GetComponent<Image>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;
    }

    void Update()
    {
        CancelSelection();
    }

    void CancelSelection()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPos = mainCamera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Ended:
                    if (myCollider == Physics2D.OverlapPoint(touchPos))
                    {
                        Image image = this.gameObject.GetComponent<Image>();

                        //  Disable Cancel Btn
                        //  When any defender btn is selected
                        foreach (DefenderButton button in defenderButtons)
                        {
                            button.DefenderButtonIsSelected = false;
                            button.transform.parent.GetComponentInParent<Image>().enabled = false;          //  Disable defender btn's selection border
                            image.enabled = false;                                                          //  Disable Cancel btn
                            myAnimator.enabled = false;
                            defenderSpawner.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0); //  Disable defender spawner area when clicked cancel btn
                        }
                    }
                    break;
            }
        }
    }

}