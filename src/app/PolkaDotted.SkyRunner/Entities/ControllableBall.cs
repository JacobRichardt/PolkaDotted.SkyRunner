using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using OpenTK.Graphics.OpenGL;
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
				_body.ApplyForce(_body.GetWorldVector(new Vector2(0, 50)), _body.GetWorldPoint(new Vector2(-_radius, 0)));

			if (Game.Keyboard[Key.Right])
				_body.ApplyForce(_body.GetWorldVector(new Vector2(0, 50)), _body.GetWorldPoint(new Vector2(_radius, 0)));
		}
	}
}