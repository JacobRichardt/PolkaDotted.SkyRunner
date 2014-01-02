using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using OpenTK.Input;

namespace PolkaDotted.SkyRunner.Entities
{
	public class ControllableBall : Ball
	{
		public ControllableBall(float x, float y, float radius) : base(x, y, radius)
		{
		}

		public override void Update()
		{
			base.Update();

			if (Game.Keyboard[Key.Left])
				_body.ApplyForce(new Vector2(-1, 0));
			if (Game.Keyboard[Key.Right])
				_body.ApplyForce(new Vector2(1, 0));
			if (Game.Keyboard[Key.Up])
				_body.ApplyForce(new Vector2(0, -1));
			if (Game.Keyboard[Key.Down])
				_body.ApplyForce(new Vector2(0, 1));
		}
	}
}