using App.Scripts.UiViews;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipHelp : BaseUiElement<ToolTipHelp>
{
    [SerializeField] private GameObject Tip;

    private void OnEnable()
    {
        OnStartHolder += OnStart;
        OnEndHolder += OnEnd;
    }
    private void OnApplicationQuit()
    {
        OnStartHolder -= OnStart;
        OnEndHolder -= OnEnd;
    }

    private void OnDisable()
    {
        OnStartHolder -= OnStart;
        OnEndHolder -= OnEnd;
    }

    private void OnDestroy()
    {
        OnStartHolder -= OnStart;
        OnEndHolder -= OnEnd;
    }

    private void OnStart(ToolTipHelp help)
    {
        ShowTip(true);
    }

    private void OnEnd(ToolTipHelp help)
    {
        ShowTip(false);
    }

    private void ShowTip(bool show)
    {
        Tip.SetActive(show);
    }

    
}

