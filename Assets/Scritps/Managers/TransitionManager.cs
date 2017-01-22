using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public GameObject RedBG, BlueBG, PurpleBG, GreenBG, NeutralBG;
    public GameObject RedMG, BueMG, PurpleMG, GreenMG, NeutralMG;
    public GameObject RedC, BueC, PurpleC, GreenC;
    public GameObject NeutralFG, PurpleFG;
    public Image WhiteScreen;
    public Transform Camera;
    public AudioSource BlueMusic, RedMusic, PurpleMusic, GreenMusic, Poow;
    public AudioClip PowClip;
    private static TransitionManager _instance;


    private AudioSource _current;

    void Awake()
    {
        if (_instance == null)
            _instance = this;

        _current = BlueMusic;
    }



    public void GoTo(Rythm color)
    {
        WhiteScreen.DOFade(0.25f, 0.05f).SetEase(Ease.Flash).SetLoops(2, LoopType.Yoyo);
        Camera.DOShakePosition(0.5f);
        switch (color)
        {
            case Rythm.Quaver:
                desactivate(BlueBG);
                desactivate(NeutralBG);
                desactivate(NeutralMG);
                desactivate(PurpleBG);
                desactivate(GreenBG);
                desactivate(BueMG);
                desactivate(PurpleMG);
                desactivate(GreenMG);
                activate(RedBG);
                activate(RedMG);
                activate(RedC);
                desactivate(BueC);
                desactivate(PurpleC);
                desactivate(GreenC);
                _current.Pause();
                RedMusic.Play();
                _current = RedMusic;
                Poow.PlayOneShot(PowClip, 1.2f);
                break;
            case Rythm.white:
                desactivate(NeutralBG);
                desactivate(NeutralMG);
                activate(BlueBG);
                desactivate(PurpleBG);
                desactivate(GreenBG);
                activate(BueMG);
                desactivate(PurpleMG);
                desactivate(GreenMG);
                desactivate(RedBG);
                desactivate(RedMG);
                desactivate(RedC);
                activate(BueC);
                desactivate(PurpleC);
                desactivate(GreenC);
                _current.Pause();
                BlueMusic.Play();
                _current = BlueMusic;
                Poow.PlayOneShot(PowClip, 1.2f);
                break;
            case Rythm.Black:
                desactivate(NeutralBG);
                desactivate(NeutralMG);
                desactivate(BlueBG);
                activate(PurpleBG);
                desactivate(GreenBG);
                desactivate(BueMG);
                activate(PurpleMG);
                desactivate(GreenMG);
                desactivate(RedBG);
                desactivate(RedMG);
                desactivate(RedC);
                desactivate(BueC);
                activate(PurpleC);
                desactivate(GreenC);
                _current.Pause();
                PurpleMusic.Play();
                _current = PurpleMusic;
                Poow.PlayOneShot(PowClip, 1.2f);
                break;
            case Rythm.Triolet:
                desactivate(NeutralBG);
                desactivate(NeutralMG);
                desactivate(BlueBG);
                desactivate(PurpleBG);
                activate(GreenBG);
                desactivate(BueMG);
                desactivate(PurpleMG);
                activate(GreenMG);
                desactivate(RedBG);
                desactivate(RedMG);
                desactivate(RedC);
                desactivate(BueC);
                desactivate(PurpleC);
                activate(GreenC);
                _current.Pause();
                GreenMusic.Play();
                _current = GreenMusic;
                Poow.PlayOneShot(PowClip, 1.2f);
                break;
            default:
                break;
        }
    }

    private void desactivate(GameObject obj)
    {
        foreach (var sp in obj.GetComponentsInChildren<SpriteRenderer>())
        {
            sp.DOFade(0, 0.3f);
        }
        DOVirtual.DelayedCall(0.3f, () => obj.SetActive(false));
    }

    private void activate(GameObject obj)
    {
        obj.SetActive(true);
        foreach (var sp in obj.GetComponentsInChildren<SpriteRenderer>())
        {
            sp.DOFade(1, 0.3f);
        }
    }

}
