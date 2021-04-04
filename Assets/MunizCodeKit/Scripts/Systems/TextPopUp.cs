﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace MunizCodeKit.Systems
{
    static public class TextPopUp
    {
        static public void CreateTextPopUp(string text, Vector3 initPosition, Vector3 finalPosition, Color color, float moveSpeed = 2f, float fontSize = 40f,float duration = 1)
        {
            GameObject TextPopUpGO = GameAssetsKeeper.instance.prefabTextPopUp;
            GameObject TextPopUpGOInstantiated = UnityEngine.Object.Instantiate(TextPopUpGO, initPosition, Quaternion.identity);
            TextMeshPro textMeshPro = TextPopUpGOInstantiated.GetComponent<TextMeshPro>();
            textMeshPro.SetText(text);
            textMeshPro.fontSize = fontSize;
            TextPopUpBehaviour textPopUpBehaviour = TextPopUpGOInstantiated.GetComponent<TextPopUpBehaviour>();
            textPopUpBehaviour.Setup((finalPosition - initPosition).normalized, moveSpeed, color,duration);

        }

        static public void CreateTextPopUp(string text, Vector3 initPosition)
        {

            Vector3 finalPosition = initPosition + new Vector3(1.2f, 1.5f);
            Color color = Color.yellow;

            CreateTextPopUp(text, initPosition, finalPosition, color);

        }

        static public void CreateTextPopUp(string text, Vector3 initPosition, float moveSpeed, Color color)
        {

            Vector3 finalPosition = initPosition + new Vector3(1.2f, 1.5f);


            CreateTextPopUp(text, initPosition, finalPosition, color, moveSpeed);

        }

        static public void CreateTextPopUp(string text, Vector3 initPosition, float moveSpeed, Color color, float fontsize)
        {

            Vector3 finalPosition = initPosition + new Vector3(1.2f, 1.5f);


            CreateTextPopUp(text, initPosition, finalPosition, color, moveSpeed, fontsize);

        }

        static public void CreateTextPopUp(string text, Vector3 initPosition, float moveSpeed, Color color, float fontsize, float duration )
        {

            Vector3 finalPosition = initPosition + new Vector3(1.2f, 1.5f);


            CreateTextPopUp(text, initPosition, finalPosition, color, moveSpeed, fontsize, duration);

        }
    }
 
}