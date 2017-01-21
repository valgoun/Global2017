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
    public static Character Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            else
            {
                Debug.LogError("Character is not existant !");
                return null;
            }
        }
    }
    public float Bpm = 60;
    public float InputWindow = 0.5f;
    public float DoubleInputPrecision = 0.2f;
    public float Amplitude = 3;
    public AnimationCurve SquareCurve;
    public RippleEffect Effect;

    private SpriteRenderer _spriteRenderer;
    private bool _inputSensible = true;
    private float _timeBeforeNextWindow;
    private WaveShape _nextShape;
    private Tween _movement;
    private bool _shouldCreate = false, _rythm, _shape;
    private float _direction = 1.0f;
    private static Character _instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (_instance != null)
            Destroy(this.gameObject);
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _timeBeforeNextWindow = 30f / Bpm;
        _nextShape = WaveShape.Linear;
        //_movement = transform.DOMoveY(3, _timeBeforeNextWindow / 2).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => CreateTween());
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
                if (!_rythm)
                {
                    _shouldCreate = true;
                    _rythm = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetButtonDown("Noire"))
            {
                // Debug.Log("Noire");
                _timeBeforeNextWindow = 60f / Bpm;
                if (!_rythm)
                {
                    _shouldCreate = true;
                    _rythm = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetButtonDown("Triolet"))
            {
                // Debug.Log("Triolet");
                _timeBeforeNextWindow = 90f / Bpm;
                if (!_rythm)
                {
                    _shouldCreate = true;
                    _rythm = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetButtonDown("Blanche"))
            {
                // Debug.Log("Blanche");
                _timeBeforeNextWindow = 120f / Bpm;
                if (!_rythm)
                {
                    _shouldCreate = true;
                    _rythm = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }

            //Shape Input
            else if (Input.GetAxis("Sin") < 0)
            {
                _nextShape = WaveShape.Sin;
                if (!_shape)
                {
                    _shouldCreate = true;
                    _shape = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetAxis("Shape") > 0)
            {
                _nextShape = WaveShape.Linear;
                if (!_shape)
                {
                    _shouldCreate = true;
                    _shape = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
            else if (Input.GetAxis("Shape") < 0)
            {
                _nextShape = WaveShape.Square;
                if (!_shape)
                {
                    _shouldCreate = true;
                    _shape = true;
                    DOVirtual.DelayedCall(DoubleInputPrecision, () => CreateTween());
                }
            }
        }
        else
            _spriteRenderer.color = Color.white;

    }

    private void CreateTween()
    {
        if (_shouldCreate && (_movement == null || !_movement.IsPlaying()))
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.x /= Screen.width;
            pos.y /= Screen.height;
            Effect.Emit(pos);
            switch (_nextShape)
            {
                case WaveShape.Linear:
                    _movement = transform.DOMoveY(Amplitude * _direction, _timeBeforeNextWindow / 2 * 0.9f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(() => CreateTween());
                    break;
                case WaveShape.Sin:
                    _movement = transform.DOMoveY(Amplitude * _direction, _timeBeforeNextWindow / 2 * 0.8f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => CreateTween());
                    break;
                case WaveShape.Square:
                    _movement = transform.DOMoveY(Amplitude * _direction, _timeBeforeNextWindow / 2 * 0.8f).SetEase(SquareCurve).SetLoops(2, LoopType.Yoyo).OnComplete(() => CreateTween());
                    break;
                default:
                    break;
            }
            DOVirtual.DelayedCall(0.3f, () =>
            {
                _shouldCreate = false;
                _shape = false;
                _rythm = false;
            });
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("caca");
    }

    public void ChangeDirection(float direction)
    {
        _direction = direction;
    }
}
