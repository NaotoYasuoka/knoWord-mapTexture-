using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using HtmlAgilityPack;

public class WordToURL : MonoBehaviour
{
    //public delegate IEnumerator Callback(string HTML);

    public IEnumerator getHTML(string key_word)
    {
        // リクエストのURL文を作成
        string URL = "https://www.google.co.jp/search?q=" + key_word + "&tbm=isch&ved=2ahUKEwibvOTL3ufsAhWmzIsBHRqnBdMQ2-cCegQIABAA&oq=" + key_word + "&gs_lcp=CgNpbWcQAzIECAAQHjIECAAQHjIECAAQHjIECAAQHjIECAAQHjIECAAQHjIECAAQHjIECAAQHjIECAAQHjIECAAQHjoECCMQJzoFCAAQsQM6AggAOgQIABAEOgcIIxDqAhAnOgcIABCxAxAEOggIABCxAxCDAToECAAQEzoGCAAQHhATUICQGFiJzRhgz88YaANwAHgAgAHLAYgB4Q6SAQYwLjEzLjGYAQCgAQGqAQtnd3Mtd2l6LWltZ7ABCsABAQ&sclient=img&ei=_gOiX5vFOqaZr7wPms6WmA0&bih=977&biw=1858&hl=ja";
        // リクエストを作成
        var www = UnityWebRequest.Get(URL);
        // リクエストを出して返答を待機
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            // エラーが起きた場合はエラー内容を表示
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            //  または、結果をバイナリデータとして取得します
            byte[] results = www.downloadHandler.data;
            string HTML = www.downloadHandler.text;
            Debug.Log(HTML);
            //getImageURL(HTML);
        }
    }


    public string getImageURL(string key_word)
    {
        string URL = "https://www.google.co.jp/search?safe=active&hl=ja&authuser=0&tbm=isch&sxsrf=ALeKk00Afoqg2HWrkekZJQDhKRDU3ymrnA%3A1605185150367&source=hp&biw=958&bih=927&ei=fi6tX56KFIvWmAWUkYD4BQ&q="+key_word+"&oq=&gs_lcp=CgNpbWcQAxgAMgcIIxDqAhAnMgcIIxDqAhAnMgcIIxDqAhAnMgcIIxDqAhAnMgcIIxDqAhAnMgcIIxDqAhAnMgcIIxDqAhAnMgcIIxDqAhAnMgcIIxDqAhAnMgcIIxDqAhAnUABYAGD7IGgBcAB4AIABAIgBAJIBAJgBAKoBC2d3cy13aXotaW1nsAEK&sclient=img";

        System.Net.WebClient web = new System.Net.WebClient();
        string html = web.DownloadString(URL);
        Debug.Log(html);

        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);


        HtmlNodeCollection nodes =
            doc.DocumentNode
            .SelectNodes("html//body//div[not(@class)]//table[1]//tr[1]//td[1]//div[1]//div[1]//div[1]//div[1]//table[1]//tr[1]//td[1]//a[1]//div[1]//img");
            //.SelectNodes("html//body//div[2]//table[1]//tr[1]//td[1]//div[1]//div[1]//div[1]//table[1]//tr[1]//td[1]//a[1]//div[1]//img");
        string url = nodes[0].GetAttributeValue("src", "");
        Debug.Log(url);
        return url;
    }



    public IEnumerator mapTexture(GameObject gameObj, string word)
    {

        //imageはスプライトを使って描画しているので
        Sprite sprite;
        //画像リンクから画像をテクスチャにする
        Texture2D texture;


        string URL = getImageURL(word);
        //URL = "https://www.facebook.com/images/fb_icon_325x325.png";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);
        //画像を取得できるまで待つ
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("えらーでたよ");
            Debug.Log(www.error);
        }
        else
        {
            //取得した画像のテクスチャをRawImageのテクスチャに張り付ける
            texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            //textureからspriteに変換
            sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.zero);
            //Imageにspriteを張り付ける
            gameObj.GetComponent<Image>().sprite = sprite;
            Debug.Log("画像の貼り付け完了");
        }

    }
}