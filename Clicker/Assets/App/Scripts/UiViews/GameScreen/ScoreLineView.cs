using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UiViews.GameScreen
{
    public class ScoreLineView : MonoBehaviour
    {
        public Image Icon => _icon;
        public TMP_Text Score => _score;

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _score;

        public void SetInformation(Sprite icon, float score)
        {
            _icon.sprite = icon;
            _score.text = $"{score:F1}";
        }
    }
}