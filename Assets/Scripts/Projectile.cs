using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variable Initialization

    Transform myTransform;
    Rigidbody2D myRb;

    [SerializeField] float projectileSpeed;
    float xSpeedOfProjectile;
    bool collidedWithAttacker = true;

    #endregion

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRb = GetComponent<Rigidbody2D>();

        FlipSprite();
        xSpeedOfProjectile = this.transform.localScale.x * projectileSpeed;
    }

    void FixedUpdate()
    {
        moveProjectile();
    }

    void moveProjectile()
    {
        //  TODO move projectile in forward direction
        myRb.velocity = new Vector2(xSpeedOfProjectile, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attacker") && collidedWithAttacker == true)
        {
            //  Destroy both, projectile as well as attacker
            //  check for bool so we only destroy one attacker at once
            collidedWithAttacker = false;
            Destroy(other.gameObject);
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

}
