using System;
using System.Runtime.CompilerServices;
using System.Threading;
using DG.Tweening;

namespace Suima.Extensions{
    /// <summary>
    /// DOTweenをasync/awaitに返還すDOTweenAwaiterの拡張メソッド
    /// </summary>
    public static class DOTweenAsyncExtensionWithCancellationToken
    {
        public static DOTweenAwaiter ToAwaiter(this Tween tween,
                                               CancellationToken cancellationToken = default,
                                               TweenCancelBehaviour behaviour = TweenCancelBehaviour.Kill)
        {
            return new DOTweenAwaiter(tween, cancellationToken, behaviour);
        }
    }

    /// <summary>
    /// DOTweenをawaiterに変換する
    /// </summary>
    public struct DOTweenAwaiter : ICriticalNotifyCompletion
    {
        private Tween tween;
        private CancellationToken cancellationToken;
        private TweenCancelBehaviour behaviour;

        public DOTweenAwaiter(Tween tween, CancellationToken cancellationToken, TweenCancelBehaviour behaviour)
        {
            this.tween = tween;
            this.cancellationToken = cancellationToken;
            this.behaviour = behaviour;
        }

        public bool IsCompleted => tween.IsPlaying() == false;

        public void GetResult() => cancellationToken.ThrowIfCancellationRequested();

        public void OnCompleted(Action continuation) => UnsafeOnCompleted(continuation);

        public void UnsafeOnCompleted(Action continuation)
        {
            DOTweenAwaiter tmpThis = this;
            var tween = this.tween;
            var regist = tmpThis.cancellationToken.Register(() =>
            {
                // tokenが発火したらタイプをチェックしてTweenの終了振る舞いを変更する
                switch (tmpThis.behaviour)
                {
                    case TweenCancelBehaviour.Kill:
                        tween.Kill();
                        break;
                    case TweenCancelBehaviour.KillWithCompleteCallback:
                        tween.Kill(true);
                        break;
                    case TweenCancelBehaviour.Complete:
                        tween.Complete();
                        break;
                }
            });
        
            this.tween.OnKill(() =>
            {
                // CancellationTokenRegistrationを破棄する
                regist.Dispose();
                // 続きを実行
                continuation();
            });
        }

        public DOTweenAwaiter GetAwaiter() => this;
    }

    /// <summary>
    /// Tweenキャンセル時の振る舞い
    /// </summary>
    public enum TweenCancelBehaviour
    {
        Kill,
        KillWithCompleteCallback,
        Complete,
    }
}