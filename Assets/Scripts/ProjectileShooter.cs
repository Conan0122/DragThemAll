//  To be added in a character that has shooting ability

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    #region Variable Initialization

    Animator myAnimator;

    [SerializeField] GameObject projectileToShoot, projectilePosition;
    [SerializeField] float durationToPauseAnim = 0.8f;
    float defaultAnimatorSpeed;

    #endregion

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        defaultAnimatorSpeed = myAnimator.speed;
    }

    public void Fire()
    {
        Instantiate(projectileToShoot, projectilePosition.transform.position, Quaternion.identity);
    }

    public IEnumerator PauseAnim()
    {
        myAnimator.speed = 0;
        yield return new WaitForSeconds(durationToPauseAnim);
        myAnimator.speed = defaultAnimatorSpeed;
    }
    

}
