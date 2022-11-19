/*      Handling Attacker Details
        Attacker Destroyer
        Attacker Health
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Attacker : MonoBehaviour
{
    #region Variable Initialization

    Destroyer destroyer;
    TaskGiver taskGiver;
    TouchControls touchControls;
    Rigidbody2D myRb;
    Collider2D bodyCollider;
    Animator myAnimator;
    Transform myTransform;

    [Header("Attacker data")]
    [SerializeField] GameObject[] attackerDestroyers;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] TrailRenderer attackerTrails;
    [SerializeField] ParticleSystem trailParticles;         //  Add this on customization section in shop
    [Tooltip("Duration before attacker starts moving")]
    [SerializeField] float delayBeforeAttackerMoves = .3f;
    [SerializeField] float attackerMovementSpeed = 2f;
    [Tooltip("Extra height for raycast to check ground touch")]
    [SerializeField] float extraRaycastHeight = 0.2f;   // Extending raycast for more accuracy
    [SerializeField] float attackerDamage;

    bool isIncremented = false;

    #endregion

    #region Getters and Setters
    public float AttackerDamage
    {
        get { return attackerDamage; }
        set { attackerDamage = value; }
    }

    public TrailRenderer AttackerTrails
    {
        get { return attackerTrails; }
        set { attackerTrails = value; }
    }

    public ParticleSystem TrailParticles
    {
        get { return trailParticles; }
        set { trailParticles = value; }
    }

    #endregion

    void Start()
    {
        taskGiver = FindObjectOfType<TaskGiver>();
        touchControls = FindObjectOfType<TouchControls>();
        bodyCollider = GetComponent<Collider2D>();
        myRb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
        destroyer = FindObjectOfType<Destroyer>();
    }

    void Update()
    {
        if (!destroyer.IsDestroyerActive)
        {
            return;
        }
        DestroyAttacker();
    }

    void FixedUpdate()
    {
        FlipSprite();
        if (IsGrounded())
        {
            myAnimator.enabled = enabled;
            StartCoroutine(MoveAttacker());
        }
        else
        {
            // Debug.Log("not grounded");
        }
    }

    IEnumerator MoveAttacker()
    {
        //  Wait for some duration before character moves after landing
        yield return new WaitForSeconds(delayBeforeAttackerMoves);

        //  Move character based on spawned position
        if (myTransform.position.x < 0 && IsGrounded())
        {
            MoveTowardsLeft();
        }
        else if (myTransform.position.x > 0 && IsGrounded())
        {
            MoveTowardsRight();
        }
    }

    void MoveTowardsLeft()
    {
        myRb.velocity = new Vector2(-attackerMovementSpeed, 0f);
    }

    void MoveTowardsRight()
    {
        myRb.velocity = new Vector2(attackerMovementSpeed, 0f);
    }

    void DestroyAttacker()
    {
        //  Destroy attacker when it touches any of the destroyer
        foreach (var item in attackerDestroyers)
        {
            // Center the position of attacker when it touches destroyer
            if (touchControls.MoveAllowed == false)
            {
                if (Mathf.Abs(this.transform.localPosition.x -
                item.transform.localPosition.x) <= 1.5f &&
                Mathf.Abs(this.transform.localPosition.y -
                item.transform.localPosition.y) <= 1.5f)
                {
                    this.transform.position = new Vector2(item.transform.position.x, item.transform.position.y);
                    myRb.gravityScale = 0;
                    Destroy(this.gameObject, 1f);   //  Can change hardcoded death duration to serialized field
                    
                    if (!isIncremented)
                    {
                        AudioManager.instance.PlayAudio(Sounds.AudioName.DestroyerAbsorb, true);
                        taskGiver.IncrementQuest(this.gameObject.name, Quest.GoalType.Kill);
                        isIncremented = true;
                    }
                }
            }
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(bodyCollider.bounds.center,
                                                    Vector2.down,
                                                    bodyCollider.bounds.extents.y + extraRaycastHeight,
                                                    groundLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(bodyCollider.bounds.center, Vector2.down * (bodyCollider.bounds.extents.y + extraRaycastHeight), rayColor);
        return raycastHit.collider != null;
    }

    Vector2 FlipSprite()
    {
        // This works only if characters are facing in left direction
        if (myTransform.position.x < 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (myTransform.position.x > 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        return transform.localScale;
    }



}
