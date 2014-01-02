using System.Drawing;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using OpenTK.Graphics.OpenGL;

namespace PolkaDotted.SkyRunner.Entities
{
	public class Ball : AEntity
	{
		protected Vector2 _position;
		protected float _radius;
		protected float _density = 0.5f;

		protected Body _body;

		public Ball(float x, float y, float radius)
		{
			_position = new Vector2(x, y);
			_radius = radius;
		}

		protected override void Load()
		{
			_body = BodyFactory.CreateCircle(World, _radius, _density, _position);
		}

		public override void Draw()
		{
			GL.Begin(PrimitiveType.Polygon);


			var test = _body.GetWorldPoint(new Vector2(-_radius, -_radius)).ToDisplayTK();

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
			
		}
	}
}