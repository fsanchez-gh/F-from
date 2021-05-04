using UnityEngine;

public class LaunchControl : MonoBehaviour
{
    [SerializeField] private float impulseSpeed;
    [SerializeField, Range(5f, 9f)] private float endLaunchTime = 7f;

    [SerializeField] private GameObject
        walls,
        trails,
        pointLight;

    private FlightControl _flightControl;

    private void Awake() => _flightControl = GetComponent<FlightControl>();

    private void Start() => Invoke(nameof(ActivateFlightProcedure), endLaunchTime);

    private void Update() => Impulse();

    private void Impulse() => transform.Translate(impulseSpeed * Time.deltaTime * Vector3.forward);

    private void ActivateFlightProcedure()
    {
        trails.SetActive(true);
        walls.SetActive(false);
        pointLight.SetActive(true);
        _flightControl.enabled = true;
        enabled = false;
    }
}
