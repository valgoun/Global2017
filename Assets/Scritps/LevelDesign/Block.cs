using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public List<SpriteMaskRenderer> _masks;
    public SpriteRenderer _graph;
    private bool _onScreen = false;
    // Use this for initialization
    void Start()
    {

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
