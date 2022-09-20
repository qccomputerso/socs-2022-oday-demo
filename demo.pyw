from time import sleep
import tkinter as tk
from vec import vec2, vec3
from perlin import Perlin3D, PerlinAccessor

root = tk.Tk()
root.title("SoCS 22-23 O'Day Demo")

WIDTH = 300
HEIGHT = 300
canvas = tk.Canvas(root, width = WIDTH, height = HEIGHT, bg = "#000000")
canvas.pack()

landscape = PerlinAccessor([
	(Perlin3D(31415926), 97, 0.75),
	(Perlin3D(31415927), 66, 0.15),
	(Perlin3D(31415929), 38, 0.1),
])

img = tk.PhotoImage(width = WIDTH, height = HEIGHT)
canvas.create_image((0, 0), anchor = tk.NW, image = img, state = "normal")

t = 0
while 1:
	t += 1
	for i in range(WIDTH + 1):
		for j in range(HEIGHT + 1):
			l = (1.03 - abs(landscape.valueAt(vec3(i, j, t)))) ** 20
			img.put((vec3(l, l * 0.9, l * 1.3)).toRGB(), (i, j))
	root.update()
	sleep(0.03)

#t = 0
#while 1:
#	t += 1
#	canvas.delete("all")
#	canvas.create_rectangle(0, 0, WIDTH, HEIGHT, fill = "#0085ff")
#	for i in range(WIDTH + 1):
#		l = (landscape.valueAt(vec2(i, t)) + 1) / 2 * HEIGHT
#		canvas.create_rectangle(i, 0, i + 1, l, fill = "black")
#	root.update()