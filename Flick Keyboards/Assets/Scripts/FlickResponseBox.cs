using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickResponseBox : MonoBehaviour
{
    MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        StartCoroutine("fadeout", (byte)(mesh.material.color.a * 255));
    }
    IEnumerator fadeout(byte alpha)
    {
        for (byte i = 0; i < alpha; i += 5)
        {
            mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 1);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
