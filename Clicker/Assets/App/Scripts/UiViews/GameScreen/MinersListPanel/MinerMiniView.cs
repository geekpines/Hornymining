using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UiViews.GameScreen.MinersListPanel
{
    public class MinerMiniView : BaseUiElement<MinerMiniView>
    {
        [SerializeField] private Image _portraitMiner;

        public void SetPortrait(Sprite newPortrait)
        {
            _portraitMiner.sprite = newPortrait;
        }
    }
}