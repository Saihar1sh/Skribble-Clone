using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TrailSync : MonoBehaviour, IPunObservable
{
    private TrailRenderer trailRenderer;
    [SerializeField]
    private Slider brushSizeSlider;

    private float brushSize;

    private bool init = false;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(brushSize);
        }
        else
        {
            brushSize = (float)stream.ReceiveNext();
        }
    }


    void Update()
    {
    }
    public float BrushSlider(float _brushSize)
    {
        if (!init)
        {
            brushSize = _brushSize;
            init = true;
        }
        else if (brushSize != _brushSize)
            return brushSize;

        return _brushSize;
    }
}
