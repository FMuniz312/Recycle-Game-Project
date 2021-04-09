using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MunizCodeKit.MonoBehaviours;
using DG.Tweening;

public class CutsceneHandler : MonoBehaviour
{
    CameraController cameraController;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject particleEffectBackGround;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject tutorialText;

    Vector3 rectTutorialFinalPos;

    const float NEAR_ZOOM = 30f;
    const float NORMAL_ZOOM = 45f;
    const float FAR_ZOOM = 55f;

    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }
    #region Cutscene1
    //Cutscene 1 **************/
    public void PlayCutScene1()
    {
        rectTutorialFinalPos = tutorialText.GetComponent<RectTransform>().anchoredPosition;

        tutorialText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,270);
        tutorialText.SetActive(true);


        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Narrator1, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Narrator2, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Narrator3, () =>
          DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Narrator4, () =>
         MoveCloserToPlanet()))));

    }
    private void MoveCloserToPlanet()
    {
        tutorialText.GetComponent<RectTransform>().DOAnchorPos(rectTutorialFinalPos, 1);
        cameraController.SetCameraFollowPosition(new Vector3(0, 0, -10));
        cameraController.SetCameraZoom(NORMAL_ZOOM);
        particleEffectBackGround.SetActive(true);
        healthBar.SetActive(true);

        StartCoroutine(WaitingForThePlanetToBeOnScreen(3));
    }

    IEnumerator WaitingForThePlanetToBeOnScreen(float secondstowait)
    {

        yield return new WaitForSeconds(secondstowait);
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet1, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet2, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet3, () =>
         GameManager.PauseGame(false))));
        Color alpha = tutorialText.GetComponent<Text>().color;
        alpha.a = 0;
        tutorialText.GetComponent<Text>().DOColor(alpha, 1).OnComplete(() => tutorialText.SetActive(false));



    }
    /**************************/
    #endregion

    #region Cutscene2
    public void PlayCutScene2()
    {
        cameraController.SetCameraFollowPosition(new Vector3(PlanetBehaviour.instance.gameObject.transform.position.x, PlanetBehaviour.instance.gameObject.transform.position.y, -10));
        cameraController.SetCameraZoom(NEAR_ZOOM);
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Enemy3, () =>
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Enemy4, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet6, () =>
          DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet7, () =>
          {
              cameraController.SetCameraFollowPosition(new Vector3(0, 0, -10));
              cameraController.SetCameraZoom(NORMAL_ZOOM);
              GameManager.PauseGame(false);
          }
          ))));
    }
    #endregion

    #region Cutscene3
    public void PlayCutScene3()
    {

        cameraController.SetCameraFollowPosition(new Vector3(PlanetBehaviour.instance.gameObject.transform.position.x, PlanetBehaviour.instance.gameObject.transform.position.y, -10));
        cameraController.SetCameraZoom(NEAR_ZOOM);
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Enemy1, () =>
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet4, () =>
         DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Enemy2, () =>
          DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet5, () =>
          {
              cameraController.SetCameraFollowPosition(new Vector3(0, 0, -10));
              cameraController.SetCameraZoom(FAR_ZOOM);
              GameManager.PauseGame(false);
          }
          ))));

    }
    #endregion
    #region Cutscene4
    public void PlayCutScene4()
    {
        cameraController.SetCameraFollowPosition(new Vector3(PlanetBehaviour.instance.gameObject.transform.position.x, PlanetBehaviour.instance.gameObject.transform.position.y, -10));
        cameraController.SetCameraZoom(NORMAL_ZOOM);
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Enemy5, () =>
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.Planet8, () =>
          {
              cameraController.SetCameraFollowPosition(new Vector3(0, 0, -10));
              cameraController.SetCameraZoom(FAR_ZOOM);
              GameManager.PauseGame(false);
          }
          ));

    }
    #endregion
    #region CutsceneWin
    public void PlayCutSceneWin()
    {
        winPanel.SetActive(true);
        cameraController.SetCameraFollowPosition(new Vector3(PlanetBehaviour.instance.gameObject.transform.position.x, PlanetBehaviour.instance.gameObject.transform.position.y, -10));
        cameraController.SetCameraZoom(NEAR_ZOOM);
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.WinGame1, () =>
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.WinGame2, () => UnityEngine.SceneManagement.SceneManager.LoadScene(1), "Jogar de novo"));

    }
    #endregion

    #region CutsceneLose
    public void PlayCutSceneLose()
    {
        losePanel.SetActive(true);

        cameraController.SetCameraFollowPosition(new Vector3(0, 0, -10));
        cameraController.SetCameraZoom(FAR_ZOOM);
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.LoseGame1, () =>
        DialogSystem.instance.StartDialog(DialogSystem.DialogEnum.LoseGame2, () => UnityEngine.SceneManagement.SceneManager.LoadScene(1), "Jogar de novo"));

    }
    #endregion
}
