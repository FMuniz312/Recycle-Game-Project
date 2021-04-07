using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MunizCodeKit.Systems;
public class GarbageCanBehaviour : MonoBehaviour
{
    public SODGarbageCan sodGarbageCan;
    SpriteRenderer spriteRenderer;
    static List<SODGarbageCan> garbageCanTypeList;


    float timer;
    public bool hardMode { get; private set; }
     

    Tween checkCompleteCollectAnim;
    Tween checkCompleteHardModeAnim;

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
        ChooseTypeRandomly();
        timer = sodGarbageCan.timerMax;
        ActivateHardMode();
    }
    private void Update()
    {
        if (hardMode)
        {
            HardModeUpdate();
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
    void HardModeUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += sodGarbageCan.timerMax;
            HandleAnimation(ChooseTypeRandomly);
        }

    }
    void HandleAnimation(TweenCallback action)
    {
        checkCompleteHardModeAnim?.Complete();
        GetComponent<ParticleSystem>().Emit(50);
        Vector3 defaultScale = transform.localScale;
        checkCompleteHardModeAnim = transform.DOScale(Vector3.zero * .1f, sodGarbageCan.delay / 2).OnComplete(() =>
           {
               action();
               transform.DOScale(defaultScale, sodGarbageCan.delay / 2);
           });

    }
    public void ActivateHardMode()
    {
        hardMode = true;
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

