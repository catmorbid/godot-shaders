shader_type canvas_item;
uniform vec3 color1 = vec3(1.0,1.0,1.0);
uniform vec3 color2 = vec3(1.0, 0.2, 0.2);
void vertex() {
	// Called for every vertex the material is visible on.
}

vec4 stripe1(vec2 uv,vec3 pixelColor) {
	float stripe = mod(uv.x, 0.1);
	vec3 c = step(0.05, stripe) == 1.0 ? color1 : color2;
	return vec4(c*pixelColor, 1.0);
}

vec4 stripe2(vec2 uv, vec3 pixelColor) {	    
    float interval = 0.1;
	float stripe = mod(uv.x+uv.y, interval);
    float a = step(stripe, interval/2.0);	
	vec3 c = a == 1.0 ? color1 : color2;
	return vec4(c*pixelColor,1.0);
}
void fragment() {
	// Called for every pixel the material is visible on.		
	vec4 color = stripe1(UV, texture(TEXTURE, UV).xyz);
	COLOR = stripe2(UV, color.xyz);
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
