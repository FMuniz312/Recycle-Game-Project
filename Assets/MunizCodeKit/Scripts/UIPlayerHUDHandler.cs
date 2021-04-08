using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class UIPlayerHUDHandler : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Gradient planetsHealthColor;
    float lifeEnergyFillAmount;

    private void Start()
    {
        PlanetBehaviour.instance.GetHealthSystem().OnPointsChanged += UIPlayerHUDHandler_OnPointsChanged;
        lifeEnergyFillAmount = PlanetBehaviour.instance.GetHealthSystem().GetPointsPercentage();
        UpdateHealthBarUI();

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
