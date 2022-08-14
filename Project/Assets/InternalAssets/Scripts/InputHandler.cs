using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private UnityEventChangePlayerPosition _changePlayerPositionAndAddPoint;

    private Ray _ray;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            _changePlayerPositionAndAddPoint?.Invoke(_ray.origin);
        }
    }
}

[System.Serializable]
internal class UnityEventChangePlayerPosition : UnityEvent<Vector3>
{

}
