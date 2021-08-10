using App.Scripts.UiViews.GameScreen.MinersPanel;
using DragonBones;
using Transform = UnityEngine.Transform;

namespace App.Scripts.UiViews.RouletteScreen
{
    public class RouletteSlotView : MinerView
    {
        public Transform MaskParant;

        //todo: добавить название / описание
        
        public override void SetArmature(UnityArmatureComponent armatureComponent)
        {
            if (Armature != null)
            {
                Destroy(Armature.gameObject);
            }

            Armature = armatureComponent;
            Armature.transform.SetParent(MaskParant);
        }
    }
}