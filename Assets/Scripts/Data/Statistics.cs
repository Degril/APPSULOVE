using Data;

namespace UI
{
    public class Statistics
    {
        public SavedObserver<float> TotalDistance { get; private set; }
        public SavedObserver<int> TotalDestroysCount { get; private set; }
        
        public void AddDistance(float distance) => TotalDistance.Value += distance;
        public void AddDestroy() => TotalDestroysCount.Value++;

        public Statistics()
        {
            TotalDistance = new SavedObserver<float>(nameof(TotalDistance));
            TotalDestroysCount = new SavedObserver<int>(nameof(TotalDestroysCount));
        }
    }
}