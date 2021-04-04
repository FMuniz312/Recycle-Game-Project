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
            PrefabFactory.instance.CreateItem(PrefabFactory.FactoryProduct.randomTrash);
        }
    }
}
