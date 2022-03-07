using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace App.Scripts.UiViews.GameScreen.MinersListPanel
{
    public class MiniMinerElementView : MonoBehaviour
    {
        public event Action<MiniMinerElementView> OnMinerClicked;
        public event Action<MiniMinerElementView> OnMinerDoubleClicked;

        private LocalizedString _name;
        private float _timeForDoubleClick = 0.5f;
        private bool _isReadyForDoubleClick;
        [SerializeField] private LocalizeStringEvent _nameEvent;
        [SerializeField] private LocalizedString _level;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _avatar;
        [SerializeField] private List<GameObject> _stars = new List<GameObject>();
        [SerializeField] private List<GameObject> _hearts = new List<GameObject>();

        [SerializeField] private GameObject _useMask;
        [SerializeField] private GameObject _lockMask;

        private int _currentLevel;

        [SerializeField, Range(1, 5)] private int _currentStars = 1;
        [SerializeField, Range(1, 5)] private int _currentHearts  = 1;
        [SerializeField] private Button _minerButton;
        public int ID { get; private set; }
        public bool IsActive { get; private set; }

        public void SetMinerInformation(LocalizedString name, Sprite icon, int id, int grade = 3, int level = 1)
        {
            SetName(name);
            SetIcon(icon);
            ID = id;
            //SetStars(grade); //временно отключены в префабе
            SetLevel(level);
            SetStars(level);
            
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
            SetStars(level);
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

        public void SetHearts()
        {
            _currentHearts++;
            int countHearts = _currentHearts;            
            _currentHearts = Mathf.Clamp(countHearts, 1, 5);
            for (int i = 0; i < _currentHearts; i++)
            {
                _hearts[i].SetActive(true);
            }
        }

        public void SetUseMask(bool state)
        {
            _useMask.SetActive(state);
            IsActive = state;
        }

        public void SetLockMask(bool state)
        {
            _lockMask.SetActive(state);

        }

        private void ResetStars()
        {
            foreach (var star in _stars)
            {
                star.SetActive(false);
            }
        }

        private void OnEnable()
        {
            _minerButton.onClick.AddListener(MinerClicked);

            //проверка уровня
        }

        private void OnDisable()
        {
            _minerButton.onClick.RemoveListener(MinerClicked);
        }

        private void MinerClicked()
        {
            OnMinerClicked?.Invoke(this);
            if (_isReadyForDoubleClick)
            {
                OnMinerDoubleClicked?.Invoke(this);
                _isReadyForDoubleClick = false;
                StopAllCoroutines();
            }
            else
            {
                StartCoroutine(WaitingDoubleClick());
            }
        }

        private void OnValidate()
        {
            SetStars(_currentStars);
        }

        private IEnumerator WaitingDoubleClick()
        {
            _isReadyForDoubleClick = true;
            yield return new WaitForSeconds(_timeForDoubleClick);
            _isReadyForDoubleClick = false;
        }

        public int GetHearts()
        {
            return _currentHearts;
        }

        public int GetStars()
        {
            return _currentStars;
        }

  
    }
}