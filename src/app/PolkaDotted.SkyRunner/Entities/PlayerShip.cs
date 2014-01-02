using System.Collections.Generic;
using System.Drawing;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace PolkaDotted.SkyRunner.Entities
{
	public class PlayerShip : AEntity, IFollowableEntity
	{
		public Vector2 FollowPoint { get; private set; }

		protected Vector2 _position;
		protected Body _body;

		public PlayerShip(float x, float y)
		{
			_position = new Vector2(x, y);
		}

		protected override void Load()
		{
			var vertices = new Vertices(new List<Vector2>
			{
				new Vector2(0, 4),
				new Vector2(1, 1),
				new Vector2(2, 0),
				new Vector2(-2, 0),
				new Vector2(1, 1),
				new Vector2(0, 4)
			});

			_body = BodyFactory.CreatePolygon(World, vertices, .5f, _position);
			_body.BodyType = BodyType.Dynamic;
		}

		public override void Draw()
		{
			GL.Begin(PrimitiveType.Polygon);

			var vertices = ((PolygonShape) _body.FixtureList[0].Shape).Vertices;

			foreach (var vertex in vertices)
			{
				GL.Color3(Color.MidnightBlue);
				GL.Vertex2(_body.GetWorldPoint(vertex).ToDisplayTK());
			}

			GL.End();
		}

		public override void Update()
		{
			if (GameWindow.Keyboard[Key.Left])
				_body.ApplyForce(_body.GetWorldVector(new Vector2(0, 30)), _body.GetWorldPoint(new Vector2(-2, 0)));

			if (GameWindow.Keyboard[Key.Right])
				_body.ApplyForce(_body.GetWorldVector(new Vector2(0, 30)), _body.GetWorldPoint(new Vector2(2, 0)));

			FollowPoint = _body.Position;
		}
	}
}