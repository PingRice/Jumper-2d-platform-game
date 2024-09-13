shader_type canvas_item;

void vertex() {
	// Called for every vertex the material is visible on.
	
}

uniform vec4 color = hint_color;

void fragment() {
	// Called for every pixel the material is visible on.
	vec4 pixelColor = texture(TEXTURE, UV);
	float brightness = (pixelColor.r + pixelColor.g + pixelColor.b) / 3.0;
	vec4 greyScale = vec4(brightness, brightness, brightness, pixelColor.a);
	COLOR = greyScale * color;
	
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
	
//}
