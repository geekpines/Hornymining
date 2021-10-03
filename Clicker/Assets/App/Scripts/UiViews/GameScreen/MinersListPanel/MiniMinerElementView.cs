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
        private LocalizedString _name;
        [SerializeField] private LocalizeStringEvent _nameEvent;
        [SerializeField] private LocalizedString _level;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _avatar;
        [SerializeField] private List<GameObject> _stars = new List<GameObject>();
        [SerializeField] private GameObject _useMask;
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
            _name = name;
            _nameEvent.StringReference = name;
        }

        public void SetLevel(int level)
        {
            _currentLevel = level;
            _levelText.text = $"{_level.GetLocalizedString()} {_currentLevel}";
        }

        public void RefreshText()
        {
            _nameEvent.RefreshString();
            SetLevel(_currentLevel);
        }

        public void SetIcon(Sprite newIcon)
        {
            _avatar.sprite = newIcon;
        }

        public void SetStars(int countStars)
        {
            _currentStars = Mathf.Clamp(countStars, 1, 5);
            
            ResetStars();
            for (int i = 0; i < _currentStars; i++)
            {
                _stars[i].SetActive(true);
            }
        }

        public void SetUseMask(bool state)
        {
            _useMask.SetActive(state);
        }

        private void ResetStars()
        {
            foreach (var star in _stars)
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