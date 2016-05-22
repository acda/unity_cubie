using UnityEngine;
using System.Collections;

/**
 * This Component observes a CHitable and moves 
 * the texture-Y-offset of "_LowerTex" on the 
 * renderer on the same object.
 */
public class texMove : MonoBehaviour
{

    Material m_mat;
    Renderer m_rend;
    CHitable m_hitt;

    public float TexOff_full = 1.0f;
    public float TexOff_empty = 0.0f;

    void Awake()
    {
        m_rend = GetComponent<Renderer>();
        m_hitt = GetComponent<CHitable>();
        if (m_rend != null)
        {
//            Debug.Log("have m_rend");
            m_mat = m_rend.material;
            if (m_mat != null)
            {
//                Debug.Log("have m_mat");
            }
        }
        if (m_hitt != null)
        {
            m_hitt.a += callback_from_hitable;
        }
    }

    void Start()
    {
        callback_from_hitable(null, 1.0f, 1.0f);    // call self to set to full.
    }

    // callback from CHitable
    void callback_from_hitable(GameObject thatone, float newHP,float maxHP)
    {
        float x = newHP / maxHP;
        x = TexOff_empty + x * (TexOff_full-TexOff_empty);
        if(m_mat!=null)
            m_mat.SetTextureOffset("_LowerTex", new Vector2(0.0f, x));
    }

/*	
	// Update is called once per frame
	void Update ()
    {
        float x;
        x = Time.fixedTime;
        x -= Mathf.Floor(x);
        m_mat.SetTextureOffset("_LowerTex", new Vector2(0.0f, x));
    }
*/
}
