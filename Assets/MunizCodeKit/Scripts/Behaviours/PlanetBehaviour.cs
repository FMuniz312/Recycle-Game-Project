using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Interface;
using MunizCodeKit.Systems;
using DG.Tweening;

public class PlanetBehaviour : MonoBehaviour, IAttackTarget
{
    public static PlanetBehaviour instance;
    PointsSystem healthSystem;
    public LevelSystem difficultyLevel { get; private set; }

    [Header("Balance")]
    [SerializeField] int maxHealth;
    [SerializeField] int startHealth;
    [SerializeField] int amountOfTrashPerLevel;
     [SerializeField] int healthPerLoop;
    [SerializeField] float timerHealLoop;

    float timer;
    private void Awake()
    {
        if (instance == null) instance = this;
        healthSystem = new PointsSystem(maxHealth, startHealth);
        timer = timerHealLoop;
        difficultyLevel = new LevelSystem(amountOfTrashPerLevel, 4, 1);
    }
    private void Start()
    {
        

    }
    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer += timerHealLoop;
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
