Text of the day can be found here:
https://adventofcode.com/2023/day/5

### PART ONE
Part one was simple. Go through all of the seeds IDs, and compute through almanac "formulas".

### PART TWO
I did some magic there thanks to the Reddit comments. All of the location values are grouped, so you don't need to go through all of them, you can go and check every 1000th seed and find the smallest one. also, you can do this for every seed range (no need to calculate range intersections).
So, I ran through, all of the ranges, and found the range with the seed with the smallest `location` value. and iterate through that only.

### Good look!
