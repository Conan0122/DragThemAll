//  Handling Defender Spawner Area
//  Handling Defender Spawner aftereffects and mechanism

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderSpawner : MonoBehaviour
{
    #region Variable Initialization

    Defender defender;
    DefenderButton[] defenderButtons;
    CancelSelectionButton cancelSelectionButton;
    Touch touch;
    Vector2 touchPos;
    Camera mainCamera;
    Collider2D myCollider;
    SpriteRenderer spriteRenderer;

    [SerializeField] Color32 spawnerOnEnableColor;
    [SerializeField] Color32 spawnerOnDisableColor;

    #endregion

    #region Getters and Setters
    public Color32 SpawnerOnEnableColor
    {
        get { return spawnerOnEnableColor; }    // read
        set
        {
            spawnerOnEnableColor = value;
        }
    }

    public Color32 SpawnerOnDisableColor
    {
        get { return spawnerOnDisableColor; }   // read
        set
        {
            spawnerOnDisableColor = value;
        }
    }
    #endregion

    private void Start()
    {
        defenderButtons = FindObjectsOfType<DefenderButton>();
        cancelSelectionButton = FindObjectOfType<CancelSelectionButton>();
        myCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(0, 0, 0, 0);
        mainCamera = Camera.main;
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void Update()
    {
        SpawnDefender();
    }

    void SpawnDefender()
    {
        if (Input.touchCount > 0 && defender != null)
        {
            touch = Input.GetTouch(0);
            touchPos = mainCamera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Ended:
                    if (myCollider == Physics2D.OverlapPoint(touchPos) && defender.NumberOfDefender > 0)
                    {
                        foreach (DefenderButton button in defenderButtons)
                        {
                            if (button.DefenderButtonIsSelected == true)
                            {
                                DefenderToSpawn(GetSpawnPosition());
                                spriteRenderer.color = new Color32(0, 0, 0, 0);                     //  Make Defender spawn game area transparent
                            }
                            button.DefenderButtonIsSelected = false;
                            cancelSelectionButton.GetComponent<Image>().enabled = false;
                            cancelSelectionButton.GetComponent<Animator>().enabled = false;
                            button.transform.parent.GetComponentInParent<Image>().enabled = false;  //  Disable selected defender's border
                        }
                    }
                    break;
            }
        }
    }

    Vector2 GetSpawnPosition()
    {
        touch = Input.GetTouch(0);
        touchPos = mainCamera.ScreenToWorldPoint(touch.position);
        Vector2 touchedPos = new Vector2(touchPos.x, touchPos.y);
        return touchedPos;
    }

    void DefenderToSpawn(Vector2 spawnPosition)
    {
        Defender newDefender = Instantiate(defender, spawnPosition, Quaternion.identity);
        defender.DecrementDefenderQuantity();
    }


}
