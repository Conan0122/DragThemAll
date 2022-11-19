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

    [SerializeField] GameObject attackerDeathVFX;
    Player player;
    float attackerDamage;
    bool screenShakeToggle;
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
            AudioManager.instance.PlayAudio(Sounds.AudioName.AttackerDeath, true);
            Instantiate(attackerDeathVFX, collider.transform.position, Quaternion.identity);
            player.DecreaseHealth(attackerDamage);
        }

        screenShakeToggle = PlayerPrefs.GetInt("screenShake", 1) == 1 ? true : false;
        if (!screenShakeToggle) ScreenShakeOnDamage();      // shake only if screenShakeToggle is false

        Destroy(collider.gameObject);
    }

    void ScreenShakeOnDamage()
    {
        int rand = Random.Range(1, 5);
        shakeAnimator.SetTrigger($"Shake{rand}");
    }

}
