using UnityEngine;

namespace UI
{
    public class StaticsPresenter : MonoBehaviour
    {
        [SerializeField] private StatisticsView statisticsView;
        private Statistics _statistics;
        public void Start()
        {
            _statistics = new Statistics();
            if (SimpleServiceLocator.TryGet<RandomSpawner>(out var randomSpawner))
            {
                randomSpawner.OnObjectCreated += SubscribeToDestroyObject;
            }
            if (SimpleServiceLocator.TryGet<CircleMover>(out var circleMover))
            {
                circleMover.OnObjectMoved += _statistics.AddDistance;
            }

            UpdateDistanceText(_statistics.TotalDistance.Value);
            UpdateDestroysText(_statistics.TotalDestroysCount.Value);
            _statistics.TotalDistance.OnChanged += UpdateDistanceText;
            _statistics.TotalDestroysCount.OnChanged += UpdateDestroysText;
        }

        private void UpdateDistanceText(float value) => statisticsView.distanceText.text = value.ToString("0.00");
        private void UpdateDestroysText(int value) => statisticsView.destroysCount.text = value.ToString();

        private void SubscribeToDestroyObject(DestroyableObject destroyableObject)
        {
            destroyableObject.OnDestroy += _=> _statistics.AddDestroy();
        }
    }
    
}