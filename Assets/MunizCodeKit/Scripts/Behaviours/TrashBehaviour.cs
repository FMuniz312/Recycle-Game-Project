using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MunizCodeKit.Systems;
public class TrashBehaviour : MonoBehaviour
{
    public TrashType trashType;
    [SerializeField] float minDistanceFromSpawn;
    [SerializeField] float goBackTime;
    Vector3 spawnPos;
    private void Awake()
    {
        trashType = (TrashType)Random.Range(0, 4);

        //DEBUG
        switch (trashType)
        {
            case TrashType.Glass: GetComponent<SpriteRenderer>().color = Color.green; break;
            case TrashType.Plastic: GetComponent<SpriteRenderer>().color = Color.red; break;
            case TrashType.Paper: GetComponent<SpriteRenderer>().color = Color.blue; break;
            case TrashType.Metal: GetComponent<SpriteRenderer>().color = Color.yellow; break;
        }
        //


        if (GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.None)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }

        GameManager.onGameEnded += GameManager_onGameEnded;
     
    }
     

    private void GameManager_onGameEnded(object sender, System.EventArgs e)
    {
        Destroy(this.gameObject);
    }

    private void Start()
    {
        spawnPos = transform.position;
        FollowPlanetsRotation(true);
    }
    //Checks if the trash type is the same as the garbage can type
    public bool CheckGarbageCan(GarbageCanBehaviour garbageCanBehaviour)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;


        if (trashType == garbageCanBehaviour.sodGarbageCan.garbageCanType)
        {//the type of the trash is the sameone as the garbage can.
            SoundSystem.instance.PlaySound(SoundSystem.Sound.GarbageCanCorrect);
            PlanetBehaviour.instance.TrashInCorrectCan();
            Destroy(gameObject);
            return true;
        }
        else
        {
            //the type of the trash is different from the garbage can
            PlanetBehaviour.instance.TrashNotInCorrectCan();
            GoBackToPlanet();
            SoundSystem.instance.PlaySound(SoundSystem.Sound.GarbageCanIncorrect);
            return false;
        }

    }

    void GoBackToPlanet()
    {
        TweenCallback onArrived = () =>
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            FollowPlanetsRotation(true);
            if (GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.None)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            }
        };
        transform.DOMove(spawnPos, goBackTime).OnComplete(onArrived);
    }

    void FollowPlanetsRotation(bool value)
    {
        if (value) transform.SetParent(PlanetBehaviour.instance.transform);
        else
        {
            transform.SetParent(null);
        }
    }

    private void OnDestroy()
    {
        GameManager.onGameEnded -= GameManager_onGameEnded;
    }
}
public enum TrashType
{
    Plastic,
    Paper,
    Metal,
    Glass
}
