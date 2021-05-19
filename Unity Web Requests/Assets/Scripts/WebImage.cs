using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


class WebImage : MonoBehaviour
{
    private GameObject _viewPanel;
    private Texture _viewImageTexture;
    private ImageLoader _imageLoader;
    private float _aspectRatio;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowViewPanel);
        _viewImageTexture = GetComponent<RawImage>().texture;
    }


    public void Construct(GameObject viewPanel , ImageLoader imageLoader , float aspectRatio)
    {
        _viewPanel = viewPanel;
        _imageLoader = imageLoader;
        _aspectRatio = aspectRatio;
    }


    public void ShowViewPanel()
    {
        _viewPanel.GetComponentInChildren<RawImage>().texture = _viewImageTexture;
        _viewPanel.SetActive(true);
        _viewPanel.transform.GetChild(0).GetComponent<AspectRatioFitter>().aspectRatio = _aspectRatio;

        _imageLoader.LoadingIsStopped = true;
    }
}