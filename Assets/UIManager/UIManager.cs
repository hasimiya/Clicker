using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _minSpeed = 10f;
    private float _maxSpeed = 100f;
    public void OnSliderValueChanged(float value)
    {
        float newSpeed = Mathf.Lerp(_minSpeed, _maxSpeed, value);
        _playerMovement.SetNewSpeedValue(newSpeed);
    }
}
