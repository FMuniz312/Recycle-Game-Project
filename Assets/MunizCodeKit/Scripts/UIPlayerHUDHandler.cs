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
}
