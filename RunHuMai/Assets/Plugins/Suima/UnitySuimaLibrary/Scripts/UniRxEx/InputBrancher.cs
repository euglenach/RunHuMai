using System;
using UnityEngine;
using UniRx;

namespace InputBranch{
    public  static class InputBrancher{
        public static IInputReceiver Current{get; private set;}

        public static IObservable<Unit> GetKey(IInputReceiver receiver, KeyCode key) =>
            InputAsObservable.GetKey(key).Where(_ => receiver == Current);

        public static IObservable<Unit> GetKeyDown(IInputReceiver receiver, KeyCode key) =>
            InputAsObservable.GetKeyDown(key).Where(_ => receiver == Current);

        public static IObservable<Unit> GetKeyUp(IInputReceiver receiver, KeyCode key) =>
            InputAsObservable.GetKeyUp(key).Where(_ => receiver == Current);

        public static IObservable<Unit> AnyKey(IInputReceiver receiver) =>
            InputAsObservable.AnyKey.Where(_ => receiver == Current);

        public static IObservable<Unit> AnyKeyDown(IInputReceiver receiver) =>
            InputAsObservable.AnyKeyDown.Where(_ => receiver == Current);
        
        public static IObservable<float> Axis(IInputReceiver receiver, string axisName) =>
             InputAsObservable.Axis(axisName).Where(_ => receiver == Current);

        public static IObservable<float> AxisRaw(IInputReceiver receiver, string axisName) =>
             InputAsObservable.AxisRaw(axisName).Where(_ => receiver == Current);
        
        public static IObservable<Unit> GetMouseButton(IInputReceiver receiver, int button) =>
            InputAsObservable.GetMouseButton(button).Where(_ => receiver == Current);
        
        public static IObservable<Unit> GetMouseButtonDown(IInputReceiver receiver, int button) =>
            InputAsObservable.GetMouseButtonDown(button).Where(_ => receiver == Current);
        
        public static IObservable<Unit> GetMouseButtonUp(IInputReceiver receiver, int button) =>
            InputAsObservable.GetMouseButtonUp(button).Where(_ => receiver == Current);
        
        public static IObservable<Unit> GetButton(IInputReceiver receiver, string buttonName) =>
            InputAsObservable.GetButton(buttonName).Where(_ => receiver == Current);
        
        public static IObservable<Unit> GetButtonDown(IInputReceiver receiver, string buttonName) =>
            InputAsObservable.GetButtonDown(buttonName).Where(_ => receiver == Current);
        
        public static IObservable<Unit> GetButtonUp(IInputReceiver receiver, string buttonName) =>
            InputAsObservable.GetButtonUp(buttonName).Where(_ => receiver == Current);
        
        public static IObservable<Unit> Get(IInputReceiver receiver,IObservable<Unit> source){
            return source.Where(_ => receiver == Current);
        }

        public static IObservable<float> Axis(IInputReceiver receiver, IObservable<float> source){
            return source.Where(_ => receiver == Current);
        }

        public static void Switch(IInputReceiver receiver){
            Current = receiver;
        }

        public static bool IsCurrent(IInputReceiver receiver) =>
            Current == receiver;
    }

    public interface IInputReceiver{}
}
