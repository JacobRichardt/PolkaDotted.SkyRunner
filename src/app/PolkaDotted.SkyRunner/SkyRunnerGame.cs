using System;
using System.Collections.Generic;
using System.Drawing;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PolkaDotted.SkyRunner.Entities;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PolkaDotted.SkyRunner
{
	public class SkyRunnerGame : IDisposable
	{
		private readonly GameWindow _game;
		private readonly World _farSeer;

		private readonly IList<AEntity> _enitities;

		public SkyRunnerGame(GameWindow game)
		{
			_game = game;
			_farSeer = new World(Vector2.Zero);
			ConvertUnits.SetDisplayUnitToSimUnitRatio(32f);

			_enitities = new List<AEntity>();

			_game.Load += (o, args) => Load();
			_game.UpdateFrame += (o, args) => Update();
			_game.RenderFrame += (sender, e) => Render();
		}

		private void Load()
		{
			_enitities.Add(new Ball(10, 13, 2));
			_enitities.Add(new Ball(6, 6, 2));
			_enitities.Add(new Ball(10, 20, 2));
			_enitities.Add(new ControllableBall(20, 10, 2));

			foreach (var enitity in _enitities)
				enitity.Load(_farSeer, _game);
		}

		private void Update()
		{
			if (_game.Keyboard[Key.Escape])
				_game.Exit();

			_farSeer.Step((float)_game.TargetUpdatePeriod);

			foreach (var enitity in _enitities)
				enitity.Update();
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0, _game.Width, 0, _game.Height, 0, 10);

			GL.Begin(PrimitiveType.Triangles);

			GL.Color3(Color.MidnightBlue);
			GL.Vertex2(-1.0f, 1.0f);
			GL.Color3(Color.SpringGreen);
			GL.Vertex2(0.0f, -1.0f);
			GL.Color3(Color.Ivory);
			GL.Vertex2(1.0f, 1.0f);

			GL.End();


			foreach (var enitity in _enitities)
				enitity.Draw();

			_game.SwapBuffers();
		}

		public void Dispose()
		{
			
		}
	}
}