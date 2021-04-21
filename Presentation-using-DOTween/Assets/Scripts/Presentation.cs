using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class Presentation : MonoBehaviour
{
    [SerializeField] Sprite[] images;
    public float changeSlideTime = 1.0f;

    private Image backgroundImage00;
    private Image backgroundImage01;
    private Text titleText;
    private Text slideTitleText;
    private Text slideMainText;

    private int tweeningCount = 0;

    private Color visible = new Color(1f, 1f, 1f, 1f);
    private Color invisible = new Color(1f, 1f, 1f, 0f);

    void Start()
    {
        // 各種コンポーネント取得
        GameObject image00Object = GameObject.Find("PresentationImage00");
        GameObject image01Object = GameObject.Find("PresentationImage01");
        GameObject titleObject = GameObject.Find("PresentationTitle");
        GameObject slideTitleObject = GameObject.Find("PresentationSlideTitle");
        GameObject slideMainTextObject = GameObject.Find("PresentationMainText");
        this.backgroundImage00 = image00Object.GetComponent<Image>();
        this.backgroundImage01 = image01Object.GetComponent<Image>();
        this.titleText = titleObject.GetComponent<Text>();
        this.slideTitleText = slideTitleObject.GetComponent<Text>();
        this.slideMainText = slideMainTextObject.GetComponent<Text>();

        // プレゼンテーション開始
        StartCoroutine ("PresentationCoroutine");
    }
    
    // プレゼンテーション
    private IEnumerator PresentationCoroutine()
    {
        // 初期化 ////////////////////////////////////////////////////////////////
        InitializePresentation();
        yield return new WaitUntil(() => WaitKey());
        // ↓本文↓ ////////////////////////////////////////////////////////////////


        // スライド01 ////////////////////////////////////////////////////////////
        SetPresentationTitle("Unityでプレゼンテーションするサンプル", 1.0f, TextAnchor.MiddleCenter);
        yield return new WaitUntil(() => WaitTweening());
        yield return new WaitUntil(() => WaitKey());

        // スライド02 ////////////////////////////////////////////////////////////
        SetPresentationNextSlideImage(images[1]);
        yield return new WaitUntil(() => WaitTweening());
        yield return new WaitUntil(() => WaitKey());
        
        SetPresentationSlideTitle("スライドタイトル01", 0.2f, TextAnchor.MiddleLeft);
        yield return new WaitUntil(() => WaitTweening());
        yield return new WaitUntil(() => WaitKey());

        SetPresentationSlideMainText(
            "あああああああああああああああ\nいいいいいいいいいいいいいいい\nううううううううううううううう\nえええええええええええええええ\nおおおおおおおおおおおおおおお", 
            1.0f,
            TextAnchor.MiddleLeft
        );
        yield return new WaitUntil(() => WaitTweening());
        yield return new WaitUntil(() => WaitKey());

        // スライド03 ////////////////////////////////////////////////////////////
        SetPresentationNextSlideImage(images[2]);
        
        SetPresentationSlideTitle("スライドタイトル02", 1.0f, TextAnchor.MiddleCenter);

        SetPresentationSlideMainText(
            "あああああああああああああああ\nいいいいいいいいいいいいいいい\nううううううううううううううう\nえええええええええええええええ\nおおおおおおおおおおおおおおお", 
            1.0f,
            TextAnchor.MiddleCenter
        );
        yield return new WaitUntil(() => WaitTweening());
        yield return new WaitUntil(() => WaitKey());


        // ↑本文↑ ////////////////////////////////////////////////////////////////
        // リスタート ////////////////////////////////////////////////////////////
        StartCoroutine ("PresentationCoroutine");
    }

    // プレゼンテーション表示内容初期化
    private void InitializePresentation()
    {
        InitializePresentationText();

        this.backgroundImage00.sprite = images[0];
        this.backgroundImage00.color = visible;
        this.backgroundImage01.color = invisible;
    }

    // プレゼンテーション テキスト初期化
    private void InitializePresentationText()
    {
        this.titleText.text = "";
        this.slideTitleText.text = "";
        this.slideMainText.text = "";
    }

    // プレゼンテーション：タイトル
    private void SetPresentationTitle(string text, float duration, TextAnchor anchor)
    {
        this.titleText.alignment = anchor;
    
        this.tweeningCount = this.tweeningCount + 1;
        this.titleText.DOText(
            text, 
            duration
        ).SetEase(Ease.Linear).OnComplete(() => this.tweeningCount = this.tweeningCount - 1);
    }

    // プレゼンテーション：スライドタイトル
    private void SetPresentationSlideTitle(string text, float duration, TextAnchor anchor)
    {
        this.slideTitleText.alignment = anchor;
    
        this.tweeningCount = this.tweeningCount + 1;
        this.slideTitleText.DOText(
            text, 
            duration
        ).SetEase(Ease.Linear).OnComplete(() => this.tweeningCount = this.tweeningCount - 1);
    }

    // プレゼンテーション：本文
    private void SetPresentationSlideMainText(string text, float duration, TextAnchor anchor)
    {
        this.slideMainText.alignment = anchor;
    
        this.tweeningCount = this.tweeningCount + 1;
        this.slideMainText.DOText(
            text, 
            duration
        ).SetEase(Ease.Linear).OnComplete(() => this.tweeningCount = this.tweeningCount - 1);
    }

    // プレゼンテーション：次スライド
    private void SetPresentationNextSlideImage(Sprite nextImage)
    {
        InitializePresentationText();

        this.backgroundImage01.sprite = nextImage;

        this.tweeningCount = this.tweeningCount + 1;
        DOTween.ToAlpha(
            () => this.backgroundImage00.color,
            color => this.backgroundImage00.color = color,
            0f,
            changeSlideTime
        );
        DOTween.ToAlpha(
            () => this.backgroundImage01.color,
            color => this.backgroundImage01.color = color,
            1f,
            changeSlideTime
        ).OnComplete(() => {
            this.tweeningCount = this.tweeningCount - 1;

            this.backgroundImage00.sprite = this.backgroundImage01.sprite;
            this.backgroundImage00.color = visible;

            this.backgroundImage01.color = invisible;
        });
    }

    // アニメーション待ち
    private bool WaitTweening()
    {
        return (this.tweeningCount == 0);
    }

    // キー押下待ち
    private bool WaitKey()
    {
        return (
               Input.GetKeyDown(KeyCode.Return) 
            || Input.GetKeyDown(KeyCode.DownArrow) 
            || Input.GetKeyDown(KeyCode.PageDown) 
            || Input.GetMouseButtonDown(0)
        );
    }
}
