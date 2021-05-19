using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _gameOverPanel;


    public void ShowGameOverPanel()
    {
        _mainPanel.SetActive(false);
        _gameOverPanel.SetActive(true);
    }
}
