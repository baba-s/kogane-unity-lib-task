using System;
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
				var task = new SingleTaskWithProfile
				{
					{ "Call 1", onEnded => Call( 3000, onEnded ) },
					{ "Call 2", onEnded => Call( 1000, onEnded ) },
					{ "Call 3", onEnded => Call( 0, onEnded ) },
					{ "Call 4", onEnded => Call( 2000, onEnded ) },
				};
				task.Play( "ピカチュウ" );
			}
		}

		private async void Call( int delay, Action callback )
		{
			await Task.Delay( delay );
			var count = UnityEngine.Random.Range( 0, 1000000 );
			for ( int i = 0; i < count; i++ )
			{
				var str = $"{i}";
			}
			callback?.Invoke();
		}
	}
}