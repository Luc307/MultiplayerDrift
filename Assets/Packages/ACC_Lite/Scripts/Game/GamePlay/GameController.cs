using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class game controller.
/// </summary>
public class GameController :MonoBehaviour
{
	public KeyCode nextCarKey = KeyCode.N;
	public static GameController Instance;
	public static CarController PlayerCar { get { return Instance.m_PlayerCar; } }
	public static bool RaceIsStarted { get { return true; } }
	public static bool RaceIsEnded { get { return false; } }

	CarController m_PlayerCar;
	List<CarController> Cars = new List<CarController>();

	protected virtual void Awake ()
	{

		Instance = this;

		//Find all cars in current game.
		Cars.AddRange(FindObjectsByType<CarController>(FindObjectsSortMode.None));
		Cars = Cars.OrderBy (c => c.name).ToList();

		foreach (var car in Cars)
		{
			var userControl = car.GetComponent<UserControl>();
			var audioListener = car.GetComponent<AudioListener>();

			if (userControl == null)
			{
				userControl = car.gameObject.AddComponent<UserControl> ();
			}

			if (audioListener == null)
			{
				audioListener = car.gameObject.AddComponent<AudioListener> ();
			}

			userControl.enabled = false;
			audioListener.enabled = false;
		}

		m_PlayerCar = Cars[0];
		m_PlayerCar.GetComponent<UserControl> ().enabled = true;
		m_PlayerCar.GetComponent<AudioListener> ().enabled = true;
	}

    //private void Update()s
    //{
    //    if(Input.))
    //}
}
