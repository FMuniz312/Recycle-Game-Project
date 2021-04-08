using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class DialogBoxController : MonoBehaviour
{
    public static DialogBoxController instance { get; private set; }
    [SerializeField] GameObject panelGameObject;
    [SerializeField] Text dialogText;
    [SerializeField] Button closeDialogButton;


    [Header("Tween")]
    [SerializeField] float dialogBoxPunchForce;
    [SerializeField] float dialogBoxPunchDuration;

    Tween checkTextComplete;
    Tween checkPopUPComplete;

    private void Awake()
    {
        if (instance == null) instance = this;


    }
    private void Start()
    {
        HideDialogBox();
    }

    public void ShowDialogBox(string text, float delay)
    {
        GameManager.PauseGame(true);
        panelGameObject.SetActive(true);
        checkPopUPComplete?.Complete();
        checkPopUPComplete = panelGameObject.GetComponent<RectTransform>().DOScale(1, dialogBoxPunchDuration).
            OnComplete(() =>
            {
                checkTextComplete?.Complete();
                checkTextComplete = dialogText.DOText(text, delay);
            });
        
    }
    public void HideDialogBox()
    {
        GameManager.PauseGame(false);

        panelGameObject.GetComponent<RectTransform>().localScale = Vector3.zero * .02f;
        panelGameObject.SetActive(false);
    }



}
