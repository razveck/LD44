//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Snobfox.Utility {
	public static class Awaiters {
		public static IObservable<Unit> Frames(int frames){
			return Observable.ReturnUnit().DelayFrame(frames);
		}

		public static IObservable<Unit> Seconds(float seconds){
			return Observable.ReturnUnit().Delay(TimeSpan.FromSeconds(seconds));
		}
	}
}
