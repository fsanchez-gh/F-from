using System;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FlightControl : MonoBehaviour
{
    [SerializeField] private GameObject flyingObject;

    [SerializeField] private float
        impulseSpeed,
        horizontalSpeed,
        verticalSpeed,
        warpSpeed,
        warpCorrectionSpeed,
        pitchCorrectionSpeed;

    [SerializeField, Range(0f, 1f)] private float
        normalFrequency,
        lowFrequency,
        highFrequency;

    public bool eventActive;

    private float
        _yaw,
        _pitch,
        _warp;

    private bool
        _yAxisIsInverted,
        _xAxisIsInverted;

    private Rumble _rumble;

    private Gamepad
        _gamePad;

    private void Awake()
    {
        _rumble = new Rumble();
        _gamePad  = Gamepad.current;
    }

    private void LateUpdate()
    {
        FlightController();
        NavigationAssistant();
    }

    private void OnEnable()
    {
        _rumble.Rumblemap.Warprumble.performed += RunRumbleEvent;
        _rumble.Rumblemap.Enable();
    }

    private void OnDisable()
    {
        _rumble.Rumblemap.Warprumble.performed -= RunRumbleEvent;
        _rumble.Rumblemap.Disable();
    }

    private void FlightController()
    {
        Impulse();
        YawController();
        PitchController();
        WarpController();
    }

    private void NavigationAssistant()
    {
        WarpRestorer();
        PitchRestorer();
    }

    private void Impulse()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            transform.Translate(impulseSpeed * Time.deltaTime * Vector3.forward);
            RestoreRumble();
        }
        else
        {
            StopRumble();
            var velocity = Input.GetAxis("Mouse X");
            if (velocity > 0)
                transform.Translate((impulseSpeed + impulseSpeed * velocity) * Time.deltaTime * Vector3.forward);
            else if (velocity == 0)
                transform.Translate(impulseSpeed * Time.deltaTime * Vector3.forward);
            else if(velocity < 0)
                transform.Translate((impulseSpeed + impulseSpeed * velocity/2) * Time.deltaTime * Vector3.forward);
        }
    }

    private void YawController()
    {
        _yaw = Input.GetAxis("Horizontal");
        if(_xAxisIsInverted)
            transform.Rotate(_yaw * horizontalSpeed * Time.deltaTime * Vector3.down);
        else
            transform.Rotate(_yaw * horizontalSpeed * Time.deltaTime * Vector3.up);
    }

    private void PitchController()
    {
        _pitch = Input.GetAxis("Vertical");
        if(_yAxisIsInverted)
            transform.Rotate( _pitch * verticalSpeed * Time.deltaTime * Vector3.left);
        else
            transform.Rotate( _pitch * verticalSpeed * Time.deltaTime * Vector3.right);
    }

    private void WarpController()
    {
        _warp = Input.GetAxis("Roll");
        flyingObject.transform.Rotate( _warp * warpSpeed * Time.deltaTime * -Vector3.forward);
    }

    private void WarpRestorer()
    {
        if (_warp == 0)
            flyingObject.transform.rotation = Quaternion.Slerp(flyingObject.transform.rotation, transform.rotation, warpCorrectionSpeed * Time.deltaTime);
    }

    private void PitchRestorer()
    {
        if (_pitch != 0 || (!(transform.up.y < 0) && transform.right.y == 0)) return;
        var restorePitch = Quaternion.LookRotation(transform.forward, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, restorePitch, pitchCorrectionSpeed * Time.deltaTime);
    }

    private void RunRumbleEvent(InputAction.CallbackContext context)
    {
        eventActive = true;
        var value = context.ReadValue<float>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            _gamePad.SetMotorSpeeds(lowFrequency * value,  highFrequency * value);
        }
        eventActive = false;
    }
    
    private void RestoreRumble()
    {
        if (!eventActive && Input.GetAxis("Roll") == 0)
            _gamePad.SetMotorSpeeds(normalFrequency,0f);
    }

    public void SetYAxis() => _yAxisIsInverted = !_yAxisIsInverted;
    public void SetXAxis() => _xAxisIsInverted = !_xAxisIsInverted;
    public void StopRumble() => _gamePad.SetMotorSpeeds(0f,0f);
}