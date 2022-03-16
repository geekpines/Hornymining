using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace App.Scripts.UiViews.GameScreen.TopPanel
{
    public class CoinInfoView : BaseUiElement<CoinInfoView>
    {
        [SerializeField] private SVGImage _icon;
        [SerializeField] private Sprite _lockIcon;
        private Sprite _openIcon;

        [SerializeField] private TMP_Text _info;
        [SerializeField] private LocalizeStringEvent _description;
        [SerializeField] private GameObject _panelDescription;

        public bool _isLocked = false;
        public void SetCoinInformation(Sprite icon, float count, LocalizedString description)
        {
            SetIcon(icon);
            SetValue(count);
            SetDescription(description);
            _openIcon = icon;
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetValue(float count)
        {
            _info.text = $"{count:F1}";
        }

        public void SetDescription(LocalizedString text)
        {
            _description.StringReference = text;
        }

        public void EnableDescription(bool show)
        {
            _panelDescription.SetActive(show);
        }

        public void SetLock()
        {
            SetIcon(_lockIcon);
            _isLocked = true;
        }

        public void SetUnlock()
        {
            SetIcon(_openIcon);
            _isLocked = false;
        }
    }
}