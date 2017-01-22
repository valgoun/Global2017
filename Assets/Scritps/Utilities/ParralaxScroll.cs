using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxScroll : MonoBehaviour
{
    public bool Maskable = false;
    public int Sign = 1;
    public float Size, Speed;
    private SpriteRenderer _sprite;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

        transform.position += Vector3.left * Speed * Time.deltaTime;
        _sprite.sortingOrder = transform.GetSiblingIndex() % 2 * Sign;
        if (transform.localPosition.x < -Size)
            Decal();

    }

    /// <summary>
    /// OnBecameInvisible is called when the renderer is no longer visible by any camera.
    /// </summary>
    void Decal()
    {
        var pos = transform.position;
        pos.x = transform.parent.GetChild(0).position.x + Size;
        transform.position = pos;
        transform.SetAsFirstSibling();
        if (Maskable)
            _sprite.sortingOrder = MaskManager.Instance.getMaskId() * Sign;
    }
}
