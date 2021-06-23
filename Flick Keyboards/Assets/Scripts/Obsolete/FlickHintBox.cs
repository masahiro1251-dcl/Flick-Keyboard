using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickHintBox : MonoBehaviour
{
    Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        //renderer.material.color = new Color32(144, 238, 144, 126);
    }

    public void onTouchStart()
    {
        renderer.material.color = new Color32(220, 20, 60, 126);
    }

    public void onTouchCompleted()
    {
        renderer.material.color = new Color32(144, 238, 144, 126);
    }
}
