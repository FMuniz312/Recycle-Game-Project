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

    [Header("Tween")]
    [SerializeField] float shakeForceCorrect;
    [SerializeField] float shakeTimerCorrect;
    [SerializeField] float shakeForceWrong;
    [SerializeField] float shakeTimerWrong;

    Tween checkComplete;

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trash"))
        {
            checkComplete?.Complete();
            if (collision.gameObject.GetComponent<TrashBehaviour>().CheckGarbageCan(this)) checkComplete = transform.DOShakeRotation(shakeTimerCorrect, shakeForceCorrect);
            else
            {
                checkComplete = transform.DOShakePosition(shakeTimerWrong, shakeForceWrong);
            }
        }
    }


}

