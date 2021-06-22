using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;


public class HandPosition : MonoBehaviour
{
    // Start is called before 
    public GameObject logger;
    public GameObject keyboard;
    private string pos;
    void Start()
    {
        pos = "";
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.transform.position.ToString();
        logger.GetComponent<TextMesh>().text = pos;
    }
}
