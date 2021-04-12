using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSystem : MonoBehaviour
{
    static public Language gameLanguage { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    static public void ChooseLanguage(Language language)
    {
        gameLanguage = language;
    }

    

}
public enum Language
{
    English,
    BrazilianPortuguese
}