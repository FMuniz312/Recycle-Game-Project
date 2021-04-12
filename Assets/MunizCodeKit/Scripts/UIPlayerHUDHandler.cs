using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class UIPlayerHUDHandler : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Gradient planetsHealthColor;
    [SerializeField] Text tutorialText;

    //Quit game UI
    [SerializeField] GameObject quitPanel;
    [SerializeField] Text acceptText;
    [SerializeField] Text cancelText;
    [SerializeField] Text quitText;

    //
    float lifeEnergyFillAmount;

    private void Start()
    {
        PlanetBehaviour.instance.GetHealthSystem().OnPointsChanged += UIPlayerHUDHandler_OnPointsChanged;
        lifeEnergyFillAmount = PlanetBehaviour.instance.GetHealthSystem().GetPointsPercentage();
        UpdateHealthBarUI();
        switch (LanguageSystem.gameLanguage)
        {
            default: tutorialText.text = "Tutorial: Use your mouse to click on the garbage. Pull it in the opposite direction of your target and then let it go! Just like a slingshot!"; ; break;
            case Language.BrazilianPortuguese: tutorialText.text = "Tutorial: Use o mouse para clicar no lixo. Puxe - o na direção oposta ao seu alvo e lançe o lixo para ele!Como um estilingue"; break;

        }
    }

    private void UIPlayerHUDHandler_OnPointsChanged(object sender, MunizCodeKit.Systems.PointsSystem.OnPointsDataEventArgs e)
    {
        UpdateHealthBarUI();
    }

    void UpdateHealthBarUI()
    {
        float beforeChange = lifeEnergyFillAmount;
        float percentage = PlanetBehaviour.instance.GetHealthSystem().GetPointsPercentage();
        healthBar.fillAmount = beforeChange;

        lifeEnergyFillAmount = percentage;

        DOTween.To(() => healthBar.fillAmount, (value) => healthBar.fillAmount = value, lifeEnergyFillAmount, .5f);

        healthBar.color = planetsHealthColor.Evaluate(percentage);
    }

    public void QuitGameRequest()
    {

        switch (LanguageSystem.gameLanguage)
        {
            default:
                quitText.text = "Do you want to quit the game?";
                acceptText.text = "Yes";
                cancelText.text = "No"; break;
            case Language.BrazilianPortuguese:
                quitText.text = "Você quer sair do jogo?";
                acceptText.text = "Sim";
                cancelText.text = "Não"; break;

        }


        quitPanel.SetActive(true);

        GameManager.PauseGame(true);
    }

    public void QuitGame()
    {
        Application.Quit();

    }
    public void CloseQuitWindow()
    {
        quitPanel.SetActive(false);
        GameManager.PauseGame(false);
    }
}
