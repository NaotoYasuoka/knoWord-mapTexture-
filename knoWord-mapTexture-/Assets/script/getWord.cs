using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getWord : MonoBehaviour
{

    //InputFieldを格納するための変数
    InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        //GameObject obj = Resources.Load<GameObject>("sprite");
    }

    public void GetInputName()
    {
        Debug.Log("押されたよ");
        //InputFieldからテキスト情報を取得する
        string key_word = inputField.text;
        
        GameObject obj = (GameObject)Resources.Load("image");
        GameObject gameObj = Instantiate(obj, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
        Debug.Log("obj取得");

        WordToURL wtu = new WordToURL();
        StartCoroutine(wtu.mapTexture(gameObj, key_word));

        //wtu.mapTexture(obj, key_word);
        //GameObject obj = Resources.Load<GameObject>("sprite");
        //GameObject gameObj = Instantiate(obj);

        //入力フォームのテキストを空にする
        inputField.text = "";
    }

    public void push()
    {
        Debug.Log("押されたよ");
    }
}
