using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using Dicom.Imaging;
using System.IO;
using System.Drawing.Imaging;
using ScintillaNET;

namespace ImageProcessing2
{
    public partial class MainForm : Form
    {
        int shaderProgramme;
        int timeUniform;
        int drawUnitUniform;
        int vao;
        int tex = 0;

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
            GL.Enable(EnableCap.Texture2D);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);


            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
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
            var fs = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fs, fragmentShaderTextBox.Text);
            GL.CompileShader(fs);
            shaderProgramme = GL.CreateProgram();
            GL.AttachShader(shaderProgramme, fs);
            GL.AttachShader(shaderProgramme, vs);
            GL.LinkProgram(shaderProgramme);


            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);


            GL.Uniform1(GL.GetUniformLocation(shaderProgramme, "image"), tex);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            timeUniform = GL.GetUniformLocation(shaderProgramme, "time");
            drawUnitUniform = GL.GetUniformLocation(shaderProgramme, "draw_unit");

            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, GL_LINEAR);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, GL_LINEAR);
        }

        private void Render()
        {
            GL.BindTexture(TextureTarget.Texture2D, tex);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.Purple);
            GL.UseProgram(shaderProgramme);
            GL.Uniform1(drawUnitUniform, (float)4 / glControl.Width);
            GL.Uniform1(timeUniform, (float)DateTime.UtcNow.TimeOfDay.TotalMilliseconds / 1000);
            GL.BindVertexArray(vao);
            GL.DrawArrays(BeginMode.TriangleStrip, 0, 4);

            glControl.SwapBuffers();
        }


        private void glControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        Bitmap bitmap;
        public MainForm()
        {
            InitializeComponent();
            vertextShaderTextBox.Text = File.ReadAllText(@"vertexShader.glsl");
            fragmentShaderTextBox.Text = File.ReadAllText(@"fragmentShader.glsl");

            var dicomImage = new DicomImage(@"MRBRAIN.DCM");
            var image = dicomImage.RenderImage();
            bitmap = image.AsBitmap();

            UpdateScintilla(vertextShaderTextBox);
            UpdateScintilla(fragmentShaderTextBox);
        }

        private void UpdateScintilla(Scintilla scintilla)
        {
            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            scintilla.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            scintilla.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            scintilla.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            scintilla.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            scintilla.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
            scintilla.Lexer = Lexer.Cpp;

            // Set the keywords
            scintilla.SetKeywords(0, "break case continue default discard do else for if return switch while");
            scintilla.SetKeywords(1, "void bool int uint float vec2 vec3 vec4 bvec2 bvec3 bvec4 ivec2 ivec2 ivec3 uvec2 uvec2 uvec3 mat2 mat3 mat4 mat2x2 mat2x3 mat2x4 mat3x2 mat3x3 mat3x4 mat4x2 mat4x3 mat4x4 sampler1D sampler2D sampler3D samplerCube sampler2DRect sampler1DShadow sampler2DShadow sampler2DRectShadow sampler1DArray sampler2DArray sampler1DArrayShadow sampler2DArrayShadow samplerBuffer sampler2DMS sampler2DMSArray struct isampler1D isampler2D isampler3D isamplerCube isampler2DRect isampler1DArray isampler2DArray isamplerBuffer isampler2DMS isampler2DMSArray usampler1D usampler2D usampler3D usamplerCube usampler2DRect usampler1DArray usampler2DArray usamplerBuffer usampler2DMS usampler2DMSArray");
            scintilla.SetKeywords(1, "attribute centroid const flat in inout invariant noperspective out smooth uniform varying");
            scintilla.SetKeywords(1, "gl_BackColor gl_BackLightModelProduct gl_BackLightProduct gl_BackMaterial gl_BackSecondaryColor gl_ClipDistance gl_ClipPlane gl_ClipVertex gl_Color gl_DepthRange gl_DepthRangeParameters gl_EyePlaneQ gl_EyePlaneR gl_EyePlaneS gl_EyePlaneT gl_Fog gl_FogCoord gl_FogFragCoord gl_FogParameters gl_FragColor gl_FragCoord gl_FragDat gl_FragDept gl_FrontColor gl_FrontFacing gl_FrontLightModelProduct gl_FrontLightProduct gl_FrontMaterial gl_FrontSecondaryColor gl_InstanceID gl_Layer gl_LightModel gl_LightModelParameters gl_LightModelProducts gl_LightProducts gl_LightSource gl_LightSourceParameters gl_MaterialParameters gl_ModelViewMatrix gl_ModelViewMatrixInverse gl_ModelViewMatrixInverseTranspose gl_ModelViewMatrixTranspose gl_ModelViewProjectionMatrix gl_ModelViewProjectionMatrixInverse gl_ModelViewProjectionMatrixInverseTranspose gl_ModelViewProjectionMatrixTranspose gl_MultiTexCoord[0-7] gl_Normal gl_NormalMatrix gl_NormalScale gl_ObjectPlaneQ gl_ObjectPlaneR gl_ObjectPlaneS gl_ObjectPlaneT gl_Point gl_PointCoord gl_PointParameters gl_PointSize gl_Position gl_PrimitiveIDIn gl_ProjectionMatrix gl_ProjectionMatrixInverse gl_ProjectionMatrixInverseTranspose gl_ProjectionMatrixTranspose gl_SecondaryColor gl_TexCoord gl_TextureEnvColor gl_TextureMatrix gl_TextureMatrixInverse gl_TextureMatrixInverseTranspose gl_TextureMatrixTranspose gl_Vertex gl_VertexIDh");
            scintilla.SetKeywords(1, "gl_MaxClipPlanes gl_MaxCombinedTextureImageUnits gl_MaxDrawBuffers gl_MaxFragmentUniformComponents gl_MaxLights gl_MaxTextureCoords gl_MaxTextureImageUnits gl_MaxTextureUnits gl_MaxVaryingFloats gl_MaxVertexAttribs gl_MaxVertexTextureImageUnits gl_MaxVertexUniformComponents");
            scintilla.SetKeywords(1, "abs acos all any asin atan ceil clamp cos cross degrees dFdx dFdy distance dot equal exp exp2 faceforward floor fract ftransform fwidth greaterThan greaterThanEqual inversesqrt length lessThan lessThanEqual log log2 matrixCompMult max min mix mod noise[1-4] normalize not notEqual outerProduct pow radians reflect refract shadow1D shadow1DLod shadow1DProj shadow1DProjLod shadow2D shadow2DLod shadow2DProj shadow2DProjLod sign sin smoothstep sqrt step tan texture1D texture1DLod texture1DProj texture1DProjLod texture2D texture2DLod texture2DProj texture2DProjLod texture3D texture3DLod texture3DProj texture3DProjLod textureCube textureCubeLod transpose");
            scintilla.SetKeywords(0, "asm double enum extern goto inline long short sizeof static typedef union unsigned volatile");
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            while (glControl.IsIdle)
            {
                Render();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitOpenGL();
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            InitOpenGL();
        }

        private void vertextShaderTextBox_TextChanged(object sender, EventArgs e)
        {
            if (texture != 0) InitOpenGL();
        }

        private void fragmentShaderTextBox_TextChanged(object sender, EventArgs e)
        {
            if (texture != 0) InitOpenGL();
        }
    }
}
