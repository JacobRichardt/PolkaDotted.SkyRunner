using System;
using System.Collections.Generic;
using System.Diagnostics;
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
				new Vector2(2, 0),
				new Vector2(-2, 0),
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
			var state = GamePad.GetState(0);

			if (state.IsConnected)
			{
				if (state.Triggers.Left > 0)
					FireEngine(new Vector2(-2f, -.2f), new Vector2(0, state.Triggers.Left * 128 * 300));

				if ( state.Triggers.Right > 0)
					FireEngine(new Vector2(2, -.2f), new Vector2(0, state.Triggers.Right * 128 * 300));

				if (state.Buttons.A == ButtonState.Pressed)
					Shoot();
			}

			FollowPoint = _body.Position;
		}

		private void Shoot()
		{
			var worldPosition = _body.GetWorldPoint(new Vector2(0, 4.2f));
			var worldPower = _body.GetWorldVector(new Vector2(0, 100));

			_skyRunnerGame.AddEntity(new Bullet(worldPosition, worldPower));
		}

		public void FireEngine(Vector2 position, Vector2 power)
		{
			var worldPosition = _body.GetWorldPoint(position);
			var worldPower = _body.GetWorldVector(power);

			_body.ApplyForce(worldPower, worldPosition);

			power.Y = -power.Y;

			_skyRunnerGame.AddEntity(new Flame(worldPosition, _body.GetWorldVector(power * .05f)));
		}
	}
}