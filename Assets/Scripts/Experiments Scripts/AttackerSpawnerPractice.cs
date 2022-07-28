// Script for spawner
// might think of making ir scriptableObject if it works
// Put Location orientation in this


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum SpawnerOrientation { Left, Right }

public class AttackerSpawnerPractice : MonoBehaviour
{
    // public SpawnerOrientation currentSpawnerOrientation;

    #region Try with bool value for movement

    // public bool Left, Right;
    

    private void Start()
    {
        // AttackerSpawnerPractice hereSpawnerPractice = GetComponent<AttackerSpawnerPractice>();
        
    }

    // public bool DirectionForSpawner()
    // {
    //     var spawnerSide;
    //     if (GetComponent<Transform>().position.x < 0)
    //     {
    //         orientation = SpawnerOrientation.Left;
    //         // Debug.Log("Position 1 = " + currentSpawnerOrientation);
    //     }
    //     else if (GetComponent<Transform>().position.x >= 0)
    //     {
    //         orientation = SpawnerOrientation.Right;
    //         // Debug.Log("Position 2 = " + currentSpawnerOrientation);
    //     }
    // }








    #endregion





    #region MYFirstOne
    // public void GetOrientation(SpawnerOrientation orientation)
    // {
    //     if (GetComponent<Transform>().position.x < 0)
    //     {
    //         orientation = SpawnerOrientation.Left;
    //         // Debug.Log("Position 1 = " + currentSpawnerOrientation);
    //     }
    //     else if (GetComponent<Transform>().position.x >= 0)
    //     {
    //         orientation = SpawnerOrientation.Right;
    //         // Debug.Log("Position 2 = " + currentSpawnerOrientation);
    //     }
    // }
    #endregion

}














// if (GetComponent<Transform>().position.x < 0)
// {
//     currentSpawnerOrientation = SpawnerOrientation.Left;
//     Debug.Log("Position 1 = " + currentSpawnerOrientation);
// }
// else if (GetComponent<Transform>().position.x >= 0)
// {
//     currentSpawnerOrientation = SpawnerOrientation.Right;
//     Debug.Log("Position 2 = " + currentSpawnerOrientation);
// }
