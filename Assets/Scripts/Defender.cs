/*      Handling defender
        Duration of spawn
        Number of defender we can spawn
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defender : MonoBehaviour
{
    #region Variable Initialization

    Collider2D bodyCollider;
    Rigidbody2D myRigidbody;
    Transform myTransform;
    Animator myAnimator;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] HealthBar healthBar;

    [Tooltip("Duration in secs")][SerializeField] float defenderLifetimeDuration = 4f;
    [SerializeField] float attackerDeathDuration = 0.8f;
    [SerializeField] float timeBeforeDefenderAttacks = 3f;
    [SerializeField] int numberOfDefender = 5;

    #endregion

    #region Getters and Setters

    public int NumberOfDefender
    {
        get { return numberOfDefender; }
        set
        {
            numberOfDefender = value;
        }
    }

    #endregion

    private void Start()
    {
        bodyCollider = GetComponent<Collider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();

        FlipSprite();
        healthBar.SetMaxHealth(defenderLifetimeDuration);
    }

    private void Update()
    {
        DefenderDuration();

        if (!IsGrounded()) { return; }

        //  Make Rigidbody static 
        //  so we can't move it while dragging attackers
        myRigidbody.bodyType = RigidbodyType2D.Static;
    }

    public void DecrementDefenderQuantity()
    {
        NumberOfDefender--;
        FindObjectOfType<DefenderButton>().UpdateDefenderQuantity();
    }

    public void IncrementDefenderQuantity(int amount)
    {
        NumberOfDefender += amount;
        FindObjectOfType<DefenderButton>().UpdateDefenderQuantity();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //  Destroy other gameobject
        //  only if it is attacker and is a Melee Character
        if (other.gameObject.GetComponent<Attacker>() && this.CompareTag("Melee Character"))
        {
            StartCoroutine(MeleeDefenderAttackControls(other.gameObject));
        }
    }

    IEnumerator MeleeDefenderAttackControls(GameObject gameObject)
    {
        //  Wait for some duration before attacking attackers
        //  Run Attack anim
        //  Destroy attacker
        //  Stop Attack anim
        yield return new WaitForSeconds(timeBeforeDefenderAttacks);
        myAnimator.SetBool("attackerDetected", true);
        Destroy(gameObject, attackerDeathDuration);
        yield return new WaitForSeconds(0.2f);                      //  WaitTime before stopping "Attack" anim
        myAnimator.SetBool("attackerDetected", false);
    }

    void DefenderDuration()
    {
        //  Decrement timer till timer gets down to zero
        defenderLifetimeDuration -= 1 * Time.deltaTime;
        healthBar.SetHealth(defenderLifetimeDuration);

        if (defenderLifetimeDuration <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FlipSprite()
    {
        // This works only if characters are facing in left direction
        if (myTransform.position.x < 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (myTransform.position.x >= 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

    bool IsGrounded()
    {
        if (bodyCollider.IsTouchingLayers(groundLayerMask))
        {
            return true;
        }
        return false;
    }



}
