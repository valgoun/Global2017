using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private static DeathManager _instance;

    public GameObject Level, MiddleGround, ForeGround;

    private List<Block> ldBlock;
    private List<ParralaxScroll> parralaxList;

    /// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    void Start()
    {
        ldBlock = new List<Block>(Level.GetComponentsInChildren<Block>());
        parralaxList = new List<ParralaxScroll>(MiddleGround.GetComponentsInChildren<ParralaxScroll>());
        parralaxList.AddRange(ForeGround.GetComponentsInChildren<ParralaxScroll>());
    }

    public void Death()
    {
        ldBlock.ForEach((b) => b.enabled = false);
        parralaxList.ForEach((p) => p.enabled = false);
    }

}
