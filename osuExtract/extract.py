f = open("osu.txt", "r")
result = ""
for line in f:
  time = int(line.split(",")[2])
  time = time / 1000
  result = result + str(time) + "f, "

  if "|" in line: 
    slideTime = line.split(",")[7]
    slideTime = slideTime.replace('\n', "")
    slideEndTime = time + int(slideTime) / (1.4 * 100) * 588.2352941 / 1000
    result = result + str(slideEndTime) + "f, "

  

print(result)
f.close()