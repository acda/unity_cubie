using UnityEngine;
using System.Collections;

public class CController : MonoBehaviour
{
	// vars for items we need
	GameObject m_UI_label_wave;
	GameObject m_UI_label_numCubes;

	CSpawnCubies_from_VertsMesh form_spawner;

	System.Collections.Generic.List<GameObject> cubelist;
	int scanpoint;
	uint currentWave;

	// Use this for initialization
	void Start ()
	{
		// find UI elements
		GameObject[] objs;
		objs = gameObject.scene.GetRootGameObjects();
		foreach (GameObject go in objs)
		{
			if (go.name != "Canvas")
				continue;
			// found canvas.
			m_UI_label_numCubes = findNamedChildObj(go, "Textlabel_number_cubes");
			m_UI_label_wave = findNamedChildObj(go, "Textlabel_wave");
			//			t = go.transform.FindChild("Textlabel_number_cubes");
			//			m_UI_label_numCubes = t.gameObject;
			//			t = go.transform.FindChild("Textlabel_wave");
			//			m_UI_label_wave = t.gameObject;
			break;
		}
		UnityEngine.Assertions.Assert.IsNotNull(m_UI_label_numCubes);
		UnityEngine.Assertions.Assert.IsNotNull(m_UI_label_wave);

		// find formation spawner(s)
		foreach (GameObject go in objs)
		{
			if (go.name=="formation")
			{
				form_spawner = go.GetComponent<CSpawnCubies_from_VertsMesh>();
				if (form_spawner.spawn_on_start)
					form_spawner = null;	// don't want that one.
				else
					break;		// that's ours.
			}
		}
		UnityEngine.Assertions.Assert.IsNotNull(form_spawner);

		currentWave = 1;

		// trigger spawn
		startWave();
	}

	void startWave()
	{
		spawnFormation();
		update_UI_wave();
	}

	// Update is called once per frame
	void Update ()
	{
		if (cubelist.Count > 0)
		{
			// not empty. check.
			for (int j = 0; j < 1; j++)
			{
				if (++scanpoint >= cubelist.Count)
					scanpoint = 0;
				if (cubelist[scanpoint] == null)
				{
					cubelist.RemoveAt(scanpoint);
					update_UI_cubies();
					break;
				}
			}
		}else{
			// empty. respawn.
			currentWave++;
			startWave();
		}
	}

	void spawnFormation()
	{
		cubelist = new System.Collections.Generic.List<GameObject>(form_spawner.DoSpawn());
		scanpoint = 0;
		update_UI_cubies();
	}

	void update_UI_cubies()
	{
		UnityEngine.UI.Text tx;
		tx = m_UI_label_numCubes.GetComponent<UnityEngine.UI.Text>();
		tx.text = cubelist.Count.ToString();
	}

	void update_UI_wave()
	{
		UnityEngine.UI.Text tx;
		tx = m_UI_label_wave.GetComponent<UnityEngine.UI.Text>();
		tx.text = currentWave.ToString();
	}

	GameObject findNamedChildObj(GameObject obj,string name)
	{
		for (int i = 0; i < obj.transform.childCount; i++)
		{
			Transform t = obj.transform.GetChild(i);
			GameObject go = t.gameObject;
			if (go.name == name)
				return go;
			go = findNamedChildObj(go, name);
			if (go != null)
				return go;
		}
		return null;
	}
}
