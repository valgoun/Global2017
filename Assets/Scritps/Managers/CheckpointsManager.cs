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

    private static CheckpointsManager _instance;
    private List<Checkpoint> _points;

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
    public void addCheckPoint(Checkpoint cp)
    {
        if (_points == null)
        {
            _points = new List<Checkpoint>();
            _points.Add(cp);
            return;
        }
        for (int i = 0; i < _points.Count; i++)
        {
            var p = _points[i];
            if (cp.transform.position.x < p.transform.position.x)
            {
                _points.Insert(i, cp);
                break;
            }
        }
    }

    public Checkpoint Pop()
    {
        Checkpoint p;
        if (_points.Count != 0)
            p = _points[0];
        else
            return null;
        _points.Remove(p);
        return p;
    }
}
