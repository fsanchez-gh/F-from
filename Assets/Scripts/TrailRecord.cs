using System.Collections.Generic;
using UnityEngine;

public class TrailRecord : MonoBehaviour
{
    private LineRenderer _trail;
    private List<Vector3> _trailPositions = new List<Vector3>();

    private void Awake() => _trail = GetComponent<LineRenderer>();

    public void RecordPosition(Vector3 trailPosition) => _trailPositions.Add(trailPosition);

    public void SetLineRendererPositions()
    {
        var totalPositions = _trailPositions.Count;

        _trail.positionCount = totalPositions;
    } 
    
    public void DrawFlight()
    {
        for (var position = 0; position < _trailPositions.Count; position++)
            _trail.SetPosition(position,_trailPositions[position]);
    }
    public void ResetTrailPositions()
    {
        _trailPositions.Clear();
        _trail.positionCount = 0;
    }
}
