from vec import vec2, vec3
import math

def PRNGfrom3(a, b, seed):
	d = (a * 100 + 1000) + (b * 100 + 1000) * seed * 15485863
	return (d * d * d % 2038074743) / 2038074743

# CREDIT: https://gpfault.net/posts/perlin-noise.txt.html
class Perlin2D:
	def __init__(self, seed, lerp = lambda t : t*t*t*(t*(t*6.0 - 15.0) + 10.0)):
		self.seed = seed
		self.points = dict()
		self.lerp = lerp

	def grad(self, p):
		key = f"{p.x}_{p.y}"
		if key not in self.points:
			self.points[key] = vec2(
				PRNGfrom3(p.x, p.y, self.seed) * 2 - 1,
				PRNGfrom3(p.x, p.y, self.seed + 1) * 2 - 1
			)
		return self.points[key]

	def valueAt(self, p):
		p0 = p.floor()
		p1 = p0 + vec2(1, 0)
		p2 = p0 + vec2(0, 1)
		p3 = p0 + vec2(1, 1)

		g0 = self.grad(p0)
		g1 = self.grad(p1)
		g2 = self.grad(p2)
		g3 = self.grad(p3)
		t0 = p.x - p0.x
		fade_t0 = self.lerp(t0)
		t1 = p.y - p0.y
		fade_t1 = self.lerp(t1)

		p0p1 = (1.0 - fade_t0) * g0.dot(p - p0) + fade_t0 * g1.dot(p - p1)
		p2p3 = (1.0 - fade_t0) * g2.dot(p - p2) + fade_t0 * g3.dot(p - p3)

		return (1.0 - fade_t1) * p0p1 + fade_t1 * p2p3

class PerlinAccessor2D:
	def __init__(self, accessList):
		self.accessList = accessList
	
	def valueAt(self, p):
		v = 0
		for i in self.accessList:
			v += i[0].valueAt(p / i[1]) * i[2]
		return v