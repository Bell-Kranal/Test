using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerController : MonoBehaviour
{
    [SerializeField] private UnityEvent _addMoney;
    [SerializeField] private UnityEvent _lose;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            coin.gameObject.SetActive(false);

            _addMoney?.Invoke();
        }
        else
        {
            if(collision.TryGetComponent(out Thorn thorn))
            {
                _lose?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
