using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tweerk : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.DOScaleY(0.8f, 0.2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo).SetRelative();
        transform.DOScaleX(1.2f, 0.2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo).SetRelative();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
