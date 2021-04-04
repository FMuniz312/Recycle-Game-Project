using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MunizCodeKit.Systems;
public class TrashBehaviour : MonoBehaviour
{
   public TrashType trashType;
    Vector3 spawnPos;
    public float goBackTime;
    private void Awake()
    {
        trashType = (TrashType)Random.Range(0, 4);
    }
    private void Start()
    {
        spawnPos = transform.position;
    }

    //Checks if the trash type is the same as the garbage can type
    public void CheckGarbageCan()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;

        RaycastHit2D info = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity);

        if (info && info.collider.CompareTag("garbageCan"))
        {
           
            if (trashType == info.collider.GetComponent<GarbageCanBehaviour>().garbageCanType)
            {//the type of the trash is the sameone as the garbage can.
                SoundSystem.instance.PlaySound(SoundSystem.Sound.GarbageCanCorrect);
                PlanetBehaviour.TrashInCorrectCan();
                Destroy(gameObject);
            }
            else
            {
                //the type of the trash is different from the garbage can
                PlanetBehaviour.TrashNotInCorrectCan();
                GoBackToPlanet(() => gameObject.GetComponent<Collider2D>().enabled = true);
                SoundSystem.instance.PlaySound(SoundSystem.Sound.GarbageCanIncorrect);
            }
        }
        GoBackToPlanet(() => gameObject.GetComponent<Collider2D>().enabled = true);
    }

    void GoBackToPlanet(TweenCallback onArrived)
    {
        transform.DOMove(spawnPos, goBackTime).OnComplete(onArrived);
    }

}
public enum TrashType
{
    Plastic,
    Paper,
    Metal,
    Glass
}
