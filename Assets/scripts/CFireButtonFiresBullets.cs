using UnityEngine;
using System.Collections;

public class CFireButtonFiresBullets : MonoBehaviour
{

    public string Button = "Fire1";
    public Vector3 startOffset;
    public Vector3 direction = new Vector3(0.0f,0.0f,1.0f);
    public float speed = 10.0f;
	public float autoFireDelay = 0.0f;
    public GameObject bullet_prefab;
	private float m_held;

    // Use this for initialization
    void Start ()
    {
        direction.Normalize();
		m_held = 0.0f;
		m_pressed = false;
	}

	// Update is called once per frame
	void Update ()
    {
		if (Input.GetAxis(Button) > 0.5f)
		{
			if (!m_pressed)
			{
				// press.
				onButton();
				m_held = 0.0f;
				m_pressed = true;
			}else{
				// held
				m_held += Time.deltaTime;
				if (autoFireDelay > 0.0f && m_held >= autoFireDelay)
				{
					onButton();
					m_held -= autoFireDelay;
				}
			}
		}
		else{
			m_pressed = false;
		}
    }

    private void onButton()
    {
        Vector3 p, d;
        GameObject bul;
        Transform xf = GetComponent<Transform>();
        d = xf.TransformDirection(direction);
        p = xf.TransformPoint(startOffset);
        bul = (GameObject)Instantiate(bullet_prefab, p, transform.rotation);
        Rigidbody rgb = bul.GetComponent<Rigidbody>();
        if (rgb != null)
            rgb.velocity = d * speed;
    }

    private bool m_pressed;
}
