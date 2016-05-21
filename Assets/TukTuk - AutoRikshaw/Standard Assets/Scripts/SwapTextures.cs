using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwapTextures : MonoBehaviour {
	
	public GameObject m_CleanTexAuto;
	public GameObject m_DirtyTexAuto;

	// Use this for initialization
	void Start () {
	
	}
	 void OnGUI() {
        GUI.Box(new Rect((Screen.width/2)- 150, 5, 300, 25), "Press SpaceBar to swap textures!!");
    }
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(m_CleanTexAuto.activeSelf)
			{
				m_CleanTexAuto.SetActive(false);
				m_DirtyTexAuto.SetActive(true);
			}
			else if(m_DirtyTexAuto.activeSelf)
			{
				m_DirtyTexAuto.SetActive(false);
				m_CleanTexAuto.SetActive(true);
			}
			}
	}
}
