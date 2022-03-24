using App.Scripts.Gameplay.CoreGameplay.Mining;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class DialogUiController : MonoBehaviour
{
    [SerializeField] private DialogUiTextContainer dialogContainer;

    [SerializeField] private GameObject DialogContent;
    
    //открытие англ. контента
    public void OpenEngDialogContent(bool state, string text)
    {
        DialogContent.SetActive(state);
        dialogContainer.Dialog.text = text;
    }
    //открытие русского контента
    public void OpenRuDialogContent(bool state, LocalizedString text)
    {
        DialogContent.SetActive(state);
        dialogContainer.Dialog.text = text.GetLocalizedString();
    }

    public void SetName(LocalizedString text)
    {
        dialogContainer.MinerName.text = text.GetLocalizedString();
    }

    public void SetOff(bool state)
    {
        DialogContent.SetActive(state);
    }

}
