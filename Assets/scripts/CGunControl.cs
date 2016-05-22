using UnityEngine;
using System.Collections;

public class CGunControl : MonoBehaviour
{
	GameObject m_base;
	GameObject m_bar;
	GameObject[] m_barrels;

	int shootNext;

	public float angle_speed = 30.0f;
	public float angle_limit_up = 45.0f;
	public float angle_limit_down = 15.0f;
	public float angle_limit_side = 45.0f;
	public float auto_fire_delay = 0.2f;

	float angle_x, angle_y;
	Vector3 vecX = new Vector3(1.0f,0.0f,0.0f);
	Vector3 vecY = new Vector3(0.0f,1.0f,0.0f);
	//Vector3 vecZ = new Vector3(0.0f,0.0f,1.0f);
	float m_pressed;

	// Use this for initialization
	void Start ()
	{
		System.Collections.Generic.List<GameObject> lis = new System.Collections.Generic.List<GameObject>();
		// find the objects for the 'cannon_bar' and the 'cannon_barrel's.
		m_base = gameObject.transform.FindChild("cannon_base").gameObject;
		m_bar = m_base.transform.FindChild("cannon_bar").gameObject;
		foreach(Transform tf in m_bar.transform)
		{
			GameObject go = tf.gameObject;
			if (go.name.StartsWith("cannon_barrel"))
				lis.Add(go);
		}
		m_barrels = new GameObject[lis.Count];
		for (int i = 0; i < lis.Count; i++)
			m_barrels[i] = lis[i];

		shootNext = 0;
		angle_x=angle_y=0.0f;

		setAngles();

		m_pressed = -1.0f;
	}

	// Update is called once per frame
	void Update ()
	{
		float ix, iy,if1;
		ix = Input.GetAxis("Horizontal")*Time.deltaTime* angle_speed;
		iy = Input.GetAxis("Vertical")*Time.deltaTime* angle_speed;
		angle_x = Mathf.Clamp(angle_x+ix, -angle_limit_side, angle_limit_side);
		angle_y = Mathf.Clamp(angle_y + iy, -angle_limit_down, angle_limit_up);
		setAngles();
		if1 = Input.GetAxis("Fire1");
		if (if1 > 0.5f)
		{
			// button is pressed
			if (m_pressed < 0.0f)
			{
				m_pressed = 0.0f;
				fireGuns();
			}else{
				m_pressed += Time.deltaTime;
				if (m_pressed >= auto_fire_delay)
				{
					m_pressed -= auto_fire_delay;
					fireGuns();
				}
			}
		}else{
			// button is not pressed
			m_pressed = -1.0f;
		}
	}

	void setAngles()
	{
		m_base.transform.localRotation = Quaternion.AngleAxis(angle_x, vecY);
		m_bar.transform.localRotation = Quaternion.AngleAxis(-angle_y, vecX);
	}

	void fireGuns()
	{
		CGun g;
		g = m_barrels[shootNext].GetComponent<CGun>();
		shootNext++;
		if (shootNext >= m_barrels.Length)
			shootNext = 0;
		if (g != null)
			g.Fire();
	}
}
