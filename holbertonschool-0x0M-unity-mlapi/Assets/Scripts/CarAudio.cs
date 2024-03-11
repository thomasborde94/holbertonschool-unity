using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour 
{
	public AudioClip[] clips;
	public Drive driveScript;
	public AudioSource engine;
	public float maxPitch = 2f;
	public float minPitch = 0.3f;
	public float pitchResetRate = 0.2f;
	public float accelerationPitchRate = 0.5f;
	public float deaccelerationPitchRate = 0.1f;

	public float maxVolume = 1f;
	public float minVolume = 0.3f;

	void Start()
	{
		driveScript = GetComponent<Drive>();
		engine = GetComponent<AudioSource>();
	}

	void Update () 
	{
		if(driveScript != null)
		{
			if(driveScript.currentGear == GearBox.NEUTRAL)
			{
				DecreaseEnginePitch(pitchResetRate);
				DecreaseEngineVolume();
			}
			else
			{
				if(driveScript.currentGear == GearBox.BRAKE)
				{
					DecreaseEnginePitch(deaccelerationPitchRate);
				}
				else
				{
					if(driveScript.currentGear == GearBox.DRIVE)
					{
						if(driveScript.isAccelerating && 
							driveScript.isGrounded)
						{
							SetDriveAudio();
						}
						else
						{
							if(driveScript.velocity < driveScript.gearShiftVelocity[0])
							{
								DecreaseEnginePitch(deaccelerationPitchRate);
								DecreaseEngineVolume();
							}
						}
					}
					else
					{
						if(driveScript.isAccelerating && 
							driveScript.isGrounded)
						{
							SetDriveAudio();
						}
						else
						{
							if(driveScript.velocity < driveScript.gearShiftVelocity[0])
							{
								DecreaseEnginePitch(deaccelerationPitchRate);
								DecreaseEngineVolume();
							}
						}
					}
				}
			}
		}
	}

	public void ShiftGearsUP()
	{
		float slightPitchReduction = 0.3f;
		engine.pitch = engine.pitch - slightPitchReduction;
	}

	public void ShiftGearsDown()
	{
		float slightPitchIncrease = 0.1f;
		engine.pitch = engine.pitch - slightPitchIncrease;
	}

	void SetDriveAudio()
	{
		if(engine.clip != clips[1])
		{
			engine.clip = clips[1];
		}

		if(!engine.isPlaying)
		{
			engine.Play();
		}

		engine.pitch = driveScript.currentGearVelocityPercentage;
//		engine.pitch = Mathf.Lerp(engine.pitch, driveScript.currentGearVelocityPercentage, Time.deltaTime * accelerationPitchRate);
		IncreaseEngineVolume();
	}

	void EngineIdle()
	{
		if(engine.clip != clips[0])
		{
			engine.clip = clips[0];
		}

		if(!engine.isPlaying)
		{
			engine.Play();
		}

		DecreaseEngineVolume();
	}

	void IncreaseEnginePitch(float pitchRate)
	{
		if(engine.pitch < maxPitch)
		{
			engine.pitch += Time.deltaTime * pitchRate;
		}

		if(engine.pitch > maxPitch)
		{
			engine.pitch = maxPitch;
		}
	}

	void DecreaseEnginePitch(float pitchRate)
	{
		if(engine.pitch > minPitch)
		{
			engine.pitch -= Time.deltaTime * pitchRate;
		}

		if(engine.pitch < minPitch)
		{
			engine.pitch = minPitch;
		}
	}

	void IncreaseEngineVolume()
	{
		if(engine.volume < maxVolume)
		{
			engine.volume += Time.deltaTime;
		}
	}

	void DecreaseEngineVolume()
	{
		if(engine.volume > minVolume)
		{
			engine.volume -= Time.deltaTime;
		}
		else
		{
			if(engine.volume < minVolume)
			{
				engine.volume = minVolume;
			}
		}
	}
}
