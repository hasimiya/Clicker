using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IController
{
    private Rigidbody2D rb;

    [SerializeField] private float _moveSpeed = 30f;
    private bool _isMoving = false;

    private float _smoothTime = 0.1f;
    private float _stoppingDistance = 0.05f;

    private Vector2 _currentPosition;
    private Vector2 _pointPosition;
    private Vector2 _currentVelocity = Vector2.zero;

    private Queue<Vector2> _queuePositions;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _queuePositions = new Queue<Vector2>();
    }
    private void FixedUpdate()
    {
        _currentPosition = transform.position;

        if (_isMoving)
        {
            if (Vector2.Distance(_currentPosition, _pointPosition) <= _stoppingDistance)
            {
                StopMove();
                GetQueue();
            }
            else
            {
                Move(_currentPosition);
            }
        }
        else
        {
            if (_queuePositions.Count > 0)
            {
                StartNewMove();
            }
        }
    }
    public void AddClickPointToQueue(Vector3 worldPosition)
    {
        _queuePositions.Enqueue(worldPosition);
    }
    private void Move(Vector2 currentPosition)
    {
        Vector2 newPosition = Vector2.SmoothDamp(currentPosition, _pointPosition, ref _currentVelocity, _smoothTime, _moveSpeed);

        rb.MovePosition(newPosition);
    }
    private void StopMove()
    {
        _isMoving = false;
        rb.linearVelocity = Vector2.zero;
    }
    private void StartNewMove()
    {
        _pointPosition = _queuePositions.Dequeue();
        _isMoving = true;
        _currentVelocity = Vector2.zero;
    }
    private void GetQueue()
    {
        Debug.LogWarning($"Позиции в очереди: {_queuePositions.Count}");
    }
    public void SetNewSpeedValue(float newValue)
    {
        _moveSpeed = newValue;
    }
}