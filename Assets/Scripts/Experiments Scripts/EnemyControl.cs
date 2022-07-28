// // Script for new Input system 
// // Only for practice purpose


// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.InputSystem;

// public class EnemyControl : MonoBehaviour
// {

//     #region Events

//     // public delegate void StartTouch(Vector2 position, float time);
//     // public event StartTouch OnStartTouch;
//     // public delegate void EndTouch(Vector2 position, float time);
//     // public event EndTouch OnEndTouch;

//     #endregion

//     PlayerInputAction playerControls;
//     // nt in use yet
//     Rigidbody2D rb;
//     InputAction drag;
//     InputAction drop;


//     private void Awake()
//     {
//         playerControls = new PlayerInputAction();
//     }

//     private void OnEnable()
//     {
//         playerControls.Enable();
//         // Debug.Log("Drag detected" + drag.ReadValue<Vector2>());
//     }

//     private void OnDisable()
//     {
//         playerControls.Disable();
//     }

//     private void Start()
//     {
//         // playerControls.Player.Drag.started += ctx => StartTouchDrag(ctx);
//         // playerControls.Player.Drag.canceled += ctx => EndTouchDrag(ctx);
//     }

//     private void StartTouchDrag(InputAction.CallbackContext ctx)
//     {
//         // Debug.Log("Touch started" + ctx.ReadValue<Vector2>());
//         // Debug.Log("Touch started" + playerControls.Player.DragPosition.ReadValue<Vector2>());
//     }

//     private void EndTouchDrag(InputAction.CallbackContext ctx)
//     {
        
//     }





// }