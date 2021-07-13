using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;


public class HandPosition : MonoBehaviour
{
    [SerializeField] GameObject Contents;

    // Start is called before 
    void Start()
    {
        Contents.SetActive(false);
        /*
        flickbutton_A.SetActive(false);
        flickbutton_K.SetActive(false);
        flickbutton_S.SetActive(false);
        flickbutton_T.SetActive(false);
        flickbutton_N.SetActive(false);
        flickbutton_H.SetActive(false);
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out MixedRealityPose pose))
        {
            Contents.SetActive(true);
            /*
            flickbutton_A.SetActive(true);
            flickbutton_K.SetActive(true);
            flickbutton_S.SetActive(true);
            flickbutton_T.SetActive(true);
            flickbutton_N.SetActive(true);
            flickbutton_H.SetActive(true);
            if(flickbutton_A.activeSelf) Debug.Log("arive");
            */
        }
        else
        {
            Contents.SetActive(false);
            /*
            Debug.Log("Lost");
            flickbutton_A.SetActive(false);
            flickbutton_K.SetActive(false);
            flickbutton_S.SetActive(false);
            flickbutton_T.SetActive(false);
            flickbutton_N.SetActive(false);
            flickbutton_H.SetActive(false);
            */
        }
    }
}