using Microsoft.Xna.Framework;
using OpenTK.Input;

namespace PolkaDotted.SkyRunner.Entities
{
	public class ControllableBall : Ball, IFollowableEntity
	{
		public Vector2 FollowPoint { get; private set; }

		public ControllableBall(float x, float y, float radius) : base(x, y, radius)
		{
		}

		public override void Update()
		{
			base.Update();
			
			if (GameWindow.Keyboard[Key.Left])
				_body.ApplyForce(_body.GetWorldVector(new Vector2(0, 50)), _body.GetWorldPoint(new Vector2(-_radius, 0)));

			if (GameWindow.Keyboard[Key.Right])
				_body.ApplyForce(_body.GetWorldVector(new Vector2(0, 50)), _body.GetWorldPoint(new Vector2(_radius, 0)));

			FollowPoint = _body.Position;
		}
	}
}