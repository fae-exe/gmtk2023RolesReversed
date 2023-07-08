using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;

public class ParallaxBackground : MonoBehaviour
{

	/// <summary>
	/// A class used to store layer settings
	/// </summary>
	[Serializable]
	public class ParallaxLayer
	{
		/// the main transform for this layer
		public Transform MainTransform;
		/// the speed at which this layer should move
		public float Speed = 2f;
		/// the maximum distance this layer can travel from its starting position
		public float Amplitude = 50f;
		/// the starting position for this layer
		[HideInInspector]
		public Vector3 StartPosition;
		/// if this is false, this layer won't move
		public bool Active = true;
	}

	/// a multiplier to apply to all layers' amplitudes
	public float AmplitudeMultiplier = 1f;
	/// a speed multiplier to apply to all layers' speeds
	public float SpeedMultiplier = 1f;
	/// a list of all the layers to pilot
	public List<ParallaxLayer> ParallaxLayers;

	protected Vector2 _referencePosition;
	protected Vector3 _newPosition;
	protected Vector2 _mousePosition;

	/// <summary>
	/// On Start we initialize our reference position
	/// </summary>
	protected virtual void Start()
	{
		Initialization();
	}

	/// <summary>
	/// Initializes the start position of all layers
	/// </summary>
	public virtual void Initialization()
	{
		foreach (ParallaxLayer layer in ParallaxLayers)
		{
			layer.StartPosition = layer.MainTransform.position;
		}
	}

	/// <summary>
	/// On Update, moves all layers according to the selected mode
	/// </summary>
	protected virtual void Update()
	{
		MoveLayers();
	}

	/// <summary>
	/// Computes the input data according to the selected mode, and moves the layers accordingly
	/// </summary>
	protected virtual void MoveLayers()
	{
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
		_mousePosition = Mouse.current.position.ReadValue();
#else
		_mousePosition = Input.mousePosition;
#endif
		_referencePosition = Camera.main.ScreenToViewportPoint(_mousePosition);

		foreach (ParallaxLayer layer in ParallaxLayers)
		{
			if (layer.Active)
			{
				_newPosition.x = Mathf.Lerp(layer.MainTransform.position.x, layer.StartPosition.x + _referencePosition.x * layer.Amplitude * AmplitudeMultiplier, layer.Speed * SpeedMultiplier * Time.deltaTime);
				_newPosition.y = Mathf.Lerp(layer.MainTransform.position.y, layer.StartPosition.y + _referencePosition.y * layer.Amplitude * AmplitudeMultiplier, layer.Speed * SpeedMultiplier * Time.deltaTime);
				_newPosition.z = layer.MainTransform.position.z;

				layer.MainTransform.position = _newPosition;
			}
		}
	}

	/// <summary>
	/// Sets a new reference position, to use when in Script mode
	/// </summary>
	/// <param name="newReferencePosition"></param>
	public virtual void SetReferencePosition(Vector3 newReferencePosition)
	{
		_referencePosition = newReferencePosition;
	}
}
