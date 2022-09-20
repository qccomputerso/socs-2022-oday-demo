from time import sleep
import tkinter as tk
from vec import vec2, vec3
from perlin import Perlin2D, PerlinAccessor2D

root = tk.Tk()
root.title("SoCS 22-23 O'Day Demo")

WIDTH = 600
HEIGHT = 300
canvas = tk.Canvas(root, width = WIDTH, height = HEIGHT, bg = "#000000")
canvas.pack()
# img = tk.PhotoImage(width = WIDTH, height = HEIGHT)
# canvas.create_image((0, 0), anchor = tk.NW, image = img, state = "normal")

landscape = PerlinAccessor2D([
	(Perlin2D(31415926), 203, 0.7),
	(Perlin2D(31415927), 66, 0.13),
	(Perlin2D(31415929), 38, 0.1),
	(Perlin2D(31415935), 19, 0.07)
])


#for i in range(WIDTH + 1):
#	for j in range(HEIGHT + 1):
#		l = (landscape.valueAt(vec2(i, j)) + 1) / 2
#		img.put(vec3(l, l, l).toRGB(), (i, j))

t = 0
while 1:
	t += 1
	canvas.delete("all")
	canvas.create_rectangle(0, 0, WIDTH, HEIGHT, fill = "#0085ff")
	for i in range(WIDTH + 1):
		l = (landscape.valueAt(vec2(i, t)) + 1) / 2 * HEIGHT
		canvas.create_rectangle(i, 0, i + 1, l, fill = "black")
	root.update()