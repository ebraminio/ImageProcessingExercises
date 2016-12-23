#version 400
//
// Vertex Shader
//
uniform float zoom;
uniform float time;
uniform float z;
uniform mat4 rotateMatrix;
in vec2 vp;
out vec2 vTexCoord;

void main() {
	gl_Position = rotateMatrix * vec4(vec3(vp, z) - vec3(0.5), 1.0);
	gl_Position.xyz *= zoom;
	vTexCoord = vp;
	vTexCoord.y *= -1;
}
