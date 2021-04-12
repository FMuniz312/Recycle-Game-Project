using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "GarbageCan")]
public class SODGarbageCan : ScriptableObject
{
    public TrashType garbageCanType;
    public Sprite garbageCanSpritePortuguese;
    public Sprite garbageCanSpriteEnglish;
    public int healAmount;
    [Header("SecondMode")]
    public float timerMax;

    [Header("ThirdMode")]
    public float xPointOfstart;
    public float radiusMultiplier;

    [Header("ThirdMode Tween")]
    public float delay;


    [Header("Tween")]
    public float shakeForceCorrect;
    public float shakeTimerCorrect;
    public float shakeForceWrong;
    public float shakeTimerWrong;

}
