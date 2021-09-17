using App.Scripts.UiViews.GameScreen.MinersPanel;
using UnityEngine;
using Transform = UnityEngine.Transform;

namespace App.Scripts.UiViews.RouletteScreen
{
    public class RouletteSlotView : MinerView
    {
        //todo: добавить название / описание
        [field: SerializeField] public Transform RootPosition { get; private set; }
    }
}