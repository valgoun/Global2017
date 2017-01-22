using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float Size;
    private bool _active = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (transform.GetSiblingIndex() != 0)
            Size = transform.position.x - transform.parent.GetChild(transform.GetSiblingIndex() - 1).position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_active)
            transform.position += Vector3.left * Time.deltaTime * 4f / 15f * Character.Instance.Bpm;
        if (transform.position.x < -Size)
        {
            transform.position = transform.parent.GetChild(transform.parent.childCount - 1).position + Vector3.right * Size;
            transform.SetAsLastSibling();
        }
    }

    public void activate()
    {
        _active = true;
    }

}
