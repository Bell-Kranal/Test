using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _leftRange;
    [SerializeField] private int _rightRange;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Thorn _thornPrefab;
    [SerializeField] private UnityEvent _win;

    private Camera _mainCamera;
    private List<GameObject> _coinsAndThorns;

    private float _horizontalBorder;
    private float _rightCircleBorder;
    private float _verticalBorder;
    private float _bottomCircleBorder;
    private float _leftCircleBorder;
    private float _topCircleBorder;

    private int _spawnCount;
    private int _takenCoins;

    private void Awake()
    {
        _mainCamera     = Camera.main;
        _takenCoins     = 0;
        Time.timeScale  = 1f;
        _coinsAndThorns = new List<GameObject>();

        BoundaryChecking();
        SpawnBoundaryCalculation();
        CreateCoinsAndThorns();
        Spawn();
    }

    #region [Spawn Coins and Thorns]

    private void CreateCoinsAndThorns()
    {
        GameObject gabage;
        for(int i = 0; i < _rightRange; i++)
        {
            gabage = Instantiate(_coinPrefab, transform).gameObject;

           _coinsAndThorns.Add(gabage);
            gabage.SetActive(false);

            gabage = Instantiate(_thornPrefab, transform).gameObject;

            _coinsAndThorns.Add(gabage);
            gabage.SetActive(false);

        }
    }

    private void BoundaryChecking()
    {
        if(_rightRange < _leftRange)
        {
            int gabage = _leftRange;

            _leftRange = _rightRange;
            _rightRange = gabage;
        }
    }

    private void SpawnBoundaryCalculation()
    {
        int halfCameraWidth     = _mainCamera.pixelWidth / 2;
        int halfCameraHeight    = _mainCamera.pixelHeight / 2;

        CalclateValues(_mainCamera.pixelWidth, _mainCamera.pixelHeight, -50, ref _horizontalBorder, ref _verticalBorder);
        CalclateValues(halfCameraWidth, halfCameraHeight, 50, ref _rightCircleBorder, ref _bottomCircleBorder);
        CalclateValues(halfCameraWidth, halfCameraHeight, -50, ref _leftCircleBorder, ref _topCircleBorder);

        Vector2 position = Vector2.zero;

        CalculatePosition(position, ref _horizontalBorder, ref _verticalBorder);
        CalculatePosition(position, ref _rightCircleBorder, ref _bottomCircleBorder);
        CalculatePosition(position, ref _leftCircleBorder, ref _topCircleBorder);
    }

    private void CalclateValues(int valueX, int valueY, int bias, ref float x, ref float y)
    {
        x = valueX + bias;
        y = valueY + bias;
    }

    private void CalculatePosition(Vector2 position, ref float x, ref float y)
    {
        position = _mainCamera.ScreenToWorldPoint(new Vector3(x, y, 0f));

        x = position.x;
        y = position.y;
    }

    private void Spawn()
    {
        _spawnCount = Random.Range(_leftRange, _rightRange) * 2;

        for (int i = 0; i < _spawnCount; i++)
        {
            _coinsAndThorns[i].transform.position = RandomVector3();
            _coinsAndThorns[i].SetActive(true);
        }

        _spawnCount /= 2;
    }

    private Vector3 RandomVector3()
    {
        switch(Random.Range(0, 4))
        {
            case 0: return RandomVector3(-_horizontalBorder, _horizontalBorder, -_verticalBorder, _topCircleBorder);
            case 1: return RandomVector3(-_horizontalBorder, _leftCircleBorder, -_verticalBorder, _verticalBorder);
            case 2: return RandomVector3(_rightCircleBorder, _horizontalBorder, -_verticalBorder, _verticalBorder);
            case 3: return RandomVector3(-_horizontalBorder, _horizontalBorder, _bottomCircleBorder, _verticalBorder);
        }

        return Vector3.zero;
    }

    private Vector3 RandomVector3(float leftBorder, float rightBorder, float topBorder, float bottomBorder)
    {
        return new Vector3(Random.Range(leftBorder, rightBorder), Random.Range(topBorder, bottomBorder), 0f);
    }
    #endregion

    public void TookACoin()
    {
        _takenCoins++;

        if(_takenCoins == _spawnCount)
        {
            _win?.Invoke();
        }
    }
}