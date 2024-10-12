using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMP_Test : MonoBehaviour
{
    TMP_Text textmesh;
    Mesh mesh;
    Vector3[] ver;
    public Gradient gradient;
    void Start()
    {
        textmesh= GetComponent<TMP_Text>();
        textmesh.text = "°¡·¢Èö°ËÆªÅä¼þ³Ø";
    }

    void Update()
    {
        textmesh.ForceMeshUpdate();
        mesh = textmesh.mesh;
        ver=mesh.vertices;
        Color[] color=mesh.colors; 
        for (int i = 0; i <textmesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textmesh.textInfo.characterInfo[i];
            int index = c.vertexIndex;
            Vector3 offset = Wobble(Time.time + i,5);
            ver[index] += offset;
            ver[index+1] += offset;
            ver[index+2] += offset;
            ver[index+3] += offset;
            Color curCol = gradient.Evaluate(Mathf.PingPong(Time.time+index*0.01f,1f));
            color[index] = curCol;
            color[index + 1] = curCol;
            color[index + 2] = curCol;
            color[index + 3] = curCol;
        }
        mesh.vertices= ver;
        mesh.colors = color;
        textmesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time,float strength)
    {
        return new Vector2(Mathf.Cos(time),Mathf.Sin(time))*strength;
    }
}
