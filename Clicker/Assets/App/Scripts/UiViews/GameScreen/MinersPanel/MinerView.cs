using App.Scripts.Gameplay.CoreGameplay.Mining;
using DragonBones;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.UiViews.GameScreen.MinersPanel
{
    public class MinerView : MonoBehaviour
    {
        [ReadOnly, ShowInInspector]
        public bool IsEmpty => _armatureComponent == null;
        [ReadOnly, ShowInInspector]
        protected UnityArmatureComponent _armatureComponent;
        public UnityArmatureComponent ArmatureComponent => _armatureComponent;

        /// <summary>
        /// Воспроизвести анимацию
        /// </summary>
        /// <param name="animationName">Название анимации</param>
        public void PlayAnimation(string animationName)
        {
            _armatureComponent.animationName = animationName;
            _armatureComponent.animation.Play();
        }

        /// <summary>
        /// Остановить воспроизведение анимации
        /// </summary>
        public void StopAnimation()
        {
            _armatureComponent.animation.Stop();
        }

        /// <summary>
        /// Установить ссылку на визуальную часть
        /// </summary>
        /// <param name="visualContext"></param>
        public virtual void SetVisual(UnityArmatureComponent armatureComponent)
        {
            _armatureComponent = armatureComponent;
        }

        public void SetLevelVisual(int level)
        {
            MinerVisualContext mvc = ArmatureComponent.gameObject.GetComponent<MinerVisualContext>();
            mvc.UnlockComponents.SetUnlockLevel(level);
        }

    }
}