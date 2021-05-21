using UnityEngine;
using Unity.Transforms;
using Unity.Entities;
using TMPro;

public class RestartTextSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((RestartTextComponent textData) =>
        {
            var textInfo = textData.TextMesh.textInfo;

            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                var charInfo = textInfo.characterInfo[i];
                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                if (charInfo.isVisible == false)
                {
                    continue;
                }

                for (int j = 0; j < 4; ++j)
                {
                    var orig = verts[charInfo.vertexIndex + j];
                    var offset = Mathf.Sin((float)Time.ElapsedTime * 2f + orig.x * 0.01f) * 0.1f;

                    verts[charInfo.vertexIndex + j] = orig + new Vector3(0, offset, 0);
                }
            }


            for (int i = 0; i < textInfo.meshInfo.Length; ++i)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textData.TextMesh.UpdateGeometry(meshInfo.mesh, i);
            }
        });
    }
}
