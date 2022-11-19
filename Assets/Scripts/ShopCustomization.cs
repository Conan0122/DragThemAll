using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCustomization : MonoBehaviour
{
    SceneControls sceneControls;
    [SerializeField] Image selectedImage;

    void Start()
    {
        sceneControls = FindObjectOfType<SceneControls>();
        DataPersistenceManager.instance.LoadFile();

        selectedImage.enabled = false;
        if (DataPersistenceManager.instance.gameData.IsDefaultTrailParticlesActive)
        {
            selectedImage.enabled = true;
        }
    }

    public void TrailParticlesToggle()
    {
        if (DataPersistenceManager.instance.gameData.BoughtTrailsAlready)
        {
            Debug.Log($"Already bought");
            DataPersistenceManager.instance.gameData.IsDefaultTrailParticlesActive = !DataPersistenceManager.instance.gameData.IsDefaultTrailParticlesActive;
            selectedImage.enabled = DataPersistenceManager.instance.gameData.IsDefaultTrailParticlesActive;

            Debug.Log($"Trail Particles status" + DataPersistenceManager.instance.gameData.IsDefaultTrailParticlesActive);

            DataPersistenceManager.instance.SaveFile();
        }
        else
        {
            Debug.Log($"Pop up");
            sceneControls.BuyTrailPopUp();
        }



    }

}
