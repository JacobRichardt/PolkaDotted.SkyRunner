using System.Drawing;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using OpenTK.Graphics.OpenGL;

namespace PolkaDotted.SkyRunner.Entities
{
	public class Flame : AEntity
	{
		private readonly Vector2 _position;
		private readonly Vector2 _velocity;
		protected Body _body;

		private float _radius;
		private float _liveTime;

		public Flame(Vector2 position, Vector2 velocity)
		{
			_position = position;
			_velocity = velocity;
			_radius = .1f;
		}

		protected override void Load()
		{
			_body = BodyFactory.CreateCircle(World, _radius, 0, _position);
			_body.IsStatic = false;
			_body.IsSensor = true;
			_body.ApplyLinearImpulse(_velocity);
		}

		public override void Draw()
		{
			GL.Begin(PrimitiveType.Polygon);

			var renderedRadius = _radius*_liveTime;

			GL.Color3(Color.Red);
			GL.Vertex2(_body.GetWorldPoint(new Vector2(-renderedRadius, -renderedRadius)).ToDisplayTK());
			GL.Color3(Color.OrangeRed);
			GL.Vertex2(_body.GetWorldPoint(new Vector2(renderedRadius, -renderedRadius)).ToDisplayTK());
			GL.Color3(Color.DarkRed);
			GL.Vertex2(_body.GetWorldPoint(new Vector2(renderedRadius, renderedRadius)).ToDisplayTK());
			GL.Color3(Color.DarkOrange);
			GL.Vertex2(_body.GetWorldPoint(new Vector2(-renderedRadius, renderedRadius)).ToDisplayTK());

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