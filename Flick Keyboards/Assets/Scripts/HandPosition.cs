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
    [SerializeField] float margin;
    [SerializeField] float dist;
    private Vector3 _estimatedAngularVelocity = Vector3.zero;
    private Vector3 shaftPosition;
    private Quaternion shaftRotation;
    private Quaternion pastRotation = Quaternion.identity;
    public Vector3 EstimatedAngularVelocity
    {
        get { return _estimatedAngularVelocity; }
    }

    // Start is called before 
    void Start()
    {
        Contents.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var handJointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
        if(HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out MixedRealityPose pose))
        {
            Contents.SetActive(true);
            Transform jointTransform = handJointService.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Left);
            shaftPosition = jointTransform.position + jointTransform.forward * (-dist);
            shaftRotation = jointTransform.rotation;
            // Contents.transform.position = shaftPosition + jointTransform.forward * (-dist);
            // Contents.transform.rotation = shaftRotation;
            Quaternion rot = Contents.transform.rotation;
            if(pastRotation != Quaternion.identity)
            {
                Quaternion deltaRotation = Quaternion.Inverse(pastRotation) * jointTransform.rotation;
                deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
                float angularSpeed = (angle * Mathf.Deg2Rad) / Time.deltaTime;
                _estimatedAngularVelocity = axis * angularSpeed;

                // 角速度がmarginより小さいなら、その方向の角度は固定

                Contents.transform.rotation = shaftRotation;

                // 没案（細かく補正をかけようとすると逆に処理が重くなる）
                // float dx = _estimatedAngularVelocity.x * Time.deltaTime;
                // float dy = _estimatedAngularVelocity.y * Time.deltaTime;
                // float dz = _estimatedAngularVelocity.z * Time.deltaTime;
                // if(dx >= margin)
                // {
                //     Vector3 xaxis = new Vector3(axis.x, 0.0f, 0.0f);
                //     Contents.transform.Rotate(xaxis, -angle);
                // }
                // if(dy >= margin)
                // {
                //     Vector3 yaxis = new Vector3(0.0f, axis.y, 0.0f);
                //     Contents.transform.Rotate(yaxis, -angle);
                // }
                // if(dz >= margin)
                // {
                //     Vector3 zaxis = new Vector3(0.0f, 0.0f, axis.z);
                //     Contents.transform.Rotate(zaxis, -angle);
                // }
                Debug.Log("old " + Contents.transform.rotation.ToString("F4"));
                // 今、最初の位置で完全に角度が固定されるような実装になっている
                Contents.transform.Rotate(axis, -angle);
                Debug.Log("new " + Contents.transform.rotation.ToString("F4"));
                Contents.transform.position = jointTransform.position;
            }
            else
            {
                Contents.transform.rotation = jointTransform.rotation;
                Contents.transform.position = jointTransform.position;
            }
            pastRotation = Contents.transform.rotation;
            // Debug.Log("Wrist " + jointTransform.rotation.ToString("F4") + " shaft " + Contents.transform.rotation.ToString("F4"));
        }
        else
        {
            Contents.SetActive(false);
            pastRotation = Quaternion.identity;
            // pastPosition = defaultPosition;
        }
    }
}