using TMPro;
using UnityEngine;


namespace UI
{
    class Record : MonoBehaviour
    {
        private readonly string text = "Record: ";
        private TextMeshProUGUI _textMeshPro;


        private void Start()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            _textMeshPro.text = text + PlayerPrefs.GetInt("Record" , 0);
        }
    }
}
