using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03;

namespace App.Scripts.UiControllers.RouletteScreen
{
    public class RouletteSlotsController : MonoBehaviour
    {
        [SerializeField] private RouletteSlotsPool _slotsViewPool;
        [SerializeField] private ExtendedScrollView scrollView = default;

        void Start()
        {
            var items = Enumerable.Range(0, 10)
                .Select(i => new ItemData())
                .ToArray();
            
            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }


    }
}