using App.Scripts.UiViews.Common.MinerLevelUnlock;
using App.Scripts.UiViews.CoreGameplay.Mining;
using DragonBones;
using UnityEngine;
using Transform = UnityEngine.Transform;

namespace App.Scripts.UiViews.GameScreen.MinersPanel
{
    public class MinerView : BaseUiElement<MinerView>
    {
        public MinerVisualContext Visual { get; private set; }

        /// <summary>
        /// Воспроизвести анимацию
        /// </summary>
        /// <param name="animationName">Название анимации</param>
        public void PlayAnimation(string animationName)
        {
            Visual.ArmatureComponent.animationName = animationName;
            Visual.ArmatureComponent.animation.Play();
        }

        /// <summary>
        /// Остановить воспроизведение анимации
        /// </summary>
        public void StopAnimation()
        {
            Visual.ArmatureComponent.animation.Stop();
        }
        
        /// <summary>
        /// Установить ссылку на визуальную часть
        /// </summary>
        /// <param name="visualContext"></param>
        public virtual void SetVisual(MinerVisualContext visualContext)
        {
            Visual = visualContext;
        }

    }
}