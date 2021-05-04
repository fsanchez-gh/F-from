using System;
using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Trail : MonoBehaviour
{
    [SerializeField] private TrailTypes type;
    [SerializeField] public float recordTime;

    private UIManager uiManager;
    private TrailRecord record;

    private enum TrailTypes
    {
        Undefined,
        Right,
        Left
    }

    private void Awake() => Assignations();

    private void Start() => StartCoroutine(SetPositionCoroutine());

    private void Assignations()
    {
        uiManager = FindObjectOfType<UIManager>();
        switch (type)
        {
            case TrailTypes.Right:
                record = GameObject.FindGameObjectWithTag("R_Record").GetComponent<TrailRecord>();
                break;
            case TrailTypes.Left:
                record = GameObject.FindGameObjectWithTag("L_Record").GetComponent<TrailRecord>();
                break;
            case TrailTypes.Undefined:
                Debug.Log("Trail record not defined");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator SetPositionCoroutine()
    {
        if (!uiManager.GameIsPaused)
            record.RecordPosition(transform.position);
        yield return new WaitForSeconds(recordTime);
        StartCoroutine(SetPositionCoroutine());
    }
    public void StopSetPosition() => StopCoroutine(SetPositionCoroutine());
}
