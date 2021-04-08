using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Interface;
using MunizCodeKit.Systems;

public class PlanetBehaviour : MonoBehaviour, IAttackTarget
{
    public static PlanetBehaviour instance;
    PointsSystem healthSystem;
    [SerializeField] int healthPerLoop;
    [SerializeField] float timerMax;
    float timer;
    private void Awake()
    {
        if (instance == null) instance = this;
        healthSystem = new PointsSystem(200, 170);
        timer = timerMax;
    }
    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer += timerMax;
                healthSystem.AddPoints(healthPerLoop);
            }
        }

    }
    public void TrashInCorrectCan()
    {
        healthSystem.AddPoints(2);
    }
    public void TrashNotInCorrectCan()
    {

    }

    public PointsSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
