using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.IO;

namespace ImageProcessing3
{
    public partial class MainForm : Form
    {
        int shaderProgramme;
        int zUniform;
        int timeUniform;
        int rotateUniform;
        int zoomUniform;
        int vao;
        int tex = 0;
        float zoom = 2;
        Matrix4 rotateMatrix = Matrix4.Identity;

        int texture;
        float[] points =
        {
            0.0f, 0.0f,
            1.0f, 0.0f,
            0.0f, 1.0f,
            1.0f, 1.0f
        };

        private void InitOpenGL()
        {
            GL.ClearColor(Color.MidnightBlue);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(8 * sizeof(float)), points, BufferUsageHint.StaticDraw);

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);

            var vs = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vs, vertextShaderTextBox.Text);
            GL.CompileShader(vs);
            Console.Write(GL.GetShaderInfoLog(vs));
            var fs = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fs, fragmentShaderTextBox.Text);
            GL.CompileShader(fs);
            Console.Write(GL.GetShaderInfoLog(fs));
            shaderProgramme = GL.CreateProgram();
            GL.AttachShader(shaderProgramme, fs);
            GL.AttachShader(shaderProgramme, vs);
            GL.LinkProgram(shaderProgramme);
            Console.WriteLine(GL.GetProgramInfoLog(shaderProgramme));

            zUniform = GL.GetUniformLocation(shaderProgramme, "z");
            timeUniform = GL.GetUniformLocation(shaderProgramme, "time");
            rotateUniform = GL.GetUniformLocation(shaderProgramme, "rotateMatrix");
            zoomUniform = GL.GetUniformLocation(shaderProgramme, "zoom");
        }

        private void Render()
        {
            GL.BindTexture(TextureTarget.Texture3D, tex);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.Purple);

            GL.UseProgram(shaderProgramme);
            GL.UniformMatrix4(rotateUniform, false, ref rotateMatrix);
            GL.Uniform1(zoomUniform, zoom);
            GL.BindVertexArray(vao);
            for (float z = 0; z < 1; z += 0.004f)
            {
                GL.Uniform1(zUniform, z);
                GL.DrawArrays(BeginMode.TriangleStrip, 0, 4);
            }

            glControl.SwapBuffers();
        }


        private void glControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        byte[] rawData;
        public MainForm()
        {
            InitializeComponent();
            vertextShaderTextBox.Text = File.ReadAllText(@"vertexShader.glsl");
            fragmentShaderTextBox.Text = File.ReadAllText(@"fragmentShader.glsl");

            rawData = File.ReadAllBytes("head256x256x109");
            var a = 2;
        }

        private void MainForm_Load(object sender, EventArgs e) =>
            Application.Idle += Application_Idle;

        private void Application_Idle(object sender, EventArgs e)
        {
            while (glControl.IsIdle)
                Render();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitOpenGL();

            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Greater, 0.05f);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.SrcAlpha);

            GL.Enable(EnableCap.Texture3DExt);

            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture3D, texture);
            GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture3D, tex);

            GL.TexImage3D(TextureTarget.Texture3D, 0, PixelInternalFormat.CompressedRed, 256, 256, 109, 0,
                PixelFormat.Red, PixelType.UnsignedByte, rawData);

            GL.Uniform1(GL.GetUniformLocation(shaderProgramme, "image"), tex);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture3D);

            glControl.MouseWheel += (sender, scrollEvent) => zoom += (float)scrollEvent.Delta / 1000;
        }

        private void vertextShaderTextBox_TextChanged(object sender, EventArgs e)
        {
            if (texture != 0) InitOpenGL();
        }

        private void fragmentShaderTextBox_TextChanged(object sender, EventArgs e)
        {
            if (texture != 0) InitOpenGL();
        }

        bool firstMouseEvent = true;
        bool buttonPressed;
        int oldX;
        int oldY;
        float angleX = 0;
        float angleY = 0;
        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!buttonPressed) return;

            if (!firstMouseEvent)
            {
                angleX += oldX - e.X;
                angleY += oldY - e.Y;
                rotateMatrix = Matrix4.CreateRotationX(-angleY / 100) *
                    Matrix4.CreateRotationY(-angleX / 100);
            }

            firstMouseEvent = false;
            oldX = e.X;
            oldY = e.Y;
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e) =>
            buttonPressed = true;

        private void glControl_MouseUp(object sender, MouseEventArgs e) =>
            buttonPressed = !(firstMouseEvent = true);
    }
}
