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

		private readonly SkyRunnerGame _skyRunnerGame;
		protected Vector2 _position;
		protected Body _body;

		public PlayerShip(float x, float y, SkyRunnerGame skyRunnerGame)
		{
			_skyRunnerGame = skyRunnerGame;
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

			_body = BodyFactory.CreatePolygon(World, vertices, 10f, _position);
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
				FireEngine(new Vector2(-2f, -.2f), new Vector2(0, 300));

			if (GameWindow.Keyboard[Key.Right])
				FireEngine(new Vector2(2, -.2f), new Vector2(0, 300));

			FollowPoint = _body.Position;
		}

		public void FireEngine(Vector2 position, Vector2 power)
		{
			var worldPosition = _body.GetWorldPoint(position);
			var worldPower = _body.GetWorldVector(power);

			_body.ApplyForce(worldPower, worldPosition);

			power.Y = -power.Y;

			_skyRunnerGame.AddEntity(new Flame(worldPosition, _body.GetWorldVector(power * .1f)));
		}
	}
}