using Data;
using UnityEngine;

namespace UI
{
    public class Statistics
    {
        public SimpleObserver<float> TotalDistance { get; private set; } = new();
        public SimpleObserver<int> TotalDestroysCount { get; private set; } = new();
        
        public void AddDistance(float distance) => TotalDistance.Value += distance;
        public void AddDestroy() => TotalDestroysCount.Value++;
    }
}