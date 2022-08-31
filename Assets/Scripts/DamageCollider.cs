/*      Handling walls' damage
        Destroy attacer on collide
        Perform certain task that we want to do while attacker collides
        Like- Descreasing player's health and increaing coins
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    #region Variable Initialization

    Player player;
    float attackerDamage;
    // [SerializeField] Camera gameCamera;
    [SerializeField] Animator shakeAnimator;

    #endregion

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //  Get Damage from Attacker
        //  And decrease health based on given health of the attacker
        if (collider.CompareTag("Attacker"))
        {
            attackerDamage = collider.gameObject.GetComponent<Attacker>().AttackerDamage;
            player.DecreaseHealth(attackerDamage);
        }

        if (true) ScreenShakeOnDamage();    // We can check here if Screen shake is off / on
        Destroy(collider.gameObject, 0.4f);
    }

    void ScreenShakeOnDamage()
    {
        int rand = Random.Range(1, 5);
        shakeAnimator.SetTrigger($"Shake{rand}");
    }

}
