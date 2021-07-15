using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInput : MonoBehaviour
{
    public GameObject brushPrefab;

    [Range(0.01f, 0.2f)]
    public float brushSize = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(Ray, out hitInfo))
            {
                var gameObj = Instantiate(brushPrefab, hitInfo.point + Vector3.up * 0.1f, Quaternion.identity, transform);
                gameObj.transform.localScale = Vector3.one * brushSize;
                gameObj.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }
        }
    }
}
