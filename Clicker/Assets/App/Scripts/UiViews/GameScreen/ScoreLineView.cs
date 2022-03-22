using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UiViews.GameScreen
{
    public class ScoreLineView : MonoBehaviour
    {
        public SVGImage Icon => _icon;
        public TMP_Text Score => _score;

        [SerializeField] private SVGImage _icon;
        [SerializeField] private TMP_Text _score;

        public void SetInformation(Sprite icon, float score)
        {
            _icon.sprite = icon;
            _score.text = $"{score:F1}";
        }
    }
}