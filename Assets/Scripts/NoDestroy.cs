using UnityEngine;

public class NoDestroy : MonoBehaviour
{
    [SerializeField] private TrailRecord
        rightTrailRecord,
        leftTrailRecord;

    private static NoDestroy _instance = null;

    private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                GameObject.DontDestroyOnLoad(gameObject);
            }
        }

    public void CallSetSetLineRendererPositions()
    {
        rightTrailRecord.SetLineRendererPositions();
        leftTrailRecord.SetLineRendererPositions();
    }

    public void DrawFlights()
    {
        rightTrailRecord.DrawFlight();
        leftTrailRecord.DrawFlight();
    }

    public void ResetTrailsPositions()
    {
        rightTrailRecord.ResetTrailPositions();
        leftTrailRecord.ResetTrailPositions();
    }
}
