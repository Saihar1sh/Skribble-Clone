﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawService : MonoBehaviour
{
    [Range(0.01f, 0.2f)]
    public float brushSize = 0.1f;

    [SerializeField]
    private GameObject brushPrefab;

    [SerializeField]
    private Slider brushSizeSlider;

    [SerializeField]
    private Button clearButton;

    private TrailRenderer trailRenderer;

    private GameObject trail;
    private Plane planeObj;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        planeObj = new Plane(Camera.main.transform.forward * -1, transform.position);
        trailRenderer = brushPrefab.GetComponent<TrailRenderer>();
        Color _color = Color.black;
        TrailColorChangeTo(_color);
        clearButton.onClick.AddListener(ClearBoard);
    }

    // Update is called once per frame
    void Update()
    {
        DrawInputs();
        BrushSizeChange();
    }

    private void BrushSizeChange()
    {
        trailRenderer.widthMultiplier = brushSizeSlider.value;
    }

    private void ClearBoard()
    {
        while (transform.childCount != 0)
        {
            Destroy(transform.GetChild(0));
        }
    }

    private void DrawInputs()
    {
        var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitpoint;
        if (planeObj.Raycast(Ray, out hitpoint))
        {
            /*            Vector3 temp = Input.mousePosition;
                        temp.z = 10f;
                        transform.position = Camera.main.ScreenToWorldPoint(temp);

            */
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                trail = Instantiate(brushPrefab, transform);
                startPos = Ray.GetPoint(hitpoint);
                trail.transform.position = startPos;

            }

            else if (Input.GetMouseButton(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 pos = Ray.GetPoint(hitpoint);
                pos.y += 1f;
                trail.transform.position = pos;
            }

        }
    }

    public void TrailColorChangeTo(Color color)
    {
        trailRenderer.colorGradient.mode = GradientMode.Fixed;
        trailRenderer.startColor = trailRenderer.endColor = color;
    }
}
