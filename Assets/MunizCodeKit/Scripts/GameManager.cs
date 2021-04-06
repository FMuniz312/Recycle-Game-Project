using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Factory;

public class GameManager : MonoBehaviour
{

    public static event EventHandler onGameEnded;
    public static bool isGameRunning { get; private set; }

    private void Start()
    {
        DialogBoxController.instance.ShowDialogBox("Ouvir dizer que você pode me ajudar! Rápido! Me ajuda a tirar esses lixos", 3f);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlanetBehaviour.instance.GetHealthSystem().AddPoints(10);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlanetBehaviour.instance.GetHealthSystem().RemovePoints(10);
        }
    }

    public static void PauseGame(bool value)
    {
        isGameRunning = value;
    }


}
