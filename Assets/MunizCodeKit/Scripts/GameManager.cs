using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Factory;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlanetBehaviour.instance.GetHealthSystem().AddPoints(10);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlanetBehaviour.instance.GetHealthSystem().RemovePoints(10);
        }
    }
}
