using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public static class TimerRx{
    public static IObservable<Unit> StoppableIntervalFrame(this Component component,Func<bool> stopFlagFunc,int intervalFrameCount,FrameCountType frameCountType = FrameCountType.Update){
        var timing = frameCountType == FrameCountType.Update
                         ? component.UpdateAsObservable()
                         : component.FixedUpdateAsObservable();

        return timing.Where(_ => !stopFlagFunc())
                     .Skip(intervalFrameCount)
                     .Take(1)
                     .RepeatUntilDestroy(component);
    }

    public static IObservable<Unit> StoppableTimerFrame(this Component component, Func<bool> stopFlagFunc,int deuTimeFrameCount,FrameCountType frameCountType = FrameCountType.Update){
        var timing = frameCountType == FrameCountType.Update
                         ? component.UpdateAsObservable()
                         : component.FixedUpdateAsObservable();
        return timing.Where(_ => !stopFlagFunc())
                     .Skip(deuTimeFrameCount)
                     .Take(1);
    }

    public static IObservable<Unit> StoppableTimer(this Component component, Func<bool> stopFlagFunc,int deuTime){
        return Observable.EveryUpdate()
                         .Where(_ => !stopFlagFunc())
                         .Skip(TimeSpan.FromSeconds(deuTime))
                         .FirstOrDefault()
                         .AsUnitObservable();
    }
    public static IObservable<Unit> StoppableInterval(this Component component, Func<bool> stopFlagFunc,int period){
        return Observable.EveryUpdate()
                         .Where(_ => !stopFlagFunc())
                         .Skip(TimeSpan.FromSeconds(period))
                         .FirstOrDefault()
                         .AsUnitObservable()
                         .RepeatUntilDestroy(component);
    }
}
