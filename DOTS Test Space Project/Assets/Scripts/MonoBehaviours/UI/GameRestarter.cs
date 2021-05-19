using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameRestarter : MonoBehaviour
{
    private EntityManager _entityManager;

    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            var textMesh = _entityManager.CreateEntityQuery(typeof(RestartTextComponent)).ToEntityArray(Allocator.Persistent);
            _entityManager.DestroyEntity(textMesh[0]);
            textMesh.Dispose();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
