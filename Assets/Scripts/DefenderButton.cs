//      Handling Defender Btn

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DefenderName
{
    Level1Wall, Level2Wall, Venus, DrInk
}

public class DefenderButton : MonoBehaviour
{
    #region Variable Initialization

    CancelSelectionButton cancelSelectionButton;
    Animator cancelAnimator;
    DefenderButton[] defenderButtons;
    DefenderSpawner defenderSpawner;
    Touch touch;
    Vector2 touchPos;
    Camera mainCamera;
    Collider2D myCollider;
    TextMeshProUGUI defenderQuantityText;

    [SerializeField] DefenderName defName;
    [SerializeField] GameObject defenderPrefab;
    [SerializeField] int numberOfDefender;

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

    public int NumberOfDefender
    {
        get { return numberOfDefender; }
        set { numberOfDefender = value; }
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
        defenderQuantityText = GetComponentInChildren<TextMeshProUGUI>();

        UpdateDefenderQuantityText();
        this.transform.parent.GetComponentInParent<Image>().enabled = false;
    }

    private void Update()
    {
        UpdateDefenderQuantity();
        UpdateDefenderQuantityText();
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

                        if (numberOfDefender > 0)
                        {
                            // Green overlay on defender spawn area
                            AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
                            defenderSpawner.GetComponent<SpriteRenderer>().color = defenderSpawner.SpawnerOnEnableColor;
                        }
                        else
                        {
                            // Red overlay on defender spawn area
                            AudioManager.instance.PlayAudio(Sounds.AudioName.EmptyDefenderSlot, true);
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
        // DataPersistenceManager.instance.SaveFile();                     // Debug purpose

        // We need to reverse the index
        // Because foreach will execute button's name in reverse order.
        // So if , we have 4 buttons then it will execute 4th button first then 3rd and so on.
        int index = DataPersistenceManager.instance.gameData.DefendersInfos.Count - 1;

        foreach (DefenderButton button in defenderButtons)
        {
            if (index < DataPersistenceManager.instance.gameData.DefendersInfos.Count)
            {
                // if (button.defName == DataPersistenceManager.instance.gameData.DefendersInfos[index].Def)
                // {
                button.numberOfDefender = DataPersistenceManager.instance.gameData.DefendersInfos[index].Amt;
                // }
            }
            index--;
        }
    }

    // Update the Qty text in defender button
    public void UpdateDefenderQuantityText()
    {
        if (!defenderQuantityText) { return; }

        defenderQuantityText.text = numberOfDefender.ToString();
    }

}
