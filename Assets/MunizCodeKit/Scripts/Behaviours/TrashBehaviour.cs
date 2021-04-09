using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MunizCodeKit.Systems;
public class TrashBehaviour : MonoBehaviour
{
    [SerializeField] SODTrash sodTrash;
    static List<SODTrash> trashTypeList;


     

    public bool canThrow;
    float timer;
    Vector3 spawnPos;
    PointsSystem planetHealthSystem;
    TrashBehaviour instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            FillList();
        }
         
        

        if (GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.None)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }

        GameManager.onGameEnded += GameManager_onGameEnded;
        GameManager.cleanGame += GameManager_cleanGame; 

    }
   

    private void GameManager_cleanGame(object sender, System.EventArgs e)
    {
        Destroy(this.gameObject);

    }

    private void GameManager_onGameEnded(object sender, System.EventArgs e)
    {
        Destroy(this.gameObject);
    }

    private void Start()
    {
        canThrow = true;
        spawnPos = transform.position;
        FollowPlanetsRotation(true);
        planetHealthSystem = PlanetBehaviour.instance.GetHealthSystem();
        ChooseTypeRandomly();
         
    }

    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer += sodTrash.timerMaxLoop;
                planetHealthSystem.RemovePoints(sodTrash.damagePerLoop);


            }
        }
    }
    //Checks if the trash type is the same as the garbage can type
    public bool CheckGarbageCan(GarbageCanBehaviour garbageCanBehaviour)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;


        if (sodTrash.trashType == garbageCanBehaviour.sodGarbageCan.garbageCanType)
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
            canThrow = true;
        };
        transform.DOMove(spawnPos, sodTrash.goBackTime).OnComplete(onArrived);
    }

    public void ChooseTypeRandomly()
    {
        int randomNumber = Random.Range(0, trashTypeList.Count);
        sodTrash = trashTypeList[randomNumber];
        GetComponent<SpriteRenderer>().sprite = trashTypeList[randomNumber].image;
        trashTypeList.RemoveAt(randomNumber);
        if (trashTypeList.Count <= 0)
        {
            FillList();
        }

    }
    void FillList()
    {
        trashTypeList = new List<SODTrash>{
        GameAssetsKeeper.instance.sodGlassTrash,
        GameAssetsKeeper.instance.sodMetalTrash,
        GameAssetsKeeper.instance.sodPaperTrash,
        GameAssetsKeeper.instance.sodPlasticTrash
       };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("invisibleWall"))
        {
            GoBackToPlanet();
        }
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
        GameManager.cleanGame -= GameManager_cleanGame;

    }
}
public enum TrashType
{
    Plastic,
    Paper,
    Metal,
    Glass
}
