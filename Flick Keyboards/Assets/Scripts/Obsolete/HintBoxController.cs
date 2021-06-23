using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBoxController : MonoBehaviour
{
    [SerializeField] GameObject hintBoxesPrefab;
    GameObject hintBoxes;
    public void createHintBoxes()
    {
        hintBoxes = Instantiate(hintBoxesPrefab, this.transform.position, Quaternion.identity);
        hintBoxes.transform.parent = this.transform;
    }

    public void destroyHintBoxes()
    {
        Destroy(hintBoxes);
    }
}
