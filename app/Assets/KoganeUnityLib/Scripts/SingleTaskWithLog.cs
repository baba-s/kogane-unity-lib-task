using KoganeUnityLib.Internal;
using System;
using System.Collections;
using System.Diagnostics;

namespace KoganeUnityLib
{
	/// <summary>
	/// ログ出力機能付きの SingleTask を管理するクラス
	/// </summary>
	public sealed class SingleTaskWithLog : IEnumerable
	{
		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly SingleTask m_task = new SingleTask();

		//==============================================================================
		// 変数
		//==============================================================================
		private string m_name = string.Empty;

		//==============================================================================
		// プロパティ(static)
		//==============================================================================
		public static bool IsLogEnabled { get; set; } = true; // ログ出力が有効の場合 true

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// タスクを追加します
		/// </summary>
		public void Add( string text, Action<Action> task )
		{
			m_task.Add( onEnded =>
			{
				Log( $"【SingleTask】「{m_name}」「{text}」開始" );
				task( () =>
				{
					Log( $"【SingleTask】「{m_name}」「{text}」終了" );
					onEnded();
				} );
			} );
		}

		/// <summary>
		/// タスクを実行します
		/// </summary>
		public void Play( string text, Action onCompleted = null )
		{
			m_name = text;

			Log( $"【SingleTask】「{m_name}」開始" );
			m_task.Play( () =>
			{
				Log( $"【SingleTask】「{m_name}」終了" );
				onCompleted?.Invoke();
			} );
		}

		/// <summary>
		/// ログ出力します
		/// </summary>
		[Conditional( TaskConst.LOG_SYMBOL )]
		private static void Log( string message )
		{
			if ( !IsLogEnabled ) return;
			UnityEngine.Debug.Log( message );
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