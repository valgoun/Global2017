using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tweerk : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.DOScaleY(0.7f, 0.2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo);
        transform.DOScaleX(0.7f, 0.2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
