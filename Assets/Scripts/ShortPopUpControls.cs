/*
    Script for handling Short message pop up UI element.
    Controls like: Duration for Pop UI element,
    Ui text that needs to be assigned.
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ShortPopUpControls : MonoBehaviour, IShortMessagePopUp
{
    [SerializeField] float popUpDuration = 2f;
    [SerializeField] TextMeshProUGUI popUpText;
    [SerializeField] Image bgImage;

    [Header("DotWeen Anim settings")]
    [SerializeField] float fadeDuration = 1f;


    public void ShortMessage(string popUpMessage)
    {
        StartCoroutine(DisplayPopUp(popUpMessage));
    }

    // Setting duration for Pop Up
    IEnumerator DisplayPopUp(string message)
    {
        popUpText.text = message;
        popUpText.gameObject.SetActive(true);
        bgImage.gameObject.SetActive(true);
        popUpText.alpha = 0f;   // Make text alpha down to zero first
        popUpText.DOFade(1f, fadeDuration);
        yield return new WaitForSeconds(popUpDuration);

        Sequence animSeq = DOTween.Sequence();
        animSeq.Append(popUpText.DOFade(0f, fadeDuration))
                .OnComplete( () => popUpText.gameObject.SetActive(false));
                
        bgImage.gameObject.SetActive(false);
    }


}
