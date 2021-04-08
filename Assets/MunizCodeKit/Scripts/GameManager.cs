using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Factory;
using MunizCodeKit.MonoBehaviours;

public class GameManager : MonoBehaviour
{
    //GameManager Main
    public static event EventHandler onGameEnded;
    public static bool isGameRunning { get; private set; }
    //

    CutsceneHandler cutsceneHandler;

    private void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
        cutsceneHandler.PlayCutScene1();
        PlanetBehaviour.instance.GetHealthSystem().OnPointsZero += OnPlanetDied;
        PlanetBehaviour.instance.difficultyLevel.levelPointsSystem.OnPointsChanged += LevelPointsSystem_OnPointsChanged;
    }

    private void LevelPointsSystem_OnPointsChanged(object sender, MunizCodeKit.Systems.PointsSystem.OnPointsDataEventArgs e)
    {
        switch (e.CurrentPointsEventArgs)
        {
            case 2:; break; //activate garbage can second mode
            case 3:; break; //activate garbage can third mode      
        }
    }

    private void OnPlanetDied(object sender, MunizCodeKit.Systems.PointsSystem.OnPointsDataEventArgs e)
    {
        onGameEnded?.Invoke(this, EventArgs.Empty);
        PauseGame(true);
    }

    private void Update()
    {

    }

    public static void PauseGame(bool value)
    {
        isGameRunning = !value;
    }


}
