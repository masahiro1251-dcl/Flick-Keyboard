using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;


public class HandPosition : MonoBehaviour
{
    [SerializeField] GameObject Contents;
    [SerializeField] GameObject Specials;
    private Vector3 _estimatedAngularVelocity = Vector3.zero;
    private Quaternion pastRotation = Quaternion.identity;
    public Vector3 EstimatedAngularVelocity
    {
        get { return _estimatedAngularVelocity; }
    }

    // Start is called before 
    void Start()
    {
        Contents.SetActive(false);
        Specials.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var handJointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
        if(HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out MixedRealityPose lpose))
        {
            Contents.SetActive(true);
            Specials.SetActive(true);
            Transform jointTransform = handJointService.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Left);
            Quaternion rot = Contents.transform.rotation;
            Transform palmTransform = handJointService.RequestJointTransform(TrackedHandJoint.Palm, Handedness.Left);
            Specials.transform.rotation = palmTransform.rotation;
            Specials.transform.position = palmTransform.position;

            if(pastRotation != Quaternion.identity)
            {
                // 角速度計算、axisが回転軸、angleが回転量
                Quaternion deltaRotation = Quaternion.Inverse(pastRotation) * jointTransform.rotation;
                deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
                float angularSpeed = (angle * Mathf.Deg2Rad) / Time.deltaTime;
                _estimatedAngularVelocity = axis * angularSpeed;

                // 現在のWristに合わせて回転
                Contents.transform.rotation = jointTransform.rotation;

                // 入力するタイミングだけ固定されればいい、右手の人差し指をトリガーにする
                if(HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose rpose))
                {
                    // Wristの回転分逆回転で戻す
                    Contents.transform.Rotate(axis, -angle);
                    Contents.transform.position = jointTransform.position;
                }
                else
                {
                    // 逆回転せず、位置だけ更新
                    Contents.transform.position = jointTransform.position;
                }
            }
            else
            {
                // Wristに追従する形
                Contents.transform.rotation = jointTransform.rotation;
                Contents.transform.position = jointTransform.position;
            }
            pastRotation = Contents.transform.rotation;
            Debug.Log("Contents " + Contents.transform.position + " Specials " + Specials.transform.position);
        }
        else
        {
            Contents.SetActive(false);
            Specials.SetActive(false);
            pastRotation = Quaternion.identity;
        }
    }
}
