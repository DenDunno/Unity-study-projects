using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    class Score : MonoBehaviour
    {
        private readonly string text = "Score: ";
        private TextMeshProUGUI _textMeshPro;
        private SpaceShipAcceleration _shipAcceleration;


        [Inject]
        private void Construct(SpaceShipAcceleration spaceShipAcceleration)
        {
            _shipAcceleration = spaceShipAcceleration;
        }


        private void Start()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }


        private void Update()
        {
            if (_shipAcceleration != null)
                _textMeshPro.text = text + (int)_shipAcceleration.DistancePassed;
        }
    }
}
