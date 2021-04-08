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
        isGameRunning = true;
        DialogSystem.instance.StartDialog(DialogSystem.Dialog.firstLevel, () => DialogSystem.instance.StartDialog(DialogSystem.Dialog.secondLevel));
    }
    private void Update()
    {

    }

    public static void PauseGame(bool value)
    {
        isGameRunning = !value;
    }


}
