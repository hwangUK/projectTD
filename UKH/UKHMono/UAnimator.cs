using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UAnimator : MonoBehaviour
{

    [SerializeField] private List<Sprite> _imgs;

    [SerializeField] private List<float> _animPlayTime;

    private IEnumerator     _coRenderAnim;
    private SpriteRenderer  _spriterRenderer;

    private int             _playPos;
    // Start is called before the first frame update
    void Start()
    {
        _spriterRenderer = GetComponent<SpriteRenderer>();
        StopAnim();
    }

    public void StartAnim()
    {
        if(_imgs.Count == 0)
        {
            Debug.LogError("EX : 이미지 파일 없음");
        }
        _playPos = 0;
        _spriterRenderer.DOFade(1f, 0.2f);

        if (_coRenderAnim != null)
        {
            StopCoroutine(_coRenderAnim);
        }
        _coRenderAnim = CoRenderUpdate();

        StartCoroutine(_coRenderAnim);
    }

    public void StopAnim()
    {
        if (_coRenderAnim != null)
        {
            StopCoroutine(_coRenderAnim);
            _spriterRenderer.DOFade(0f, 0.5f);
        }
    }

    IEnumerator CoRenderUpdate()
    {
        while(_playPos < _imgs.Count)
        {
            _spriterRenderer.sprite = _imgs[_playPos];
            yield return new WaitForSeconds(_animPlayTime[_playPos]);
            _playPos++;
        }
        StopAnim();
    }
}
