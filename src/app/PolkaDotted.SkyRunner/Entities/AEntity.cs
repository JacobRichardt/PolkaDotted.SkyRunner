using FarseerPhysics.Dynamics;
using OpenTK;

namespace PolkaDotted.SkyRunner.Entities
{
	public abstract class AEntity
	{
		public bool IsDisposed { get; protected set; }
		protected World World { get; set; }
		protected GameWindow GameWindow { get; set; }

		protected AEntity()
		{
			IsDisposed = false;
		}

		public void Load(World world, GameWindow gameWindow)
		{
			World = world;
			GameWindow = gameWindow;
			Load();
		}

		protected abstract void Load();
		public abstract void Draw();
		public abstract void Update();
	}
}