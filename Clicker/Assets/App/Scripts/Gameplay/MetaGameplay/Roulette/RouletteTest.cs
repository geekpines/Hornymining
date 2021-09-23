using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Gameplay.MetaGameplay.Roulette
{
    public class RouletteTest : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RollMinerController _rollController;
        
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
            var result = _rollController.RollItem();
            if (result != null)
            {
                Debug.Log($"Result: {result.Name.GetLocalizedString()}");
            }
        }
    }
}