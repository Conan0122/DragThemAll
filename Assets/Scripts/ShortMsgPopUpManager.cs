/*
    Call this script in other script where
    we want to show pop up.
    We don't neccessarily need this script to be called to access pop up
    but using this script makes it clean and makes control and manager class separate.
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
