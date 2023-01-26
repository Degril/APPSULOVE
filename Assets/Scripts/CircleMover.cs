using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircleMover : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField] private float baseSlideSpeed;
    [SerializeField] private AnimationCurve accelerationSpeed;
        
    private Circle _circle;
    private readonly Queue<Vector3> _slidePositions = new();
    private float _slideMovingTime;
    private float _timeWhenMoveToNextPose;
    private Camera _camera;
    private readonly RaycastHit[] _raycastHits = new RaycastHit[5];

    public event Action<float> OnObjectMoved;

    private void Start()
    {
        _camera = Camera.main;
        if (SimpleServiceLocator.TryGet<Circle>(out var circle))
        {
            _circle = circle;
        }
    }
        
    private void Update()
    {
        MoveCircle();
    }

    public void OnPointerClick(PointerEventData eventData) => OnClick(eventData);
    public void OnDrag(PointerEventData eventData) => OnClick(eventData);

    private void OnClick(PointerEventData eventData)
    {
        if(_circle.IsMovable == false)
            return;
            
        var ray = _camera.ScreenPointToRay(eventData.position);
        var size = Physics.RaycastNonAlloc(ray, _raycastHits);
        for (int i = 0; i < size; i++)
        {
            var raycastHit = _raycastHits[i];
            if (raycastHit.transform.TryGetComponent<CircleMover>(out _))
            {
                _slidePositions.Enqueue(raycastHit.point);
            }
        }
    }

    private void MoveCircle()
    {
        if (_slidePositions.Count == 0) return;
            
        _slideMovingTime += Time.deltaTime;
        while (_timeWhenMoveToNextPose < Time.time && _slidePositions.Count > 0)
        {
            var addedSpeed = accelerationSpeed.Evaluate(_slideMovingTime);
            var currentSpeed = Mathf.Max(_slideMovingTime * addedSpeed, 0) + baseSlideSpeed;
            _timeWhenMoveToNextPose += 1 / currentSpeed;
            var nextPosition = _slidePositions.Dequeue();
            var distance = Vector3.Distance(nextPosition, _circle.transform.position);
            _circle.transform.position = nextPosition;
            OnObjectMoved?.Invoke(distance);
        }

        if (_slidePositions.Count == 0)
        {
            _slideMovingTime = 0;
        }
    }

}