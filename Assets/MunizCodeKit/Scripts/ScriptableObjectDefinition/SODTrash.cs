using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Trash")]
public class SODTrash : ScriptableObject
{
    public TrashType trashType;
    public Sprite image;

    [Header("Balance")]
    public float minDistanceFromSpawn;//so the player doesn't have to wait 5 sec when he accidentally clicked on the trash and then clicked off
    public float goBackTime; //How long it takes to go back to the planet once they missed the can
    public float timerMaxLoop; // How long for each damage loop to repeat
    public int damagePerLoop; // Damage dealt to the planets life per loop

    [Header("Tween")]
    public float scaleUpDelay;
}

