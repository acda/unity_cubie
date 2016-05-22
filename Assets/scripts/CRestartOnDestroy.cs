using UnityEngine;
using System.Collections;

public class CRestartOnDestroy : MonoBehaviour
{

	void OnDestroy()
	{
		UnityEngine.SceneManagement.Scene scn;
		scn = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
		Debug.Log("reload scene #"+scn.buildIndex.ToString());
		UnityEngine.SceneManagement.SceneManager.LoadScene(scn.buildIndex);
	}

}
