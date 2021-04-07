using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "GarbageCan")]
public class SODGarbageCan : ScriptableObject
{
    public TrashType garbageCanType;
    public Sprite garbageCanSprite;
    
    [Header("HardMode")]
    public float timerMax;
 
    [Header("HardMode Tween")]
    public float delay;


    [Header("Tween")]
    public float shakeForceCorrect;
    public float shakeTimerCorrect;
    public float shakeForceWrong;
    public float shakeTimerWrong;

}
