using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour
{
    public static CheckpointsManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject Level;
    private static CheckpointsManager _instance;
    public List<Checkpoint> _points;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("there already is a MaskManager");
            Destroy(this.gameObject);
        }
        else
            _instance = this;


    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        var Cps = Level.GetComponentsInChildren<Checkpoint>();
        _points = new List<Checkpoint>(Cps);
        /*for (int i = 0; i < Cps.Length; i++)
        {
            var cp = Cps[i];
            int index = _points.FindIndex((p) => p.transform.position.x <= cp.transform.position.x);
            _points.Insert(index, cp);
        }*/
    }

    public Checkpoint Pop()
    {
        Checkpoint p;
        if (_points.Count != 0)
            p = _points[0];
        else
            return null;
        _points.Remove(p);
        _points.Add(p);
        return p;
    }
}
