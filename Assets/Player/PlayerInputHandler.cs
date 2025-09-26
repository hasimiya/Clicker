using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls _playerControls;
    private IController _controller;

    [SerializeField] private MonoBehaviour _objectToControl;
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _controller = _objectToControl.GetComponent<IController>();

        if (_controller == null)
        {
            Debug.LogError("Объект управления не реализует интерфейс IController!");
        }
    }
    private void OnEnable()
    {
        _playerControls.Player.Enable();
        _playerControls.Player.Click.performed += OnClick;
    }
    private void OnDisable()
    {
        _playerControls.Player.Disable();
        _playerControls.Player.Click.performed -= OnClick;
    }

    private void OnClick(InputAction.CallbackContext obj)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Vector2 screenPosition = _playerControls.Player.Position.ReadValue<Vector2>();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Debug.Log($"Нажатие/косание обнаружено в положении экрана: {screenPosition}");

        _controller?.AddClickPointToQueue(worldPosition);
    }
}
