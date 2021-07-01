using System;
using App.Scripts.Utilities.Extensions;
using App.Scripts.Utilities.MonoBehaviours;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.UiViews.GameScreen
{
    public class StoneCoinsView : BaseUiElement<StoneCoinsView>
    {
        [Header("Score Line")]
        [SerializeField] private ScoreLineView _lineViewPrefab;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _movingDistance;
        
        [Header("Pool")]
        [SerializeField] private int _poolSize;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField, Range(0,200)] private float _randomSpawnRangeX;
        [SerializeField, Range(0, 200)] private float _randomSpawnRangeY;
        private PoolObject<ScoreLineView> ScoreLines;

        private void Awake()
        {
            ScoreLines = new PoolObject<ScoreLineView>(_lineViewPrefab, _poolSize, _spawnPosition.transform, true);
        }
        
        public void ShowScoreLine(Sprite icon, float score)
        {
            var scoreLine = ScoreLines.GetObject();
            scoreLine.SetInformation(icon, score);

            scoreLine.transform.position = _spawnPosition.position;
            scoreLine.transform.AddToCurrentPosition(new Vector3(
                Random.Range(-_randomSpawnRangeX, _randomSpawnRangeX),
                Random.Range(-_randomSpawnRangeY, _randomSpawnRangeY),
                0));

            scoreLine.transform.DOMoveY(scoreLine.transform.position.y + _movingDistance, _lifeTime)
                .OnComplete(() =>
                {
                    //Debug.Log("Complete");
                    scoreLine.transform.position = _spawnPosition.position;
                    ScoreLines.ReturnObject(scoreLine);
                }
                );
            
        }

    }
}