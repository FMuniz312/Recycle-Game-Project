using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIPlayerHUDHandler : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Gradient planetsHealthColor;

    private void Start()
    {
        PlanetBehaviour.instance.GetHealthSystem().OnPointsChanged += UIPlayerHUDHandler_OnPointsChanged;
        UpdateHealthBarUI();


    }

    private void UIPlayerHUDHandler_OnPointsChanged(object sender, MunizCodeKit.Systems.PointsSystem.OnPointsDataEventArgs e)
    {
        UpdateHealthBarUI();
    }

    void UpdateHealthBarUI()
    {
        healthBar.fillAmount = PlanetBehaviour.instance.GetHealthSystem().GetPointsPercentage();
        healthBar.color = planetsHealthColor.Evaluate(PlanetBehaviour.instance.GetHealthSystem().GetPointsPercentage());
    }
}
