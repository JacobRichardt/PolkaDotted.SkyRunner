using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PolkaDotted.SkyRunner.Entities;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PolkaDotted.SkyRunner
{
	public class SkyRunnerGame : IDisposable
	{
		private readonly GameWindow _gameWindow;
		private readonly World _world;
		private readonly Camera _camera;
		private readonly IList<AEntity> _enitities;

		public SkyRunnerGame(GameWindow gameWindow)
		{
			_gameWindow = gameWindow;
			_world = new World(Vector2.Zero);
			ConvertUnits.SetDisplayUnitToSimUnitRatio(32f);
			_camera = new Camera(_gameWindow);
			_enitities = new List<AEntity>();

			_gameWindow.Load += (o, args) => Load();
			_gameWindow.UpdateFrame += (o, args) => Update();
			_gameWindow.RenderFrame += (sender, e) => Render();
		}

		public void AddEntity(AEntity entity)
		{
			entity.Load(_world, _gameWindow);

			_enitities.Add(entity);
		}

		private void Load()
		{
			var rnd = new Random();

			for (int i = 0; i < 200; i++)
			{
				AddEntity(new Ball((float)rnd.NextDouble() * 30, (float)rnd.NextDouble() * 15, 0.3f));
			}

			var player = new PlayerShip(20, 10, this);

			AddEntity(player);

			_camera.SetTarget(player);
		}

		private void Update()
		{
			if (_gameWindow.Keyboard[Key.Escape])
				_gameWindow.Exit();

			Debug.WriteLine(_gameWindow.UpdatePeriod);

			_world.Step((float)_gameWindow.TargetUpdatePeriod);

			var index = 0;
			while (index < _enitities.Count)
			{
				_enitities[index].Update();

				if (_enitities[index].IsDisposed)
					_enitities.RemoveAt(index);
				else
					index++;
			}
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			
			_camera.Update();

			foreach (var enitity in _enitities)
				enitity.Draw();

			_gameWindow.SwapBuffers();
		}

		public void Dispose()
		{
			
		}
	}
}