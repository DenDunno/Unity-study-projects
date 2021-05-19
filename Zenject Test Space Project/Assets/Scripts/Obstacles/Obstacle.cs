using System.Collections;
using UnityEngine;


class Obstacle : MonoBehaviour
{
    private readonly float _rotationSpeed = 40f;


    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0, _rotationSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SpaceShipAcceleration>(out var spaceShip) == true)
        {
            int record = PlayerPrefs.GetInt("Record");

            if (record < spaceShip.DistancePassed)
            {
                PlayerPrefs.SetInt("Record", (int)spaceShip.DistancePassed);
            }

            spaceShip.Stop();

            Instantiate(Resources.Load<ParticleSystem>("Prefabs/Explosion"), spaceShip.transform.position, Quaternion.identity);

            Destroy(spaceShip.gameObject);

            StartCoroutine(FinishGame());
        }
    }


    private IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<UIManager>().ShowGameOverPanel();
    }
}