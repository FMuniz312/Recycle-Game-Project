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

    //ThirdMode
    public bool thirdMode { get; private set; }
    float timer;
    //**********************//
    //Second Mode
    public bool secondMode { get; private set; }
    Vector3 startPos;

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
         
    }
    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            if (thirdMode)
            {
                ThirdModeUpdate();
            }
            if (secondMode)
            {
                SecondModeUpdate();
            }
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
    void ThirdModeUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += sodGarbageCan.timerMax;
            HandleAnimation(ChooseTypeRandomly);
        }

    }
    void SecondModeUpdate()
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
            secondMode = true;
            Camera.main.GetComponent<CameraController>().SetCameraZoom(Camera.main.orthographicSize + 10f);
        }
        else
        {
            thirdMode = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trash"))
        {
            checkCompleteCollectAnim?.Complete();
            if (collision.gameObject.GetComponent<TrashBehaviour>().CheckGarbageCan(this)) checkCompleteCollectAnim = transform.DOShakeRotation(sodGarbageCan.shakeTimerCorrect, sodGarbageCan.shakeForceCorrect);
            else
            {
                checkCompleteCollectAnim = transform.DOShakePosition(sodGarbageCan.shakeTimerWrong, sodGarbageCan.shakeForceWrong);
            }
        }
    }


}

