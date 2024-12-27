using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modpack
{
	public class ConstantsManager : MonoBehaviour
	{
		public ConstantsManager()
		{
			ConstantsManager.gravityModifiers = new Dictionary<string, float>();
			ConstantsManager.rotationModifiers = new Dictionary<string, float>();
			ConstantsManager.timeModifiers = new Dictionary<string, float>();
		}

		public void Awake()
		{
			ConstantsManager.updateRotation();
		}

		private static void updateGravity()
		{
			ConstantsManager.gravity = -30f;
			foreach (float num in ConstantsManager.gravityModifiers.Values)
			{
				ConstantsManager.gravity *= num;
			}
			Physics2D.gravity = new Vector2(Mathf.Cos(ConstantsManager.rotation), Mathf.Sin(ConstantsManager.rotation)) * ConstantsManager.gravity;
		}

		private static void updateRotation()
		{
			ConstantsManager.rotation = 1.5707964f;
			foreach (float num in ConstantsManager.rotationModifiers.Values)
			{
				ConstantsManager.rotation += num;
			}
			float num2 = 6.2831855f;
			ConstantsManager.rotation = (ConstantsManager.rotation % num2 + num2) % num2;
			ConstantsManager.updateGravity();
		}

		private static void updateTime()
		{
			ConstantsManager.timeScale = 1f;
			foreach (float num in ConstantsManager.timeModifiers.Values)
			{
				ConstantsManager.timeScale *= num;
			}
			Time.timeScale = ConstantsManager.timeScale;
		}

		public static void removeGravityModifier(string name)
		{
			ConstantsManager.gravityModifiers.Remove(name);
			ConstantsManager.updateGravity();
		}

		public static void removeRotationModifier(string name)
		{
			ConstantsManager.rotationModifiers.Remove(name);
			ConstantsManager.updateRotation();
		}

		public static void removeTimeModifier(string name)
		{
			ConstantsManager.timeModifiers.Remove(name);
			ConstantsManager.updateTime();
		}

		public static void modifyGravity(string name, float amount)
		{
			ConstantsManager.gravityModifiers[name] = amount;
			ConstantsManager.updateGravity();
		}

		public static void modifyRotation(string name, float amount)
		{
			ConstantsManager.rotationModifiers[name] = amount;
			ConstantsManager.updateRotation();
		}

		public static void modifyTime(string name, float amount)
		{
			ConstantsManager.timeModifiers[name] = amount;
			ConstantsManager.updateTime();
		}

		private static Dictionary<string, float> gravityModifiers;

		private static Dictionary<string, float> rotationModifiers;

		private static Dictionary<string, float> timeModifiers;

		private static float rotation;

		private static float timeScale;

		private static float gravity;
	}
}
