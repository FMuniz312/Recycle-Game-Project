using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.MonoBehaviours;
using DG.Tweening;

public class CutsceneHandler : MonoBehaviour
{
    CameraController cameraController;


    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    //Cutscene 1 **************/
    public void PlayCutScene1()
    {
         
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Narrator1, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Narrator2, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Narrator3, () =>
          DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Narrator4, () =>
         MoveCloserToPlanet()))));

    }
    private void MoveCloserToPlanet()
    {
        cameraController.SetCameraFollowPosition(new Vector3(0, 0, -10));
        cameraController.SetCameraZoom(45);
        StartCoroutine(WaitingForThePlanetToBeOnScreen(3));
    }

    IEnumerator WaitingForThePlanetToBeOnScreen(float secondstowait)
    {
        yield return new WaitForSeconds(secondstowait);
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet1, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet2, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet3, () =>
         GameManager.PauseGame(false))));

    }
    /**************************/

}
