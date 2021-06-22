using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;


public class HandPosition : MonoBehaviour
{
    // Start is called before 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose pose))
        {
            Debug.Log(pose.Position);
        }
        else
        {
            Debug.Log("Lost");
        }
    }
}
