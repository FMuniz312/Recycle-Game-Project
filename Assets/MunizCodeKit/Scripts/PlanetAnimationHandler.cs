using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Systems;

public class PlanetAnimationHandler : MonoBehaviour
{
    [Header("Tween")]
    [SerializeField] int rotationSpeed;

    [Header("Particle System")]
    [SerializeField] Gradient gradientHealth;


    ParticleSystem planetsParticleSystem;



    float rotationHolder;
    Quaternion target;

    private void Start()
    {
        planetsParticleSystem = GetComponent<ParticleSystem>();
        planetsParticleSystem.startColor = gradientHealth.Evaluate(PlanetBehaviour.instance.GetHealthSystem().GetPointsPercentage());
        PlanetBehaviour.instance.GetHealthSystem().OnPointsChanged += PlanetAnimationHandler_OnPointsChanged;
    }

    private void PlanetAnimationHandler_OnPointsChanged(object sender, PointsSystem.OnPointsDataEventArgs e)
    {
        planetsParticleSystem.startColor = gradientHealth.Evaluate(PlanetBehaviour.instance.GetHealthSystem().GetPointsPercentage());

    }

    void Update()
    {
        rotationHolder += Time.deltaTime * -rotationSpeed;
        target = Quaternion.Euler(0, 0, rotationHolder);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
    }
}
