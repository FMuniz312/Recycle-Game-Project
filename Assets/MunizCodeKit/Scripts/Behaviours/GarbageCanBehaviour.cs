using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GarbageCanBehaviour : MonoBehaviour
{
    public TrashType garbageCanType;

    [Header("Tween")]
    [SerializeField] float shakeForceCorrect;
    [SerializeField] float shakeTimerCorrect;
    [SerializeField] float shakeForceWrong;
    [SerializeField] float shakeTimerWrong;

    Tween checkComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trash"))
        {
            checkComplete?.Complete();
            if (collision.gameObject.GetComponent<TrashBehaviour>().CheckGarbageCan(this)) checkComplete = transform.DOShakeRotation(shakeTimerCorrect, shakeForceCorrect);
            else
            {
                checkComplete =transform.DOShakePosition(shakeTimerWrong, shakeForceWrong);
            }
        }
    }


}

