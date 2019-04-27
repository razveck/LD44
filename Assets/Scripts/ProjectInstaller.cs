//Author: João Azuaga

using Zenject;

namespace Snobfox {
	public sealed class ProjectInstaller : MonoInstaller {

		public Config Config;

		public override void InstallBindings() {
			Container.Bind<Config>().ToSelf().FromInstance(Config).AsSingle();
		}

	}
}
