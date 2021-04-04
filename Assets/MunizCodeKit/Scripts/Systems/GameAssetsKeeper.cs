using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MunizCodeKit.Systems
{
    public class GameAssetsKeeper : MonoBehaviour
    {
        static GameAssetsKeeper _i;
        static public GameAssetsKeeper instance
        {
            get
            {
                if (_i == null) _i = (Instantiate(Resources.Load("GameAssetsKeeper")) as GameObject).GetComponent<GameAssetsKeeper>();
                return _i;
                        }
            set { }
        }



        public GameObject prefabTextPopUp;


    }
}