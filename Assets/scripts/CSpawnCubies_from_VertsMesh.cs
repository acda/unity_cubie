using UnityEngine;
using System.Collections;

public class CSpawnCubies_from_VertsMesh : MonoBehaviour
{

    Mesh m_vertMesh;
    public GameObject cubie_prototype;

    void Awake()
    {
        MeshFilter fil;
        fil = GetComponent<MeshFilter>();
        m_vertMesh = fil.mesh;
    }

    void Start ()
    {
        Vector3[] pts;
        ulong[] hts;
        uint num, i, j,numhts;

        pts = m_vertMesh.vertices;
        num = (uint)pts.Length;
        hts = new ulong[num];
        numhts = 0;
        for (i = 0; i < num; i++)
        {
            Vector3 pos = pts[i];
            pos = transform.TransformPoint(pos);
            ulong u = ul_from_v3(pos);
            for (j = 0; j < numhts; j++)
            {
                if (hts[j] == u)
                    break;
            }
            if (j >= numhts)
            {
                hts[numhts++] = u;
                GameObject cube;
                cube = (GameObject)Instantiate(cubie_prototype, pos, transform.rotation);
            }
        }

        Debug.Log("spawned " + numhts.ToString() + "cubies.");
        Debug.Log("num = " + num.ToString());
        //        Destroy(gameObject);
    }

    private ulong ul_from_v3(Vector3 v)
    {
        ulong a, b, c;
        a = (ulong)(v.x * 1024.0f + 4.0f * 131072.0 + 0.5f);
        b = (ulong)(v.y * 1024.0f + 4.0f * 131072.0 + 0.5f);
        c = (ulong)(v.z * 1024.0f + 4.0f * 131072.0 + 0.5f);
        return (a & 0xFFFFF)+((b & 0xFFFFF)<<20) + ((c & 0xFFFFF) << 40);
    }


}
