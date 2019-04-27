//Author: João Azuaga

using Snobfox.Player.Score;
using Snobfox.World;
using Zenject;

namespace Snobfox {
	public sealed class ProjectInstaller : MonoInstaller {

		public override void InstallBindings() {
			Container.Bind<WorldContext>().ToSelf().AsSingle();
			Container.Bind<ScoreContext>().ToSelf().AsSingle();
		}

	}
}
