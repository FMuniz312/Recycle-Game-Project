using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    TrashType trashType;
    Vector3 spawnPos;

    private void Start()
    {
        spawnPos = transform.position;
    }

    public void CheckGarbageCan()
    {
        RaycastHit2D info = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity);
        if(info && info.collider.CompareTag("garbageCan"))
        {
            if(trashType == info.collider.GetComponent<GarbageCanBehaviour>().garbageCanType)
            {//o tipo é o mesmo, devo destruir o lixo.

                PlanetBehaviour.TrashInCorrectCan();
                Destroy(gameObject);
            }
            else
            {
                //o tipo é diferente, o lixo volta pra terra e a terra sofre dano imediato
                PlanetBehaviour.TrashNotInCorrectCan();
            }
        }
    }

}
public enum TrashType
{
    Plastic,
    Paper,
    Metal,
    Glass
}
