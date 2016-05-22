using UnityEngine;
using System.Collections;

public class CHitable : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		m_hitPoints = def_baseHitpoints;
	}

    public delegate void ChangeHpCallback(GameObject thisOne,float newHP,float maxHP);

    public event ChangeHpCallback a;

	public void takeDamage( GameObject instigator , Vector3 impact , float amount )
	{
		m_hitPoints -= amount;
		if(m_hitPoints<=0.0f)
		{
			// spawn a boom.
			if( ! System.Object.ReferenceEquals(def_boomAnim,null) )
				Instantiate(def_boomAnim,transform.position,new Quaternion());
			Transform walk = transform;
			while(!System.Object.ReferenceEquals(walk.parent,null))
			{
				walk = walk.parent;
			}
			Destroy(walk.gameObject);
        }else{
            if(a!=null)
                a.Invoke(gameObject, m_hitPoints, def_baseHitpoints); 
        }
	}

    private void OnTouchOther(GameObject other)
    {
        CBullet shot = other.GetComponent<CBullet>();
        if ((!System.Object.ReferenceEquals(shot, null)) && 0!=(hitmask&shot.hitmask))
        {
            // hit by a bullet.
            takeDamage(other, other.GetComponent<Rigidbody>().velocity, shot.def_damage);
            Destroy(other);  // ..... add boom.
        }
    }

    void OnTriggerEnter2D(Collider2D other)
	{
        OnTouchOther(other.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        OnTouchOther(other.gameObject);
    }

    void OnCollisionEnter(Collision info)
    {
        OnTouchOther(info.collider.gameObject);
    }

    public float def_baseHitpoints = 1.0f;
	public GameObject def_boomAnim;
	public uint hitmask = 65536;

	internal float m_hitPoints;
}
