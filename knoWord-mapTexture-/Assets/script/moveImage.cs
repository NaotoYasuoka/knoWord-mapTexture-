using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class moveImage : MonoBehaviour
{

    public void moveImage(Transform transform, float X, float Y)
    {

        //float X = 600;
        //float Y = 750;

        float center_X = X - 1440/2;
        float center_Y = 810/2 - Y;


        float relay_x = Random.Range(X-100 -1440/2, X+100 - 1440/2);

        //Debug.Log(coordinate_y);

        Vector3[] path = {
            new Vector3(relay_x, 810/2 -200, 0),
            new Vector3(center_X, center_Y, 0),
        };

        transform.DOLocalPath(path, 5f, PathType.CatmullRom).SetEase(Ease.Linear);
    }
}
