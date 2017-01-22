using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private bool _active = false;

    // Update is called once per frame
    void Update()
    {
        if (_active)
            transform.position += Vector3.left * Time.deltaTime * 4f / 15f * Character.Instance.Bpm;
    }

    public void activate()
    {
        _active = true;
    }

}
