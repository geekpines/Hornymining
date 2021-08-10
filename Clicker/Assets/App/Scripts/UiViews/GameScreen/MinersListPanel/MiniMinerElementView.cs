using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace App.Scripts.UiViews.GameScreen.MinersListPanel
{
    public class MiniMinerElementView : MonoBehaviour
    {
        public LocalizeStringEvent NameEvent;
        public LocalizedString Level;
        public TextMeshProUGUI LevelText;
        public Image Avatar;
        public List<GameObject> Stars = new List<GameObject>();
        private int _currentLevel;
        [SerializeField, Range(1, 5)] private int _currentStars = 3;
        
        public void SetMinerInformation(LocalizedString name, Sprite icon, int grade, int level = 1)
        {
            SetName(name);
            SetIcon(icon);
            SetStars(grade);
            SetLevel(level);
        }
        
        public void SetName(LocalizedString name)
        {
            NameEvent.StringReference = name;
        }

        public void SetLevel(int level)
        {
            _currentLevel = level;
            LevelText.text = $"{Level.GetLocalizedString()} {_currentLevel}";
        }

        public void RefreshText()
        {
            NameEvent.RefreshString();
            SetLevel(_currentLevel);
        }

        public void SetIcon(Sprite newIcon)
        {
            Avatar.sprite = newIcon;
        }

        public void SetStars(int countStars)
        {
            _currentStars = Mathf.Clamp(countStars, 1, 5);
            
            ResetStars();
            for (int i = 0; i < _currentStars; i++)
            {
                Stars[i].SetActive(true);
            }
        }

        private void ResetStars()
        {
            foreach (var star in Stars)
            {
                star.SetActive(false);
            }
        }

        private void OnValidate()
        {
            SetStars(_currentStars);
        }
    }
}