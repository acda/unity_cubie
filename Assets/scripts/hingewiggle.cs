using UnityEngine;
using System.Collections;

public class hingewiggle : MonoBehaviour
{

	private HingeJoint m_hing;
	private JointMotor m_mot;
	private float m_tim;
	public float wiggle_period;
	public float momentum=5.0f;
	public float rotSpeed=5.0f;

	// Use this for initialization
	void Start ()
	{
		m_hing = GetComponent<HingeJoint>();
		m_hing.useMotor = true;
		m_mot = m_hing.motor;
		m_mot.freeSpin = true;
		m_tim = 0.0f;
	}

	// Update is called once per frame
	void Update ()
	{
		m_tim += Time.deltaTime;
		if (m_tim >= 0.5f * wiggle_period)
			m_tim -= wiggle_period;

		m_mot.force = momentum;
		if (m_tim >= 0.0f)
			m_mot.targetVelocity = rotSpeed;
		else
			m_mot.targetVelocity = -rotSpeed;
		m_hing.motor = m_mot;
	}
}
