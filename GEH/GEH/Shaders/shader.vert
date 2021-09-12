#version 330
layout (location = 0) in  vec3 vPosition;
layout (location = 1) in  vec3 vColor;
out vec4 color;
uniform mat4 all;
void main()
{
    gl_Position = all*vec4(vPosition, 1.0);
    color = vec4( vColor, 1.0);
}