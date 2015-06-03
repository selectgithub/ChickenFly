using UnityEngine;
using System.Collections;

public class UIButtonSound : MonoBehaviour {

	AudioListener mListener;

	public enum Trigger
	{
		OnClick,
		OnMouseOver,
		OnMouseOut,
		OnPress,
		OnRelease,
	}
	
	public AudioClip audioClip;
	public Trigger trigger = Trigger.OnClick;
	public float volume = 1f;
	public float pitch = 1f;
	
	void OnHover (bool isOver)
	{
		if (enabled && ((isOver && trigger == Trigger.OnMouseOver) || (!isOver && trigger == Trigger.OnMouseOut)))
		{
			PlaySound(audioClip, volume, pitch);
		}
	}
	
	void OnPress (bool isPressed)
	{
		if (enabled && ((isPressed && trigger == Trigger.OnPress) || (!isPressed && trigger == Trigger.OnRelease)))
		{
			PlaySound(audioClip, volume, pitch);
		}
	}
	
	public void OnClick ()
	{
		int v = PlayerPrefs.GetInt ("Sound", 1);
		if (enabled && trigger == Trigger.OnClick && v != 0)
		{
			PlaySound(audioClip, volume, pitch);
		}
	}

	AudioSource PlaySound (AudioClip clip, float volume, float pitch)
	{	
		if (clip != null && volume > 0.01f)
		{
			if (mListener == null)
			{
				mListener = GameObject.FindObjectOfType(typeof(AudioListener)) as AudioListener;
				
				if (mListener == null)
				{
					Camera cam = Camera.main;
					if (cam == null) cam = GameObject.FindObjectOfType(typeof(Camera)) as Camera;
					if (cam != null) mListener = cam.gameObject.AddComponent<AudioListener>();
				}
			}
			
			if (mListener != null && mListener.enabled && mListener.gameObject && mListener.gameObject.activeInHierarchy)
			{
				AudioSource source = mListener.GetComponent<AudioSource>();
				if (source == null) source = mListener.gameObject.AddComponent<AudioSource>();
				source.pitch = pitch;
				source.PlayOneShot(clip, volume);
				return source;
			}
		}
		return null;
	}
}
