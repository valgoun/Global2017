using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hover : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.DOLocalMoveY(0.2f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
