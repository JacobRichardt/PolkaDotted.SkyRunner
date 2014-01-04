using System.Drawing;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using OpenTK.Graphics.OpenGL;

namespace PolkaDotted.SkyRunner.Entities
{
	public class Bullet : AEntity
	{
		private Body _body;
		private float _radius = 0.1f;
		private Vector2 _position;
		private Vector2 _velocity;
		private float _liveTime;

		public Bullet(Vector2 position, Vector2 velocity)
		{
			_position = position;
			_velocity = velocity;
		}

		protected override void Load()
		{
			_body = BodyFactory.CreateCircle(World, _radius, 0.1f, _position);
			_body.IsStatic = false;
			_body.IsBullet = true;
			_body.ApplyLinearImpulse(_velocity);
		}

		public override void Draw()
		{
			GL.Begin(PrimitiveType.Polygon);

			GL.Color3(Color.MidnightBlue);
			GL.Vertex2(_body.GetWorldPoint(new Vector2(-_radius, -_radius)).ToDisplayTK());
			GL.Color3(Color.SpringGreen);
			GL.Vertex2(_body.GetWorldPoint(new Vector2(_radius, -_radius)).ToDisplayTK());
			GL.Color3(Color.Ivory);
			GL.Vertex2(_body.GetWorldPoint(new Vector2(_radius, _radius)).ToDisplayTK());
			GL.Color3(Color.Aquamarine);
			GL.Vertex2(_body.GetWorldPoint(new Vector2(-_radius, _radius)).ToDisplayTK());

			GL.End();
		}

		public override void Update()
		{
			_liveTime += World.UpdateTime / 50000;

			if (_liveTime > 5)
			{
				IsDisposed = true;
				World.RemoveBody(_body);
			}
		}
	}
}