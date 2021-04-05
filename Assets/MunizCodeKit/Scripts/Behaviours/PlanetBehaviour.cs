using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Interface;
using MunizCodeKit.Systems;

public class PlanetBehaviour : MonoBehaviour, IAttackTarget
{
    public static PlanetBehaviour instance;
    PointsSystem healthSystem;

    private void Awake()
    {
        if (instance == null) instance = this;
        healthSystem = new PointsSystem(100, 70);
    }
    public void TrashInCorrectCan()
    {
        healthSystem.AddPoints(5);
    }
    public void TrashNotInCorrectCan()
    {

    }

    public PointsSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
