using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DrawService : MonoBehaviour
{
    private int layerNum = 0;

    private float trailSize;

    private TrailSync trailSync;

    [SerializeField]
    private GameObject brushPrefab;

    [SerializeField]
    private Slider brushSizeSlider;

    [SerializeField]
    private Button clearButton;

    private TrailRenderer trailRenderer;

    private PhotonView photonView;

    private GameObject trail;
    private Plane planeObj;

    private bool tillTrailCreation = true;

    List<GameObject> drawings;


    // Start is called before the first frame update
    void Start()
    {
        planeObj = new Plane(Camera.main.transform.forward * -1, transform.position);
        trailRenderer = brushPrefab.GetComponent<TrailRenderer>();
        trailSync = brushPrefab.GetComponent<TrailSync>();
        Color _color = Color.black;
        TrailColorChangeTo(_color);
        drawings = new List<GameObject>();
        clearButton.onClick.AddListener(ClearBoard);
        trailSize = brushSizeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        DrawInputs();
        BrushSizeChange();

    }

    private void BrushSizeChange()
    {
        trailRenderer.widthMultiplier = trailSync.BrushSlider(trailSize);

    }

    private void ClearBoard()
    {
        foreach (GameObject item in drawings)
        {
            PhotonNetwork.Destroy(item.gameObject);
        }
    }

    private void DrawInputs()
    {
        var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(Ray, out hit))
        {
            trailRenderer.sortingOrder = layerNum;

            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 startPos = hit.point;
                trail = PhotonNetwork.Instantiate("Brush", startPos, Quaternion.identity);
                photonView = trail.gameObject.GetComponent<PhotonView>();
                tillTrailCreation = false;
                drawings.Add(trail.gameObject);
                layerNum++;
            }

            else if (Input.GetMouseButton(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 pos = hit.point;
                pos.y += 1f;
                ChangeTrailPos(pos);
            }
        }

    }
    private void ChangeTrailPos(Vector3 pos)
    {
        if (photonView.IsMine)
            trail.transform.position = pos;
    }
    public void TrailColorChangeTo(Color color)
    {
        if (tillTrailCreation || photonView.IsMine)
        {
            trailRenderer.colorGradient.mode = GradientMode.Fixed;
            trailRenderer.startColor = trailRenderer.endColor = color;

        }
    }
}
