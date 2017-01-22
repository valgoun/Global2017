using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{

    public GameObject LevelDesign;
    public Transform ShapeIndicator;

    public Image Tuto, Credits;

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

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("Croche"))
        {
            if (!Title.gameObject.activeInHierarchy)
            {
                Tuto.gameObject.SetActive(false);
                Credits.gameObject.SetActive(false);
                Title.gameObject.SetActive(true);
            }
        }
    }

    public void ShowTuto()
    {
        Tuto.gameObject.SetActive(true);
        Title.gameObject.SetActive(false);
    }

    public void ShowCredit()
    {
        Credits.gameObject.SetActive(true);
        Title.gameObject.SetActive(false);
    }
}
