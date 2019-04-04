using KoganeUnityLib.Internal;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace KoganeUnityLib
{
	/// <summary>
	/// 処理時間のログ出力機能付きの MultiTask を管理するクラス
	/// </summary>
	public sealed class MultiTaskWithTimeLog : IEnumerable
	{
		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly MultiTask m_task = new MultiTask();

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
				Log( $"【MultiTask】「{m_name}」「{text}」開始" );
				var startTime = Time.realtimeSinceStartup;
				task( () =>
				{
				var elapsedTime = Time.realtimeSinceStartup - startTime;
					Log( $"【MultiTask】「{m_name}」「{text}」終了    {elapsedTime.ToString( "0.###" ) } 秒" );
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

			Log( $"【MultiTask】「{m_name}」開始" );
			var startTime = Time.realtimeSinceStartup;
			m_task.Play( () =>
			{
				var elapsedTime = Time.realtimeSinceStartup - startTime;
				Log( $"【MultiTask】「{m_name}」終了    {elapsedTime.ToString( "0.###" ) } 秒" );
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