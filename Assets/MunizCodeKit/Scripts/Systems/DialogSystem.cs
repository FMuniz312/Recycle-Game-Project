using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogSystem : MonoBehaviour
{


    [Header("Main Dialog")]
    [SerializeField] DialogDataHolder[] arrayDialogDataHolder;
    const float TEXT_DELAY = 3f;

    #region singleton
    static public DialogSystem instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    public void StartDialog(DialogEnum dialogenum)
    {
        try
        {
            DialogBoxController.instance.ShowDialogBox(GetDialogDataHolder(dialogenum).dialogText, TEXT_DELAY, GetDialogDataHolder(dialogenum).image);
        }

        catch
        {
            Debug.LogError("something went wrong when looking up the key in the dictionary");
        }

    }

    public void StartDialog(DialogEnum dialogenum, UnityEngine.Events.UnityAction afterButtonClicked)
    {
        
            DialogBoxController.instance.ShowDialogBox(GetDialogDataHolder(dialogenum).dialogText, TEXT_DELAY, GetDialogDataHolder(dialogenum).image, afterButtonClicked);
         

    }

    DialogDataHolder GetDialogDataHolder(DialogEnum dialogenum)
    {
        return arrayDialogDataHolder.Where(p => p.dialogEnum == dialogenum).First();
    }
     

    [System.Serializable]
    public class DialogDataHolder
    {
        public DialogEnum dialogEnum;
        public string dialogText;
        public Sprite image;
    }
    public enum DialogEnum
    {
        Narrator1,
        Narrator2,
        Narrator3,
        Narrator4,
        Planet1,
        Planet2,
        Planet3,
    }
}
