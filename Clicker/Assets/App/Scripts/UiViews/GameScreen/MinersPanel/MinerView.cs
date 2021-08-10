using System;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

namespace App.Scripts.UiViews.GameScreen.MinersPanel
{
    public class MinerView : BaseUiElement<MinerView>
    {
        [field: SerializeField] public UnityArmatureComponent Armature { get; private set; }
        
        /// <summary>
        /// Установить компонент анимации для майнера
        /// </summary>
        /// <param name="armature"></param>
        public void SetArmature(UnityArmatureComponent armature)
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