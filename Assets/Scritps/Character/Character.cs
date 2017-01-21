using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum WaveShape
{
    Sin,
    Linear,
    Square
}
public class Character : MonoBehaviour
{
    public float Speed = 6f;
    public float Bpm = 60;
    public float InputWindow = 0.5f;
    public float DoubleInputPrecision = 0.2f;
    public AnimationCurve SquareCurve;
    public RippleEffect Effect;

    private SpriteRenderer _spriteRenderer;
    private bool _inputSensible = true;
    private float _timeBeforeNextWindow;
    private WaveShape _nextShape;
    private Tween _movement;
    private bool _shouldCreate = false;

    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _timeBeforeNextWindow = 30f / Bpm;
        _nextShape = WaveShape.Linear;
        _movement = transform.DOMoveY(3, _timeBeforeNextWindow / 2).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => CreateTween());
        //transform.DOMoveX(8, 10).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.y) < (3 * InputWindow) / _timeBeforeNextWindow)
        {
            _spriteRenderer.color = Color.green;

            //frequence inputs
            if (Input.GetButtonDown("Croche"))
            {
                // Debug.Log("Croche");
                _timeBeforeNextWindow = 30f / Bpm;
                if (!_shouldCreate)
                {
                    _shouldCreate = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetButtonDown("Noire"))
            {
                // Debug.Log("Noire");
                _timeBeforeNextWindow = 60f / Bpm;
                if (!_shouldCreate)
                {
                    _shouldCreate = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetButtonDown("Triolet"))
            {
                // Debug.Log("Triolet");
                _timeBeforeNextWindow = 90f / Bpm;
                if (!_shouldCreate)
                {
                    _shouldCreate = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetButtonDown("Blanche"))
            {
                // Debug.Log("Blanche");
                _timeBeforeNextWindow = 120f / Bpm;
                if (!_shouldCreate)
                {
                    _shouldCreate = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }

            //Shape Input
            else if (Input.GetButtonDown("Sin"))
            {
                _nextShape = WaveShape.Sin;
                if (!_shouldCreate)
                {
                    _shouldCreate = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetButtonDown("Linear"))
            {
                _nextShape = WaveShape.Linear;
                if (!_shouldCreate)
                {
                    _shouldCreate = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetButtonDown("Square"))
            {
                _nextShape = WaveShape.Square;
                if (!_shouldCreate)
                {
                    _shouldCreate = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
        }
        else
            _spriteRenderer.color = Color.white;

    }

    private void CreateTween()
    {
        if (_shouldCreate && !_movement.IsPlaying())
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.x /= Screen.width;
            pos.y /= Screen.height;
            Effect.Emit(pos);
            switch (_nextShape)
            {
                case WaveShape.Linear:
                    _movement = transform.DOMoveY(3, _timeBeforeNextWindow / 2).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(() => CreateTween());
                    break;
                case WaveShape.Sin:
                    _movement = transform.DOMoveY(3, _timeBeforeNextWindow / 2).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => CreateTween());
                    break;
                case WaveShape.Square:
                    _movement = transform.DOMoveY(3, _timeBeforeNextWindow / 2).SetEase(SquareCurve).SetLoops(2, LoopType.Yoyo).OnComplete(() => CreateTween());
                    break;
                default:
                    break;
            }
            DOVirtual.DelayedCall(0.3f, () => _shouldCreate = false);
        }
    }
}
