using System.Runtime.CompilerServices;
using FarseerPhysics.Dynamics;
using OpenTK;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PolkaDotted.SkyRunner.Entities
{
	public abstract class AEntity
	{
		protected World World { get; set; }
		protected GameWindow Game { get; set; }

		public void Load(World world, GameWindow game)
		{
			World = world;
			Game = game;
			Load();
		}

		protected abstract void Load();

		public abstract void Draw();
		public abstract void Update();
	}
}