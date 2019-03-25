using System;
using System.Collections;
using System.Collections.Generic;

namespace KoganeUnityLib
{
	/// <summary>
	/// 直列でタスクを管理するクラス
	/// </summary>
	public sealed class SingleTask : IEnumerable
	{
		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly List<Action<Action>> m_list = new List<Action<Action>>();

		//==============================================================================
		// 変数
		//==============================================================================
		private bool m_isPlaying;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// タスクを追加します
		/// </summary>
		public void Add( Action<Action> task )
		{
			if ( task == null || m_isPlaying ) return;
			m_list.Add( task );
		}

		/// <summary>
		/// タスクを実行します
		/// </summary>
		public void Play( Action onCompleted = null )
		{
			if ( onCompleted == null )
			{
				onCompleted = delegate { };
			}

			if ( m_list.Count <= 0 )
			{
				onCompleted();
				return;
			}

			int count = 0;

			Action task = null;
			task = () =>
			{
				if ( m_list.Count <= count )
				{
					m_isPlaying = false;
					m_list.Clear();
					onCompleted();
					return;
				}

				Action nextTask = task;

				m_list[ count++ ]( () =>
				{
					if ( nextTask == null ) return;
					nextTask();
					nextTask = null;
				} );
			};

			m_isPlaying = true;
			task();
		}

		/// <summary>
		/// タスクを追加します
		/// </summary>
		public void Add( string text, Action<Action> task )
		{
			Add( task );
		}

		/// <summary>
		/// タスクを実行します
		/// </summary>
		public void Play( string text, Action onCompleted = null )
		{
			Play( onCompleted );
		}

		/// <summary>
		/// コレクションを反復処理する列挙子を返します
		/// </summary>
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}