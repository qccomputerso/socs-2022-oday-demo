from math import sqrt, floor

class vec2:
	def __init__(self, x, y):
		self.x = x
		self.y = y

	def mag(self):
		return sqrt(self.x ** 2 + self.y ** 2)
	
	def __add__(self, other):
		return vec2(self.x + other.x, self.y + other.y)

	def __sub__(self, other):
		return vec2(self.x - other.x, self.y - other.y)

	def __mul__(self, scale):
		return vec2(self.x * scale, self.y * scale)

	def __truediv__(self, scale):
		return vec2(self.x / scale, self.y / scale)

	def dot(self, other):
		return self.x * other.x + self.y * other.y

	def normalise(self):
		return self / self.mag()

	def floor(self):
		return vec2(floor(self.x), floor(self.y))


class vec3:
	def __init__(self, x, y, z):
		self.x = x
		self.y = y
		self.z = z

	def mag(self):
		return sqrt(self.x ** 2 + self.y ** 2 + self.x ** 2)
	
	def __add__(self, other):
		return vec3(self.x + other.x, self.y + other.y, self.z + other.z)

	def __sub__(self, other):
		return vec3(self.x - other.x, self.y - other.y, self.z - other.z)

	def __mul__(self, scale):
		return vec3(self.x * scale, self.y * scale, self.z * scale)

	def __truediv__(self, scale):
		return vec3(self.x / scale, self.y / scale, self.z / scale)

	def dot(self, other):
		return self.x * other.x + self.y * other.y + self.z * other.z

	def normalise(self):
		return self / self.mag()

	def floor(self):
		return vec3(floor(self.x), floor(self.y), floor(self.z))

	def toRGB(self):
		return f"#{'{0:x}'.format(int(self.x * 255)).zfill(2)}{'{0:x}'.format(int(self.y * 255)).zfill(2)}{'{0:x}'.format(int(self.z * 255)).zfill(2)}"