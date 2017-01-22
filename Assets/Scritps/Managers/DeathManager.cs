using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
    public Transform GameOver, GameUi;
    public Image BlackScreen;

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
        BlackScreen.DOFade(0, 0.3f).OnComplete(() => BlackScreen.transform.SetAsFirstSibling());
        ldBlock = new List<Block>(Level.GetComponentsInChildren<Block>());
        parralaxList = new List<ParralaxScroll>(MiddleGround.GetComponentsInChildren<ParralaxScroll>());
        parralaxList.AddRange(ForeGround.GetComponentsInChildren<ParralaxScroll>());
    }

    public void Death()
    {
        ldBlock.ForEach((b) => b.enabled = false);
        parralaxList.ForEach((p) => p.enabled = false);
        GameOver.DOMoveY(500, 0.2f).SetEase(Ease.OutBack).SetDelay(0.3f);
        GameOver.GetComponent<Image>().DOFade(0, 0.2f).SetDelay(2.6f);
        GameUi.DOMoveY(-200, 0.2f).SetEase(Ease.OutBack).SetDelay(0.3f);
        BlackScreen.DOFade(1, 0.3f).SetEase(Ease.OutQuad).SetDelay(0.2f);
        DOVirtual.DelayedCall(3, () => SceneManager.LoadScene(0));
    }

}
