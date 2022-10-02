/*      Handling Defender Spawner Area
        Handling Defender Spawner aftereffects and mechanism
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderSpawner : MonoBehaviour
{
    #region Variable Initialization

    GameObject defender;
    DefenderButton defenderButton;
    DefenderButton[] defenderButtons;
    CancelSelectionButton cancelSelectionButton;
    Touch touch;
    Vector2 touchPos;
    Camera mainCamera;
    Collider2D myCollider;
    SpriteRenderer spriteRenderer;

    [SerializeField] Color32 spawnerOnEnableColor;
    [SerializeField] Color32 spawnerOnDisableColor;
    GameObject defenderParent;
    const string DEFENDER_PARENT = "Defender Parent";

    #endregion

    #region Getters and Setters
    public Color32 SpawnerOnEnableColor
    {
        get { return spawnerOnEnableColor; }
        set
        {
            spawnerOnEnableColor = value;
        }
    }

    public Color32 SpawnerOnDisableColor
    {
        get { return spawnerOnDisableColor; }
        set
        {
            spawnerOnDisableColor = value;
        }
    }
    #endregion

    private void Start()
    {
        defenderButton = FindObjectOfType<DefenderButton>();
        defenderButtons = FindObjectsOfType<DefenderButton>();
        cancelSelectionButton = FindObjectOfType<CancelSelectionButton>();
        myCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(0, 0, 0, 0);
        mainCamera = Camera.main;

        CreateDefenderParent();
    }

    void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT);
        if (!defenderParent) defenderParent = new GameObject(DEFENDER_PARENT);
    }

    public void SetSelectedDefender(GameObject defenderToSelect)
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
                    // if (myCollider == Physics2D.OverlapPoint(touchPos))
                    if (myCollider == Physics2D.OverlapPoint(touchPos))
                    {
                        int index = DataPersistenceManager.instance.gameData.defendersInfos.Count - 1;
                        foreach (DefenderButton button in defenderButtons)
                        {
                            if (button.DefenderButtonIsSelected == true && button.numberOfDefender > 0)
                            {
                                DefenderToSpawn(GetSpawnPosition());
                                DecrementDefenderQuantity(index);
                            }
                            index--;

                            button.DefenderButtonIsSelected = false;
                            cancelSelectionButton.GetComponent<Image>().enabled = false;
                            cancelSelectionButton.GetComponent<Animator>().enabled = false;
                            button.transform.parent.GetComponentInParent<Image>().enabled = false;  //  Disable selected defender's border
                            spriteRenderer.color = new Color32(0, 0, 0, 0);                         //  Make Defender spawn game area transparent
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
        GameObject newDefender = Instantiate(defender, spawnPosition, Quaternion.identity);
        AudioManager.instance.PlayAudio(Sounds.AudioName.DefenderSpawn, true);
        newDefender.transform.parent = defenderParent.transform;
    }

    public void DecrementDefenderQuantity(int index)
    {
        // Decrement the defender from gamedata
        DataPersistenceManager.instance.gameData.defendersInfos[index].amt--;
        DataPersistenceManager.instance.SaveFile();
    }

    public void IncrementDefenderQuantity(int amount, int index)
    {
        // Increment the defender from gamedata
        DataPersistenceManager.instance.gameData.defendersInfos[index].amt--;
        DataPersistenceManager.instance.SaveFile();
    }


}
