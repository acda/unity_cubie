using UnityEngine;
using System.Collections;

public class CGun : MonoBehaviour
{

	public GameObject prefab_bullet;
	public float move_recoil = 0.25f;
	public float time_moveRecoil = 0.05f;
	public float time_moveReturn = 0.333f;
	public float spawn_offset_Z = 1.0f;
	Vector3 orgPos;
	Vector3 vecZ = new Vector3(0.0f,0.0f,1.0f);
	float m_tim;
	float m_recoil;
	public float shot_speed = 15.0f;

	// Use this for initialization
	void Start ()
	{
		orgPos = transform.localPosition;
		if (time_moveRecoil < 0.01) time_moveRecoil = 0.01f;
		if (time_moveReturn < 0.01) time_moveReturn = 0.01f;
		m_tim = time_moveReturn;
		m_recoil = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_tim < time_moveReturn)
		{
			m_tim += Time.deltaTime;
			if (m_tim > time_moveReturn)
				m_tim = time_moveReturn;
			if (m_tim < 0.0f)
				m_recoil = (time_moveRecoil + m_tim) / time_moveRecoil;
			else
				m_recoil = (time_moveReturn-m_tim) / time_moveReturn;
			transform.localPosition = orgPos - vecZ*(m_recoil * move_recoil);
		}
	}

	public void Fire()
	{
		GameObject bullet;
		Rigidbody rgb;
		m_tim = -time_moveRecoil;
		m_recoil = 0.0f;
		transform.localPosition = orgPos;
		Vector3 pos;
		Vector3 shotdir = Random.insideUnitCircle*0.04f;
		shotdir.z = 1.0f;
		Quaternion rot;
		pos = transform.TransformPoint(vecZ * spawn_offset_Z);
		rot = Quaternion.FromToRotation(vecZ,transform.TransformDirection(shotdir));
		bullet = (GameObject)Instantiate(prefab_bullet, pos, rot);
		rgb = bullet.GetComponent<Rigidbody>();
		if (rgb != null)
			rgb.velocity = shot_speed * transform.TransformDirection(shotdir);
	}
}
