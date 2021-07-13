using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickResponseBoxController : MonoBehaviour
{
    [SerializeField] GameObject flickResponseBoxPrefab;
    [Tooltip("フリックした際、ResponseBoxを中心位置からどれだけずらした位置に表示するか")]
    [SerializeField] float offset;
    public void createFlickResponseBox(int direction)
    {
        float x = 0, y = 0;
        switch (direction)
        {
            case 0: x = 0; y = 0; break;
            case 1: x = -offset; y = 0; break;
            case 2: x = 0; y = offset; break;
            case 3: x = offset; y = 0; break;
            case 4: x = 0; y = -offset; break;
            default: break;
        }
        GameObject responseBox = Instantiate(flickResponseBoxPrefab, this.transform);
        responseBox.transform.position += new Vector3(x, y, 0);
    }

}
