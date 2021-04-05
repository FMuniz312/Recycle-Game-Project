
 
using System.Collections.Generic;
using UnityEngine;

namespace MunizCodeKit.MonoBehaviours {

    /*
     * Easy set up for CameraFollow, it will follow the transform with zoom
     * */
    public class CameraFollowSetup : MonoBehaviour {

        [SerializeField] private CameraController cameraFollow;
        [SerializeField] private Transform followTransform;
        [SerializeField] private float zoom;

        private void Start() {
            if (followTransform == null) {
                Debug.LogError("followTransform is null! Intended?");
                cameraFollow.Setup(() => Vector3.zero, () => zoom);
            } else {
                cameraFollow.Setup(() => followTransform.position, () => zoom);
            }
        }
    }

}