using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class JumpToOtherScene : MonoBehaviour {
	IEnumerator goToSceneDelay(string scene){
		yield return new WaitForSeconds (4f);

		#if UNITY_5_5_OR_NEWER
			SceneManager.LoadScene(scene);
		#else
			Application.LoadLevel(scene);
		#endif
	}

    public static void quickGoToScene(string scene)
    {
		#if UNITY_5_5_OR_NEWER
			SceneManager.LoadScene(scene);
		#else
        	Application.LoadLevel(scene);
		#endif
	}

    public void goToScene(string scene)
	{
		#if UNITY_5_5_OR_NEWER
			SceneManager.LoadScene(scene);
		#else
        	Application.LoadLevel(scene);
		#endif
	}

    public void goToScene()
	{
		#if UNITY_5_5_OR_NEWER
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		#else
        	Application.LoadLevel(Application.loadedLevelName);
		#endif
	}

	public void goCloseApplication()
	{
		Application.Quit ();
	}
}