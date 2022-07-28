//  Handling Defender Btn

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefenderButton : MonoBehaviour
{
    #region Variable Initialization

    CancelSelectionButton cancelSelectionButton;
    Animator cancelAnimator;
    DefenderButton[] defenderButtons;
    Defender defender;
    [SerializeField] Defender defenderPrefab;
    DefenderSpawner defenderSpawner;
    Touch touch;
    Vector2 touchPos;
    Camera mainCamera;
    Collider2D myCollider;
    TextMeshProUGUI defenderQuantityText;

    bool defenderButtonIsSelected = false;

    #endregion

    #region Getters and Setters
    public bool DefenderButtonIsSelected
    {
        get { return defenderButtonIsSelected; }
        set
        {
            defenderButtonIsSelected = value;
        }
    }
    #endregion

    private void Start()
    {
        mainCamera = Camera.main;
        myCollider = GetComponent<Collider2D>();
        defenderSpawner = FindObjectOfType<DefenderSpawner>();
        cancelSelectionButton = FindObjectOfType<CancelSelectionButton>();
        cancelAnimator = cancelSelectionButton.GetComponent<Animator>();
        defenderButtons = FindObjectsOfType<DefenderButton>();
        defender = FindObjectOfType<Defender>();
        defenderQuantityText = GetComponentInChildren<TextMeshProUGUI>();

        UpdateDefenderQuantity();
        this.transform.parent.GetComponentInParent<Image>().enabled = false;
    }

    private void Update()
    {
        UpdateDefenderQuantity();
        SelectDefender();
    }

    void SelectDefender()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPos = mainCamera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (myCollider == Physics2D.OverlapPoint(touchPos)
                        && DefenderButtonIsSelected == false)
                    {
                        //  Turn off all the defender buttons
                        foreach (DefenderButton button in defenderButtons)
                        {
                            button.DefenderButtonIsSelected = false;
                            button.transform.parent.GetComponentInParent<Image>().enabled = false;
                        }

                        if (defenderPrefab.NumberOfDefender > 0)
                        {
                            // Green overlay on defender spawn area
                            defenderSpawner.GetComponent<SpriteRenderer>().color = defenderSpawner.SpawnerOnEnableColor;
                        }
                        else
                        {
                            // Red overlay on defender spawn area
                            defenderSpawner.GetComponent<SpriteRenderer>().color = defenderSpawner.SpawnerOnDisableColor;
                        }

                        //  Perform tasks after button is selected
                        //  Turn on the only button which is selected
                        DefenderButtonIsSelected = true;
                        this.transform.parent.GetComponentInParent<Image>().enabled = true;
                        defenderSpawner.SetSelectedDefender(defenderPrefab);
                        cancelSelectionButton.GetComponent<Image>().enabled = true;
                        cancelAnimator.enabled = true;
                        cancelAnimator.Play("Cancel Anim", -1, 0f);                                         //  Reset Cancel btn animation everytime btn is disabled

                    }
                    break;
            }
        }
    }

    public void UpdateDefenderQuantity()
    {
        if (!defenderQuantityText)
        {
            return;
        }
        defenderQuantityText.text = defenderPrefab.NumberOfDefender.ToString();
    }

}
