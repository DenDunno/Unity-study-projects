using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestartText : MonoBehaviour
{
    private TMP_Text _textMeshPro;
    private TMP_TextInfo _textInfo;


    private void Start()
    {
        _textMeshPro = GetComponent<TMP_Text>();
        _textInfo = _textMeshPro.textInfo;
    }


    private void Update()
    {
        for (int i = 0; i < _textInfo.characterCount; ++i)
        {
            var charInfo = _textInfo.characterInfo[i];
            var verts = _textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            if (charInfo.isVisible == false)
            {
                continue;
            }

            for (int j = 0; j < 4; ++j)
            {
                var orig = verts[charInfo.vertexIndex + j];
                float offset = Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 0.1f;

                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, offset, 0);
            }
        }

        for(int i = 0; i < _textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = _textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            _textMeshPro.UpdateGeometry(meshInfo.mesh, i);
        }

    }
}
