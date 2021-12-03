using App.Scripts.UiViews.GameScreen.MinersPanel;
using DragonBones;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using Transform = UnityEngine.Transform;

namespace App.Scripts.UiViews.RouletteScreen
{
    public class RouletteSlotView : MinerView
    {
        //todo: добавить название / описание
        [field: SerializeField] public Transform RootPosition { get; private set; }
        public GameObject RootVisual { get; private set; }
        public int ConfigHash { get; private set; }
        [SerializeField] private LocalizeStringEvent _nameLocalize;
        public LocalizedString Name => _nameLocalize.StringReference;

        public void SetVisual(GameObject rootObject, UnityArmatureComponent armatureComponent, int configHash)
        {
            RootVisual = rootObject;
            ConfigHash = configHash;
            SetVisual(armatureComponent);
        }

        public void SetInformation(LocalizedString name)
        {
            _nameLocalize.StringReference = name;
        }

        public void DestroyVisual()
        {
            if (RootVisual != null)
            {
                Destroy(RootVisual);
            }
        }
    }
}