using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Utilities.MonoBehaviours
{
    public class ForceRebuildLayout : MonoBehaviour
    {
        [SerializeField] private bool _withFrameDelay;
        private RectTransform LayoutRoot => GetComponent<RectTransform>();

        private void Start()
        {
            Run();
        }

        private void OnEnable()
        {
            Run();
        }

        public void Run()
        {
            if (_withFrameDelay)
            {
                StartCoroutine(DelayRebuild());
            }
            else
            {
                Rebuild();
            }
        }

        private void Rebuild()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(LayoutRoot);
        }

        private IEnumerator DelayRebuild()
        {
            yield return null;
            Rebuild();
        }
    }
}