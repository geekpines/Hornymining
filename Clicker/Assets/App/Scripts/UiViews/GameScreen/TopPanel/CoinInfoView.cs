using TMPro;
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
            SetValue(count);
            SetDescription(description);
        }
        
        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetValue(float count)
        {
            _info.text = $"{count:F1}";
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