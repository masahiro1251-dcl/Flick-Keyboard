using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class FlickButton : MonoBehaviour
{
    [SerializeField] bool useFlick;

    KeyInput keyInput;
    [SerializeField] KeyInput.Buttons buttonName;
    [SerializeField] float flickThreshold = 0.01f;
    MyPressableButtonHoloLens2 pressableButton;

    Vector3 touchedPointAtFirst, touchedPointAtEnd;

    FlickResponseBoxController responseBoxController;

    // Start is called before the first frame update
    void Start()
    {
        pressableButton = GetComponent<MyPressableButtonHoloLens2>();
        keyInput = FindObjectOfType<KeyInput>().GetComponent<KeyInput>();
        responseBoxController = GetComponent<FlickResponseBoxController>();
    }

    public void pressed()
    {
        Debug.Log("Pressed!");
        if (useFlick) touchedPointAtFirst = pressableButton.recentTouchedPoint;
    }

    public void released()
    {
        Debug.Log("Pressed!");
        if (useFlick)
        {
            touchedPointAtEnd = pressableButton.recentTouchedPoint;
            //Vector2 v = new Vector2(touchedPointAtEnd.x - touchedPointAtFirst.x, touchedPointAtEnd.y - touchedPointAtFirst.y); //初期押し込み位置からの位置によるフリック判定
            Vector2 v = touchedPointAtEnd; //初期押し込み位置に関係なくフリック判定
            int direction = 0;

            if (Mathf.Abs(v.x) > Mathf.Abs(v.y))
            {
                if (v.x > flickThreshold) direction = 3;
                else if (v.x < -flickThreshold) direction = 1;
            }
            else
            {
                if (v.y > flickThreshold) direction = 2;
                else if (v.y < -flickThreshold) direction = 4;
            }
            keyInput.button(buttonName, direction);
            responseBoxController.createFlickResponseBox(direction);
        }
        else
        {
            keyInput.button(buttonName, 0);
            responseBoxController.createFlickResponseBox(0);
        }
    }
}
