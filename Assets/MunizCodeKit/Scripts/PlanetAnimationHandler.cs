using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Systems;
 
public class PlanetAnimationHandler : MonoBehaviour
{
    [Header("Sprite Changes")]
    [SerializeField] Sprite[] arraySpritesPlanet;


    [Header("Tween")]
    [SerializeField] int rotationSpeed;

    [Header("Particle System")]
    [SerializeField] Gradient gradientHealth;


    ParticleSystem planetsParticleSystem;
    SpriteRenderer spriteRenderer;
    PointsSystem planetHealthSystem;

    float rotationHolder;
    Quaternion target;

    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        planetHealthSystem = PlanetBehaviour.instance.GetHealthSystem();
         spriteRenderer.sprite = arraySpritesPlanet[arraySpritesPlanet.Length - 1];
        planetsParticleSystem = GetComponent<ParticleSystem>();
        planetsParticleSystem.startColor = gradientHealth.Evaluate(planetHealthSystem.GetPointsPercentage());
        planetHealthSystem.OnPointsChanged += PlanetAnimationHandler_OnPointsChanged;
    }

    private void PlanetAnimationHandler_OnPointsChanged(object sender, PointsSystem.OnPointsDataEventArgs e)
    {
        planetsParticleSystem.startColor = gradientHealth.Evaluate(planetHealthSystem.GetPointsPercentage());
        float index = (planetHealthSystem.GetPointsPercentage() * 10);
        spriteRenderer.sprite = arraySpritesPlanet[(int)index];

    }

    void Update()
    {
        if (GameManager.isGameRunning)
        {
            rotationHolder += Time.deltaTime * -rotationSpeed;
            target = Quaternion.Euler(0, 0, rotationHolder);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
        }
    }
}
