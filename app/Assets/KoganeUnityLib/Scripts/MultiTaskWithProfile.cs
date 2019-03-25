using KoganeUnityLib.Internal;
using System;
using System.Collections;
using System.Diagnostics;

namespace KoganeUnityLib
{
	/// <summary>
	/// 処理時間や GC の発生回数の出力機能付きの MultiTask を管理するクラス
	/// </summary>
	public sealed class MultiTaskWithProfile : IEnumerable
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
				var sw = new Stopwatch();
				sw.Start();
				var gcw = new GCWatcher();
				gcw.Start();
				task( () =>
				{
					gcw.Stop();
					sw.Stop();
					var elapsedTime = ToElapsedTime( sw );
					Log( $"【MultiTask】「{m_name}」「{text}」終了 / {elapsedTime} / GC {gcw.Count.ToString()} 回" );
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

			Log( $"【MultiTask】「{text}」開始" );
			var sw = new Stopwatch();
			sw.Start();
			var gcw = new GCWatcher();
			gcw.Start();
			m_task.Play( () =>
			{
				gcw.Stop();
				sw.Stop();
				var elapsedTime = ToElapsedTime( sw );
				Log( $"【MultiTask】「{text}」終了 / {elapsedTime} / GC {gcw.Count.ToString()} 回" );
				onCompleted?.Invoke();
			} );
		}

		/// <summary>
		/// 経過時間のテキストに変換して返します
		/// </summary>
		private static string ToElapsedTime( Stopwatch sw )
		{
			var ts = sw.Elapsed;
			var elapsedTime = $"{ts.Seconds.ToString()}.{ts.Milliseconds.ToString()} 秒";
			return elapsedTime;
		}

		/// <summary>
		/// ログ出力します
		/// </summary>
		[Conditional( "ENABLE_DEBUG_LOG" )]
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