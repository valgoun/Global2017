using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    public static MaskManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private static MaskManager _instance;



    private List<Block> _blocks;

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



    // Use this for initialization
    void Start()
    {
        _blocks = new List<Block>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Subscribe(Block b)
    {
        _blocks.Add(b);
        int n = _blocks.Count;
        b.SetLayers(2 * (n - 1), 2 * (n - 1) + 1);
    }

    public void Unuscribe(Block b)
    {
        _blocks.Remove(b);
        int n = 0;
        foreach (var bl in _blocks)
        {
            bl.SetLayers(2 * n, 2 * n + 1);
            n++;
        }
    }
}
