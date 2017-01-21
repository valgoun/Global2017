using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxScroll : MonoBehaviour
{

    public float Size, Speed;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
    }

    /// <summary>
    /// OnBecameInvisible is called when the renderer is no longer visible by any camera.
    /// </summary>
    void OnBecameInvisible()
    {
        var pos = transform.position;
        pos.x = transform.parent.GetChild(0).position.x + Size;
        transform.position = pos;
        transform.SetAsFirstSibling();
    }
}
