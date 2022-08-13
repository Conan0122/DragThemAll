//  Handling Pop up Animations

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpAnimControls : MonoBehaviour
{
    #region Variable Initialization

    [SerializeField] CanvasGroup popUpBG;
    [SerializeField] GameObject popUp;
    [SerializeField] RectTransform rectTransform;
    Vector2 initialPopupScale;

    [Header("DOTween Settings")]
    [Space(10)]
    [SerializeField] float fadeTime = 1f;
    [SerializeField] float animDuration = 1f;
    [SerializeField] float animStrength = 0.3f;

    #endregion

    private void Start()
    {
        popUpBG.alpha = 0f;
        initialPopupScale = rectTransform.localScale;
        rectTransform.localScale = new Vector2(0f, 0f);
    }

    public void OpenPopUpAnim()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(popUpBG.DOFade(1f, fadeTime))
            .Join(rectTransform.DOScale(initialPopupScale, animDuration))
            .Append(rectTransform.DOShakeScale(animDuration, animStrength, 10, 0f))
            .SetUpdate(true);
    }

    public void ClosePopUpAnim()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(popUpBG.DOFade(0, fadeTime))
            .Join(rectTransform.DOScale(new Vector2(0f, 0f), 0.1f))
            .SetUpdate(true)
            .OnComplete(SetPopUp);
    }

    void SetPopUp()
    {
        popUpBG.gameObject.SetActive(false);
        popUp.SetActive(false);
    }


}
