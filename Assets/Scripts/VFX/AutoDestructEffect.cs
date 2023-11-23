using UnityEngine;
using System.Collections;

public class AutoDestructEffect : MonoBehaviour
{
	[SerializeField] private float _awaitTime = 1f;

	private void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
	}

	private IEnumerator CheckIfAlive()
	{
		yield return new WaitForSeconds(_awaitTime);
		gameObject.SetActive(false);
	}
}
