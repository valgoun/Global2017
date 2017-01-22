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



    private bool _one = true;

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


    public int getMaskId()
    {
        if (_one)
        {
            _one = false;
            return 1;
        }
        else
        {
            _one = true;
            return 0;
        }
    }




}
