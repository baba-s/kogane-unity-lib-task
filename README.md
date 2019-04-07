# kogane-unity-lib-task

順番にコールバックを実行したり、同時にコールバックを実行したりできるクラス

[![](https://img.shields.io/github/release/baba-s/kogane-unity-lib-task.svg?label=latest%20version)](https://github.com/baba-s/kogane-unity-lib-task/releases)
[![](https://img.shields.io/github/release-date/baba-s/kogane-unity-lib-task.svg)](https://github.com/baba-s/kogane-unity-lib-task/releases)
![](https://img.shields.io/badge/Unity-2018.3%2B-red.svg)
![](https://img.shields.io/badge/.NET-4.x-orange.svg)
[![](https://img.shields.io/github/license/baba-s/kogane-unity-lib-task.svg)](https://github.com/baba-s/kogane-unity-lib-task/blob/master/LICENSE)

## バージョン

- Unity 2018.3.9f1

## 順番にコールバックを実行

### 基本

```cs
var task = new SingleTask
{
    onEnded => Hoge1( onEnded ),
    onEnded => Hoge2( onEnded ),
    onEnded => Hoge3( onEnded ),
};
task.Play( () => Debug.Log( "Complete" ) );
```

### 各コールバックの開始、終了時にログ出力

```cs
var task = new SingleTaskWithLog
{
    { "Hoge1", onEnded => Hoge1( onEnded ) },
    { "Hoge2", onEnded => Hoge2( onEnded ) },
    { "Hoge3", onEnded => Hoge3( onEnded ) },
};
task.Play( "Task", () => Debug.Log( "Complete" ) );
```

### 各コールバックの開始、終了時にログ出力（経過時間付き）

```cs
var task = new SingleTaskWithTimeLog
{
    { "Hoge1", onEnded => Hoge1( onEnded ) },
    { "Hoge2", onEnded => Hoge2( onEnded ) },
    { "Hoge3", onEnded => Hoge3( onEnded ) },
};
task.Play( "Task", () => Debug.Log( "Complete" ) );
```

### 各コールバックの開始、終了時にログ出力（経過時間、GC 発生回数付き）

```cs
var task = new SingleTaskWithProfiler
{
    { "Hoge1", onEnded => Hoge1( onEnded ) },
    { "Hoge2", onEnded => Hoge2( onEnded ) },
    { "Hoge3", onEnded => Hoge3( onEnded ) },
};
task.Play( "Task", () => Debug.Log( "Complete" ) );
```

## 同時にコールバックを実行

### 基本

```cs
var task = new MultiTask
{
    onEnded => Hoge1( onEnded ),
    onEnded => Hoge2( onEnded ),
    onEnded => Hoge3( onEnded ),
};
task.Play( () => Debug.Log( "Complete" ) );
```

### 各コールバックの開始、終了時にログ出力

```cs
var task = new MultiTaskWithLog
{
    { "Hoge1", onEnded => Hoge1( onEnded ) },
    { "Hoge2", onEnded => Hoge2( onEnded ) },
    { "Hoge3", onEnded => Hoge3( onEnded ) },
};
task.Play( "Task", () => Debug.Log( "Complete" ) );
```

### 各コールバックの開始、終了時にログ出力（経過時間付き）

```cs
var task = new MultiTaskWithTimeLog
{
    { "Hoge1", onEnded => Hoge1( onEnded ) },
    { "Hoge2", onEnded => Hoge2( onEnded ) },
    { "Hoge3", onEnded => Hoge3( onEnded ) },
};
task.Play( "Task", () => Debug.Log( "Complete" ) );
```

### 各コールバックの開始、終了時にログ出力（経過時間、GC 発生回数付き）

```cs
var task = new MultiTaskWithProfiler
{
    { "Hoge1", onEnded => Hoge1( onEnded ) },
    { "Hoge2", onEnded => Hoge2( onEnded ) },
    { "Hoge3", onEnded => Hoge3( onEnded ) },
};
task.Play( "Task", () => Debug.Log( "Complete" ) );
```

## ログ出力を有効化

ログ出力を有効化したい場合は `ENABLE_DEBUG_LOG` のシンボルを追加する必要があります