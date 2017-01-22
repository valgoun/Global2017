using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{

    public GameObject LevelDesign;
    public Transform ShapeIndicator;

    public Image Title;

    private List<Block> _ld;
    // Use this for initialization
    void Start()
    {
        _ld = new List<Block>(LevelDesign.GetComponentsInChildren<Block>());
    }

    public void Play()
    {
        Title.transform.DOMoveY(900, 0.5f).SetEase(Ease.OutExpo).SetRelative().OnComplete(() => Title.gameObject.SetActive(false));
        ShapeIndicator.DOMoveY(300, 0.5f).SetEase(Ease.OutExpo).SetRelative();
        _ld.ForEach((b) => b.activate());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
