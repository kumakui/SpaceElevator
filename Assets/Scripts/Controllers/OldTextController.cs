using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldTextController : MonoBehaviour
{

//     private TextData _textData;
//     private EventText _eventText;
//     private int _sheetNumber = 0;
//
//     private Sentence _currentSentence;
//     private Phrase _currentPhrase;
//     private String _currentLetter;
//
//    //1イベンドテキスト内でどこまでセンテンスを表示したか．現在表示しているセンテンスの番号．
//     private int _currentSentenceCount = 0;
//     //1センテンス内でどこまでフレーズを表示したか．現在表示しているフレーズ数．
//     private int _currentPhraseCount = 0;
//     //1フレーズ内でどこまで文字を表示したか．現在表示しているフレーズ内の文字数．
//     private int _currentLetterCount = 0;
//
//
//     public string[] scenarios;
//     public Text text;
//
//     [SerializeField][Range(0.001f, 0.3f)]
//     float intervalForCharacterDisplay = 0.05f;	// 1文字の表示にかかる時間
//
//     private int _currentLine = 0;
//     private string _currentText = string.Empty;	// 現在の文字列
//     private float _timeUntilDisplay = 0;		// 表示にかかる時間
//     private float _timeElapsed = 1;			// 文字列の表示を開始した時間
//     private int _lastUpdateCharacter = -1;		// 表示中の文字数
//
//     // 文字の表示が完了しているかどうか
//     public bool IsCompleteDisplayText
//     {
//         get { return  Time.time > _timeElapsed + _timeUntilDisplay; }
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
//         _textData = (TextData) Resources.Load("Text");
//
//         scenarios = new string[] {"クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す",
//             "<color=orange>クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す2</color>"};
//
//         UpdateText();
//
//         Debug.Log(_textData.sheets[0].list[0].Text);
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         // 文字の表示が完了してるならクリック時に次の行を表示する
//         if( IsCompleteDisplayText ){
//             if(_currentLine < scenarios.Length && Input.GetMouseButtonDown(0)){
//                 //UpdateText();
//                 UpdateSentence();
//             }
//         }else{
//             // 完了してないなら文字をすべて表示する
//             if(Input.GetMouseButtonDown(0)){
//                 _timeUntilDisplay = 0;
//             }
//         }
//
//         // クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
//         int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - _timeElapsed) / _timeUntilDisplay) * _currentText.Length);
//
//         //1フレーズの表示が終わったら次のフレーズを表示する．
//         if (_currentPhrase.text.Length > _currentLetterCount)
//         {
//             UpdatePhrase();
//
//            // 表示文字数が前回の表示文字数と異なるならテキストを更新する
//            // if( displayCharacterCount != _lastUpdateCharacter ){
//            //     // text.text = _currentText.Substring(0, displayCharacterCount);
//            //     // _lastUpdateCharacter = displayCharacterCount;
//            //
//            //     _currentLetterCount = displayCharacterCount;
//            // }
//
//            if( displayCharacterCount != _currentLetterCount ){
//                text.text = _currentPhrase.text.Substring(0, displayCharacterCount);
//                _currentLetterCount = displayCharacterCount;
//
//                _currentLetterCount = displayCharacterCount;
//            }
//         }
//
//
//
//
//
//     }
//
//
// //start()とクリック時に呼ばれる
//     private void UpdateText()
//     {
//         // _currentText = scenarios[_currentLine];
//         _currentLine++;
//         _currentLetterCount++;
//
//
//
//         // 想定表示時間と現在の時刻をキャッシュ
//         _timeUntilDisplay = _currentSentence.Length * intervalForCharacterDisplay;
//         _timeElapsed = Time.time;
//
//         // 文字カウントを初期化
//         _lastUpdateCharacter = -1;
//     }
//
//     //次の行(ページ)を表示
//     private void UpdateSentence()
//     {
//         _currentSentence = _eventText.Sentences[_currentSentenceCount];
//         _currentSentenceCount++;
//
//         // 想定表示時間と現在の時刻をキャッシュ
//         _timeUntilDisplay = _currentText.Length * intervalForCharacterDisplay;
//         _timeElapsed = Time.time;
//
//         // 文字カウントを初期化
//         _currentLetterCount = -1;
//     }
//
//     private void UpdatePhrase()
//     {
//         _currentPhrase = _eventText.Sentences[_currentSentenceCount].phrases[_currentPhraseCount];
//         text.color = _currentPhrase.color;
//
//         _currentPhraseCount++;
//     }
}
