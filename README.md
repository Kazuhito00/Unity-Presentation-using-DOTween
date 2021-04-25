# Unity-Presentation-using-DOTween
UnityのDOTweenアセットを用いたプレゼンテーションの試験的なサンプルです。<br><br>
<img src="https://user-images.githubusercontent.com/37477845/115559147-637d3c00-a2ee-11eb-8751-a6dad0807372.gif" width="65%"><br>

ひとまず、以下の簡易な操作のみ実装しています。<br>
今後LT等で実際に使用していく上で、不定期に機能を増やすかもしれません。<br>
* プレゼンテーション タイトル表示
* スライド タイトル表示
* スライド 本文表示
* スライド画像の切り替え(クロスフェード)

# Demo
動作確認用デモは以下。<br>
[https://kazuhito00.github.io/Unity-Presentation-using-DOTween/WebGL-Build](https://kazuhito00.github.io/Unity-Presentation-using-DOTween/WebGL-Build/)

# Requirement (Unity)
* Unity 2020.2.3f1 or later
* DOTween 1.2.420 or later<br>※リポジトリには同梱していないためアセットストアから導入してください

# Preparation
前準備として以下2点が必要です。
1. 画像の登録(①、②)
2. フォントの登録(③)

① 画像を登録し、Texture を Type Sprite(2D and UI) に変更<br>
<img src="https://user-images.githubusercontent.com/37477845/115728860-d73b4980-a3bf-11eb-93b7-7082ffea38bf.png" width="100%"><br><br>
② EventSystem の Images に画像を設定 ※Element 0が起動時の画像になります<br>
<img src="https://user-images.githubusercontent.com/37477845/115728709-aeb34f80-a3bf-11eb-9afc-2a5da62d09d8.png" width="100%"><br><br>
③ PresentationTitle、PresentationSlideTitle、PresentationMainText の Fontを任意のフォントに変更<br>
<img src="https://user-images.githubusercontent.com/37477845/115728745-b541c700-a3bf-11eb-8fc8-90ebfe18c0a8.png" width="100%"><br><br>

# Code
「Assets/Scripts/Presentation.cs」の「PresentationCoroutine()」にプレゼンテーションの動作を記載します。

・タイトル表示<br>
　引数1：表示テキスト<br>
　引数2：アニメーション時間　※0.0f指定時はアニメーション無し<br>
　引数3：表示位置<br>
```
SetPresentationTitle("Unityでプレゼンテーションするサンプル", 1.0f, TextAnchor.MiddleCenter);
```

・スライド画像変更<br>
　引数1：次画像　※以下例はElement 1指定時<br>
　引数2：アニメーション時間　※0.0f指定時はアニメーション無し<br>
```
SetPresentationNextSlideImage(images[1], 1.0f);
```

・スライドタイトル表示<br>
　引数1：表示テキスト<br>
　引数2：アニメーション時間　※0.0f指定時はアニメーション無し<br>
　引数3：表示位置<br>
```
SetPresentationSlideTitle("スライドタイトル01", 0.2f, TextAnchor.MiddleLeft);
```

・スライド本文表示<br>
　引数1：表示テキスト<br>
　引数2：アニメーション時間　※0.0f指定時はアニメーション無し<br>
　引数3：表示位置<br>
```
SetPresentationSlideMainText(
    "あああああああああああああああ\nいいいいいいいいいいいいいいい\nううううううううううううううう\nえええええええええええええええ\nおおおおおおおおおおおおおおお", 
    1.0f,
    TextAnchor.MiddleLeft
);
```

・アニメーション描画待ち<br>
```
yield return new WaitUntil(() => WaitTweening());
```

・指定時間待ち<br>
　引数1：秒数<br>
```
yield return new WaitForSeconds(1.0f);
```

・キー入力待ち(Enter、↓、PgDn、マウス左クリック)<br>
```
yield return new WaitUntil(() => WaitKey());
```

# Author
高橋かずひと(https://twitter.com/KzhtTkhs)

# License 
Unity-Presentation-using-DOTween is under [MIT License](LICENSE).
