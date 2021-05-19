using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


class ImageLoader : MonoBehaviour
{
    [SerializeField] private Transform _gridContent;
    [SerializeField] private GameObject _viewPanel;
    private WebImage _gameImage;

    private List<WebImageData> _webImages;
    private readonly string _apiURL = "https://picsum.photos/v2/list?page=2&limit=100";

    private bool _wasClicked = false;
    public bool LoadingIsStopped = false;

    private readonly int _photosLimit = 20;
    private int _currentImageIndex = 0;


    private void Start()
    {
        _gameImage = Resources.Load<WebImage>("Prefabs/Image");
    }


    public async void LoadImages()
    {
        if (_wasClicked == false)
        {
            _wasClicked = true;

            await ReadWebImages(_apiURL);
            await LoadAllImages();
        }
    }


    private async UniTask ReadWebImages(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            await webRequest.SendWebRequest();

            _webImages = JsonConvert.DeserializeObject<List<WebImageData>>(webRequest.downloadHandler.text);
        }
    }


    private async UniTask LoadAllImages()
    {
        while(_currentImageIndex < _photosLimit)
        {
            if (LoadingIsStopped == false)
            {
                using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(_webImages[_currentImageIndex].DownloadUrl))
                {
                    await webRequest.SendWebRequest();

                    var gameImage = Instantiate(_gameImage, _gridContent);
                    var textureResult = DownloadHandlerTexture.GetContent(webRequest);

                    gameImage.GetComponent<RawImage>().texture = textureResult;

                    float aspectRatio = (float)textureResult.width / (float)textureResult.height;
                    gameImage.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;

                    gameImage.Construct(_viewPanel , this , aspectRatio);
                }
            }

            else
            {
                return;
            }

            ++_currentImageIndex;
        }
    }


    public async void ToggleLoading()
    {
        LoadingIsStopped = !LoadingIsStopped;

        if (LoadingIsStopped == false && _webImages != null)
        {
            await LoadAllImages();
        }
    }


    public void CleanImages()
    {
        List<Transform> imagesToDelete = _gridContent.GetComponentsInChildren<Transform>().ToList();
        imagesToDelete.Remove(transform);
        imagesToDelete.Remove(_gridContent);

        imagesToDelete.ForEach(image => Destroy(image.gameObject));

        _currentImageIndex = 0;
        _wasClicked = false;
        LoadingIsStopped = false;
    }
}