from vec import vec2, vec3
from perlin import Perlin2D, PerlinAccessor2D

landscape = PerlinAccessor2D(Perlin2D(31415926), 100)

print(landscape.valueAt(vec2(20, 99)))
print("-----")
print(landscape.valueAt(vec2(20, 100)))
print("-----")
print(landscape.valueAt(vec2(20, 101)))