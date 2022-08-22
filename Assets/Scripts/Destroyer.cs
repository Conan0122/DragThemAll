//  Handling destroyer mechanism

using System.Collections;
using UnityEngine;
using TMPro;

public class Destroyer : MonoBehaviour
{
    #region Variable Initialization
    GameTimer gameTimer;
    SpriteRenderer mySpriteRenderer;
    Color32 myColor;
    TextMeshProUGUI cooldownText;
    [SerializeField] TextMeshProUGUI timerText;
    Animator myAnimator;        // Destroyer animator
    Animator myTimerAnimator;   // Destroyer's Timer animator

    [Header("Destroyer Data")]
    [SerializeField] TMP_ColorGradient redColorGradient;
    [SerializeField] TMP_ColorGradient greenColorGradient;
    [SerializeField] float destroyerCooldownDuration = 3;
    bool isDestroyerActive;

    #endregion

    #region Getters and setters
    public bool IsDestroyerActive
    {
        get { return isDestroyerActive; }
        set { isDestroyerActive = value; }
    }

    #endregion

    void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        cooldownText = GetComponentInChildren<TextMeshProUGUI>();
        myAnimator = GetComponent<Animator>();
        myTimerAnimator = timerText.GetComponent<Animator>();

        StartCoroutine(DestroyerTimerMechanismm());
    }

    IEnumerator DestroyerTimerMechanismm()
    {
        while (!gameTimer.LevelTimerIsReached)
        {
            // Disable destroyer
            // Disable Timer
            // Reset Timer Anim
            myAnimator.speed = 0;
            IsDestroyerActive = false;
            mySpriteRenderer.color = new Color32(159, 159, 159, 130);
            cooldownText.alpha = 0;
            myTimerAnimator.enabled = false;
            myTimerAnimator.Play("Destroyer Timer_anim", -1, 0f);


            // Wait for CD
            yield return new WaitForSeconds(destroyerCooldownDuration);

            // Run the timer for 3 Secs
            // Enable Timer text
            // Reset Timer Anim
            for (int i = 3; i >= 1; i--)
            {
                cooldownText.alpha = 1;
                cooldownText.colorGradientPreset = greenColorGradient;  // Change Text gradient to green
                cooldownText.text = i.ToString();
                myTimerAnimator.enabled = true;
                myTimerAnimator.Play("Destroyer Timer_anim", -1, 0f);

                // Wait for a second
                yield return new WaitForSeconds(1f);
            }

            // Enable destroyer
            // Run Destroyer anim
            // Disable Timer text
            mySpriteRenderer.color = new Color32(255, 255, 255, 255);
            IsDestroyerActive = true;
            myAnimator.speed = 1;
            cooldownText.alpha = 0;
            myTimerAnimator.enabled = false;

            // Wait for CD
            yield return new WaitForSeconds(destroyerCooldownDuration);

            // Run the timer for 3 Secs
            // Enable Timer text
            // Reset Timer Anim
            for (int i = 3; i >= 1; i--)
            {
                cooldownText.alpha = 1;
                myTimerAnimator.enabled = true;
                cooldownText.colorGradientPreset = redColorGradient;    // Change Text gradient to red
                cooldownText.text = i.ToString();
                myTimerAnimator.Play("Destroyer Timer_anim", -1, 0f);

                // Wait for a second
                yield return new WaitForSeconds(1f);
            }
        }
    }

}
