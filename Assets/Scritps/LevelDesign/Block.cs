﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public List<SpriteMaskRenderer> _masks;
    public SpriteRenderer _graph;
    private bool _onScreen = false;
    private float _left;
    private bool _active = false;
    // Use this for initialization
    void Start()
    {
        _left = _graph.bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * 4f / 15f * Character.Instance.Bpm;
        if (!_onScreen && transform.position.x < 32.5f)
        {
            MaskManager.Instance.Subscribe(this);
        }
        else if (_onScreen && transform.position.x < -32.5f)
        {
            MaskManager.Instance.Subscribe(this);
        }

        if (transform.position.x < _left + Character.Instance.transform.position.x && !_active)
        {
            Character.Instance.ChangeDirection(Mathf.Sign(transform.up.y));
            _active = true;
        }
    }

    public void SetLayers(int graphLayer, int maskLayer)
    {
        _graph.sortingOrder = graphLayer;
        foreach (var m in _masks)
        {
            m.sortingOrder = maskLayer;
        }
    }


}
