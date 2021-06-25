using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;


public class HandPosition : MonoBehaviour
{
    [SerializeField] GameObject flickbutton_A;
    [SerializeField] GameObject flickbutton_K;
    [SerializeField] GameObject flickbutton_S;
    [SerializeField] GameObject flickbutton_T;
    [SerializeField] GameObject flickbutton_N;
    [SerializeField] GameObject flickbutton_H;
    
    // Start is called before 
    void Start()
    {
        flickbutton_A.SetActive(false);
        flickbutton_K.SetActive(false);
        flickbutton_S.SetActive(false);
        flickbutton_T.SetActive(false);
        flickbutton_N.SetActive(false);
        flickbutton_H.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out MixedRealityPose pose))
        {
            Debug.Log(pose.Position);
            flickbutton_A.SetActive(true);
            flickbutton_K.SetActive(true);
            flickbutton_S.SetActive(true);
            flickbutton_T.SetActive(true);
            flickbutton_N.SetActive(true);
            flickbutton_H.SetActive(true);

        }
        else
        {
            Debug.Log("Lost");
            flickbutton_A.SetActive(false);
            flickbutton_K.SetActive(false);
            flickbutton_S.SetActive(false);
            flickbutton_T.SetActive(false);
            flickbutton_N.SetActive(false);
            flickbutton_H.SetActive(false);
        }
    }
}
