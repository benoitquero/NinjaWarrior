using System;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class Restarter : MonoBehaviour
    {
        

	public void OnClick()
	{
		SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
	}

}

