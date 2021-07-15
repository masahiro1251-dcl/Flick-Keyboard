using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBackPlate : MonoBehaviour
{
    private void OnDisable()
    {
        this.gameObject.SetActive(false);
    }
}
