using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    Dictionary<Dialog, string> dialogDictionary = new Dictionary<Dialog, string>();
    

    [Header("Main Dialog")]
    [SerializeField] string firstLevel;
    [SerializeField] string secondLevel;
    [SerializeField] string thirdLevel;

    [Header("Extra")]
    [SerializeField] string good1;
    [SerializeField] string good2;
    [SerializeField] string good3;
    [SerializeField] string bad1;
    [SerializeField] string bad2;
    [SerializeField] string bad3;

    #region singleton
    static public DialogSystem instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion
    
    public void StartDialog(Dialog dialog)
    {
        if (dialogDictionary.Count <= 0) FillDictionary();
        string dialogString;
        if (dialogDictionary.TryGetValue(dialog, out dialogString))
        {
            DialogBoxController.instance.ShowDialogBox(dialogString, 3f);
        }
        else
        {
            Debug.LogError("something went wrong when looking up the key in the dictionary");
        }

    }

    public void StartDialog(Dialog dialog, UnityEngine.Events.UnityAction afterButtonClicked)
    {
        if (dialogDictionary.Count <= 0) FillDictionary();
        string dialogString;
        if (dialogDictionary.TryGetValue(dialog, out dialogString))
        {
            DialogBoxController.instance.ShowDialogBox(dialogString, 3f, afterButtonClicked);
        }
        else
        {
            Debug.LogError("something went wrong when looking up the key in the dictionary");
        }

    }

    void FillDictionary()
    {

        dialogDictionary.Add(Dialog.firstLevel, firstLevel);
        dialogDictionary.Add(Dialog.secondLevel, secondLevel);
        dialogDictionary.Add(Dialog.thirdLevel, thirdLevel);
        dialogDictionary.Add(Dialog.good1, good1);
        dialogDictionary.Add(Dialog.good2, good2);
        dialogDictionary.Add(Dialog.good3, good3);
        dialogDictionary.Add(Dialog.bad1, bad1);
        dialogDictionary.Add(Dialog.bad2, bad2);
        dialogDictionary.Add(Dialog.bad3, bad3);
    }
    public enum Dialog
    {
        firstLevel,
        secondLevel,
        thirdLevel,
        good1,
        good2,
        good3,
        bad1,
        bad2,
        bad3
    }
}
