using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Factory;
using MunizCodeKit.MonoBehaviours;
using MunizCodeKit.Systems;

public class GameManager : MonoBehaviour
{
    //GameManager Main
    public static event EventHandler onGameEnded;
    public static event EventHandler cleanGame;
    public static bool isGameRunning { get; private set; }
    //

     

    CutsceneHandler cutsceneHandler;

    private void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
        cutsceneHandler.PlayCutScene1();
        PlanetBehaviour.instance.GetHealthSystem().OnPointsZero += OnPlanetDied;
        PlanetBehaviour.instance.difficultyLevel.levelPointsSystem.OnPointsChanged += LevelPointsSystem_OnPointsChanged;
        PlanetBehaviour.instance.difficultyLevel.experiencePointsSystem.OnPointsMax += ExperiencePointsSystem_OnPointsMax;
       
     }

    private void ExperiencePointsSystem_OnPointsMax(object sender, MunizCodeKit.Systems.PointsSystem.OnPointsDataEventArgs e)
    {
        if(PlanetBehaviour.instance.difficultyLevel.levelPointsSystem.GetPointsPercentage() == 1) // max level and max trash collected 
        {//Win game
            CleanGame();
            cutsceneHandler.PlayCutSceneWin();

        }
    }

    private void LevelPointsSystem_OnPointsChanged(object sender, MunizCodeKit.Systems.PointsSystem.OnPointsDataEventArgs e)
    {
        CleanGame();
        switch (e.CurrentPointsEventArgs)
        {
            case 2:
                SoundSystem.instance.PlaySound(SoundSystem.Sound.RoundWin);
                cutsceneHandler.PlayCutScene2();
                ; break;
            case 3:
                SoundSystem.instance.PlaySound(SoundSystem.Sound.RoundWin);
                cutsceneHandler.PlayCutScene3();
                ; break;
            case 4:
                SoundSystem.instance.PlaySound(SoundSystem.Sound.RoundWin);
                cutsceneHandler.PlayCutScene4();
                ; break;
        }
    }

    private void OnPlanetDied(object sender, MunizCodeKit.Systems.PointsSystem.OnPointsDataEventArgs e)
    {
        SoundSystem.instance.PlaySound(SoundSystem.Sound.RoundLose);
        CleanGame();
        onGameEnded?.Invoke(this, EventArgs.Empty);
        cutsceneHandler.PlayCutSceneLose();

    }

    private void Update()
    {

    }

    public static void PauseGame(bool value)
    {
        isGameRunning = !value;
    }
    public static void CleanGame()
    {
        cleanGame?.Invoke(null, EventArgs.Empty);
        PauseGame(true);
       
    }
    
   

}
 