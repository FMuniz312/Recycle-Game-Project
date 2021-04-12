using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGameUI : MonoBehaviour
{
    [SerializeField] Text playText;

    private void Start()
    {
        switch (LanguageSystem.gameLanguage)
        {
            default: playText.text = "Play"; break;
            case Language.BrazilianPortuguese: playText.text = "Jogar"; break;

        }
    }

    public void ChooseGameBRPortugueseLanguage()
    {
        LanguageSystem.ChooseLanguage(Language.BrazilianPortuguese);
        UpdateUI();
    }
    public void ChooseGameEnglishLanguage()
    {
        LanguageSystem.ChooseLanguage(Language.English);
        UpdateUI();
    }

    void UpdateUI()
    {
        switch (LanguageSystem.gameLanguage)
        {
            default: playText.text = "Play"; break;
            case Language.BrazilianPortuguese: playText.text = "Jogar"; break;

        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
