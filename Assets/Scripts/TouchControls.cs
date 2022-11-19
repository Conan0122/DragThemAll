/*      Handling Touch Controls for Attackers
        Drag and Drop
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    #region Variable Initialization

    Player player;
    Rigidbody2D rb;
    Camera mainCamera;
    Collider2D myCollider;
    Animator attackerAnimator;
    Touch touch;
    Vector2 touchPos;
    Attacker attacker;
    DefenderButton defenderButton;

    float deltaX, deltaY, initialGravity;
    bool moveAllowed = false;

    #endregion

    #region Getters and Setters
    public bool MoveAllowed
    {
        get { return moveAllowed; }
        set { moveAllowed = value; }
    }
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialGravity = rb.gravityScale;
        myCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        attacker = FindObjectOfType<Attacker>();
        attackerAnimator = attacker.GetComponent<Animator>();
        defenderButton = FindObjectOfType<DefenderButton>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        DragControls();
    }

    void DragControls()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPos = mainCamera.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    AttackerDragStarted();
                    break;

                case TouchPhase.Moved:
                    AttackerDragging();
                    break;

                case TouchPhase.Ended:
                    AttackerDragReleased();
                    break;
            }
        }
    }

    void AttackerDragStarted()
    {
        if (myCollider == Physics2D.OverlapPoint(touchPos))
        {
            // Get the current touch position w.r.t. character
            AudioManager.instance.PlayAudio(Sounds.AudioName.AttackerDrag, true);
            deltaX = touchPos.x - transform.position.x;
            deltaY = touchPos.y - transform.position.y;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);        //  Make character stable while dragging
            MoveAllowed = true;
        }
    }

    void AttackerDragging()
    {
        var trailParticlesEmission = attacker.TrailParticles.emission;
        if (myCollider == Physics2D.OverlapPoint(touchPos) || MoveAllowed)
        {
            rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
            rb.velocity = new Vector2(0, 0);
            attackerAnimator.enabled = false;      //  disable animation while dragging

            if (attacker.AttackerTrails && attacker.TrailParticles)
            {
                // attacker.AttackerTrails.startColor = Color.blue;
                // attacker.AttackerTrails.endColor = Color.white;
                attacker.AttackerTrails.emitting = true;

                if (trailParticlesEmission.enabled && DataPersistenceManager.instance.gameData.IsDefaultTrailParticlesActive)
                {
                    // Here we can change in settings if we want to enable particle effects or not,
                    // turning off will improve performance.
                    trailParticlesEmission.enabled = true;
                    attacker.TrailParticles.Play();
                }
            }
        }
    }

    void AttackerDragReleased()
    {
        var trailParticlesEmission = attacker.TrailParticles.emission;
        if (myCollider == Physics2D.OverlapPoint(touchPos) || MoveAllowed)
        {
            MoveAllowed = false;
            rb.gravityScale = initialGravity;
            attackerAnimator.enabled = enabled;    //  enable animation when dropped

            if (attacker.AttackerTrails)
                attacker.AttackerTrails.emitting = false;
            if (attacker.TrailParticles)
                trailParticlesEmission.enabled = false;
        }
    }


}
