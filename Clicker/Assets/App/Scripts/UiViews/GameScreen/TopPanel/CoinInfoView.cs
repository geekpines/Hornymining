using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UiViews.GameScreen.TopPanel
{
    public class CoinInfoView : BaseUiElement<CoinInfoView>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _info;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private GameObject _panelDescription;

        public void SetCoinInformation(Sprite icon, float count, string description)
        {
            SetIcon(icon);
            SetCount(count);
            SetDescription(description);
        }
        
        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetCount(float count)
        {
            _info.text = String.Format("{0:C}", count);
        }

        public void SetDescription(string text)
        {
            _description.text = text;
        }

        public void EnableDescription(bool show)
        {
            _panelDescription.SetActive(show);
        }

    }
}