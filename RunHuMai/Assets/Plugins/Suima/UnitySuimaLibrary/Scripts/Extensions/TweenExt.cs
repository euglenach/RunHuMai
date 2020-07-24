using DG.Tweening;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public static class TweenExt{
    public static TaskAwaiter<Tween> GetAwaiter(this Tween self){
        var source = new TaskCompletionSource<Tween>();

        TweenCallback onComplete = null;
        onComplete = () => {
            self.onComplete -= onComplete;
            source.SetResult(self);
        };
        self.onComplete += onComplete;

        return source.Task.GetAwaiter();
    }
}
