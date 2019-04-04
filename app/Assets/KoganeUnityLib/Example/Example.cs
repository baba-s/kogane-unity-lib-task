using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace KoganeUnityLib.Example
{
	public class Example : MonoBehaviour
	{
		private void Update()
		{
			if ( Input.GetKeyDown( KeyCode.Space ) )
			{
				var task = new SingleTaskWithProfiler
				{
					{ "Call 1", onEnded => StartCoroutine( Call( 0.3f, onEnded ) ) },
					{ "Call 2", onEnded => StartCoroutine( Call( 0.1f, onEnded ) ) },
					{ "Call 3", onEnded => StartCoroutine( Call( 0.0f, onEnded ) ) },
					{ "Call 4", onEnded => StartCoroutine( Call( 0.2f, onEnded ) ) },
				};
				task.Play( "ピカチュウ" );
			}
		}

		private IEnumerator Call( float delay, Action callback )
		{
			yield return new WaitForSeconds( delay );
			callback?.Invoke();
		}
	}
}