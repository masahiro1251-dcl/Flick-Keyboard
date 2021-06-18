using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyInput : MonoBehaviour
{
    public enum Buttons
    {
        A = 0,
        Ka = 1,
        Sa = 2,
        Ta = 3,
        Na = 4,
        Ha = 5,
        Ma = 6,
        Ya = 7,
        Ra = 8,
        Wa = 9,
        Symbol,
        Convert,
        Delete,
        Null,
        Alldelete
    }

    readonly string[,][] chars = new string[,][]{
        { new string[] {"あ", "ぁ"}, new string[] {"い", "ぃ"}, new string[]{"う", "ぅ"},new string[] {"え", "ぇ"}, new string[] {"お", "ぉ"}},
        { new string[] {"か", "が"}, new string[] {"き", "ぎ"}, new string[]{"く", "ぐ"},new string[] {"け", "げ"}, new string[] {"こ", "ご"}},
        { new string[] {"さ", "ざ"}, new string[] {"し", "じ"}, new string[]{"す", "ず"},new string[] {"せ", "ぜ"}, new string[] {"そ", "ぞ"}},
        { new string[] {"た", "だ"}, new string[] {"ち", "ぢ"}, new string[]{"つ", "っ", "づ"},new string[] {"て", "で"},new string[] {"と", "ど"}},
        { new string[] {"な"}, new string[] {"に"}, new string[]{"ぬ"},new string[] {"ね"}, new string[] {"の"}},
        { new string[] {"は", "ば", "ぱ"}, new string[] {"ひ", "び", "ぴ"}, new string[]{"ふ", "ぶ", "ぷ"},new string[] {"へ", "べ", "ぺ"}, new string[] {"ほ", "ぼ", "ぽ"}},
        { new string[] {"ま"}, new string[] {"み"}, new string[]{"む"},new string[] {"め"}, new string[] {"も"}},
        { new string[] {"や", "ゃ"}, new string[] {"（", "「", "＜"}, new string[]{"ゆ", "ゅ"}, new string[] {"）", "」", "＞"} ,new string[] {"よ", "ょ"}},
        { new string[] {"ら"}, new string[] {"り"}, new string[]{"る"},new string[] {"れ"}, new string[] {"ろ"}},
        { new string[] {"わ", "ゎ"}, new string[] {"を"}, new string[]{"ん"},new string[] {"ー", "～", "-"}, new string[] {"わ", "ゎ"}},
        { new string[] {"、", "・", ","}, new string[] {"。", "…", "."}, new string[]{"？", "?"},new string[] {"！", "!"}, new string[] {"、", "・", ","}}
    };

    private TextMeshProUGUI textMesh;
    private Stack<int[]> inputChars;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        inputChars = new Stack<int[]>();
        /*
        button(Buttons.Ha, 0);
        button(Buttons.Ra, 4);
        button(Buttons.Wa, 3);
        button(Buttons.Wa, 0);
        button(Buttons.Wa, 3);
        button(Buttons.Ra, 2);
        button(Buttons.Ta, 4);
        button(Buttons.Convert, 0);
        Debug.Log("Text is \"" + textMesh.text + "\"");
        button(Buttons.Symbol, 3);
        Debug.Log("Text is \"" + textMesh.text + "\"");
        button(Buttons.Delete, 3);
        Debug.Log("Text is \"" + textMesh.text + "\"");
        */
    }
    public void button(Buttons button, int direction)
    {
        switch (button)
        {
            case Buttons.A:
            case Buttons.Ka:
            case Buttons.Sa:
            case Buttons.Ta:
            case Buttons.Na:
            case Buttons.Ha:
            case Buttons.Ma:
            case Buttons.Ya:
            case Buttons.Ra:
            case Buttons.Wa:
            case Buttons.Symbol:
                input((int)button, direction);
                break;
            case Buttons.Convert:
                convert();
                break;
            case Buttons.Delete:
                delete();
                break;
            case Buttons.Null:
                break;
            case Buttons.Alldelete:
                alldelete();
                break;
            default:
                Debug.LogWarning("unknown key has been input.");
                break;
        }
        refreshText();
    }

    //文字を入力する
    private void input(int consonant, int vowel)
    {
        inputChars.Push(new int[] { consonant, vowel, 0 });
    }

    //一文字消す
    private void delete()
    {
        inputChars.Pop();
    }

    //大文字を小文字に変更したり、濁点・半濁点をつけたりする
    private void convert()
    {
        int[] recentInputChar = inputChars.Pop();   //直接変更してもいいが、分かりやすく。
        recentInputChar[2] = (recentInputChar[2] + 1) % chars[recentInputChar[0], recentInputChar[1]].Length;
        inputChars.Push(recentInputChar);
    }

    //TextMeshのテキストを変更する。
    //データの一貫性を重視するなら、毎回全部の文字を生成しなおすべき。ただ処理が重くなるので、編集箇所のみをいじる実装にする予定。
    //今回は短文しか入力しないだろうということと、カーソル移動を追加実装するときに楽になるので、上記の手法で実装。
    private void refreshText()
    {
        textMesh.text = "";
        foreach (int[] inputChar in inputChars)
        {
            textMesh.text = chars[inputChar[0], inputChar[1]][inputChar[2]] + textMesh.text;
        }
    }

    private void alldelete()
    {
        textMesh.text = "";
        inputChars = new Stack<int[]> {};
    }

}
