/*
    Call this script in other script where
    we want to show pop up.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortMsgPopUpManager : MonoBehaviour
{
    [SerializeField] GameObject popUpComponent;
    IShortMessagePopUp popUpControlScript;
    
    void Start()
    {
        popUpControlScript = popUpComponent.GetComponentInChildren<IShortMessagePopUp>();
    }

    // Call this method from scripts where we want to show short pop ups
    public void ShowPopUpMessage(string message)
    {
        popUpControlScript.ShortMessage(message);
    }



}
