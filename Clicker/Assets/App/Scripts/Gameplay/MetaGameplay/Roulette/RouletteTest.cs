using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Gameplay.MetaGameplay.Roulette
{
    public class RouletteTest : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RollMinerSystem _rollSystem;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(Clicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Clicked);
        }

        private void Clicked()
        {
            var result = _rollSystem.RollItem();
            if (result != null)
            {
                Debug.Log($"Result: {result.Name.GetLocalizedString()}");
            }
        }
    }
}