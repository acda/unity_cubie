using UnityEngine;
using System.Collections;

public class track_obj_FPS_cam : MonoBehaviour
{

    Transform m_from;
    Transform m_to;

	// Use this for initialization
	void Start ()
    {
        Camera cam;
        m_to = gameObject.transform;
        cam = (Camera)m_to.parent.gameObject.GetComponentInChildren<Camera>();
        if (cam != null)
        {
            m_from = cam.gameObject.transform;
        }else{
            m_from = null;
            Debug.Log("Could not find cam to track!");
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (m_from != null)
            m_to.localRotation = m_from.localRotation;
	}
}
