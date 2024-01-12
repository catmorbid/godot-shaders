shader_type canvas_item;
uniform vec3 color1 = vec3(0.3,1.0,0.3);
uniform vec3 color2 = vec3(0.0, 0.2, 0.0);
void vertex() {
	// Called for every vertex the material is visible on.
}

vec4 stripe1(vec2 uv,vec4 pixelColor) {
	float stripe = mod(uv.x, 0.1);
	vec3 c = step(0.05, stripe) == 1.0 ? color1 : color2;
	return vec4(c*pixelColor.xyz, pixelColor.w);
}

vec4 stripe2(vec2 uv, vec4 pixelColor) {
    float interval = 0.02;
	float stripe = mod(uv.x+uv.y, interval);
    float a = step(stripe, interval/2.0);	
	vec3 c = a == 1.0 ? color1 : color2;
	return vec4(c*pixelColor.xyz,pixelColor.w);
}
vec4 stripe3(vec2 uv, vec4 pixelColor) {	    
    float interval = 0.02;
	float speed = 0.03;
	float stripe = mod(uv.x+uv.y+TIME*speed, interval);
    float a = step(stripe, interval/2.0);	
	vec3 c = a == 1.0 ? color1 : color2;
	return vec4(c*pixelColor.xyz,pixelColor.w);
}
void fragment() {
	// Called for every pixel the material is visible on.		
	//vec4 color = stripe1(UV, texture(TEXTURE, UV));
	COLOR = stripe3(UV, texture(TEXTURE, UV));
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
