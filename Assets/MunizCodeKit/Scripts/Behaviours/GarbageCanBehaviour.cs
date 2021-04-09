using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MunizCodeKit.Systems;
using MunizCodeKit.MonoBehaviours;
public class GarbageCanBehaviour : MonoBehaviour
{
    public SODGarbageCan sodGarbageCan;
    SpriteRenderer spriteRenderer;
    static List<SODGarbageCan> garbageCanTypeList;
    //Second Mode
    Vector3 startPos;
    public bool secondMode { get; private set; }


    //**********************//
    //ThirdMode
    public bool thirdMode { get; private set; }
    float timer;
    //**********************//
    Tween checkCompleteCollectAnim;
    Tween checkCompleteThirdModeAnim;

    public static GarbageCanBehaviour instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            FillList();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        startPos = transform.position;
        ChooseTypeRandomly();
        timer = sodGarbageCan.timerMax;
        PlanetBehaviour.instance.difficultyLevel.levelPointsSystem.OnPointsChanged += LevelPointsSystem_OnPointsChanged;

    }
    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            if (secondMode)
            {
                SecondModeUpdate();
            }
            if (thirdMode)
            {
                ThirdModeUpdate();
            }
        }
    }
    private void LevelPointsSystem_OnPointsChanged(object sender, MunizCodeKit.Systems.PointsSystem.OnPointsDataEventArgs e)
    {
        switch (e.CurrentPointsEventArgs)
        {
            case 2: ActivateNewMode(2); break; //activate garbage can second mode
            case 3: ActivateNewMode(3); break; //activate garbage can third mode      
            case 4: ActivateNewMode(4); break; //activate garbage can last mode      
        }
    }

    public void ChooseTypeRandomly()
    {
        int randomNumber = Random.Range(0, garbageCanTypeList.Count);
        sodGarbageCan = garbageCanTypeList[randomNumber];
        spriteRenderer.sprite = garbageCanTypeList[randomNumber].garbageCanSprite;
        garbageCanTypeList.RemoveAt(randomNumber);
        if (garbageCanTypeList.Count <= 0)
        {
            FillList();
        }

    }
    void FillList()
    {
        garbageCanTypeList = new List<SODGarbageCan>{
        GameAssetsKeeper.instance.sodGlassCan,
        GameAssetsKeeper.instance.sodMetalCan,
        GameAssetsKeeper.instance.sodPaperCan,
        GameAssetsKeeper.instance.sodPlasticCan
       };
    }
    void SecondModeUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += sodGarbageCan.timerMax;
            HandleAnimation(ChooseTypeRandomly);
        }

    }
    void ThirdModeUpdate()
    {
        sodGarbageCan.xPointOfstart += Time.deltaTime;

        transform.position = new Vector3(Mathf.Cos(sodGarbageCan.xPointOfstart) * sodGarbageCan.radiusMultiplier, Mathf.Sin(sodGarbageCan.xPointOfstart) * sodGarbageCan.radiusMultiplier) + startPos;

    }
    void HandleAnimation(TweenCallback action)
    {
        checkCompleteThirdModeAnim?.Complete();
        GetComponent<ParticleSystem>().Emit(50);
        Vector3 defaultScale = transform.localScale;
        checkCompleteThirdModeAnim = transform.DOScale(Vector3.zero * .1f, sodGarbageCan.delay / 2).OnComplete(() =>
           {
               action();
               transform.DOScale(defaultScale, sodGarbageCan.delay / 2);
           });

    }
    public void ActivateNewMode(int index)
    {
        if (index == 2)
        {
            thirdMode = false;
            secondMode = true;


        }
        else if (index == 3)
        {
            thirdMode = true;
            secondMode = false;

        }
        else
        {

            thirdMode = true;
            secondMode = true;
        }
        transform.position = startPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trash"))
        {
            checkCompleteCollectAnim?.Complete();
            if (collision.gameObject.GetComponent<TrashBehaviour>().CheckGarbageCan(this))
            {//Correct Can
                PlanetBehaviour.instance.difficultyLevel.AddExperience(1);
                checkCompleteCollectAnim = transform.DOShakeRotation(sodGarbageCan.shakeTimerCorrect, sodGarbageCan.shakeForceCorrect);
            }
            else
            {//Incorrect Can
                checkCompleteCollectAnim = transform.DOShakePosition(sodGarbageCan.shakeTimerWrong, sodGarbageCan.shakeForceWrong);
            }
        }
    }


}

