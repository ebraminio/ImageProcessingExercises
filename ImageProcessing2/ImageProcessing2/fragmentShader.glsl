// Fragment Shader
//
uniform sampler2D basic_texture;
uniform float draw_unit;
uniform float time;
in vec2 vTexCoord;
out vec4 fragmentColor;

void main() {
	// Gaussian filter
	//mat3 filter = mat3(1, 2, 1, 2, 4, 2, 1, 2, 2);
	// Laplacian filter
	mat3 filter = mat3(0, -1, 0, -1, 4, -1, 0, -1, 0);

	fragmentColor += texture(basic_texture, vec2(vTexCoord.x, vTexCoord.y));

	for (int i = -1; i <= 1; ++i)
		for (int j = -1; j <= 1; ++j)
			fragmentColor += texture(basic_texture, vec2(vTexCoord.x + draw_unit * i, vTexCoord.y + draw_unit * j)) * filter[i + 1][j + 1];

	/*fragmentColor /= 16;

	vec2 pixelRight_Coord = vTexCoord + vec2(draw_unit, 0.0),
		pixelLeft_Coord = vTexCoord + vec2(-draw_unit, 0.0),
		pixelTop_Coord = vTexCoord + vec2(0.0, draw_unit),
		pixelBottom_Coord = vTexCoord + vec2(0.0, -draw_unit);
	vec2 gradient =
		vec2(length(texture2D(basic_texture, pixelRight_Coord).xyz
			- texture2D(basic_texture, pixelLeft_Coord).xyz),
			length(texture2D(basic_texture, pixelTop_Coord).xyz
				- texture2D(basic_texture, pixelBottom_Coord).xyz));
	fragmentColor = vec4(length(gradient)) * 1;*/
}