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
    [SerializeField] float ydist;   // Contentsから離す距離
    [SerializeField] float buttonangle; // ボタン自身の角度
    [SerializeField] float margin;  // 今回は0.02f
    private float dist = 0.18f;
    private float buttontheta;
    private float calc_distance_y, calc_distance_z;
    private float calc_margin_y, calc_margin_z;
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
        buttontheta = Mathf.PI * (90.0f - buttonangle) / 180.0f;
        calc_distance_y = -(ydist + dist * Mathf.Sin(buttontheta));
        calc_distance_z = -(dist * Mathf.Cos(buttontheta));
        calc_margin_y = margin * Mathf.Sin(buttontheta);
        calc_margin_z = margin * Mathf.Cos(buttontheta);
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
            // Debug.Log("Contents " + Contents.transform.rotation + " Specials " + Specials.transform.position);
            ChildrenPosition();
        }
        else
        {
            Contents.SetActive(false);
            Specials.SetActive(false);
            pastRotation = Quaternion.identity;
        }
    }

    private void ChildrenPosition()
    {
        Transform children = Contents.GetComponentInChildren<Transform>();
        int cnt = 0;
        foreach(Transform obj in children)
        {
            if(obj.name[0] == 'F')
            {
                obj.transform.localRotation = Quaternion.Euler(-buttonangle, 0, -90);
                // 今の状態だとその場で回転しているだけなのでローカル座標もうまくいじる
                if(cnt == 0 || cnt == 6)
                {
                    obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, calc_distance_y, calc_distance_z);
                }
                else
                {
                    obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, calc_distance_y + calc_margin_y*(cnt%6), calc_distance_z + calc_margin_z*(cnt%6));
                }
            }
            cnt++;
        }
    }
}
