using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private UnityEvent _removeLastPoint;
    [SerializeField] private Ease _ease;
    [SerializeField] private float _duration;

    private Queue<Vector3> _travelPoints;
    private bool _inMove;

    private void Start()
    {
        _inMove = false;
        _travelPoints = new Queue<Vector3>();
    }
    public void AddTravelPoint(Vector3 point)
    {
        _travelPoints.Enqueue(point);
        
        TryMove();
    }

    private void TryMove()
    {
        if(_inMove == false)
        {
            if (_travelPoints.Count != 0)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        _inMove = true;

        transform
            .DOMove(_travelPoints.Dequeue(), _duration)
            .SetEase(_ease)
            .OnComplete(() => {

                _inMove = false;
                TryMove();

                _removeLastPoint?.Invoke();
            });
    }
}