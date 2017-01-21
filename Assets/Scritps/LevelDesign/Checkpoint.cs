using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public WaveShape Shape;
    public Rythm Note;

    // Use this for initialization
    void Start()
    {
        CheckpointsManager.Instance.addCheckPoint(this);
    }
}
