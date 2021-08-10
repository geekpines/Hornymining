using System;
using System.Collections.Generic;
using App.Scripts.UiControllers.Common.MinerLevelUnlock;
using DragonBones;
using UnityEngine;

namespace App.Scripts.UiViews.GameScreen.MinersPanel
{
    public class MinerView : BaseUiElement<MinerView>
    {
        [field: SerializeField] public UnityArmatureComponent Armature { get; protected set; }
        public LevelUnlockComponents UnlockComponent;

        /// <summary>
        /// Установить компонент анимации для майнера
        /// </summary>
        /// <param name="armature"></param>
        public virtual void SetArmature(UnityArmatureComponent armature)
        {
            Armature = armature;
        }

        /// <summary>
        /// Воспроизвести анимацию
        /// </summary>
        /// <param name="animationName">Название анимации</param>
        public void PlayAnimation(string animationName)
        {
            Armature.animationName = animationName;
            Armature.animation.Play();
        }

        /// <summary>
        /// Остановить воспроизведение анимации
        /// </summary>
        public void StopAnimation()
        {
            Armature.animation.Stop();
        }
    }
}