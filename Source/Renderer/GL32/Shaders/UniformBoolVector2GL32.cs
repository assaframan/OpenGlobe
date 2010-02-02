﻿#region License
//
// (C) Copyright 2009 Patrick Cozzi and Deron Ohlarik
//
// Distributed under the Boost Software License, Version 1.0.
// See License.txt or http://www.boost.org/LICENSE_1_0.txt.
//
#endregion

using OpenTK;
using OpenTK.Graphics.OpenGL;
using MiniGlobe.Renderer;

namespace MiniGlobe.Renderer.GL32
{
    internal class UniformBoolVector2GL32 : Uniform<Vector2b>, ICleanable
    {
        internal UniformBoolVector2GL32(int programHandle, string name, int location)
            : base(name, location, UniformType.BoolVector2)
        {
            int[] initialValue = new int[2];
            GL.GetUniform(programHandle, location, initialValue);
            _value = new Vector2b((initialValue[0] != 0), (initialValue[1] != 0));
        }

        #region ICleanable Uniform<>

        public override Vector2b Value
        {
            set
            {
                if (_value != value)
                {
                    _value = value;
                    _dirty = true;
                }
            }

            get { return _value; }
        }

        #endregion

        #region ICleanable Members

        public void Clean()
        {
            if (_dirty)
            {
                GL.Uniform2(Location, _value.X ? 1 : 0, _value.Y ? 1 : 0);
                _dirty = false;
            }
        }

        #endregion

        private Vector2b _value;
        private bool _dirty;
    }
}