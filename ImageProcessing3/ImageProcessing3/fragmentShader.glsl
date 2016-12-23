#version 400
//
// Fragment Shader
//
uniform sampler3D basic_texture;
uniform float draw_unit;
uniform float time;
uniform float z;
uniform mat4 rotateMatrix;
in vec2 vTexCoord;
out vec4 fragmentColor;

void main() {
	fragmentColor = vec4(0.0, 0.0, 0.0, 0.0);
	if (pow(vTexCoord.x - 0.5, 2) +
		pow(vTexCoord.y + 0.5, 2) +
		pow(z - 0.5, 2) < 0.03) {
		fragmentColor = vec4(0.6, 0, 0.0, 1);
		return;
	}
	float actualZ = z * (256 / 108) - 0.5;
	if (actualZ < 0 || actualZ > 1) return;
	vec4 pos = vec4(vTexCoord.x, vTexCoord.y, actualZ, 0);
	float r = texture(basic_texture, pos.xyz).r;
	fragmentColor.r = r;
	fragmentColor.b = r;
	fragmentColor.g = r;
	fragmentColor.a = r > 0.1 && r < 0.8 ? 1 : 0;
	fragmentColor /= 1.4;
}