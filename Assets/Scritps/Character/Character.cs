using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public enum WaveShape
{
    Sin,
    Linear,
    Square
}

public enum Rythm
{
    white,
    Triolet,
    Black,
    Quaver,
    Neutral
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
    public float Amplitude = 3;
    public AnimationCurve SquareCurve;
    public RippleEffect Effect;
    public Image Center, Right, Left;
    public Sprite Sin, Square, Lin;
    public AudioClip Death;
    private float _timeBeforeNextWindow;
    private WaveShape _nextShape;
    private Rythm _note, _actualNote;
    private Tween _movement;
    private bool _shouldCreate = false;
    private float _direction = 1.0f;
    private Checkpoint _activeCheckpoint;
    private static Character _instance;
    private bool _alive = true;
    private bool _Tl, _Tr;
    private AudioSource _source;

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
        _timeBeforeNextWindow = 30f / Bpm;
        _nextShape = WaveShape.Sin;
        _actualNote = Rythm.Neutral;
        _activeCheckpoint = CheckpointsManager.Instance.Pop();
        _Tr = false;
        _Tl = false;
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _activeCheckpoint.transform.position) < InputWindow && transform.position.x < _activeCheckpoint.transform.position.x)
        {

            //frequence inputs
            if (Input.GetButtonDown("Croche"))
            {
                // Debug.Log("Croche");
                _timeBeforeNextWindow = 30f / Bpm;
                _note = Rythm.Quaver;
                _shouldCreate = true;
            }
            else if (Input.GetButtonDown("Noire"))
            {
                // Debug.Log("Noire");
                _timeBeforeNextWindow = 60f / Bpm;
                _note = Rythm.Black;
                _shouldCreate = true;
            }
            else if (Input.GetButtonDown("Triolet"))
            {
                Debug.Log("Triolet");
                _timeBeforeNextWindow = 90f / Bpm;
                _note = Rythm.Triolet;
                _shouldCreate = true;
            }
            else if (Input.GetButtonDown("Blanche"))
            {
                // Debug.Log("Blanche");
                _timeBeforeNextWindow = 120f / Bpm;
                _note = Rythm.white;
                _shouldCreate = true;
            }

        };
        if (transform.position.x >= _activeCheckpoint.transform.position.x)
        {
            if (_shouldCreate)
            {
                Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
                pos.x /= Screen.width;
                pos.y /= Screen.height;
                Effect.Emit(pos);
                _shouldCreate = false;

                if (_nextShape == _activeCheckpoint.Shape && _note == _activeCheckpoint.Note)
                {
                    switch (_nextShape)
                    {
                        case WaveShape.Linear:
                            _movement = transform.DOMoveY(Amplitude * _activeCheckpoint.transform.up.y, _timeBeforeNextWindow / 2 * 0.9f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
                            break;
                        case WaveShape.Sin:
                            _movement = transform.DOMoveY(Amplitude * _activeCheckpoint.transform.up.y, _timeBeforeNextWindow / 2 * 0.9f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
                            break;
                        case WaveShape.Square:
                            _movement = transform.DOMoveY(Amplitude * _activeCheckpoint.transform.up.y, _timeBeforeNextWindow / 2 * 0.7f).SetEase(SquareCurve).SetLoops(2, LoopType.Yoyo);
                            break;
                        default:
                            break;
                    }
                    if (_note != _actualNote)
                        TransitionManager.Instance.GoTo(_note);
                    var p = CheckpointsManager.Instance.Pop();
                    if (p != null)
                        _activeCheckpoint = p;
                }
            }
            else if (_alive)
            {
                _alive = false;
                Debug.Log("caca");
                DeathManager.Instance.Death();
                _source.PlayOneShot(Death);
                for (int i = 0; i < 4; i++)
                {
                    var t = transform.GetChild(i);
                    if (t.gameObject.activeInHierarchy)
                        t.GetComponent<Animator>().SetTrigger("Death");
                }
            }

        }

        if (Input.GetAxis("ShapeR") > 0 && !_Tl)
        {
            _Tl = true;
            switch (_nextShape)
            {
                case WaveShape.Linear:
                    _nextShape = WaveShape.Square;
                    Center.sprite = Square;
                    Right.sprite = Sin;
                    Left.sprite = Lin;
                    break;
                case WaveShape.Sin:
                    _nextShape = WaveShape.Linear;
                    Center.sprite = Lin;
                    Right.sprite = Square;
                    Left.sprite = Sin;
                    break;
                case WaveShape.Square:
                    _nextShape = WaveShape.Sin;
                    Center.sprite = Sin;
                    Right.sprite = Lin;
                    Left.sprite = Square;
                    break;
                default:
                    break;
            }
        }
        else if (Input.GetAxis("ShapeR") <= 0)
        {
            _Tl = false;
        }
        if (Input.GetAxis("ShapeL") > 0 && !_Tr)
        {
            _Tr = true;
            switch (_nextShape)
            {
                case WaveShape.Linear:
                    _nextShape = WaveShape.Sin;
                    Center.sprite = Sin;
                    Right.sprite = Lin;
                    Left.sprite = Square;
                    break;
                case WaveShape.Sin:
                    _nextShape = WaveShape.Square;
                    Center.sprite = Square;
                    Right.sprite = Sin;
                    Left.sprite = Lin;
                    break;
                case WaveShape.Square:
                    _nextShape = WaveShape.Linear;
                    Center.sprite = Lin;
                    Right.sprite = Square;
                    Left.sprite = Sin;
                    break;
                default:
                    break;
            }
        }
        else if (Input.GetAxis("ShapeL") <= 0)
        {
            _Tr = false;
        }


    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("caca");
    }

    public void ChangeDirection(float direction)
    {
        _direction = direction;
    }
}
