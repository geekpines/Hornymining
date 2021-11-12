using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using Mkey;
using App.Scripts.Gameplay.CoreGameplay.Mining;

namespace MkeyFW
{
    [ExecuteInEditMode]
    public class Sector : MonoBehaviour
    {
        

        [SerializeField]
        private bool autoText = true;
        [SerializeField]
        private string coins;
        [SerializeField]
        private bool bigWin;
        [SerializeField]
        private List<GameObject> hitPrefabs;
        private float destroyTime = 3f;
        [SerializeField]
        private UnityEvent  hitEvent;

        [SerializeField]
        public AudioClip hitSound;

        public TextMesh Text { get; private set; }

        public string Coins
        {
            get { return coins; }
            set { coins =  value; RefreshText(); }
        }

        public bool BigWin
        {
            get { return bigWin; }
        }

        #region regular
        void Start()
        {
            Text = GetComponent<TextMesh>();
            RefreshText();
        }

        void OnValidate()
        {
           coins = coins;
           RefreshText();
        }
        #endregion regular

        private void RefreshText()
        {
            if (!autoText) return;
            if (!Text) Text = GetComponent<TextMesh>();
            if (!Text) return;
            var f = new NumberFormatInfo { NumberGroupSeparator = " " }; // Text.text = Coins.ToString("n0", f);
            Text.text = coins;
        }

        /// <summary>
        /// Instantiate all prefabs and invoke hit event
        /// </summary>
        /// <param name="position"></param>
        public void PlayHit(Vector3 position)
        {
            if (hitPrefabs != null)
            {
                foreach (var item in hitPrefabs)
                {
                    if (item)
                    {
                        Transform partT = Instantiate(item).transform;
                        partT.position = position;
                        if (this && partT) Destroy(partT.gameObject, destroyTime);
                    }
                }
            }
           hitEvent?.Invoke();
        }


    }
}