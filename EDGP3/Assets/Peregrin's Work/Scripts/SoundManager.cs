using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundBehavior{
	public AudioSource source;
	public int index;
	
	public void Constructor(AudioSource s, int ind)
	{
		this.source = s;
		this.index = ind;
	}
}

public class SoundManager : MonoBehaviour {

	public string[] names;
	public AudioClip[] clips;
	
	
	public Dictionary<string, SoundBehavior> sources = new Dictionary<string, SoundBehavior>();

	// Use this for initialization
	void Start ()
	{
		
		for (int i = 0; i < clips.Length; i++)
		{
			sources.Add(names[i], new SoundBehavior());
			sources[names[i]].Constructor(GetComponents<AudioSource>()[i], i);
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void PlaySound(string soundName)
	{
		SoundBehavior behavior = sources[soundName];
		behavior.source.clip = clips[behavior.index]; 
		behavior.source.Play();
	}
}