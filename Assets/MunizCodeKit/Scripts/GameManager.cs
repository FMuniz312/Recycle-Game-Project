using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Factory;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlanetBehaviour.instance.GetHealthSystem().AddPoints(10);
            MunizCodeKit.Systems.TextPopUp.CreateTextPopUp("Ouvir dizer que você pode me ajudar! Rápido! Me ajuda a tirar esses lixos", Vector3.zero);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlanetBehaviour.instance.GetHealthSystem().RemovePoints(10);
        }
    }
}
