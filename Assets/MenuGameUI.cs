using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGameUI : MonoBehaviour
{
    [SerializeField] Text playText;
    [SerializeField] Text chooseLanguageText;
    [SerializeField] Text GameTitle;

    private void Start()
    {
        UpdateUILanguage();
    }

    public void ChooseGameBRPortugueseLanguage()
    {
        LanguageSystem.ChooseLanguage(Language.BrazilianPortuguese);
        UpdateUILanguage();
    }
    public void ChooseGameEnglishLanguage()
    {
        LanguageSystem.ChooseLanguage(Language.English);
        UpdateUILanguage();
    }

    void UpdateUILanguage()
    {
        switch (LanguageSystem.gameLanguage)
        {
            default:
                playText.text = "Play";
                chooseLanguageText.text = "Choose a language";
                GameTitle.text = "Planet Dirty Earth"; break;
            case Language.BrazilianPortuguese:
                playText.text = "Jogar";
                chooseLanguageText.text = "Escolha um idioma";
                GameTitle.text = "Planeta Terra Suja"; break;

        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
